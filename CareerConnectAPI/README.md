# Career Connect API

ASP.NET Core Web API backend for the Career Connect job platform.

## Tech Stack

- **Framework**: ASP.NET Core 9.0
- **Architecture**: Clean Architecture
- **Database**: PostgreSQL
- **ORM**: Entity Framework Core 9.0
- **API Documentation**: Swagger/OpenAPI

## Project Structure (Clean Architecture)

```
CareerConnectAPI/
├── CareerConnect.API/              # Presentation Layer
│   ├── Controllers/                # API endpoints
│   ├── Middleware/                 # Custom middleware
│   └── Program.cs                  # Application entry point
│
├── CareerConnect.Application/      # Application Layer
│   ├── DTOs/                       # Data Transfer Objects
│   ├── Interfaces/                 # Service interfaces
│   ├── Services/                   # Business logic
│   └── Mappings/                   # AutoMapper profiles
│
├── CareerConnect.Domain/           # Domain Layer
│   ├── Entities/                   # Domain entities
│   ├── Interfaces/                 # Repository interfaces
│   └── Common/                     # Base classes
│
└── CareerConnect.Infrastructure/   # Infrastructure Layer
    ├── Data/                       # DbContext
    ├── Repositories/               # Repository implementations
    ├── Migrations/                 # EF Core migrations
    └── DbSeeder.cs                 # Database seeding
```

## Prerequisites

- .NET 8.0 SDK
- PostgreSQL 14+
- Visual Studio 2022 / VS Code / Rider

## Setup

### 1. Database Setup

Create a PostgreSQL database:

```sql
CREATE DATABASE CareerConnectDB;
```

### 2. Configure Connection String

Update `appsettings.json` with your PostgreSQL connection:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=CareerConnectDB;Username=postgres;Password=your_password"
  }
}
```

### 3. Run Migrations

```bash
cd CareerConnectAPI/CareerConnectAPI
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 4. Run the Application

```bash
dotnet run
```

The API will be available at:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001
- Swagger UI: http://localhost:5000/swagger

## API Endpoints

### Jobs
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/jobs` | Get all jobs |
| GET | `/api/jobs/featured` | Get featured jobs |
| GET | `/api/jobs/search` | Search jobs with filters |
| GET | `/api/jobs/{id}` | Get job by ID |
| GET | `/api/jobs/{id}/details` | Get job with full details |
| POST | `/api/jobs` | Create a new job |
| PUT | `/api/jobs/{id}` | Update a job |
| DELETE | `/api/jobs/{id}` | Delete a job |

### Companies
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/companies` | Get all companies |
| GET | `/api/companies/search` | Search companies |
| GET | `/api/companies/{id}` | Get company by ID |
| GET | `/api/companies/{id}/details` | Get company with jobs |
| POST | `/api/companies` | Create a new company |
| PUT | `/api/companies/{id}` | Update a company |
| DELETE | `/api/companies/{id}` | Delete a company |

### Categories
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/categories` | Get all categories with job counts |
| GET | `/api/categories/{id}` | Get category by ID |
| POST | `/api/categories` | Create a category |
| PUT | `/api/categories/{id}` | Update a category |
| DELETE | `/api/categories/{id}` | Delete a category |

### Users
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/users` | Get all users |
| GET | `/api/users/{id}` | Get user by ID |
| GET | `/api/users/email/{email}` | Get user by email |
| POST | `/api/users` | Create/Register a user |
| PUT | `/api/users/{id}` | Update user profile |
| DELETE | `/api/users/{id}` | Delete a user |

### Applications
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/applications/{id}` | Get application by ID |
| GET | `/api/applications/job/{jobId}` | Get applications for a job |
| GET | `/api/applications/user/{userId}` | Get applications by user |
| POST | `/api/applications` | Apply for a job |
| PUT | `/api/applications/{id}/status` | Update application status |
| DELETE | `/api/applications/{id}` | Withdraw application |

### Statistics
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/statistics` | Get platform statistics |

### Tags
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tags` | Get all tags |

## Search Parameters

The `/api/jobs/search` endpoint supports the following query parameters:

| Parameter | Type | Description |
|-----------|------|-------------|
| `query` | string | Search by title, company, or tags |
| `location` | string | Filter by location |
| `types` | string[] | Filter by job types (Full-time, Part-time, etc.) |
| `salaryMin` | decimal | Minimum salary filter |
| `salaryMax` | decimal | Maximum salary filter |
| `categoryId` | int | Filter by category |
| `featuredOnly` | bool | Show only featured jobs |
| `page` | int | Page number (default: 1) |
| `pageSize` | int | Items per page (default: 10) |
| `sortBy` | string | Sort field (title, salary, company, date) |
| `sortDescending` | bool | Sort direction (default: true) |

## CORS Configuration

The API is configured to allow requests from:
- http://localhost:5173 (Vite dev server)
- http://localhost:3000 (React dev server)

## Database Seeding

The application automatically seeds the database with sample data on first run, including:
- 8 job categories
- 10 companies
- 10 jobs
- 29 tags
- Job responsibilities, requirements, and benefits

## Next Steps

- [ ] Add authentication (JWT)
- [ ] Add file upload for resumes
- [ ] Add email notifications
- [ ] Add admin dashboard endpoints
- [ ] Add rate limiting
- [ ] Add caching with Redis
