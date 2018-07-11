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
            sector.SetupGetPlanetAmount(()=>constants.GetPlanetsInSector());
            
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
            sector.SetupGetPlanetAmount(() => constants.GetPlanetsInSector());

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
            sector.SetupGetPlanetAmount(() => constants.GetPlanetsInSector());
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

        [Test]
        public void PlanetFrom4Sectors()
        {
            var constants = new Constants();
            var player = new Player();
            var camera = new CameraMock();
            camera.SetupTopFunc(() => 2);
            camera.SetupBottomFunc(() => -2);
            camera.SetupLeftFunc(() => -2);
            camera.SetupRightFunc(() => 2);

            var sectorAmount = 4;
            var sectors = new List<ISector>(sectorAmount);
            var sectorWidth = sectorAmount / 2;
            var sectorHalfWidth = sectorWidth / 2;
            for (int y = -1; y < 1; ++y)
            {
                for (int x = -1; x < 1; ++x)
                {
                    var rawData = new int[constants.GetCellsInSector()];
                    for (int k = 0; k < constants.GetCellsInSector(); ++k)
                    {
                        rawData[k] = k * constants.GetCellsInSector() + k;
                    }
                    var createdSector = new Sector(constants, x, y, rawData);
                    sectors.Add(createdSector);
                }
                
            }


            var visiblePlanetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var listPanetData = new List<PlanetData>();
            visiblePlanetProvider.GetVisiblePlanets(listPanetData, sectors.ToArray());

            Assert.AreEqual(constants.GetPlanetsToVisualize(), listPanetData.Count);
        }

        [Test]
        public void PlanetFrom4SectorsPositiveOnly()
        {
            var constants = new Constants();
            var player = new Player();
            var camera = new CameraMock();
            camera.SetupTopFunc(() => 102);
            camera.SetupBottomFunc(() => 98);
            camera.SetupLeftFunc(() => 98);
            camera.SetupRightFunc(() => 102);

            var sectorAmount = 4;
            var sectors = new List<ISector>(sectorAmount);
            var sectorWidth = sectorAmount / 2;
            var sectorHalfWidth = sectorWidth / 2;
            for (int y = 0; y < 1; ++y)
            {
                for (int x = 0; x < 1; ++x)
                {
                    var rawData = new int[constants.GetCellsInSector()];
                    for (int k = 0; k < constants.GetCellsInSector(); ++k)
                    {
                        rawData[k] = k * constants.GetCellsInSector() + k;
                    }
                    var createdSector = new Sector(constants, x, y, rawData);
                    sectors.Add(createdSector);
                }

            }


            var visiblePlanetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var listPanetData = new List<PlanetData>();
            visiblePlanetProvider.GetVisiblePlanets(listPanetData, sectors.ToArray());

            Assert.AreEqual(4, listPanetData.Count);
        }
    }
}
