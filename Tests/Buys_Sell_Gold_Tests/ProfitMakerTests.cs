using System;
using System.Collections.Generic;
using API;
using Buy_Sell_Gold;
using Moq;
using NUnit.Framework;
namespace Buys_Sell_Gold_Tests
{
    [TestFixture]
    public class ProfitMakerTests
    {
        private ProfitMaker _profitMaker;
        private IApi _api;

        [SetUp]
        public void Setup()
        {
            _profitMaker = new ProfitMaker();
            _api = Mock.Of<IApi>();
        }

        [Test]
        public void GetProfitValueTest()
        {
            Dictionary<DateTime,double> fixtures= new Dictionary<DateTime, double>
            {
                {DateTime.Today.AddDays(-4),2  },
                {DateTime.Today.AddDays(-3),4  },
                {DateTime.Today.AddDays(-2),6  },
                {DateTime.Today.AddDays(-1),8  },
                {DateTime.Today.AddDays(-0),10  }
            };
            Mock.Get(_api).Setup(x => x.GetDates()).Returns(fixtures.Keys);
            foreach (var fixture in fixtures)
            {
                Mock.Get(_api).Setup(x => x.GetPrice(fixture.Key)).Returns(fixture.Value);
            }

            double expected = 12;

            double actual = _profitMaker.GetProfitValue(_api);

            Assert.AreEqual(expected,actual);
        }
    }
}
