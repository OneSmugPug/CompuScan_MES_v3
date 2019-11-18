using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Client
{
    public partial class Bolt : Form
    {
        private int curBoltNum = 0;
        private int picBoxY = 12;
        private int picBoxX = 12;
        private int boltNum;
        public Bolt(int boltNum, int stationNum)
        {
            InitializeComponent();
            this.boltNum = boltNum;
            txt_station_num.Text = stationNum.ToString();
        }

        private void Bolt_Load(object sender, EventArgs e)
        {
            current_bolt.Text = curBoltNum.ToString();
            total_bolt.Text = boltNum.ToString();

            for (int i = 0; i < boltNum; i++)
            {
                PictureBox picBox = new PictureBox();
                picBox.Image = Image.FromFile("C:\\Users\\dawid\\source\\repos\\CompuScan_MES_v2\\CompuScan_MES\\CompuScan_MES_Client\\Resources\\empty_bolt_image.png");
                picBox.Location = new Point(picBoxX, picBoxY);
                picBox.Size = new Size(75, 75);
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                picBoxY += 87;
                if (picBoxY > 500)
                {
                    picBoxX += 87;
                    picBoxY = 12;
                }

                this.Controls.Add(picBox);
            }
        }
    }
}
