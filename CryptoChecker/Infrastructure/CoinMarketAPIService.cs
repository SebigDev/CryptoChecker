using CryptoChecker.Model;
using CryptoChecker.Utility;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoChecker.Infrastructure
{
    public class CoinMarketApiService : ICoinMarketApiService 
    {
        private readonly HttpClient _httpClient;
        private readonly SecureSettings _secureSettings;
        private const string BaseCurrency = "USD";

        public CoinMarketApiService(HttpClient httpClient, IOptions<SecureSettings> secureSettings)
        {
            _httpClient = httpClient;
            _secureSettings = secureSettings.Value;
        }

        public async Task<List<ConvertedCurrencyRate>> GetExchangeRatesForCurrency(string cryptoCode)
        {
            var currencyRates = new List<ConvertedCurrencyRate>(); 

            if (string.IsNullOrEmpty(cryptoCode)) { throw new ArgumentNullException(nameof(cryptoCode)); }

            var currencies = _secureSettings.CurrenciesToDisplay.Split(",").ToList();

            var quote = await GetQuoteForValuedCryptoCurrency(cryptoCode);
            if (quote == null) return currencyRates;
            var rateResponse = await this.PerformConversionOnQuotes(BaseCurrency);
            if (rateResponse == null || rateResponse.Rates == null) return currencyRates;
            currencyRates.AddRange(from rate in rateResponse.Rates
                where currencies.Contains(rate.Key)
                let convertedRate = CurrencyValueConverter.ResolveExchangeRate(rate.Value, quote.Usd.Price)
                let currentRate = new ConvertedCurrencyRate(rate.Key, convertedRate)
                select currentRate);
            return currencyRates;
        }

        public async Task<QuoteWrapper> GetQuoteForValuedCryptoCurrency(string inputValue)
        {
            //Configure Headers with API Keys and Accepts
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _secureSettings.CoinMarketAPIKey);
            _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
            _httpClient.BaseAddress = new Uri(_secureSettings.CoinMarketAPIBaseUrl);
            var url = $"{_secureSettings.CoinMarketAPIQuotes}?symbol={inputValue}";
           
            try
            {
                var httpResponse = await _httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)//if status code is 200
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    var result = JObject.Parse(response);
                    var objResult = ObjectResult(inputValue, result);

                    var res = JsonConvert.DeserializeObject<QuoteWrapper>(objResult);
                    return res;
                }

                //log the failure to console
                var responseFailed  = await httpResponse.Content.ReadAsStringAsync();
                Console.WriteLine("Failed Response=>{0}", responseFailed);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private static string ObjectResult(string inputValue, JObject result)
        {
            //using JObject to pick currency in a nested JSON
            return result["data"][inputValue]["quote"].ToString().Replace("\r\n", "");
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
