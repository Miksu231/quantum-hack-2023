FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

WORKDIR /src

COPY ./src/ ./
RUN dotnet restore
EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]
