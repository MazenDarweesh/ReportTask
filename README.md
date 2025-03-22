# Student Report Generation API

## Overview
  The Student Report Generation API is a robust and scalable solution designed to generate student reports in various formats (PDF, Excel, JSON). This API is built using ASP.NET Core and targets .NET 8. It leverages Entity Framework Core for data access and includes a well-structured service layer to handle business logic. The project is designed with extensibility and maintainability in mind, making it easy to add new features or modify existing ones.

## Features
  •	Generate Reports: Generate student reports in PDF, Excel, and JSON formats.
  •	Dynamic Filtering: Filter reports based on school, academic year, grade, and class.
  •	Extensible Export Strategies: Easily add new export formats by implementing the IExportStrategy interface.
  •	Detailed School Headers: Include detailed school headers at the top of each report.
  •	Dependency Injection: Utilizes ASP.NET Core's built-in dependency injection for better testability and maintainability.
  •	Swagger Integration: Integrated with Swagger for easy API documentation and testing.

## Project Structure
ReportTask/
├── Application/
│   ├── Services/
│   │   └── StudentReportService.cs
├── Domain/
│   ├── Entities/
│   │   ├── Student.cs
│   │   ├── StudentAcademicYear.cs
│   │   ├── School.cs
│   │   ├── Classroom.cs
│   │   ├── Grade.cs
│   │   ├── Semester.cs
│   │   ├── AcademicYear.cs
│   │   └── Section.cs
│   ├── Enums/
│   │   └── ExportFormat.cs
├── Infrastructure/
│   ├── ExportStrategies/
│   │   ├── PdfExportStrategy.cs
│   │   ├── ExcelExportStrategy.cs
│   │   └── JsonExportStrategy.cs
│   ├── Interfaces/
│   │   └── IExportStrategy.cs
│   ├── Repositories/
│   │   └── StudentReportRepository.cs
├── ReportTask/
│   ├── Controllers/
│   │   └── StudentReportController.cs
│   ├── Program.cs
│   └── appsettings.json


## Key Components
  1.	Application Layer: Contains the service interfaces and implementations.
  •	StudentReportService: Implementation of the student report service, handling business logic and data processing.
  2.	Domain Layer: Contains the core entities and enums.
  •	Entities: Student, StudentAcademicYear, School, Classroom, Grade, Semester, AcademicYear, Section.
  •	Enums: ExportFormat.
  3.	Infrastructure Layer: Contains the export strategies and repository interfaces.
  •	IExportStrategy: Interface for export strategies.
  •	PdfExportStrategy, ExcelExportStrategy, JsonExportStrategy: Implementations of the export strategies for different formats.
  4.	Presentation Layer: Contains the API controllers.
  •	StudentReportController: API controller for handling report generation requests.
  5.	Configuration:
  •	Program.cs: Configures services, middleware, and the HTTP request pipeline.
  •	appsettings.json: Configuration file for application settings.

## Export Strategies
  The export strategies are designed to be easily extensible. Each strategy implements the IExportStrategy interface, which defines methods for exporting data, getting the content type, and getting the file name.
  •	PdfExportStrategy: Uses iTextSharp to generate PDF reports.
  •	ExcelExportStrategy: Uses EPPlus to generate Excel reports. The license context is set to NonCommercial to comply with EPPlus licensing.
  •	JsonExportStrategy: Uses System.Text.Json to generate JSON reports.

## Service Layer
The StudentReportService handles the core business logic. It fetches data from the database using AppDbContext and processes it to generate the report data structure. It then uses the appropriate export strategy to generate the report in the requested format.

## Controller
The StudentReportController handles API requests. It provides endpoints for fetching report data and exporting reports in different formats. The controller interacts with the StudentReportService to process the requests and return the appropriate responses.

## Configuration
  The Program.cs file configures the services and middleware for the application. It sets up dependency injection, configures JSON serialization options, and adds Swagger for API documentation.

## Getting Started
  ## Prerequisites
  •	.NET 8 SDK
  •	SQL Server (or any other supported database)

## Setup
1.	Clone the repository:
2.	Configure the database connection in appsettings.json:
3.	Run the application:
4.	Access the Swagger UI at https://localhost:5001/swagger to explore the API endpoints.

## Summary of the API Cycle
1.	User hits the API endpoint: The request is routed to the StudentReportController.
2.	Controller processes the request: The controller calls the StudentReportService to fetch data and generate the report.
3.	Service layer fetches data: The StudentReportService uses AppDbContext to query the database.
4.	Export strategies generate the report: Based on the requested format, the appropriate export strategy is used to generate the report.
5.	Response is sent back to the user: The controller sends the generated report back to the user.
