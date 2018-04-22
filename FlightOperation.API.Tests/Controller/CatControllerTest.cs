using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleCAT.API.Online.Controllers;
using PeopleCAT.API.Online.Interface;
using System.Web.Http.Results;

namespace FlightOperation.API.Tests.Controller
{
    [TestClass]
    public class CatControllerTest
    {
        [TestMethod]
        public void CatController_GetCatsNameByOwnerGender_OK200Result()
        {
            //Arrange
            var mockRepository = new Mock<ICatManager>();
            var testdata = new { Male = "malecat", Female = "femalecat" };
            mockRepository.Setup(x => x.GetCatNameListByOwnerGender()).ReturnsAsync(testdata);

            var controller = new CatController(mockRepository.Object);
            //Act
            var result = controller.GetCatsNameByOwnerGender();
            var contentResult = result.Result as OkNegotiatedContentResult<object>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content, testdata);

        }

        [TestMethod]
        public void CatController_GetCatsNameByOwnerGender_NOT_FOUND()
        {
            //Arrange
            var mockRepository = new Mock<ICatManager>();
            var controller = new CatController(mockRepository.Object);

            //Act
            var result = controller.GetCatsNameByOwnerGender();

            //Assert
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
