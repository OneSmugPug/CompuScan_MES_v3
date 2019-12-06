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

namespace CompuScan_MES_Main
{
    public partial class MainPage : Form
    {
        #region [Objects and Variables]
        private PLC_Threads threadUtil = new PLC_Threads();
        public string rfidCode { get; set; }
        private DataTable dt;
        private bool
            btnMainOverviewDown = false,
            btnUserDown = false,
            btnReportsDown = false,
            btnLogInDown = false,
            btnVarManDown = false,
            userLoggedIn = false;
        public Form curForm;
        #endregion

        public MainPage()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void MainPage_Load(object sender, EventArgs e)
        {
            threadUtil.EstablishConnection();
            if (threadUtil.isConnected)
            {
                Console.WriteLine("CONNECTED");
                Thread conThread = new Thread(new ThreadStart(threadUtil.Main_PLC_Interaction));
                //conThread.Start();
                //lbl_MP_PLCStatus.Text = "Online";
                //lbl_MP_PLCStatus.ForeColor = Color.Green;
            }
        }
        #endregion

        #region [Main Overview Button]
        private void Btn_MP_MainOverview_Click(object sender, EventArgs e)
        {
            main_Panel.Controls.Clear();
            curForm = null;
            //UserManagement frmUserMan = new UserManagement();
            //frmUserMan.TopLevel = false;
            //frmUserMan.TopMost = true;
            //main_Panel.Controls.Add(frmUserMan);
            //frmUserMan.Show();
        }
        private void Btn_MP_MainOverview_MouseDown(object sender, MouseEventArgs e)
        {
            btnMainOverviewDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_MainOverview_MouseUp(object sender, MouseEventArgs e)
        {
            btnMainOverviewDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_MainOverview_Paint(object sender, PaintEventArgs e)
        {
            if (btnMainOverviewDown == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [User Management Button]
        private void Btn_MP_UserManagement_Click(object sender, EventArgs e)
        {
            main_Panel.Controls.Clear();
            UserManagement frmUserMan = new UserManagement();
            frmUserMan.SetPLCThread(threadUtil);
            curForm = frmUserMan;
            curForm.TopLevel = false;
            curForm.TopMost = true;
            curForm.Dock = DockStyle.Fill;
            main_Panel.Controls.Add(curForm);
            curForm.Show();
        }
        private void Btn_MP_UserManagement_MouseDown(object sender, MouseEventArgs e)
        {
            btnUserDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_UserManagement_MouseUp(object sender, MouseEventArgs e)
        {
            btnUserDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_UserManagement_Paint(object sender, PaintEventArgs e)
        {
            if (btnUserDown == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [Reports Button]
        private void Btn_MP_Reports_Click(object sender, EventArgs e)
        {
            using (Reports frmReport = new Reports())
                frmReport.ShowDialog(this);
        }

        private void Btn_MP_Reports_MouseDown(object sender, MouseEventArgs e)
        {
            btnReportsDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_Reports_MouseUp(object sender, MouseEventArgs e)
        {
            btnReportsDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_Reports_Paint(object sender, PaintEventArgs e)
        {
            if (btnReportsDown == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [Sequence Manager Button]
        private void Btn_MP_SM_Click(object sender, EventArgs e)
        {
            main_Panel.Controls.Clear();

            SeqManager frmVM = new SeqManager();

            curForm = frmVM;
            curForm.TopLevel = false;
            curForm.TopMost = true;
            curForm.Dock = DockStyle.Fill;

            main_Panel.Controls.Add(curForm);

            curForm.Show();
        }

        private void Btn_MP_SM_MouseDown(object sender, MouseEventArgs e)
        {
            btnVarManDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_SM_MouseUp(object sender, MouseEventArgs e)
        {
            btnVarManDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_SM_Paint(object sender, PaintEventArgs e)
        {
            if (btnVarManDown == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [On Form Close]
        private void MainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            threadUtil.DisconnectFromPLC();
        }
        #endregion

        #region [Log IN Button]
        private void Btn_MP_Log_Click(object sender, EventArgs e)
        {
            if (!userLoggedIn)
            {
                using (LogIn frmLogIn = new LogIn())
                {
                    frmLogIn.Owner = this;
                    frmLogIn.ShowDialog();
                }

                if (rfidCode != string.Empty)
                {
                    if (!SetUser())
                    {
                        MessageBox.Show("User Not Found", "Log In Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        userLoggedIn = true;
                        btn_MP_Log.Text = "Log Out";
                    }
                }
            }
            else
            {
                userLoggedIn = false;
                btn_MP_Log.Text = "Log In";

                txt_MP_UserName.Clear(); ;
                txt_MP_AccessLvl.Clear();
            }
            
        }

        private bool SetUser()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserDetails", conn);
                da.Fill(dt);
            }

            string[] tempArr = rfidCode.Split(',');
            string tempStr = tempArr[1].Remove(0, 4);

            foreach (DataRow row in dt.Rows)
            {
                if (row["[Card ID]"].ToString().Equals(tempStr))
                {
                    txt_MP_UserName.Text = row["[First Name]"].ToString() + " " + row["[Last Name]"];
                    txt_MP_AccessLvl.Text = row["[Access Level]"].ToString();
                    return true;
                }
            }
            return false;
        }

        private void Btn_MP_Log_MouseDown(object sender, MouseEventArgs e)
        {
            btnLogInDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_Log_MouseUp(object sender, MouseEventArgs e)
        {
            btnLogInDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_MP_Log_Paint(object sender, PaintEventArgs e)
        {
            if (btnLogInDown == false)
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Outset);
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, (sender as Button).ClientRectangle,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset,
                    SystemColors.ControlLightLight, 2, ButtonBorderStyle.Inset);
            }
        }
        #endregion

        #region [On Size Changed]
        private void MainPage_SizeChanged(object sender, EventArgs e)
        {
            if (curForm is UserManagement || curForm is SeqManager)
            {
                main_Panel.Controls.Clear();
                main_Panel.Controls.Add(curForm);
                curForm.Show();
            }
        }
        #endregion

        #region [Getters & Setters]
        public PLC_Threads GetPLC_Threads()
        {
            return threadUtil;
        }
        #endregion
    }
}
