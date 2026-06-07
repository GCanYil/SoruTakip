# SoruTakip

A web application built with ASP.NET Core MVC that allows students preparing for the YKS exam to upload questions they get stuck on, organize them, and track their progress.

## Features

- Upload question images and categorize by subject and topic
- Organize questions into custom folders
- Track question status (Stuck / Solved)
- Dashboard with statistics and progress analytics
- User-specific data isolation

## Requirements

- .NET 9 SDK
- PostgreSQL

## Setup

1. Clone the repository:

```
git clone https://github.com/GCanYil/SoruTakip.git
```

2. Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=sorutakip;Username=postgres;Password=YOUR_PASSWORD"
}
```

3. Apply migrations:

```
dotnet ef database update
```

4. Run the project:

```
dotnet run
```

5. Open in browser: `https://localhost:PORT`
