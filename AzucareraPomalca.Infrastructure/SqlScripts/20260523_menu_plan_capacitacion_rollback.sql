-- =============================================================================
-- ROLLBACK de 20260523_menu_plan_capacitacion.sql
-- Idempotente. Quita el menú "Planes de Capacitación" y sus permisos.
-- Lógica en EXEC(N'...') por el mismo motivo del forward (D-010: Somee parte el
-- batch y rompe el alcance de @idMenu).
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

EXEC(N'
    DECLARE @idMenu INT = (SELECT TOP 1 id_menu FROM menu WHERE url_menu = N''/planes-capacitacion'');

    IF @idMenu IS NOT NULL
    BEGIN
        DELETE FROM permiso WHERE id_menu = @idMenu;
        DELETE FROM menu WHERE id_menu = @idMenu;
    END
');

COMMIT;
