# ConsoleBasedMicroservice
A simple rabbitmq client-listener implementation using .NET 6, MassTransit and a RabbitMQ Docker image.


## Requirements
* Docker Desktop (Windows or Mac) or Docker Engine (Linux)
* Dotnet SDK 6 installed
* dotnet CLI / Visual Studio 2019/2022

## Installation & Running
To install clone [this repository](https://github.com/AliM-01/ConsoleBasedMicroservice):
```
git clone https://github.com/AliM-01/ConsoleBasedMicroservice
```

And run `docker-compose.yml` file:
```
cd ConsoleBasedMicroservice/src/
docker-compose -f docker-compose.yml up -d
```

To test it open a browser and navigate to `http://localhost:7000/swagger/index.html` and access swagger :

You can send messages from browser; to view recieved messages 

* Open Docker Desktop
* Open `cbm_client` container
* View logs

**Thanks !**
