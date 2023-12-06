using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public class LNRuntimeArguments : EventArgs
    {
        public LNRuntimeArguments() { }

        public LNMeterCategory lnMeterCategory { get; set; }
        public LNSerialPortServer lnSerialPortServer { get; set; }
        public LNElectricityMeter lnElectricityMeter { get; set; }
        public LNWaterMeter lnWaterMeter { get; set; }
        public LNCompressedAirFlowMeter lnCompressedAirFlowMeter { get; set; }
        public LNChilledWaterFlowMeter lnChilledWaterFlowMeter { get; set; }
        public Exception RuntimeException { get; set; }
        public string RuntimeErrMsg { get; set; }
        public string ReceivedHexData { get; set; }
        public byte[] RecData { get; set; }
        public Hashtable ReceivedData { get; set; }
    }
}
