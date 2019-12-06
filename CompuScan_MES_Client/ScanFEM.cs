using System;
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
        #region [Variables]
        Hub hub = Hub.Default;
        private S7Client
            transactClient = new S7Client();

        private bool
            hasReadOne = false,
            isConnected = false,
            handshakeCleared = false;

        private int
            oldReadTransactionID = 0,
            numOfProcesses,
            skidID;

        public static byte[]
            transactReadBuffer = new byte[402],
            transactWriteBuffer = new byte[402];

        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false);

        private Thread readTransact,
            handshakeThr;

        private string
            stationID,
            entireSequence,
            FEMLabel;
            

        private string[] FEMLabelParts;
        private DataTable dt;
        #endregion


        public ScanFEM(string stationID, int skidID)
        {
            InitializeComponent();
            this.stationID = stationID;
            this.skidID = skidID;
        }

        private void ScanFEM_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            if (isConnected)
            {
                readTransact = new Thread(ReadTransactionDB);
                readTransact.IsBackground = true;
                readTransact.Start();

                handshakeThr = new Thread(Handshake);
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

        #region [PLC DB Read Threads]
        private void ReadTransactionDB()
        {
            while (isConnected)
            {
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                if (readTransactionID == 0 && !handshakeCleared)
                {
                    handshakeCleared = true;
                    if (isConnected)
                    {
                        S7.SetByteAt(transactWriteBuffer, 45, 1);
                        S7.SetByteAt(transactWriteBuffer, 94, 20); // Send Equipment ID *** HANDHELD SCANNER
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("-------------------------" +
                                          "\nTransaction ID : 1" +
                                          "\nEquipment ID : 20" +
                                          "\nPLC Write Result : " + result1 +
                                          "\n-------------------------");
                    }
                }

                if ((readTransactionID != oldReadTransactionID) && handshakeCleared)
                {
                    Console.WriteLine("Transaction ID : " + readTransactionID);
                    oSignalTransactEvent.Set();
                    oldReadTransactionID = readTransactionID;
                }

                Thread.Sleep(50);
            }
        }
        #endregion

        #region [Handshake Structure]
        private void Handshake()
        {
            while (isConnected)
            {
                oSignalTransactEvent.WaitOne();
                oSignalTransactEvent.Reset();

                switch (readTransactionID)
                {
                    case 2:
                        FEMLabel = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        Console.WriteLine(FEMLabel);

                        FEMLabelParts = FEMLabel.Split(',');

                        if (!FEMLabel.Equals(""))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                FEM_txt.Text = FEMLabel.ToString();
                            });

                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();
                                dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [Processes], [Sequence] FROM Sequences WHERE [Model] = '" + FEMLabelParts[5] + "' AND [Variant] = '" + FEMLabelParts[6] + "'", conn);
                                da.Fill(dt);
                            }

                            if (dt.Rows.Count > 0)
                            {
                                string sequenceNum = "1";
                                foreach (DataRow row in dt.Rows)
                                {
                                    FEMLabelParts = row["Sequence"].ToString().Split('-');
                                    numOfProcesses = (int)row["Processes"];
                                    entireSequence = row["Sequence"].ToString();
                                }

                                //currentSequenceStep = arr[0].Split(',');
                                S7.SetByteAt(transactWriteBuffer, 45, 99);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, numOfProcesses.ToString());
                                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("-------------------------" +
                                      "\nTransaction ID : 99" + 
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
                        else // Did not receive any data from plc
                        {
                            S7.SetByteAt(transactWriteBuffer, 45, 1);
                            S7.SetByteAt(transactWriteBuffer, 48, 98);
                            int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("-------------------------" +
                                      "\nTransaction ID : " + readTransactionID +
                                      "\nResult : No data received from PLC." +
                                      "\nErrorcode : 98" +
                                      "\nPLC Write Result : " + result3 +
                                      "\n-------------------------");
                        }
                        
                        Thread.Sleep(50);
                        break;
                    case 100:
                        if (FEMLabelParts != null)
                        {
                            Console.WriteLine("-------------------------" +
                                  "\nTransaction ID : " + readTransactionID +
                                  "\nResult : Handshake done... Starting next screen." +
                                  "\n-------------------------");
                            string[] nextStep = FEMLabelParts[0].Split(',');
                            hub.PublishAsync(new ScreenChangeObject(nextStep[0], nextStep[1], entireSequence, 0, skidID, FEMLabel));
                            this.Close();
                        }
                        break;
                    default:
                        break;
                }
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
