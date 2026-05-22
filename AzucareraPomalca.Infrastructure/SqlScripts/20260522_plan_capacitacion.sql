-- =============================================================================
-- Migración manual: módulo Plan de Capacitación (FASE 1 — base del plan)
-- Idempotente: re-ejecutable sin error.
-- Orden:
--   1. tabla plan_capacitacion + índice único filtrado (anio por estado activo)
--   2. seed Estado del Plan en tabla_comun (id_tabla = 6)
--   3. capacitacion.id_plan_capacitacion + capacitacion.id_competencia (FKs nullable)
--
-- IMPORTANTE: las FKs sobre columnas RECIÉN agregadas van dentro de EXEC
-- (dynamic SQL), por las mismas razones documentadas en
-- 20260521_equivalencias.sql y CONTEXTO.md D-010:
--   a) SQL Server parsea el batch completo antes de ejecutarlo y, sin EXEC,
--      fallaría con "Invalid column name" al referenciar columnas que la propia
--      migración acaba de crear.
--   b) Algunos clientes (panel web de Somee) parten el script por sentencia;
--      mantener la lógica condicional dentro de un EXEC evita ese problema.
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- 1. tabla plan_capacitacion --------------------------------------------------
IF OBJECT_ID('plan_capacitacion') IS NULL
BEGIN
    CREATE TABLE plan_capacitacion (
        id_plan_capacitacion INT IDENTITY(1, 1) PRIMARY KEY,
        anio INT NOT NULL,
        descripcion NVARCHAR(500) NULL,
        presupuesto_total DECIMAL(18, 2) NOT NULL
            CONSTRAINT DF_plan_capacitacion_presupuesto_total DEFAULT 0,
        fuerza_laboral INT NOT NULL
            CONSTRAINT DF_plan_capacitacion_fuerza_laboral DEFAULT 0,
        id_estado_plan INT NOT NULL,
        fecha_aprobacion DATETIME2 NULL,
        created_at DATETIME2 NOT NULL,
        updated_at DATETIME2 NULL,
        state BIT NOT NULL CONSTRAINT DF_plan_capacitacion_state DEFAULT 1,
        CONSTRAINT FK_plan_capacitacion_estado
            FOREIGN KEY (id_estado_plan) REFERENCES tabla_comun (id_tabla_comun)
    );

    -- Un plan activo (no eliminado) por año.
    CREATE UNIQUE INDEX IX_plan_capacitacion_anio_activo
        ON plan_capacitacion (anio) WHERE state = 1;
END

-- 2. seed Estado del Plan en tabla_comun (id_tabla = 6) ------------------------
IF NOT EXISTS (SELECT 1 FROM tabla_comun WHERE id_tabla = 6 AND id_fila = 1)
    INSERT INTO tabla_comun (id_tabla, id_fila, codigo, descripcion, created_at, state)
    VALUES (6, 1, 'BORRADOR', 'Borrador', SYSUTCDATETIME(), 1);

IF NOT EXISTS (SELECT 1 FROM tabla_comun WHERE id_tabla = 6 AND id_fila = 2)
    INSERT INTO tabla_comun (id_tabla, id_fila, codigo, descripcion, created_at, state)
    VALUES (6, 2, 'APROBADO', 'Aprobado', SYSUTCDATETIME(), 1);

IF NOT EXISTS (SELECT 1 FROM tabla_comun WHERE id_tabla = 6 AND id_fila = 3)
    INSERT INTO tabla_comun (id_tabla, id_fila, codigo, descripcion, created_at, state)
    VALUES (6, 3, 'EN_EJECUCION', 'En ejecución', SYSUTCDATETIME(), 1);

IF NOT EXISTS (SELECT 1 FROM tabla_comun WHERE id_tabla = 6 AND id_fila = 4)
    INSERT INTO tabla_comun (id_tabla, id_fila, codigo, descripcion, created_at, state)
    VALUES (6, 4, 'CERRADO', 'Cerrado', SYSUTCDATETIME(), 1);

-- 3. capacitacion: FKs nuevas (nullable) --------------------------------------
IF COL_LENGTH('capacitacion', 'id_plan_capacitacion') IS NULL
BEGIN
    ALTER TABLE capacitacion ADD id_plan_capacitacion INT NULL;
END

IF COL_LENGTH('capacitacion', 'id_competencia') IS NULL
BEGIN
    ALTER TABLE capacitacion ADD id_competencia INT NULL;
END

-- FKs en EXEC (columnas recién agregadas — D-010).
EXEC('
    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = ''FK_capacitacion_plan_capacitacion'')
        ALTER TABLE capacitacion
            ADD CONSTRAINT FK_capacitacion_plan_capacitacion
            FOREIGN KEY (id_plan_capacitacion) REFERENCES plan_capacitacion (id_plan_capacitacion);

    IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = ''FK_capacitacion_competencia'')
        ALTER TABLE capacitacion
            ADD CONSTRAINT FK_capacitacion_competencia
            FOREIGN KEY (id_competencia) REFERENCES competencia (id_competencia);
');

COMMIT;
