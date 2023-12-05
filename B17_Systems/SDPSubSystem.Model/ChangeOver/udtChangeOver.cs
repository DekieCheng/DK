using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.ChangeOver
{
    public  class udtChangeOver
    {
        public Int32 ID { get; set; }
        public string Project { get; set; }
        public Int32 LineID { get; set; }
        public Int32 AmrNumberID { get; set; }
        public Int32 ActionID { get; set; }
        public Int32 EmployeeID { get; set; }
        public DateTime? PlanTime { get; set; }
        public DateTime ProductTime { get; set; }
        public DateTime CreationTime { get; set; }
        public Int32 StatusID { get; set; }
    }
}
