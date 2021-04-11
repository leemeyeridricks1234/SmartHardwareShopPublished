# Smart Hardware Shop - Sample API

Technology Used
* Visual Studio 2019
* .NET 5.0 (most comfortable based on time constraints - can be ported to .NET Core)
* Web API Core
* Unit Tests
* Moq
* Bearer Security Tokens
* Swagger
* Postman
* SQL Server / TSQL
* Entity Framework


Getting Started
* Download .NET Solution locally
* Run SQL Seeding Script in Seed.sql (under API project) - will create required DBs and sample data
* Change connection string in appsettings.json to point to correct DB (under API project)
* Optional change secret key in appsettings.json
* Build Solution
* Run Solution - will open Swagger landing page by default (https://localhost:44308/swagger/index.html)


Solution Structure
* Startup - contains initialization code - register dependencies
* Setup using a combination of API controllers and DDD project structure
* Controllers - contains API methods with optional security authorization
* DTOs - contains DTOs for API requests and responses that are custom
* Models - contains the EF models used 
* Repositories - contains the DB access components for EF
* Responses - contains the generic PagedResponses and ServiceResponse classes used to provide consistency across responses
* Services - contains business services - orchestrates activities and encapsulates business rules
* Validators - contains the validators for API requests - validation logic centralized in one location


NOTES
* Not all validations are implemented (time constrained) - TODOs are specified where validators can be implemented
* Logging not implemented - can be done using DI
* Not all unit tests and integration tests are impelemented (time constrained)
* Async Tasks can be used for operations
* Not implemented: Additional shopping cart functions can be added (update basket)
* Not implemented: Transactions can be added in future
* Not implemented: Updating of Order status can be implemented in future
* ServiceResponse object can be enhanced to include Error list (not just message)
* Controller base class can be created to encapsulate common functions (getting userId, handling errors, etc)
* Passwords are current stored in plain text in DB - change to hashed

FUTURE DESIGN CHANGES
* Separate APIs in their own domain - allow APIs to be deployed/scaled separately - can be done using Cloud (Azure, AWS, etc) or Docker
* Not implemented: Caching can be implemented to improve performance for reads - recommended distributed cache and invalidate cache strategies
* Not implemented: 3rd party security providers - oAuth, OpenID, etc - allow for flexible login options
* Not implemented: Payments and Notifications can be implemented in the future - can be done via a Service Bus
* Traffic Manager can be implemented - improvement performance - deploy in regions closer to customers
* Load balancing across multiple deployments can be implemented - improved reliability, availability and performance
* Implement SQL replication for improved reliability and availability - Load balancer and SQL Replication
* CQRS design - separate between reads/writes - allow scaling of read operations to improve performance
* Host images and static content in CDN/Blob Storage to improve scalability and performance
* Full text search can be implemented for the website - exacmple: Azure Search or Elastic Search
* Monitoring and Insights can be implemented to monitor APIs and web sites
* Automated deployments can be implemented to roll out code to environments (CI/CD, Azure DevOps, etc)

APIs

Login/Auth
*  /api/Auth/Login - required to login and create a token to use (admin vs customer)
* Usernames and pwds stored in DB
* admin/admin - admin profile
* customer1/customer1 - customer profile

Product - Get Paged Results - /api​/Product​/GetAllPaged
* Implements paging on Product result set
* Security: Any one can query
* Example: 
* parameters: pageNumber, pageSize
```shell script
GET https://localhost:44308/api/Product/GetAllPaged?PageNumber=1&PageSize=2
```

Update Product - PUT /api/Product/{id}
* Update a product
* Security: Only admin users
* Requires Bearer Token in Headers
* First login with Admin user and copy token
* Postman - add Authorization Bearer token - Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxIiwidW5pcXVlX25hbWUiOiJhZG1pbiIsInJvbGUiOiJhZG1pbiIsIm5iZiI6MTYxNzgyNDE1MywiZXhwIjoxNjE3OTEwNTUzLCJpYXQiOjE2MTc4MjQxNTN9.qXFelqfsDvJoSEKYicwQ8hzQ4cgeJp9vvj4Gq3dLtXGs2H5XLK2rCbdf25Di5zgRwq0rlZU5XOI9c8svUBkG-w
```shell script
{
  "id": 0,
  "name": "string",
  "description": "string",
  "category": "string",
  "price": 0,
  "rrp": 0,
  "quantity": 0,
  "image": "string",
  "dateAdded": "2021-04-08T07:36:06.336Z",
  "userId": 0
}
```

Add to Cart 
* Only Customers
* Add product in cart for a user

View Cart Summary
* Only Customers
* View a summary of current cart for a user

Checkout
* Only Customers
* Creates an Order with Order Items linked to user
* Reduces quantity of available products
* Clears the cart for the user





