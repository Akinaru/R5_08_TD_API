using Microsoft.AspNetCore.Mvc;
using Moq;
using TD1.Models.EntityFramework;
using TD1.Models.Repository;

namespace TD1.Controllers.Tests
{
    [TestClass()]
    public class ProduitsControllerTests
    {

        private ProduitsController produitsController;
        private ProdDBContext context;
        private IDataRepository<Produit> dataRepository;

        //Arange
        //Act
        //Assert

        [TestMethod()]
        public void ProduitsControllerTest()
        {
            
            // Arrange
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var produitController = new ProduitsController(mockRepository.Object);
        }

        [TestMethod()]
        public void GetProduitsTest()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Produit>>();
            var produitController = new ProduitsController(mockRepository.Object);

            Produit p = new Produit
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
            var actionResult = produitController.GetProduit(1).Result;

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Produit>), "Pas un ActionResult<Produit>");
        }

        [TestMethod()]
        public void GetProduitTest()
        {
        }

        [TestMethod()]
        public void PutProduitTest()
        {
        }

        [TestMethod()]
        public void PostProduitTest()
        {
        }

        [TestMethod()]
        public void DeleteProduitTest()
        {
        }
    }
}