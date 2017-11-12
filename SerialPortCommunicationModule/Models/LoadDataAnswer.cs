using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class LoadDataAnswer : Answer
    {
        public int Output1Mode { get; set; }
        public int Output1Enable { get; set; }
        public int Output1TimeDelayOn { get; set; }
        public int Output1TimeDelayOff { get; set; }
        public int Output2Mode { get; set; }
        public int Output2Enable { get; set; }
        public int Output2TimeDelayOn { get; set; }
        public int Output2TimeDelayOff { get; set; }

        public LoadDataAnswer() : base(13, 7)
        {

        }

        public LoadDataAnswer(
            int out1mode, int out1enable, int out1delayOn, int out1delayOff,
            int out2mode, int out2enable, int out2delayOn, int out2delayOff
            ) : this()
        {
            this.Output1Mode = out1mode;
            this.Output1Enable = out1enable;
            this.Output1TimeDelayOn = out1delayOn;
            this.Output1TimeDelayOff = out1delayOff;
            this.Output2Mode = out1mode;
            this.Output2Enable = out1enable;
            this.Output2TimeDelayOn = out1delayOn;
            this.Output2TimeDelayOff = out1delayOff;
        }


        public override byte[] GetBytes()
        {
            var output = new byte[10];
            var dataPack = new byte[8];
            dataPack[0] = 165;
            dataPack[1] = (byte)this.Func;
            dataPack[2] = (byte)this.Address;
            dataPack[3] = (byte)this.Len;

            ushort first16 = (ushort)((this.Output1TimeDelayOff << 10) |
                (this.Output1TimeDelayOn << 5) |
                (this.Output1Enable << 4) |
                (this.Output1Mode));
                
            ushort second16 = (ushort)(
                (this.Output2TimeDelayOff << 10) |
                (this.Output2TimeDelayOn << 5) |
                (this.Output2Enable << 4) |
                (this.Output2Mode));

            BitConverter.GetBytes(first16).CopyTo(dataPack, 4);
            BitConverter.GetBytes(second16).CopyTo(dataPack, 6);

            dataPack.CopyTo(output, 0);

            BitConverter.GetBytes(ComputeCrc(dataPack)).CopyTo(output, output.Length - 2);
            return output;
        }

    }
}
