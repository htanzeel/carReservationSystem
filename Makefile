build:
	docker build -t reservation-app:latest .

run: stop
	docker run -p 5000:80 --name reservation-app -v ${PWD}/CarReservationSystem/Database:/app/Database reservation-app:latest

exec:
	docker exec -it reservation-app /bin/sh

stop:
	docker stop reservation-app || true
	docker rm reservation-app || true

test: stop
	docker run -d -p 5000:80 --name reservation-app -v ${PWD}/CarReservationSystem/Database:/app/Database reservation-app:latest
	sleep 3
	docker exec -it reservation-app /bin/sh -c "dotnet CarReservationSystemTests.dll"
