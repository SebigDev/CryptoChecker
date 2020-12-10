using CryptoChecker.Model;
using CryptoChecker.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoChecker.Infrastructure
{
    public class CoinMarketAPIService : ICoinMarketAPIService
    {
        private readonly HttpClient _httpClient;
        private readonly SecureSettings _secureSettings;
        private const string BaseCurrency = "USD";

        public CoinMarketAPIService(HttpClient httpClient, IOptions<SecureSettings> secureSettings)
        {
            _httpClient = httpClient;
            _secureSettings = secureSettings.Value;
        }

        public async Task<List<ConvertedCurrencyRate>> GetExchangeRatesForCurrency(string cryptoCode)
        {
            List<ConvertedCurrencyRate> currencyRates = new List<ConvertedCurrencyRate>(); 

            if (string.IsNullOrEmpty(cryptoCode)) { throw new ArgumentException(nameof(cryptoCode)); };

            var currencies = _secureSettings.CurrenciesToDisplay.Split(",").ToList();

            var quote = await GetQuoteForValuedCryptoCurrency(cryptoCode);
            if(quote  != null)
            {
                var rateResponse = await this.PerformConversionOnQuotes(BaseCurrency);
                if(rateResponse != null)
                {
                    foreach(var rate in rateResponse.Rates)
                    {
                        if (currencies.Contains(rate.Key))
                        {
                            var convertedRate = CurrencyValueConverter.ResolveExchangerate(rate.Value, quote.Usd.Price);
                            var currRate = new ConvertedCurrencyRate { CurrencyName = rate.Key, ExchangeValue= convertedRate };
                            currencyRates.Add(currRate);
                        }
                    }
                    return currencyRates;
                }
                return currencyRates;
            }
            return currencyRates;
        }

        public async Task<Quote> GetQuoteForValuedCryptoCurrency(string inputValue)
        {
            //Configure Headers with API Keys and Accepts
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _secureSettings.CoinMarketAPIKey);
            _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
            _httpClient.BaseAddress = new Uri(_secureSettings.CoinMarketAPIBaseUrl);
            string url = $"{_secureSettings.CoinMarketAPIQuotes}?symbol={inputValue}";
           
            try
            {
                var httpResponse = await _httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)//if status code is 200
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<QuoteWrapper>(response);
                    return res.Data.Btc.Quote;
      
                }
                var responseFailed  = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Failed Response=>{0}", responseFailed);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.Message);
                return null;
            }
           
        }

        public async Task<ExchangeRate> PerformConversionOnQuotes(string baseCurrency)
        {
            var url = $"{_secureSettings.ExchangeRateService}?base={baseCurrency}";
            var result = await _httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExchangeRate>(response);
            }
            var responseFailed = await result.Content.ReadAsStringAsync();
            Console.WriteLine("Failed Response=>{0}", responseFailed);
            return null;
        }
    }
}
