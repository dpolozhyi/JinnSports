using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JinnSports.UnitTests.Services
{
    [TestFixture]
    public class EventsServiceTests
    {
        [OneTimeSetUp]
        public void Init()
        {
            
        }

        [OneTimeTearDown]
        public void Clean()
        {

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void TestCount(int sportId)
        {

        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void TestGetAllSportEvents(int sportId)
        {

        }

        [Test]
        public void TestGetSportEvents(int sportId, int skip, int take)
        {

        }
    }
}
