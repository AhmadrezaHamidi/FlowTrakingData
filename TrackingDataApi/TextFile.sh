dotnet publish "TrackingDataApi.csproj" -c Release -o "F:\Publish\TrackingData"
cd "F:\Publish\TrackingData"
docker build 
