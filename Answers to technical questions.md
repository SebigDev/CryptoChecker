### Answers to technical questions
* **Question 1**
    * *I spent approximately 7 hours on the coding assignment. If I had more time, I would subscribe to a standard plan and explore the **CoinMarketAPI** for easier usage in displaying the **Quotes**.*
    
* **Question 2**
    * *The most useful feature that was added to the Language of choice **(.Net Core)** is the **dependency injection** because it is a pattern that makes code flexible and loosely coupled.*
    * *The Code snippet below*
    
       ```cs 
           private readonly ICoinMarketAPIService _coinMarketAPIService;
           public CryptoCheckerController(ICoinMarketAPIService coinMarketAPIService)
           {
            _coinMarketAPIService = coinMarketAPIService ??
                                   throw new ArgumentNullException(nameof(coinMarketAPIService));
            }
        
* **Question 3**
     * *Knowing when performance issue exists by monitoring requests and response time using AppInsight*
     * **
