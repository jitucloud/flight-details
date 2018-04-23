using FlightOperation.API.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightOperation.API.Model;
using System.Threading.Tasks;
using Dapper;

namespace FlightOperation.API.Manager
{
    public class FlightManager : IFlightManager
    {
        private IDbManager dbManager;

        /// <summary>
        /// FlightManager ctor
        /// </summary>
        /// <param name="dbManager"></param>
        public FlightManager(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        /// <summary>
        /// Check Availbility Of Flight
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="passengerCount"></param>
        /// <returns></returns>
        public async Task<List<FlightDetail>> CheckAvailbilityOfFlight(DateTime startDate, DateTime endDate, int passengerCount)
        {
            var sql = @"SELECT * FROM [flightbooking].[dbo].[vwflightdetails] 
                        WHERE departure_date BETWEEN  CONVERT (date, @sdate) AND  CONVERT (date, @edate) AND capacity >= @pcount
                        ORDER BY departure_date ASC, departure_time ASC";

            using (var db = dbManager.GetOpenConnection())
            {
                var results = await db.QueryAsync<FlightDetail>(new CommandDefinition(sql, new { sdate = startDate, edate = endDate, pcount = passengerCount }));
                if (results != null && results.Count() > 0)
                    return results.ToList();
                else return null;
            }
        }

        /// <summary>
        /// Get All Flights List
        /// </summary>
        /// <returns></returns>
        public async Task<List<FlightDetail>> GetAllFlightsList()
        {
            var sql = @"SELECT * FROM [flightbooking].[dbo].[vwflightdetails] WHERE departure_date >= CONVERT (date, GETUTCDATE())
                        ORDER BY departure_date ASC, departure_time ASC";

            using (var db = dbManager.GetOpenConnection())
            {
                var results = await db.QueryAsync<FlightDetail>(new CommandDefinition(sql));
                if (results != null && results.Count() > 0)
                    return results.ToList();
                else return null;
            }
        }

        /// <summary>
        /// Get Flight Details
        /// </summary>
        /// <param name="flightDetails"></param>
        /// <returns></returns>
        public async Task<FlightDetail> GetFlightDetails(FlightDetailRequest flightDetails)
        {
            var sql = @"SELECT * FROM [flightbooking].[dbo].[vwflightdetails] 
                        WHERE departure_date = @ddate AND arrival_date = @adate AND departure_city_code = @dcode
                        AND departure_time = @dtime AND  arrival_time = @atime AND arrival_city_code = @acode
                        ";

            using (var db = dbManager.GetOpenConnection())
            {
                var result = (await db.QueryAsync<FlightDetail>(new CommandDefinition(sql , new {
                    ddate = flightDetails.DepartureDate,
                    adate = flightDetails.ArrivalDate,
                    dcode = flightDetails.DepartureCityCode,
                    dtime = flightDetails.DepartureTime,
                    atime = flightDetails.ArrivalTime,
                    acode = flightDetails.ArrivalCityCode
                }))).FirstOrDefault();
                return result;
            }
        }

        /// <summary>
        /// Get Flight List by date
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<FlightDetail>> GetFlightList(DateTime date)
        {
            var sql = @"SELECT * FROM [flightbooking].[dbo].[vwflightdetails] WHERE departure_date = CONVERT (date, @tdate)
                        ORDER BY departure_time ASC";

            using (var db = dbManager.GetOpenConnection())
            {
                var results = await db.QueryAsync<FlightDetail>(new CommandDefinition(sql, new { tdate = date }));
                if (results != null && results.Count() > 0)
                    return results.ToList();
                else return null;
            }
        }
    }
}