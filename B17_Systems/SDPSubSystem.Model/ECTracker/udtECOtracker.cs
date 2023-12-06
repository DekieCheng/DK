using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.ECTracker
{
    public class udtECOtracker
    {
        public int ID { get; set; }
        public object GNECO { get; set; }
        public object FlexECO { get; set; }
        public object ECType { get; set; }
        public object ChangeType { get; set; }
        public object ECReceivedDate { get; set; }
        public object CustomerProject { get; set; }
        public object ECDescription { get; set; }
        public object EstimateImplementationDate { get; set; }
        public object ActualImplementationDate { get; set; }
        public object Status { get; set; }
        public object Owner { get; set; }
    }
}
