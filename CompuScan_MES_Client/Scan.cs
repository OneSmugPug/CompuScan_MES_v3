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
using Sharp7;

namespace CompuScan_MES_Client
{
    public partial class Scan : Form
    {
        private S7Client
            transactClient = new S7Client();
        private bool
            hasReadOne = false,
            isConnected = false;
        private int 
            oldReadTransactionID = 0;
        public static byte[]
            transactReadBuffer = new byte[296],
            transactWriteBuffer = new byte[296];
        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false);

        public Scan(int stationNum)
        {
            InitializeComponent();
            txt_station_num.Text = stationNum.ToString();
        }

        private void Scan_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            if (isConnected)
            {
                Thread readTransact = new Thread(ReadTransactionDB);
                readTransact.IsBackground = true;
                readTransact.Start();

                Thread handshakeThr = new Thread(Handshake);
                handshakeThr.IsBackground = true;
                handshakeThr.Start();
            }

        }

        #region [Establish Connection]
        private void EstablishConnection()
        {
            transactClient.ConnectTo("192.168.1.1", 0, 1);

            if (transactClient.Connected)
                isConnected = true;
            else MessageBox.Show("Connection to PLC on 192.168.1.1 unsuccessful.", "No PLC Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                oSignalTransactEvent.WaitOne(); //Thread waits for new value to be read by PLC DB Read Thread
                oSignalTransactEvent.Reset();

                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {
                            //WriteToSQLDataBase();
                            //Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + " back to PLC.");
                            //writeTransactionID = readTransactionID + 1;

                            //Thread readPallet = new Thread(ReadPalletDB);
                            //readPallet.IsBackground = true;
                            //readPallet.Start();

                            //bool skidIDSet = false;
                            //while (!skidIDSet)
                            //{
                            //    if (!skidID.Text.Equals("Skid ID: -"))
                            //        skidIDSet = true;
                            //}

                            //oSignalSendPalletEvent.WaitOne();
                            //oSignalSendPalletEvent.Reset();

                            WriteBackToPLC();
                            hasReadOne = true;

                            Thread.Sleep(50);
                        }
                        break;
                    case 99:
                        Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
                        writeTransactionID = 100;
                        WriteBackToPLC();
                        hasReadOne = false;

                        Thread.Sleep(50);
                        break;
                    default:
                        break;
                }
            }
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
        #endregion

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

        #endregion

        #region [Form Closing]
        private void Scan_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
