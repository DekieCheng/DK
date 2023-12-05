using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SDPSubSystem.Web.Models
{
    public class ModelTree
    {
        // GET: ModelTree
        public string text { set; get; }
        public List<ModelTree> nodes { set; get; }
    }
}