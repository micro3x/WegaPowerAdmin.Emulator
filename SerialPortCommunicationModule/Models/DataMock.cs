using System;
using System.Collections.Generic;
using System.Text;
using SerialPortCommunicationModule.Models;

namespace SerialPortCommunicationModule
{
    public class DataMock
    {
        public StateAnswer CurrentState { get; set; } // OK
        public WindDataAnswer WindSettings { get; set; } // OK
        public SolarDataAnswer SolarSettings { get; set; } // OK
        public LoadDataAnswer LoadSettings { get; set; } // OK
        public BatteryDataAnswer BatterySettings { get; set; } // OK

        public DataMock()
        {
            this.CurrentState = new StateAnswer(batCapacity: 98, batV: 498, rpm: 12, windV: 188, out1: 2, windA: 17);

            this.WindSettings = new WindDataAnswer(1,1,1,1,1,1,1,1,1);

            this.SolarSettings = new SolarDataAnswer(1,1,1);

        }
    }
}
