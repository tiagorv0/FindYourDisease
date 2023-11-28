FROM mcr.microsoft.com/dotnet/aspnet:7.0  AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /src
COPY ["FindYourDisease.Users.API/FindYourDisease.Users.API.csproj", "FindYourDisease.Users.API/"]
COPY ["FindYourDisease.Users.Domain/FindYourDisease.Users.Domain.csproj", "FindYourDisease.Users.Domain/"]
COPY ["FindYourDisease.Users.Application/FindYourDisease.Users.Application.csproj", "FindYourDisease.Users.Application/"]
COPY ["FindYourDisease.Users.Infra/FindYourDisease.Users.Infra.csproj", "FindYourDisease.Users.Infra/"]
RUN dotnet restore "FindYourDisease.Users.API/FindYourDisease.Users.API.csproj"
COPY . .
WORKDIR "/src/FindYourDisease.Users.API"
RUN dotnet build "FindYourDisease.Users.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FindYourDisease.Users.API.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FindYourDisease.Users.API.dll"]