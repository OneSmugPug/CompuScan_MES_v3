using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompuScan_MES_Client
{
    class ScreenChangeObject
    {
        private string screenNum;
        private string count;
        private string sequence;
        private int pos;

        public ScreenChangeObject(string screenNum)
        {
            this.screenNum = screenNum;
        }

        public ScreenChangeObject(string screenNum, string count, string sequence)
        {
            this.screenNum = screenNum;
            this.count = count;
            this.sequence = sequence;
        }

        public ScreenChangeObject(string screenNum, string count, string sequence, int pos)
        {
            this.screenNum = screenNum;
            this.count = count;
            this.sequence = sequence;
            this.pos = pos;
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
    }
}
