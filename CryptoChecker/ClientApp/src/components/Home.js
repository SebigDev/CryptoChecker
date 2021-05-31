import React, { useState } from 'react';

const CryptoCheckerHome = () => {
    const [quoteSearch, setQuoteInput] = useState(
        {
            quoteInput: "",
            quoteStatus: false
        });
    const [quotes, setQuotes] = useState([]);
    const [fetching, setFetching] = useState(false);

    const getCryptoQuotes = (e) => {
        e.preventDefault();
        setFetching(true);
        fetch(`/api/CryptoChecker/GetCryptoCurrencyQuotes/${quoteInput.toUpperCase()}`)
            .then(res => res.json())
            .then(resp => {
                if (resp.status) {
                    setQuotes(resp.data);
                } else {
                    alert(resp.message)
                    setQuotes(resp.data)
                }
                setFetching(false)
            }).catch(err => {
                setFetching(false);
                console.log(err)
            })
    }
    const handleChange = (e) => {
        const { name, value } = e.target;
        setQuoteInput({
            ...quoteSearch,
            [name]: value
        })
    }
   
    const { quoteInput } = quoteSearch
    return (
     <>
        <div className="container">
            <h4>CHECK YOUR CRYPTOCURRENCY EXCHANGE RATE</h4>
            <div className="jumbotron">  
                <form className="form" onSubmit={getCryptoQuotes}>
                    <div className="row">
                        <div className="col-md-4">
                            <input type="text"
                                className="form-control"
                                defaultValue={quoteInput}
                                placeholder="Enter Cryptocurrency code (Eg. BTC)"
                                onChange={handleChange}
                                name="quoteInput"
                            />
                        </div>
                            <div className="col-md-4">
                                <button type="submit"
                                    className="btn btn-outline-secondary"
                                    disabled={fetching}
                                >
                                    {fetching ? "Fetching..." : "Get Exchange Rate"}
                                </button>
                        </div>
                    </div>
                </form>
             </div>
            </div>
            <div className="row">
                <div className="col-md-2"></div>
                <div className="col-md-8">
                    {fetching ? <p style={{ marginLeft: '45%' }}>Loading....</p> : quotes.length === 0 ? null : <ul className="list-group">
                        <>
                            <h4 style={{ marginLeft: '20%' }}>Currency Exchange Rate for {quoteInput.toUpperCase()}</h4>
                            {quotes.length && quotes.map((quote, index) => (

                                <li className="list-group-item d-flex justify-content-between align-items-center" key={index}>
                                    {quote.currencyName}
                                    <span className="badge badge-outline-secondary">{quote.exchangeValue}</span>
                                </li>
                            ))}
                        </>
                    </ul>}
                </div>
                <div className="col-md-2"></div>
             </div>
            <br/>
      </> 
    ); 
}

export default CryptoCheckerHome;
