using CompuScan_MES_Client.Properties;
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
using Sharp7;
using System.Threading;

namespace CompuScan_MES_Client
{
    public partial class Bolt : Form
    {
        private S7Client
            transactClient = new S7Client();
        private bool
            hasReadOne = false,
            isConnected = false;
        private int
            oldReadTransactionID = 0;
        public static byte[]
            transactReadBuffer = new byte[296],
            transactWriteBuffer = new byte[296];
        private ManualResetEvent
            oSignalTransactEvent = new ManualResetEvent(false);
        private int 
            curBoltNum = 0,
            picBoxY = 12,
            picBoxX = 12,
            boltNum;
        private Dictionary<string, PictureBox> picBoxes;

        public Bolt(int boltNum, int stationNum)
        {
            InitializeComponent();
            this.boltNum = boltNum;
            txt_station_num.Text = stationNum.ToString();
            picBoxes = new Dictionary<string, PictureBox>();
        }

        private void Bolt_Load(object sender, EventArgs e)
        {
            EstablishConnection();
            
            if (isConnected)
            {
                current_bolt.Text = curBoltNum.ToString();
                total_bolt.Text = boltNum.ToString();
                PopulateBoltImages();

                Thread readTransact = new Thread(ReadTransactionDB);
                readTransact.IsBackground = true;
                readTransact.Start();

                Thread handshakeThr = new Thread(Handshake);
                handshakeThr.IsBackground = true;
                handshakeThr.Start();
            }
            
        }

        #region [Establish Connection]
        private void EstablishConnection()
        {
            transactClient.ConnectTo("192.168.1.1", 0, 1);

            if (transactClient.Connected)
                isConnected = true;
            else MessageBox.Show("Connection to PLC on 192.168.1.1 unsuccessful.", "No PLC Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region [Handshake Structure]
        private void Handshake()
        {
            while (isConnected)
            {

                oSignalTransactEvent.WaitOne(); //Thread waits for new value to be read by PLC DB Read Thread
                oSignalTransactEvent.Reset();

                switch (readTransactionID)
                {
                    case 1:
                        if (!hasReadOne)
                        {

                            // DO WORK

                            //WriteBackToPLC();
                            hasReadOne = true;

                            Thread.Sleep(50);
                        }
                        break;
                    case 99:
                        Console.WriteLine("PLC Requested to stop communication... Sending final transaction to PLC.");
                        S7.SetByteAt(transactWriteBuffer, 45, 100);
                        int result4 = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);
                        Console.WriteLine("Write Result : " + result4 + "---------------------------------------");
                        hasReadOne = false;

                        Thread.Sleep(50);
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region [PLC DB Read Threads]
        private void ReadTransactionDB()
        {
            while (isConnected)
            {
                transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110
                readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

                if (readTransactionID != oldReadTransactionID)
                {
                    Console.WriteLine("Transaction ID : " + readTransactionID + "---------------------------------------");
                    oSignalTransactEvent.Set();
                    oldReadTransactionID = readTransactionID;
                }

                Thread.Sleep(50);
            }
        }
        #endregion

        #region [PLC DB Read/Write]
        private void ReadAllValues()
        {
            transactClient.DBRead(3100, 0, transactReadBuffer.Length, transactReadBuffer);//1110

            lineID = S7.GetStringAt(transactReadBuffer, 0);

            identifier = S7.GetStringAt(transactReadBuffer, 22);

            identifierCount = S7.GetByteAt(transactReadBuffer, 44); // Number of entries in the database (Does not really need to read but write it to plc?)

            readTransactionID = S7.GetByteAt(transactReadBuffer, 45);

            channelStatus = S7.GetByteAt(transactReadBuffer, 46);

            stationStatus = S7.GetByteAt(transactReadBuffer, 47);

            errorCode = S7.GetByteAt(transactReadBuffer, 48);

            userName = S7.GetStringAt(transactReadBuffer, 50);

            equipmentID = S7.GetByteAt(transactReadBuffer, 94);

            productionData = S7.GetStringAt(transactReadBuffer, 96);
        }

        public void WriteBackToPLC()
        {
            //S7.SetStringAt(writeBuffer, 0, 20, lineID);
            //S7.SetStringAt(writeBuffer, 22, 20, identifier);
            //S7.SetByteAt(writeBuffer, 44, (byte)identifierCount);
            S7.SetByteAt(transactWriteBuffer, 45, (byte)writeTransactionID);
            //S7.SetByteAt(writeBuffer, 46, (byte)channelStatus);
            //S7.SetByteAt(writeBuffer, 47, (byte)stationStatus);
            //S7.SetByteAt(writeBuffer, 48, (byte)errorCode);
            //S7.SetStringAt(writeBuffer, 50, 20, userName);
            //S7.SetByteAt(writeBuffer, 94, (byte)equipmentID);
            //S7.SetStringAt(writeBuffer, 96, 200, productionData);
            int writeResult = transactClient.DBWrite(3101, 0, transactWriteBuffer.Length, transactWriteBuffer);//1111
            if (writeResult == 0)
            {
                Console.WriteLine("==> Successfully wrote to PLC");
            }
        }
        #endregion

        #region [PLC Getters/Setters]
        public string lineID { get; set; }
        public string identifier { get; set; }
        public int identifierCount { get; set; }
        public int readTransactionID { get; set; }
        public int writeTransactionID { get; set; }
        public int channelStatus { get; set; }
        public int stationStatus { get; set; }
        public int errorCode { get; set; }
        public string userName { get; set; }
        public int equipmentID { get; set; }
        public string productionData { get; set; }
        public bool skidPresent { get; set; }
        public bool addPallet { get; set; }
        public int palletNum { get; set; }

        #endregion

        #region [Populate Bolt Images]
        private void PopulateBoltImages()
        {
            for (int i = 0; i < boltNum; i++)
            {
                PictureBox picBox = new PictureBox();
                picBox.Name = "picBox" + (i+1);
                picBox.Image = (Image)Resources.ResourceManager.GetObject("empty_bolt_image");
                picBox.Location = new Point(picBoxX, picBoxY);
                picBox.Size = new Size(75, 75);
                picBox.SizeMode = PictureBoxSizeMode.StretchImage;
                picBoxY += 87;
                if (picBoxY > 500)
                {
                    picBoxX += 87;
                    picBoxY = 12;
                }

                picBoxes.Add(picBox.Name, picBox);

                this.Controls.Add(picBox);
            }
        }
        #endregion

        #region [Form Closing]
        private void Bolt_FormClosing(object sender, FormClosingEventArgs e)
        {
            transactClient.Disconnect();
            isConnected = false;
        }
        #endregion
    }
}
