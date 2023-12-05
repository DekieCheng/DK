using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.GlueTracker
{
    public class GlueTrack_GlueMain
    {
        public int ID { get; set; }
        public object GluePartNumber { get; set; }
        public object PartDescription { get; set; }
        public object BaanUsage { get; set; }
        public object ProjectName { get; set; }
        public object StationTypeName { get; set; }
        public object CurrentUsage { get; set; }
    }
}
