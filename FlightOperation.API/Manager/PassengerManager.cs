using Dapper;
using FlightOperation.API.Interface;
using FlightOperation.API.Model;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FlightOperation.API.Manager
{
    public class PassengerManager : IPassengerManager
    {
        private IDbManager dbManager;
        public PassengerManager(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public async Task<int> CreatePassenger(Passenger passenger)
        {
            var sql = @"INSERT INTO [flightbooking].[dbo].[passenger] (firstname,lastname)
                        VALUES (@fname,@lname);

                        SELECT CAST(SCOPE_IDENTITY() as INT);
                        ";

            using (var db = dbManager.GetOpenConnection())
            {
                var cmd = new CommandDefinition(sql, new
                {
                    fname = passenger.FirstName,
                    lname = passenger.LastName
                });

                return (await db.QueryAsync<int>(cmd)).FirstOrDefault();
            }
        }
        public async Task<bool> UpdatePassengerBookingRecord(int passengerId, string pnr)
        {
            var sql = @"INSERT INTO [flightbooking].[dbo].[bookingrecord] 
                        VALUES (@pnr,@pid);

                        SELECT CAST(SCOPE_IDENTITY() as INT);
                        ";

            using (var db = dbManager.GetOpenConnection())
            {
                var cmd = new CommandDefinition(sql, new
                {
                    pnr = pnr,
                    pid = passengerId
                });

                return (await db.QueryAsync<int>(cmd)).FirstOrDefault() > 0;
            }
        }
    }
}