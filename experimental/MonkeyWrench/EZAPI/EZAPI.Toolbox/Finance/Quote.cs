/*
    Jarloo
    http://jarloo.com
 
    This work is licensed under a Creative Commons Attribution-ShareAlike 3.0 Unported License  
    http://creativecommons.org/licenses/by-sa/3.0/     

*/
using System;
using System.ComponentModel;

namespace Jarloo.CardStock.Models
{
    public class Quote : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string symbol;
        private decimal? averageDailyVolume;
        private decimal? bid;
        private decimal? ask;
        private decimal? bookValue;
        private decimal? changePercent;
        private decimal? change;
        private decimal? dividendShare;
        private DateTime? lastTradeDate;
        private decimal? earningsShare;
        private decimal? epsEstimateCurrentYear;
        private decimal? epsEstimateNextYear;
        private decimal? epsEstimateNextQuarter;
        private decimal? dailyLow;
        private decimal? dailyHigh;
        private decimal? yearlyLow;
        private decimal? yearlyHigh;
        private decimal? marketCapitalization;
        private decimal? ebitda;
        private decimal? changeFromYearLow;
        private decimal? percentChangeFromYearLow;
        private decimal? changeFromYearHigh;
        private decimal? percentChangeFromYearHigh;
        private decimal? lastTradePrice;
        private decimal? fiftyDayMovingAverage;
        private decimal? twoHunderedDayMovingAverage;
        private decimal? changeFromTwoHundredDayMovingAverage;
        private decimal? percentChangeFromFiftyDayMovingAverage;
        private string name;
        private decimal? open;
        private decimal? previousClose;
        private decimal? changeInPercent;
        private decimal? priceSales;
        private decimal? priceBook;
        private DateTime? exDividendDate;
        private decimal? pegRatio;
        private decimal? priceEpsEstimateCurrentYear;
        private decimal? priceEpsEstimateNextYear;
        private decimal? shortRatio;
        private decimal? oneYearPriceTarget;
        private decimal? dividendYield;
        private DateTime? dividendPayDate;
        private decimal? percentChangeFromTwoHundredDayMovingAverage;
        private decimal? peRatio;
        private decimal? volume;
        private string stockExchange;
        private DateTime lastUpdate;
        

        public DateTime LastUpdate
        {
            get { return lastUpdate; }
            set
            {
                lastUpdate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastUpdate"));
            }
        }


        public string StockExchange
        {
            get { return stockExchange; }
            set
            {
                stockExchange = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("StockExchange"));
            }
        }


        public decimal? Volume
        {
            get { return volume; }
            set
            {
                volume = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Volume"));
            }
        }

        public decimal? PeRatio
        {
            get { return peRatio; }
            set
            {
                peRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PeRatio"));
            }
        }

        public decimal? PercentChangeFromTwoHundredDayMovingAverage
        {
            get { return percentChangeFromTwoHundredDayMovingAverage; }
            set
            {
                percentChangeFromTwoHundredDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromTwoHundredDayMovingAverage"));
            }
        }

        public Quote(string ticker)
        {
            symbol = ticker;
        }

        public DateTime? DividendPayDate
        {
            get { return dividendPayDate; }
            set
            {
                dividendPayDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendPayDate"));
            }
        }

        public decimal? DividendYield
        {
            get { return dividendYield; }
            set
            {
                dividendYield = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendYield"));
            }
        }


        public decimal? OneYearPriceTarget
        {
            get { return oneYearPriceTarget; }
            set
            {
                oneYearPriceTarget = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OneYearPriceTarget"));
            }
        }

        public decimal? ShortRatio
        {
            get { return shortRatio; }
            set
            {
                shortRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ShortRatio"));
            }
        }


        public decimal? PriceEpsEstimateNextYear
        {
            get { return priceEpsEstimateNextYear; }
            set
            {
                priceEpsEstimateNextYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceEpsEstimateNextYear"));
            }
        }


        public decimal? PriceEpsEstimateCurrentYear
        {
            get { return priceEpsEstimateCurrentYear; }
            set
            {
                priceEpsEstimateCurrentYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceEpsEstimateCurrentYear"));
            }
        }


        public decimal? PegRatio
        {
            get { return pegRatio; }
            set
            {
                pegRatio = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PegRatio"));
            }
        }


        public DateTime? ExDividendDate
        {
            get { return exDividendDate; }
            set
            {
                exDividendDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ExDividendDate"));
            }
        }


        public decimal? PriceBook
        {
            get { return priceBook; }
            set
            {
                priceBook = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceBook"));
            }
        }


        public decimal? PriceSales
        {
            get { return priceSales; }
            set
            {
                priceSales = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PriceSales"));
            }
        }


        public decimal? ChangeInPercent
        {
            get { return changeInPercent; }
            set
            {
                changeInPercent = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeInPercent"));
            }
        }


        public decimal? PreviousClose
        {
            get { return previousClose; }
            set
            {
                previousClose = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PreviousClose"));
            }
        }


        public decimal? Open
        {
            get { return open; }
            set
            {
                open = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Open"));
            }
        }


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }


        public decimal? PercentChangeFromFiftyDayMovingAverage
        {
            get { return percentChangeFromFiftyDayMovingAverage; }
            set
            {
                percentChangeFromFiftyDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromFiftyDayMovingAverage"));
            }
        }


        public decimal? ChangeFromTwoHundredDayMovingAverage
        {
            get { return changeFromTwoHundredDayMovingAverage; }
            set
            {
                changeFromTwoHundredDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromTwoHundredDayMovingAverage"));
            }
        }


        public decimal? TwoHunderedDayMovingAverage
        {
            get { return twoHunderedDayMovingAverage; }
            set
            {
                twoHunderedDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TwoHunderedDayMovingAverage"));
            }
        }


        public decimal? FiftyDayMovingAverage
        {
            get { return fiftyDayMovingAverage; }
            set
            {
                fiftyDayMovingAverage = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FiftyDayMovingAverage"));
            }
        }


        public decimal? LastTradePrice
        {
            get { return lastTradePrice; }
            set
            {
                lastTradePrice = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastTradePrice"));
            }
        }


        public decimal? PercentChangeFromYearHigh
        {
            get { return percentChangeFromYearHigh; }
            set
            {
                percentChangeFromYearHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromYearHigh"));
            }
        }


        public decimal? ChangeFromYearHigh
        {
            get { return changeFromYearHigh; }
            set
            {
                changeFromYearHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromYearHigh"));
            }
        }


        public decimal? PercentChangeFromYearLow
        {
            get { return percentChangeFromYearLow; }
            set
            {
                percentChangeFromYearLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("PercentChangeFromYearLow"));
            }
        }


        public decimal? ChangeFromYearLow
        {
            get { return changeFromYearLow; }
            set
            {
                changeFromYearLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangeFromYearLow"));
            }
        }


        public decimal? Ebitda
        {
            get { return ebitda; }
            set
            {
                ebitda = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Ebitda"));
            }
        }


        public decimal? MarketCapitalization
        {
            get { return marketCapitalization; }
            set
            {
                marketCapitalization = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MarketCapitalization"));
            }
        }


        public decimal? YearlyHigh
        {
            get { return yearlyHigh; }
            set
            {
                yearlyHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("YearlyHigh"));
            }
        }


        public decimal? YearlyLow
        {
            get { return yearlyLow; }
            set
            {
                yearlyLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("YearlyLow"));
            }
        }


        public decimal? DailyHigh
        {
            get { return dailyHigh; }
            set
            {
                dailyHigh = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DailyHigh"));
            }
        }


        public decimal? DailyLow
        {
            get { return dailyLow; }
            set
            {
                dailyLow = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DailyLow"));
            }
        }


        public decimal? EpsEstimateNextQuarter
        {
            get { return epsEstimateNextQuarter; }
            set
            {
                epsEstimateNextQuarter = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateNextQuarter"));
            }
        }


        public decimal? EpsEstimateNextYear
        {
            get { return epsEstimateNextYear; }
            set
            {
                epsEstimateNextYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateNextYear"));
            }
        }


        public decimal? EpsEstimateCurrentYear
        {
            get { return epsEstimateCurrentYear; }
            set
            {
                epsEstimateCurrentYear = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EpsEstimateCurrentYear"));
            }
        }


        public decimal? EarningsShare
        {
            get { return earningsShare; }
            set
            {
                earningsShare = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("EarningsShare"));
            }
        }


        public DateTime? LastTradeDate
        {
            get { return lastTradeDate; }
            set
            {
                lastTradeDate = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("LastTradeDate"));
            }
        }


        public decimal? DividendShare
        {
            get { return dividendShare; }
            set
            {
                dividendShare = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DividendShare"));
            }
        }


        public decimal? Change
        {
            get { return change; }
            set
            {
                change = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Change"));
            }
        }


        public decimal? ChangePercent
        {
            get { return changePercent; }
            set
            {
                changePercent = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ChangePercent"));
            }
        }


        public decimal? BookValue
        {
            get { return bookValue; }
            set
            {
                bookValue = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("BookValue"));
            }
        }


        public decimal? Ask
        {
            get { return ask; }
            set
            {
                ask = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Ask"));
            }
        }


        public decimal? Bid
        {
            get { return bid; }
            set
            {
                bid = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Bid"));
            }
        }


        public decimal? AverageDailyVolume
        {
            get { return averageDailyVolume; }
            set
            {
                averageDailyVolume = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AverageDailyVolume"));
            }
        }


        public string Symbol
        {
            get { return symbol; }
            set
            {
                symbol = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Symbol"));
            }
        }
    }
}