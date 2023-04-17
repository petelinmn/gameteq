import React, { Component } from 'react';
import './custom.css';
import {CurrencyMonitor} from "./components/CurrencyMonitor/CurrencyMonitor";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
        <div className="app">
          <CurrencyMonitor />
        </div>
    );
  }
}
