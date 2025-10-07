# Product Catalog Project

This archive contains a minimal but complete backend (ASP.NET Core 6 + EF Core + SQLite) and frontend (React) scaffold that implement the exact assessment requirements.

## Backend (run)
1. Navigate to `backend` folder.
2. Ensure .NET 6 SDK is installed.
3. Run `dotnet restore`.
4. Run `dotnet tool install --global dotnet-ef --version 6.0.0` if not installed.
5. Run `dotnet ef migrations add InitialCreate` then `dotnet ef database update`.
6. Run `dotnet run`.

Swagger UI: https://localhost:5001/swagger

## Frontend (run)
1. Navigate to `frontend`.
2. Run `npm install`.
3. Copy `.env.development` if needed and set REACT_APP_API_BASE_URL to your backend API base.
4. Run `npm start`.

