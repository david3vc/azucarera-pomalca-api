# Migración 20260522 — Plan de Capacitación (FASE 1)

Introduce la base del módulo **Plan de Capacitación**: tabla `plan_capacitacion`,
el catálogo *Estado del Plan* en `tabla_comun` (id_tabla = 6) y dos FKs nullable en
`capacitacion` (`id_plan_capacitacion`, `id_competencia`).

Sigue las decisiones del proyecto: scripts SQL manuales (D-006), rollback aparte
(D-009) y dynamic SQL para columnas recién creadas (D-010).

## Archivos

| Archivo | Rol |
|---|---|
| `20260522_plan_capacitacion.sql` | Forward (idempotente) |
| `20260522_plan_capacitacion_rollback.sql` | Rollback (idempotente) |

## Cambios de esquema

- **Tabla nueva `plan_capacitacion`**: `id_plan_capacitacion` PK IDENTITY, `anio`,
  `descripcion`, `presupuesto_total DECIMAL(18,2)`, `fuerza_laboral`,
  `id_estado_plan` FK → `tabla_comun(id_tabla_comun)`, `fecha_aprobacion`,
  auditoría (`created_at`/`updated_at`/`state`).
- **Índice único filtrado** `IX_plan_capacitacion_anio_activo` sobre `(anio)` con
  `WHERE state = 1` → un plan activo (no eliminado) por año.
- **Seed `tabla_comun` (id_tabla = 6)**: filas Borrador(1), Aprobado(2),
  En ejecución(3), Cerrado(4). El id surrogate (`id_tabla_comun`) lo asigna
  IDENTITY; el código resuelve el estado por `(id_tabla, id_fila)`
  (ver `Utils/Constants/EstadosPlan.cs`), NO por id fijo.
- **`capacitacion`**: `+ id_plan_capacitacion INT NULL` (FK → `plan_capacitacion`),
  `+ id_competencia INT NULL` (FK → `competencia`). Ambas nullable: las
  capacitaciones ad-hoc (sin plan) siguen siendo válidas.

## Pasos de ejecución (Somee)

1. Parar la API (o no arrancar la imagen nueva todavía).
2. Backup de `capacitacion` y `tabla_comun`.
3. Ejecutar `20260522_plan_capacitacion.sql` en la BD de Somee.
4. Verificar (ver §Verificación).
5. Arrancar el binario nuevo de la API.

> **Dependencia:** esta migración requiere que `tabla_comun` y `competencia` ya
> existan (ambas preexistentes). Es independiente de la migración 20260521
> (equivalencias); pueden aplicarse en cualquier orden relativo.

## Verificación

```sql
-- Tabla creada
SELECT * FROM sys.tables WHERE name = 'plan_capacitacion';

-- Índice único filtrado
SELECT name, filter_definition FROM sys.indexes
WHERE name = 'IX_plan_capacitacion_anio_activo';

-- Catálogo Estado del Plan (4 filas)
SELECT id_tabla_comun, id_tabla, id_fila, codigo, descripcion
FROM tabla_comun WHERE id_tabla = 6 ORDER BY id_fila;

-- Columnas y FKs nuevas en capacitacion
SELECT COL_LENGTH('capacitacion','id_plan_capacitacion') AS col_plan,
       COL_LENGTH('capacitacion','id_competencia')       AS col_comp;
SELECT name FROM sys.foreign_keys
WHERE name IN ('FK_capacitacion_plan_capacitacion','FK_capacitacion_competencia',
               'FK_plan_capacitacion_estado');
```

Esperado: 1 tabla, 1 índice filtrado (`[state]=(1)`), 4 filas de catálogo, ambas
columnas no nulas (longitud), 3 FKs presentes.

## Rollback

Ejecutar `20260522_plan_capacitacion_rollback.sql`. **Destructivo**: elimina la
tabla `plan_capacitacion` (y todos los planes), las columnas nuevas de
`capacitacion` y el seed de `tabla_comun` (id_tabla = 6). Revertir además el
binario de la API a la versión previa a FASE 1.
