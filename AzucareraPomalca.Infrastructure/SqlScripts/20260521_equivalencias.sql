-- =============================================================================
-- Migración manual: equivalencias horas <-> nivel (blandas) + horas/brecha (duros)
-- Idempotente: re-ejecutable sin error.
-- Orden:
--   1. grado_dominio.horas_requeridas
--   2. puesto_curso.horas_requeridas
--   3. empleado_curso.horas_acumuladas + fecha_calculo
--   4. perfil_competencia_empleado: id_competencia (con backfill desde grado_dominio),
--      horas_reales, horas_equivalentes, fecha_calculo, índice único
--   5. curso_competencia (tabla nueva) con FKs e índice único (id_curso, id_competencia)
--
-- IMPORTANTE: las secciones que tocan columnas RECIÉN agregadas usan EXEC
-- (dynamic SQL). Esto es necesario por DOS razones:
--   a) SQL Server parsea el batch completo antes de ejecutarlo y, sin EXEC,
--      fallaría con "Invalid column name" al referenciar columnas que la
--      propia migración acaba de crear.
--   b) Algunos clientes (panel web de Somee, entre otros) parten el script en
--      batches por sentencia, lo que rompe variables locales (DECLARE → IF).
--      Mantener TODA la lógica condicional dentro de un mismo EXEC evita el
--      problema porque la dynamic SQL es 1 sola sentencia para el parser
--      externo.
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- 1. grado_dominio.horas_requeridas -------------------------------------------
IF COL_LENGTH('grado_dominio', 'horas_requeridas') IS NULL
BEGIN
    ALTER TABLE grado_dominio
        ADD horas_requeridas INT NOT NULL
        CONSTRAINT DF_grado_dominio_horas_requeridas DEFAULT 0;
END

-- 2. puesto_curso.horas_requeridas --------------------------------------------
IF COL_LENGTH('puesto_curso', 'horas_requeridas') IS NULL
BEGIN
    ALTER TABLE puesto_curso
        ADD horas_requeridas INT NOT NULL
        CONSTRAINT DF_puesto_curso_horas_requeridas DEFAULT 0;
END

-- 3. empleado_curso.horas_acumuladas + fecha_calculo --------------------------
IF COL_LENGTH('empleado_curso', 'horas_acumuladas') IS NULL
BEGIN
    ALTER TABLE empleado_curso
        ADD horas_acumuladas DECIMAL(10, 2) NOT NULL
        CONSTRAINT DF_empleado_curso_horas_acumuladas DEFAULT 0;
END

IF COL_LENGTH('empleado_curso', 'fecha_calculo') IS NULL
BEGIN
    ALTER TABLE empleado_curso
        ADD fecha_calculo DATETIME2 NULL;
END

-- 4. perfil_competencia_empleado ----------------------------------------------
-- 4.a Agregar id_competencia como nullable
IF COL_LENGTH('perfil_competencia_empleado', 'id_competencia') IS NULL
BEGIN
    ALTER TABLE perfil_competencia_empleado
        ADD id_competencia INT NULL;
END

-- 4.b Backfill + promoción a NOT NULL + FK + índice único.
-- TODO va dentro de un solo EXEC para evitar (a) parseo temprano de
-- referencias a id_competencia y (b) clientes que parten el batch entre
-- DECLARE/IF. Las comillas simples internas se escapan con ''.
EXEC('
    -- Backfill desde grado_dominio
    UPDATE pce
    SET pce.id_competencia = gd.id_competencia
    FROM perfil_competencia_empleado pce
    INNER JOIN grado_dominio gd ON gd.id_grado_dominio = pce.id_grado_dominio
    WHERE pce.id_competencia IS NULL;

    -- Si no quedaron huérfanas, promovemos a NOT NULL + FK + índice único
    IF NOT EXISTS (SELECT 1 FROM perfil_competencia_empleado WHERE id_competencia IS NULL)
    BEGIN
        -- ALTER COLUMN sólo si todavía es nullable
        IF EXISTS (
            SELECT 1 FROM sys.columns
            WHERE object_id = OBJECT_ID(''perfil_competencia_empleado'')
              AND name = ''id_competencia''
              AND is_nullable = 1
        )
            ALTER TABLE perfil_competencia_empleado
                ALTER COLUMN id_competencia INT NOT NULL;

        -- FK a competencia
        IF NOT EXISTS (
            SELECT 1 FROM sys.foreign_keys
            WHERE name = ''FK_perfil_competencia_empleado_competencia''
        )
            ALTER TABLE perfil_competencia_empleado
                ADD CONSTRAINT FK_perfil_competencia_empleado_competencia
                FOREIGN KEY (id_competencia) REFERENCES competencia (id_competencia);

        -- Índice único (id_empleado, id_competencia)
        IF NOT EXISTS (
            SELECT 1 FROM sys.indexes
            WHERE name = ''IX_perfil_competencia_empleado_emp_comp''
              AND object_id = OBJECT_ID(''perfil_competencia_empleado'')
        )
            CREATE UNIQUE INDEX IX_perfil_competencia_empleado_emp_comp
                ON perfil_competencia_empleado (id_empleado, id_competencia);
    END
');

-- 4.c Resto de columnas (no requieren dynamic SQL: nadie más las referencia)
IF COL_LENGTH('perfil_competencia_empleado', 'horas_reales') IS NULL
BEGIN
    ALTER TABLE perfil_competencia_empleado
        ADD horas_reales DECIMAL(10, 2) NOT NULL
        CONSTRAINT DF_perfil_competencia_empleado_horas_reales DEFAULT 0;
END

IF COL_LENGTH('perfil_competencia_empleado', 'horas_equivalentes') IS NULL
BEGIN
    ALTER TABLE perfil_competencia_empleado
        ADD horas_equivalentes DECIMAL(10, 2) NOT NULL
        CONSTRAINT DF_perfil_competencia_empleado_horas_equivalentes DEFAULT 0;
END

IF COL_LENGTH('perfil_competencia_empleado', 'fecha_calculo') IS NULL
BEGIN
    ALTER TABLE perfil_competencia_empleado
        ADD fecha_calculo DATETIME2 NULL;
END

-- 5. curso_competencia (tabla nueva) ------------------------------------------
IF OBJECT_ID('curso_competencia') IS NULL
BEGIN
    CREATE TABLE curso_competencia (
        id_curso_competencia INT IDENTITY(1, 1) PRIMARY KEY,
        id_curso INT NOT NULL,
        id_competencia INT NOT NULL,
        factor DECIMAL(5, 2) NOT NULL CONSTRAINT DF_curso_competencia_factor DEFAULT 1,
        created_at DATETIME2 NOT NULL,
        updated_at DATETIME2 NULL,
        state BIT NOT NULL CONSTRAINT DF_curso_competencia_state DEFAULT 1,
        CONSTRAINT FK_curso_competencia_curso
            FOREIGN KEY (id_curso) REFERENCES curso (id_curso),
        CONSTRAINT FK_curso_competencia_competencia
            FOREIGN KEY (id_competencia) REFERENCES competencia (id_competencia)
    );

    CREATE UNIQUE INDEX IX_curso_competencia_curso_comp
        ON curso_competencia (id_curso, id_competencia);
END

COMMIT;
