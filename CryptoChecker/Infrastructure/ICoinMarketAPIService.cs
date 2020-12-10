using CryptoChecker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Infrastructure
{
    public interface ICoinMarketAPIService
    {
        Task<QuoteWrapper> GetQuoteForValuedCryptoCurrency(string inputValue);
        Task<ExchangeRate> PerformConversionOnQuotes(string baseCurrency);

        Task<List<ConvertedCurrencyRate>> GetExchangeRatesForCurrency(string cryptoCode);
    }
}
