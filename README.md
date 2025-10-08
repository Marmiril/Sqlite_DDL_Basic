Descripción

SQLite DB Manager es una aplicación de consola en C# desarrollada con Visual Studio que permite gestionar bases de datos SQLite de forma interactiva.
Su objetivo es didáctico: aprender cómo crear, modificar y eliminar bases de datos, tablas y columnas mediante código SQL.

Funcionalidades principales

* Crear, listar y eliminar bases de datos.
* Crear, modificar y eliminar (u ocultar) tablas.
* Añadir columnas nuevas a una tabla.
* Ocultar columnas sin eliminarlas físicamente.
* Exploración del esquema (tablas y columnas) usando SchemaExplorer.
* Sistema modular con helpers para entrada de datos, pausas y confirmaciones.

Estructura del proyecto
SQLite_DB_Manager/
│
├── Program.cs
├── MainCast.cs
├── Helpers/
│   ├── ConsoleInput.cs
│   ├── ConsolePause.cs
│   ├── DbBrowser.cs
│   ├── HiddenColumns.cs
│   ├── SchemaExplorer.cs
│   └── Storage.cs
│
└── DDL/
    ├── CreateDb.cs
    ├── ModifyDb.cs
    ├── DeleteDb.cs
    ├── CreateTable.cs
    ├── ModifyTable.cs
    ├── DeleteTable.cs
    ├── CreateColumn.cs
    ├── DeleteColumn.cs
    └── HideColumn.cs (si se usa)

Requisitos
- .NET 6.0 o superior
- Paquete NuGet: Microsoft.Data.Sqlite
- Compatible con Windows o Linux

Uso
Abre el proyecto en Visual Studio.
Compila y ejecuta (Ctrl + F5).
Usa el menú principal para:
Crear nuevas bases de datos.
Modificar o eliminar existentes.
Añadir o eliminar (ocultar) columnas.
Consultar el esquema de tablas.
Las bases de datos se guardan en la carpeta Databases/.

Notas didácticas
Este proyecto se ha diseñado como herramienta de aprendizaje.
Su objetivo es comprender los comandos SQL (DDL) básicos y cómo aplicarlos mediante C# y ADO.NET.

README (English)
Description

SQLite DB Manager is a C# console application built with Visual Studio that allows interactive management of SQLite databases.
It is designed as a learning tool to practice how to create, modify, and delete databases, tables, and columns using SQL commands.

Main Features

* Create, list, and delete databases.
* Create, modify, and remove (or hide) tables.
* Add new columns to existing tables.
* Hide columns without physically deleting them.
* Explore database schemas using SchemaExplorer.
* Modular helper system for input, pauses, and confirmations.

Project Structure
SQLite_DB_Manager/
│
├── Program.cs
├── MainCast.cs
├── Helpers/
│   ├── ConsoleInput.cs
│   ├── ConsolePause.cs
│   ├── DbBrowser.cs
│   ├── HiddenColumns.cs
│   ├── SchemaExplorer.cs
│   └── Storage.cs
│
└── DDL/
    ├── CreateDb.cs
    ├── ModifyDb.cs
    ├── DeleteDb.cs
    ├── CreateTable.cs
    ├── ModifyTable.cs
    ├── DeleteTable.cs
    ├── CreateColumn.cs
    ├── DeleteColumn.cs
    └── HideColumn.cs (optional)

Requirements

- .NET 6.0 or later
- NuGet package: Microsoft.Data.Sqlite
- Works on Windows or Linux

How to Use
Open the project in Visual Studio.
Build and run (Ctrl + F5).
Use the main menu to:
Create or delete databases.
Modify or hide tables.
Add or hide columns.
Inspect database schemas.
Databases are stored in the Databases/ folder.

Educational Notes

This project was designed as a learning exercise to understand fundamental SQL DDL operations and their implementation using C# and ADO.NET.# Sqlite_DDL_Basic
