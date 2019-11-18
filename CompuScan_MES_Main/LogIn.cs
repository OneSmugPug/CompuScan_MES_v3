using Sharp7;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompuScan_MES_Main
{
    public partial class LogIn : Form
    {
        #region [Objects and Variables]
        private static byte[] rfidReadBuffer = new byte[54];
        public string rfidCode { get; set; }
        private bool hasReadRFID = false;
        private bool closingForm = false;
        private int plcDB;
        private PLC_Threads plcThread;

        private EditDelUser frmEDU;
        private AddUser frmAU;
        private MainPage frmMain;
        #endregion

        public LogIn()
        {
            InitializeComponent();
        }

        #region [Load Method]
        private void LogIn_Load(object sender, EventArgs e)
        {
            switch ((this.Owner).GetType().Name)
            {
                case "EditDelUser":
                    frmEDU = (EditDelUser)this.Owner;
                    plcThread = frmEDU.GetPLCThread();
                    plcDB = 3003;
                    break;
                case "AddUser":
                    frmAU = (AddUser)this.Owner;
                    plcThread = frmAU.GetPLCThread();
                    plcDB = 3003;
                    break;
                case "MainPage":
                    frmMain = (MainPage)this.Owner;
                    plcThread = frmMain.GetPLC_Threads();
                    plcDB = 3003;
                    break;
                default: break;
            }

            Thread rfidThread = new Thread(new ThreadStart(ReadRFID));
            rfidThread.Start();
        }
        #endregion

        #region [Read RFID]
        private void ReadRFID()
        {
            while (!hasReadRFID)
            {
                int dbread = plcThread.client.DBRead(plcDB, 0, rfidReadBuffer.Length, rfidReadBuffer);
                hasReadRFID = S7.GetBitAt(rfidReadBuffer, 0, 0);

                if (hasReadRFID)
                {
                    rfidCode = S7.GetStringAt(rfidReadBuffer, 2);
                    Console.WriteLine(rfidCode);

                    if (frmEDU != null)
                        frmEDU.rfidCode = this.rfidCode;
                    else if (frmAU != null)
                        frmAU.rfidCode = this.rfidCode;
                    else if (frmMain != null)
                        frmMain.rfidCode = this.rfidCode;

                    this.Invoke((MethodInvoker)delegate
                   {
                       this.Close();
                   });
                }
                if (closingForm)
                {
                    break;
                }
                Thread.Sleep(100);
            }
        }
        #endregion

        private void LogIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            closingForm = true;
        }
    }
}
