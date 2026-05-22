# Migración 20260524 — Capacitación: datos de ejecución opcionales

Vuelve **NULLABLE** las columnas `capacitacion.id_tipo_facilitador` y
`capacitacion.id_modalidad`. Habilita el flujo de **planeación simplificado**: en
el Plan de Capacitación una línea se crea sólo con el curso + los participantes (a
quién va dirigido), y los detalles de ejecución (facilitador, modalidad, horas,
costo) se completan después editando la capacitación.

Sigue las decisiones del proyecto: scripts SQL manuales (D-006), rollback aparte
(D-009). Ver también CONTEXTO.md D-014 / R10.

## Archivos

| Archivo | Rol |
|---|---|
| `20260524_capacitacion_datos_opcionales.sql` | Forward (idempotente) |
| `20260524_capacitacion_datos_opcionales_rollback.sql` | Rollback (idempotente, con salvaguarda) |

## Cambios de esquema

- `capacitacion.id_tipo_facilitador`: `INT NOT NULL` → `INT NULL`.
- `capacitacion.id_modalidad`: `INT NOT NULL` → `INT NULL`.
- Las FK que referencian estas columnas **no se tocan** (cambiar la nullabilidad
  no requiere soltarlas); ahora aceptan `NULL` = "sin asignar".

## Pasos de ejecución (Somee)

1. Parar la API (o no arrancar la imagen nueva todavía).
2. Backup de `capacitacion`.
3. Ejecutar `20260524_capacitacion_datos_opcionales.sql` en la BD de Somee.
4. Verificar (ver §Verificación).
5. Arrancar el binario nuevo de la API (entidad/DTO con `int?`).

> **Dependencia:** la tabla `capacitacion` debe existir (preexistente). Es
> independiente de 20260522; aplicar en cualquier orden relativo.

## Verificación

```sql
SELECT name, is_nullable
FROM sys.columns
WHERE object_id = OBJECT_ID('capacitacion')
  AND name IN ('id_tipo_facilitador', 'id_modalidad');
```

Esperado: ambas columnas con `is_nullable = 1`.

## Rollback

Ejecutar `20260524_capacitacion_datos_opcionales_rollback.sql`. La salvaguarda
**aborta** si existen filas con `id_tipo_facilitador`/`id_modalidad` en NULL
(líneas en planeación sin completar): hay que completarlas o eliminarlas primero.
Revertir además el binario de la API a la versión con FKs no-nulas.
