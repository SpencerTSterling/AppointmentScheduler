# Appointment Scheduler
![image](https://github.com/user-attachments/assets/59052076-787e-4a66-a1ed-608488e6f3c0)

The **Appointment Scheduler** is a Windows Forms application developed as part of a school project. Written in C# for a fictional global consulting organization, this tool helps employees manage appointments with customers. The application features login authentication, customer and appointment management, and calendar views. 

## Scenario

You are working for a software company that has been contracted to develop a **GUI-based scheduling desktop application.** The contract is with a global consulting organization that conducts business in multiple languages and has main offices in Los Angeles, US; New York, US; Toronto, Canada; Vancouver, Canada; and Olso, Norway. 

The consulting organization has provided a MySQL database from which the application must pull data. *The database is used for other systems, so its structure cannot be modified.*

## Features
The organization outlined specific business requirements met by the application.

### Secure User Authentication
* Username and password verification against a MySQL database.
* Multilingual login support with automatic language detection based on user location *(English/Spanish)*.
* Logs both successful and failed login attempts for user activity tracking. 
### Appointment Management 
* Create, update, and delete appointments linked to specific customers.
* Prevents scheduling conflicts and enforces business hours *(9AM - 5PM EST, Mon-Fri)*.
* Provides alerts for upcoming appointments within 15 minutes of logging in. 
### Customer Data 
* Customer management system capabilities.
* Field validation for name, address, and phone number *(only allows digits and dashes).*
* Prevents duplicate or invalid entries with structured error handling. 
### Calendar Views
* View appointments by day, week, or month. 
* Filters to display all, weekly, or monthly appointments. 
### Time Zone Awareness
* Stores all appointment times in UTC and automatically concerts time based on the user's system time zones. 
### Activity Logging
*   Tracks every login attempt in `Login_History.txt`.
*   Logs include timestamps for user activity tracking.

## System Requirements
### Software

-   **Operating System**: Windows (Windows 10 and above recommended)
-   **.NET Framework**: .NET Framework 4.7.2 or higher
-   **MySQL**: MySql.Data version 8.3.0 to connect to MySQL database. 
### Hardware
While hardware specifications may vary, the application should work on most systems with the following recommended minimum configuration:
-   **Processor**: 1.5 GHz or faster processor
-   **RAM**: 2 GB or more
-   **Disk Space**: 200 MB of available storage
-   **Screen Resolution**: 1280x720 or higher
