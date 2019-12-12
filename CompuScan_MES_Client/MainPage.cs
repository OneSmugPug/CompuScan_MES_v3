using Sharp7;
using PubSub;
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
        Hub hub = Hub.Default;

        private S7Client 
            usersClient = new S7Client(),
            oldUserClient = new S7Client(),
            sequenceClient = new S7Client();

        private static byte[]
            rfidReadBuffer = new byte[54],
            userWriteBuffer = new byte[106], //use this to write access level to plc
            stepReadBuffer = new byte[8],
            echoReadBuffer = new byte[24],
            echoWriteBuffer = new byte[2],
            oldUserReadBuffer = new byte[106];
        
        private bool 
            hasReadRFID = false,
            isConnected = false,
            stationSet = false,
            hasRestarted,
            changeScreen = false,
            hasChanged = false;


        private ManualResetEvent
            oSignalSeqEvent = new ManualResetEvent(false),
            oSignalUserEvent = new ManualResetEvent(false);

        private DataTable dt;
        private Form curForm;
        private int oldStepNum = -1;

        private string stationID;
        #endregion

        #region [Form Load]
        public MainPage()
        {
            InitializeComponent();
        }
     
        private void MainPage_Load(object sender, EventArgs e)
        {
            hasRestarted = true;

            stationID = "31";
            //ObtainStationID();

            EstablishConnection();

            StartSequence();
            
            hub.Publish(new ScreenChangeObject("0"));

            if (isConnected)
            {
                Thread readUser = new Thread(ReadUserDB);
                readUser.IsBackground = true;
                readUser.Start();

                Thread rfidThread = new Thread(new ThreadStart(ReadRFID));
                rfidThread.IsBackground = true;
                rfidThread.Start();
            }
        }
        #endregion

        #region [Establish Connection]
        private void EstablishConnection()
        {
            if (!isConnected)
            {
                sequenceClient.ConnectTo("192.168.1.1", 0, 1);
                usersClient.ConnectTo("192.168.1.1", 0, 1);
                oldUserClient.ConnectTo("192.168.1.1", 0, 1);

                if (sequenceClient.Connected && usersClient.Connected && oldUserClient.Connected)
                    isConnected = true;
                else MessageBox.Show("Connection to PLC on 192.168.1.1 unsuccessful.", "No PLC Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region [Sequence Handling]
        private void StartSequence()
        {
            hub.Subscribe<ScreenChangeObject>(this, obj =>
            {
                switch (obj.GetScreenNum())
                {
                    // Await part
                    case "0":
                        Console.WriteLine(
                            "-------------------------" +
                            "\nAwaiting Part Screen Called" +
                            "\n-------------------------");

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();

                                main_Panel.Controls.Clear();
                                lbl_SkidID.Text = "Skid ID: -";
                                txt_MP_SequenceNumber.Text = "";
                                txt_MP_ValeoRef.Text = "";
                            });
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                            lbl_SkidID.Text = "Skid ID: -";
                            txt_MP_SequenceNumber.Text = "";
                            txt_MP_ValeoRef.Text = "";
                        }

                        AwaitPart frmAwait = new AwaitPart(stationID);
                        frmAwait.Owner = this;

                        curForm = frmAwait;
                        curForm.TopLevel = false;
                        curForm.TopMost = true;
                        curForm.Dock = DockStyle.Fill;

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                main_Panel.Controls.Add(curForm);
                                curForm.Show();
                            });
                        }
                        else
                        {
                            main_Panel.Controls.Add(curForm);
                            curForm.Show();
                        }
                        break;
                    // Scan Skid
                    case "1":
                        Console.WriteLine(
                            "-------------------------" +
                            "\nScan Skid Screen Called" +
                            "\n-------------------------");

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();

                                main_Panel.Controls.Clear();
                            });
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                        }

                        ScanSkid frmScanSkid = new ScanSkid(stationID);
                        frmScanSkid.Owner = this;
                        frmScanSkid.SetLabel(lbl_SkidID);

                        curForm = frmScanSkid;
                        curForm.TopLevel = false;
                        curForm.TopMost = true;
                        curForm.Dock = DockStyle.Fill;

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                main_Panel.Controls.Add(curForm);
                                curForm.Show();
                            });
                        }
                        else
                        {
                            main_Panel.Controls.Add(curForm);
                            curForm.Show();
                        }
                        break;
                    // Scan FEM
                    case "2":
                        Console.WriteLine(
                            "-------------------------" +
                            "\nScan FEM Label Screen Called" +
                            "\n-------------------------");

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();                             
                            });

                            main_Panel.Controls.Clear();
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                        }

                        ScanFEM frmScanFEM = new ScanFEM(stationID, obj.GetSkidID());
                        frmScanFEM.SetTxtBoxes(txt_MP_SequenceNumber,txt_MP_ValeoRef);
                        curForm = frmScanFEM;
                        curForm.TopLevel = false;
                        curForm.TopMost = true;
                        curForm.Dock = DockStyle.Fill;

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                main_Panel.Controls.Add(curForm);
                                curForm.Show();
                            });
                        }
                        else
                        {
                            main_Panel.Controls.Add(curForm);
                            curForm.Show();
                        }
                        break;
                    // Pick
                    case "3":
                        Console.WriteLine(
                            "-------------------------" +
                            "\nPick Parts Screen Called" +
                            "\n-------------------------");

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();

                                main_Panel.Controls.Clear();
                            });
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                        }

                        Pick frmPick = new Pick(Int32.Parse(obj.GetCount()), stationID, obj.GetSequence(), obj.GetPos(), obj.GetSkidID(), obj.GetFEMLabel());

                        

                        curForm = frmPick;
                        curForm.TopLevel = false;
                        curForm.TopMost = true;
                        curForm.Dock = DockStyle.Fill;

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                main_Panel.Controls.Add(curForm);
                                curForm.Show();
                            });
                        }
                        else
                        {
                            main_Panel.Controls.Add(curForm);
                            curForm.Show();
                        }
                        break;
                    // Bolt
                    case "4":
                        Console.WriteLine(
                            "-------------------------" +
                            "\nBolting Screen Called" +
                            "\n-------------------------");

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();

                                main_Panel.Controls.Clear();
                            });
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                        }

                        Bolt frmBolt = new Bolt(Int32.Parse(obj.GetCount()), stationID, obj.GetSequence(), obj.GetPos(), obj.GetSkidID(), obj.GetFEMLabel());

                        curForm = frmBolt;
                        curForm.TopLevel = false;
                        curForm.TopMost = true;
                        curForm.Dock = DockStyle.Fill;

                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                main_Panel.Controls.Add(curForm);
                                curForm.Show();
                            });
                        }
                        else
                        {
                            main_Panel.Controls.Add(curForm);
                            curForm.Show();
                        }
                        break;
                    // Sequence Done
                    case "99":
                        if (main_Panel.InvokeRequired)
                        {
                            main_Panel.Invoke((MethodInvoker)delegate
                            {
                                if (curForm != null)
                                    curForm.Close();

                                main_Panel.Controls.Clear();
                            });
                        }
                        else
                        {
                            if (curForm != null)
                                curForm.Close();

                            main_Panel.Controls.Clear();
                        }

                        if (lbl_SkidID.InvokeRequired)
                        {
                            lbl_SkidID.Invoke((MethodInvoker)delegate
                            {
                                lbl_SkidID.Text = "Skid ID: -";
                            });
                        }
                        else lbl_SkidID.Text = "Skid ID: -";
                        break;
                    default:
                        break;
                }
            });
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
            while (isConnected)
            {
                oSignalUserEvent.WaitOne();
                oSignalUserEvent.Reset();

                rfidCode = S7.GetStringAt(rfidReadBuffer, 2);
                string[] tempArr = rfidCode.Split(',');

                if (!tempArr[0].Equals(string.Empty) && tempArr != null)
                {
                    if (tempArr[1] != "ALIVE")
                    {
                        string tempStr = tempArr[1].Remove(0, 4);

                        using (SqlConnection conn = DBUtils.GetMainDBConnection())
                        {
                            conn.Open();
                            dt = new DataTable();
                            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserDetails", conn);
                            da.Fill(dt);
                        }

                        foreach (DataRow row in dt.Rows)
                        {
                            if (row["Card ID"].ToString().Equals(tempStr))
                            {
                                if (txt_MP_UserName.InvokeRequired)
                                {
                                    txt_MP_UserName.Invoke((MethodInvoker)delegate
                                    {
                                        txt_MP_UserName.Text = row["First Name"].ToString() + " " + row["Last Name"];
                                    });
                                }
                                else txt_MP_UserName.Text = row["First Name"].ToString() + " " + row["Last Name"];

                                if (txt_MP_AccessLvl.InvokeRequired)
                                {
                                    txt_MP_AccessLvl.Invoke((MethodInvoker)delegate
                                    {
                                        txt_MP_AccessLvl.Text = row["Access Level"].ToString();
                                    });
                                } 
                                else txt_MP_AccessLvl.Text = row["Access Level"].ToString();

                                S7.SetStringAt(userWriteBuffer, 0, 50, tempStr);
                                S7.SetStringAt(userWriteBuffer, 52, 50, txt_MP_UserName.Text.ToString());

                                short temp = short.Parse(txt_MP_AccessLvl.Text);
                                S7.SetIntAt(userWriteBuffer, 104, temp);

                                usersClient.DBWrite(3104, 0, userWriteBuffer.Length, userWriteBuffer);
                            }
                        }
                    }
                    else
                    {
                        oldUserClient.DBRead(3104, 0, oldUserReadBuffer.Length, oldUserReadBuffer);

                        if (txt_MP_UserName.InvokeRequired)
                        {
                            txt_MP_UserName.Invoke((MethodInvoker)delegate
                            {
                                txt_MP_UserName.Text = S7.GetStringAt(oldUserReadBuffer, 52);
                            });
                        }
                        else txt_MP_UserName.Text = S7.GetStringAt(oldUserReadBuffer, 52);

                        if (txt_MP_AccessLvl.InvokeRequired)
                        {
                            txt_MP_AccessLvl.Invoke((MethodInvoker)delegate
                            {
                                txt_MP_AccessLvl.Text = S7.GetIntAt(oldUserReadBuffer, 104).ToString();
                            });
                        }
                        else txt_MP_AccessLvl.Text = S7.GetIntAt(oldUserReadBuffer, 104).ToString();
                    }
                }
                else
                {
                    if (txt_MP_UserName.InvokeRequired)
                    {
                        txt_MP_UserName.Invoke((MethodInvoker)delegate
                        {
                            txt_MP_UserName.Text = string.Empty;
                        });
                    }
                    else txt_MP_UserName.Text = string.Empty;

                    if (txt_MP_AccessLvl.InvokeRequired)
                    {
                        txt_MP_AccessLvl.Invoke((MethodInvoker)delegate
                        {
                            txt_MP_AccessLvl.Text = string.Empty;
                        });
                    }
                    else txt_MP_AccessLvl.Text = string.Empty;
                    
                    S7.SetStringAt(userWriteBuffer, 0, 50, string.Empty);
                    S7.SetStringAt(userWriteBuffer, 52, 50, string.Empty);
                    S7.SetIntAt(userWriteBuffer, 104, 0);

                    usersClient.DBWrite(3104, 0, userWriteBuffer.Length, userWriteBuffer);
                }

                hasReadRFID = false; // creates unlimited rfid reading
                Thread.Sleep(100);
            }
        }
        #endregion

        #region [Obtain Station ID]
        private void ObtainStationID()
        {
            string ip = "";
            // get the local ip
            string[] arr = ip.Split('.');
            ip = arr[3];
            arr = ip.Split();
            stationID = arr[0] + arr[2];
        }
        #endregion

        #region [PLC DB Read Threads]
        private void ReadSeqDB()
        {
            while (isConnected)
            {
                sequenceClient.DBRead(3102, 0, stepReadBuffer.Length, stepReadBuffer);
                changeScreen = S7.GetBitAt(stepReadBuffer, 0, 0);
                stationNum = S7.GetIntAt(stepReadBuffer, 2);

                //Console.WriteLine(changeScreen);

                if (!stationSet)
                {
                    if (lblStationNum.InvokeRequired)
                    {
                        lblStationNum.Invoke((MethodInvoker)delegate
                        {
                            lblStationNum.Text = "STN: " + stationNum;
                        });
                    }

                    stationSet = true;
                }

                if (hasRestarted)
                {
                    oSignalSeqEvent.Set();
                    hasRestarted = false;
                }
                else if (changeScreen && !hasChanged)
                {
                    hasChanged = true;
                    oSignalSeqEvent.Set();
                }
                else if (!changeScreen && hasChanged)
                {
                    hasChanged = false;
                }

                Thread.Sleep(50);
            }
        }

        private void ReadUserDB()
        {
            while (isConnected)
            {
                usersClient.DBRead(3103, 0, rfidReadBuffer.Length, rfidReadBuffer);
                hasReadRFID = S7.GetBitAt(rfidReadBuffer, 0, 0);

                if (hasReadRFID)
                    oSignalUserEvent.Set();

                Thread.Sleep(50);
            }
        }
        #endregion

        #region [PLC Getters/Setters] 
        public string rfidCode { get; set; }
        public string user { get; set; }
        public int accessLevel { get; set; }
        public int heartBeat { get; set; }
        public int stationNum { get; set; }
        public int stepNum { get; set; }
        public int stepData { get; set; }
        #endregion

        #region [Form Closing]
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            usersClient.Disconnect();
            sequenceClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
