using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Main
{
    public partial class UserManagement : Form
    {
        #region [Objects and Variables]
        private bool btnAddUser = false;
        private DataTable userTable;
        private BindingSource bs = new BindingSource();
        private int userRowIndex;
        private PLC_Threads plcThread;
        #endregion

        public UserManagement()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void UserManagement_Load(object sender, EventArgs e)
        {
            dgv_UserManagement.DataSource = bs;
            LoadUsers();
        }

        private void LoadUsers()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM UserDetails", conn);
                userTable = new DataTable();
                da.Fill(userTable);
            }
            bs.DataSource = userTable;
        }
        #endregion

        #region [Add User Button]
        private void Btn_AddUser_Click(object sender, EventArgs e)
        {
            using (AddUser frmAddUser = new AddUser())
            {
                frmAddUser.Owner = this;
                frmAddUser.ShowDialog();
            }
            LoadUsers();
        }
        private void Btn_AddUser_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddUser == false)
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

        private void Btn_AddUser_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddUser = true;
            (sender as Button).Invalidate();
        }

        private void Btn_AddUser_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddUser = false;
            (sender as Button).Invalidate();
        }
        #endregion

        #region [User Data Gridview]
        private void Dgv_UserManagement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            userRowIndex = e.RowIndex;
            using (EditDelUser frmEditDelUser = new EditDelUser())
            {
                frmEditDelUser.Owner = this;
                frmEditDelUser.ShowDialog();
            }
                
            LoadUsers();
        }

        public int GetSelectedUser()
        {
            return userRowIndex;
        }

        public DataTable GetUsers()
        {
            return userTable;
        }
        #endregion

        #region [Getters & Setters]
        public void SetPLCThread(PLC_Threads plcThread)
        {
            this.plcThread = plcThread;
        }

        public PLC_Threads GetPLCThread() { return plcThread; }
        #endregion
    }
}
