FROM mcr.microsoft.com/dotnet/sdk:8.0

WORKDIR /app
COPY . .

# Restore all projects
RUN dotnet restore

RUN dotnet build DailyNotificationService.csproj -c Debug
RUN dotnet build Tests/DailyNotificationService.Tests.csproj -c Debug

CMD ["dotnet", "watch", "--project", "DailyNotificationService.csproj", "run", "--urls=http://0.0.0.0:80"]