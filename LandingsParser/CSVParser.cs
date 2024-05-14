using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.WebSockets;

namespace LandingsParser
{
    public class CSVParser
    {
        // opted to have parseCSV as a separate function instead of called in constructor
        // in case we choose to parse multiple files (they'll just get added to the Landings list)
        private List<MeteoriteLanding> Landings = new List<MeteoriteLanding>();
        
        public List<MeteoriteLanding> getData()
        {
            return Landings;
        }
        /*
        parseCSV can parse any string that is at least:
        name,,,,,,,,,
        if the line being read is missing commas or doesn't have a name
        there will be issues. More error handling could be implemented but 
        due to time constraints we'll assume proper inputs
        */
        public void parseCSV(string CSVLocation)
        {
            var reader = new StreamReader(CSVLocation);
            var line = reader.ReadLine(); // reads the first line (which doesn't contain data)
            while (!reader.EndOfStream)
            {
                /*
                 Line gets broken into clusters
                each cluster is before or after a '"'
                So that will typically look like:
                cluster 0 (Data): "Aachen,1,Valid,L5,21,Fell,1880,50.775000,6.083330,"
                cluster 1 (Geolocation): "(50.775, 6.08333)"
                cluster 2: ""

                or if the recclass includes metal along with type:
                cluster 0 (Data): "Akyumak,433,Valid,"
                cluster 1 (Recclass): "Iron, IVA"
                cluster 2 (Data): ",50000,Fell,1981,39.916670,42.816670,"
                cluster 3 (Geolocation): "(39.91667, 42.81667)"
                cluster 4: ""

                We then break these clusters down and input them
                into their respective places
                 */
                line = reader.ReadLine();
                var clusters = line.Split('"');
                int landingValueCount = 0; // tracks what value we are trying to input for
                MeteoriteLanding temp = new MeteoriteLanding();
                foreach (String cluster in clusters)
                {
                    int clusterValueCounter = 0; // tracks the current input we are trying to enter for a value
                    var values = cluster.Split(",");
                    foreach (var value in values)
                    {
                        /*
                         if cases: 
                        if (!String.Equals(value, "")) for non blank field

                        else if (String.Equals(value, "") && 
                            ((clusterValueCounter != 0 && clusterValueCounter != values.Length - 1) 
                            || (landingValueCount == 4 && String.Equals(temp.recclass, "Unknown")))) 
                            for valid blank fields

                        the way cluster.Split(",") happens we get a empty input before " and after "
                        so we need to make sure to not count++ and input incorrectly on a "fake" blank input
                        Hence why we look for valid blank fields instead of considering all blank values as valid
                        since we know the last blank field in a cluster string is a parsing result,
                        not from the data

                        the else if statement is a bit complex because it needs to ensure that
                        the given blank value isn't a blank starting value after a '"' due to
                        how string splitting works, a blank ending value after a '"' due to how 
                        splitting works, and while ensuring that blank mass values
                        aren't ignored after an "Unkown" entry for recclass 
                        (these are related, one happening usually means the other is also occurring)


                         */
                        if (!(value == "")) // case for non-blank field
                        {
                            if(landingValueCount == 3 && values.Length == 2) // if "metal, type" case such as
                                                                             // "Iron, IIAB" we need to
                                                                             // input both values into recclass
                                                                            // instead of accidentally putting IIAB
                                                                            // into mass... -1 is our special case
                                                                            // for this
                            {
                                landingValueCount = -1;
                            } else if (landingValueCount == 4 && values.Length == 2) // second part of the special
                                                                                     // case discussed above
                            {
                                landingValueCount = -2;
                            }


                            switch (landingValueCount)
                            {
                                case 0: // name (string)
                                    temp.name = value.ToString();
                                    break;
                                case 1: // id (int)
                                    temp.id = Int32.Parse(value.ToString());
                                    break;
                                case 2: // nametype (string)
                                    temp.nametype = value.ToString();
                                    break;
                                case 3: // recclass (string) (single value is just type)
                                    temp.recclass.type = value.ToString();
                                    break;
                                case 4: // mass (int)
                                    temp.mass = double.Parse(value.ToString());
                                    break;
                                case 5: // fall (string)
                                    temp.fall = value.ToString();
                                    break;
                                case 6: // year (int)
                                    temp.year = Int32.Parse(value.ToString());
                                    break;
                                case 7: // reclat (double)
                                    temp.reclat = double.Parse(value.ToString());
                                    break;
                                case 8: // reclong (double)
                                    temp.reclong = double.Parse(value.ToString());
                                    break;
                                case 9: // Geolocation x (double)
                                    String tempValueX = value.Trim('"', '(');
                                    temp.GeoLocation.x = double.Parse(tempValueX.ToString());
                                    break;
                                case 10: // Geolocation y (double)
                                    String tempValueY = value.Trim('"', ')');
                                    temp.GeoLocation.y = double.Parse(tempValueY.ToString());
                                    break;
                                case -1: // recclass (string) (two values is "metal, type") SPECIAL CASE
                                    temp.recclass.metal = value.ToString();
                                    landingValueCount = 3; // next value will also need a special case
                                    break;
                                case -2: // recclass (string) (two values is "metal, type") SPECIAL CASE
                                    temp.recclass.type = value[1..].ToString(); // [1..] removes initial " " space
                                    landingValueCount = 3; // end of special cases for recclass
                                    break;
                            }
                            landingValueCount++;
                        } else if ((value == "") && 
                            ((clusterValueCounter != 0 && clusterValueCounter != values.Length - 1) 
                            || (landingValueCount == 4 && (temp.recclass.type == "Unknown")))) 
                            // blank valid field checks
                        {
                            landingValueCount++;
                        }
                        clusterValueCounter++;
                    }
                }
                Landings.Add(temp);
            }

        }
    }
}
