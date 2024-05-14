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
            GeoLocationPair normal = new GeoLocationPair(2, 500);
            if (normal.x != 2 || normal.y != 500)
            {
                Assert.Fail();
            }

            // boundary case
            GeoLocationPair boundary = new GeoLocationPair(234234.555, 8797.990);
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
            GeoLocationPair normalPair = new GeoLocationPair(50.775, 6.08333);
            RecclassPair normalRecPair = new RecclassPair("", "L5");
            MeteoriteLanding normal = new MeteoriteLanding("Aachen", 1, "Valid", normalRecPair, 21,
                "Fell", 1880, 50.775000, 6.083330, normalPair);
            if (normal.name != "Aachen" || normal.id != 1 || normal.nametype != "Valid" || normal.recclass.type != "L5" 
                || normal.mass != 21 || normal.fall != "Fell" || normal.year != 1880 || normal.reclat != 50.775000
                || normal.reclong != 6.083330 || normal.GeoLocation.x != normalPair.x || normal.GeoLocation.y != normalPair.y)
            {
                Assert.Fail();
            }

            // boundary case
            GeoLocationPair boundaryPair = new GeoLocationPair(33.98333, -115.68333);
            RecclassPair boundaryRecPair = new RecclassPair("Iron", "L3.7");
            MeteoriteLanding boundary = new MeteoriteLanding("Zulu Queen", 30414, "Valid", boundaryRecPair, 200, 
                "Found", 1976, 33.983330, -115.683330, boundaryPair);
            if (boundary.name != "Zulu Queen" || boundary.id != 30414 || boundary.nametype != "Valid" || boundary.recclass.type != "L3.7" 
                || boundary.recclass.metal != "Iron" || boundary.mass != 200 || boundary.fall != "Found" || boundary.year != 1976 
                || boundary.reclat != 33.983330 || boundary.reclong != -115.683330 || boundary.GeoLocation.x != boundaryPair.x 
                || boundary.GeoLocation.y != boundaryPair.y)
            {
                Assert.Fail();
            }

            // overflow case
            // N/A
        }
        [Test]
        public void TestCSVParser()
        {
            // normal case
            GeoLocationPair normalPair = new GeoLocationPair(29.037, 17.0185);
            RecclassPair normalRecclassPair = new RecclassPair("", "L6");
            MeteoriteLanding normalLanding = new MeteoriteLanding("Zillah 001", 31355, "Valid", normalRecclassPair, 1475, "Found", 1990, 29.037000, 17.018500, normalPair);
            CSVParser normalParser = new CSVParser();
            normalParser.parseCSV("../../../normal.csv");
            if (normalParser.getData()[0] != normalLanding)
            {
                Assert.Fail();
            }

            // boundary case
            GeoLocationPair boundaryPair = new GeoLocationPair(24.23333, 111.18333);
            RecclassPair boundaryRecclassPair = new RecclassPair("Iron", "IAB complex");
            MeteoriteLanding boundaryLanding = new MeteoriteLanding("Zhaoping", 54609, "Valid", boundaryRecclassPair, 2000000, "Found", 1983, 24.233330, 111.183330, boundaryPair);
            CSVParser boundaryParser = new CSVParser();
            boundaryParser.parseCSV("../../../boundary.csv");
            if (boundaryParser.getData()[0] != boundaryLanding)
            {
                Assert.Fail();
            }

            // overflow case
            MeteoriteLanding overflowLanding = new MeteoriteLanding();
            overflowLanding.name = "Cacilandia";
            CSVParser overflowParser = new CSVParser();
            overflowParser.parseCSV("../../../overflow.csv");
            if (overflowParser.getData()[0] != overflowLanding)
            {
                Assert.Fail();
            }
        }
    }
}