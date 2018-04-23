using FlightOperation.API.Interface;
using FlightOperation.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace FlightOperation.API.Controllers
{
    /// <summary>
    /// flight controller for flight related endpoints
    /// </summary>
    [RoutePrefix("api/flights")]
    public class FlightsController : ApiController
    {
        private readonly IFlightManager flightManager;
        public FlightsController(IFlightManager flightManager)
        {
            this.flightManager = flightManager;
        }


        /// <summary>
        /// Get All Flight List
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<FlightDetail>))]

        public async Task<IHttpActionResult> GetAllFlightList()
        {

            var flightList = await flightManager.GetAllFlightsList();
            if (flightList != null)
                return Ok(flightList);
            else
                return NotFound();
        }

        /// <summary>
        /// Get Flight List For Today
        /// </summary>
        /// <returns></returns>
        [Route("today")]
        [HttpGet]
        [ResponseType(typeof(List<FlightDetail>))]
        public async Task<IHttpActionResult> GetAllFlightListForToday()
        {
            var flightList = await flightManager.GetFlightList(DateTime.UtcNow);
            if (flightList != null)
                return Ok(flightList);
            else
                return NotFound();
        }

        /// <summary>
        /// Get flight list for the given date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ResponseType(typeof(List<FlightDetail>))]
        public async Task<IHttpActionResult> GetFlightList(DateTime date)
        {

            var flightList = await flightManager.GetFlightList(date);
            if (flightList != null)
                return Ok(flightList);
            else
                return NotFound();
        }

        /// <summary>
        /// Get Availbility of flight between given date and passenger
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        [Route("checkavailbility")]
        [HttpGet]
        [ResponseType(typeof(List<FlightDetail>))]

        public async Task<IHttpActionResult> CheckAvailbilityOfFlight(DateTime startDate, DateTime endDate, int passengerCount)
        {

            var flightList = await flightManager.CheckAvailbilityOfFlight(startDate, endDate, passengerCount);
            if (flightList != null && flightList.Count() > 0)
                return Ok(flightList);
            else
                return NotFound();
        }
    }
}
