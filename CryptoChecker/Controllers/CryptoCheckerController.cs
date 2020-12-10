using CryptoChecker.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CryptoChecker.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CryptoCheckerController : ControllerBase 
    {
        private readonly ICoinMarketAPIService _coinMarketAPIService;
        public CryptoCheckerController(ICoinMarketAPIService coinMarketAPIService)
        {
            _coinMarketAPIService = coinMarketAPIService ?? throw new ArgumentNullException(nameof(coinMarketAPIService));
        }

        [HttpGet]
        [Route("{inputvalues}")]
        public async Task<IActionResult> GetCryptoCurrencyQuotes(string inputvalues)
        {
            if (string.IsNullOrEmpty(inputvalues))
            {
                return BadRequest("Input value cannot be null or empty.");
            }
           
            var _callResponse  = await _coinMarketAPIService.GetExchangeRatesForCurrency(inputvalues);
            if (_callResponse == null)
            {
                return BadRequest($"No response returned for the {inputvalues} entered. ");
            }
            return Ok(_callResponse);
        }


    }
}
