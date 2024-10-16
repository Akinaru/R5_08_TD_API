using Microsoft.VisualStudio.TestTools.UnitTesting;
using TD1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using TD1.Models.DataManager;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Controllers.Tests
{
    [TestClass()]
    public class TypeProduitsControllerTests
    {
        private ProdDBContext context;

        private IDataRepository<TypeProduit> dataRepository;


        [TestMethod()]
        public void TypeProduitsControllerTest()
        {

            var builder = new DbContextOptionsBuilder<ProdDBContext>().UseNpgsql("Server=localhost;port=5432;Database=TD1;uid=postgres;password=postgres;");
            context = new ProdDBContext(builder.Options);
            dataRepository = new TypeProduitManager(context);

        }

        [TestMethod()]
        public void GetTypeProduitById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit
            {
                idTypeProduit = 1,
                nomTypeProduit = "TypeProduit1"
            };
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeProduit);

            var typeProduitController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitController.GetTypeProduitById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(typeProduit, actionResult.Value as TypeProduit);
        }

        [TestMethod]
        public void GetTypeProduitById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var typeProduitController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitController.GetTypeProduitById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostTypeProduit_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            var typeProduitController = new TypeProduitsController(mockRepository.Object);

            TypeProduit typeProduit = new TypeProduit
            {
                idTypeProduit = 1,
                nomTypeProduit = "TypeProduit1"
            };

            // Act
            var actionResult = typeProduitController.PostTypeProduit(typeProduit).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<TypeProduit>), "Pas un ActionResult<TypeProduit>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(TypeProduit), "Pas une TypeProduit");
            typeProduit.idTypeProduit = ((TypeProduit)result.Value).idTypeProduit;
            Assert.AreEqual(typeProduit, (TypeProduit)result.Value, "TypeProduits pas identiques");
        }

        [TestMethod]
        public void DeleteTypeProduitTest_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit
            {
                idTypeProduit = 1,
                nomTypeProduit = "TypeProduit1"
            };

            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeProduit);
            var typeProduitControler = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = typeProduitControler.DeleteTypeProduit(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PutTypeProduit_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            TypeProduit typeProduit = new TypeProduit
            {
                idTypeProduit = 1,
                nomTypeProduit = "TypeProduit1"
            };
            TypeProduit typeProduitUpdated = new TypeProduit
            {
                idTypeProduit = 1,
                nomTypeProduit = "TypeProduit1 Updated"
            };
            var mockRepository = new Mock<IDataRepository<TypeProduit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(typeProduit);
            var userController = new TypeProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.PutTypeProduit(typeProduitUpdated.idTypeProduit, typeProduitUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}