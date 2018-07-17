using Assets.Scripts;
using Editor.Mocks;
using NUnit.Framework;
using QuadTree;

namespace Editor.UnitTests
{
    [TestFixture]
    public sealed class QuadTreeInitTest
    {
        [Test]
        public void InitTest()
        {
            var constants = new ConstantsMock();
            constants.SetupGetSectorSideSize(()=>100);
            constants.SetupGetMaxCameraSize(()=>1000);

            var startNodeCreator = new StartNodeCreator(constants);
            var rootNode = startNodeCreator.Create();

            var nodeVisitor = new VisibleNodesCollector();
            rootNode.VisitVisibleNodes(rootNode.GetAABBox(), nodeVisitor);
            var generatedLeafs = nodeVisitor.GetVisibleLeaves();
            Assert.NotNull(generatedLeafs);
            Assert.AreEqual(256, generatedLeafs.Count);
        }
    }
}
