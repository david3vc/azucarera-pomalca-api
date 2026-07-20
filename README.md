# 🏭 AzucareraPomalca API — Backend (.NET 7, EF Core, SQL Server)

API REST para la **gestión de talento humano y estructura organizacional** de una empresa azucarera: organigrama, puestos, empleados, competencias, brechas de capacitación, cursos y planes de capacitación.

Implementa arquitectura en capas, EF Core con configuraciones por entidad, DI con Autofac, autenticación JWT, Dapper para consultas complejas, y un flujo de migraciones SQL manuales versionadas con scripts de rollback.

---

## 🏗️ Arquitectura del proyecto

```bash
azucarera-pomalca-api/
├── AzucareraPomalca.Api/            # Capa de presentación (Web API)
│   ├── Controllers/                  # ~45 controllers (organigrama, puestos, empleados, capacitación, seguridad, etc.)
│   ├── Exceptions/                   # ErrorModel, ErrorResponse, ErrorValidationModel
│   ├── Filters/                       # AuthorizeOperationFilter (Swagger), ValidationFilter
│   ├── Middlewares/                    # ExceptionMiddleware
│   ├── Program.cs                       # DI, JWT, CORS, Swagger, Autofac
│   ├── appsettings.json                  # Configuración (ConnectionStrings, JWT secret)
│   └── appsettings.Development.json
├── AzucareraPomalca.Application/      # Lógica de negocio, DTOs, servicios, perfiles de AutoMapper
│   ├── Dtos/                           # Un folder por entidad (Puestos, Empleados, Competencias, Cursos, etc.)
│   ├── Services/                        # Interfaces + Implementations
│   └── Cores/                            # Registro de servicios y módulo Autofac
├── AzucareraPomalca.Domain/            # Dominio (Entidades y Contratos)
│   ├── Models/                          # ~45 entidades del dominio
│   ├── Repositories/                     # Interfaces de repositorios
│   └── Cores/                             # CoreModel, PagedResult, Paging, IBaseRepository, etc.
├── AzucareraPomalca.Infrastructure/     # Acceso a datos (EF Core + Dapper)
│   ├── Configurations/                   # Mapeos Fluent API por entidad
│   ├── Persistences/                      # Repositorios (EF Core + Dapper)
│   ├── Cores/                              # ApplicationDbContext, Paginator, BaseRepository, CrudRepository
│   └── SqlScripts/                         # Migraciones manuales versionadas + guías + scripts de rollback
├── AzucareraPomalca.Utils/               # Constantes compartidas (EstadosPlan, TipoGerencia, TiposCursos, TiposNodo, etc.)
├── Dockerfile                             # Build multi-stage (.NET 7 SDK → ASP.NET runtime)
└── .dockerignore
```

---

## 🧩 Módulos funcionales

El dominio cubre estas áreas de negocio (organizadas por entidad, no por carpeta física):

- **Organización**: `Division`, `Gerencia`, `Departamento`, `Coordinacion`, `Seccion`, `ArbolOrganizacional`
- **Puestos**: `Puesto`, `PuestoCurso`, `PuestoProfesion`, `Responsabilidad(Puesto)`, `EsfuerzoRequerido(Puesto)`, `CondicionTrabajo(Puesto)`, `TomaDecision(Puesto)`, `FuncionEspecifica`, `PerfilCompetencia`
- **Empleados**: `Empleado`, `EmpleadoCurso`, `EmpleadoProfesion`, `ExperienciaLaboral`, `CondicionEmpleado`, `EmpleadoSugerido`
- **Capacitación**: `Curso`, `Capacitacion`, `PlanCapacitacion`, `CapacitacionEmpleado`, `CursoCompetencia`, `TipoCurso`
- **Competencias y brechas**: `Competencia`, `TipoCompetencia`, `GradoDominio`, `Brecha` (brechas duras y blandas)
- **Catálogos generales**: `TablaComun`, `Nivel`, `ClaseOcupacional`, `GrupoOcupacional`, `Profesion`, `TipoProfesion`, `GradoAcademico`
- **Seguridad y acceso**: `Usuario`, `Rol`, `Permiso`, `Menu`, `Auth`

---

## 📦 Dependencias (NuGet) por proyecto

### `AzucareraPomalca.Api`
- `Microsoft.AspNetCore.OpenApi` **7.0.13**
- `Microsoft.EntityFrameworkCore.Design` **7.0.14**
- `Newtonsoft.Json` **13.0.3**
- `Serilog.AspNetCore` **7.0.0**
- `Swashbuckle.AspNetCore` **6.5.0**

### `AzucareraPomalca.Application`
- `Autofac.Extensions.DependencyInjection` **8.0.0**
- `AutoMapper` **12.0.1**
- `AutoMapper.Extensions.Microsoft.DependencyInjection` **12.0.1**
- `FluentValidation.AspNetCore` **11.3.0**

### `AzucareraPomalca.Core`
- `Microsoft.AspNetCore.Authentication.JwtBearer` **7.0.12**
- `Microsoft.Extensions.Identity.Core` **7.0.12**

### `AzucareraPomalca.Infrastructure`
- `Autofac.Extensions.DependencyInjection` **8.0.0**
- `Dapper` **2.1.21**
- `Microsoft.EntityFrameworkCore` **7.0.11**
- `Microsoft.EntityFrameworkCore.SqlServer` **7.0.11**

### `AzucareraPomalca.Utils`
- Sin dependencias externas — solo constantes compartidas entre capas.

> 💡 El proyecto usa **Autofac** para DI, **EF Core + Dapper** combinados (EF Core para CRUD, Dapper para consultas específicas), **JWT Bearer** para autenticación, **FluentValidation** para DTOs y **Swagger** con soporte de Bearer Token.

---

## 🔐 Autenticación

- `POST /api/auth/login` — recibe credenciales (`UsuarioAuthDto`) y devuelve un token JWT (`UsuarioSecurityDto`).
- Todos los demás endpoints requieren `Authorization: Bearer {token}` (política global `RequireAuthenticatedUser`).
- Hash de contraseñas con `PasswordHasher` de ASP.NET Identity (modo `IdentityV3`).
- Control de acceso adicional vía `Rol`, `Permiso` y `Menu`.

---

## 🗄️ Base de datos y migraciones

- Motor: **SQL Server**
- Acceso: **EF Core** (configuraciones Fluent API por entidad) + **Dapper** para consultas puntuales
- El proyecto **no usa EF Core Migrations**: el esquema se mantiene con **scripts SQL manuales versionados** en `AzucareraPomalca.Infrastructure/SqlScripts/`, cada uno con:
  - Un script de aplicación (`YYYYMMDD_nombre.sql`)
  - Un script de rollback (`YYYYMMDD_nombre_rollback.sql`)
  - Una guía en Markdown (`MIGRACION_YYYYMMDD_nombre.md`) con contexto, pasos y validaciones


---


---

## 🚀 Ejecución

Requisitos previos:

- .NET SDK 7.0+
- SQL Server (local o remoto)

### Levantar la API

1. Restaurar paquetes

```bash
dotnet restore
```

2. Ejecutar la API

```bash
dotnet run --project AzucareraPomalca.Api
```

---

## 📝 Estándares y buenas prácticas

- Arquitectura por capas (Api / Application / Domain / Infrastructure / Core / Utils)
- DTOs + AutoMapper para separar dominio de transporte
- Validación de entrada con FluentValidation
- Autenticación JWT + hashing de contraseñas con ASP.NET Identity
- Paginación genérica (`IPaginatedRepository`, `PagedResult`, `Paging`)
- Migraciones de esquema versionadas manualmente, con guía y rollback por cada cambio
- Contenerización con Docker (build multi-stage)
- Documentación de endpoints con Swagger, incluyendo soporte de Bearer Token

---

## 👤 Autor
David Vera

Software Developer — .NET | SQL Server
