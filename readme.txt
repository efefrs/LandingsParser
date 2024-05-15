All testing and programming was done in Visual Studio 2022.

LandingsParser is the code, it parses Meteorite_Landings.csv. If you want to run this you can 
click the green start button at the top right of the screen assuming you're in visual studio.
A terminal should pop up with some information regarding trends in the data.
If you want to print the entire csv to console, simply uncomment the 'observer.printLandings();' line
in Program.cs before running the code.

LandingParserTests is the NUnit tests. It has three test .csv files and tests all of the functions
from LandingsParser except for LandingObservations.PrintLandings() 
(since it's a print function, can only really test manually). 

To run NUnit tests I would right click LandingParserTests and click the "Run Tests" option.


The 'Solution Items' folder contains this readme as well as a UML Diagram outlining
what is going on in this project from a high-level view.

If you somehow got to this code without viewing the github page, 
the link for that is here: https://github.com/efefrs/LandingsParser

Thank you for taking the time to read this and for reviewing the code.

