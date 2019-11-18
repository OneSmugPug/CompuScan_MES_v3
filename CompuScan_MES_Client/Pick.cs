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
    public partial class Pick : Form
    {
        private int curPickNum = 0;
        public Pick(int pickNum, int stationNum)
        {
            InitializeComponent();
            current_pick.Text = curPickNum.ToString();
            total_pick.Text = pickNum.ToString();
            txt_station_num.Text = stationNum.ToString();
        }
    }
}
