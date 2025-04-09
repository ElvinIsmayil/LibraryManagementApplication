
# 📚 Library Management Application

## 🌟 Overview
The **Library Management Application** is a powerful console-based system built with C# and Entity Framework Core. It's designed to help libraries efficiently manage their books, authors, borrowers, and loans through an intuitive interface.

## ✨ Key Features
- 📖 **Book Management**: Create, update, delete and filter books
- ✍️ **Author Management**: Manage author information and book relationships
- 👥 **Borrower System**: Register and track library members
- 🔄 **Loan Processing**: Handle book borrowing and returns
- ⏰ **Overdue Tracking**: Monitor late returns
- 📊 **Statistics**: View most borrowed books and borrower activity

## 🔧 Tech Stack
- 🖥️ **.NET 8.0** Framework
- 🗃️ **Entity Framework Core 8.0** ORM
- 🛢️ **SQL Server** Database
- 🧩 **Repository Pattern** Architecture

## 📋 Data Model
```
📦 BaseEntity
 ┣ 🔑 Id
 ┣ 🗑️ IsDeleted
 ┣ 📅 CreatedAt
 ┗ 🔄 UpdatedAt

📕 Book ← BaseEntity
 ┣ 📝 Title
 ┣ 📄 Description
 ┣ 📆 PublishedYear
 ┗ 👤 Authors (M:M)

👤 Author ← BaseEntity
 ┣ 📝 Name
 ┗ 📚 Books (M:M)

👥 Borrower ← BaseEntity
 ┗ 📝 Personal information

📝 Loan ← BaseEntity
 ┗ 📚 Loan details

📑 LoanItem ← BaseEntity
 ┗ 📕 Individual book loan record
```

## 🚀 Getting Started

### 📋 Prerequisites
- ✅ .NET 8.0 SDK or newer
- ✅ SQL Server (LocalDB or full instance)
- ✅ IDE (Visual Studio 2022 recommended)

### ⚙️ Installation
```bash
# Clone this repository
git clone https://github.com/YourUsername/LibraryManagementApplication.git

# Navigate into the project directory
cd LibraryManagementApplication

# Build the project
dotnet build

# Run the application
dotnet run --project "Project - ConsoleApp (Library Management Application)"
```

## 📱 Usage Guide

### 🧭 Main Menu
The application presents an easy-to-navigate menu:
1. 👤 Manage Authors
2. 📚 Manage Books
3. 👥 Manage Borrowers
4. 📤 Borrow Book
5. 📥 Return Book
6. 📊 Show Most Borrowed Books
7. ⏰ Show Late Borrowers
8. 👥 Show All Borrowers
9. 🔍 Apply Filters for Books
Esc. 🚪 Exit Application

## 🏗️ Architecture
```
🎮 UI Layer (Console)
   ↓
💼 Service Layer (Business Logic)
   ↓
🗄️ Repository Layer (Data Access)
   ↓
🛢️ Database (SQL Server)
```

## 🛠️ Future Enhancements
- 🔐 User authentication system
- 📊 Enhanced reporting capabilities
- 💰 Fine calculation for late returns
- 🖥️ GUI interface
- 🔖 Book reservation system

## 👨‍💻 Contribution Guidelines
Contributions are welcome! To contribute:
1. Fork the repository
2. Create a new branch (`git checkout -b feature/your-feature`)
3. Commit your changes (`git commit -m 'Add some feature'`)
4. Push to the branch (`git push origin feature/your-feature`)
5. Open a Pull Request

## 📄 License
This project is licensed under the MIT License - see the LICENSE file for details.

## 📞 Contact
If you have any questions or suggestions, please feel free to reach out!

---

⭐ Don't forget to star this repo if you find it useful! ⭐
