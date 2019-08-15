FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY *.sln .
COPY CarReservationSystem/*.csproj ./CarReservationSystem/
COPY CarReservationSystemTests/*.csproj ./CarReservationSystemTests/
RUN dotnet restore

# copy everything else and build app
COPY CarReservationSystem/. ./CarReservationSystem/
COPY CarReservationSystemTests/. ./CarReservationSystemTests/
WORKDIR /app
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/CarReservationSystem/out ./
COPY --from=build /app/CarReservationSystemTests/out ./
ENTRYPOINT ["dotnet", "CarReservationSystem.dll"]