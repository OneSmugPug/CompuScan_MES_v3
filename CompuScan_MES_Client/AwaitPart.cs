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
        private S7Client
            transactClient = new S7Client(),
            palletClient = new S7Client();

        private bool 
            hasReadOne = false, 
            isConnected = false;

        private int oldReadTransactionID = 0;
        private DataTable dt;
        private MainPage parent = null;
        private Label skidID;

        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false),
            oSignalSendPalletEvent = new ManualResetEvent(false),
            oSignalAddPalletEvent = new ManualResetEvent(false);


        public static byte[] 
            transactReadBuffer = new byte[402],
            transactWriteBuffer = new byte[402],
            palletReadBuffer = new byte[12],
            palletWriteBuffer = new byte[2];
        #endregion

        #region [Form Load]
        public AwaitPart()
        {
            InitializeComponent();
            palletNum = 0;
        }

        private void AwaitPart_Load(object sender, EventArgs e)
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

            parent = (MainPage)this.Owner;
        }
        #endregion

        #region [Establish Connection]
        private void EstablishConnection()
        {
            transactClient.ConnectTo("192.168.1.1", 0, 1);
            palletClient.ConnectTo("192.168.1.1", 0, 1);

            if (transactClient.Connected && palletClient.Connected)
                isConnected = true;
            else MessageBox.Show("Connection to PLC on 192.168.1.1 unsuccessful.", "No PLC Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);


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

        #region [Handshake Structure]
        private void Handshake()
        {
            while (isConnected)
            {

                oSignalTransactEvent.WaitOne(); //Thread waits for new value to be read by PLC DB Read Thread
                oSignalTransactEvent.Reset();

                StringBuilder sb = new StringBuilder();

                sb.Append(S7.GetByteAt(transactReadBuffer, 352))
                    .Append(S7.GetByteAt(transactReadBuffer, 353))
                    .Append(S7.GetByteAt(transactReadBuffer, 354))
                    .Append(S7.GetByteAt(transactReadBuffer, 355))
                    .Append(S7.GetByteAt(transactReadBuffer, 356))
                    .Append(S7.GetByteAt(transactReadBuffer, 357))
                    .Append(S7.GetByteAt(transactReadBuffer, 358))
                    .Append(S7.GetByteAt(transactReadBuffer, 359));

                long palletID = Int64.Parse(sb.ToString());
                Console.WriteLine("Pallet ID : " + sb.ToString() + "---------------------------------------");

                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {
                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();
                                dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [Pallet Number] FROM Pallets WHERE [Pallet ID] = '" + palletID + "'", conn);
                                da.Fill(dt);
                                Console.WriteLine("Data table rows : " + dt.Rows.Count + "---------------------------------------");
                            }

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    palletNum = Int32.Parse(row["Pallet Number"].ToString());
                                    UpdateUISkidID();
                                }
                                Console.WriteLine("FOUND IN DATABASE ... SENDING 2 BACK TO PLC---------------------------------------");
                                S7.SetByteAt(transactWriteBuffer, 45, 2);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, ((short)palletNum).ToString());
                                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("Write Result : " + result1 + "---------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine("NOT FOUND IN DATABASE ... SENDING 99 BACK TO PLC AS AN ERROR CODE---------------------------------------");
                                S7.SetByteAt(transactWriteBuffer, 45, 1);
                                S7.SetByteAt(transactWriteBuffer, 48, 99);
                                int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("Write Result : " + result2 + "---------------------------------------");
                            }

                            hasReadOne = true;

                            Thread.Sleep(50);
                        }
                        else
                        {
                            MessageBox.Show("Previous handshake was not completed.", "Out of order Transaction ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 3:
                        if (hasReadOne)
                        {
                            string a = S7.GetStringAt(transactReadBuffer, 96).ToString();
                            palletNum = Int32.Parse(a);
                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();

                                dt = new DataTable();
                                SqlDataAdapter da1 = new SqlDataAdapter("SELECT [Pallet Number] FROM Pallets WHERE [Pallet Number] = " + palletNum, conn);
                                da1.Fill(dt);
                                Console.WriteLine("Data table rows : " + dt.Rows.Count + "---------------------------------------");

                                // If no duplicates were found
                                if (dt.Rows.Count == 0)
                                {
                                    SqlCommand da2 = new SqlCommand("INSERT INTO Pallets ([Pallet ID],[Pallet Number]) VALUES (@palletID,@palletNum)", conn);
                                    da2.Parameters.AddWithValue("@palletID", sb.ToString());
                                    da2.Parameters.AddWithValue("@palletNum", palletNum);
                                    da2.ExecuteNonQuery();
                                    UpdateUISkidID();
                                    S7.SetByteAt(transactWriteBuffer, 45, 4);
                                    S7.SetStringAt(transactWriteBuffer, 96, 200, palletNum.ToString());
                                    int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                    Console.WriteLine("Write Result : " + result3 + "---------------------------------------");
                                }
                                else
                                {
                                    S7.SetByteAt(transactWriteBuffer, 45, 1);
                                    S7.SetByteAt(transactWriteBuffer, 48, 98);
                                    int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                    Console.WriteLine("Write Result : " + result3 + "---------------------------------------");
                                    MessageBox.Show("Duplicate Pallet Number Was Found.", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Transaction ID of 1 never recieved.", "Out of order Transaction ID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                    case 99:
                        Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
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
        private void AwaitPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            palletClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
