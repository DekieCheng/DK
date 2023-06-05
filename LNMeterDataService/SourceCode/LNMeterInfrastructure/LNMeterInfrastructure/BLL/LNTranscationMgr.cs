using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.SqlServer;
using System.ComponentModel;
using LNMeterInfrastructure.DAL;
using Model;

namespace LNMeterInfrastructure.BLL
{
    public class LNTranscationMgr : IDisposable
    {
        private LNTranscationDAL objLNTranscationDAL;
        private bool disposedValue;

        public LNTranscationMgr()
        {
            objLNTranscationDAL = new LNTranscationDAL();
        }

        public bool ImportDataToDB(LNRuntimeArguments lNRuntimeArguments)
        {
            return objLNTranscationDAL.ImportDataToDB(lNRuntimeArguments);
        }

        public bool SaveRuntimeErrLog(LNRuntimeArguments lNRuntimeArguments)
        {
            return objLNTranscationDAL.SaveRuntimeErrLog(lNRuntimeArguments);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~LNTranscationMgr()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
