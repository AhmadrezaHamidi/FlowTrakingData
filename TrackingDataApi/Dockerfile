#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["TrackingDataApi/TrackingDataApi.csproj", "TrackingDataApi/"]
#COPY ["RepositoryEfCore/RepositoryEfCore.csproj", "RepositoryEfCore/"]
#COPY ["WorkFlow/WorkFlow.csproj", "WorkFlow/"]
#RUN dotnet restore "TrackingDataApi/TrackingDataApi.csproj"
#COPY . .
#WORKDIR "/src/TrackingDataApi"
#RUN dotnet build "TrackingDataApi.csproj" -c Release -o /app/build
#
#FROM build AS publish
#RUN dotnet publish "TrackingDataApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrackingDataApi.dll"]