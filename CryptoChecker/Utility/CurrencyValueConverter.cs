namespace CryptoChecker.Utility
{
    public class CurrencyValueConverter
    {
        public static double ResolveExchangeRate(double rate, double btcRate) => rate * btcRate; 
        
    }
}
