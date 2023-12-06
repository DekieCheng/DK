using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SDPSubSystem.Web.Models
{
    public class DataTablesResultInfo
    {
        public DataTablesResultInfo() { }

        public DataTablesResultInfo(object data, int draw, int recordsTotal, int recordsFiltered)
        {
            this.draw = draw;
            this.data = data;
            this.recordsTotal = recordsTotal;
            this.recordsFiltered = recordsFiltered;
        }

        public DataTablesResultInfo(object data)
        {
            this.data = data;
        }
        public DataTablesResultInfo(object data,int times)
        {
            this.data = data;
            this.flushTimes = times;
        }

        public int draw { get; set; }

        /// <summary>
        /// 过滤前记录总数
        /// </summary>
        public int recordsTotal { get; set; }

        /// <summary>
        /// 过滤后记录数
        /// </summary>
        public int recordsFiltered { get; set; }

        public object data { get; set; }
        public int flushTimes { get; set; }
    }
}