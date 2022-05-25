# ConsoleBasedMicroservice
A simple Microservice that implements messaging(sender, receiver) with .NET 6 & RabbitMQ


## Requirements
* Docker Desktop (Windows or Mac) or Docker Engine (Linux)
* Dotnet SDK 6 installed
* dotnet CLI

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

To try it open a browser and navigate to `http://localhost:7000/swagger/index.html` and access swagger :

You can send messages from the browser; to view received messages 

* Open Docker Desktop
* Open `cbm_client` container
* View logs

**Thanks !**
