# Senit Platform API

Backend ASP.NET Core para Senit. Usa Entity Framework Core, MySQL, Swagger, CORS e internacionalización con archivos `.resx` por bounded context.

## Abrir en Rider

1. Abre Rider.
2. Selecciona **Open**.
3. Abre `Senit.Platform.API.csproj`.
4. Instala los paquetes desde **Tools > NuGet > Manage NuGet Packages** si Rider no los restaura automáticamente.
5. Modifica `appsettings.Development.json` con tu usuario y contraseña de MySQL local.
6. Crea una base de datos vacía llamada `senit_db` en MySQL.
7. Ejecuta el proyecto con el botón **Run**.

## Paquetes NuGet a buscar en Rider

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Relational`
- `Microsoft.EntityFrameworkCore.Design`
- `MySql.EntityFrameworkCore`
- `Swashbuckle.AspNetCore`
- `Swashbuckle.AspNetCore.Annotations`
- `Humanizer`

## Endpoints compatibles con el frontend

- `/hotels`
- `/users`
- `/authentication/sign-in`
- `/rooms`
- `/reservations`
- `/guests`
- `/guest-stays`
- `/consumptions`
- `/payments`
- `/invoices`
- `/cleaning-tasks`
- `/notifications`
- `/subscriptions`
- `/subscription-payments`

## Credenciales sembradas

- `admin@admin.com` / `123456`
- `recepcion@recepcion.com` / `12345`

## Frontend local

En el frontend cambia `.env.development`:

```env
VITE_API_BASE_URL=http://localhost:5026
```

Los paths de endpoints pueden quedarse iguales.
