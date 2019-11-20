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
    public partial class EditDelUser : Form
    {
        #region [Objects and Variables]
        private bool btnCardUpdate = false;
        private bool btnDone = false;
        private bool btnCancel = false;
        private bool btnDelete = false;
        private int SELECTED_USER;
        private DataTable userDataTable;
        private string prevName;
        private string prevSurname;
        private string prevAccessLevel;
        private string prevCardID;
        private bool hasReadRFID = false;
        private bool closingForm = false;
        public string rfidCode { get; set; }
        private static byte[] rfidReadBuffer = new byte[54];
        private PLC_Threads plcThread;
        private DataTable dt;
        #endregion

        public EditDelUser()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void EditDelUser_Load(object sender, EventArgs e)
        {
            UserManagement owner = (UserManagement)this.Owner;
            plcThread = owner.GetPLCThread();

            SELECTED_USER = owner.GetSelectedUser();
            userDataTable = owner.GetUsers();
            DataRow row = userDataTable.Rows[SELECTED_USER];
            txt_EDU_Name.Text = row["[First Name]"].ToString();
            prevName = row["[First Name]"].ToString();
            txt_EDU_Surname.Text = row["[Last Name]"].ToString();
            prevSurname = row["[Last Name]"].ToString();
            txt_EDU_AccessLevel.Text = row["[Access Level]"].ToString();
            prevAccessLevel = row["[Access Level]"].ToString();
            prevCardID = row["[Card ID]"].ToString();

            rfidCode = string.Empty;
        }
        #endregion

        #region [Update RFID Button]
        private void Btn_EDU_UpdateRFID_Click(object sender, EventArgs e)
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
                else
                {
                    UpdateRFID();
                }
            }
            else
            {
                MessageBox.Show("RFID Card has not been scanned successfully", "Scan Unsuccessful", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UpdateRFID()
        {
            string[] tempArr = rfidCode.Split(',');
            string tempStr = tempArr[1].Remove(0, 4);

            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                try
                {
                    if (!prevCardID.Equals(string.Empty))
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE UserDetails SET [Card ID] = @NewCardID WHERE [Card ID] = @OldCardID", conn))
                        {
                            cmd.Parameters.AddWithValue("@NewCardID", tempStr);
                            cmd.Parameters.AddWithValue("@OldCardID", prevCardID);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("UPDATE UserDetails SET [Card ID] = @NewCardID WHERE [First Name] = " +
                            "@FirstName AND [Last Name] = @LastName AND [Access Level] = @AccessLevel", conn))
                        {
                            cmd.Parameters.AddWithValue("@FirstName", prevName);
                            cmd.Parameters.AddWithValue("@LastName", prevSurname);
                            cmd.Parameters.AddWithValue("@NewCardID", tempStr);
                            cmd.Parameters.AddWithValue("@AccessLevel", prevAccessLevel);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CheckDuplicate()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();
                dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter("SELECT [Card ID] FROM UserDetails", conn);
                da.Fill(dt);
            }

            string[] tempArr = rfidCode.Split(',');
            string tempStr = tempArr[1].Remove(0, 4);

            foreach (DataRow row in dt.Rows)
            {
                if (row["[Card ID]"].ToString().Equals(tempStr))
                {
                    return true;
                }
            }
            return false;
        }

        private void Btn_EDU_UpdateRFID_MouseDown(object sender, MouseEventArgs e)
        {
            btnCardUpdate = true;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_UpdateRFID_MouseUp(object sender, MouseEventArgs e)
        {
            btnCardUpdate = false;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_UpdateRFID_Paint(object sender, PaintEventArgs e)
        {
            if (btnCardUpdate == false)
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

        #region [Done Button]
        private void Btn_EDU_Done_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("UPDATE UserDetails SET [Access Level] = @AccessLevel" +
                        " WHERE [First Name] = @FirstName AND [Last Name] = @LastName", conn))
                    {
                        sqlCommand.Parameters.AddWithValue("@FirstName", txt_EDU_Name.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@LastName", txt_EDU_Surname.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@AccessLevel", txt_EDU_AccessLevel.Text.Trim());


                        sqlCommand.ExecuteNonQuery();

                        MessageBox.Show("User successfully updated.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Btn_EDU_Done_MouseDown(object sender, MouseEventArgs e)
        {
            btnDone = true;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Done_MouseUp(object sender, MouseEventArgs e)
        {
            btnDone = false;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Done_Paint(object sender, PaintEventArgs e)
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
        #endregion

        #region [Delete Button]
        private void Btn_EDU_Delete_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand("DELETE FROM UserDetails WHERE [First Name] = @FirstName AND [Last Name] = @LastName AND [Access Level] = @AccessLevel", conn))
                    {
                        sqlCommand.Parameters.AddWithValue("@FirstName", txt_EDU_Name.Text.Trim());
                        sqlCommand.Parameters.AddWithValue("@LastName", txt_EDU_Surname.Text.Trim());
                        //sqlCommand.Parameters.AddWithValue("@CardID", DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@AccessLevel", txt_EDU_AccessLevel.Text.Trim());


                        sqlCommand.ExecuteNonQuery();

                        MessageBox.Show("User successfully deleted.", "Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Btn_EDU_Delete_MouseDown(object sender, MouseEventArgs e)
        {
            btnDelete = true;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Delete_MouseUp(object sender, MouseEventArgs e)
        {
            btnDelete = false;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Delete_Paint(object sender, PaintEventArgs e)
        {
            if (btnDelete == false)
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

        #region [Cancel Button]
        private void Btn_EDU_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Btn_EDU_Cancel_MouseDown(object sender, MouseEventArgs e)
        {
            btnCancel = true;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Cancel_MouseUp(object sender, MouseEventArgs e)
        {
            btnCancel = false;
            (sender as Button).Invalidate();
        }

        private void Btn_EDU_Cancel_Paint(object sender, PaintEventArgs e)
        {
            if (btnCancel == false)
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

        #region [Getters & Setters]
        public PLC_Threads GetPLCThread() { return plcThread; }
        #endregion
    }
}
