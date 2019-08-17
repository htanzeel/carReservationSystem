The application works on port 5000

download docker
download the project folder
Open a terminal and browse to the folder where the Makefile is located
run command "make build" to download the image and build
run command "make run" to run the application
open a browser and go to http://localhost:5000/api/appointments to get all reservations
run command "make test" to run all the test cases

application runs on: http://localhost:5000/

There are 3 tables for the application.

CarUsers, Cars and CarReservations

GET Cars
http://localhost:5000/api/Cars

GET CarUsers
http://localhost:5000/api/Users

GET CarReservations
http://localhost:5000/api/appointments

GET CarReservations by ID
http://localhost:5000/api/appointments/31

GET CarReservations by date range sorted by price
http://localhost:5000/api/appointments/byDate?start=2019-01-02&end=2019-01-08

POST (only posts a reservation if the carID doesnt have conflicting date range)
http://localhost:5000/api/appointments
{
    "userID": 1,
    "carID" : 2,
    "reservationStartTime": "2021-05-01T10:20:05.123",
    "reservationEndTime": "2021-06-01T10:20:05.123"
}

PUT (only updates a reservation if the carID doesnt have conflicting date range)
http://localhost:5000/api/appointments/31
{
    "reservationID": 33,
    "userID": 1,
    "carID": 1,
    "reservationStartTime": "2022-08-01T00:00:00",
    "reservationEndTime": "2022-08-15T00:00:00",
    "totalCost": 2800.0
}

DELETE
http://localhost:5000/api/appointments/40



