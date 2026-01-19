# Library Management System API

A RESTful Web API for managing library books with JWT-based authentication built with ASP.NET Core.

## Features

- User registration and login
- JWT-based authentication
- CRUD operations for books (Create, Read, Update, Delete)
- Search books by title or author
- Entity Framework Core with MSSQL
- Database seeding with sample data
- Clean architecture with layered design
- Comprehensive error handling
- Swagger UI for API documentation

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: Microsoft SQL Server
- **ORM**: Entity Framework Core
- **Authentication**: JWT Bearer Token
- **Documentation**: Swagger/OpenAPI


## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/Juninhotech/LibraryManagementAPI.git
cd LibraryManagementAPI
```

### 2. Configure Database Connection

Update the connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LibraryManagementDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

**For SQL Server:**
```json
"DefaultConnection": "Server=localhost;Database=LibraryManagementDb;User Id=sa;Password=YourPassword;TrustServerCertificate=true;"
```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Apply Database Migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

> **Note**: The database will be automatically seeded with sample books on the first run.

### 5. Run the Application

```bash
dotnet run
```

The API will start at:
- HTTP: `http://localhost:5226`

## Testing the API

### Using Swagger UI

1. Navigate to `http://localhost:5226/swagger`
2. Test endpoints directly from the browser

### Using Postman or cURL

#### 1. Register a New User

```bash
POST https://localhost:5226/api/auth/register
Content-Type: application/json

{
  "username": "testuser",
  "email": "test@example.com",
  "password": "Password123!"
}
```

**Response:**
```json
{
  "username": "testuser",
  "email": "test@example.com"
}
```

#### 2. Login

```bash
POST https://localhost:5226/api/auth/login
Content-Type: application/json

{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "testuser",
  "password": "Password123!"
}
```

#### 3. Get All Books (Requires Authentication)

```bash
GET https://localhost:5226/api/books
Authorization: Bearer <your-jwt-token>
```

#### 4. Search Books

```bash
GET https://localhost:5226/api/books?search=clean
Authorization: Bearer <your-jwt-token>
```

#### 5. Get Book by ID

```bash
GET https://localhost:5226/api/books/1
Authorization: Bearer <your-jwt-token>
```

#### 6. Create a New Book

```bash
POST https://localhost:5226/api/books
Authorization: Bearer <your-jwt-token>
Content-Type: application/json

{
  "title": "Domain-Driven Design",
  "author": "Eric Evans",
  "isbn": "978-0321125217",
  "publishedDate": "2003-08-20T00:00:00"
}
```

#### 7. Update a Book

```bash
PUT https://localhost:5226/api/books/1
Authorization: Bearer <your-jwt-token>
Content-Type: application/json

{
  "title": "Clean Code - Updated",
  "author": "Robert C. Martin",
  "isbn": "978-0132350884",
  "publishedDate": "2008-08-01T00:00:00"
}
```

#### 8. Delete a Book

```bash
DELETE https://localhost:5226/api/books/1
Authorization: Bearer <your-jwt-token>
```

## API Endpoints

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/register` | Register new user | No |
| POST | `/api/auth/login` | Login user | No |

### Books

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/books` | Get all books | Yes |
| GET | `/api/books?search={term}` | Search books | Yes |
| GET | `/api/books/{id}` | Get book by ID | Yes |
| POST | `/api/books` | Create new book | Yes |
| PUT | `/api/books/{id}` | Update book | Yes |
| DELETE | `/api/books/{id}` | Delete book | Yes |

## Authentication Flow

1. Register a new user or login with existing credentials
2. Receive a JWT token in the response
3. Include the token in the `Authorization` header for all protected endpoints:
   ```
   Authorization: Bearer <your-jwt-token>
   ```

## Database Schema

### Books Table
- `Id` (int, Primary Key)
- `Title` (nvarchar(200), Required)
- `Author` (nvarchar(100), Required)
- `ISBN` (nvarchar(20), Required, Unique)
- `PublishedDate` (datetime2)

### Users Table
- `Id` (int, Primary Key)
- `Username` (nvarchar(50), Required, Unique)
- `Email` (nvarchar(100), Required, Unique)
- `PasswordHash` (nvarchar(max), Required)
- `CreatedAt` (datetime2)

## Sample Data

The database is seeded with 5 programming books:
1. Clean Code - Robert C. Martin
2. The Pragmatic Programmer - Andrew Hunt and David Thomas
3. Design Patterns - Gang of Four
4. C# in Depth - Jon Skeet
5. Refactoring - Martin Fowler

## Error Handling

The API returns appropriate HTTP status codes:
- `200 OK` - Successful GET/PUT requests
- `201 Created` - Successful POST requests
- `204 No Content` - Successful DELETE requests
- `400 Bad Request` - Invalid input data
- `401 Unauthorized` - Missing or invalid token
- `404 Not Found` - Resource not found
- `500 Internal Server Error` - Server errors

## Security Considerations

- Passwords are hashed using Bcrypt
- JWT tokens expire after 60 mins (configurable)
- All book endpoints require authentication
- SQL injection protection via EF Core parameterized queries

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure database exists or migrations have been applied

### JWT Token Issues
- Ensure the token is included in the Authorization header
- Check token expiration (default 24 hours)
- Verify the JWT secret key matches in configuration

## Additional Notes

- The project follows Repository and Service patterns for clean separation of concerns
- Dependency injection is used throughout the application
- The API uses DTOs to separate domain models from API contracts
- Comprehensive logging is implemented for debugging

## Future Enhancements

- Add pagination for book listing
- Implement role-based authorization (Admin/User)
- Add book categories and tags
- Implement book borrowing system
- Add unit and integration tests
- Implement refresh tokens
- Add API rate limiting
