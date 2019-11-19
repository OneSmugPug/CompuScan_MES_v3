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
    }
}
