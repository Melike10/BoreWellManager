# Bore Well Manager Multi Layers Web Api

This project, created as part of the Patika Dev+ final assignment, is a multi-layered Web API application developed for a company that drills borewells. The **BoreWellManager** API manages entities like land, users, documents, and payments, specifically focusing on managing landowners, tenants, and company employees, with added features for authentication and authorization.

## Project Overview

**BoreWellManager** is a multi-layered web API solution designed for a borewell drilling company. This API enables the management of various data such as landowners and tenants, while offering users role-based authorization. JWT (JSON Web Token) authentication provides secure access, while Entity Framework and middleware layers deliver a robust backend architecture.

### Technologies and Tools Used

- **ASP.NET Core Web API** - For building Web APIs
- **Entity Framework Core (Code First)** - Database management
- **JWT (JSON Web Token)** - For authentication and authorization
- **ASP.NET Core Identity or Custom User Management** - For user management
- **Repository Pattern** - For data handling
- **Unit of Work** - Manages data transactions
- **Dependency Injection (DI)** - Dependency management
- **Middleware** - Custom middlewares
- **Action Filters** - For additional processing
- **Model Validation** - To ensure data integrity

## Project Architecture

This project is structured into three primary layers:

1. **Business Layer**: Manages business rules and operations. Each `Controller` has an associated `Service` (e.g., `ILandService`), which defines CRUD operations as asynchronous `Task` methods.
2. **Data Layer**: Contains entity definitions, enums, repository, and unit of work patterns. Migrations are also executed in this layer.
3. **Web API Layer**: Defines API endpoints, allowing user interaction with the system.

## API Features

The following API operations are supported:

- **GET**: Retrieve and view records
- **POST**: Add new records
- **PUT**: Update existing records
- **PATCH**: Partially update records
- **DELETE**: Remove records

### Authentication and Authorization

- **Authentication**: Users are authenticated using JWT tokens.
- **Authorization**: Access is role-based, granting specific privileges to different user roles.
- **User Management**: Implemented using ASP.NET Core Identity or Custom User Management.

## Database Tables and Relationships

![tables-relation](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/diagram3.png)

### 1. Users

The **Users** table stores user information.

- **Id**: Unique identifier (Primary Key).
- **TC**: User's ID number.
- **Name**: User's name.
- **Phone**: User's phone number.
- **Address**: User's address.
- **UserType**: User role (e.g., Employee, Owner, Tenant).
- **IsResponsible**: Indicates if the user has a responsibility.
- **CreateDate**: Date of creation.
- **ModifiedDate**: Last updated date.
- **IsDeleted**: Indicates if the user is deleted.

### 2. Land

The **Land** table holds information about the land.

- **Id**: Unique identifier (Primary Key).
- **City**: City where the land is located.
- **Town**: Town where the land is located.
- **Street**: Neighborhood of the land.
- **Block**: Block number.
- **Plot**: Plot number.
- **Location**: Specific location.
- **LandType**: Type of land.
- **HasLien**: Indicates if there is a lien on the land.
- **IsCksRequired**: Indicates if CKS is required.
- **LienType**: Type of lien.
- **LandOwners**: Associated landowners (many-to-many relation with LandOwnersEntity).
- **Wells**: Associated wells (one-to-many relation with WellEntity).

### 3. LandOwners

The **LandOwners** table stores landowners' information.

- **LandId**: Associated land ID (Foreign Key).
- **UserId**: Landowner's user ID (Foreign Key).

### 4. Well

The **Well** table contains well information.

- **Id**: Unique identifier (Primary Key).
- **UserId**: User ID of the well owner (Foreign Key).
- **LandId**: Land ID where the well is located (Foreign Key).
- **XCordinat**: X-coordinate of the well.
- **YCordinat**: Y-coordinate of the well.
- **Debi**: Flow rate of the well.
- **StaticLevel**: Static level.
- **DynamicLevel**: Dynamic level.
- **Documents**: Associated documents (one-to-many relation with DocumentEntity).

### 5. Document

The **Document** table stores document details.

- **Id**: Unique identifier (Primary Key).
- **WellId**: Related well ID (Foreign Key).
- **PaymentId**: Related payment ID (Foreign Key).
- **InstitutionId**: Related institution ID (Foreign Key).
- **Type**: Type of document.
- **CustomerSubmissionDate**: Customer submission date.
- **InstitutionSubmissionDate**: Submission date to institution.
- **SignaturesReceived**: Status of signatures.
- **DeliveredToInstitution**: Indicates if delivered to institution.
- **IsLienCertificate**: Indicates if the document is a lien certificate.
- **DocumentFee**: Fee for the document.
- **FeeReceived**: Indicates if fee is received.
- **CreatedBy**: User who created the document.
- **ModifiedBy**: User who last modified the document.

### 6. Payment

The **Payment** table holds payment details.

- **Id**: Unique identifier (Primary Key).
- **DocumentId**: Related document ID (Foreign Key).
- **DepositorFullName**: Full name of the depositor.
- **PaymentDate**: Date of payment.
- **TotalAmount**: Total payment amount.
- **RemaningAmount**: Remaining payment amount.
- **EmployeeWhoReceivedPayment**: Employee who received the payment.
- **IsInstallmentPayment**: Indicates if it is an installment payment.
- **InstallmentAmount**: Amount per installment.
- **LastPaymentDate**: Date of the last payment.

### 7. Institution

The **Institution** table stores institution details.

- **Id**: Unique identifier (Primary Key).
- **Name**: Name of the institution.
- **City**: City of the institution.
- **Town**: Town of the institution.
- **Documents**: Documents related to the institution.

## Endpoints

### 1. Auth
![Auth](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/auth.png)

Users can register in the system through `register`. Only `Employee` or `IsResponsible` users have access to certain endpoints.

### 2. Users
![Users](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Users.png)

Each user can view their information by providing their ID or TC. Only users of type `Employee` can view all users.

### 3. Lands
![Lands](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Lands.png)

- **Land Information**: Each user can access land information, filtering based on location or land type. Employees with **IsResponsible** status can view and manage all lands in the system.

### 4. Wells
![Wells](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Wells.png)

- **Well Information**: Users can retrieve details about wells on their associated lands. Employees with **IsResponsible** status have full access to all well records.

### 5. Documents
![Documents](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Documents.png)

- **Document Management**: Users can upload and view documents related to their lands or wells. Document status can be tracked, including customer submission date, institution submission date, and signature status. Fees and lien certificates can also be managed within this section.

### 6. Payments
![Payments](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Payments.png)

- **Payment Tracking**: Users can record and track payments associated with documents. This includes installment payments, remaining balance, and last payment date. Employees who are **IsResponsible** can view all payment records.

### 7. Institutions
![Institutions](https://github.com/Melike10/BoreWellManager/blob/25e854deb52161fff44e007b7196669c9f93e48e/Institutions.png)

- **Institution Information**: Users can access information about institutions involved with their land and well documents. This section includes details on institution name, city, and town, along with related documents.


