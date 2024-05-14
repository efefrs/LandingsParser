using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LandingsParser
{
    // using nullable values because int and double inputs could be negative values
    // wanted to use -1 as placeholder for empty inputs but since that could be the geolocation, reclat, etc
    // null is the only way to depict a ",," entry correctly
    public struct GeoLocationPair {
        public Nullable<double> x;
        public Nullable<double> y;

        public GeoLocationPair(Nullable<double> x, Nullable<double> y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct MeteoriteLanding
    {
        public string name;
        public Nullable<int> id;
        public string nametype;
        public string recclass;
        public Nullable<double> mass;
        public string fall;
        public Nullable<int> year;
        public Nullable<double> reclat;
        public Nullable<double> reclong;
        public GeoLocationPair GeoLocation;

        public MeteoriteLanding(string name, Nullable<int> id, string nametype, string recclass,
            Nullable<double> mass, string fall, Nullable<int> year, Nullable<double> reclat, Nullable<double> reclong, GeoLocationPair Geolocation)
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

        public MeteoriteLanding()
        {
            this.name = "";
            this.id = null;
            this.nametype = "";
            this.recclass = "";
            this.mass = null;
            this.fall = "";
            this.year = null;
            this.reclat = null;
            this.reclong = null;
            this.GeoLocation = new GeoLocationPair(null,null);
        }
    }
}
