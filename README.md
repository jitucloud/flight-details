**Get All Flights: HttpGet**

Request URL: http://localhost:61429/api/flights/today


**Get Flights for Today: HttpGet**

Request URL:  http://localhost:61429/api/flights/today


**Get Flights for a given date : HttpGet**

Request URL:  http://localhost:61429/api/flights?date=4/24/2018


**Check Availbility : HttpGet**

http://localhost:61429/api/flights/checkavailbility?startDate=4/23/2018&endDate=4/26/2018&passengerCount=2


**Make A Booking : HttpPost**

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


**SEARCH: HttpPost** 

Request URL: http://localhost:61429/api/bookings/search


Request Json: 

{
	"PNR":"1573346",
	"FirstName":"test 1",
	"LastName":"jitu"
}