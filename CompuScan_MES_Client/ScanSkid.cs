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
    public partial class ScanSkid : Form
    {
        #region [Variables]
        Hub hub = Hub.Default;

        private S7Client
            transactClient = new S7Client();

        private bool
            hasReadOne = false,
            isConnected = false,
            handshakeCleared = false;

        private long palletID;
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

        private Thread readTransact,
                       handshakeThr;
        #endregion

        #region [Form Load]
        public ScanSkid(string stationID)
        {
            InitializeComponent();
            this.stationID = stationID;
            palletNum = 0;
        }

        private void ScanSkid_Load(object sender, EventArgs e)
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
                        S7.SetByteAt(transactWriteBuffer, 94, 85); // Send Equipment ID *** RFID SCANNER
                        int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("-------------------------" +
                                          "\nTransaction ID OUT : 1" +
                                          "\nEquipment ID : 85" +
                                          "\nPLC Write Result : " + result1 +
                                          "\n-------------------------");
                    }
                }

                if ((readTransactionID != oldReadTransactionID) && handshakeCleared)
                {
                    Console.WriteLine("Transaction ID IN : " + readTransactionID);
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
                        StringBuilder sb = new StringBuilder();

                        sb.Append(S7.GetByteAt(transactReadBuffer, 352))
                            .Append(S7.GetByteAt(transactReadBuffer, 353))
                            .Append(S7.GetByteAt(transactReadBuffer, 354))
                            .Append(S7.GetByteAt(transactReadBuffer, 355))
                            .Append(S7.GetByteAt(transactReadBuffer, 356))
                            .Append(S7.GetByteAt(transactReadBuffer, 357))
                            .Append(S7.GetByteAt(transactReadBuffer, 358))
                            .Append(S7.GetByteAt(transactReadBuffer, 359));

                        palletID = Int64.Parse(sb.ToString());

                        this.Invoke((MethodInvoker)delegate
                        {
                            skid_txt.Text = sb.ToString();
                        });

                        using (SqlConnection conn = DBUtils.GetMainDBConnection())
                        {
                            conn.Open();
                            dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT [Pallet Number] FROM Pallets WHERE [Pallet ID] = '" + palletID + "'", conn);
                            da.Fill(dt);
                        }

                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                palletNum = Int32.Parse(row["Pallet Number"].ToString());
                                UpdateUISkidID();
                            }

                            S7.SetByteAt(transactWriteBuffer, 45, 99);
                            S7.SetStringAt(transactWriteBuffer, 96, 200, ((short)palletNum).ToString());
                            int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("-------------------------" +
                                  "\nTransaction ID OUT : " + readTransactionID +
                                  "\nPallet ID : " + palletID +
                                  "\nResult : Found pallet in database" +
                                  "\nPLC Write Result : " + result1 +
                                  "\n-------------------------");
                        }
                        else
                        {
                            S7.SetByteAt(transactWriteBuffer, 45, 3);
                            S7.SetByteAt(transactWriteBuffer, 48, 99);
                            int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                            Console.WriteLine("-------------------------" +
                                  "\nTransaction ID OUT : " + readTransactionID +
                                  "\nPallet ID : " + palletID +
                                  "\nResult : Did not find pallet in database" +
                                  "\nErrorcode : 99" +
                                  "\nPLC Write Result : " + result2 +
                                  "\n-------------------------");
                        }

                        Thread.Sleep(50);
                        break;
                    case 4:
                        string a = S7.GetStringAt(transactReadBuffer, 96).ToString();
                        palletNum = Int32.Parse(a);
                        using (SqlConnection conn = DBUtils.GetMainDBConnection())
                        {
                            conn.Open();

                            dt = new DataTable();
                            SqlDataAdapter da1 = new SqlDataAdapter("SELECT [Pallet Number] FROM Pallets WHERE [Pallet Number] = " + palletNum, conn);
                            da1.Fill(dt);

                            // If no duplicates were found
                            if (dt.Rows.Count == 0)
                            {
                                SqlCommand da2 = new SqlCommand("INSERT INTO Pallets ([Pallet ID],[Pallet Number]) VALUES (@palletID,@palletNum)", conn);
                                da2.Parameters.AddWithValue("@palletID", palletID.ToString());
                                da2.Parameters.AddWithValue("@palletNum", palletNum);
                                da2.ExecuteNonQuery();
                                UpdateUISkidID();
                                S7.SetByteAt(transactWriteBuffer, 45, 99);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, palletNum.ToString()); // Confirm what needs to be written back to plc with the trID
                                int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("-------------------------" +
                                  "\nTransaction ID OUT : " + readTransactionID +
                                  "\nPallet ID : " + palletID +
                                  "\nResult : Successfully added pallet to database" +
                                  "\nPLC Write Result : " + result3 +
                                  "\n-------------------------");
                            }
                            else
                            {
                                S7.SetByteAt(transactWriteBuffer, 45, 5);
                                S7.SetByteAt(transactWriteBuffer, 48, 98);
                                int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("-------------------------" +
                                  "\nTransaction ID OUT : " + readTransactionID +
                                  "\nPallet ID : " + palletID +
                                  "\nResult : Successfully added pallet to database" +
                                  "\nPLC Write Result : " + result3 +
                                  "\n-------------------------");
                            }
                        }
                        break;
                    case 100:
                        Console.WriteLine("-------------------------" +
                                  "\nTransaction ID OUT : " + readTransactionID +
                                  "\nResult : Handshake done... Starting next screen." +
                                  "\n-------------------------");
                        hub.PublishAsync(new ScreenChangeObject("2", palletNum));
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region [Update SkidID on UI]
        private bool UpdateUISkidID()
        {
            if (palletNum > 0)
            {
                if (skidID.IsHandleCreated && parent.IsHandleCreated)
                {
                    skidID.Invoke((MethodInvoker)delegate
                    {
                        skidID.Text = "Skid ID: " + palletNum;
                    });
                }
                else skidID.Text = "Skid ID: " + palletNum;
                return true;
            }
            else
            {
                if (skidID.IsHandleCreated)
                {
                    skidID.Invoke((MethodInvoker)delegate
                    {
                        skidID.Text = "Skid ID: -";
                        MessageBox.Show("Skid not found.", "Unknown Skid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    });
                }
                else
                {
                    skidID.Text = "Skid ID: -";
                    MessageBox.Show("Skid not found.", "Unknown Skid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
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
        private void ScanSkid_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
