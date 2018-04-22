using FlightOperation.API.Interface;
using FlightOperation.API.Model;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FlightOperation.API.Controllers
{
    /// <summary>
    /// flight controller for flight related endpoints
    /// </summary>
    [RoutePrefix("api/bookings")]
    public class BookingsController : ApiController
    {
        private readonly IBookingManager bookingManager;
        public BookingsController(IBookingManager bookingManager)
        {
            this.bookingManager = bookingManager;
        }


        /// <summary>
        /// Make a booking
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpPost]
        [ResponseType(typeof(BookingResponse))]

        public async Task<IHttpActionResult> MakeBooking(CreateBookingRequestDeatils booking)
        {

            var result = await bookingManager.MakeBooking(booking);
            if (result != null && result.Item1 == HttpStatusCode.OK)
                return Ok(new BookingResponse { PNR = result.Item2 });
            else
                return BadRequest(result.Item2);
        }


        /// <summary>
        /// Search a booking
        /// </summary>
        /// <returns></returns>
        [Route("search")]
        [HttpPost]
        [ResponseType(typeof(List<Booking>))]
        public async Task<IHttpActionResult> SearchBooking(SearchBookingModel booking)
        {

            var result = await bookingManager.SearchBooking(booking);
            if (result != null && result.Count > 0)
                return Ok(result);
            else
                return NotFound();
        }

    }
}
