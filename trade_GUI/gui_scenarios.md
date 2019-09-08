GUI Functional Design
=====================

### Intro

*UPDATE:* MikePie explained that the correct way to think about "scenarios" is "As a (blank) I want to (blank)". For example, "As a (trader) I want to (view the spread price)".

Not sure if this is exactly what you meant by "scenarios", but here's a crack at dividing the app functionality into five main pieces of functionality:

1. **Markets** (live, updating market prices and information)
2. **Trading** (any interfaces with which to execute trades)
3. **Data Analytics** (retrieval and statistical analysis of historical market data)
4. **Strategy/Algorithms** (ability to view, create, launch and monitor automated strategies/algorithms)
5. other (these would perhaps fit under the "horizontal lines" menu on various apps)



### 1. Markets
- Prices and Spreads form (I think this should be split into two displays: (1) prices (2) spreads)
- Crypto Market Information form
- Crypto Prices form
- Live (streaming) bar charts (not yet implemented)


### 2. Trading
- Crypto Aggregator form ("GATOR")
- Crypto Trader form
- Futures Trader form (not yet implemented)
- Bitcoin futures hedging form (not yet implemented)


### 3. Data Analytics
- Historical Data form
- DataFrame Files list (Data tab on main form) and Data Grid (button on toolbar)
- Charts (button on toolbar) and Charts (tab on main form)
- Scripts tab on main form (launch Python scripts)
- Back Test tab on main form
- Study tab on Crypto Trader form


### 4. Strategy/Algorithms
- Strategy tab on main form
- Algo tab on Crypto Trader form (this will be fleshed out to display algo parameters and launch/monitor algos)


### 5. other
- Settings form
- Messages form

