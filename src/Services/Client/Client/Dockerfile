FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Client/Client/Client.csproj", "Services/Client/Client/"]
COPY ["BuildingBlocks/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus.RabbitMQ/"]
RUN dotnet restore "Services/Client/Client/Client.csproj"
COPY . .
WORKDIR "/src/Services/Client/Client"
RUN dotnet build "Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Client.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Client.dll"]
