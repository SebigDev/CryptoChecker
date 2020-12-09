import React, { useState } from 'react';

const CryptoCheckerHome = () => {
    const [quoteSearch, setQuoteInput] = useState(
        {
            quoteInput: "",
            quoteStatus: false
        });
    const [quotes, setQuotes] = useState([]);

    const getCryptoQuotes = (e) => {
        e.preventDefault();
        fetch("/api/CryptoChecker/GetCryptoCurrencyQuotes?inputvalues?=" + quoteInput)
            .then(res => res.json())
            .then(resp => {
                setQuotes(resp);
            }).catch(err => {
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
    console.log(quoteInput)
    return (
      
        <div classname="container">
            <h3>CHECK YOUR CRYPTO QUOTES</h3>
            <p>Welcome To Crypto Currency Quotes Checker:</p>
            <div className="jumbotron">
                <form className="form" onSubmit={getCryptoQuotes}>
                    <input type="text"
                        className="form_control"
                        defaultValue={quoteInput}
                        placeholder="enter cryptocurrency code"
                        onChange={handleChange}
                        name="quoteInput"
                    />
                    &nbsp;
                    <button type="submit">GET QUOTES</button>
                </form>
            </div>
        </div>
    );
}

export default CryptoCheckerHome;
