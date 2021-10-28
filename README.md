# Factiva Article Parser
Simple parser for Factiva articles.
Can be useful for further analyzing with TopicMiner_LINIS.

## Usage:
1. Open 100 articles from Factiva in one page, save HTML page. Repeat as many times you wish. You can store your HTML files in different folders, next to each other.
2. Run FactivaHtmlParser.Core.exe from cmd with arguments:
* 	-p <path> - Set path where factiva html files would be searched.
* 	-d <int> - Set depth of finding folders with factiva html data.
* 	
3. Parser will find all *.html files, and write text content of each article to separate files with file name like article title. 
Files will be stored in 'out' directory, next to original articles located.