using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Model
{
    public class Quote
    {
        public Currency Currency { get; set; }
      
    }


    public class Currency
    {
        public double price { get; set; }
        public double volume_24h { get; set; }
        public double percent_change_1h { get; set; }
        public double percent_change_24h { get; set; }
        public double percent_change_7d { get; set; }
        public double market_cap { get; set; }
        public DateTime last_updated { get; set; }
    }
}
