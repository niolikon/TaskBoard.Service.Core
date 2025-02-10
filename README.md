# TaskBoard.Service.Core
[![Build](https://github.com/niolikon/TaskBoard.Service.Core/actions/workflows/dotnet.yml/badge.svg)](https://github.com/niolikon/TaskBoard.Service.Core/actions)
[![Package](https://github.com/niolikon/TaskBoard.Service.Core/actions/workflows/publish-nuget.yml/badge.svg)](https://github.com/niolikon/TaskBoard.Service.Core/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](https://opensource.org/licenses/MIT)

Task Board Service Web API (.NET Case Study)

# Overview

📚 **TaskBoard.Service.Core** is a simple Web API project designed to manage Personal Tasks as a Todo list.
This project demonstrates fundamental software development concepts such as BDD (Behavior-Driven Development), unit testing, and clean architecture principles.

---

## 🚀 Features

- **Todo Management**: Manage To-dos with CRUD operations.
- **Dependency Injection**: Decouple components for better testability and maintainability.
- **Rest exceptions Management**: Centralize JSON error response management with RestControllerAdvice for better separation of concerns.

---

## 📖 User Stories

- 🆕 [Todo Marking](https://github.com/niolikon/TaskBoard.Service.Core/issues/1)

---

## 🛠️ Getting Started

### Prerequisites

- [.NET 8 or later](https://dotnet.microsoft.com/download)
- A text editor or IDE (e.g., [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/))

### Quickstart Guide

1. Clone the repository:
   ```bash
   git clone https://github.com/niolikon/TaskBoard.Service.Core.git
   cd TaskBoard.Service.Core
   ```

2. Restore dependencies
   ```bash
   dotnet restore
   ```
   
3. Run the project
   ```bash
   dotnet run
   ```

### Deploy on container

1. Configure credentials on a .env file as follows
   ```
    DB_NAME=todolist
    DB_PASSWORD=apppassword
    KEYCLOAK_DB_PASSWORD=supersecretkeycloak
    KEYCLOAK_ADMIN_PASSWORD=adminpassword
   ```

2. Create build artifact
   ```bash
   dotnet publish TaskBoard.Service.Core.Api/TaskBoard.Service.Core.Api.csproj -c Production -o ./output
   ```
   
3. Create project image
   ```bash
   docker build -t taskboard-service-core:latest .
   ```

4. Compose docker container
   ```bash
   docker-compose up -d
   ```

---

## 📬 Feedback

If you have suggestions or improvements, feel free to open an issue or create a pull request. Contributions are welcome!

---

## 📝 License

This project is licensed under the MIT License.

---
🚀 **Developed by Simone Andrea Muscas | Niolikon**

