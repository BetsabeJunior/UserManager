# User Management API

## Autor

**Betsabe Junior Hoyos Barrios**  
Repositorio: [UserManager en GitHub](https://github.com/BetsabeJunior/UserManager)


## Descripción

API RESTful desarrollada con **ASP.NET Core** para la gestión de usuarios, que permite crear, editar, eliminar y consultar usuarios.

Cada usuario contiene los siguientes datos:

- Nombre y apellido
- Tipo de identificación (CC, RC, TI, PA), cargado desde base de datos
- Número de identificación
- Correo electrónico (validación de formato)
- Contraseña (mínimo 6 caracteres)

## Funcionalidades implementadas

- Crear usuario con validación de datos.
- Editar usuario existente.
- Eliminar usuario.
- Consultar todos los usuarios.
- Consultar un usuario por ID.
- Autenticación de usuarios con JWT.
- Listado de tipos de identificación desde base de datos.
- Documentación interactiva con Swagger.
- Registro de logs con ILogger.
- Manejo de errores y respuestas estandarizadas.

## Tecnologías utilizadas

- .NET 8
- C#
- ASP.NET Core
- Entity Framework Core
- SQL Server
- JWT (Json Web Token)
- Swagger
- Clean Architecture (por capas)

## Arquitectura del proyecto

La solución sigue el patrón de **Clean Architecture**, con separación de responsabilidades:

  |
  |   
- |--> `UserManager.API`: Controladores y configuración general.
- |--> `UserManager.Application`: Servicios, interfaces y DTOs.
- |--> `UserManager.Domain`: Entidades del dominio.
- |--> `UserManager.Infrastructure`: Repositorios e implementación de interfaces.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server (o versión compatible)

## Instalación y ejecución

1. Clonar el repositorio:

   ```bash
   git clone https://github.com/BetsabeJunior/UserManager.git
   cd UserManager
   ```

2. Configurar la cadena de conexión en `appsettings.json`:

   ```json
   "ConnectionStrings": {
     "SQLConnection": "Server=TU_SERVIDOR;Database=UserManagerDb;Trusted_Connection=True;"
   }
   ```

3. Ejecutar el script SQL `database.sql` para crear la base de datos y datos iniciales.

4. Restaurar paquetes NuGet:

   ```bash
   dotnet restore
   ```

5. Ejecutar la API:

   ```bash
   dotnet run --project UserManager.API
   ```

6. Acceder a la documentación Swagger:

   ```
   https://localhost/swagger/index.html
   ```

## Notas adicionales

- Todos los endpoints protegidos requieren autenticación JWT. Para probarlos:
  1. Realizar login con usuario válido.
  2. Copiar el token y usarlo en "Authorize" de Swagger.

- Validaciones en la creación de usuarios:
  - Email debe ser válido.
  - Contraseña debe tener mínimo 6 caracteres.
  - `IdentificationTypeId` debe existir en base de datos.

- Si el tipo de identificación es inválido, se retorna `400 Bad Request`.

---

Este proyecto fue desarrollado como **prueba técnica** para la vacante de **Desarrollador Back-End Jr**.
