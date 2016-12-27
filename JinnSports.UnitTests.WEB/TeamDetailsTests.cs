using System;
using JinnSports.WEB.Controllers;
using System.Web.Mvc;
using NUnit.Framework;
using JinnSports.WEB;

namespace JinnSports.UnitTests.WEB
{
    [TestFixture]
    public class TeamDetailsTest
    {
        [SetUp]
        public void Init()
        {
            AutoMapperConfiguration.Configure();
        }
        [Test]
        public void Details([Values(0, 1)] int id)
        {
            // Arrange
            TeamDetailsController controller = new TeamDetailsController();

            // Act
            ActionResult result = controller.Details(id);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
