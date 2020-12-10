using CryptoChecker.Infrastructure;
using CryptoChecker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;
using System.Threading.Tasks;
using CryptoChecker.Utility;

namespace CryptoChecker.Test
{
    public class CryptoCheckerUnitTest
    {
        Mock<ICoinMarketAPIService> _service;
        List<ConvertedCurrencyRate> _convertedCurrencies;
        QuoteWrapper _quote;
        ExchangeRate exchangeRate;
    

        public CryptoCheckerUnitTest()
        {
            _service = new Mock<ICoinMarketAPIService>();
            _convertedCurrencies = new List<ConvertedCurrencyRate>();
            _quote = new QuoteWrapper
            {
                Usd = new Usd
                {
                    Price = 6602.60701122,
                }
            };
            exchangeRate = new ExchangeRate
            {
                Rates = new Dictionary<string, double>(){}
            };
        }

         [Fact]
        public void GetQuoteForValuedCryptoCurrency_Success()
        {
            //Arrange
            var currencyCode = "BTC";

            //act
            var resp = _service.Setup(svc => svc.GetQuoteForValuedCryptoCurrency(currencyCode)).Returns(Task.FromResult(_quote));
            var expectedResult = _service.Object.GetQuoteForValuedCryptoCurrency(currencyCode).Result;
            //Assert
            Assert.Equal(expectedResult,_quote);
        }


        [Fact]
        public void PerformConversionOnQuotes_Success() 
        {
            //Arrange
            var baseCurrency = "USD";

            //act
            var resp = _service.Setup(svc => svc.PerformConversionOnQuotes(baseCurrency)).Returns(Task.FromResult(exchangeRate));
            var expectedResult = _service.Object.PerformConversionOnQuotes(baseCurrency).Result;

            //Assert
            Assert.Equal(expectedResult, exchangeRate);
        }


        [Fact]
        public void CurrencyValueConverter_Success() 
        {
            //Arrange
            var rate = 0.5;
            var btcRate = 900;

            //act
            var actual  = CurrencyValueConverter.ResolveExchangerate(rate,btcRate);

            //Assert
            Assert.Equal(450,actual);
        }
    }
}
