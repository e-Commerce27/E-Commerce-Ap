E-Commerce ASP.Net core(MVC)  project for E-commerce Market.

- Project name : 

- our frattures till now is that we have : Admin Dashboard

1. As an admin user, I would like to have a dashboard where I can see a list of product categories.
2. As an admin user, I would like to view a detail page for each category so that I can eventually edit its data or delete it.
3. As an admin user, I would like to see a list of the products assigned to a category on the category details page.
4. As an admin user, I would like a detail page for each product so that I can eventually edit its data or delete it.
5. As an administrator I would like add and save a new category so that I can expand my product lines.
6. As an administrator I would like add and save a new product so that I can expand my inventory.
7. As an administrator I would like to associate a product to a category so that my users can more easily browse our inventory.
8. As an administrator I would like to be able to delete products and categories as needed.
9. As an administrator I would like to be able to edit/modify categories so that I can change my storefront structure in real time.
10. As an administrator I would like to be able to edit/modify products so that I can change my inventory in real time.

# Features
- User Authentication (Registration, Login, Logout)
- Role-based Access Control (Administrator, Editor)
- Product Management (Add, Edit, Delete)
- Image Upload to Azure Blob Storage

# Technologies Used
- ASP.NET Core
- Entity Framework Core
- Identity Framework
- Azure Blob Storage
- HTML/CSS
- Bootstrap
- C#
- Razor Pages

# File Structure
- **Controllers**: Contains the controller classes responsible for handling requests and returning responses.
- **Models**: Contains the model classes representing the data entities used in the application.
- **DTO**: Contains Data Transfer Objects used for data validation and transport.
- **Views**: Contains the Razor views responsible for rendering HTML pages.
- **Services**: Contains the services responsible for business logic.
- **Data**: Contains the database context and migrations.

# Uploading Images to Azure Blob Storage
Azure Blob Storage is a cloud-based object storage solution provided by Microsoft Azure. It allows you to store and retrieve large amounts of unstructured data, including images.

## Steps to Upload Images to Azure Blob Storage
1. **Create an Azure Storage Account**:
   - Log in to the Azure portal, navigate to Storage Accounts, and create a new account.

2. **Get the Connection String**:
   - Obtain the connection string for your storage account. This will be used to authenticate your application with the storage account.

3. **Install the Azure.Storage.Blobs NuGet Package**:
   - Use the `Azure.Storage.Blobs` library to interact with Azure Blob Storage. You can install it using NuGet Package Manager.

4. **Initialize a BlobContainerClient**:
   - Create a `BlobContainerClient` object to represent the container where your images will be stored.

5. **Upload an Image**:
   - Use the `UploadAsync` method to upload an image to the storage account.
     - `blobName`: The name you want to give to the blob (image).
     - `imageStream`: A stream containing the image data.

6. **Access the Image**:
   - To access the uploaded image, you can use the URL of the blob. This URL is typically in the format `https://<account-name>.blob.core.windows.net/<container-name>/<blob-name>`.

   - Remember to handle exceptions and errors that may occur during the upload process, and secure your storage account credentials to prevent unauthorized access.

   - By using Azure Blob Storage, you can efficiently store and serve images for your web application, benefiting from scalability, durability, and performance provided by Azure's infrastructure.
