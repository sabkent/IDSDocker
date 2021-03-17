#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 8443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IDServer/IDServer.csproj", "IDServer/"]
RUN dotnet restore "IDServer/IDServer.csproj"
COPY . .
WORKDIR "/src/IDServer"
RUN dotnet build "IDServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IDServer.dll"]
