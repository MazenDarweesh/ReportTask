Summary
1.	User hits the API endpoint: The request is routed to the StudentReportController.
2.	Controller processes the request: The controller calls the StudentReportService to fetch data and generate the report.
3.	Service layer fetches data: The StudentReportService uses AppDbContext to query the database.
4.	Export strategies generate the report: Based on the requested format, the appropriate export strategy is used to generate the report.
5.	Response is sent back to the user: The controller sends the generated report back to the user.
