# 🐾 PetConnect  

PetConnect is a multi-location animal shelter management application built with ASP.NET Core MVC using a layered, service-based architecture inspired by Clean Architecture principles.

The application manages shelters, animals, adopters, adoptions, payments, staff, and administrative workflows while enforcing role-based access control across multiple shelter locations.

## Project Overview

PetConnect was designed to simulate real-world shelter operations through a structured web application that emphasizes:

- Separation of concerns
- Maintainable backend architecture
- Role-based authorization
- Multi-location data management
- Scalable CRUD operations
- Dashboard reporting and analytics

The system supports multiple shelters across different locations, each with its own staff, managers, animals, adopters, payments, and adoption records.

## Features

### Authentication & Authorization

- ASP.NET Core Identity authentication
- Role-based access control (RBAC)
- Protected routes and restricted operations
- Shelter-scoped data access

### Role Permissions

#### Staff
- Manage animals
- Manage adopters
- Manage adoptions
- Manage payments
- Create and view notes
- View shelters assigned to their location

#### Manager
Includes all Staff permissions plus:
- Create staff accounts
- Update staff information
- Deactivate staff accounts

#### Admin
Full system access including:
- Manage all shelters
- Manage all users and staff
- Manage animal types and attributes
- Global reporting and dashboard access
- Access to all shelter data

## Core Functionality

### Animal Management
- Create, update, and deactivate animals
- Track adoption status
- Assign animal types and attributes
- Store shelter-specific records

### Adoption Workflow
- Manage adoption lifecycle
- Link adopters to animals
- Track adoption status and payments

### Shelter Management
- Multi-location shelter support
- Shelter-specific data isolation
- Staff assignment by shelter

### Dashboard & Reporting
Role-specific dashboards displaying:
- Animal totals
- Adoption statistics
- Pending adoptions
- Revenue tracking
- Animal trend charts
- Animal type distribution charts

### Search & Filtering
Search and filtering functionality implemented for:
- Animals
- Adopters
- Adoptions

## Architecture

The application follows a layered architecture inspired by Clean Architecture concepts.

### Project Structure

#### Presentation Layer
- ASP.NET Core MVC Controllers
- Razor Views
- ViewModels

#### Application Layer
- Query services (read operations)
- Command services (write operations)
- Business logic

#### Domain Layer
- Core entities
- Interfaces and contracts

#### Infrastructure Layer
- Entity Framework Core
- SQL Server persistence
- Data access implementation

## Technology Stack

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- LINQ
- Bootstrap 5
- Razor Views
- Dependency Injection

## Testing

Manual QA testing was performed for:

- Authentication workflows
- Authorization and protected routes
- CRUD operations
- Validation handling
- Edge cases
- Role permissions

See `TESTING.md` for detailed test cases and known issues.

## Known Limitations

Current known issues and planned improvements are documented in the testing documentation.

Areas planned for future enhancement include:
- Additional responsive design improvements
- Real-time validation feedback
- Pagination for large datasets
- Expanded automated testing coverage

## Deployment

Deployment is currently in progress.

Planned hosting architecture:
- ASP.NET Core MVC application hosted on AWS
- SQL Server database hosted separately

## Learning Goals Demonstrated

This project demonstrates practical experience with:

- ASP.NET Core MVC application development
- Entity Framework Core
- Authentication and authorization
- Role-based access control
- Service-layer architecture
- Query/write service separation
- Relational database design
- Dashboard and reporting systems
- Manual QA testing practices

## About

Developed by Tanya Thomas as a portfolio project focused on backend architecture, scalable application structure, and real-world workflow management using ASP.NET Core technologies.
