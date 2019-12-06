using PubSub;
using Sharp7;
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

namespace CompuScan_MES_Client
{
    public partial class AwaitPart : Form
    {
        #region [Variables]
        Hub hub = Hub.Default;

        private S7Client
            transactClient = new S7Client(),
            palletClient = new S7Client();

        private bool 
            hasReadTwo = false, 
            isConnected = false,
            handshakeCleared = false;

        private int oldReadTransactionID = 0;
        private DataTable dt;
        private MainPage parent = null;
        private Label skidID;
        private string stationID;

        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false);

        public static byte[] 
            transactReadBuffer = new byte[402],
            transactWriteBuffer = new byte[402],
            palletReadBuffer = new byte[12],
            palletWriteBuffer = new byte[2];

        private Thread 
            readTransact,
            handshakeThr;
        #endregion

        #region [Form Load]
        public AwaitPart(string stationID)
        {
            InitializeComponent();
            this.stationID = stationID;
            palletNum = 0;
        }

        private void AwaitPart_Load(object sender, EventArgs e)
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

            parent = (MainPage)this.Owner;
        }
        #endregion


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
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                if (readTransactionID == 0 && !handshakeCleared)
                {
                    handshakeCleared = true;
                    if (isConnected)
                    {
                        S7.SetByteAt(transactWriteBuffer, 45, 1);
                        S7.SetByteAt(transactWriteBuffer, 94, 1);
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("-------------------------" + "\nTransaction ID : 1" +
                                                                          "\nWrite Result : " + result1 +
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
                        Console.WriteLine("Awaiting part complete");
                        S7.SetByteAt(transactWriteBuffer, 45, 99);
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        break;
                    case 100:
                        if (stationID[1].Equals('1'))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                hub.PublishAsync(new ScreenChangeObject("1"));
                                this.Close();
                            });
                        }

                        else
                        {
                            // OBTAIN SKID ID FROM DATABASE AND SEND IT TO THE SCAN FEM LABEL SCREEN
                            this.Invoke((MethodInvoker)delegate
                            {
                                hub.PublishAsync(new ScreenChangeObject("2"));
                                this.Close();
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        public void SetLabel(Label skidID)
        {
            this.skidID = skidID;
        }

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
        private void AwaitPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
