FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /src

COPY . ./
RUN dotnet restore
EXPOSE 80
ENTRYPOINT ["dotnet", "run"]
