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

        private void ReadPalletDB()
        {
            while (isConnected)
            {
                palletClient.DBRead(3107, 0, palletReadBuffer.Length, palletReadBuffer);//1110
                skidPresent = S7.GetBitAt(palletReadBuffer, 0, 0);
                addPallet = S7.GetBitAt(palletReadBuffer, 0, 1);

                if (skidPresent && !addPallet) {
                    if (SendPallet())
                    {
                        oSignalSendPalletEvent.Set();
                    }
                }
                else if (skidPresent && addPallet)
                    oSignalAddPalletEvent.Set();

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
                Console.WriteLine("Pallet ID : " + palletID + "---------------------------------------");

                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {
                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();
                                dt = new DataTable();
                                SqlDataAdapter da = new SqlDataAdapter("SELECT [Pallet Number] FROM Pallets WHERE [Pallet ID] = " + palletID, conn);
                                da.Fill(dt);
                                Console.WriteLine("Data table rows : " + dt.Rows.Count + "---------------------------------------");
                            }

                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow row in dt.Rows)
                                {
                                    palletNum = Int32.Parse(row["Pallet Number"].ToString());
                                }
                                Console.WriteLine("FOUND IN DATABASE ... SENDING 2 BACK TO PLC---------------------------------------");
                                S7.SetByteAt(transactWriteBuffer, 45, 2);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, ((short)palletNum).ToString());
                                int result1 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("Write Result : " + result1 + "---------------------------------------");
                            }
                            else
                            {
                                Console.WriteLine("NOT FOUND IN DATABASE ... SENDING 99 BACK TO PLC---------------------------------------");
                                S7.SetByteAt(transactWriteBuffer, 45, 2);
                                S7.SetStringAt(transactWriteBuffer, 96, 254, "99");
                                int result2 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("Write Result : " + result2 + "---------------------------------------");
                            }

                            bool skidIDSet = false;
                            //while (!skidIDSet)
                            //{
                            //    if (!skidID.Text.Equals("Skid ID: -"))
                            //        skidIDSet = true;
                            //}

                            hasReadOne = true;

                            Thread.Sleep(50);
                        }
                        break;
                    case 3:
                        if (hasReadOne)
                        {
                            string a = S7.GetStringAt(transactReadBuffer, 96).ToString();
                            palletNum = Int32.Parse(a); // CHECK FOR DUPLICATE PALLET NUM IN DATABASE, IF DUPLICATE, TELL PLC, OTHERWISE ADD TO DATABASE
                            using (SqlConnection conn = DBUtils.GetMainDBConnection())
                            {
                                conn.Open();
                                SqlCommand da = new SqlCommand("INSERT INTO Pallets ([Pallet ID],[Pallet Number]) VALUES (@palletID,@palletNum)", conn);
                                da.Parameters.AddWithValue("@palletID", palletID);
                                da.Parameters.AddWithValue("@palletNum", palletNum);
                                da.ExecuteNonQuery();
                                S7.SetByteAt(transactWriteBuffer, 45, 4);
                                S7.SetStringAt(transactWriteBuffer, 96, 200, palletNum.ToString());
                                int result3 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                                Console.WriteLine("Write Result : " + result3 + "---------------------------------------");
                            }
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

        #region [Pallet in Station Structure]
        private bool SendPallet()
        {
            //ReadAllValues();
            //client.DBRead(3000, 0, readBuffer.Length, readBuffer);//1110
            //readTransactionID = S7.GetByteAt(readBuffer, 45);

            //oSignalSendPalletEvent.WaitOne(); //Thread waits for new value to be read by PLC DB Read Thread
            //oSignalSendPalletEvent.Reset();

            StringBuilder sb = new StringBuilder();

            sb.Append(S7.GetByteAt(palletReadBuffer, 4))
                .Append(S7.GetByteAt(palletReadBuffer, 5))
                .Append(S7.GetByteAt(palletReadBuffer, 6))
                .Append(S7.GetByteAt(palletReadBuffer, 7))
                .Append(S7.GetByteAt(palletReadBuffer, 8))
                .Append(S7.GetByteAt(palletReadBuffer, 9))
                .Append(S7.GetByteAt(palletReadBuffer, 10))
                .Append(S7.GetByteAt(palletReadBuffer, 11));

            long palletID = Int64.Parse(sb.ToString());

            using (SqlConnection conn = DBUtils.GetMainDBConnection())
            {
                conn.Open();
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Pallets WHERE [Pallet ID] = " + palletID, conn);
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                palletNum = Int32.Parse(row["Pallet Number"].ToString());
            }

            S7.SetIntAt(palletWriteBuffer, 0, (short)palletNum);
            palletClient.DBWrite(3108, 0, palletWriteBuffer.Length, palletWriteBuffer);

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
            
            //parent.SetPalletNum(palletNum);
        }

        private void AddPallet()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(S7.GetByteAt(palletReadBuffer, 4))
                .Append(S7.GetByteAt(palletReadBuffer, 5))
                .Append(S7.GetByteAt(palletReadBuffer, 6))
                .Append(S7.GetByteAt(palletReadBuffer, 7))
                .Append(S7.GetByteAt(palletReadBuffer, 8))
                .Append(S7.GetByteAt(palletReadBuffer, 9))
                .Append(S7.GetByteAt(palletReadBuffer, 10))
                .Append(S7.GetByteAt(palletReadBuffer, 11));

            long palletID = Int64.Parse(sb.ToString());

            foreach (DataRow row in dt.Rows)
            {
                palletNum = Int32.Parse(row["Pallet Number"].ToString());
            }

            using (SqlConnection conn = DBUtils.GetMainDBConnection())
            {
                conn.Open();
                SqlCommand da = new SqlCommand("INSERT INTO Pallets ([Pallet ID],[Pallet Number]) VALUES (@palletID,@palletNum)", conn);
                da.Parameters.AddWithValue("@palletID", palletID);
                da.Parameters.AddWithValue("@palletNum", palletID);
                da.ExecuteNonQuery();
            }

            S7.SetIntAt(palletWriteBuffer, 0, (short)palletNum);
            palletClient.DBWrite(3108, 0, palletWriteBuffer.Length, palletWriteBuffer);
        }
        #endregion

        public void SetLabel(Label skidID)
        {
            this.skidID = skidID;
        }

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
        private void AwaitPart_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            palletClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
