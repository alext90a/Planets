using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using NUnit.Framework;
using Planets;
using QuadTree;

public sealed class QuadTreeCreatorTest
{
    [Test]
    public void CreatorTest()
    {
        var constants = new Constants();
        var player = new Player(constants);
        var planetFactoryCreator = new PlanetFactoryCreator(player, constants);

        var quadCreator = new StartNodeCreator(constants, player, planetFactoryCreator);
        var rootNode = quadCreator.Create();

        var cameraBox = new AABBox(0f, 0f, 100f, 100f);
        var visiblePlanets = new List<PlanetData>();
        Assert.AreEqual(constants.GetPlanetsToVisualize(), visiblePlanets.Count);
    }


    [Test]
    public void PerformanceTest()
    {
        var constants = new Constants();
        var player = new Player(constants);
        var planetFactoryCreator = new PlanetFactoryCreator(player, constants);
        var camera = new Camera();
        camera.IncreaseZoom();
        camera.IncreaseZoom();
        var visiblePlanets2 = new List<PlanetData>();
        var cameraBox = new AABBox(0f, 0f, 100f, 100f);
        var visiblePlanets = new List<PlanetData>();

        var timeNodeCreator = Stopwatch.StartNew();
        var quadCreator = new StartNodeCreator(constants, player, planetFactoryCreator);
        var rootNode = quadCreator.Create();
        timeNodeCreator.Stop();


        var timerSegmentCreator = Stopwatch.StartNew();
        var segment = new SegmentCreator(constants, player);
        var allSectors = segment.CreateSectors();
        timerSegmentCreator.Stop();


        var timerTreeSearcher = Stopwatch.StartNew();
        timerTreeSearcher.Stop();

        var timerSegmentSearcher = Stopwatch.StartNew();
        timerSegmentSearcher.Stop();
        
        
        Assert.AreEqual(20, visiblePlanets.Count);
        Assert.AreEqual(20, visiblePlanets2.Count);

        //for (int i = 0; i < visiblePlanets.Count; ++i)
        //{
        //    var planetData1 = visiblePlanets[i];
        //    var planetData2 = visiblePlanets2[i];
        //    Assert.AreEqual(planetData2.Score, planetData1.Score);
        //    Assert.AreEqual(planetData2.X, planetData1.X);
        //    Assert.AreEqual(planetData2.Y, planetData1.Y);
        //}
    }
}
