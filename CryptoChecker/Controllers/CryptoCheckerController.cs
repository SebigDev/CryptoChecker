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
        private readonly ICoinMarketApiService _coinMarketApiService; 
        public CryptoCheckerController(ICoinMarketApiService coinMarketApiService)
        {
            _coinMarketApiService = coinMarketApiService ?? throw new ArgumentNullException(nameof(coinMarketApiService));
        }

        [HttpGet]
        [Route("{inputValues}")]
        public async Task<IActionResult> GetCryptoCurrencyQuotes(string inputValues)
        {
            if (string.IsNullOrEmpty(inputValues))
            {
                return BadRequest("Input value cannot be null or empty.");
            }
            ResponseModel responseModel;
            var callResponse  = await _coinMarketApiService.GetExchangeRatesForCurrency(inputValues);
            if (!callResponse.Any())
            {
                responseModel = new ResponseModel
                {
                    Message = $"No response returned for the {inputValues} entered. ",
                    Data = callResponse,
                    Status = false,
                };
                return BadRequest(responseModel);
            }
            responseModel = new ResponseModel
            {
                Message = "Successful",
                Data = callResponse,
                Status = true,
            };
            return Ok(responseModel);
        }


    }
}
