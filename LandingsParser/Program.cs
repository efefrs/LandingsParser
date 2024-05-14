using LandingsParser;
using System.IO;

CSVParser parser = new CSVParser();
// System.IO wants to read from '\LandingsParser\LandingsParser\bin\Debug\net6.0\Meteorite_Landings.csv'
// when it should be reading from '\LandingsParser\LandingsParser\Meteorite_Landings.csv'
// which is why we are using '../../../' in our filename string to move back three folders.
// please note this may be different on your device
parser.parseCSV("../../../Meteorite_Landings.csv");

LandingObservations observer = new LandingObservations(parser.getData());
observer.observations();

/* If you want to print all of the landings uncomment the line below: */
//observer.printLandings();
