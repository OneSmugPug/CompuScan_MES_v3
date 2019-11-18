using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Client
{
    class BoltObject
    {
        private PictureBox image { get; set; }
        private bool bolted { get; set; }

        public BoltObject()
        {
            image.Image = Image.FromFile("empty_bolt_image.png");
            bolted = false;
        }
    }
}
