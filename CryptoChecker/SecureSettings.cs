using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker
{
    public class SecureSettings
    {
        public string CoinMarketAPIKey { get; set; }
        public string CoinMarketAPIBaseUrl { get; set; }
        public string CoinMarketAPIQuotes { get; set; }

        public string CoinMarketConversionEndpoint => CoinMarketAPIBaseUrl + CoinMarketAPIQuotes;
    }
}
