**Get All Flights**

http://localhost:61429/api/flights/today
HTTP Verb: HttpGet

**Get Flights for Today**

Request URL:  http://localhost:61429/api/flights/today
HTTP Verb: HttpGet


**Get Flights for a given date**

Request URL:  http://localhost:61429/api/flights?date=4/24/2018
HTTP Verb: HttpGet


**Make A Booking**

Request URL: http://localhost:61429/api/bookings
HTTP Verb: HttpPost

Request Json:

{
	"FlightDetails": {
		"DepartureCityCode": "DXB",
		"DepartureDate": "2018-04-24",
		"DepartureTime": "10:00:00",
		"ArrivalCityCode": "DEL",
		"ArrivalDate": "2018-04-24",
		"ArrivalTime": "14:00:00"
	},
	"Passenger": [{
			"FirstName": "test 1",
			"LastName": "test 1 last"
		},
		{
			"FirstName": "test 2",
			"LastName": "test 2 last"
		}
	]
}


**SEARCH** 
Request URL: http://localhost:61429/api/bookings/search
HTTP Verb:  HttpPost

Request Json: 

{
	"PNR":"1573346",
	"FirstName":"test 1",
	"LastName":"jitu"
}