# API Testing Framework

A lightweight and scalable API automation framework built with C# and .NET. The framework uses HttpClient for API communication, WireMock.Net for service virtualization, and Serilog for structured logging.

## Technologies

* C#
* .NET
* NUnit
* HttpClient
* WireMock.Net
* Serilog
* FluentAssertions
* Newtonsoft.Json

## Features

* REST API testing with HttpClient
* Reusable API client
* Service virtualization using WireMock.Net
* Structured request and response logging with Serilog
* JSON serialization and deserialization
* Centralized configuration
* Clean and maintainable project structure

## Project Structure

```text
ApiTestingFramework
│
├── Clients
├── Configuration
├── Helpers
├── Logging
├── Models
├── Tests
├── WireMock
└── appsettings.json
```

## Running the Tests

```bash
git clone https://github.com/TheDarkSeal/ApiTestingFramework.git

cd ApiTestingFramework

dotnet restore

dotnet test
```

## Purpose

This project demonstrates good practices for API test automation, including reusable components, maintainable architecture, structured logging, and service virtualization.

## Author

TheDarkSeal

GitHub: https://github.com/TheDarkSeal
