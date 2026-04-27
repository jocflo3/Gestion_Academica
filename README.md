# Sistema de Gestión Académica

API REST desarrollada en .NET para la gestión de alumnos, materias y horarios académicos.  
El sistema permite administrar información escolar, validar conflictos de horario y consultar el estado académico de los alumnos.

---

## Descripción

Este proyecto simula un sistema utilizado en instituciones educativas para gestionar:

- Información de alumnos
- Materias y su estado académico
- Horarios con múltiples bloques
- Validaciones de conflictos
- Consultas académicas avanzadas

El objetivo es centralizar la información y evitar inconsistencias en la asignación de materias y horarios.

---

## Problema que resuelve

En muchos sistemas escolares:

- Los horarios se asignan manualmente
- No se validan conflictos de tiempo
- Los estados académicos no están centralizados
- Las consultas requieren mucho trabajo manual

Este sistema automatiza estos procesos mediante lógica de negocio y consultas SQL optimizadas.

---

## Tecnologías utilizadas

- .NET (ASP.NET Web API)
- C#
- SQL Server
- Entity Framework Core (opcional)
- Git / GitHub

---

## Funcionalidades principales

### estión de alumnos
- Crear, actualizar, eliminar y consultar alumnos
- Información básica (nombre, semestre, carrera, etc.)

---

### Gestión de materias
- Registro de materias por alumno
- Estado académico:
  - 0: Sin cursar
  - 1: Cursando
  - 2: Cursada
  - 3: Reprobada

---

### Gestión de horarios
- Una materia puede tener múltiples horarios
- Asignación por día y hora
- Validación de conflictos de horario

#### Validación clave:
Un alumno no puede tener dos materias en el mismo horario.

---

### Consultas avanzadas

- Materias cursadas por alumno
- Materias reprobadas
- Historial académico
- Detección de conflictos de horario
- Materias en común entre alumnos

---

## Arquitectura

El proyecto sigue una arquitectura en capas:

- Controllers → Manejo de endpoints
- Services → Lógica de negocio
- Repositories → Acceso a datos
- Models → Entidades
- DTOs → Transferencia de datos

---

## Endpoints principales

### Alumnos
- `GET /api/alumnos`
- `GET /api/alumnos/{id}`
- `POST /api/alumnos`
- `PUT /api/alumnos/{id}`
- `DELETE /api/alumnos/{id}`

---

### Materias
- `GET /api/materias`
- `POST /api/materias`
- `PUT /api/materias/{id}`
- `DELETE /api/materias/{id}`

---

### Horarios
- `POST /api/horarios`
- `GET /api/horarios/alumno/{id}`

---

### Validaciones
- `GET /api/horarios/conflictos/{alumnoId}`

---

## Reglas de negocio importantes

- No permitir traslapes de horario
- Validar existencia de alumno antes de asignar materia
- Validar estado académico
- Una materia debe pertenecer a un alumno

---

## Cómo ejecutar el proyecto

1. Clonar el repositorio:
```bash
git clone https://github.com/jocflo3/Gestion_Academica.git
