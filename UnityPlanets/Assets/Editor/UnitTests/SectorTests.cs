using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Planets;

namespace Assets.Editor.UnitTests
{
    public class SectorTests
    {
        [Test]
        public void PlanetDataTest()
        {
            var constants = new Constants();
            var rawData = new int[] { 100000211, 12233, 500991, 30000103};
            var sector = new Sector(constants, 1, 1, rawData);

            var planet01 = sector.GetPlanetData(0);

            Assert.AreEqual(planet01.Score, 10000);
            Assert.AreEqual(planet01.X, 111);
            Assert.AreEqual(planet01.Y, 102);
            Assert.AreEqual(sector.GetPlanetRating(0), 10000);
        }

        [Test]
        public void PlanetDataNegativeTest()
        {
            var constants = new Constants();
            var rawData = new int[] { 100000211};
            var sector = new Sector(constants, -1, -1, rawData);

            var planet01 = sector.GetPlanetData(0);

            Assert.AreEqual(planet01.Score, 10000);
            Assert.AreEqual(planet01.X, -89);
            Assert.AreEqual(planet01.Y, -98);
            Assert.AreEqual(sector.GetPlanetRating(0), 10000);
        }

        [Test]
        public void PlanetDataMassiveTest()
        {
            var constants = new Constants();
            var totalPlanets = constants.GetCellsInSector();
            var rawData = new int[totalPlanets];
            for (int i = 0; i < totalPlanets; ++i)
            {
                rawData[i] = i * constants.GetCellsInSector() + i;
            }
            var sector = new Sector(constants, -1, -1, rawData);

            for (int i = 0; i < totalPlanets; ++i)
            {
                var planet = sector.GetPlanetData(i);

                Assert.AreEqual(planet.Score, i);
                Assert.AreEqual(sector.GetPlanetRating(i), i);
                var y = i / constants.GetSectorSideSize();
                var x = i - y * constants.GetSectorSideSize();
                Assert.AreEqual(planet.X, sector.GetX()*constants.GetSectorSideSize() + x);
                Assert.AreEqual(planet.Y, sector.GetY() * constants.GetSectorSideSize() +y);
            }
        }
    }
}
