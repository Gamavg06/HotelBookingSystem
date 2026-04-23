FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copiamos el archivo .csproj que ahora está en la raíz
COPY ["HotelReservation.csproj", "./"]
RUN dotnet restore "HotelReservation.csproj"

# 2. Copiamos todo lo demás
COPY . .

# 3. Compilamos directamente
RUN dotnet build "HotelReservation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelReservation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "HotelReservation.dll"]