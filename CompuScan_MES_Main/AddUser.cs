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

namespace CompuScan_MES_Main
{
    public partial class AddUser : Form
    {
        #region [Objects and Variables]
        private bool btnCardSetup = false;
        private bool btnDone = false;
        private bool hasReadRFID = false;
        private bool closingForm = false;
        public string rfidCode { get; set; }
        private static byte[] rfidReadBuffer = new byte[54];
        private PLC_Threads plcThread;
        private DataTable dt;
        #endregion

        public AddUser()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void AddUser_Load(object sender, EventArgs e)
        {
            UserManagement owner = (UserManagement)this.Owner;
            plcThread = owner.GetPLCThread();
            rfidCode = string.Empty;
        }
        #endregion

        #region [RFID Setup Button]
        private void Btn_RFIDSetup_Click(object sender, EventArgs e)
        {
            using (LogIn frmLI = new LogIn())
            {
                frmLI.Owner = this;
                frmLI.ShowDialog();
            }

            if (rfidCode != string.Empty)
            {
                MessageBox.Show("RFID Card has been scanned successfully", "Scan Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (CheckDuplicate())
                {
                    MessageBox.Show("RFID Card has already been used", "Duplicate Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    rfidCode = string.Empty;
                }
            }
            else
            {
                MessageBox.Show("RFID Card has not been scanned successfully", "Scan Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool CheckDuplicate()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT CardID FROM UserDetails", conn);
                da.Fill(dt);
            }

            string[] tempArr = rfidCode.Split(',');
            string tempStr = tempArr[1].Remove(0, 4);

            foreach (DataRow row in dt.Rows)
            {
                if (row["CardID"].ToString().Equals(tempStr))
                {
                    return true;
                }
            }
            return false;
        }

        private void Btn_RFIDSetup_Paint(object sender, PaintEventArgs e)
        {
            if (btnCardSetup == false)
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

        private void Btn_RFIDSetup_MouseDown(object sender, MouseEventArgs e)
        {
            btnCardSetup = true;
            (sender as Button).Invalidate();
        }

        private void Btn_RFIDSetup_MouseUp(object sender, MouseEventArgs e)
        {
            btnCardSetup = false;
            (sender as Button).Invalidate();
        }
        #endregion

        #region [Done Button]
        private void Btn_Done_Click(object sender, EventArgs e)
        {
            if (!rfidCode.Equals(string.Empty))
            {
                using (SqlConnection conn = DBUtils.GetDBConnection())
                {
                    conn.Open();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO UserDetails VALUES (@FirstName, @LastName, @CardID, @AccessLevel)", conn))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", txt_AU_Name.Text.Trim());
                            cmd.Parameters.AddWithValue("@LastName", txt_AU_Surname.Text.Trim());
                            string[] tempArr = rfidCode.Split(',');
                            string tempStr = tempArr[1].Remove(0, 4);
                            cmd.Parameters.AddWithValue("@CardID", tempStr);
                            cmd.Parameters.AddWithValue("@AccessLevel", txt_AU_AccessLevel.Text.Trim());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("RFID Card Not Scanned","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_Done_Paint(object sender, PaintEventArgs e)
        {
            if (btnDone == false)
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

        private void Btn_Done_MouseDown(object sender, MouseEventArgs e)
        {
            btnDone = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Done_MouseUp(object sender, MouseEventArgs e)
        {
            btnDone = false;
            (sender as Button).Invalidate();
        }
        #endregion

        #region [Getters & Setters]
        public PLC_Threads GetPLCThread() { return plcThread; }
        #endregion
    }
}
