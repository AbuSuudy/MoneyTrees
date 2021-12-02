using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MoneyTrees.Models
{
    public class PaymentDetailsModel
    {
        [JsonProperty("locale_uk")]
        public LocaleUkModel LocaleUk { get; set; }
    }
}