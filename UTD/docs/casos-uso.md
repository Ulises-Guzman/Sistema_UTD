# Casos de usos

## 🔑 Rol administrador

![](imagenes/CU_Rol_Administrador.png)

---

## 🛠️ Rol Técnico

![](imagenes/CU_Rol_Tecnico.png)

---

## 🧑‍🔧 Rol cliente

![](imagenes/CU_Rol_Cliente.png)

---

# Especificaciones

## Requisitos funcionales

| #    | Descripción                                                                     | Caso de uso                      |
|:----:| ------------------------------------------------------------------------------- |:-------------------------------- |
| RF01 | ABM usuarios                                                                    | Administrador                    |
| RF02 | Filtro de búsqueda de usuarios                                                  | Administrador                    |
| RF03 | Inicio de sesión                                                                | Administrador, Técnico,  Cliente |
| RF04 | Cierre de sesión                                                                | Administrador, Técnico,  Cliente |
| RF05 | ABM modelos de instrumentos                                                     | Administrador, Técnico           |
| RF06 | Filtro de búsqueda de modelos de intrumentos                                    | Administrador, Técnico           |
| RF07 | ABM unidades de medidas                                                         | Administrador, Técnico           |
| RF08 | Filtro de búsqueda de unidades de medidas                                       | Administrador, Técnico           |
| RF09 | ABM rangos de medición                                                          | Administrador, Técnico           |
| RF10 | Filtro de búsqueda de rangos de medición                                        | Administrador, Técnico           |
| RF11 | ABM destinos                                                                    | Administrador, Técnico           |
| RF12 | Filtro de búsqueda de destinos                                                  | Administrador, Técnico           |
| RF13 | ABM secciones                                                                   | Administrador, Técnico           |
| RF14 | Filtro de búsqueda de secciones                                                 | Administrador, Técnico           |
| RF15 | ABM estados                                                                     | Administrador, Técnico           |
| RF16 | Filtro de búsqueda de estados                                                   | Administrador, Técnico           |
| RF17 | ABM modelos de instrumentos                                                     | Administrador                    |
| RF18 | Filtro de búsqueda modelos de instrumento                                       | Administrador                    |
| RF19 | Alta de instrumento recibido                                                    | Administrador, Técnico           |
| RF20 | Filtro de búsqueda de instrumento recibido                                      | Administrador, Técnico           |
| RF21 | Creación de planilla de calibración                                             | Administrador, Técnico           |
| RF22 | Alta de historial de instrumento                                                | Administrador, Técnico           |
| RF23 | Registro de inicio y cierre de sesión (*logs*)                                  | Sistema                          |
| RF24 | Consulta de logs inicio y cierre de sesión                                      | Administrador                    |
| RF25 | Ejecución de alarmas de vencimientos y notificaciones de estado de instrumentos | Sistema                          |
| RF26 | Consulta para dashboard de estado de instrumentos, por tipo de usuario          | Sistema                          |
| RF27 | Consulta auditoría por criterios                                                | Administrador                    |
| RF28 | Reporte consulta de instrumento recibido                                        | Administrador, Técnico           |
| RF29 | Reporte planilla de calibración                                                 | Administrador, Técnico           |
| RF30 | Reporte historial de instrumento                                                | Administrador, Técnico           |

## Requisitos no funcionales

| #     | Descripción                                             | Caso de uso                    |
|:-----:|:------------------------------------------------------- |:------------------------------ |
| RNF01 | Acceso al sistema desde otros destinos fuera de la zona | Cliente                        |
| RNF02 | Modo de temas claro/oscuro                              | Administrador, Técnico,Cliente |
