-- =============================================================================
-- ROLLBACK de la migración 20260521_equivalencias.sql
-- Idempotente: re-ejecutable sin error. Envuelto en TRAN: si algo falla,
-- se hace rollback automático.
--
-- ⚠️  ANTES de ejecutar:
--    1. Parar el contenedor api (docker stop proyecto-api-1) o la imagen que
--       está usando los modelos nuevos, porque al deshacer las columnas el
--       binario actual fallará al hacer SELECT.
--    2. Tener un backup. Si las columnas nuevas ya tenían datos poblados
--       (horas_acumuladas, horas_reales, horas_equivalentes, factor, etc.),
--       este script los pierde para siempre — restaurar desde backup en ese
--       caso.
--    3. Después del rollback, desplegar también la versión PREVIA del backend
--       (sin los modelos nuevos), o EF Core seguirá esperando columnas que ya
--       no existen.
--
-- Orden inverso al de la migración:
--   1. DROP tabla curso_competencia
--   2. Quitar índice y FK de perfil_competencia_empleado
--   3. Quitar columnas de perfil_competencia_empleado
--   4. Quitar columnas de empleado_curso
--   5. Quitar columna de puesto_curso
--   6. Quitar columna de grado_dominio
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

-- 1. Tabla nueva curso_competencia
IF OBJECT_ID('curso_competencia') IS NOT NULL
    DROP TABLE curso_competencia;

-- 2. Quitar índice y FK que el script de migración agregó a perfil_competencia_empleado
IF EXISTS (SELECT 1 FROM sys.indexes
           WHERE name = 'IX_perfil_competencia_empleado_emp_comp'
             AND object_id = OBJECT_ID('perfil_competencia_empleado'))
    DROP INDEX IX_perfil_competencia_empleado_emp_comp ON perfil_competencia_empleado;

IF EXISTS (SELECT 1 FROM sys.foreign_keys
           WHERE name = 'FK_perfil_competencia_empleado_competencia')
    ALTER TABLE perfil_competencia_empleado
        DROP CONSTRAINT FK_perfil_competencia_empleado_competencia;

-- 3. Columnas perfil_competencia_empleado
IF COL_LENGTH('perfil_competencia_empleado','id_competencia') IS NOT NULL
    ALTER TABLE perfil_competencia_empleado DROP COLUMN id_competencia;

IF COL_LENGTH('perfil_competencia_empleado','horas_reales') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.default_constraints
               WHERE name = 'DF_perfil_competencia_empleado_horas_reales')
        ALTER TABLE perfil_competencia_empleado
            DROP CONSTRAINT DF_perfil_competencia_empleado_horas_reales;
    ALTER TABLE perfil_competencia_empleado DROP COLUMN horas_reales;
END

IF COL_LENGTH('perfil_competencia_empleado','horas_equivalentes') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.default_constraints
               WHERE name = 'DF_perfil_competencia_empleado_horas_equivalentes')
        ALTER TABLE perfil_competencia_empleado
            DROP CONSTRAINT DF_perfil_competencia_empleado_horas_equivalentes;
    ALTER TABLE perfil_competencia_empleado DROP COLUMN horas_equivalentes;
END

IF COL_LENGTH('perfil_competencia_empleado','fecha_calculo') IS NOT NULL
    ALTER TABLE perfil_competencia_empleado DROP COLUMN fecha_calculo;

-- 4. Columnas empleado_curso
IF COL_LENGTH('empleado_curso','horas_acumuladas') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.default_constraints
               WHERE name = 'DF_empleado_curso_horas_acumuladas')
        ALTER TABLE empleado_curso DROP CONSTRAINT DF_empleado_curso_horas_acumuladas;
    ALTER TABLE empleado_curso DROP COLUMN horas_acumuladas;
END

IF COL_LENGTH('empleado_curso','fecha_calculo') IS NOT NULL
    ALTER TABLE empleado_curso DROP COLUMN fecha_calculo;

-- 5. Columna puesto_curso
IF COL_LENGTH('puesto_curso','horas_requeridas') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.default_constraints
               WHERE name = 'DF_puesto_curso_horas_requeridas')
        ALTER TABLE puesto_curso DROP CONSTRAINT DF_puesto_curso_horas_requeridas;
    ALTER TABLE puesto_curso DROP COLUMN horas_requeridas;
END

-- 6. Columna grado_dominio
IF COL_LENGTH('grado_dominio','horas_requeridas') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.default_constraints
               WHERE name = 'DF_grado_dominio_horas_requeridas')
        ALTER TABLE grado_dominio DROP CONSTRAINT DF_grado_dominio_horas_requeridas;
    ALTER TABLE grado_dominio DROP COLUMN horas_requeridas;
END

COMMIT;
