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
using PubSub;
using Sharp7;

namespace CompuScan_MES_Client
{
    public partial class Pick : Form
    {
        Hub hub = Hub.Default;
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
        private int
            curPickNum = 0,
            pickNum,
            txtBoxY = 38;
        private Dictionary<string,TextBox> txtBoxes;
        private string stationID;

        public Pick(int pickNum, string stationID)
        {
            InitializeComponent();
            this.pickNum = pickNum;
            this.stationID = stationID;
            current_pick.Text = curPickNum.ToString();
            total_pick.Text = pickNum.ToString();
            txtBoxes = new Dictionary<string, TextBox>();
        }

        private void Pick_Load(object sender, EventArgs e)
        {
            //EstablishConnection();

            if (isConnected)
            {
                PopulatePickTextBoxes();

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
                S7.SetStringAt(transactWriteBuffer, 96, 200, ((short)palletNum).ToString()); // Send Equipment ID *** small label scanner
                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                Console.WriteLine("-------------------------" +
                                  "\nTransaction ID : 1" +
                                  "\nEquipment ID : " + equipmentID +
                                  "\nPLC Write Result : " + result1 +
                                  "\n-------------------------");
            }

            int count = 1;
            string scanData;
            
            while (isConnected)
            {

                oSignalTransactEvent.WaitOne();
                oSignalTransactEvent.Reset();

                
                switch (readTransactionID)
                {
                    case 2:

                        scanData = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        Console.WriteLine(scanData);
                        
                        if (!scanData.Equals(""))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                TextBox temp;
                                txtBoxes.TryGetValue("pickedBox" + count, out temp);
                                count++;
                                temp.Text = scanData.ToString();
                            });
                        
                            // LOG TO DATABASE
                        
                            S7.SetByteAt(transactWriteBuffer, 45, 2);
                            int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("Write Result : " + result2 + "---------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("NO DATA FOUND");
                        }
                        
                        Thread.Sleep(50);
                        
                        break;
                    case 4:
                    case 6:
                    case 8:
                    case 10:
                    case 12:
                    case 14:
                    case 16:
                    case 18:
                    case 20:
                    case 22:
                        scanData = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        Console.WriteLine(scanData);

                        if (!scanData.Equals(""))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                TextBox temp;
                                txtBoxes.TryGetValue("pickedBox" + count, out temp);
                                count++;
                                temp.Text = scanData.ToString();
                            });

                            // LOG TO DATABASE

                            S7.SetByteAt(transactWriteBuffer, 45, (byte)(readTransactionID + 1));
                            int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("Write Result : " + result2 + "---------------------------------------");
                            hasReadOne = true;
                        }
                        else
                        {
                            Console.WriteLine("NO DATA FOUND");
                        }

                        Thread.Sleep(50);
                        break;
                    case 100:
                        Console.WriteLine("PLC Requested to stop communication...");
                        S7.SetByteAt(transactWriteBuffer, 45, 100);
                        int result4 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("Write Result : " + result4 + "---------------------------------------");
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
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                //Console.WriteLine(readTransactionID);

                if (readTransactionID != oldReadTransactionID)
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

        #region [Populate Panel with Textboxes]
        private void PopulatePickTextBoxes()
        {
            for (int i = 0; i < pickNum; i++)
            {
                TextBox txtReq = new TextBox();
                txtReq.Name = "reqBox" + (i + 1);
                txtReq.Location = new Point(lbl_Req.Location.X - 6, lbl_Req.Location.Y + txtBoxY);
                txtReq.Size = new Size(100, 20);
                txtReq.Text = txtReq.Name;
                TextBox txtPicked = new TextBox();
                txtPicked.Name = "pickedBox" + (i + 1);
                txtPicked.Location = new Point(lbl_picked.Location.X - 17, lbl_picked.Location.Y + txtBoxY);
                txtPicked.Size = new Size(100, 20);
                txtPicked.Text = txtPicked.Name;
                txtBoxY += 38;

                txtBoxes.Add(txtReq.Name, txtReq);
                txtBoxes.Add(txtPicked.Name, txtPicked);

                pickBoxPnl.Controls.Add(txtReq);
                pickBoxPnl.Controls.Add(txtPicked);
            }
        }
        #endregion

        #region [Form Closing]
        private void Pick_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
