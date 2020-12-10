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
     * *With Continuous Delivery, Performance issues can be tracked down in production because feedback can easily be obtained from different sources using monitoring tools            such as site 24x7 etc.*
     * *I have also had the opportunity of monitoring a production application using Dynatrace.*
     
* **Question 4**
    * *The latest technical book I read was **The Digital transformation Playbook** by David Rogers, I have also attended a virtual **DevOpsNG** conference where I learnt about Site Reliability Engineering, which has to do with building and maintaining reliable systems, implementing gradual changes by leveraging on tools and automation processes.*
    
* **Question 5**
    * *I think about this technical assessment as a means to evaluate one's creative thinking, one's approach to solving technical problems and adptation to solutions.*
    
* **Question 6**
  * __Describing myself in JSON__
    ``` json
         {
           "firstname": "Chukwunasa",
           "lastname": "Igwe",
           "age": 30,
           "nationality": "Nigerian",
           "city": "Lagos",
           "mobileNo":"2348068950220",
           "gender":"Male",
           "occupation":"Software Engineer",
           "passions": [
             "Programming",
             "Teaching others"
           ],
           "dreams": [
             "To always be an improved version of myself in my career and vocation."
           ],
           "believes": [
             "No one gives what he/she has not.",
             "The best way to learn is to unlearn."
           ]
         }
    
