using FlightOperation.API.Controllers;
using FlightOperation.API.Interface;
using FlightOperation.API.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace FlightOperation.API.Tests.Controller
{
    [TestClass]
    public class FlightsControllerTest
    {
        [TestMethod]
        public void FlightsController_GetTheListOFALLFlights_ARE_TRUE()
        {
            //Arrange
            var mockRepository = new Mock<IFlightManager>();
            List<FlightDetail> listFD = new List<FlightDetail>();
            listFD.Add(new FlightDetail()
            {
                DepartureCityCode = "DXB",
                ArrivalCityCode = "DEL",
                FlightNumber = "101",
                Capacity = 20

            });

            listFD.Add(new FlightDetail()
            {
                DepartureCityCode = "MEL",
                ArrivalCityCode = "SYD",
                FlightNumber = "103",
                Capacity = 25
            });

            mockRepository.Setup(x => x.GetAllFlightsList()).ReturnsAsync(listFD);
            var controller = new FlightsController(mockRepository.Object);

            //Act
            var result = controller.GetAllFlightList();
            var contentResult = result.Result as OkNegotiatedContentResult<List<FlightDetail>>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count, 2);

        }


        [TestMethod]
        public void FlightsController_CheckAvailbilityOfFlight_ARE_TRUE()
        {
            //Arrange
            var mockRepository = new Mock<IFlightManager>();
            DateTime startDate = Convert.ToDateTime("4/24/2018");
            DateTime endDate = Convert.ToDateTime("4/26/2018");

            List<FlightDetail> listFD = new List<FlightDetail>();
            listFD.Add(new FlightDetail()
            {
                DepartureCityCode = "DXB",
                ArrivalCityCode = "DEL",
                FlightNumber = "101",
                Capacity = 10
            });

            listFD.Add(new FlightDetail()
            {
                DepartureCityCode = "MEL",
                ArrivalCityCode = "SYD",
                FlightNumber = "103",
                Capacity = 25
            });


            mockRepository.Setup(x => x.CheckAvailbilityOfFlight(startDate, endDate, 5)).ReturnsAsync(listFD);
            var controller = new FlightsController(mockRepository.Object);

            //Act
            var result = controller.CheckAvailbilityOfFlight(startDate, endDate, 5);
            var contentResult = result.Result as OkNegotiatedContentResult<List<FlightDetail>>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count, 2);

        }


        [TestMethod]
        public void FlightsController_CheckAvailbilityOfFlight_NOT_FOUND()
        {
            //Arrange
            var mockRepository = new Mock<IFlightManager>();
            DateTime startDate = Convert.ToDateTime("4/24/2018");
            DateTime endDate = Convert.ToDateTime("4/26/2018");
            mockRepository.Setup(x => x.CheckAvailbilityOfFlight(startDate, endDate, 5)).ReturnsAsync(new List<FlightDetail>());
            var controller = new FlightsController(mockRepository.Object);

            //Act
            var result = controller.CheckAvailbilityOfFlight(startDate, endDate, 5);            
            var contentResult = result.Result as NotFoundResult;

            //Assert
            Assert.IsInstanceOfType(contentResult, typeof(NotFoundResult));

        }
    }
}
