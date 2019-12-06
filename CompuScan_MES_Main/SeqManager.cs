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
    public partial class SeqManager : Form
    {
        #region [Objects & Variables]
        private bool
            btnSeqDown = false,
            btnVarDown = false,
            btnSCDown = false,
            btnImportDown = false,
            btnExportDown = false,
            btnAddSeqDown = false,
            btnRemoveSeqDown = false,
            btnRemoveSCDown = false,
            btnAddSCDown = false,
            btnAddVarDown = false,
            btnRemoveVar = false;
        private BindingSource bs = new BindingSource();
        private DataTable dt;
        #endregion

        #region [Form Load]
        public SeqManager()
        {
            InitializeComponent();
        }

        private void VariantManager_Load(object sender, EventArgs e)
        {
            btnSeqDown = true;
            Btn_Seq.Invalidate();

            Btn_Seq.PerformClick();

            DGV_SM.DataSource = bs;
        }
        #endregion

        #region [Tab Buttons]

        #region [Sequences Button]
        private void Btn_Seq_Click(object sender, EventArgs e)
        {
            if (Btn_Export.Visible && Btn_Import.Visible)
            {
                Btn_Export.Visible = false;
                Btn_Import.Visible = false;
            }

            if (!Btn_RemoveSeq.Visible && !Btn_AddSeq.Visible)
            {
                Btn_RemoveSeq.Visible = true;
                Btn_AddSeq.Visible = true;
            }

            if (Btn_RemoveSC.Visible && Btn_AddSC.Visible)
            {
                Btn_RemoveSC.Visible = false;
                Btn_AddSC.Visible = false;
            }

            if (Btn_RemoveVar.Visible && Btn_AddVar.Visible)
            {
                Btn_AddVar.Visible = false;
                Btn_RemoveVar.Visible = false;
            }

            LoadSequences();
        }

        private void LoadSequences()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Sequences", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;

            Cbb_Column.Items.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                Cbb_Column.Items.Add(column.ColumnName);
            }

            Cbb_Column.SelectedIndex = 0;
        }

        private void Btn_Seq_MouseDown(object sender, MouseEventArgs e)
        {
            ResetButtons();

            btnSeqDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Seq_Paint(object sender, PaintEventArgs e)
        {
            if (btnSeqDown == false)
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

        #region [Variant Button]
        private void Btn_Var_Click(object sender, EventArgs e)
        {
            Btn_Import.Click -= ShortCodesImport;
            Btn_Export.Click -= ShortCodesExport;

            Btn_Import.Click += VariantImport;
            Btn_Export.Click += VariantExport;

            if (!Btn_Export.Visible && !Btn_Import.Visible)
            {
                Btn_Export.Visible = true;
                Btn_Import.Visible = true;
            }

            if (Btn_RemoveSeq.Visible && Btn_AddSeq.Visible)
            {
                Btn_RemoveSeq.Visible = false;
                Btn_AddSeq.Visible = false;
            }
               
            if (Btn_RemoveSC.Visible && Btn_AddSC.Visible)
            {
                Btn_RemoveSC.Visible = false;
                Btn_AddSC.Visible = false;
            }
            
            if (!Btn_RemoveVar.Visible && !Btn_AddVar.Visible)
            {
                Btn_AddVar.Visible = true;
                Btn_RemoveVar.Visible = true;
            }

            LoadVariants();
        }

        private void LoadVariants()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Variants", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;

            Cbb_Column.Items.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                Cbb_Column.Items.Add(column.ColumnName);
            }

            Cbb_Column.SelectedIndex = 0;
        }

        private void Btn_Var_MouseDown(object sender, MouseEventArgs e)
        {
            if (btnSeqDown)
                Btn_Seq.Invalidate();

            ResetButtons();

            btnVarDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Var_Paint(object sender, PaintEventArgs e)
        {
            if (btnVarDown == false)
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

        #region [Short Codes Button]
        private void Btn_ShortC_Click(object sender, EventArgs e)
        {
            Btn_Import.Click -= VariantImport;
            Btn_Export.Click -= VariantExport;

            Btn_Import.Click += ShortCodesImport;
            Btn_Export.Click += ShortCodesExport;

            if (!Btn_Export.Visible && !Btn_Import.Visible)
            {
                Btn_Export.Visible = true;
                Btn_Import.Visible = true;
            }

            if (Btn_RemoveSeq.Visible && Btn_AddSeq.Visible)
            {
                Btn_RemoveSeq.Visible = false;
                Btn_AddSeq.Visible = false;
            }

            if (!Btn_RemoveSC.Visible && !Btn_AddSC.Visible)
            {
                Btn_RemoveSC.Visible = true;
                Btn_AddSC.Visible = true;
            }

            if (Btn_RemoveVar.Visible && Btn_AddVar.Visible)
            {
                Btn_AddVar.Visible = false;
                Btn_RemoveVar.Visible = false;
            }

            LoadShortCodes();
        }

        private void LoadShortCodes()
        {
            using (SqlConnection conn = DBUtils.GetDBConnection())
            {
                conn.Open();

                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM ShortCodes", conn);
                dt = new DataTable();
                da.Fill(dt);
            }

            bs.DataSource = dt;

            Cbb_Column.Items.Clear();

            foreach (DataColumn column in dt.Columns)
            {
                Cbb_Column.Items.Add(column.ColumnName);
            }

            Cbb_Column.SelectedIndex = 0;
        }

        private void Btn_ShortC_MouseDown(object sender, MouseEventArgs e)
        {
            if (btnSeqDown)
                Btn_Seq.Invalidate();

            ResetButtons();

            btnSCDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_ShortC_Paint(object sender, PaintEventArgs e)
        {
            if (btnSCDown == false)
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

        #region [Reset Tab Buttons]
        private void ResetButtons()
        {
            btnSeqDown = false;
            btnVarDown = false;
            btnSCDown = false;
        }
        #endregion

        #endregion

        #region [Import Button]
        private void Btn_Import_MouseDown(object sender, MouseEventArgs e)
        {
            btnImportDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Import_MouseUp(object sender, MouseEventArgs e)
        {
            btnImportDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Import_Paint(object sender, PaintEventArgs e)
        {
            if (btnImportDown == false)
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

        private void ShortCodesImport(object sender, EventArgs e)
        {
            Console.WriteLine("SCI");
        }

        private void VariantImport(object sender, EventArgs e)
        {
            Console.WriteLine("VI");
        }
        #endregion

        #region [Export Button]
        private void Btn_Export_MouseDown(object sender, MouseEventArgs e)
        {
            btnExportDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Export_MouseUp(object sender, MouseEventArgs e)
        {
            btnExportDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Export_Paint(object sender, PaintEventArgs e)
        {
            if (btnExportDown == false)
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

        private void ShortCodesExport(object sender, EventArgs e)
        {
            Console.WriteLine("SCE");
        }

        private void VariantExport(object sender, EventArgs e)
        {
            Console.WriteLine("VE");
        }
        #endregion

        #region [Add/Remove Buttons]

        #region [Add Sequence Button]
        private void Btn_AddSeq_Click(object sender, EventArgs e)
        {
            using (AddSeq frmAddSeq = new AddSeq())
            {
                frmAddSeq.Owner = this;
                frmAddSeq.ShowDialog();

                if (frmAddSeq.DialogResult == DialogResult.OK)
                {

                }
            }
        }

        private void Btn_AddSeq_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddSeqDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_AddSeq_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddSeqDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_AddSeq_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddSeqDown == false)
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

        #region [Remove Sequence Button]
        private void Btn_RemoveSeq_Click(object sender, EventArgs e)
        {

        }

        private void Btn_RemoveSeq_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemoveSeqDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveSeq_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemoveSeqDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveSeq_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemoveSeqDown == false)
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

        #region [Remove Short Code Button]
        private void Btn_RemoveSC_Click(object sender, EventArgs e)
        {

        }

        private void Btn_RemoveSC_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemoveSCDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveSC_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemoveSCDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveSC_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemoveSCDown == false)
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

        #region [Add Short Code Button]
        private void Btn_AddSC_Click(object sender, EventArgs e)
        {

        }

        private void Btn_AddSC_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddSCDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_AddSC_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddSCDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_AddSC_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddSCDown == false)
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

        #region [Add Variant Button]
        private void Btn_AddVar_Click(object sender, EventArgs e)
        {

        }

        private void Btn_AddVar_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddVarDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_AddVar_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddVarDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_AddVar_Paint(object sender, PaintEventArgs e)
        {
            if (btnAddVarDown == false)
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

        #region [Remove Variant Button]
        private void Btn_RemoveVar_Click(object sender, EventArgs e)
        {

        }

        private void Btn_RemoveVar_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemoveVar = true;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveVar_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemoveVar = false;
            (sender as Button).Invalidate();
        }

        private void Btn_RemoveVar_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemoveVar == false)
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

        #endregion

        #region [Datagridview Filter]
        private void Txt_Search_TextChanged(object sender, EventArgs e)
        {
            if (dt.Columns[Cbb_Column.SelectedIndex].DataType == typeof(Int32))
                dt.DefaultView.RowFilter = string.Format("CONVERT(" + Cbb_Column.SelectedItem.ToString() + ", 'System.String') LIKE '{0}%'", Txt_Search.Text);
            else if (dt.Columns[Cbb_Column.SelectedIndex].DataType == typeof(String))
                dt.DefaultView.RowFilter = string.Format(Cbb_Column.SelectedItem.ToString() + " LIKE '{0}%'", Txt_Search.Text);
        }
        #endregion
    }
}
