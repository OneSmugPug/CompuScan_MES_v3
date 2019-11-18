using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Client
{
    public partial class AwaitPart : Form
    {
        public AwaitPart(int stationNum)
        {
            InitializeComponent();
            txt_station_num.Text = stationNum.ToString();
        }
    }
}
