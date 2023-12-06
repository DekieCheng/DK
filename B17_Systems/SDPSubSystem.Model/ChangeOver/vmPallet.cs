using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.ChangeOver
{
    public class vmPallet
    {
        public int ID { get; set; }
        public string Pallet { get; set; }
        public string PartNumber { get; set; }
        public int CartonQty { get; set; }
        public int UnitQty { get; set; }
        public string ProductionOrder { get; set; }
        public string SubmitPerson { get; set; }
        public string RecievePerson { get; set; }
        public string TransferPerson { get; set; }
        public string EgateNum { get; set; }
    }
}
