**Make A Booking**

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

Request Json: 

{
	"PNR":"1573346",
	"FirstName":"test 1",
	"LastName":"jitu"
}