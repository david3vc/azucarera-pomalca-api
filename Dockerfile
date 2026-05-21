FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY AzucareraPomalca.sln ./
COPY AzucareraPomalca.Api/AzucareraPomalca.Api.csproj AzucareraPomalca.Api/
COPY AzucareraPomalca.Application/AzucareraPomalca.Application.csproj AzucareraPomalca.Application/
COPY AzucareraPomalca.Core/AzucareraPomalca.Core.csproj AzucareraPomalca.Core/
COPY AzucareraPomalca.Domain/AzucareraPomalca.Domain.csproj AzucareraPomalca.Domain/
COPY AzucareraPomalca.Infrastructure/AzucareraPomalca.Infrastructure.csproj AzucareraPomalca.Infrastructure/
COPY AzucareraPomalca.Utils/AzucareraPomalca.Utils.csproj AzucareraPomalca.Utils/

RUN dotnet restore AzucareraPomalca.Api/AzucareraPomalca.Api.csproj

COPY . .
RUN dotnet publish AzucareraPomalca.Api/AzucareraPomalca.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "AzucareraPomalca.Api.dll"]
