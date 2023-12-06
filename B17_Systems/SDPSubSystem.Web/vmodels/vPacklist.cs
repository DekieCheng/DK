using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Web.vmodels
{
    public class vPacklist
    {
        public int ID { get; set; }
        public string packlistid { get; set; }
        public string PackListDetailID { get; set; }
        public string ShiptoName { get; set; }
        public string ShiptoAddress { get; set; }
        public string ShipAttn { get; set; }
        public string ShipTel { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        public string ConsigneeAttn { get; set; }
        public string ConsigneeTel { get; set; }
        public string PLNO { get; set; }
        public string ProjectName { get; set; }
        public string ShippedDate { get; set; }
        public string Incoterms { get; set; }
        public string Countryoforigin { get; set; }
        public string Destination { get; set; }
        public string Forwarder { get; set; }
        public string ShippingMode { get; set; }
        public string Pallettype { get; set; }
        public string ContainerNumber { get; set; }
        public string TruckNumber { get; set; }

    }
}