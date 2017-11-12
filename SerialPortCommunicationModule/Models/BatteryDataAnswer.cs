using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class BatteryDataAnswer : Answer
    {
        public int BatCapacity { get; set; }
        public int LowVoltagePoint { get; set; }
        public int LowVoltageRecoverPoint { get; set; }
        public int OverVoltagePoint { get; set; }
        public int OverVoltageRecoverPoint { get; set; }
        public int FloatChargePoint { get; set; }
        public int OverVoltageCloseOutput { get; set; }
        public int OverVoltageRecoverCloseOutput { get; set; }

        public BatteryDataAnswer() : base (14, 19)
        {
        }

        public BatteryDataAnswer(int batCap, int lowV, int lowVrecover, int overV, int overVrecover, int floatCharge, int overVclose, int overVcloseRecover) : this()
        {
            this.BatCapacity = batCap;
            this.LowVoltagePoint = lowV;
            this.LowVoltageRecoverPoint = lowVrecover;
            this.OverVoltagePoint = overV;
            this.OverVoltageRecoverPoint = overVrecover;
            this.FloatChargePoint = floatCharge;
            this.OverVoltageCloseOutput = overVclose;
            this.OverVoltageRecoverCloseOutput = overVcloseRecover;
        }

        public override byte[] GetBytes()
        {
            var output = new byte[22];
            var dataPack = new byte[20];
            dataPack[0] = 165;
            dataPack[1] = (byte)this.Func;
            dataPack[2] = (byte)this.Address;
            dataPack[3] = (byte)this.Len;

            BitConverter.GetBytes(((ushort)this.BatCapacity)).CopyTo(dataPack, 4);
            BitConverter.GetBytes(((ushort)this.LowVoltagePoint)).CopyTo(dataPack, 6);
            BitConverter.GetBytes(((ushort)this.LowVoltageRecoverPoint)).CopyTo(dataPack, 8);
            BitConverter.GetBytes(((ushort)this.OverVoltagePoint)).CopyTo(dataPack, 10);
            BitConverter.GetBytes(((ushort)this.OverVoltageRecoverPoint)).CopyTo(dataPack, 12);
            BitConverter.GetBytes(((ushort)this.FloatChargePoint)).CopyTo(dataPack, 14);
            BitConverter.GetBytes(((ushort)this.OverVoltageCloseOutput)).CopyTo(dataPack, 16);
            BitConverter.GetBytes(((ushort)this.OverVoltageRecoverCloseOutput)).CopyTo(dataPack, 18);
            
            dataPack.CopyTo(output, 0);
            BitConverter.GetBytes(Answer.ComputeCrc(dataPack)).CopyTo(output, output.Length - 2);
            return output;
        }
    }
}
