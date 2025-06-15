# Requisitos Técnicos

## Padrões

### Convenções

- Entidades e código de framework em **inglês**.
- Descrições, comentários e valores internos em **português**.
- Convenções de nomenclatura para classes, métodos e código em geral:
  [Microsoft C# Coding Conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Padrão de resposta da API baseado na [RFC 7807 - Problem Details for HTTP APIs](https://datatracker.ietf.org/doc/html/rfc7807)

### Arquiteturais

- Result Pattern
- Repository Pattern
- Options Pattern
- Constants Pattern

## Tecnologias

### Back-End (API)

- **Solução:** `.slnx` (Solution Filter)
- **Framework:** .NET 9
- **ORM:** Entity Framework (para escrita) | Dapper (para leitura)
- **Banco de Dados:** Microsoft SQL Server
- **Plugins e Bibliotecas:**
  - Swagger
  - Swagger Annotations
  - FluentValidation
  - ASP.NET API Versioning

### Front-End (Blazor, React ou Angular)

- A definir.

### Worker

- A definir.
