# PetConnect - Animal Shelter Management System

[![Customer Site](https://img.shields.io/badge/Customer_Site-Live-brightgreen)](https://youtu.be/U_MHaU1u62k)
[![Staff Portal](https://img.shields.io/badge/Staff_Portal-Repo-blue)](https://github.com/TanyaDThomas/PetConnect)
[![Azure](https://img.shields.io/badge/Hosted_on-Azure-0078D4)](https://azure.microsoft.com)

Full-stack ASP.NET Core MVC platform for multi-location animal shelters. Features role-based authorization (Admin, Manager, Staff), Clean Architecture, EF Core, and comprehensive shelter operations.

**Demo Video - Click Below**  
[![PetConnect Demo](https://img.youtube.com/vi/gXAyRN5UgeQ/0.jpg)](https://youtu.be/gXAyRN5UgeQ)

Was hosted on Azure Free Tier .

    
---

## Key Features
- Role-based access control across Admin, Manager, and Staff roles
- Multi-tenant support for multiple shelter locations
- Full CRUD operations for animals, adopters, adoptions, and payments
- Dashboard with analytics and reporting
- Notes system attached to records
- Responsive UI with Bootstrap

---

## Frontend Integration
This backend connects to the [PetConnect Frontend API](https://github.com/TanyaDThomas/PetConnectFrontend) using HttpClient.

---

## Role Permissions
| Feature              | Staff | Manager | Admin |
|----------------------|-------|---------|-------|
| Manage Animals       | Yes   | Yes     | Yes   |
| Manage Adoptions     | Yes   | Yes     | Yes   |
| Manage Payments      | Yes   | Yes     | Yes   |
| Manage Staff         | No    | Yes     | Yes   |
| Manage Shelters      | No    | No      | Yes   |

---

## Screenshots & GIFs
![Animal Management](https://github.com/TanyaDThomas/PetConnect/blob/main/images/staff.gif?raw=true)
![Adoption Workflow](https://github.com/TanyaDThomas/PetConnect/blob/main/images/adoption.gif?raw=true)
![Admin View](https://github.com/TanyaDThomas/PetConnect/blob/main/images/admin.gif?raw=true)
![Dashboard](images/dashboard.png)
![Admin Management](images/admin.png)
![Swagger Animal Api](images/Swagger.png)

---

## Architecture
Layered architecture inspired by Clean Architecture:
- **Presentation**: MVC Controllers & Razor Views
- **Application**: Services & Business Logic
- **Domain**: Entities & Interfaces
- **Infrastructure**: EF Core & SQL Server

**Customer Portal**: Separate Razor Pages frontend → [PetConnectFrontend](https://github.com/TanyaDThomas/PetConnectFrontend)

---

## Technologies
- ASP.NET Core MVC & Web API
- Entity Framework Core
- SQL Server
- ASP.NET Identity (RBAC)
- Bootstrap 5
- Swagger (OpenAPI)

---

## Author
Built by Tanya Thomas as a portfolio project showcasing backend architecture and real-world workflow management.

[Main GitHub Profile](https://github.com/TanyaDThomas)









