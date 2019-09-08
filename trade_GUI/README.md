The legacy GUI app is PrimeTrader.exe, and the C# solution resides in the PrimeTrader folder.

To get the PrimeTrader app running: When launching PrimeTrader.exe, the app attempts to find/read from a settings file “PrimeTrader.exe.settings.txt” (in the same folder as the executable file).

The "PrimeTrader.exe.settings.txt" file has a simple “NAME=VALUE” format. This settings file is meant to be a place where general settings for the app can be stored.


Here are two relevant lines in the settings file:

ROOT_DATA_FOLDER=Z:\Dropbox
SECURITY_FILENAME=C:/Users/michael/Documents/hat_apis.json


ROOT_DATA_FOLDER specifies the root directory in which various data folders reside. The "data_folders.zip" file contains the directory structure containing these relevant data folders along with a small sample of data in each folder. You can unzip this file to any location and then set the ROOT_DATA_FOLDER setting to match the location you chose. If the ROOT_DATA_FOLDER setting does not exist in the settings file, the app will attempt to use the same folder as the PrimeTrader.exe executable file.

The Windows environment variable "DROPBOXPATH" can also be used (in lieu of the ROOT_DATA_FOLDER setting) to specify this root directory location.


SECURITY_FILENAME specifies the document which contains the ApiKey/ApiSecret pairs for use by the various exchange APIs. This file is a JSON file which has a format similar to the following:

{"BINANCE":["xxxxxxxx","yyyyyyyyyyyyy"],"BITFINEX":["xxxxxxxxx", "yyyyyyyyyyyy"],"BITHUMB":["xxxxxxxxx","yyyyyyyyyyyyyyyy"]}

This snippet of the JSON file shows the ApiKey/ApiSecret pairs for three different exchanges, but there can be as many exchange name/apikey/apisecret entries as needed.

If the SECURITY_FILENAME setting does not exist, all ApiKey/ApiSecret combinations will default to empty string ("",""). In most cases, this should still allow access to public API data (such as market data).

