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
        private S7Client usersClient = new S7Client();
        private S7Client sequenceClient = new S7Client();
        private static byte[] rfidReadBuffer = new byte[54];
        private static byte[] userWriteBuffer = new byte[106]; //use this to write access level to plc
        private static byte[] stepReadBuffer = new byte[8];
        
        public static byte[] echoReadBuffer = new byte[24];
        public static byte[] echoWriteBuffer = new byte[2];
        
        private bool hasReadRFID = false;
        private bool isConnected = false;
        private bool isReading = true;
        
        private bool changeScreen = false;
        private DataTable dt;
        private Form curForm;
        public string rfidCode { get; set; }
        
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

            //main_Panel.Controls.Clear();
            //AwaitPart frmAwait = new AwaitPart(31);
            ////frmAwait.SetS7Client(client);
            //curForm = frmAwait;
            //curForm.TopLevel = false;
            //curForm.TopMost = true;
            //curForm.Dock = DockStyle.Fill;
            //main_Panel.Controls.Add(curForm);
            //curForm.Show();

            if (isConnected)
            {
                Thread rfidThread = new Thread(new ThreadStart(ReadRFID));
                rfidThread.Start();
                Thread sequenceThread = new Thread(new ThreadStart(StartSequence));
                sequenceThread.Start();
            }
            else
            {
                MessageBox.Show("No Connection to the plc", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region [Establish Connection]
        private void EstablishConnection()
        {
            if (!isConnected)
            {
                int connectionResult1 = sequenceClient.ConnectTo("192.168.1.1", 0, 1);
                int connectionResult2 = usersClient.ConnectTo("192.168.1.1", 0, 1);

                if (connectionResult1 == 0 && connectionResult2 == 0)
                {
                    Console.WriteLine("=========Connection success===========");
                    isConnected = true;
                }
                else
                {
                    Console.WriteLine("=========Connection error============");
                    isConnected = false;
                    Console.WriteLine(connectionResult1);
                    Console.WriteLine(connectionResult2);
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
                sequenceClient.DBRead(3002, 0, stepReadBuffer.Length, stepReadBuffer);
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
                                    frmAwait.SetS7Client(sequenceClient);
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
                                    frmScan.SetS7Client(sequenceClient);
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
                                    frmPick.SetS7Client(sequenceClient);
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
                                    frmBolt.SetS7Client(sequenceClient);
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
                Thread.Sleep(10);
            }
        }
        
        #endregion

        #region [Heart Beat]
        public void Echo()
        {
            //client.DBRead(1021, 0, echoReadBuffer.Length, echoReadBuffer);
            //heartBeat = S7.GetIntAt(echoReadBuffer, 0);
            //S7.SetIntAt(echoWriteBuffer, 0, (short)heartBeat);
            //int writeResult = client.DBWrite(1022, 0, echoWriteBuffer.Length, echoWriteBuffer);
        }
        #endregion

        #region [Read RFID / LogIn User]
        private void ReadRFID()
        {
            while (isReading)
            {
                int dbread = usersClient.DBRead(3003, 0, rfidReadBuffer.Length, rfidReadBuffer);
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
                                int temp1 = Int32.Parse(txt_MP_AccessLvl.Text);
                                short temp2 = short.Parse(txt_MP_AccessLvl.Text);
                                S7.SetIntAt(userWriteBuffer, 104, temp2);
                                int writeResult = usersClient.DBWrite(3004, 0, userWriteBuffer.Length, userWriteBuffer);
                                if (writeResult == 0)
                                {
                                    Console.WriteLine("==> Successfully wrote to PLC");
                                }
                            }
                        }
                    });
                    
                    hasReadRFID = false; // creates unlimited rfid reading
                }
                Thread.Sleep(20);
            }
        }
        #endregion

        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            usersClient.Disconnect();
            sequenceClient.Disconnect();
            isConnected = false;
            isReading = false;
        }
    }
}
