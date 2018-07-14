using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.QuadTree;
using NUnit.Framework;
using Planets;
using UnityEngine;

public class QuadTreeCreatorTest
{
    [Test]
    public void CreatorTest()
    {
        var constants = new Constants();
        var player = new Player();

        var quadCreator = new StartNodeCreator(constants, player);
        var rootNode = quadCreator.Create();

        var cameraBox = new AABBox(0f, 0f, 100f, 100f);
        var visiblePlanets = new List<PlanetData>();
        rootNode.GetVisiblePlanets(cameraBox, visiblePlanets);
    }
}
