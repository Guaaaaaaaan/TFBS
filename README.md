# TFBS – Tiny Faculty Booking System

TFBS is a backend system built with ASP.NET Core (.NET 10) and Entity Framework Core using a Database-First approach.
It manages vehicle reservations, trips, maintenance workflows, and reporting for a college motor vehicle pool.

## Architecture

- ASP.NET Core Web API (MVC-style project, JSON responses only)
- EF Core (Database First, Scaffolded DbContext and Entities)
- SQL Server
- Service-based business logic
- Thin Controllers
- Global exception handling middleware

## Key Features

### Reservations

- Create vehicle reservations by department and faculty
- Enforces faculty-department relationship

### Trips

- One-to-one relationship between Reservation and Trip
- Trip creation and completion with odometer validation
- Fuel purchase and credit card validation

### Maintenance

- Two-step maintenance workflow:
  - Create Maintenance Log
  - Complete Maintenance with details, parts usage, and authorization
- Transactional integrity (all-or-nothing)
- Inventory deduction for parts
- Role-based mechanic authorization

### Lookups

- Departments, faculties, vehicles, mechanics
- Optimized read-only endpoints

### Reports

- Mileage by vehicle
- Mileage by department
- Revenue by department

## Error Handling

- All business rules throw domain-specific exceptions in Service layer
- Global middleware maps exceptions to consistent HTTP responses
- No try/catch logic inside controllers

## Getting Started

1. Create database using provided SQL scripts
2. Update connection string in appsettings.json
3. Run `Scaffold-DbContext` if schema changes
4. Run the project and test APIs using Postman
