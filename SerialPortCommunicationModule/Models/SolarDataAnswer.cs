using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class SolarDataAnswer : Answer
    {
        public int SolOnV { get; set; }
        public int SolOffV { get; set; }
        public int ManualCharge { get; set; }

        public SolarDataAnswer():base(12, 6)
        {
        }

        public SolarDataAnswer(int solOnV = 0, int solOffV = 0, int manualCharge = 0) : base(12, 6)
        {
            this.SolOnV = solOnV;
            this.SolOffV = solOffV;
            this.ManualCharge = manualCharge;
        }

        public override byte[] GetBytes()
        {
            var output = new byte[9];
            var dataPack = new byte[7];
            dataPack[0] = 165;
            dataPack[1] = (byte)this.Func;
            dataPack[2] = (byte)this.Address;
            dataPack[3] = (byte)this.Len;
            dataPack[4] = (byte)this.SolOnV;
            dataPack[5] = (byte)this.SolOffV;
            dataPack[6] = (byte)(this.ManualCharge);

            dataPack.CopyTo(output, 0);
            BitConverter.GetBytes(ComputeCrc(dataPack)).CopyTo(output, output.Length - 2);
            return output;
        }
    }
}
