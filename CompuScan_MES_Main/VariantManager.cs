using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Main
{
    public partial class VariantManager : Form
    {
        private PLC_Threads threadUtil;
        private bool
            btnAddVarDown = false,
            btnRemVarDown = false,
            btnVarImpDown = false,
            btnVarExpDown = false,
            btnSCImp = false,
            btnSCExp = false;

        public VariantManager()
        {
            InitializeComponent();
        }

        private void VariantManager_Load(object sender, EventArgs e)
        {
            
        }

        public void SetPLCThread(PLC_Threads threadUtil)
        {
            this.threadUtil = threadUtil;
        }

        #region [Add Variant Button]
        private void btn_AddVar_MouseDown(object sender, MouseEventArgs e)
        {
            btnAddVarDown = true;
            (sender as Button).Invalidate();
        }

        private void btn_AddVar_MouseUp(object sender, MouseEventArgs e)
        {
            btnAddVarDown = false;
            (sender as Button).Invalidate();
        }

        private void btn_AddVar_Paint(object sender, PaintEventArgs e)
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
        private void btn_RemVar_MouseDown(object sender, MouseEventArgs e)
        {
            btnRemVarDown = true;
            (sender as Button).Invalidate();
        }

        private void btn_RemVar_MouseUp(object sender, MouseEventArgs e)
        {
            btnRemVarDown = false;
            (sender as Button).Invalidate();
        }

        private void btn_RemVar_Paint(object sender, PaintEventArgs e)
        {
            if (btnRemVarDown == false)
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

        #region [Varaint Import Button]
        private void btn_VarImp_MouseDown(object sender, MouseEventArgs e)
        {
            btnVarImpDown = true;
            (sender as Button).Invalidate();
        }

        private void btn_VarImp_MouseUp(object sender, MouseEventArgs e)
        {
            btnVarImpDown = false;
            (sender as Button).Invalidate();
        }

        private void btn_VarImp_Paint(object sender, PaintEventArgs e)
        {
            if (btnVarImpDown == false)
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

        #region [Variant Export Button]
        private void btn_VarExp_MouseDown(object sender, MouseEventArgs e)
        {
            btnVarExpDown = true;
            (sender as Button).Invalidate();
        }

        private void btn_VarExp_MouseUp(object sender, MouseEventArgs e)
        {
            btnVarExpDown = false;
            (sender as Button).Invalidate();
        }

        private void btn_VarExp_Paint(object sender, PaintEventArgs e)
        {
            if (btnVarExpDown == false)
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

        #region [Short Code Import Button]
        private void btn_SCImp_MouseDown(object sender, MouseEventArgs e)
        {
            btnSCImp = true;
            (sender as Button).Invalidate();
        }

        private void btn_SCImp_MouseUp(object sender, MouseEventArgs e)
        {
            btnSCImp = false;
            (sender as Button).Invalidate();
        }

        private void btn_SCImp_Paint(object sender, PaintEventArgs e)
        {
            if (btnSCImp == false)
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

        #region [Short Code Export Button]
        private void btn_SCExp_MouseDown(object sender, MouseEventArgs e)
        {
            btnSCExp = true;
            (sender as Button).Invalidate();
        }

        private void btn_SCExp_MouseUp(object sender, MouseEventArgs e)
        {
            btnSCExp = false;
            (sender as Button).Invalidate();
        }

        private void btn_SCExp_Paint(object sender, PaintEventArgs e)
        {
            if (btnSCExp == false)
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
    }
}
