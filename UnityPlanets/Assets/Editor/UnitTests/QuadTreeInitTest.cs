using System.Diagnostics.CodeAnalysis;
using Assets.Scripts;
using Moq;
using NUnit.Framework;
using QuadTree;

namespace Editor.UnitTests
{
    [TestFixture]
    [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    public sealed class QuadTreeInitTest
    {
        [Test]
        public void InitTest()
        {
            var constants = new Mock<IConstants>();
            constants.Setup(c => c.GetSectorSideSize()).Returns(100);
            constants.Setup(c => c.GetMaxCameraSize()).Returns(1000);
            var startNodeCreator = new StartNodeCreator(constants.Object, new ThreadQuadTreeNodeCreatorFactory(constants.Object), new QuadTreeNodeMerger());
            var rootNode = startNodeCreator.Create();
            var nodeVisitor = new VisibleNodesCollector();
            rootNode.VisitVisibleNodes(rootNode.GetAABBox(), nodeVisitor);

            var generatedLeafs = nodeVisitor.GetVisibleLeaves();

            Assert.NotNull(generatedLeafs);
            Assert.AreEqual(256, generatedLeafs.Count);
        }
    }
}
