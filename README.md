# 🔐 TokenExample

A simple C# example demonstrating how **Access Token** and **Refresh Token** authentication works in a .NET application.

![C#](https://img.shields.io/badge/C%23-100%25-239120?style=flat-square&logo=csharp)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat-square&logo=dotnet)

---

## 📖 About

This project shows how to implement a basic JWT-based authentication flow using access tokens and refresh tokens. It's intended as a learning reference for developers who want to understand how token-based auth works under the hood.

---

## 🔑 Key Concepts

- **Access Token** — A short-lived token used to authenticate API requests. Once it expires, the client must request a new one.
- **Refresh Token** — A long-lived token used to obtain a new access token without requiring the user to log in again.

---

## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)

### Run the Project

```bash
git clone https://github.com/themuhsinoglu/TokenExample.git
cd TokenExample
dotnet run --project AccessTokenAndRefreshToken
```

---

## 📁 Project Structure

```
TokenExample/
├── AccessTokenAndRefreshToken/   # Main project
├── TokenExample.sln
└── README.md
```

---



<p align="center">
  <i>Made by <a href="https://github.com/themuhsinoglu">themuhsinoglu</a></i>
</p>
