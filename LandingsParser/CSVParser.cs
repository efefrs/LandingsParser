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
    class CSVParser
    {
        public List<MeteoriteLanding> Landings = new List<MeteoriteLanding>();
        
        public List<MeteoriteLanding> getData()
        {
            return Landings;
        }

        public void parseCSV(string CSVLocation)
        {
            //!reader.EndOfStream
            var reader = new StreamReader(CSVLocation);
            var line = reader.ReadLine(); // reads the first line (which doesn't contain data)
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                var clusters = line.Split('"'); // cluser can be [a,b,c] ["36,37"] [""]...
                                                // (split by "" for portions with multiple
                                                // inputs such as pairs,
                                                // or in some cases recclass, etc.
                int landingValueCount = 0;
                MeteoriteLanding temp = new MeteoriteLanding();
                foreach (String cluster in clusters)
                {
                    int clusterValueCounter = 0;
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
                        the given blank value isn't ...

                         */
                        if (!String.Equals(value, "")) // case for non-blank field
                        {
                            if(landingValueCount == 3 && values.Length == 2) // if "Iron, IIAB" case we need to
                                                                      // input both values into recclass
                                                                      // instead of accidentally putting IIAB
                                                                      // into mass... -1 is our special case
                                                                      // for this
                            {
                                landingValueCount = -1;
                            } else if (landingValueCount == 4 && values.Length == 2)
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
                                case 3: // recclass (string)
                                    temp.recclass = value.ToString();
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
                                case -1:
                                    temp.recclass = value.ToString();
                                    landingValueCount = 3;
                                    break;
                                case -2:
                                    temp.recclass = String.Join(", ", temp.recclass, value.ToString());
                                    landingValueCount = 3;
                                    break;
                            }
                            landingValueCount++;
                        } else if (String.Equals(value, "") && 
                            ((clusterValueCounter != 0 && clusterValueCounter != values.Length - 1) 
                            || (landingValueCount == 4 && String.Equals(temp.recclass, "Unknown")))) 
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
