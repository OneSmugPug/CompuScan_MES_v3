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
    public partial class MainPage : Form
    {
        #region [Objects and Variables]
        private S7Client client = new S7Client();
        private static byte[] rfidReadBuffer = new byte[54];
        private static byte[] userWriteBuffer = new byte[108]; //use this to write access level to plc
        private static byte[] stepReadBuffer = new byte[8];
        public static byte[] readBuffer = new byte[296];
        public static byte[] writeBuffer = new byte[296];
        public static byte[] echoReadBuffer = new byte[24];
        public static byte[] echoWriteBuffer = new byte[2];
        
        private bool hasReadRFID = false;
        private bool isConnected = false;
        private bool hasReadOne = false;
        private bool changeScreen = false;
        private DataTable dt;
        private Form curForm;
        public string rfidCode { get; set; }
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
        public int heartBeat { get; set; }
        public int stationNum { get; set; }
        public int stepNum { get; set; }
        public int stepData { get; set; }
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void MainPage_Load(object sender, EventArgs e)
        {
            EstablishConnection();

            main_Panel.Controls.Clear();
            Scan frmScan = new Scan(31);
            //frmScan.SetPLCThread(plc_Threads);
            curForm = frmScan;
            curForm.TopLevel = false;
            curForm.TopMost = true;
            curForm.Dock = DockStyle.Fill;
            main_Panel.Controls.Add(curForm);
            curForm.Show();

            //if (isConnected)
            //{
            //    Thread sequenceThread = new Thread(new ThreadStart(StartSequence));
            //    sequenceThread.Start();
            //    //Thread rfidThread = new Thread(new ThreadStart(ReadRFID));
            //    //rfidThread.Start();
            //}
            //else
            //{
            //    MessageBox.Show("No Connection to the plc", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        #endregion

        #region [Establish Connection]
        private void EstablishConnection()
        {
            if (!isConnected)
            {
                int connectionResult = client.ConnectTo("192.168.1.1", 0, 1);

                if (connectionResult == 0)
                {
                    Console.WriteLine("=========Connection success===========");
                    isConnected = true;
                }
                else
                {
                    Console.WriteLine("=========Connection error============");
                    isConnected = false;
                    Console.WriteLine(connectionResult);
                    return;

                }
            }
        }
        #endregion

        #region [Sequence Handling]
        private void StartSequence()
        {
            while (isConnected)
            {
                client.DBRead(3002, 0, stepReadBuffer.Length, stepReadBuffer);
                changeScreen = S7.GetBitAt(stepReadBuffer, 0, 0);
                //Console.WriteLine(changeScreen);
                if (changeScreen)
                {
                    stationNum = S7.GetIntAt(stepReadBuffer, 2);
                    stepNum = S7.GetIntAt(stepReadBuffer, 4);
                    stepData = S7.GetIntAt(stepReadBuffer, 6);
                    switch (stepNum)
                    {
                        // Await part
                        case 0:
                                this.Invoke((MethodInvoker)delegate
                                {
                                    main_Panel.Controls.Clear();
                                    AwaitPart frmAwait = new AwaitPart(stationNum);
                                    //frmScan.SetPLCThread(plc_Threads);
                                    curForm = frmAwait;
                                    curForm.TopLevel = false;
                                    curForm.TopMost = true;
                                    curForm.Dock = DockStyle.Fill;
                                    main_Panel.Controls.Add(curForm);
                                    curForm.Show();
                                });
                            break;
                        // Scan part
                        case 1:
                                this.Invoke((MethodInvoker)delegate
                                {
                                    main_Panel.Controls.Clear();
                                    Scan frmScan = new Scan(stationNum);
                                    //frmScan.SetPLCThread(plc_Threads);
                                    curForm = frmScan;
                                    curForm.TopLevel = false;
                                    curForm.TopMost = true;
                                    curForm.Dock = DockStyle.Fill;
                                    main_Panel.Controls.Add(curForm);
                                    curForm.Show();
                                });
                            break;
                        // Pick part
                        case 2:
                                this.Invoke((MethodInvoker)delegate
                                {
                                    main_Panel.Controls.Clear();
                                    Pick frmPick = new Pick(stepData,stationNum);
                                    //frmScan.SetPLCThread(plc_Threads);
                                    curForm = frmPick;
                                    curForm.TopLevel = false;
                                    curForm.TopMost = true;
                                    curForm.Dock = DockStyle.Fill;
                                    main_Panel.Controls.Add(curForm);
                                    curForm.Show();
                                });
                            break;
                        // Bolt
                        case 3:
                                this.Invoke((MethodInvoker)delegate
                                {
                                    main_Panel.Controls.Clear();
                                    Bolt frmBolt = new Bolt(stepData, stationNum);
                                    //frmScan.SetPLCThread(plc_Threads);
                                    curForm = frmBolt;
                                    curForm.TopLevel = false;
                                    curForm.TopMost = true;
                                    curForm.Dock = DockStyle.Fill;
                                    main_Panel.Controls.Add(curForm);
                                    curForm.Show();
                                });
                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(20);
            }

            //while (isConnected)
            //{
            //    //Echo(); // This is the heart beat
            //    ReadAllValues();
            //    switch (readTransactionID)
            //    {
            //        case 1:
            //            if (!hasReadOne)
            //            {
            //                DoWork(productionData);
            //                //WriteToSQLDataBase();
            //                Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + "back to PLC.");
            //                writeTransactionID = readTransactionID + 1;
            //                //WriteBackToPLC();
            //                hasReadOne = true;
            //            }
            //            break;
            //        case 3:
            //        case 5:
            //        case 7:
            //        case 9:
            //        case 11:
            //        case 13:
            //        case 15:
            //        case 17:
            //        case 19:
            //        case 21:
            //        case 23:
            //        case 25:
            //        case 27:
            //        case 29:
            //        case 31:
            //        case 33:
            //        case 35:
            //            if (hasReadOne)
            //            {
            //                DoWork(productionData);
            //                //WriteToSQLDataBase();
            //                Console.WriteLine("Got Transaction ID: " + readTransactionID + ". Writing to database and sending a transaction ID of " + (readTransactionID + 1) + "back to PLC.");
            //                writeTransactionID = readTransactionID + 1;
            //                //WriteBackToPLC();
            //            }
            //            break;
            //        case 99:
            //            Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
            //            writeTransactionID = 100;
            //            WriteBackToPLC();
            //            hasReadOne = false;
            //            break;
            //        default:
            //            break;
            //    }
            //    Thread.Sleep(500);
            //}
        }

        private void ReadAllValues()
        {
            client.DBRead(3002, 0, stepReadBuffer.Length, stepReadBuffer);

            changeScreen = S7.GetBitAt(rfidReadBuffer, 0, 0);
            stationNum = S7.GetIntAt(stepReadBuffer, 2);
            stepNum = S7.GetIntAt(stepReadBuffer, 4);
            stepData = S7.GetIntAt(stepReadBuffer, 6);

            //client.DBRead(3000, 0, readBuffer.Length, readBuffer);//1110

            //lineID = S7.GetStringAt(readBuffer, 0);

            //identifier = S7.GetStringAt(readBuffer, 22);

            //identifierCount = S7.GetByteAt(readBuffer, 44); // Number of entries in the database (Does not really need to read but write it to plc?)

            //readTransactionID = S7.GetByteAt(readBuffer, 45);

            //channelStatus = S7.GetByteAt(readBuffer, 46);

            //stationStatus = S7.GetByteAt(readBuffer, 47);

            //errorCode = S7.GetByteAt(readBuffer, 48);

            //userName = S7.GetStringAt(readBuffer, 50);

            //equipmentID = S7.GetByteAt(readBuffer, 94);

            //productionData = S7.GetStringAt(readBuffer, 96);
        }

        //private void DoWork(string work)
        //{
        //    switch (work)
        //    {
        //        case "a":
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                main_Panel.Controls.Clear();
        //                Scan frmScan = new Scan(stationNum);
        //                //frmScan.SetPLCThread(plc_Threads);
        //                curForm = frmScan;
        //                curForm.TopLevel = false;
        //                curForm.TopMost = true;
        //                curForm.Dock = DockStyle.Fill;
        //                main_Panel.Controls.Add(curForm);
        //                curForm.Show();
        //            });
        //            break;
        //        case "b":
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                main_Panel.Controls.Clear();
        //                Pick frmPick = new Pick(5, stationNum);
        //                //frmScan.SetPLCThread(plc_Threads);
        //                curForm = frmPick;
        //                curForm.TopLevel = false;
        //                curForm.TopMost = true;
        //                curForm.Dock = DockStyle.Fill;
        //                main_Panel.Controls.Add(curForm);
        //                curForm.Show();
        //            });
        //            break;
        //        case "c":
        //            this.Invoke((MethodInvoker)delegate
        //            {
        //                main_Panel.Controls.Clear();
        //                Bolt frmBolt = new Bolt(5, stationNum);
        //                //frmScan.SetPLCThread(plc_Threads);
        //                curForm = frmBolt;
        //                curForm.TopLevel = false;
        //                curForm.TopMost = true;
        //                curForm.Dock = DockStyle.Fill;
        //                main_Panel.Controls.Add(curForm);
        //                curForm.Show();
        //            });
        //            break;
        //        default:
        //            break;
        //    }
        //}
        public void WriteBackToPLC()
        {
            S7.SetStringAt(writeBuffer, 0, 20, lineID);
            S7.SetStringAt(writeBuffer, 22, 20, identifier);
            S7.SetByteAt(writeBuffer, 44, (byte)identifierCount);
            S7.SetByteAt(writeBuffer, 45, (byte)writeTransactionID);
            S7.SetByteAt(writeBuffer, 46, (byte)channelStatus);
            S7.SetByteAt(writeBuffer, 47, (byte)stationStatus);
            S7.SetByteAt(writeBuffer, 48, (byte)errorCode);
            S7.SetStringAt(writeBuffer, 50, 20, userName);
            S7.SetByteAt(writeBuffer, 94, (byte)equipmentID);
            S7.SetStringAt(writeBuffer, 96, 20, productionData);
            int writeResult = client.DBWrite(3001, 0, writeBuffer.Length, writeBuffer);//1111
            if (writeResult == 0)
            {
                Console.WriteLine("==> Successfully wrote to PLC");
            }
        }
        #endregion

        #region [Heart Beat]
        public void Echo()
        {
            client.DBRead(1021, 0, echoReadBuffer.Length, echoReadBuffer);
            heartBeat = S7.GetIntAt(echoReadBuffer, 0);
            S7.SetIntAt(echoWriteBuffer, 0, (short)heartBeat);
            int writeResult = client.DBWrite(1022, 0, echoWriteBuffer.Length, echoWriteBuffer);
        }
        #endregion

        #region [Read RFID / LogIn User]
        private void ReadRFID()
        {
            while (!hasReadRFID)
            {
                int dbread = client.DBRead(3003, 0, rfidReadBuffer.Length, rfidReadBuffer);
                hasReadRFID = S7.GetBitAt(rfidReadBuffer, 0, 0);
                if (hasReadRFID)
                {
                    rfidCode = S7.GetStringAt(rfidReadBuffer, 2);
                    Console.WriteLine(rfidCode);
                    this.Invoke((MethodInvoker)delegate
                    {
                        string[] tempArr = rfidCode.Split(',');
                        string tempStr = tempArr[1].Remove(0, 4);
                        using (SqlConnection conn = DBUtils.GetDBConnection())
                        {
                            conn.Open();
                            dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserDetails", conn);
                            da.Fill(dt);
                        }
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["CardID"].ToString().Equals(tempStr))
                            {
                                txt_MP_UserName.Text = row["FirstName"].ToString() + " " + row["LastName"];
                                txt_MP_AccessLvl.Text = row["AccessLevel"].ToString();
                                //Write the accesslevel to plc
                                //only access level?
                                S7.SetStringAt(userWriteBuffer, 0, 50, tempStr);
                                S7.SetStringAt(userWriteBuffer, 52, 50, txt_MP_UserName.Text.ToString());
                                S7.SetStringAt(userWriteBuffer, 104, 1, txt_MP_AccessLvl.Text.ToString());
                                int writeResult = client.DBWrite(3004, 0, userWriteBuffer.Length, userWriteBuffer);
                                if (writeResult == 0)
                                {
                                    Console.WriteLine("==> Successfully wrote to PLC");
                                }
                            }
                        }
                    });
                    
                    //hasReadRFID = false; // creates unlimited rfid reading
                }
                Thread.Sleep(20);
            }
        }
        #endregion

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            client.Disconnect();
            isConnected = false;
        }
    }
}
