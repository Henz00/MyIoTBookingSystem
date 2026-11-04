# MyIoTBookingSystem

A full-stack accommodation rental management system built as a personal learning and portfolio project.

## About the project

MyIoTBookingSystem is a work-in-progress project designed to simulate the software infrastructure of an accommodation rental company. The system aims to provide customers with a platform for browsing and booking rental properties, while allowing administrators to manage properties, bookings and customer interactions.

The project is primarily focused on developing practical software engineering skills, with particular emphasis on backend development, API design, database management, authentication, and system architecture.

IoT integration is also part of the project's longer-term scope, allowing the system to explore communication between physical devices and the software platform.

## Project status

**Work in progress**

Development is currently focused on establishing the backend foundation and implementing the core functionality before moving on to the frontend and IoT integration.

### Implemented

- ASP.NET Core Web API using controllers and .NET 10.
- SQL Server database integration using Entity Framework Core.
- ASP.NET Core Identity with cookie-based authentication.
- Role-based authorization for customers and administrators.
- Department management, including CRUD operations.
- Home management, including CRUD operations and publishing/bookability controls.
- DTO-based API responses and input validation.
- Image upload processing using SkiaSharp, including format validation, dimension limits and re-encoding.
- Local image storage.

### In development
- Image storage references stored in the database.
- Home image management endpoints.
- Further backend functionality and domain logic.

### Planned

- Customer-facing Angular frontend.
- Administrative dashboard.
- Booking and availability management.
- Payment and refund handling.
- Customer inquiries and fault reporting.
- Email notifications.
- IoT integration for monitoring and access management.
- Containerized deployment and production hosting.

## Technology stack

### Backend
- C#
- .NET 10 / ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- SkiaSharp

### Database
- Microsoft SQL Server

### Frontend (planned)
- Angular
- TypeScript
- HTML / CSS

### Development and infrastructure
- Docker / Docker Compose
- Git
- Scalar / OpenAPI

## Development approach

The project is being developed incrementally, prioritizing a functional and reliable foundation before expanding into additional features.

Some of the main areas of focus include:

- Clear separation of responsibilities and maintainable code.
- Secure authentication and authorization.
- Consistent API design and validation.
- Database relationships, constraints and data integrity.
- Reliable handling of concurrent booking operations.
- Practical error handling and testing.
- Exploring architectural decisions and their trade-offs.

The initial implementation is structured as a single ASP.NET Core API application. Architectural complexity will be introduced only when there is a practical reason to do so.

## Project goals

The overall goal is to build a complete application that brings together backend development, frontend development, database design and IoT integration.

Beyond the final application, the project serves as an opportunity to explore real-world software engineering challenges, understand architectural trade-offs and develop solutions that are maintainable, secure and reliable.