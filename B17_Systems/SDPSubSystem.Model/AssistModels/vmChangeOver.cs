using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.AssistModels
{
    public class vmChangeOver
    {
        public string Project { get; set;}
        public string LineName { get; set; }
        public string AmrNumber { get; set; }
        public string Action { get; set; }
        public string Employee { get; set; }
        public DateTime PlanTime { get; set; }
        public DateTime ProductTime { get; set; }
        public DateTime CreationTime { get; set; }
        public int StatusID { get; set; }

    }
}
