# API_PROGRAMACION_DE_SOFTWARE
---
# API - Software de Biblioteca Universitaria

## Descripción

Este software está diseñado para la gestión eficiente de préstamos y reservas de material bibliográfico en una universidad. Su objetivo es facilitar el acceso y control de los recursos de la biblioteca.

La API expone los endpoints necesarios para realizar operaciones como consultar material disponible, gestionar reservas y controlar el flujo de préstamos, etc.

---

## Tecnologías utilizadas

- .NET Core
- Entity Framework Core
- SQL Server
- Swagger (documentación interactiva de la API)

---

## Alcance actual de la API

La versión actual de la API incluye las siguientes funcionalidades:

### Sistema de autenticación
- Login.

### Gestión de Usuarios
- Listar todos los Usuarios.
- Buscar un usuario.
- Crear un usuario (Guardarlo en la base de datos).
- Actualizar la informacion de un usuario.
- Eliminar un usuario.

### Gestión de material bibliográfico

- Listar todos los Materiales.
- Consulta de materiales.
- Listar materiales disponibles.
- Crear un material (Guardarlo en la base de datos)
- Actualizar la informacion de un material.
- Eliminar un material.

### Gestión Reservas de material

- Listar todas las reservas.
- Buscar una reserva.
- Realizar reservas.
- Visualizar reservas de un usuario.
- Crear una reserva.
- Extender el tiempo de una reserva.
- Rechazar una reserva.
- Cancelar una reserva.
- Eliminar una reserva

### Gestión de préstamos

- Listar todos los prestamos.
- Buscar un prestamo.
- Crear un prestamo.
- Devolver un prestamo.
- Cancelar un prestamo.
- Eliminar un prestamo.
---

## Fase del desarrollo

El software se encuentra en una **fase inicial**. Ya incluye funcionalidades para la autenticación, funcionalidades para administrar los materiales, funcionalidades para administrar las reservas y préstamos y funcionalidades para administrar los usuarios. En próximas versiones se espera incorporar:

- Funcionalidades administrativas avanzadas.

---

## Documentación de la API

La API está documentada y disponible en Swagger 

