Random GUI Notes
================

1. Hat will work with MikePie from the opposite direction (code-to-GUI) in an attempt to completely separate the GUI view from the program logic.

2. At some point, look to move the data files from the current "files-in-directory-structure" to the cloud (Azure, AWS, whatever)

3. For any place where there is a (potentially) long list of files (i.e. DataFrame files for both futures and crypto historical data, HTML-charts, etc.): We need to add a simple "filter" textbox where the list filters in real-time as the user types in the textbox. (If the user starts typing "binan..." the list would filter for only those items that contain that string—ignoring upper/lower case)

4. So many of our displays are grid-based. We can come up with a strategy for the look of these grids (or even generic base grid), and we I will design any future grid-based displays around the models we develop.



