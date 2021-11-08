using Microsoft.VisualStudio.TestTools.UnitTesting;
using TVRemote.models;
using System.Threading;

namespace TvRemote.UnitTests
{
    [TestClass]
    public class RemoteTests
    {
        [TestMethod]
        public void TestPowerOnTv()
        {
            // Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            
            //Act
            remote.PressButton(ButtonName.Power);

            //Assert
            Assert.IsTrue(tv.Powered);
        }

        [TestMethod]
        public void TestPowerOffTv()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.Power);

            //Assert
            Assert.IsFalse(tv.Powered);
        }

        [TestMethod]
        public void TestVolumeUp()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.VolumeUp);

            //Assert
            Assert.AreEqual(tv.CurrentVolume, 21);
        }

        [TestMethod]
        public void TestVolumeDown()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.VolumeDown);

            //Assert
            Assert.AreEqual(tv.CurrentVolume, 19);
        }

        [TestMethod]
        public void TestChannelUp()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.ChannelUp);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 2);
        }

        [TestMethod]
        public void TestChannelDown()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            tv.CurrentChannel = 2;

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.ChannelDown);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 1);
        }

        [TestMethod]
        public void TestVolumeUpAboveMax()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            tv.CurrentVolume = 60;
            remote.PressButton(ButtonName.VolumeUp);

            //Assert
            Assert.AreEqual(tv.CurrentVolume, 60);
        }

        [TestMethod]
        public void TestVolumeDownUnderMin()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);

            //Act
            remote.PressButton(ButtonName.Power);
            tv.CurrentVolume = 0;
            remote.PressButton(ButtonName.VolumeDown);

            //Assert
            Assert.AreEqual(tv.CurrentVolume, 0);
        }

        [TestMethod]
        public void TestChannelUpAboveMax()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            tv.CurrentChannel = 999;

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.ChannelUp);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 1);
        }

        [TestMethod]
        public void TestChannelDownUnderMin()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            tv.CurrentChannel = 1;

            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.ChannelDown);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 999);
        }

        [TestMethod]
        public void TestGoToChannelWithOneNumber()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.WaitTime = 30;
            //Act
            remote.PressButton(ButtonName.Power);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(50);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 4);
        }

        [TestMethod]
        public void TestGoToChannelWithTwoNumbers()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.WaitTime = 30;
            //Act
            remote.PressButton(ButtonName.Power);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(50);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 46);
        }

        [TestMethod]
        public void TestGoToChannelWithThreeNumbers()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.WaitTime = 30;
            //Act
            remote.PressButton(ButtonName.Power);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(50);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 466);
        }

        [TestMethod]
        public void TestGoToChannelWithFourNumbers()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.WaitTime = 30;
            //Act
            remote.PressButton(ButtonName.Power);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(50);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 999);
        }

        [TestMethod]
        public void TestGoToChannelWithNonNumber()
        {
            //Arrange
            var tv = new Tv();
            var remote = new Remote(tv);
            remote.WaitTime = 30;
            //Act
            remote.PressButton(ButtonName.Power);
            remote.PressButton(ButtonName.Four);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.VolumeDown);
            Thread.Sleep(5);
            remote.PressButton(ButtonName.Six);
            Thread.Sleep(50);

            //Assert
            Assert.AreEqual(tv.CurrentChannel, 466);
        }
    }
}
