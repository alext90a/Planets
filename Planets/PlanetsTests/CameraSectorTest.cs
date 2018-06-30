using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Planets;

namespace PlanetsTests
{
    [TestFixture]
    public class CameraSectorTest
    {
        [Test]
        public void CameraInsideSectorTest()
        {
            var segment = new Segment(Mock.Of<ISectorCreator>(), new Constants());
            var camera = new Mock<ICamera>();
            camera.Setup(c => c.Top).Returns(155);
            camera.Setup(c => c.Bottom).Returns(145);
            camera.Setup(c => c.Left).Returns(145);
            camera.Setup(c => c.Right).Returns(155);

            var res = segment.IsCameraInercectSector(camera.Object, 1, 1);
            
            Assert.IsTrue(res);
        }

        [Test]
        public void SectorInsideCameraTest()
        {
            var segment = new Segment(Mock.Of<ISectorCreator>(), new Constants());
            var camera = new Mock<ICamera>();
            camera.Setup(c => c.Top).Returns(300);
            camera.Setup(c => c.Bottom).Returns(0);
            camera.Setup(c => c.Left).Returns(0);
            camera.Setup(c => c.Right).Returns(300);

            var res = segment.IsCameraInercectSector(camera.Object, 1, 1);

            Assert.IsTrue(res);
        }

        [Test]
        public void SectorIntersectCameraTest()
        {
            var segment = new Segment(Mock.Of<ISectorCreator>(), new Constants());
            var camera = new Mock<ICamera>();
            camera.Setup(c => c.Top).Returns(300);
            camera.Setup(c => c.Bottom).Returns(150);
            camera.Setup(c => c.Left).Returns(150);
            camera.Setup(c => c.Right).Returns(300);

            var res = segment.IsCameraInercectSector(camera.Object, 1, 1);

            Assert.IsTrue(res);
        }

        [Test]
        public void SectorNotIntersectCameraTest()
        {
            float data1 = 49.9f;
            int data2 = (int)data1;
            var segment = new Segment(Mock.Of<ISectorCreator>(), new Constants());
            var camera = new Mock<ICamera>();
            camera.Setup(c => c.Top).Returns(299);
            camera.Setup(c => c.Bottom).Returns(200);
            camera.Setup(c => c.Left).Returns(200);
            camera.Setup(c => c.Right).Returns(299);

            var res = segment.IsCameraInercectSector(camera.Object, 1, 1);

            Assert.IsFalse(res);
        }
    }
}
