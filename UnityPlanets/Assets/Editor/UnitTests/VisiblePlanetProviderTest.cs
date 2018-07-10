using System.Collections.Generic;
using Assets.Editor.UnitTests;
using Assets.Scripts;
using NUnit.Framework;
using Planets;
using Assert = NUnit.Framework.Assert;

namespace Editor.UnitTests
{
    public  class VisiblePlanetProviderTest
    {
        [Test]
        public void VisiblePlanetsTest()
        {
            var constants = new Constants();
            var player = new Player();
            var camera = new CameraMock();
            camera.SetupTopFunc(()=> 5);
            camera.SetupBottomFunc(()=> 0);
            camera.SetupLeftFunc(()=>0);
            camera.SetupRightFunc(()=>5);

            var sector = new SectorMock();
            sector.SetupGetX(()=>0);
            sector.SetupGetY(()=>0);
            sector.SetupGetPlanetData((i) => new PlanetData(2, 2, 5000));
            
            var sectorStore = new ISector[] {sector};

            var visiblePlanetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var listPanetData = new List<PlanetData>();
            visiblePlanetProvider.GetVisiblePlanets(listPanetData, sectorStore);

            Assert.AreEqual(listPanetData.Count, constants.GetPlanetsToVisualize());
        }

        [Test]
        public void VisiblePlanetsZeroTest()
        {
            var constants = new Constants();
            var player = new Player();
            var camera = new CameraMock();
            camera.SetupTopFunc(() => 5);
            camera.SetupBottomFunc(() => 0);
            camera.SetupLeftFunc(() => 0);
            camera.SetupRightFunc(() => 5);

            var sector = new SectorMock();
            sector.SetupGetX(() => 0);
            sector.SetupGetY(() => 0);
            sector.SetupGetPlanetData((i) => new PlanetData(6, 6, 5000));

            var sectorStore = new ISector[] { sector };

            var visiblePlanetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var listPanetData = new List<PlanetData>();
            visiblePlanetProvider.GetVisiblePlanets(listPanetData, sectorStore);

            Assert.AreEqual(listPanetData.Count, 0);
        }

        [Test]
        public void VisiblePlanetsFewTest()
        {
            var constants = new Constants();
            var player = new Player();
            var camera = new CameraMock();
            camera.SetupTopFunc(() => 5);
            camera.SetupBottomFunc(() => 0);
            camera.SetupLeftFunc(() => 0);
            camera.SetupRightFunc(() => 5);

            var sector = new SectorMock();
            sector.SetupGetX(() => 0);
            sector.SetupGetY(() => 0);
            sector.SetupGetPlanetData((i) => {
                if (i < 5)
                {
                    return new PlanetData(0, 0, 4000);
                }
                return new PlanetData(100, 100, 1000);
            });

            var sectorStore = new ISector[] { sector };

            var visiblePlanetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var listPanetData = new List<PlanetData>();
            visiblePlanetProvider.GetVisiblePlanets(listPanetData, sectorStore);

            Assert.AreEqual(listPanetData.Count, 5);
        }
    }
}
