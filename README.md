Personal Finance Tracker - Backend API
Overview
This project is a robust, multi-tenant backend application designed to manage personal finances, track expenses, and project credit card statements. It is built using C# .NET and Entity Framework Core, following a strict Object-Oriented architecture and the Code-First approach for database management.

Tech Stack
Language: C# .NET

ORM: Entity Framework Core (EF Core)

Database: SQL Server (SQL Express / LocalDB)

Querying: LINQ (Language Integrated Query)

Core Architecture & Domain Driven Design
The system is built around clearly defined domain entities that map real-world financial concepts into relational data:

Usuario (User): The core of the multi-tenant design. Every transaction, category, and credit card is strictly linked to a UsuarioId.

Gasto (Expense) & Ingreso (Income): Transactional records featuring timestamps, amounts, and relational links to categories.

Categoria (Category): Customizable classification for transactions, segmented by operation type (Income/Expense).

TarjetaCredito (Credit Card): Advanced entity tracking credit limits, closing dates, and billing cycles to project future debt.

Database Design & EF Core Implementation
The infrastructure was built using the Code-First methodology, allowing the C# domain models to dictate the SQL Server schema:

ApplicationDbContext: Acts as the bridge between the domain and SQL Server, utilizing DbSet properties to generate physical tables (Usuarios, Gastos, Ingresos, Categorias, Tarjetas).

Fluent API Configuration: Complex database constraints were handled overriding the OnModelCreating method. Specifically, the Multiple Cascade Paths error (inherent to SQL Server when deleting a user impacts both categories and transactions simultaneously) was resolved by applying DeleteBehavior.Restrict to the foreign keys, ensuring data integrity and preventing cyclic deletion loops.

Migrations: Schema versioning is actively maintained through EF Core CLI (Add-Migration, Update-Database).

Current Business Logic (Gestor)
Before exposing the endpoints via Web API, the core business rules were validated using an in-memory manager (Gestor.cs) powered by advanced LINQ queries:

Authentication Flow: Validates existing users via .FirstOrDefault() or provisions new user records dynamically.

Financial Summaries: Aggregates total income vs. total expenses to calculate liquid balances.

Categorical Reporting: Groups and orders expenses by category utilizing .GroupBy() and .Select() to detect spending patterns.

Credit Card Projections: Filters transactions by TarjetaCreditoId and calculates the required installment payments (Monto / Cuotas) for the upcoming billing cycle, adjusting the available credit limit in real-time.

Next Steps
Refactor the Gestor.cs to execute all LINQ queries directly against the SQL Server database via ApplicationDbContext.

Implement asynchronous data access (async/await).

Expose the business logic through RESTful Web API Controllers.
