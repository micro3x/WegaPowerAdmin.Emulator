using System;
using System.Collections.Generic;
using System.Text;

namespace SerialPortCommunicationModule.Models
{
    public class Request
    {

        public int Func { get; set; }

        public int Address { get; set; }

        public Request(int func, int address)
        {
            this.Address = address;
            this.Func = func;
        }

        //todo: maybe create func for response of this request
        
    }
}
