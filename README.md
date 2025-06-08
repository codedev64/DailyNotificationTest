
# 📬 DailyNotificationService

**DailyNotificationService** is a lightweight background worker built with .NET 8 that sends scheduled daily push notifications to users via the OneSignal API. Users can enable or disable notifications and specify their preferred notification time through simple API endpoints.

---

## 🔧 Features

- Sends daily push notifications via OneSignal
- User-defined notification time preferences
- Toggle notification on/off per user
- Built with .NET 8, EF Core, MySQL, and OneSignal
- Dockerized for local development
- Unit tested with xUnit

---

## 📁 Project Structure

```
DailyNotificationService/
├── Background/            # Background worker for push logic
├── Controllers/           # API endpoints
├── Data/                  # DbContext and EF configuration
├── Models/                # Data models
├── Services/              # Notification and database services
├── Configurations/        # App settings classes
├── Tests/                 # Unit tests
├── appsettings.json       # Default config
├── Dockerfile             # Docker build config
├── docker-compose.yml     # Compose for local db + service
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Docker + Docker Compose
- OneSignal account with App ID and REST API Key

---

## 🛠 Setup

**Configure OneSignal settings**

Update `appsettings.Development.json`:

```json
{
  "OneSignalSettings": {
    "AppId": "your-onesignal-app-id",
    "ApiKey": "your-api-key"
  },
  "ConnectionStrings": {
    "DefaultConnection": "server=db;port=3306;database=notificationsdb;user=root;password=rootpassword"
  }
}
```

---

## 🐳 Run with Docker (Development Mode)

### Build and run

```bash
docker build -t dailynotify-dev .

docker run --rm -it -v %cd%:/app -w /app -p 5000:80 dailynotify-dev \
dotnet watch run --urls=http://0.0.0.0:80
```

> On Mac/Linux use `${PWD}` instead of `%cd%`.

### Or use Docker Compose

```bash
docker-compose up --build
```

---

## 🧪 Run Unit Tests

```bash
# Run tests in container
docker run --rm -it -v %cd%:/app -w /app/Tests dailynotify-dev \
dotnet test
```

---

## 📬 API Endpoints

| Method | Endpoint                | Description                              |
|--------|-------------------------|------------------------------------------|
| POST   | `/enableNotifications`  | Enable notifications for a user          |
| POST   | `/disableNotifications` | Disable notifications for a user         |
| POST   | `/setNotificationTime`  | Set the time a user receives notifications |

---

## 🧱 Tech Stack

- **.NET 8**
- **Entity Framework Core**
- **MySQL** (via Docker)
- **OneSignal REST API**
- **Docker + Compose**
- **xUnit** for testing

---

## 🔮 Future Enhancements

- Add authentication for endpoints
- Notification retry logic
- Admin dashboard for analytics
- Configurable templates per user

---

## 🧑‍💻 Maintainer

Built and maintained by **Jason Cole**

---
