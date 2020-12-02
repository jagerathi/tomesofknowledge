## Full stack Engineer Challenge

Hello, we've created this coding challenge to let you show us how you would work in our tech stack of .NET Core and Angular. Please complete the **TODOs** section below.

## The repository

This repository contains a project that you can either run with Visual Studio or run via the command line with the following command to see changes as you work. Running the C# application will launch both the API and UI tiers. 
```
cd .\FullStack\
dotnet watch run
```

You can run the tests with Visual Studio or via the command line with the following command
```
dotnet test
```

## Things you might need

* A text editor or IDE of your choice. Here are a few that we use and find handy
	* VSCode: https://code.visualstudio.com/download	
	* Visual Studio (Community): https://visualstudio.microsoft.com/vs/community/	
	* Notepad++: https://notepad-plus-plus.org/downloads/
* A .NET Standard 2.0 compatible framework (you will need .NET Core 3.1 for the tests to run)
	* More information: https://docs.microsoft.com/en-us/dotnet/standard/net-standard
* Node 12+
	* https://nodejs.org/
* Angular CLI
	* https://cli.angular.io/
* Docker (optional)
	* https://www.docker.com/

## TODOs
* Implement sorting and paging in the ListComponent Dotnet Core API
* Implement sorting and paging in the ListComponent Angular Application
* Implement a UpdateComponent Dotnet Core API endpoint
* Integrate the Angular ChangeComponent to invoke the UpdateComponent API to change a component's status

## Requirements
* Do not change any C# interfaces
* Do not add any Nuget packages
* Do not change any existing unit tests (feel free to add your own as you develop)
* All (current) unit tests must pass

## Bonus
* Add a `Dockerfile` and a `Docker Compose` file that supports running your application with `docker-compose up`


## Troubleshooting
If you see any node/javascript build errors, check if the node modules were installed correctly by going to the `ClientApp` directory and running a `npm install` command.
