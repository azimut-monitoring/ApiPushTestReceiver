# ApiPushTestReceiver Sample project

Please, first consult [the documentation reference for the Azimut-monitoring API push feature](https://doc-analytics.azimut-monitoring.com/articles/ApiPush/apipush-v1.html)


## Overview




You can use this project as a starting point for your integration. It receives push events and stores them in memory (only keeping the last N elements - configurable with `BufferSize` in  [appsettings.json](appsettings.json)) The parsed details can then be displayed in a user-friendly page at the [site root](https://localhost:5001).

The endpoint listens to push events on [https://localhost:5001/push](https://localhost:5001/push).
It expects an HTTP header for authentication with a specific value (see [Helpers/ApiKeyAuthenticationHandler.cs](Helpers/ApiKeyAuthenticationHandler.cs) - `ApiKeyHeaderName` and `ValidApiKey` can be configured in [appsettings.json](appsettings.json)).

A sample valid request can be found in [TestRequests.rest](TestRequests.rest)

## Building the project

This project is written in C# on [.NET Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2) and can be used cross-platform. You can build it using `dotnet build` from the root folder, then run it with `dotnet run`.

