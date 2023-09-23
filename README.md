# Yu Gi Oh Tournaments

![](./documentation/banner.jpg)

## Backend

### Technologies

- ASP.NET Core
- C#
- Entity Framework

### 1. Up Database

```bash
docker compose up -d
```

This command execute el `docker compose` file and download the last version of the `postgreSQL`.

By default the the database credentials are:

**USER**: postgres

**PASSWORD**: postgres

**DATABASE**: yu-gi-oh

### 2. Restore packages

```bash
dotnet restore
```

This command restore/install all packages installed in the project.

### 3. Run project

```bash
# run in production mode
dotnet run

# run in development mode with watch mode
dotnet watch run --project . # inside backend folder

# create build
dotnet build
```

### 4. Fill basic data in the databse

Doing `POST` petition to endpoint:

```http
POST http://localhost:5202/api/seed
```

### 5. See Swagger Documentation

http://localhost:5202/swagger/index.html

### Frontend

> Vue App
