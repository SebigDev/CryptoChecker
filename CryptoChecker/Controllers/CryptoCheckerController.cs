using CryptoChecker.Infrastructure;
using CryptoChecker.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
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
            var responseModel = new ResponseModel();
            var _callResponse  = await _coinMarketAPIService.GetExchangeRatesForCurrency(inputvalues);
            if (!_callResponse.Any())
            {
                responseModel = new ResponseModel
                {
                    Message = $"No response returned for the {inputvalues} entered. ",
                    Data = _callResponse,
                    Status = false,
                };
                return BadRequest(responseModel);
            }
             responseModel = new ResponseModel
            {
                Message = "Successfull",
                Data = _callResponse,
                Status = true,
            };
            return Ok(responseModel);
        }


    }
}
