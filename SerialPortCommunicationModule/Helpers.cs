using System;
using System.Collections.Generic;
using System.Text;
using SerialPortCommunicationModule.Models;

namespace SerialPortCommunicationModule
{
    public static class Helpers
    {
        private static DataMock Data = new DataMock();

        public static Request ParseRequestData(byte[] data )
        {
            return new Request(data[1], data[2]);
        }

        public static IAnswer GetAnswer(Request req)
        {
            switch (req.Address)
            {
                case 10:
                    return Data.CurrentState;
                case 11:
                    return Data.WindSettings;
                case 12:
                    return Data.SolarSettings;
                case 13:
                    return new LoadDataAnswer();
                case 14:
                    return new BatteryDataAnswer();
                default:
                    throw new ArgumentException("Invalid Request Received");
            }
        }
    }
}
