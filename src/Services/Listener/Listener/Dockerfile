FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Listener/Listener/Listener.csproj", "Services/Listener/Listener/"]
COPY ["BuildingBlocks/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "BuildingBlocks/EventBus.RabbitMQ/"]
RUN dotnet restore "Services/Listener/Listener/Listener.csproj"
COPY . .
WORKDIR "/src/Services/Listener/Listener"
RUN dotnet build "Listener.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Listener.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Listener.dll"]