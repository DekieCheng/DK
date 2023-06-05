using LNMeterInfrastructure.BLL;
using LNMeterInfrastructure.Common;
using Model;
using ServiceMgr;
using System;
using System.ServiceProcess;
using System.Timers;

namespace LNMeterDataService
{
    public partial class LNMeterDataService : ServiceBase
    {
        #region Variables in class level
        private Timer _Cycletimer;
        private LNMeterCategory _lMeterCategory = null;
        #endregion

        public LNMeterDataService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                LNLogMgr.WriteLogAsy("Service is starting.");
                _lMeterCategory = new LNMeterCategory(LNGlobal.g_ServiceFilter);
                _Cycletimer = new Timer(_lMeterCategory.CycleTime * 1000);
                _Cycletimer.Elapsed += new ElapsedEventHandler(ProcessToTimerElapsed);
                _Cycletimer.Start();
                LNLogMgr.WriteLogAsy("Service is started successfully.");
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }

        private void ProcessToTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                LNLogMgr.WriteLogAsy(string.Format("Service is running for the filter {0}.", LNGlobal.g_ServiceFilter));
                LNSvrMgr.FireCollectDataAction(_lMeterCategory);
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }

        protected override void OnStop()
        {
            try
            {
                LNLogMgr.WriteLogAsy("Stopping the service.");
                _Cycletimer.Stop();
                _Cycletimer.Dispose();
                LNLogMgr.WriteLogAsy("Service is stopped successfully.");
            }
            catch (Exception ex)
            {
                LNLogMgr.WriteLogAsy(ex);
            }
        }
    }
}
