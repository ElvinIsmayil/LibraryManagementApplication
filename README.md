```markdown
# 📚 Library Management Application

## 📋 Overview
The **Library Management Application** is a console-based application designed to help manage library resources efficiently. This application allows users to perform various operations such as adding, removing, and searching for books, as well as managing library members. The goal of this project is to provide an intuitive interface for library management tasks, making it easier for librarians and staff to handle daily operations.

## ✨ Features
- 📖 **Book Management**: Add, remove, and search for books in the library.
- 👤 **Member Management**: Register new members and manage existing member details.
- 🔍 **Search Functionality**: Quickly find books or members using search filters.
- 📅 **Loan Management**: Track book loans and returns.
- 🔒 **User Authentication**: Secure access for library staff with login functionality.

## 🚀 Installation
To set up the Library Management Application on your local machine, follow these steps:

1. **Clone the Repository**
   ```bash
   git clone https://github.com/ElvinIsmayil/LibraryManagementApplication.git
   ```

2. **Navigate to the Project Directory**
   ```bash
   cd LibraryManagementApplication
   ```

3. **Open the Solution File**
   - Open the solution file `Project - ConsoleApp (Library Management Application).sln` in your preferred IDE (e.g., Visual Studio).

4. **Restore NuGet Packages**
   - In Visual Studio, right-click on the solution in Solution Explorer and select **Restore NuGet Packages**.

5. **Build the Project**
   ```bash
   dotnet build
   ```

6. **Run the Application**
   ```bash
   dotnet run --project "Project - ConsoleApp (Library Management Application)"
   ```

## 🔧 Configuration
The application can be configured using the `appsettings.json` file located in the project directory. Below is an example configuration:

```json
{
  "LibrarySettings": {
    "MaxBooksPerMember": 5,
    "LoanDurationDays": 14
  }
}
```

**Configuration Options:**
| Option                   | Description                              |
|--------------------------|------------------------------------------|
| `MaxBooksPerMember`      | Maximum number of books a member can borrow. |
| `LoanDurationDays`       | Duration in days for which a book can be loaned. |

## 📊 Usage Examples
Here are a few examples of how to interact with the Library Management Application:

### 1. Adding a New Book
```csharp
library.AddBook(new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565" });
```
*This code snippet adds a new book to the library's collection.*

### 2. Searching for a Member
```csharp
var member = library.SearchMember("John Doe");
```
*This searches for a member named "John Doe" in the library database.*

### 3. Loaning a Book
```csharp
library.LoanBook("9780743273565", "John Doe");
```
*This loans the book with the specified ISBN to the member named "John Doe".*

## 📘 API Reference
### `Library`
- **Methods**:
    - `AddBook(Book book)`
        - **Parameters**: `book` - The book to be added.
        - **Returns**: `void`
    - `SearchMember(string name)`
        - **Parameters**: `name` - The name of the member to search for.
        - **Returns**: `Member` - The found member object.
    - `LoanBook(string isbn, string memberName)`
        - **Parameters**: 
            - `isbn` - The ISBN of the book to loan.
            - `memberName` - The name of the member borrowing the book.
        - **Returns**: `void`

## 🧩 Architecture
The Library Management Application follows a simple architecture pattern:

```
+-------------------+
|   User Interface   |
+-------------------+
          |
          v
+-------------------+
|     Controller     |
+-------------------+
          |
          v
+-------------------+
|      Services      |
+-------------------+
          |
          v
+-------------------+
|     Repository     |
+-------------------+
```

## 🔒 Security Considerations
- Ensure that sensitive data, such as user credentials, are stored securely.
- Implement proper input validation to prevent SQL injection attacks.
- Use secure password hashing mechanisms for user authentication.

## 🧪 Testing
To run the tests for the Library Management Application, follow these steps:

1. **Navigate to the Test Project Directory**
   ```bash
   cd LibraryManagementApplication.Tests
   ```

2. **Run the Tests**
   ```bash
   dotnet test
   ```

## 🤝 Contributing
Contributions are welcome! To contribute to the Library Management Application:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature/YourFeature`).
3. Make your changes and commit them (`git commit -m 'Add some feature'`).
4. Push to the branch (`git push origin feature/YourFeature`).
5. Open a pull request.

## 📝 License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
```
