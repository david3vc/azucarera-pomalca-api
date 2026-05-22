-- =============================================================================
-- Migración manual: capacitación — datos de ejecución opcionales (planeación)
-- Idempotente: re-ejecutable sin error.
--
-- Qué hace:
--   Vuelve NULLABLE las columnas capacitacion.id_tipo_facilitador y
--   capacitacion.id_modalidad. A partir de ahora una "línea de plan" se crea
--   en la fase de planeación con sólo el curso + los participantes (a quién va
--   dirigido); facilitador, modalidad, horas y costo se completan después
--   editando la capacitación. Ver CONTEXTO.md D-014 / R10.
--
-- Notas:
--   - Cambiar la nullabilidad de una columna NO requiere soltar las FK que la
--     referencian; las FK 'FK__capacitac__id_ti__*' / 'FK__capacitac__id_mo__*'
--     siguen vigentes (ahora aceptan NULL = "sin asignar").
--   - No se usa dynamic SQL porque no se referencian columnas recién creadas
--     (D-010 no aplica aquí).
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- id_tipo_facilitador -> NULLABLE -------------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('capacitacion')
             AND name = 'id_tipo_facilitador'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE capacitacion ALTER COLUMN id_tipo_facilitador INT NULL;
END

-- id_modalidad -> NULLABLE ---------------------------------------------------
IF EXISTS (SELECT 1 FROM sys.columns
           WHERE object_id = OBJECT_ID('capacitacion')
             AND name = 'id_modalidad'
             AND is_nullable = 0)
BEGIN
    ALTER TABLE capacitacion ALTER COLUMN id_modalidad INT NULL;
END

COMMIT;
