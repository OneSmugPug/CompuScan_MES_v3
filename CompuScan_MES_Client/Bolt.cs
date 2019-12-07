using CompuScan_MES_Client.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;
using System.Threading;
using PubSub;
using System.Data.SqlClient;

namespace CompuScan_MES_Client
{
    public partial class Bolt : Form
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
            curBoltNum = 0,
            picBoxY = 12,
            picBoxX = 12,
            boltNum,
            sequencePos,
            skidID,
            maxNumAttempts;

        private Dictionary<string, PictureBox> picBoxes;

        private string 
            stationID,
            boltResult,
            entireSequence,
            FEMLabel;

        private Thread readTransact,
            handshakeThr;

        private string[]
            subSequences,
            currentSequence;

        public Bolt(int boltNum, string stationID, string entireSequence, int sequencePos, int skidID, string FEMLabel)
        {
            InitializeComponent();
            this.boltNum = boltNum;
            this.stationID = stationID;
            this.entireSequence = entireSequence;
            this.sequencePos = sequencePos;
            this.skidID = skidID;
            this.FEMLabel = FEMLabel;
            picBoxes = new Dictionary<string, PictureBox>();
        }

        private void Bolt_Load(object sender, EventArgs e)
        {
            EstablishConnection();
            
            if (isConnected)
            {
                current_bolt.Text = curBoltNum.ToString();
                total_bolt.Text = boltNum.ToString();
                PopulateBoltImages();

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
                        S7.SetByteAt(transactWriteBuffer, 94, 60); // Send Equipment ID *** bolting tool
                        //S7.SetByteAt(transactWriteBuffer, ?, TORQUE); // Send required torque of bolting tool (maybe not required)
                        S7.SetByteAt(transactWriteBuffer, 44, (byte)maxNumAttempts); // Send MaxNumOfAttempts
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("-------------------------" +
                                          "\nTransaction ID : 1" +
                                          "\nEquipment ID : " + equipmentID +
                                          "\nPLC Write Result : " + result1 +
                                          "\n-------------------------");
                    }
                }

                if ((readTransactionID != oldReadTransactionID) && handshakeCleared)
                {
                    Console.WriteLine("Transaction ID IN : " + readTransactionID + "\n-------------------------");
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
            try
            {
                using (SqlConnection conn = DBUtils.GetFEMDBConnection())
                {
                    conn.Open();
                    String query = "INSERT INTO Station31Bolt ([FEM Label],Timestamp,[Bolt Result]) VALUES (@femlabel, @timestamp, @boltresult)";

                    while (isConnected)
                    {

                        oSignalTransactEvent.WaitOne();
                        oSignalTransactEvent.Reset();

                        SqlCommand command = new SqlCommand(query, conn);

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
                                curBoltNum++;
                                boltResult = S7.GetStringAt(transactReadBuffer, 96).ToString();

                                if (!boltResult.Equals(""))
                                {

                                    this.Invoke((MethodInvoker)delegate
                                    {
                                        current_bolt.Text = curBoltNum.ToString();
                                    });

                                    command.Parameters.AddWithValue("@femlabel", FEMLabel);
                                    command.Parameters.AddWithValue("@timestamp", DateTime.Now);
                                    command.Parameters.AddWithValue("@boltresult", boltResult);
                                    command.ExecuteNonQuery();

                                    if (curBoltNum >= boltNum)
                                    {
                                        S7.SetByteAt(transactWriteBuffer, 45, 99);
                                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                        break;
                                    }

                                    S7.SetByteAt(transactWriteBuffer, 45, (byte)(readTransactionID + 1));
                                    S7.SetByteAt(transactWriteBuffer, 94, 60); // Send Equipment ID *** bolting tool
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
                                break;
                            case 100:

                                Console.WriteLine("-------------------------" +
                                          "\nTransaction ID : " + readTransactionID +
                                          "\nResult : Handshake done... Starting next screen." +
                                          "\n-------------------------");

                                subSequences = entireSequence.Split('-');
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                handshakeCleared = false;
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

        #region [Populate Bolt Images]
        private void PopulateBoltImages()
        {
            subSequences = entireSequence.Split('-');
            currentSequence = subSequences[sequencePos].Split(',');
            maxNumAttempts = Int32.Parse(currentSequence[2]);

            for (int i = 0; i < boltNum; i++)
            {
                PictureBox picBox = new PictureBox();
                picBox.Name = "picBox" + (i+1);
                picBox.Image = (Image)Resources.ResourceManager.GetObject("empty_bolt_image");
                picBox.Location = new Point(picBoxX, picBoxY);
                picBox.Size = new Size(75, 75);
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                picBoxY += 87;
                if (picBoxY > 500)
                {
                    picBoxX += 87;
                    picBoxY = 12;
                }

                picBoxes.Add(picBox.Name, picBox);

                this.Controls.Add(picBox);
            }
        }
        #endregion

        #region [Form Closing]
        private void Bolt_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
