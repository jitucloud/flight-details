using FlightOperation.API.Controllers;
using FlightOperation.API.Interface;
using FlightOperation.API.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;

namespace FlightOperation.API.Tests.Controller
{
    [TestClass]
    public class BookingControllerTest
    {
        [TestMethod]
        public void BookingController_CheckForValidPNR_Are_Equal()
        {
            //Arrange
            var mockRepository = new Mock<IBookingManager>();
            CreateBookingRequestDeatils moqdata = new CreateBookingRequestDeatils();
            mockRepository.Setup(x => x.MakeBooking(moqdata)).ReturnsAsync(new Tuple<HttpStatusCode, string>(HttpStatusCode.OK, "27373"));
            var controller = new BookingsController(mockRepository.Object);

            //Act
            var result = controller.MakeBooking(moqdata);
            var contentResult = result.Result as OkNegotiatedContentResult<BookingResponse>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.PNR, "27373");
        }

        [TestMethod]
        public void BookingController_NotValidFlightDetail_BAD_REQUEST()
        {
            //Arrange
            var mockRepository = new Mock<IBookingManager>();
            CreateBookingRequestDeatils moqdata = new CreateBookingRequestDeatils();
            moqdata.FlightDetails = new FlightDetailRequest
            {
                DepartureCityCode = "DXB",
                ArrivalCityCode = ""

            };

            mockRepository.Setup(x => x.MakeBooking(moqdata)).ReturnsAsync(new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, "bad request"));

            var controller = new BookingsController(mockRepository.Object);

            //Act
            var result = controller.MakeBooking(moqdata);
            var contentResult = result.Result as BadRequestErrorMessageResult;

            //Assert
            Assert.IsInstanceOfType(contentResult, typeof(BadRequestErrorMessageResult));
            Assert.AreEqual(contentResult.Message, "bad request");
        }


        [TestMethod]
        public void BookingController_SEARCHBOOKING_IS_NOT_NUll()
        {
            //Arrange
            var mockRepository = new Mock<IBookingManager>();
            SearchBookingModel moqdata = new SearchBookingModel();
            List<Booking> returnData = new List<Booking>();
            returnData.Add(new Booking()
            {
                DepartureCityCode = "DXB",
                ArrivalCityCode = "DEL",
                FlightNumber = "101",
            });

            mockRepository.Setup(x => x.SearchBooking(moqdata)).ReturnsAsync(returnData);
            var controller = new BookingsController(mockRepository.Object);

            //Act
            var result = controller.SearchBooking(moqdata);
            var contentResult = result.Result as OkNegotiatedContentResult<List<Booking>>;

            //Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(contentResult.Content.Count, 1);
            Assert.AreEqual(contentResult.Content[0].DepartureCityCode, "DXB");
        }

        [TestMethod]
        public void BookingController_SEARCHBOOKING_IS_NOT_NUll_NOT_FOUND()
        {
            //Arrange
            var mockRepository = new Mock<IBookingManager>();
            SearchBookingModel moqdata = new SearchBookingModel();
            List<Booking> returnData = new List<Booking>();        
            mockRepository.Setup(x => x.SearchBooking(moqdata)).ReturnsAsync(returnData);
            var controller = new BookingsController(mockRepository.Object);

            //Act
            var result = controller.SearchBooking(moqdata);
            var contentResult = result.Result as NotFoundResult;

            //Assert
            Assert.IsInstanceOfType(contentResult, typeof(NotFoundResult));
        }

    }
}
