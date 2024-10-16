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
    public class ProduitsControllerTests
    {
        private ProdDBContext context;

        private IDataRepository<Produit> dataRepository;


        [TestMethod()]
        public void ProduitsControllerTest()
        {

            var builder = new DbContextOptionsBuilder<ProdDBContext>().UseNpgsql("Server=localhost;port=5432;Database=TD1;uid=postgres;password=postgres;");
            context = new ProdDBContext(builder.Options);
            dataRepository = new ProduitManager(context);

        }

        [TestMethod()]
        public void GetProduitById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Produit produit = new Produit
            {
                idProduit = 1,
                nomProduit = "Produit1",
                description = "Description du produit 1",
                nomPhoto = "photo1.jpg",
                uriPhoto = "http://example.com/photo1.jpg",
                idTypeProduit = 1,
                idMarque = 1,
                stockReel = 100,
                stockMin = 10,
                stockMax = 200
            };
            var mockRepository = new Mock<IDataRepository<Produit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(produit);

            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduitById(1).Result;

            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(produit, actionResult.Value as Produit);
        }

        [TestMethod]
        public void GetProduitById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var produitController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitController.GetProduitById(0).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void PostProduit_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var produitController = new ProduitsController(mockRepository.Object);

            Produit produit = new Produit
            {
                idProduit = 1,
                nomProduit = "Produit1",
                description = "Description du produit 1",
                nomPhoto = "photo1.jpg",
                uriPhoto = "http://example.com/photo1.jpg",
                idTypeProduit = 1,
                idMarque = 1,
                stockReel = 100,
                stockMin = 10,
                stockMax = 200
            };

            // Act
            var actionResult = produitController.PostProduit(produit).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Produit>), "Pas un ActionResult<Produit>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Produit), "Pas une Produit");
            produit.idProduit = ((Produit)result.Value).idProduit;
            Assert.AreEqual(produit, (Produit)result.Value, "Produits pas identiques");
        }

        [TestMethod]
        public void DeleteProduitTest_AvecMoq()
        {
            // Arrange
            Produit produit = new Produit
            {
                idProduit = 1,
                nomProduit = "Produit1",
                description = "Description du produit 1",
                nomPhoto = "photo1.jpg",
                uriPhoto = "http://example.com/photo1.jpg",
                idTypeProduit = 1,
                idMarque = 1,
                stockReel = 100,
                stockMin = 10,
                stockMax = 200
            };

            var mockRepository = new Mock<IDataRepository<Produit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(produit);
            var produitControler = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = produitControler.DeleteProduit(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PutProduit_ModelValidated_UpdateOK_AvecMoq()
        {
            // Arrange
            Produit produit = new Produit
            {
                idProduit = 1,
                nomProduit = "Produit1",
                description = "Description du produit 1",
                nomPhoto = "photo1.jpg",
                uriPhoto = "http://example.com/photo1.jpg",
                idTypeProduit = 1,
                idMarque = 1,
                stockReel = 100,
                stockMin = 10,
                stockMax = 200
            };
            Produit produitUpdated = new Produit
            {
                idProduit = 1,
                nomProduit = "Produit1 Updated",
                description = "Description du produit 1",
                nomPhoto = "photo1.jpg",
                uriPhoto = "http://example.com/photo1.jpg",
                idTypeProduit = 1,
                idMarque = 1,
                stockReel = 100,
                stockMin = 10,
                stockMax = 200
            };
            var mockRepository = new Mock<IDataRepository<Produit>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(produit);
            var userController = new ProduitsController(mockRepository.Object);

            // Act
            var actionResult = userController.PutProduit(produitUpdated.idProduit, produitUpdated).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}