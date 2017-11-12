using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class StateAnswer : Answer
    {
        public StateAnswer() : base(10, 17)
        {
        }

        public StateAnswer(int batV = 0,
            int out1 =0, int solV = 0, int out2 = 0, int windV = 0,
            int mppt = 0, int windA = 0, int outA = 0, int rpm = 0,
            int solA = 0, int dumpA = 0, int batCapacity = 0,
            int batState = 0, int dayOrNight = 0) : base(10, 17)
        {
            this.BatVoltage = batV;
            this.Output1 = out1;
            this.SolVoltage = solV;
            this.Output2 = out2;
            this.WindVoltage = windV;
            this.MpptType = mppt;
            this.WindChargeA = windA;
            this.OutputA = outA;
            this.WindRpm = rpm;
            this.SolarChargeA = solA;
            this.DumpLoadA = dumpA;
            this.BatteryCapacityPercent = batCapacity;
            this.BatteryState = batState;
            this.DayOrNight = dayOrNight;

        }

        public int BatVoltage { get; set; }
        public int Output1 { get; set; }
        public int SolVoltage { get; set; }
        public int Output2 { get; set; }
        public int WindVoltage { get; set; }
        public int MpptType { get; set; }
        public int WindChargeA { get; set; }
        public int OutputA { get; set; }
        public int WindRpm { get; set; }
        public int SolarChargeA { get; set; }
        public int DumpLoadA { get; set; }
        public int BatteryCapacityPercent { get; set; }
        public int BatteryState { get; set; }
        public int DayOrNight { get; set; }


        public override byte[] GetBytes()
        {
            var output = new byte[20];
            var dataPack = new byte[18];
            dataPack[0] = 165;
            dataPack[1] = (byte)this.Func;
            dataPack[2] = (byte)this.Address;
            dataPack[3] = (byte)this.Len;

            ushort first16 = (ushort)((this.Output1 << 14) | (this.BatVoltage));
            ushort second16 = (ushort)((this.Output2 << 14) | this.SolVoltage );
            ushort tirth16 = (ushort)((this.MpptType << 14) | this.WindVoltage);
            var test = BitConverter.GetBytes(first16);

            dataPack[4] = test[0];
            dataPack[5] = test[1];

            BitConverter.GetBytes(second16).CopyTo(dataPack, 6);
            BitConverter.GetBytes(tirth16).CopyTo(dataPack, 8);

            uint forth32 = (uint)(this.WindRpm << 20) | (uint)(this.OutputA << 10) | (uint)(this.WindChargeA);
            BitConverter.GetBytes(forth32).CopyTo(dataPack, 10);

            uint fifth32 = (uint)(this.DayOrNight << 30) |
                (uint)(this.BatteryState << 27) |
                (uint)(this.BatteryCapacityPercent << 20) |
                (uint)(this.DumpLoadA << 10) |
                (uint)(this.SolarChargeA);

            BitConverter.GetBytes(fifth32).CopyTo(dataPack, 14);
            dataPack.CopyTo(output, 0);
            BitConverter.GetBytes(Answer.ComputeCrc(dataPack)).CopyTo(output, output.Length -2);

            return output;
        }
    }
}
