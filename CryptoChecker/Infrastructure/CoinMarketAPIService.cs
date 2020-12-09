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
        public CoinMarketAPIService(HttpClient httpClient, IOptions<SecureSettings> secureSettings)
        {
            _httpClient = httpClient;
            _secureSettings = secureSettings.Value;
        }
        public async Task<string> GetQuoteForValuedCryptoCurrency(object inputValue)
        {
            //Configure Headers with API Keys and Accepts
            _httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", _secureSettings.CoinMarketAPIKey);
            _httpClient.DefaultRequestHeaders.Add("Accepts", "application/json");
            _httpClient.BaseAddress = new Uri(_secureSettings.CoinMarketAPIBaseUrl);
            string url = $"{_secureSettings.CoinMarketAPIQuotes}?id={inputValue}";
           
            try
            {
                var httpResponse = await _httpClient.GetAsync(url);

                if (httpResponse.IsSuccessStatusCode)//if status code is 200
                {
                    var response = await httpResponse.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<dynamic>(response);
                    return res;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex?.Message);
                return null;
            }
           
        }

        public Task<string> PerformConversionOnQuotes(object quoteValue)
        {
            throw new NotImplementedException();
        }
    }
}
