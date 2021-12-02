using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class OwnerModel
    {
        public string user_id { get; set; }
        public string preferred_name { get; set; }
        public string preferred_first_name { get; set; }

    }
}