using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sharp7;

namespace CompuScan_MES_Client
{
    public partial class Scan : Form
    {
        private S7Client client;
        public Scan(int stationNum)
        {
            InitializeComponent();
            txt_station_num.Text = stationNum.ToString();
        }

        public void SetS7Client(S7Client client)
        {
            this.client = client;
        }
    }
}
