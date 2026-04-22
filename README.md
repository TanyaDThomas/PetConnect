# 🐾 PetConnect  
### ASP.NET Core MVC Animal Shelter Management System

A full-stack web application built with **ASP.NET Core MVC** demonstrating a **Clean Architecture–inspired design** for managing animal shelters, adopters, animals, and adoption workflows.

---

# 🌟 Project Overview

PetConnect is a structured, database-driven web application designed to simulate real-world shelter operations. It focuses on **clean separation of concerns**, maintainable architecture, and scalable backend design using services and Entity Framework Core.

The system supports full **CRUD operations** across all core entities while maintaining a clear separation between UI, business logic, and data access layers.

---

# 🧩 Key Features

## 🏠 Shelter Management
- Create, update, view, and deactivate shelters  
- Track contact details and location data  

## 🐶 Animal Management
- Manage animals with breed, type, and adoption status  
- Track medical and care attributes (vaccination, special needs, etc.)  

## 👤 Adopter System
- Maintain adopter profiles  
- Link adopters to adoption records  

## 📝 Adoption Workflow
- Full adoption lifecycle management  
- Assign animals to adopters and shelters  
- Track adoption status and fees  

## 💰 Financial Tracking
- Store and manage adoption fees  
- Support structured payment-related data design (extensible)  

---

# 🏗️ Architecture & Design

This project follows a **Clean Architecture–inspired layered structure**, ensuring separation of responsibilities and scalable code organization.

## 📦 Structure

### Presentation Layer (Web)
- ASP.NET Core MVC Controllers  
- Razor Views  

### Application Layer
- Business logic services  
- Query services (read operations)  
- Command services (write operations)  

### Domain Layer
- Core entities  
- Domain contracts/interfaces  

### Infrastructure Layer
- Entity Framework Core  
- Database context and persistence logic  

### ViewModels Layer
- UI-specific models for data transfer and validation  

---

# ⚙️ Technology Stack

```bash
ASP.NET Core MVC
C#
Entity Framework Core
SQL Server
LINQ
Bootstrap 5
Dependency Injection
Razor Views
```

---
# 🎯 What This Project Demonstrates

This project was built to demonstrate:

- Real-world ASP.NET Core MVC Architecture
- Clean separation of concerns
- Service-based design patterns
- ViewModel mapping and UI abstraction
- Database-first workflow using Entity Framework Core
- Scalable CRUD system design
- Practiccal application of Clean Architecture principles

---
# 🚀 Future Enhancements
- ASP.NET Core Identity (authentication & role management)
- Payment gateway integration
- Advanced search and filtering system
- Admin dashboard analytics
- REST API layer for external integrations
-UI improvements and responsive enhancements

---
# 👨‍💻 About the Developer

## Tanya Thomas
ASP.NET Core Developer focused on Clean Architecture, scalable backend systems, and real-world application design.

---
# 📌 Summary

PetConnect is a demonstration of building a structured, maintainable web application using modern ASP.NET Core practices. It emphasizes separation of concerns, scalable architecture, and real-world workflow modeling for animal shelter management systems.
