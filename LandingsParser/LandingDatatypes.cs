using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingsParser
{
    public struct Pair {
        public double x;
        public double y;

        public Pair(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct MeteoriteLanding
    {
        public string name;
        public int id;
        public string nametype;
        public string recclass;
        public int mass;
        public string fall;
        public int year;
        public double reclat;
        public double reclong;
        public Pair GeoLocation;

        public MeteoriteLanding(string name, int id, string nametype, string recclass, 
            int mass, string fall, int year, double reclat, double reclong, Pair Geolocation)
        {
            this.name = name;
            this.id = id;
            this.nametype = nametype;
            this.recclass = recclass;
            this.mass = mass;
            this.fall = fall;
            this.year = year;
            this.reclat = reclat;
            this.reclong = reclong;
            this.GeoLocation = Geolocation;
        }
    }
}
