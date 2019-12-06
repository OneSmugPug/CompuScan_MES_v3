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
    public partial class Pick : Form
    {
        Hub hub = Hub.Default;
        private S7Client
            transactClient = new S7Client();

        private bool
            hasReadOne = false,
            isConnected = false,
            handshakeCleared = false;

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
            txtBoxY = 38,
            skidID,
            sequencePos,
            maxNumAttempts;

        private Dictionary<string,TextBox> txtBoxes;

        private DataTable dt;

        private string 
            stationID,
            entireSequence,
            
            FEMLabel;

        private Thread readTransact,
            handshakeThr;

        private string[] 
            subSequences,
            currentSequence,
            shortCodes;


        public Pick(int pickNum, string stationID, string entireSequence, int sequencePos, int skidID, string FEMLabel)
        {
            InitializeComponent();
            this.pickNum = pickNum;
            this.stationID = stationID;
            this.entireSequence = entireSequence;
            this.sequencePos = sequencePos;
            this.skidID = skidID;
            this.FEMLabel = FEMLabel;
            current_pick.Text = curPickNum.ToString();
            total_pick.Text = pickNum.ToString();
            txtBoxes = new Dictionary<string, TextBox>();
        }

        private void Pick_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            if (isConnected)
            {
                PopulatePickTextBoxes();

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
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);
                errorCode = S7.GetByteAt(transactReadBuffer, 45);

                //if (errorCode != 0)
                //{
                //    HandleError();
                //}

                if (readTransactionID == 0 && !handshakeCleared)
                {
                    handshakeCleared = true;
                    if (isConnected)
                    {
                        S7.SetByteAt(transactWriteBuffer, 45, 1);
                        S7.SetByteAt(transactWriteBuffer, 94, 31); // Send Equipment ID *** small label scanner
                        S7.SetByteAt(transactWriteBuffer, 44, (byte)maxNumAttempts); // Send MaxNumOfAttempts
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("-------------------------" +
                                          "\nTransaction ID : 1" +
                                          "\nEquipment ID 31 : " +
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

        private void HandleError()
        {
            while (errorCode != 0)
            {
                switch (errorCode)
                {
                    case 90:
                        // part failed
                        break;
                    case 96:
                        // max attempts reached, show dialog
                        break;
                    case 98:
                        break;
                    default:
                        Console.WriteLine("UNKOWN ERROR");
                        break;
                }

                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);
                errorCode = S7.GetByteAt(transactReadBuffer, 45);
                
                Thread.Sleep(50);
            }
        }
        #endregion

        #region [Handshake Structure]
        private void Handshake()
        {
            
            //int count = 1;
            string scanData;
            
            while (isConnected)
            {

                oSignalTransactEvent.WaitOne();
                oSignalTransactEvent.Reset();

                
                switch (readTransactionID)
                {
                    case 2:
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
                        curPickNum++;
                        if (curPickNum > pickNum)
                        {
                            S7.SetByteAt(transactWriteBuffer, 45, 99);
                            int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            break;
                        }

                        scanData = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        Console.WriteLine(scanData);

                        if (!scanData.Equals(""))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                TextBox temp;
                                txtBoxes.TryGetValue("pickedBox" + curPickNum, out temp);
                                //count++;
                                current_pick.Text = curPickNum.ToString();
                                temp.Text = scanData.ToString();
                            });

                            S7.SetByteAt(transactWriteBuffer, 45, (byte)(readTransactionID + 1));
                            S7.SetByteAt(transactWriteBuffer, 94, 31); // Send Equipment ID *** small label scanner
                            S7.SetByteAt(transactWriteBuffer, 44, (byte)maxNumAttempts); // Send MaxNumOfAttempts
                            int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("Write Result : " + result2 + "\n-------------------------");
                        }
                        else // no data received from plc
                        {
                            //S7.SetByteAt(transactWriteBuffer, 45, 3); // Check what tr id to send back if no data was received
                            S7.SetByteAt(transactWriteBuffer, 48, 99);
                            int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        }

                        Thread.Sleep(50);
                        break;
                    case 100:
                        Console.WriteLine("-------------------------" +
                                  "\nTransaction ID : " + readTransactionID +
                                  "\nResult : Handshake done... Starting next screen." +
                                  "\n-------------------------");
                        if ((sequencePos + 1) >= subSequences.Length)
                        {
                            hub.PublishAsync(new ScreenChangeObject("0"));
                            this.Close();
                        }
                        else
                        {
                            string[] nextStep = subSequences[sequencePos + 1].Split(',');
                            hub.PublishAsync(new ScreenChangeObject(nextStep[0], nextStep[1], entireSequence, (sequencePos + 1), skidID, FEMLabel));
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

        #region [Populate Panel with Textboxes]
        private void PopulatePickTextBoxes()
        {
            subSequences = entireSequence.Split('-');
            currentSequence = subSequences[sequencePos].Split(',');
            shortCodes = String.Join(" ", currentSequence[3].SplitIntoParts(3)).Split(' ');
            maxNumAttempts = Int32.Parse(currentSequence[2]);

            if (shortCodes.Length != pickNum)
            {
                Console.WriteLine("The number of picks do not equal the number of part numbers in the database");
                return;
            }

            using (SqlConnection conn = DBUtils.GetMainDBConnection())
            {
                conn.Open();
                
                for (int i = 0; i < pickNum; i++)
                {
                    // POSSIBLE OPTIMIZATION REQUIRED
                    dt = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT [Valeo Comp.] FROM ShortCodes WHERE [Short Code] = '" + shortCodes[i] + "'", conn);
                    da.Fill(dt);
                    // -----------------------------------

                    if (dt.Rows.Count != 1)
                    {
                        Console.WriteLine("Part number not found in database");
                        return;
                    }

                    DataRow row = dt.Rows[0];

                    TextBox txtReq = new TextBox();
                    txtReq.Name = "reqBox" + (i + 1);
                    txtReq.Location = new Point(lbl_Req.Location.X - 6, lbl_Req.Location.Y + txtBoxY);
                    txtReq.Size = new Size(100, 20);
                    txtReq.Text = row["Valeo Comp."].ToString();
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
