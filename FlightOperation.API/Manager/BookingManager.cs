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
        public BookingManager(IDbManager dbManager, IFlightManager flightManager, IPassengerManager passengerManager)
        {
            this.dbManager = dbManager;
            this.flightManager = flightManager;
            this.passengerManager = passengerManager;
        }
        public async Task<Tuple<HttpStatusCode, string>> MakeBooking(BookingRequestDeatils booking)
        {
            var flightDetails = await flightManager.GetFlightDetails(booking.FlightDetails);
            if (flightDetails != null && booking.Passenger != null && booking.Passenger.Count() > 0)
            {
                var passengerCount = booking.Passenger.Count();
                if (flightDetails.Capacity > booking.Passenger.Count())
                {
                    var pnr = String.Format("{0:0000}", new Random().Next(000000, 999999));
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
            // TODO: Get the flight ID and Capacity details
            // TODO : Generate the PNR if capacity is okay
            // Create an entry into BookingDetails table with PNR , flight number etc.
            // Create an entry into passenger table and get the passenger id and insert into booking detail table against PNR


            throw new NotImplementedException();
        }

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

        public Task<List<Booking>> SearchBooking()
        {
            throw new NotImplementedException();
        }
    }
}