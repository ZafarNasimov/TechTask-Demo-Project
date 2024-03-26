This repository contains an ASP.NET solution comprising an ASP.NET Web API project and an ASP.NET MVC Web Application project. The solution is designed to manage product data through CRUD operations exposed via RESTful API endpoints and interact with the data through a web application frontend.

Projects
Web API Service (ASP.NET Core):
Responsible for exposing RESTful API endpoints to manage data in the Product table.
Implements CRUD operations (Create, Read, Update, Delete) for the Product entity.
Utilizes ASP.NET Core and Entity Framework Core for handling HTTP requests and database interaction.

MVC Web Application (ASP.NET Core):
Serves as the frontend for managing product data.
Consumes RESTful API endpoints provided by the Web API Service to perform CRUD operations on the Product entity.
Implements views and controllers to interact with the API endpoints and display product data to users.
Provides functionality for listing products, adding new products, editing existing products, and deleting products.
Includes server-side filtering for product names and utilizes modal dialogs for adding/editing products.
