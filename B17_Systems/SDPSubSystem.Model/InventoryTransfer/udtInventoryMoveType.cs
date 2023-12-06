using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.InventoryTransfer
{
    public class udtInventoryMoveType
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public int Source_WH_Code { get; set; }
        public int Dest_WH_Code { get; set; }
        public int HostName { get; set; }
        public int IN_Folder { get; set; }
        public int OUT_Folder { get; set; }
        public int FTP_UserName { get; set; }
        public int FTP_PWD { get; set; }
    }
}
