using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.AssistModels
{
    public class Language
    {
        /// <summary>
        /// 语言编号
        /// </summary>
        public int LangID { get; set; }
        /// <summary>
        /// 语言显示的名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 语言详细的文件
        /// </summary>
        public string FileName { get; set; }
    }
}
