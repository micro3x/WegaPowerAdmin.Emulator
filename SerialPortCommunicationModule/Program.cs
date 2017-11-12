using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Windows.Forms;
using System.IO;

namespace SerialPortCommunicationModule
{
    class SerialPortProgram
    {
        private SerialPort port = new SerialPort("COM9", 19200, Parity.None);
        //private List<byte[]> packets = new List<byte[]>();
        //private int counter = 0;
        //private int maxCounter = 0;


        [STAThread]
        static void Main(string[] args)
        {
            new SerialPortProgram();

        }

        private SerialPortProgram()
        {
            //Console.WriteLine("Incoming Data:");

            //var datafile = new StreamReader("Export.csv");

            //using (datafile)
            //{
            //    var line = datafile.ReadLine();
            //    while (!string.IsNullOrEmpty(line))
            //    {
            //        var integers = ParseReadings(line);
            //        packets.Add(ConvertForTrans(integers));
            //        line = datafile.ReadLine();
            //    }
            //}

            //maxCounter = packets.Count;

            // Attach a method to be called when there
            // is data waiting in the port's buffer
            port.DataReceived += new
                  SerialDataReceivedEventHandler(port_DataReceived);

            // Begin communications
            port.Open();

            //var a = Encoding.UTF8.GetBytes(port.ReadExisting());
            //Console.WriteLine(string.Join(",", receivedConvert(a)));

            //var data = Encoding.UTF8.GetBytes("\\??\\ACPI#PNP0501#1#{0100fdd7-be5a-4808-91f5-05002bc60f72}");
            //Console.WriteLine(string.Join(",", receivedConvert(data)));
            //port.Write(data, 0, data.Length);


            Application.Run();
        }

        //private byte[] ConvertForTrans(int[] data)
        //{
        //    var output = new byte[20];
        //    output[0] = 165;
        //    output[1] = 4;
        //    output[2] = 10;
        //    output[3] = 17;
        //    var vbat = BitConverter.GetBytes(data[0]);
        //    output[4] = vbat[0];
        //    output[5] = vbat[1];
        //    var vwind = BitConverter.GetBytes(data[3]);
        //    output[8] = vwind[0];
        //    output[9] = vwind[1];
        //    var rpm = BitConverter.GetBytes(data[5] << 4);
        //    output[12] = rpm[0];
        //    output[13] = rpm[1];
        //    return output;
        //}

        //private int[] ParseReadings(string line)
        //{
        //    var values = line.Split(';');
        //    var output = new int[7];
        //    {
        //        output[i] = int.Parse(values[i]);
        //    }
        //    return output;
        //}

        private void port_DataReceived(object sender,
      SerialDataReceivedEventArgs e)
        {
            // Show all the incoming data in the port's buffer
            var head = port.ReadByte();
            while (head != 165)
            {
                head = port.ReadByte();
            }

            var func = port.ReadByte();
            var address = port.ReadByte();
            var len = port.ReadByte();
            var dataBytes = new byte[len + 3];

            dataBytes[0] = (byte)head;
            dataBytes[1] = (byte)func;
            dataBytes[2] = (byte)address;
            dataBytes[3] = (byte)len;

            var readdata = port.Read(dataBytes, 4, len - 1);

            //var test = port.ReadExisting();
            //Console.WriteLine(test);
            var req = Helpers.ParseRequestData(dataBytes);

            var response = Helpers.GetAnswer(reqM;my

            var blah = response.GetBytes();

            port.Write(blah, 0, blah.Length);

            //Console.WriteLine(string.Join(",", receivedConvert(dataBytes)));

            //if (dataBytes[0] == 165 &&
            //    dataBytes[1] == 3 &&
            //    dataBytes[2] == 10 &&
            //    dataBytes[3] == 3 &&
            //    dataBytes[4] == 148 &&
            //    dataBytes[5] == 73)
            //{
            //    port.Write(new byte[20] { 165, 4, 10, 17, 242, 1, 0, 0, 31, 1, 0, 0, 32, 1, 0, 0, 192, 133, 48, 228 }, 0, 20);
            //    Console.WriteLine("Data Sent");
            //    return;
            //}

            //Console.WriteLine("fragmented data");
            //port.Write(packets[counter], 0, packets[counter].Length);
            //counter++;
            //if(counter == maxCounter - 1) { counter = 0; }

        }

        //private string[] receivedConvert(byte[] dataBytes)
        //{
        //    var dataStrings = new string[dataBytes.Length];

        //    for (int i = 0; i < dataBytes.Length; i++)
        //    {
        //        dataStrings[i] = dataBytes[i].ToString("X2");
        //    }
        //    return dataStrings;
        //}

    }
}
