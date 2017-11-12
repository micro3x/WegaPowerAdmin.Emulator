using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class WindDataAnswer : Answer
    {
        public int WindMaxV { get; set; }
        public int WindChargeManual { get; set; }
        public int WindMaxA { get; set; }
        public int WindManualBrake { get; set; }
        public int WindMaxRpm { get; set; }
        public int MpptSwitch { get; set; }
        public int WindStartChargeV { get; set; }
        public int WindBrakeTime { get; set; }
        public int WindMagnetPoleDouble { get; set; }

        public WindDataAnswer() : base(11, 13)
        {
        }

        public WindDataAnswer(
            int windMaxV = 0, int windChargeM = 0, int windMaxA = 0,
            int windManualB = 0, int windMaxRpm = 0, int mpptSw = 0,
            int windStartCharge = 0, int windBrakeTime = 0, int windMagnetPoleDouble = 0) : this()
        {
            this.WindMaxV = windMaxV;
            this.WindChargeManual = windChargeM;
            this.WindMaxA = windMaxA;
            this.WindManualBrake = windManualB;
            this.WindMaxRpm = windMaxRpm;
            this.MpptSwitch = mpptSw;
            this.WindStartChargeV = windStartCharge;
            this.WindBrakeTime = windBrakeTime;
            this.WindMagnetPoleDouble = windMagnetPoleDouble;
        }

        public override byte[] GetBytes()
        {
            var output = new byte[16];
            var dataPack = new byte[14];
            var head = new byte[4];
            dataPack[0] = 165;
            dataPack[1] = (byte)this.Func;
            dataPack[2] = (byte)this.Address;
            dataPack[3] = (byte)this.Len;

            ushort first16 = (ushort)((this.WindChargeManual << 10) | (this.WindMaxV));
            ushort second16 = (ushort)((this.WindManualBrake << 10) | (this.WindMaxA));
            ushort tirth16 = (ushort)((this.MpptSwitch << 10) | (this.WindMaxRpm << 6));
            BitConverter.GetBytes(first16).CopyTo(dataPack, 4);
            BitConverter.GetBytes(second16).CopyTo(dataPack, 6);
            BitConverter.GetBytes(tirth16).CopyTo(dataPack, 8);

            ushort forth16 = (ushort)(this.WindStartChargeV);
            BitConverter.GetBytes(forth16).CopyTo(dataPack, 10);

            ushort fifth16 = (ushort)((this.WindMagnetPoleDouble<< 8) | this.WindBrakeTime);
            BitConverter.GetBytes(fifth16).CopyTo(dataPack, 12); ;
            dataPack.CopyTo(output, 0);
            BitConverter.GetBytes(ComputeCrc(dataPack)).CopyTo(output, 14);
            return output;
        }

    }
}
