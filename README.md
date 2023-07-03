# Library Management

## Project Summary
This project is a software developed for library automation. It enables library officials to manage books, track members, and perform loan transactions. The project aims to facilitate the management of library operations in a digital environment and store them in a database to ensure more efficient workflow.

## Technologies Used
- .NET Core 6 MVC
- PostgreSQL (Database)
- AWS S3 (File Storage)
- AWS EC2 (Virtual Server)
- Docker (Containerization)
- MediatR (CQRS Pattern)
- FluentValidation (Request Validation)
- Serilog (Used for logging operations. Logs are stored in the database logs table.)
  
![](https://d2an46wc102jd8.cloudfront.net/files/tgicons.png)

## Installation and Running
1. In order to start the project, the migration folder needs to be deleted first.
2. If PostgreSQL database will be used, the access information for the relevant database should be entered in the `appsettings.json` file. If PostgreSQL will not be used, another EF Core compatible database library should be installed, and the necessary service configurations should be made in the Data layer.
3. If AWS S3 Storage service will be used for uploading library members' and books' photos, you need to enter the information provided by AWS in the `appsettings.json` file. If a different service will be used (e.g., Azure), the relevant libraries in the Business layer need to be updated.

## Project Structure
The project is organized according to N-Tier architecture principles and includes layers separated by functionality:

- Business: Contains the code for the project's business logic and services.
- Core: Contains commonly used classes and middlewares.
- Data: Represents the data access layer and interacts with the PostgreSQL database using Microsoft Entity Framework ORM library.
- Domain: Contains the core abstract classes and interfaces for the project.
- MVC: Handles user requests, directs them to the relevant services, and provides responses to the user.

## Testing the Project
The project can be accessed through the following URL:
[http://ec2-54-93-52-253.eu-central-1.compute.amazonaws.com/](http://ec2-54-93-52-253.eu-central-1.compute.amazonaws.com/)

## Database Diagram
![Database Diagram](https://d2an46wc102jd8.cloudfront.net/files/diagrams.png)

## Important Notes
Instead of code examples, detailed explanations regarding the codes are provided within the scope of the project files.
