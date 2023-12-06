using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDPSubSystem.Model.AssistModels
{
    public class SqlWhere
    {
        public string Field { get; set; }

        public string Symbol { get; set; }


        private string _value;
        public string Value
        {
            set { _value = value; }
            get {
                return _value.Replace("'", "''");
            }
        }
    }
}
