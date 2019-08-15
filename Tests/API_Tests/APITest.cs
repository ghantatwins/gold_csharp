using System;
using Gold_API;
using NUnit.Framework;

namespace API_Tests
{
    [TestFixture]
    public class ApiTest
    {
        private Api _api;

        [SetUp]
        public void Setup()
        {
            _api = new Api();
        }

        [Test]
        public void FirstRecordTest()
        {
            double expected = 34.73;
            double actual = _api.GetPrice(0);
            Assert.AreEqual(expected,actual);
        }
        [Test]
        public void FirstRecordByDateTest()
        {
            double expected = 34.73;
            double actual = _api.GetPrice(new DateTime(1950, 1, 1));
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void LastRecordTest()
        {
            double expected = 1414.611;
            double actual = _api.GetPrice(835);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void LastRecordByDateTest()
        {
            double expected = 1414.611;
            double actual = _api.GetPrice(new DateTime(2019, 7, 1));
            Assert.AreEqual(expected, actual);
        }
    }
}
