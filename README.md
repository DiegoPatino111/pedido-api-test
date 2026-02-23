# 🛒 API de Registro de Pedidos - Sistema Transaccional Empresarial


Este proyecto implementa un servicio RESTful para el registro de pedidos dentro de un sistema transaccional empresarial, cumpliendo con altos estándares de integridad de datos, manejo de errores y buenas prácticas de desarrollo backend.

## 🌟 Características Principales

- **Transaccionalidad completa**: Todos los procesos se ejecutan dentro de una única transacción SQL, garantizando consistencia de datos.
- **Validación externa robusta**: Integración con servicios de validación de clientes con control adecuado de fallos.
- **Auditoría integral**: Registro detallado de eventos en tabla de auditoría.
- **Manejo de errores profesional**: Respuestas HTTP semánticas y registro detallado en logs.
- **Arquitectura limpia**: Separación por capas (Controlador, Servicio, Repositorio).
- **Resiliencia**: Manejo de timeouts y fallos en servicios externos.

## ⚙️ Tecnologías Utilizadas

| Componente | Tecnología |
|------------|------------|
| Framework | .NET Framework 4.7.2 |
| ORM | Dapper |
| Base de Datos | SQL Server 2019 |
| Inyección de Dependencias | Unity |
| Logging | log4net |

## 📋 Requisitos Previos

- Visual Studio 2019 o superior
- SQL Server 2016 o superior
- .NET Framework 4.7.2 Developer Pack
- Paquetes NuGet:
  - Dapper
  - Unity
  - Unity.WebApi
  - log4net

## 🔧 Configuración e Instalación

### 1️⃣ Configuración de la Base de Datos

Ejecuta el script SQL proporcionado para crear la base de datos y tablas:

```sql
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'PedidoDB')
BEGIN
    CREATE DATABASE PedidoDB;
END
GO

USE PedidoDB;
GO

-- Resto del script de creación de tablas...
```

### 2️⃣ Configuración del Proyecto

1. Clona el repositorio:

```bash
git clone https://github.com/DiegoPatino111/pedido-api-test.git
```

2. Configura la cadena de conexión en `Web.config`:

```xml
<connectionStrings>
  <add name="PedidoDB" 
       connectionString="Server=.;Database=PedidoDB;Integrated Security=True;" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

3. Verifica la URL del servicio de validación externa:

```xml
<appSettings>
  <add key="ExternalValidationUrl" value="https://jsonplaceholder.typicode.com/users/" />
</appSettings>
```

### 3️⃣ Ejecución

1. Compila la solución.
2. Ejecuta el proyecto en IIS Express o IIS.
3. Consume el endpoint desde Postman o cualquier cliente HTTP.

## 📌 Buenas Prácticas Implementadas

- Uso de patrones Repository y Service.
- Control de excepciones centralizado.
- Uso de transacciones explícitas con `IDbTransaction`.
- Separación de responsabilidades (SRP).
- Logging estructurado.

## 📄 Licencia

Proyecto desarrollado con fines demostrativos y académicos.


