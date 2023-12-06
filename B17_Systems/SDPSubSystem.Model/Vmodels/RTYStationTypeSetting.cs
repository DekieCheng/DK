using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.Vmodels
{
    public class RTYStationTypeSetting
    {
        public int ID { get; set; }
        public int FFID { get; set; }
        public int PartfamilyID { get; set; }
        public int StationTypeID { get; set; }
        public int StationType { get; set; }
        public int StationTypeAlias { get; set; }
        public Double Target { get; set; }
        public int IsRTY { get; set; }
        public int IsOutput { get; set; }
        public int Sort { get; set; }
        public int Active { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Partfamily { get; set; }
        public int PartfamilyAlias { get; set; }
        public int PartfamilySort { get; set; }
        public int IsCustomerStation { get; set; }
        public  int CustomerMatrixID { get; set; }
        public int IsobaDPPM { get; set; }
    }
}
