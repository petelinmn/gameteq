import React, {Component, useEffect, useState} from 'react';
import {CurrencyList} from '../CurrencyList/CurrencyList';
import DatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import './CurrencyMonitor.css'
import {GtDateTimePicker} from "../GtDateTimePicker/GtDateTimePicker";

const formatDate = date => date?.toLocaleDateString() ?? '-'

export function CurrencyMonitor(props) {
  const [selectedCurrency, setSelectedCurrency] = useState(0)
  const [selectedDate, setSelectedDate] = useState()
  const [price, setPrice] = useState()

  const formattedDate = formatDate(selectedDate)
  useEffect(() => {
      (async function fetchCurrencies() {
          if (selectedCurrency && selectedDate) {
            try {
              const response = await fetch(
                  `weatherforecast/currency?currencyId=${selectedCurrency.id}&date=${formattedDate}`)
              if (response.status === 204) {
                alert('No data!')
                return
              }
              const currency = await response.json()

              if (currency?.prices?.length > 0) {
                setPrice(currency.prices[0].value)
              } else {
                alert('No data!')
              }
            } catch (error) {
              alert(error)
            }
          }
      })();
  }, [selectedCurrency, selectedDate])

  return (
    <>
    <div className="currency-monitor-item">
        <CurrencyList onSelect={setSelectedCurrency}/>
    </div>
    <div className="currency-monitor-item">
        <GtDateTimePicker onSelectDate={setSelectedDate}/>
        <p />
        <h3>Date: {formatDate(selectedDate)}</h3>
        <p />
        <h3>Currency: {selectedCurrency.name ?? '-'}</h3>
        <p />
        <h3>Price: {price ?? '-'}</h3>
    </div>
    </>
  )
}
