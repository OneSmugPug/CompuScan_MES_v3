using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Client
{
    public partial class AwaitPart : Form
    {
        #region [Variables]
        private S7Client
            transactClient = new S7Client(),
            palletClient = new S7Client();

        private bool 
            hasReadOne = false, 
            isConnected = false;

        private int oldReadTransactionID = 0;
        private ManualResetEvent oSignalTransactEvent = new ManualResetEvent(false);
        private ManualResetEvent oSignalPalletEvent = new ManualResetEvent(false);

        public static byte[] 
            transactReadBuffer = new byte[296],
            transactWriteBuffer = new byte[296],
            palletReadBuffer = new byte[296],
            palletWriteBuffer = new byte[296];
        #endregion

        public AwaitPart()
        {
            InitializeComponent();
        }

        private void AwaitPart_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            Thread read = new Thread(ReadTransactionDB);
            read.Start();

            Thread handshakeThr = new Thread(Handshake);
            handshakeThr.Start();
        }

        #region [PLC DB Read/Write]
        private void ReadAllValues()
        {
            transactClient.DBRead(3000, 0, transactReadBuffer.Length, transactReadBuffer);//1110

            lineID = S7.GetStringAt(transactReadBuffer, 0);

            identifier = S7.GetStringAt(transactReadBuffer, 22);

            identifierCount = S7.GetByteAt(transactReadBuffer, 44); // Number of entries in the database (Does not really need to read but write it to plc?)

            readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

            channelStatus = S7.GetByteAt(transactReadBuffer, 46);

            stationStatus = S7.GetByteAt(transactReadBuffer, 47);

            errorCode = S7.GetByteAt(transactReadBuffer, 48);

            userName = S7.GetStringAt(transactReadBuffer, 50);

            equipmentID = S7.GetByteAt(transactReadBuffer, 94);

            productionData = S7.GetStringAt(transactReadBuffer, 96);
        }

        public void WriteBackToPLC()
        {
            //S7.SetStringAt(writeBuffer, 0, 20, lineID);
            //S7.SetStringAt(writeBuffer, 22, 20, identifier);
            //S7.SetByteAt(writeBuffer, 44, (byte)identifierCount);
            S7.SetByteAt(transactWriteBuffer, 45, (byte)writeTransactionID);
            //S7.SetByteAt(writeBuffer, 46, (byte)channelStatus);
            //S7.SetByteAt(writeBuffer, 47, (byte)stationStatus);
            //S7.SetByteAt(writeBuffer, 48, (byte)errorCode);
            //S7.SetStringAt(writeBuffer, 50, 20, userName);
            //S7.SetByteAt(writeBuffer, 94, (byte)equipmentID);
            //S7.SetStringAt(writeBuffer, 96, 200, productionData);
            int writeResult = transactClient.DBWrite(3001, 0, transactWriteBuffer.Length, transactWriteBuffer);//1111
            if (writeResult == 0)
            {
                Console.WriteLine("==> Successfully wrote to PLC");
            }
        }
        #endregion

        #region [Establish Connection]
        private void EstablishConnection()
        {
            transactClient.ConnectTo("192.168.1.1", 0, 1);
            palletClient.ConnectTo("192.168.1.1", 0, 1);

            if (transactClient.Connected && palletClient.Connected)
                isConnected = true;
            //if (!isConnected)
            //{
                

            //    if (connectionResult1 == 0 && connectionResult2 == 0)
            //    {
            //        Console.WriteLine("=========Connection success===========");
            //        isConnected = true;
            //    }
            //    else
            //    {
            //        Console.WriteLine("=========Connection error============");
            //        isConnected = false;
            //        Console.WriteLine(connectionResult1);
            //        Console.WriteLine(connectionResult2);
            //        return;

            //    }
            //}
        }
        #endregion

        #region [PLC DB Read Threads]
        private void ReadTransactionDB()
        {
            while (isConnected)
            {
                transactClient.DBRead(3000, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                if (readTransactionID != oldReadTransactionID)
                {
                    oSignalTransactEvent.Set();
                    oldReadTransactionID = readTransactionID;
                }

                Thread.Sleep(50);
            }
        }

        private void ReadPalletDB()
        {
            while (isConnected)
            {
                palletClient.DBRead(3107, 0, palletReadBuffer.Length, palletReadBuffer);//1110
                skidPresent = S7.GetBitAt(palletReadBuffer, 0, 0);
                addPallet = S7.GetBitAt(palletReadBuffer, 0, 2);

                if (skidPresent && !addPallet)
                    oSignalPalletEvent.Set();

                Thread.Sleep(50);
            }
        }
        #endregion

        #region [Handshake Structure]
        private void Handshake()
        {
            while (isConnected)
            {
                //ReadAllValues();
                //client.DBRead(3000, 0, readBuffer.Length, readBuffer);//1110
                //readTransactionID = S7.GetByteAt(readBuffer, 45);
                oSignalTransactEvent.WaitOne();
                oSignalTransactEvent.Reset();
                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {
                            //WriteToSQLDataBase();
                            Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + " back to PLC.");
                            writeTransactionID = readTransactionID + 1;
                            WriteBackToPLC();
                            hasReadOne = true;
                            Thread.Sleep(50);
                        }
                        break;
                    case 3:
                    case 5:
                    case 7:
                    case 9:
                    case 11:
                    case 13:
                    case 15:
                    case 17:
                    case 19:
                    case 21:
                    case 23:
                    case 25:
                    case 27:
                    case 29:
                    case 31:
                    case 33:
                    case 35:
                        if (hasReadOne)
                        {
                            //WriteToSQLDataBase();
                            Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + " back to PLC.");
                            writeTransactionID = readTransactionID + 1;
                            WriteBackToPLC();
                        }
                        break;
                    case 99:
                        Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
                        writeTransactionID = 100;
                        WriteBackToPLC();
                        hasReadOne = false;
                        break;
                    default:
                        break;
                }
                //Thread.Sleep(50);
            }
        }
        #endregion

        #region [PLC Getters/Setters]
        public string lineID { get; set; }
        public string identifier { get; set; }
        public int identifierCount { get; set; }
        public int readTransactionID { get; set; }
        public int writeTransactionID { get; set; }
        public int channelStatus { get; set; }
        public int stationStatus { get; set; }
        public int errorCode { get; set; }
        public string userName { get; set; }
        public int equipmentID { get; set; }
        public string productionData { get; set; }
        public bool skidPresent { get; set; }
        public bool addPallet { get; set; }
        public int palletNum { get; set; }
        public byte palletID0 { get; set; }
        public byte palletID1 { get; set; }
        public byte palletID2 { get; set; }
        public byte palletID3 { get; set; }
        public byte palletID4 { get; set; }
        public byte palletID5 { get; set; }
        public byte palletID6 { get; set; }
        public byte palletID7 { get; set; }

        #endregion

        private void AwaitPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            palletClient.Disconnect();
            isConnected = false;
        }
    }
}
