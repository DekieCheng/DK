using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.ESDAudit
{
    public class udtEsdSpec
    {
        public int ID { get; set; }
        public string Specification { get; set; }
        public float Min1 { get; set; }
        public float Max1 { get; set; }
        public string Unit1 { get; set; }
        public float Min2 { get; set; }
        public float Max2 { get; set; }
        public string Unit2 { get; set; }
    }
}
