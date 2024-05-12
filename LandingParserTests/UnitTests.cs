using LandingsParser;
using Microsoft.VisualBasic.FileIO;
using System;

namespace LandingParserTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestPairStruct()
        {
            // normal case
            Pair normal = new Pair(2, 500);
            if (normal.x != 2 || normal.y != 500)
            {
                Assert.Fail();
            }

            // boundary case
            Pair boundary = new Pair(234234.555, 8797.990);
            if (boundary.x != 234234.555 || boundary.y != 8797.990)
            {
                Assert.Fail();
            }

            // overflow case
            // N/A
        }
        [Test]
        public void TestMeteoriteLandingStruct()
        {
            // first entry: Aachen,1,Valid,L5,21,Fell,1880,50.775000,6.083330,"(50.775, 6.08333)"
            // Last entry: Zulu Queen,30414,Valid,L3.7,200,Found,1976,33.983330,-115.683330,"(33.98333, -115.68333)"
            // values: name,id,nametype,recclass,mass (g),fall,year,reclat,reclong,GeoLocation

            // normal case
            Pair normalPair = new Pair(50.775, 6.08333);
            MeteoriteLanding normal = new MeteoriteLanding("Aachen", 1, "Valid", "L5", 21,
                "Fell", 1880, 50.775000, 6.083330, normalPair);
            if (normal.name != "Aachen" || normal.id != 1 || normal.nametype != "Valid" || normal.recclass != "L5" 
                || normal.mass != 21 || normal.fall != "Fell" || normal.year != 1880 || normal.reclat != 50.775000
                || normal.reclong != 6.083330 || normal.GeoLocation.x != normalPair.x || normal.GeoLocation.y != normalPair.y)
            {
                Assert.Fail();
            }

            // boundary case
            Pair boundaryPair = new Pair(33.98333, -115.68333);
            MeteoriteLanding boundary = new MeteoriteLanding("Zulu Queen", 30414, "Valid", "L3.7", 200, 
                "Found", 1976, 33.983330, -115.683330, boundaryPair);
            if (boundary.name != "Zulu Queen" || boundary.id != 30414 || boundary.nametype != "Valid" || boundary.recclass != "L3.7"
                || boundary.mass != 200 || boundary.fall != "Found" || boundary.year != 1976 || boundary.reclat != 33.983330
                || boundary.reclong != -115.683330 || boundary.GeoLocation.x != boundaryPair.x || boundary.GeoLocation.y != boundaryPair.y)
            {
                Assert.Fail();
            }

            // overflow case
            // N/A
        }
    }
}