# Sistema_UTD [ Unidad Técnica Dimensional ]

# 🔬 Sistema de Metrología y Trazabilidad

Un software profesional de gestión de calibración (CMS) diseñado para asegurar la integridad de los datos, la trazabilidad de los instrumentos de medición.

Este sistema permite a los técnicos, administradores y gerentes llevar un control exhaustivo del ciclo de vida de los equipos, gestionar patrones de referencia y generar certificados de calibración automatizados e inalterables.

---

## 🚀 Stack Tecnológico

El proyecto está desarrollado utilizando un entorno robusto y corporativo:

* **Backend:** C# con .NET Framework.
* **Base de Datos:** SQL Server.
* **Acceso a Datos:** ADO.NET puro (con control manual de consultas `SqlConnection` y `SqlCommand`).
* **Frontend / UI:** ASP.NET WebForms integrando **Bootstrap 5** para un diseño responsivo (implementando `Site.Master` para el dashboard y `Login.Master` para el acceso).
* **Seguridad:** Encriptación de contraseñas mediante.
* **Generación de Reportes:** Creación de PDFs.

---

## 🏗️ Arquitectura del Sistema

El código fuente está estructurado siguiendo una **Arquitectura en Capas (N-Tier)** para garantizar alta cohesión, bajo acoplamiento y mantenibilidad a largo plazo. Se implementó un modelo de dominio de acuerdo al caso de la solución.

La Solución de Visual Studio se divide en los siguientes proyectos lógicos:

1. **`Sistema_UTD.Dominio`:** El núcleo del sistema. Contiene entidades puras (POCOs) como `Instrumento`, `Modelo`, `Calibracion` y objetos de valor, sin dependencias de infraestructura.
2. **`Sistema_UTD.Datos`:** Repositorios que interactúan exclusivamente con SQL Server.
3. **`Sistema_UTD.Negocio`:** Capa que concentra las reglas de negocio, validaciones y cálculos (ej. cálculo de porcentaje de errores sistemáticos).
4. **`Sistema_UTD.Web` (Capa Presentación):** Interfaz gráfica, webforms, vistas HTML/CSS y componentes modales dinámicos.
5. **`Helpers`:** Utilidades transversales (ej. motor de generación de PDF).

---

## ✨ Características Principales

* **Dashboard Analítico:** Panel principal con indicadores visuales y semáforos (Verde, Amarillo, Rojo) para el control rápido de equipos vigentes y vencidos[cite: 1].
* **Gestión de Activos Dual:** Separación arquitectónica entre el *Catálogo de Modelos* (especificaciones de fábrica) y los *Instrumentos Físicos* (instancias con Número de Serie único).
* **Motor de Calibraciones:** Registro estructurado Maestro-Detalle de mediciones (Valor Nominal vs. Valor Leído) con cálculo de error almacenado de forma persistente en SQL Server para máxima trazabilidad.
* **Trazabilidad de Patrones:** Enlace estricto en la base de datos entre la calibración actual y el Patrón de Referencia primario utilizado durante el ensayo.
* **Rastro de Auditoría (Audit Trail):** Tablas diseñadas con campos de `FechaCreacion`, `UsuarioCreacionId`.

---

## 📚 Documentación Oficial

La documentación técnica, de análisis y los manuales de usuario se encuentran centralizados en el directorio `/docs` de este repositorio.

### 🛠️ Ingeniería y Diseño

* [Casos de Uso y Especificaciones](docs/casos-uso.md)
* [Modelo de Dominio Conceptual](docs/modelo-dominio.md)
* [Arquitectura del Sistema](docs/arquitectura.md)
* [Estructura de Base de Datos y Diccionario de Datos](docs/base-datos.md)
* [Diseño de UI / UX](docs/interfaz.md)

### 📖 Manuales de Usuario

Instructivos segmentados según el rol dentro del sistema:

1. [Guía para Gerencia y Auditores de Calidad](docs/manuales/manual-gerencia.md)
2. [Manual de Configuración para Administradores](docs/manuales/manual-administrador.md)
3. [Manual Operativo para Técnicos de Metrología](docs/manuales/manual-tecnico.md)
4. [Guía de Consulta Rápida para Clientes](docs/manuales/guia-cliente.md)
