-- =============================================================================
-- ROLLBACK de 20260522_plan_capacitacion.sql
-- Idempotente. Revierte el módulo Plan de Capacitación (FASE 1).
--
-- PRERREQUISITOS:
--   * Parar la API (el binario nuevo lee columnas/entidad que aquí se eliminan).
--   * Backup de plan_capacitacion y capacitacion.
--
-- CONSECUENCIAS:
--   * Se PIERDEN todos los planes (DROP TABLE plan_capacitacion).
--   * Se pierden las asociaciones capacitacion→plan/competencia (DROP COLUMN).
--   * Se eliminan las filas de Estado del Plan en tabla_comun (id_tabla = 6).
--   * Hay que revertir el binario de la API a la versión previa a FASE 1.
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- 1. capacitacion: quitar FKs nuevas ------------------------------------------
EXEC('
    IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = ''FK_capacitacion_competencia'')
        ALTER TABLE capacitacion DROP CONSTRAINT FK_capacitacion_competencia;

    IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = ''FK_capacitacion_plan_capacitacion'')
        ALTER TABLE capacitacion DROP CONSTRAINT FK_capacitacion_plan_capacitacion;
');

-- 2. capacitacion: quitar columnas nuevas -------------------------------------
IF COL_LENGTH('capacitacion', 'id_competencia') IS NOT NULL
BEGIN
    ALTER TABLE capacitacion DROP COLUMN id_competencia;
END

IF COL_LENGTH('capacitacion', 'id_plan_capacitacion') IS NOT NULL
BEGIN
    ALTER TABLE capacitacion DROP COLUMN id_plan_capacitacion;
END

-- 3. eliminar tabla plan_capacitacion (FK a tabla_comun se va con la tabla) ----
IF OBJECT_ID('plan_capacitacion') IS NOT NULL
BEGIN
    DROP TABLE plan_capacitacion;
END

-- 4. eliminar seed Estado del Plan --------------------------------------------
DELETE FROM tabla_comun WHERE id_tabla = 6;

COMMIT;
