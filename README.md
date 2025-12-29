<div align="center">

# 🚀 Career Connect

### A Modern Full-Stack Job Platform

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![React](https://img.shields.io/badge/React-18-61DAFB?style=for-the-badge&logo=react&logoColor=black)](https://reactjs.org/)
[![TypeScript](https://img.shields.io/badge/TypeScript-5.0-3178C6?style=for-the-badge&logo=typescript&logoColor=white)](https://www.typescriptlang.org/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-14+-4169E1?style=for-the-badge&logo=postgresql&logoColor=white)](https://www.postgresql.org/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind_CSS-3.4-06B6D4?style=for-the-badge&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)

*Connect talented professionals with their dream careers*

[Features](#-features) • [Tech Stack](#-tech-stack) • [Getting Started](#-getting-started) • [API Documentation](#-api-documentation) • [Screenshots](#-screenshots) • [Contributing](#-contributing)

</div>

---

## 📋 Overview

**Career Connect** is a comprehensive job platform that bridges the gap between job seekers and employers. Built with a clean architecture approach using ASP.NET Core 8.0 for the backend and React with TypeScript for the frontend, it provides a seamless experience for finding and posting jobs.

## ✨ Features

### For Job Seekers
- 🔍 **Advanced Job Search** - Filter by location, salary, job type, and categories
- 📝 **Easy Applications** - Apply to jobs with just a few clicks
- 👤 **User Profiles** - Manage your profile and track applications
- 🏢 **Company Discovery** - Explore companies and their open positions
- ⭐ **Featured Jobs** - Discover highlighted opportunities

### For Employers
- 📊 **Company Dashboard** - Manage job postings and applications
- 📈 **Platform Statistics** - View engagement metrics
- 🏷️ **Job Categories & Tags** - Organize listings effectively

### Platform Features
- 🔐 **JWT Authentication** - Secure user authentication
- 📱 **Responsive Design** - Works on all devices
- 🎨 **Modern UI** - Clean interface with shadcn/ui components
- ⚡ **Fast Performance** - Optimized queries and React Query caching

## 🛠 Tech Stack

### Backend
| Technology | Purpose |
|------------|---------|
| ASP.NET Core 8.0 | Web API Framework |
| Entity Framework Core 8.0 | ORM & Database Access |
| PostgreSQL | Database |
| AutoMapper | Object Mapping |
| JWT Bearer | Authentication |
| Swagger/OpenAPI | API Documentation |

### Frontend
| Technology | Purpose |
|------------|---------|
| React 18 | UI Library |
| TypeScript | Type Safety |
| Vite | Build Tool |
| React Router | Navigation |
| TanStack Query | Server State Management |
| Tailwind CSS | Styling |
| shadcn/ui | Component Library |
| React Hook Form | Form Handling |
| Zod | Schema Validation |

## 📁 Project Structure

```
Career-Connect/
├── 📂 CareerConnectAPI/                    # Backend API
│   ├── CareerConnect.API/                  # API Layer (Controllers, Middleware)
│   ├── CareerConnect.Application/          # Business Logic (Services, DTOs)
│   ├── CareerConnect.Domain/               # Domain Entities & Interfaces
│   └── CareerConnect.Infrastructure/       # Data Access (EF Core, Repositories)
│
└── 📂 Frontend/                            # React Application
    ├── components/                         # Reusable UI Components
    ├── pages/                              # Page Components
    ├── services/                           # API Integration
    ├── contexts/                           # React Context Providers
    ├── hooks/                              # Custom React Hooks
    └── lib/                                # Utility Functions
```

## 🚀 Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/) (or [Bun](https://bun.sh/))
- [PostgreSQL 14+](https://www.postgresql.org/download/)

### Backend Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/DimonBel/Job-Platform-ASP.NET-React.git
   cd Job-Platform-ASP.NET-React
   ```

2. **Configure the database**
   
   Create a PostgreSQL database and update `CareerConnectAPI/CareerConnect.API/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Host=localhost;Port=5432;Database=CareerConnectDB;Username=postgres;Password=your_password"
     }
   }
   ```

3. **Run migrations and start the API**
   ```bash
   cd CareerConnectAPI/CareerConnect.API
   dotnet ef database update
   dotnet run
   ```
   
   The API will be available at `http://localhost:5000`  
   Swagger UI: `http://localhost:5000/swagger`

### Frontend Setup

1. **Install dependencies**
   ```bash
   cd Frontend
   npm install
   # or
   bun install
   ```

2. **Configure environment**
   
   Create a `.env` file:
   ```env
   VITE_API_URL=http://localhost:5000/api
   ```

3. **Start the development server**
   ```bash
   npm run dev
   # or
   bun dev
   ```
   
   The app will be available at `http://localhost:5173`

## 📚 API Documentation

### Core Endpoints

| Resource | Endpoints | Description |
|----------|-----------|-------------|
| **Jobs** | `GET, POST, PUT, DELETE /api/jobs` | Job listings management |
| **Companies** | `GET, POST, PUT, DELETE /api/companies` | Company profiles |
| **Categories** | `GET, POST, PUT, DELETE /api/categories` | Job categories |
| **Auth** | `POST /api/auth/login, /register` | Authentication |
| **Statistics** | `GET /api/statistics` | Platform metrics |

### Search & Filtering

```
GET /api/jobs/search?query=developer&location=New York&types=Full-time&salaryMin=50000
```

| Parameter | Type | Description |
|-----------|------|-------------|
| `query` | string | Search in title, company, tags |
| `location` | string | Filter by location |
| `types` | string[] | Job types (Full-time, Part-time, Remote) |
| `salaryMin` / `salaryMax` | decimal | Salary range |
| `categoryId` | int | Category filter |
| `page` / `pageSize` | int | Pagination |

📖 **Full API documentation available at** `/swagger` when running the backend

## 🖼 Screenshots

<div align="center">
<i>Coming soon...</i>
</div>

## 🗺 Roadmap

- [x] Core job listing functionality
- [x] User authentication (JWT)
- [x] Company profiles
- [x] Job search with filters
- [ ] Resume upload & management
- [ ] Email notifications
- [ ] Admin dashboard
- [ ] Redis caching
- [ ] Rate limiting

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<div align="center">

**⭐ Star this repository if you find it helpful!**

Made with ❤️ by [DimonBel](https://github.com/DimonBel)

</div>