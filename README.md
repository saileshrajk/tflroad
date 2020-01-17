# Tfl.RoadStatus
Console application to retreive the status of a road

## Development

The following tools are used required:

* VS 2019
* .NET Core 3.1

## Configuration
Configuration are in appsettings.json. All settings are mandatory.
Please change the Application ID and Application key in this file.

## Usage
Included in the zip is a folder called published. It is a self contained folder. 
It needs the name of the road as an argument.
The application can be run from with in here by executing the Tfl.RoadStatus.exe like below

- From the command prompt : RoadStatus.exe A2
- From PowerShell : .\RoadStatus.exe A2

## Unit Tests
Unit Tests can be run either via Visual Studio or dotnet cli running command 
below in the folder where the .sln file is. Unit test uses XUnit, FluentAssertions and NSubstitute

command: dotnet test