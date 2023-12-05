using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.AssistModels
{
    public class Users
    {
        public int ID { get; set; }
        public string Building { get; set; }
        public string SiteCode { get; set; }
        public string EmployeeNO { get; set; }
        public string Account { get; set; }
        public string CName { get; set; }
        public string EName { get; set; }
        public int LangID { get; set; }
        public string SysCode { get; set; }
        public string MailAddress { get; set; }
        public string ReportTo { get; set; }
    }
}
