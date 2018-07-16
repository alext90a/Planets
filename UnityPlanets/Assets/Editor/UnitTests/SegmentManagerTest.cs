using System;
using System.Collections.Generic;
using Assets.Scripts;
using NUnit.Framework;
using Planets;

namespace Editor.UnitTests
{

    public class SegmentManagerTest
    {

        [Test]
        public void GetVisiblePlanetsTest()
        {
            var constants = new Constants();
            var player = new Player(constants);
            var planetFactory = new PlanetFactory(constants, new Random(0), new PlanetComparer(player, constants));
            var segmentCreator = new SegmentCreatorMock();
            
            segmentCreator.SetupCreateSectors(() =>
            {
                return new Sector[]
                {
                    new Sector(constants, -1, -1, planetFactory.CreatePlanetsForSector()),
                    new Sector(constants, -1, 0, planetFactory.CreatePlanetsForSector()),
                    new Sector(constants, 0, 1, planetFactory.CreatePlanetsForSector()),
                    new Sector(constants, 0, 0, planetFactory.CreatePlanetsForSector()),
                };
            });


            var camera = new CameraMock();
            camera.SetupBottomFunc(() => -4);
            camera.SetupTopFunc(() => 5);
            camera.SetupLeftFunc(()=> -1);
            camera.SetupRightFunc(() => 8);

            var planetProvider = new VisiblePlanetsProvider(camera, constants, player);

            var sectorManager = new SectorManager(segmentCreator, constants, camera, player, planetProvider);
            sectorManager.Init();

            var listVisiblePlanets = new List<PlanetData>();
            sectorManager.GetVisiblePlanets(listVisiblePlanets);

        }
    }
}

