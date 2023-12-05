using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.GlueTracker
{
    public class GlueTrack_ProductStructure
    {
        public int ID { get; set; }
        public object GluePartNumber { get; set; }
        public object ProjectName { get; set; }
        public object DevicePartNumber { get; set; }
        public object IsActive { get; set; }
    }
}
