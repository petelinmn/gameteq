import React, { useState, useEffect } from 'react';
import './CurrencyList.css'

export function CurrencyList(props) {
    const [currencies, setCurrencies] = useState([])
    const [selected, select] = useState(0);

    useEffect(() => {
        (async function fetchCurrencies() {
            const response = await fetch('currency/all')
            const currencies = await response.json()
            setCurrencies(currencies)
        })();
    }, [])

    const onClick = curId => {
        select(curId)
        props.onSelect(curId)
    }
    return (
        <ul className="list-group list-group-currencies">
            {currencies.map(c => (
                <li key={c.id}
                    onClick={onClick.bind(this, { id: c.id , name: c.name })}
                    className={`list-group-item ${selected === c.id ? 'selected' : ''}`}>
                    {c.name}
                </li>
            ))}
        </ul>
    )
}
