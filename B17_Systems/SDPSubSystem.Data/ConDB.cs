using SDPSubSystem.Common.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Data
{
    public class ConDB
    {
        /// <summary>
        /// 中央数据库
        /// </summary>
        public static string SYSDBContext
        {
            get
            {
                //return Encry.Encry.Decryptosdp(ConfigurationManager.ConnectionStrings["SYSDbContext"].ConnectionString);
                string conn = SDPSubSystem.Common.Security.GetConnUtil.GetConn(System.Configuration.ConfigurationManager.AppSettings["SDPURL"]);
                return conn;
            }
        }
        /// <summary>
        /// 业务数据库
        /// </summary>
        public static string BUSDBContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
            }
        }

        /// <summary>
        /// 业务数据库
        /// </summary>
        public static string LifeSpanContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["LifeSpan"].ConnectionString);
            }
        }


        /// <summary>
        /// 业务数据库
        /// </summary>
        public static string DWHDBContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["B17DWH"].ConnectionString);
            }
        }

        /// <summary>
        /// 园区Desktop数据库
        /// </summary>
        public static string ITDMDBContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["ITDM"].ConnectionString);
            }
        }

        /// <summary>
        /// 资产管理系统数据库
        /// </summary>
        public static string AMSDBContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["AMS"].ConnectionString);
            }
        }

        /// <summary>
        /// B17ActionChecker业务数据库
        /// </summary>
        public static string ActionCheckerDBContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["B17ActionChecker"].ConnectionString);
            }
        }


        /// <summary>
        /// PlataReport业务数据库
        /// </summary>
        public static string PlataReportContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["PlataReportContext"].ConnectionString);
            }
        }

        /// <summary>
        /// PlataTest业务数据库
        /// </summary>
        public static string PlataTestContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["PlataTestContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Plata业务数据库
        /// </summary>
        public static string PlataContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["PlataContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Intel业务数据库
        /// </summary>
        public static string IntelContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["IntelContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Dyson业务数据库
        /// </summary>
        public static string DysonContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["DysonContext"].ConnectionString);
            }
        }

        /// <summary>
        /// GHD业务数据库
        /// </summary>
        public static string GHDContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["GHDContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Nuskin业务数据库
        /// </summary>
        public static string NuskinContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["NuskinContext"].ConnectionString);
            }
        }

        /// <summary>
        /// Rebel业务数据库
        /// </summary>
        public static string RebelContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["RebelContext"].ConnectionString);
            }
        }

        /// <summary>
        ///  Paperless 业务数据库
        /// </summary>
        public static string PPAppContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["PPApp"].ConnectionString);
            }
        }

        /// <summary>
        /// B17Dashboard业务数据库
        /// </summary>
        public static string DashboardContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["B17Dashboard"].ConnectionString);
            }
        }

        /// <summary>
        ///  eCard 业务数据库
        /// </summary>
        public static string DBKQXTContext
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["eCard"].ConnectionString);
            }
        }

        /// <summary>
        ///  as3 baan 业务数据库
        /// </summary>
        public static string AS3Context
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["AS3Context"].ConnectionString);
            }
        }

        /// <summary>
        ///  am3 baan 业务数据库
        /// </summary>
        public static string AM3Context
        {
            get
            {
                return DESSecurity.Decrypt(ConfigurationManager.ConnectionStrings["AM3Context"].ConnectionString);
            }
        }

    }
}
