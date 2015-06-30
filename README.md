# DictionaryLookup
C# library and console application for looking up the definitions of words from a remote source, and using those definitions to build a local dictionary file.

I created this project as the first step in a word search game I am making in Unity.

The problem I have with most word games you see on mobile platforms these days, is that you will encounter many odd words that have no definition.  I set out to solve that problem with this library that I wrote using C#.  It works as both a class library and a console application, and takes the following steps when you run the program:
* Accept a word as input from the keyboard.
* Attempt to look up the word in a local dictionary file.
* If the word does not exist, go to http://dictionary.reference.com/, and do the following:
** Look up the word with a WebRequest.
** If the request is redirected to the misspelling page, return a null value to the local dictionary.
** If the request is redirected to a definition page, scrap the page for definitions, and return a word reference to the local dictionary.
** If the remote dictionary returns a valid word, save it to the local dictionary.
* Once the word is in the dictionary, display it's definitions to the user.
* If the word is undefined, let the user know.
* This will allow the local dictionary to become smarter over time, as the words you loop up are added to it.

The dictionary file format is a simple XML file.  Not very efficient on disk space, but easy to see what is going on.

This project using the HtmlAgilityPack library to parse and scrap web pages.
