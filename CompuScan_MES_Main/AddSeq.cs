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
    public partial class AddSeq : Form
    {
        private bool btnNextDown = false;

        public AddSeq()
        {
            InitializeComponent();
        }

        private void Btn_Next_Click(object sender, EventArgs e)
        {

        }

        private void Btn_Next_MouseDown(object sender, MouseEventArgs e)
        {
            btnNextDown = true;
            (sender as Button).Invalidate();
        }

        private void Btn_Next_MouseUp(object sender, MouseEventArgs e)
        {
            btnNextDown = false;
            (sender as Button).Invalidate();
        }

        private void Btn_Next_Paint(object sender, PaintEventArgs e)
        {
            if (btnNextDown == false)
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

        private void AddSeq_Load(object sender, EventArgs e)
        {

        }
    }
}