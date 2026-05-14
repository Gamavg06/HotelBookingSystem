FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Al no haber carpeta, copiamos directamente el archivo al contenedor
COPY ["HotelReservation.csproj", "./"]
RUN dotnet restore "HotelReservation.csproj"

# 2. Copiamos el resto de los archivos (Program.cs, Controllers, etc.)
COPY . .

# 3. Compilamos directamente en la raíz del WORKDIR
RUN dotnet build "HotelReservation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelReservation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "HotelReservation.dll"]