using System.Collections;
using System.Collections.Generic;
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
        var player = new Player();
        var planetFactoryCreator = new PlanetFactoryCreator(player, constants);

        var quadCreator = new StartNodeCreator(constants, player, planetFactoryCreator);
        var rootNode = quadCreator.Create();

        var cameraBox = new AABBox(0f, 0f, 100f, 100f);
        var visiblePlanets = new List<PlanetData>();
        rootNode.GetVisiblePlanets(cameraBox, visiblePlanets);
    }
}
