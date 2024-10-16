using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TD1.Models.Repository;
using TD1.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TD1.Controllers.Tests
{
    [TestClass()]
    public class MarquesControllerTests
    {

        private MarquesController marquesController;
        private ProdDBContext context;
        private IDataRepository<Marque> dataRepository;

        [TestMethod()]
        public void PostMarquesTest()
        {

            // Arrange

            Marque marque = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1"
            };

            var mockRepository = new Mock<IDataRepository<Marque>>();
            var userController = new MarquesController(mockRepository.Object);
            Assert.Fail();

            // Act
            var actionResult = marquesController.PostMarque(marque).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Marque>), "Pas instance de ActionResult<Marque>");
        }
    }
}