using FFSystem;
using FlexFlow;
using System;
using System.Data;

namespace TKFFTcpIpSDK
{
    public static class UDPMgr
    {
        public static string GetResponseACKByUDP(
            string customizedUDPName,
            Unit objUnit,
            Part objPart,
            ProductionOrder objPO,
            PackageCollection objPackages,
            XMLDetailBuilder objExtra)
        {
            string sResult = ProcessToExcuteUDP(customizedUDPName, objUnit, objPart, objPO, objPackages, objExtra);
            return sResult;
        }

        public static string GetHeartBeartCallStringByUDP(
            string customizedUDPName,
            Unit objUnit,
            Part objPart,
            ProductionOrder objPO,
            PackageCollection objPackages,
            XMLDetailBuilder objExtra)
        {
            string sResult = ProcessToExcuteUDP(customizedUDPName, objUnit, objPart, objPO, objPackages, objExtra);
            return sResult;
        }

        private static string ProcessToExcuteUDP(
            string customizedUDPName,
            Unit objUnit,
            Part objPart,
            ProductionOrder objPO,
            PackageCollection objPackages,
            XMLDetailBuilder objExtra)
        {
            string returnCallString = "";
            DataTable dt = null;
            int retVal = 0;
            FFParameterCollection InputParameters = new FFParameterCollection();
            try
            {
                if (objUnit != null)
                {
                    InputParameters.Add(new FFParameter("@xmlUnitSN", UtilityMgr.GetUnitXML(objUnit)));
                }
                if (objPart != null)
                {
                    InputParameters.Add(new FFParameter("@xmlPart", UtilityMgr.GetPartXML(objPart)));
                }
                if (objPO != null)
                {
                    InputParameters.Add(new FFParameter("@xmlProdOrder", UtilityMgr.GetProductionOrderXML(objPO)));
                }
                if (objPackages != null)
                {
                    InputParameters.Add(new FFParameter("@xmlPackages", UtilityMgr.GetPackagesXML(objPackages)));
                }
                InputParameters.Add(new FFParameter("@xmlStation", UtilityMgr.GetStationXML(StationMgr.CurrentStation())));
                InputParameters.Add(new FFParameter("@EmployeeID", EmployeeMgr.CurrentEmployee().ID.ToString()));

                if (objExtra != null)
                {
                    InputParameters.Add(new FFParameter("@xmlExtraData", objExtra.ToXMLString()));
                }

                DataSet ds = FFApplication.GetDBContext().ExecuteSPWithResults(customizedUDPName, InputParameters, ref retVal, false);

                if (retVal != 0)
                {
                    string errMsg = ConfigurationMgr.GetErrorFromCode(retVal);
                    throw (new Exception(errMsg));
                }
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        returnCallString = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch (FFException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return returnCallString;
        }
    }
}
