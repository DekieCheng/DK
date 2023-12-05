using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model
{
    public class BaseEntity
    {
        /// <summary>
        /// ID编号
        /// </summary>
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? CreationTime { get; set; }

        [StringLength(50)]
        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public int StatusID { get; set; }
    }
}
