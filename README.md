# MovieManager

This is a simple WPF application that enables the user to browse their locally stored movie files and see basic info for the movie in focus. In a nutshell...the app makes a number of calls to TmDb (https://www.themoviedb.org/) to retrieve information, it will then store all the information it has collected in a Json file that is also held locally.

Currently the app uses a pretty basic query to pull back information based on a files name, if a single result is returned from TmDb it then assumes this is a perfect match. Any queries made which yield more than one result will require input from the user to decide which TmDb record marries with their file.

# Setup
The app requires an API key from TmDB to work, which is pretty easy to get if you request one. Once you have a key just update the ApiKey value in the App.config.

You'll then have to update the Directory value to point to a folder in which you keep all of your movie files.

At some point I'll add a window into the app to make this setup process simpler...
