﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using PubSub;
using Sharp7;

namespace CompuScan_MES_Client
{
    public partial class ScanFEM : Form
    {
        Hub hub = Hub.Default;
        private S7Client
            transactClient = new S7Client();
        private bool
            hasReadOne = false,
            isConnected = false;
        private int
            oldReadTransactionID = 0,
            numOfProcesses;
        public static byte[]
            transactReadBuffer = new byte[402],
            transactWriteBuffer = new byte[402];
        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false);
        private string 
            stationID,
            entireSequence;
        private string[] arr;
        private DataTable dt;

        public ScanFEM(string stationID)
        {
            InitializeComponent();
            this.stationID = stationID;
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
            if (isConnected)
            {
                S7.SetByteAt(transactWriteBuffer, 45, 1);
                S7.SetStringAt(transactWriteBuffer, 96, 200, ((short)palletNum).ToString()); // Send Equipment ID *** HANDHELD SCANNER
                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                Console.WriteLine("-------------------------" +
                                  "\nTransaction ID : 1" +
                                  "\nEquipment ID : " + equipmentID +
                                  "\nPLC Write Result : " + result1 +
                                  "\n-------------------------");
            }

            while (isConnected)
            {
                oSignalTransactEvent.WaitOne();
                oSignalTransactEvent.Reset();

                switch (readTransactionID)
                {
                    case 2:
                        string modelVar = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        Console.WriteLine(modelVar);

                        arr = modelVar.Split(',');

                        if (!modelVar.Equals(""))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                scan_txt.Text = modelVar.ToString();
                            });

                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();
                                dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [Processes], [Sequence] FROM Sequences WHERE [Model] = '" + arr[5] + "' AND [Variant] = '" + arr[6] + "'", conn);
                                da.Fill(dt);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                string sequenceNum = "1";
                                foreach (DataRow row in dt.Rows)
                                {
                                    // step screen (scan...) , count , part numbers - '' , '' , '' -
                                    // get the sequence from the row with part numbers
                                    // get number of processes and send it to plc
                                    arr = row["Sequence"].ToString().Split('-');
                                    numOfProcesses = (int)row["Processes"];
                                    entireSequence = row["Sequence"].ToString();
                                }
                                //currentSequenceStep = arr[0].Split(',');
                                S7.SetByteAt(transactWriteBuffer, 45, 99);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, numOfProcesses.ToString());
                                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("-------------------------" +
                                      "\nTransaction ID : " + readTransactionID +
                                      "\nSequence Number : " + sequenceNum +
                                      "\nPLC Write Result : " + result1 +
                                      "\n-------------------------");
                            }
                            else // Did not find the model & variant in the database
                            {
                                S7.SetByteAt(transactWriteBuffer, 45, 1);
                                S7.SetByteAt(transactWriteBuffer, 48, 99);
                                int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("-------------------------" +
                                      "\nTransaction ID : " + readTransactionID +
                                      "\nResult : Did not find model/variant in database." +
                                      "\nErrorcode : 99" +
                                      "\nPLC Write Result : " + result2 +
                                      "\n-------------------------");
                            }
                        }
                        else
                        {
                            S7.SetByteAt(transactWriteBuffer, 45, 1);
                            S7.SetByteAt(transactWriteBuffer, 48, 98);
                            int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("-------------------------" +
                                      "\nTransaction ID : " + readTransactionID +
                                      "\nResult : No data recieved from PLC." +
                                      "\nErrorcode : 98" +
                                      "\nPLC Write Result : " + result3 +
                                      "\n-------------------------");
                        }
                        
                        Thread.Sleep(50);
                        break;
                    case 100:
                        Console.WriteLine("-------------------------" +
                                  "\nTransaction ID : " + readTransactionID +
                                  "\nResult : Handshake done... Starting next screen." +
                                  "\n-------------------------");
                        string[] nextStep = arr[0].Split(',');
                        hub.Publish(new ScreenChangeObject(nextStep[0], nextStep[1], entireSequence, 1));
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
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                if ((readTransactionID != oldReadTransactionID) && (readTransactionID != 0))
                {
                    Console.WriteLine("Transaction ID : " + readTransactionID + "---------------------------------------");
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
            transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110

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
            int writeResult = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);//1111
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