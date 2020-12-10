using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Utility
{
    public class CurrencyValueConverter
    {
        public static double ResolveExchangerate(double rate, double btcRate)
        {
            var resolvedRate = (rate) * (btcRate);
            return resolvedRate;
        }
    }
}
