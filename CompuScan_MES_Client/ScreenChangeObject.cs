using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuScan_MES_Client
{
    class ScreenChangeObject
    {
        private string screenNum, count, sequence, FEMLabel;
        private int pos, skidID;
        public ScreenChangeObject(string screenNum)
        {
            this.screenNum = screenNum;
        }

        public ScreenChangeObject(string screenNum, int skidID)
        {
            this.screenNum = screenNum;
            this.skidID = skidID;
        }

        public ScreenChangeObject(string screenNum, string count, string sequence, int pos, int skidID, string FEMLabel)
        {
            this.screenNum = screenNum;
            this.count = count;
            this.sequence = sequence;
            this.pos = pos;
            this.skidID = skidID;
            this.FEMLabel = FEMLabel;
        }

        public string GetScreenNum()
        {
            return screenNum;
        }

        public string GetCount()
        {
            return count;
        }

        public string GetSequence()
        {
            return sequence;
        }

        public int GetPos()
        {
            return pos;
        }

        public int GetSkidID()
        {
            return skidID;
        }

        public string GetFEMLabel()
        {
            return FEMLabel;
        }
    }
}
