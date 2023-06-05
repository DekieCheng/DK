using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlexFlow;

namespace TKFFTcpIpSDK.TaskSettings
{
    public class FFTcpIpHandlerSettingMgr
    {
        public FFTcpIpHandlerSettingMgr(Task CurrTask)
        {
            try
            {
                ReadTaskSettings(CurrTask);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool ReadTaskSettings(Task CurrTask)
        {
            bool flag = false;

            try
            {
                bool.TryParse(ConfigurationMgr.GetStationConfigSetting("TCPControl"), out m_TCPControl);
                int.TryParse(ConfigurationMgr.GetStationConfigSetting("ServerPort"), out m_ServerPort);
                IPAddress.TryParse(ConfigurationMgr.GetStationConfigSetting("OutgoingIP"), out m_OutgoingIpAddress);
                int.TryParse(ConfigurationMgr.GetStationConfigSetting("OutgoingPort"), out m_OutgoingPort);
                int.TryParse(ConfigurationMgr.GetStationConfigSetting("HeartbeatIntervalTime"), out m_HeartbeatIntervalTime);
                bool.TryParse(ConfigurationMgr.GetStationConfigSetting("WriteDataLocalFile"), out m_WriteDataLocalFile);
                m_OutgoingParameter = ConfigurationMgr.GetStationConfigSetting("OutParameter");
                m_UDPToGetHeartBeatCall = ConfigurationMgr.GetStationConfigSetting("UDPToGetHeartBeatCall");
                m_UDPToGetResponseValue = ConfigurationMgr.GetStationConfigSetting("UDPToGetResponseValue");
                bool.TryParse(CurrTask.GetSetting("AsyncTcpIp"), out m_AsyncTcpIp);
                flag = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        private bool m_TCPControl;
        public bool TCPControl
        {
            get { return m_TCPControl; }
        }

        private int m_ServerPort;
        public int FFServerPort
        {
            get
            {
                return m_ServerPort;
            }
        }

        private IPAddress m_OutgoingIpAddress;
        public IPAddress OutgoingIpAddress
        {
            get
            {
                return m_OutgoingIpAddress;
            }
        }

        private int m_OutgoingPort;
        public int OutgoingPort
        {
            get
            {
                return m_OutgoingPort;
            }
        }

        private int m_HeartbeatIntervalTime;
        public int HeartbeatIntervalTime
        {
            get
            {
                return m_HeartbeatIntervalTime;
            }
        }

        private bool m_WriteDataLocalFile;
        public bool WriteDataLocalFile
        {
            get
            {
                return m_WriteDataLocalFile;
            }
        }
        private string m_OutgoingParameter;
        public string OutgoingParameter
        {
            get
            {
                return m_OutgoingParameter;
            }
        }
        private string m_UDPToGetHeartBeatCall;
        public string UDPToGetHeartBeatCallValues
        {
            get
            {
                return m_UDPToGetHeartBeatCall;
            }
        }
        private string m_UDPToGetResponseValue;
        public string UDPToGetResponseValue
        {
            get
            {
                return m_UDPToGetResponseValue;
            }
        }

        private bool m_AsyncTcpIp = false;
        public bool AsyncTcpIp
        {
            get
            {
                return m_AsyncTcpIp;
            }
        }
    }
}
