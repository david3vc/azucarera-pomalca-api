-- =============================================================================
-- ROLLBACK de 20260524_capacitacion_datos_opcionales.sql
-- Vuelve NOT NULL las columnas capacitacion.id_tipo_facilitador / id_modalidad.
--
-- PRERREQUISITOS (leer antes de ejecutar):
--   1. Parar la API (el binario nuevo escribe NULLs; revertir el binario también).
--   2. Backup de la tabla capacitacion.
--   3. NO puede haber filas con id_tipo_facilitador IS NULL o id_modalidad IS NULL,
--      o el ALTER COLUMN ... NOT NULL fallará. Las "líneas de plan" creadas en
--      planeación tienen estas columnas en NULL: deben completarse (editar la
--      capacitación) o eliminarse antes de revertir.
--
-- CONSECUENCIAS:
--   - Tras revertir, las líneas en planeación sin facilitador/modalidad no se
--     pueden persistir; el frontend del modal de planeación dejaría de funcionar.
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- Salvaguarda: abortar si quedan NULLs (no forzar pérdida de datos en silencio).
IF EXISTS (SELECT 1 FROM capacitacion WHERE id_tipo_facilitador IS NULL OR id_modalidad IS NULL)
BEGIN
    RAISERROR('Existen capacitaciones con id_tipo_facilitador/id_modalidad NULL. Complételas o elimínelas antes de revertir.', 16, 1);
    ROLLBACK TRAN;
    RETURN;
END

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('capacitacion')
             AND name = 'id_tipo_facilitador'
             AND is_nullable = 1)
BEGIN
    ALTER TABLE capacitacion ALTER COLUMN id_tipo_facilitador INT NOT NULL;
END

IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('capacitacion')
             AND name = 'id_modalidad'
             AND is_nullable = 1)
BEGIN
    ALTER TABLE capacitacion ALTER COLUMN id_modalidad INT NOT NULL;
END

COMMIT;
