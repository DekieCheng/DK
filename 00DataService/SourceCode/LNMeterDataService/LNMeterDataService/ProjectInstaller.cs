using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace LNMeterDataService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            SetNames();
        }

        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);
            SetNames();
        }

        private void SetNames()
        {
            this.serviceInstaller1.DisplayName = AddSuffix(this.serviceInstaller1.DisplayName);
            this.serviceInstaller1.ServiceName = AddSuffix(this.serviceInstaller1.ServiceName);
        }

        private string AddSuffix(string originalName)
        {
            if (!String.IsNullOrWhiteSpace(this.Context.Parameters["ServiceSuffix"]))
                return originalName + " - " + this.Context.Parameters["ServiceSuffix"];
            else
                return originalName;
        }
    }
}
