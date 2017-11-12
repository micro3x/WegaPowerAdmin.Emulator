using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public interface IAnswer
    {
        int Head { get; }
        int Func { get; set; }
        int Address { get; set; }
        int Len { get; set; }

        byte[] GetBytes();
    }
}
