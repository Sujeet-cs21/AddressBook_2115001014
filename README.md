# AddressBook_2115001014

## Section 1
### UC1
    -Configured Database and Application Settings

### UC2
    -Implemented Address Book API controller

## Section 2
### UC1
    -Created Model Class (AddressBookEntity)
    -Implemented DTO Class to structure API responses
    -Used AutoMapper for Model-DTO conversion
    -Validated DTOs using FluentValidation
### UC2
    -Created IAddressBookService interface
    -Implemented AddressBookService: 
        -Move logic from controller to service layer
        -Handle CRUD operations in business logic
    -Injected Service Layer into Controller using Dependency Injection

## Section 3
### UC1
    -Created User Model & DTO
    -Implement Password Hashing (Salting)
    -Generated JWT Token on successful login
    -Stored User Data in MS SQL Database
    -Endpoints:
        -POST /api/auth/register
        -POST /api/auth/login
### UC2
    -Generated Reset Token
    -Sent Password Reset Email (SMTP)
    -Verify token & allow password reset
    -Endpoints:
        -POST /api/auth/forgot-password
        -POST /api/auth/reset-password

