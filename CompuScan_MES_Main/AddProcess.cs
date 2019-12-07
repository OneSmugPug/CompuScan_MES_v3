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
    public partial class AddProcess : Form
    {
        private DataTable dt;
        private bool
            btnDoneDown = false,
            btnCancelDown = false;

        #region [Form Load]
        public AddProcess()
        {
            InitializeComponent();
        }

        private void AddProcess_Load(object sender, EventArgs e)
        {
            Cbb_Type.SelectedIndex = 0;

            ShowPick();

            LoadComponents();
        }
        #endregion

        #region [Done Button]
        private void Btn_Done_Click(object sender, EventArgs e)
        {
            if (Cbb_Type.SelectedIndex == 0)
            {
                Type = Cbb_Type.SelectedItem.ToString();
                Component = Cbb_Comp.SelectedItem.ToString();
                Instructions = Rtb_Instruct.Text;              
            }
            else if (Cbb_Type.SelectedIndex == 1)
            {
                Type = Cbb_Type.SelectedItem.ToString();
                Component = Cbb_Comp.SelectedItem.ToString();
                Groups = Int32.Parse(Txt_Groups.Text);
                Steps = Int32.Parse(Nud_Steps.Value.ToString());
                Retries = Int32.Parse(Nud_Retries.Value.ToString());
            }
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

        #region [Load Components into Combobox]
        private void LoadComponents()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT [Short Code], [Valeo Component], Description FROM ShortCodes", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            foreach (DataRow row in dt.Rows)
            {
                string tempComp = row["Short Code"].ToString() + " - "
                    + row["Valeo Component"].ToString() + " - " + row["Description"].ToString();

                Cbb_Comp.Items.Add(tempComp);
            }

            if (Cbb_Comp.Items.Count != 0)
                Cbb_Comp.SelectedIndex = 0;
        }
        #endregion

        #region [Pick/Bolt Selection]
        private void ShowPick()
        {
            Lbl_Groups.Visible = false;
            Txt_Groups.Visible = false;

            Lbl_Steps.Visible = false;
            Nud_Steps.Visible = false;

            Lbl_Retries.Visible = false;
            Nud_Retries.Visible = false;

            Lbl_Instruct.Visible = true;
            Rtb_Instruct.Visible = true;
        }

        private void ShowBolt()
        {
            Lbl_Groups.Visible = true;
            Txt_Groups.Visible = true;

            Lbl_Steps.Visible = true;
            Nud_Steps.Visible = true;

            Lbl_Retries.Visible = true;
            Nud_Retries.Visible = true;

            Lbl_Instruct.Visible = false;
            Rtb_Instruct.Visible = false;
        }

        private void Cbb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Cbb_Type.SelectedIndex == 0)
                ShowPick();
            else if (Cbb_Type.SelectedIndex == 1)
                ShowBolt();
        }
        #endregion

        #region [Steps Value Changed]
        private void Nud_Steps_ValueChanged(object sender, EventArgs e)
        {
            Txt_Groups.Text = (Math.Floor(Nud_Steps.Value / 17) + 1).ToString();
        }
        #endregion

        public string Type { get; set; }
        public string Component { get; set; }
        public string Instructions { get; set; }
        public int Groups { get; set; }
        public int Steps { get; set; }
        public int Retries { get; set; }
    }
}
