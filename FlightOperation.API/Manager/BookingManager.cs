using FlightOperation.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightOperation.API.Model;
using System.Threading.Tasks;
using System.Net;
using Dapper;

namespace FlightOperation.API.Manager
{
    public class BookingManager : IBookingManager
    {
        private IDbManager dbManager;
        private IFlightManager flightManager;
        private IPassengerManager passengerManager;

        /// <summary>
        /// BookingManager ctor
        /// </summary>
        /// <param name="dbManager"></param>
        /// <param name="flightManager"></param>
        /// <param name="passengerManager"></param>
        public BookingManager(IDbManager dbManager, IFlightManager flightManager, IPassengerManager passengerManager)
        {
            this.dbManager = dbManager;
            this.flightManager = flightManager;
            this.passengerManager = passengerManager;
        }

        /// <summary>
        /// Make Booking
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        public async Task<Tuple<HttpStatusCode, string>> MakeBooking(CreateBookingRequestDeatils booking)
        {
            if (booking.FlightDetails != null && !String.IsNullOrEmpty(booking.FlightDetails.DepartureDate)
                && !String.IsNullOrEmpty(booking.FlightDetails.ArrivalDate)
                && !String.IsNullOrEmpty(booking.FlightDetails.DepartureCityCode)
                && !String.IsNullOrEmpty(booking.FlightDetails.ArrivalCityCode)
                && !String.IsNullOrEmpty(booking.FlightDetails.DepartureTime)
                && !String.IsNullOrEmpty(booking.FlightDetails.ArrivalTime))
            {
                var flightDetails = await flightManager.GetFlightDetails(booking.FlightDetails);
                if (flightDetails != null && booking.Passenger != null && booking.Passenger.Count() > 0)
                {
                    var passengerCount = booking.Passenger.Count();
                    if (flightDetails.Capacity > booking.Passenger.Count())
                    {
                        var pnr = String.Format("{0:000000}", new Random().Next(000000, 999999));
                        var status = await UpdateBooking(pnr, flightDetails, passengerCount);
                        if (status)
                        {
                            foreach (Passenger passenger in booking.Passenger)
                            {
                                var passengerId = await passengerManager.CreatePassenger(passenger);
                                await passengerManager.UpdatePassengerBookingRecord(passengerId, pnr);
                            }
                            return new Tuple<HttpStatusCode, string>(HttpStatusCode.OK, pnr);
                        }
                        else
                            return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, "something went wrong while creating booking");
                    }
                    else
                        return new Tuple<HttpStatusCode, string>(HttpStatusCode.NotFound, "capacity exceeded");
                }
                else
                    return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, "flight details not valid");
            }
            else
            {
                return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, "Supplied flight details are not correct");
            }
        }

        /// <summary>
        /// Update Booking
        /// </summary>
        /// <param name="pnr"></param>
        /// <param name="flightDetails"></param>
        /// <param name="passengerCount"></param>
        /// <returns></returns>
        private async Task<bool> UpdateBooking(string pnr, FlightDetail flightDetails, int passengerCount)
        {
            var sql = @"
                        Update [flightbooking].[dbo].[flightdetail] set capacity= @capacity where id= @fid;

                        INSERT INTO [flightbooking].[dbo].[booking] 
                        VALUES (@pnr,GETUTCDATE(),GETUTCDATE(),@dcode,@acode,@fn);

                        SELECT CAST(SCOPE_IDENTITY() as INT);

                        ";

            using (var db = dbManager.GetOpenConnection())
            {
                var result = (await db.QueryAsync<int>(new CommandDefinition(sql, new
                {
                    pnr = pnr,
                    dcode = flightDetails.DepartureCityCode,
                    acode = flightDetails.ArrivalCityCode,
                    fn = flightDetails.FlightNumber,
                    capacity = flightDetails.Capacity - passengerCount,
                    fid = flightDetails.Id
                }))).SingleOrDefault() > 0;

                return result;
            }
        }

        /// <summary>
        /// Search Booking
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public async Task<List<Booking>> SearchBooking(SearchBookingModel search)
        {
            var sql = @"SELECT * FROM [flightbooking].[dbo].[vwbookingdetails]
                        WHERE pnr = @pnr or firstname= @fname or lastname = @lname or flight_number = @fn
                        or departure_city_code = @dcc or arrival_city_code = @acc
                        or departure_city_name = @dcn or arrival_city_name = @acn or convert(date,booking_date)  = @bdate";

            using (var db = dbManager.GetOpenConnection())
            {
                var results = await db.QueryAsync<Booking>(new CommandDefinition(sql, new
                {
                    pnr = search.PNR,
                    fname = search.FirstName,
                    lname = search.LastName,
                    fn = search.FlightNumber,
                    dcc = search.DepartureCityCode,
                    acc = search.ArrivalCityCode,
                    dcn = search.DepartureCityName,
                    acn = search.ArrivalCityName,
                    bdate = search.BookingDate
                }));
                if (results != null && results.Count() > 0)
                    return results.ToList();
                else return null;
            }
        }
    }
}