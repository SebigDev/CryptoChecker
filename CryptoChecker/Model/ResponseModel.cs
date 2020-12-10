using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoChecker.Model
{
    public class ResponseModel
    {
        public List<ConvertedCurrencyRate> Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
