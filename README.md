This is a one-sided game of Battleship, where the program generates the specified ships at random locations on a 10x10 board. The user then gets to fire shots, with coordinates, such as `B1` and `J10`, and the program registers those
coordinates, logs them as hits on the board and then feeds back to the user whether their shot was a success or not.

Change `Config.GameConfig.DebugMode = false;` to true in order to display the saved ship coordinates.

No external packages were used, so no dependency manager was used.

My task was to make an Object Oriented game but I still took a slightly functional approach to the placing of ships on the board, as I thought that would be a more beautiful method.

I added a User Interface, even though it wasn't requested. Being a console application I took a bit of an odd approach, and rather than doing `Console.Write()` etc. from the Controller, I made a View that contains a List called "Lines". Every time I want to 
add a line to the console I just added it to the Lines list, and then my Display method, when called, would clear the console, output the data and do a few other things to keep it tidy ... Well, as tidy as a console application's view can be I'd say!