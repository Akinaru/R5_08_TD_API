using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TD1.Models.EntityFramework;
using Moq;
using TD1.Models.Repository;
using Microsoft.EntityFrameworkCore;
using TD1.Models.DataManager;

namespace TD1.Controllers.Tests
{
    [TestClass()]
    public class MarquesControllerTests
    {
        private ProdDBContext context;

        private IDataRepository<Marque> dataRepository;


        [TestMethod()]
        public void MarquesControllerTest()
        {

            var builder = new DbContextOptionsBuilder<ProdDBContext>().UseNpgsql("Server=localhost;port=5432;Database=TD1;uid=postgres;password=postgres;");
            context = new ProdDBContext(builder.Options);
            dataRepository = new MarqueManager(context);

        }

        [TestMethod()]
        public void GetMarqueById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Marque marque = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(marque);

            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.GetMarqueById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(marque, actionResult.Value as Marque);
        }

        [TestMethod]
        public void GetMarqueById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marqueController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueController.GetMarqueById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostMarque_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Marque>>();
            var marqueController = new MarquesController(mockRepository.Object);

            Marque marque = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1"
            };

            // Act
            var actionResult = marqueController.PostMarque(marque).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Marque>), "Pas un ActionResult<Marque>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Marque), "Pas une Marque");
            marque.idMarque = ((Marque)result.Value).idMarque;
            Assert.AreEqual(marque, (Marque)result.Value, "Marques pas identiques");
        }

        [TestMethod]
        public void DeleteMarqueTest_AvecMoq()
        {
            // Arrange
            Marque marque = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1"
            };

            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(marque);
            var marqueControler = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = marqueControler.DeleteMarque(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PutMarque_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            Marque marque = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1"
            };
            Marque marqueUpdated = new Marque
            {
                idMarque = 1,
                nomMarque = "Marque1 Updated"
            };
            var mockRepository = new Mock<IDataRepository<Marque>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(marque);
            var userController = new MarquesController(mockRepository.Object);

            // Act
            var actionResult = userController.PutMarque(marqueUpdated.idMarque, marqueUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}