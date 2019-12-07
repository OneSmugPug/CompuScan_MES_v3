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
    public partial class AddSeq : Form
    {
        private bool
            btnDoneDown = false,
            btnCancelDown = false,
            btnAddProcDown = false,
            btnRemoveProcDown = false;
        private DataTable dt;

        #region [Form Load]
        public AddSeq()
        {
            InitializeComponent();
        }

        private void AddSeq_Load(object sender, EventArgs e)
        {
            Cbb_Line.SelectedIndex = 0;

            LoadStations();

            LoadVariants();
        }
        #endregion

        #region [Load Stations into Combobox]
        private void LoadStations()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Stations WHERE Line='" + Cbb_Line.SelectedItem + "'", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                string tempStation = row["Station Number"].ToString();

                if (!row["Description"].ToString().Equals(String.Empty))
                    tempStation = tempStation + " - " + row["Description"].ToString();

                Cbb_Station.Items.Add(tempStation);
            }

            if (Cbb_Station.Items.Count != 0)
                Cbb_Station.SelectedIndex = 0;
        }
        #endregion

        #region [Load Variants into Combobox]
        private void LoadVariants()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT Reference, Model, Variant FROM Variants", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                string tempVar = row["Reference"].ToString() + "_" 
                    + row["Model"].ToString() + "_" + row["Variant"].ToString();

                Cbb_Var.Items.Add(tempVar);
            }

            if (Cbb_Var.Items.Count != 0)
                Cbb_Var.SelectedIndex = 0;
        }
        #endregion

        #region [Done Button]
        private void Btn_Done_Click(object sender, EventArgs e)
        {
            Process = Lb_Proc;

            string[] tempArr = Cbb_Station.SelectedItem.ToString().Split('-');
            Station = tempArr[0];
            Variant = Cbb_Var.SelectedItem.ToString();
        }

        private void Btn_Done_MouseDown(object sender, MouseEventArgs e)
        {
            btnDoneDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Done_MouseUp(object sender, MouseEventArgs e)
        {
            btnDoneDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Done_Paint(object sender, PaintEventArgs e)
        {
            if (btnDoneDown == false)
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
        private void Btn_Cancel_MouseDown(object sender, MouseEventArgs e)
        {
            btnCancelDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Cancel_MouseUp(object sender, MouseEventArgs e)
        {
            btnCancelDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Cancel_Paint(object sender, PaintEventArgs e)
        {
            if (btnCancelDown == false)
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

        #region [Add Process Button]
        private void Btn_AddProc_Click(object sender, EventArgs e)
        {
            using (AddProcess frmAddProc = new AddProcess())
            {
                frmAddProc.ShowDialog();

                if (frmAddProc.DialogResult == DialogResult.OK)
                {
                    string type = frmAddProc.Type;
                    string[] temp = type.Split(null);
                    type = temp[1];
                    type = type.Substring(1, 1);

                    if (type.Equals("3"))
                    {
                        string line = frmAddProc.Type + "," + frmAddProc.Component + "," + frmAddProc.Instructions;
                        Lb_Proc.Items.Add(line);
                    }
                    else if (type.Equals("4"))
                    {
                        for (int i = 1; i <= frmAddProc.Groups; i++)
                        {
                            string line = frmAddProc.Type + ",";

                            if (i != frmAddProc.Groups)
                            {
                                line += 16 + ",";
                                frmAddProc.Steps -= 16;
                            }
                            else line += frmAddProc.Steps + ",";

                            line += frmAddProc.Retries + "," + frmAddProc.Component;
                            Lb_Proc.Items.Add(line);
                        }
                    }
                }
            }
        }

        private void Btn_AddProc_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddProcDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_AddProc_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddProcDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_AddProc_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddProcDown == false)
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

        #region [Remove Process Button]
        private void Btn_RemoveProc_Click(object sender, EventArgs e)
        {
            if (Lb_Proc.SelectedIndex != -1)
                Lb_Proc.Items.RemoveAt(Lb_Proc.SelectedIndex);
            else MessageBox.Show("Select an item in the list to remove.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void Btn_RemoveProc_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemoveProcDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveProc_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemoveProcDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveProc_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemoveProcDown == false)
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

        private void Cbb_Line_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStations();
        }

        public ListBox Process { get; set; }

        public string Station { get; set; }

        public string Variant { get; set; }
    }
}