# \# Talabat



# A full-stack \*\*online food ordering platform\*\* built with \*\*Angular 16\*\* (Frontend) and \*\*ASP.NET Core Web API\*\* (Backend), using \*\*SQL Server\*\* database and integrated with \*\*Stripe Payment Gateway\*\*.  

# 

# The project is designed using \*\*Onion Architecture\*\*, \*\*Dependency Injection\*\*, and clean code practices to ensure maintainability, scalability, and readability for any developer.

# 

# ---

# 

# \## Features

# 

# \### Product Module

# \- Browse all products with \*\*filtering, sorting, and pagination\*\*

# \- View details of a single product

# \- Add new products to the database and update existing ones

# 

# \### Order Module

# \- Retrieve all orders for a specific user

# \- Retrieve a single order

# \- View available delivery methods with pricing

# 

# \### Basket Module

# \- Temporary in-memory storage for the shopping cart

# \- Add/remove products and adjust quantities

# \- Checkout process integrated with payment

# 

# \### Checkout \& Payment

# \- Step 1: Select delivery address

# \- Step 2: Choose delivery method and cost

# \- Step 3: Payment via \*\*Stripe\*\* without leaving the site

# \- Payment result page for success/failure feedback

# 

# \### Security Module

# \- User authentication: Register / Login

# \- Protected routes to ensure only authenticated users can checkout

# 

# \### Error Handling

# \- Centralized error handling using custom middleware

# \- Clear and consistent API responses for unexpected errors

# 

# ---

# 

# \## Architecture \& Design Patterns

# \- \*\*Onion Architecture\*\* for separation of concerns

# \- \*\*Repository Pattern\*\* for data access

# \- \*\*Service Layer\*\* for business logic

# \- \*\*Specification Pattern\*\* for flexible queries

# \- \*\*Dependency Injection\*\* for modularity and testability

# \- Clean code principles to ensure readability and maintainability

# 

# ---

# 

# \## Tech Stack

# 

# \*\*Frontend:\*\*  

# \- Angular 16, TypeScript, SCSS

# 

# \*\*Backend:\*\*  

# \- ASP.NET Core Web API  

# \- Entity Framework Core  

# \- SQL Server  

# \- Libraries: AutoMapper, JWT Bearer, Stripe.net, StackExchange.Redis

# 

# \*\*Other:\*\*  

# \- Payment Integration: Stripe  

# \- In-memory basket storage for efficiency  

# 

# ---

# 

# \## Setup \& Installation

# 

# 1\. Clone the repository:

# ```bash

# git clone https://github.com/YOUR\_USERNAME/Talabat.git

# 

