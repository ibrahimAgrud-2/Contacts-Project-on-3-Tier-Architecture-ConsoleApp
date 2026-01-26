# Contacts Project on 3 Tier Architecture





 ### Project Overview

This project is a simple C# Console Application developed to understand and practice the **3-Tier Architecture** concept. The application manages contact data and demonstrates how responsibilities can be cleanly separated across different layers of an application.

The project is structured into three main layers: Presentation Layer, Business Layer, and Data Access Layer. Each layer has a specific responsibility and communicates only with the appropriate adjacent layer. Detailed explanations for each layer can be found in the individual `README.md` files located inside their respective folders.

The database used by this project is located inside the **ContactDataAccessLayer** folder. All database-related operations are isolated within the Data Access Layer to maintain a clear separation of concerns.

Since this project was created as a learning-focused implementation of 3-Tier Architecture, it may contain some validation limitations. The primary goal of the project is to understand architectural structure and layer responsibilities rather than to provide a production-ready solution.

The diagram below illustrates the overall flow and interaction between the layers in the application.

![](C:\Users\ibrah\Masaüstü\files\ProjectsToGit\CSharpProjects\08_Contacts-Project-on-3-Tier-Architecture\Diagram.png)