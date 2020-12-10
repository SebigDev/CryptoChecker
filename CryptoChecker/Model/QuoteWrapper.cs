using Newtonsoft.Json;
using System;

namespace CryptoChecker.Model
{
    public partial class QuoteWrapper
    {
        public Status Status { get; set; }
        public Data Data { get; set; }
    }

    public partial class Data
    {
        public Btc Btc { get; set; }
    }

    public partial class Btc
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public long NumMarketPairs { get; set; }
        public DateTimeOffset DateAdded { get; set; }
        public string[] Tags { get; set; }
        public long MaxSupply { get; set; }
        public long CirculatingSupply { get; set; }
        public long TotalSupply { get; set; }
        public long IsActive { get; set; }
        public object Platform { get; set; }
        public long CmcRank { get; set; }
        public long IsFiat { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
        public Quote Quote { get; set; }
    }

    public partial class Quote
    {
        public Usd Usd { get; set; }
    }

    public partial class Usd
    {
        public double Price { get; set; }
        public double Volume24H { get; set; }
        public double PercentChange1H { get; set; }
        public double PercentChange24H { get; set; }
        public double PercentChange7D { get; set; }
        public double MarketCap { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }

    public partial class Status
    {
        public DateTimeOffset Timestamp { get; set; }
        public long ErrorCode { get; set; }
        public object ErrorMessage { get; set; }
        public long Elapsed { get; set; }
        public long CreditCount { get; set; }
        public object Notice { get; set; }
    }
}
