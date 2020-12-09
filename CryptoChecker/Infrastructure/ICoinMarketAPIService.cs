using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Infrastructure
{
    public interface ICoinMarketAPIService
    {
        Task<string> GetQuoteForValuedCryptoCurrency(object inputValue);
        Task<string> PerformConversionOnQuotes(object quoteValue);
    }
}
