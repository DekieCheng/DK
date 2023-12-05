using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.ChangeOver
{
    public class udtWHLocationLimitation
    {
        public int ID { get; set; }
        public string WarehouseCode { get; set; }
        public int CustomerID { get; set; }
        public int PartFamilyID { get; set; }
        public string DateTimeFrame { get; set; }
        public int LimitationQTY { get; set; }
        public int TargetPercentage { get; set; }
        public int MaxAgaingDay { get; set; }

        public DateTime TimeStamp { get; set; }
        public string Reserved_01 { get; set; }
        public string Reserved_02 { get; set; }
        public string Reserved_03 { get; set; }
        public string Reserved_04 { get; set; }
        public string Reserved_05 { get; set; }
    }
}
