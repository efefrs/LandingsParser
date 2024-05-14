using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingsParser
{
    public class LandingObservations
    {

        // takes a list of meteorite landings (most likely parsed from a file)
        // and makes observations that are printed to console based off that data
        public void observe(List<MeteoriteLanding> Landings)
        {
            KeyValuePair<string, int> commonMetal = mostCommonMetal(Landings);
            Console.WriteLine("The most common metal in the given data is " + commonMetal.Key + " with a total of " + commonMetal.Value + " instances\n");

            KeyValuePair<string, int> uncommonMetal = leastCommonMetal(Landings);
            Console.WriteLine("The least common metal in the given data is " + uncommonMetal.Key + " with a total of " + uncommonMetal.Value + " instances\n");

            Console.WriteLine("The amount of non-metal (i.e. stone) meteors in the given data is " + stoneCount(Landings));
        }

        private KeyValuePair<string, int> mostCommonMetal(List<MeteoriteLanding> Landings)
        {
            Dictionary<string, int> metalCount = new Dictionary<string, int>();
            foreach(MeteoriteLanding Landing in Landings)
            {
                if (Landing.recclass.metal != "")
                {
                    if (metalCount.ContainsKey(Landing.recclass.metal))
                    {
                        metalCount[Landing.recclass.metal]++;
                    } else
                    {
                        metalCount[Landing.recclass.metal] = 1;
                    }
                }
            }
            int maxCount = metalCount.Values.Max();

            // followed from here: https://stackoverflow.com/questions/10290838/how-to-get-max-value-from-dictionary
            KeyValuePair<string, int> commonMetal = metalCount.MaxBy(entry => entry.Value);

            return commonMetal;
        }

        private KeyValuePair<string, int> leastCommonMetal(List<MeteoriteLanding> Landings)
        {
            Dictionary<string, int> metalCount = new Dictionary<string, int>();
            foreach (MeteoriteLanding Landing in Landings)
            {
                if (Landing.recclass.metal != "")
                {
                    if (metalCount.ContainsKey(Landing.recclass.metal))
                    {
                        metalCount[Landing.recclass.metal]++;
                    }
                    else
                    {
                        metalCount[Landing.recclass.metal] = 1;
                    }
                }
            }
            int maxCount = metalCount.Values.Min();
            KeyValuePair<string, int> uncommonMetal = metalCount.MinBy(entry => entry.Value);

            return uncommonMetal;
        }

        private int stoneCount(List<MeteoriteLanding> Landings)
        {
            int stoneCount = 0;
            foreach (MeteoriteLanding Landing in Landings)
            {
                if (Landing.recclass.metal == "")
                {
                    stoneCount++;
                }
            }
            return stoneCount;
        }


    }
}
