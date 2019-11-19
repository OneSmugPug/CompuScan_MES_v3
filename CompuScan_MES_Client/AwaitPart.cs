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
        private S7Client client = new S7Client();
        private bool hasReadOne = false, isConnected = false;
        public static byte[] readBuffer = new byte[296];
        public static byte[] writeBuffer = new byte[296];
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

        public AwaitPart(int stationNum)
        {
            InitializeComponent();
            txt_station_num.Text = stationNum.ToString();
        }
        private void ReadAllValues()
        {
            client.DBRead(3000, 0, readBuffer.Length, readBuffer);//1110

            lineID = S7.GetStringAt(readBuffer, 0);

            identifier = S7.GetStringAt(readBuffer, 22);

            identifierCount = S7.GetByteAt(readBuffer, 44); // Number of entries in the database (Does not really need to read but write it to plc?)

            readTransactionID = S7.GetByteAt(readBuffer, 45);

            channelStatus = S7.GetByteAt(readBuffer, 46);

            stationStatus = S7.GetByteAt(readBuffer, 47);

            errorCode = S7.GetByteAt(readBuffer, 48);

            userName = S7.GetStringAt(readBuffer, 50);

            equipmentID = S7.GetByteAt(readBuffer, 94);

            productionData = S7.GetStringAt(readBuffer, 96);
        }

        public void WriteBackToPLC()
        {
            //S7.SetStringAt(writeBuffer, 0, 20, lineID);
            //S7.SetStringAt(writeBuffer, 22, 20, identifier);
            //S7.SetByteAt(writeBuffer, 44, (byte)identifierCount);
            S7.SetByteAt(writeBuffer, 45, (byte)writeTransactionID);
            //S7.SetByteAt(writeBuffer, 46, (byte)channelStatus);
            //S7.SetByteAt(writeBuffer, 47, (byte)stationStatus);
            //S7.SetByteAt(writeBuffer, 48, (byte)errorCode);
            //S7.SetStringAt(writeBuffer, 50, 20, userName);
            //S7.SetByteAt(writeBuffer, 94, (byte)equipmentID);
            //S7.SetStringAt(writeBuffer, 96, 200, productionData);
            int writeResult = client.DBWrite(3001, 0, writeBuffer.Length, writeBuffer);//1111
            if (writeResult == 0)
            {
                Console.WriteLine("==> Successfully wrote to PLC");
            }
        }

        public void SetS7Client(S7Client client)
        {
            //this.client = client;
        }

        private void AwaitPart_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            Thread handshakeThr = new Thread(Handshake);
            handshakeThr.Start();
        }

        #region [Establish Connection]
        private void EstablishConnection()
        {
            int connectionResult1 = client.ConnectTo("192.168.1.1", 0, 1);
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

        private void Handshake()
        {
            while (isConnected)
            {
                //ReadAllValues();
                client.DBRead(3000, 0, readBuffer.Length, readBuffer);//1110
                readTransactionID = S7.GetByteAt(readBuffer, 45);
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
                Thread.Sleep(20);
            }
        }

        private void AwaitPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
            isConnected = false;
        }
    }
}
