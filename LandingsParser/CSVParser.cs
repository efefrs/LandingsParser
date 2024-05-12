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

                var values = line.Split(',');
                // name,id,nametype,recclass,mass (g),fall,year,reclat,reclong,GeoLocation
                // Aachen,1,Valid,L5,21,Fell,1880,50.775000,6.083330,"(50.775, 6.08333)"
                if (values.Length != 11)
                {
                    throw new Exception("line missing values");
                } /*if (values[0].GetType() != typeof(string) || values[1].GetType() != typeof(int)
                    || values[2].GetType() != typeof(string) || values[3].GetType() != typeof(string) 
                    || values[4].GetType() != typeof(int) || values[5].GetType() != typeof(string) 
                    || values[6].GetType() != typeof(int) || values[7].GetType() != typeof(double)
                    || values[8].GetType() != typeof(double) || values[9].GetType() != typeof(double)
                    || values[10].GetType() != typeof(double))
                {
                    throw new Exception("incorrect input types");
                }*/

                values[9] = values[9].Trim('"', '(');
                values[10] = values[10].Trim('"', ')');
                double PairXVal = Convert.ToDouble(values[9]);
                double PairYVal = Convert.ToDouble(values[10]);
                Pair tempPair = new Pair(PairXVal, PairYVal);


                MeteoriteLanding temp = new MeteoriteLanding(values[0], Convert.ToInt32(values[1]), values[2], values[3], Convert.ToInt32(values[4]), values[5], Convert.ToInt32(values[6]), Convert.ToDouble(values[7]), Convert.ToDouble(values[8]), tempPair);

                Landings.Add(temp);

            }
        }
    }
}
