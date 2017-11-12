using System;
using System.Collections.Generic;
using System.Text;

namespace TranscodeDataReceived
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new byte[20] { 165, 4, 10, 17, 240, 1, 0, 0, 138, 0, 0, 0, 96, 0, 0, 0, 192, 133, 27, 219 };

            var a = 6;
            var bytes = BitConverter.GetBytes(a << 4);
            //bytes[0] = (byte)(bytes[0] << 4);


        }
    }
}
