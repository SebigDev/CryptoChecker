using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Model
{
    public partial class ExchangeRate
    {
        [JsonProperty("rates")]
        public Dictionary<string, double> Rates { get; set; }
    }
}
