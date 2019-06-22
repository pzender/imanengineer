# etap 1 - kompilacja
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-bionic AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

# etap 2 - obraz z gotową aplikacją
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-bionic
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TV_App.dll"]