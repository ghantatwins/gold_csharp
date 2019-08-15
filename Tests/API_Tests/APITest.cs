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
        public void LastRecordTest()
        {
            double expected = 1414.611;
            double actual = _api.GetPrice(835);
            Assert.AreEqual(expected, actual);
        }
    }
}
