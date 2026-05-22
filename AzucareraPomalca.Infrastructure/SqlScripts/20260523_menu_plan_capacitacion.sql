-- =============================================================================
-- Menú de navegación para el módulo Plan de Capacitación.
-- Idempotente: re-ejecutable sin duplicar.
--
-- Qué hace:
--   1. Crea el menú "Planes de Capacitación" (url_menu = '/planes-capacitacion')
--      como HERMANO de "Capacitaciones": mismo id_menu_padre, nivel e icono, de
--      modo que el Sidebar lo pinte como link hijo bajo el mismo encabezado.
--   2. Otorga permiso a TODOS los roles que ya tienen acceso a '/capacitaciones'.
--
-- IMPORTANTE (D-010): TODA la lógica va dentro de un único EXEC(N'...'). El panel
-- web de Somee parte el script por sentencia, lo que rompe el alcance de las
-- variables DECLARE (error "Must declare the scalar variable @idPadre"). La
-- dynamic SQL es 1 sola sentencia para el parser externo y preserva las variables.
-- Las comillas simples internas se escapan duplicándolas ('').
--
-- Tras ejecutarlo, recargar la app / volver a iniciar sesión: el sidebar cachea
-- los menús por rol (useMenusByIdRol).
-- =============================================================================

SET XACT_ABORT ON;
BEGIN TRAN;

EXEC(N'
    DECLARE @urlNuevo NVARCHAR(200) = N''/planes-capacitacion'';
    DECLARE @urlRef   NVARCHAR(200) = N''/capacitaciones'';

    -- Referencia: menú de Capacitaciones (hereda padre, nivel, orden).
    -- El icono NO se hereda: usamos uno propio para distinguir visualmente el
    -- módulo de su hermano "Capacitaciones" (ambos compartían fa-graduation-cap).
    DECLARE @idPadre INT, @nivel INT, @orden INT;
    DECLARE @icono NVARCHAR(100) = N''fa-solid fa-clipboard-list'';
    SELECT TOP 1
        @idPadre = id_menu_padre,
        @nivel   = nivel,
        @orden   = orden
    FROM menu
    WHERE url_menu = @urlRef;

    -- 1. Crear el menú si no existe.
    IF NOT EXISTS (SELECT 1 FROM menu WHERE url_menu = @urlNuevo)
        INSERT INTO menu (nombre, orden, nivel, icono, url_menu, visible, created_at, state, id_menu_padre)
        VALUES (N''Planes de Capacitación'', ISNULL(@orden, 0) + 1, @nivel, @icono, @urlNuevo, 1, SYSUTCDATETIME(), 1, @idPadre);

    DECLARE @idMenu INT = (SELECT TOP 1 id_menu FROM menu WHERE url_menu = @urlNuevo);

    -- 1b. Idempotente: asegurar el icono propio aunque la fila ya existiera con
    -- el icono heredado de Capacitaciones (corrige instalaciones previas).
    UPDATE menu SET icono = @icono WHERE id_menu = @idMenu AND ISNULL(icono, N'''') <> @icono;

    -- 2. Otorgar permiso a los roles que ya ven Capacitaciones (sin duplicar).
    INSERT INTO permiso (id_menu, id_rol, created_at, state)
    SELECT @idMenu, p.id_rol, SYSUTCDATETIME(), 1
    FROM permiso p
    INNER JOIN menu m ON m.id_menu = p.id_menu AND m.url_menu = @urlRef
    WHERE NOT EXISTS (
        SELECT 1 FROM permiso p2 WHERE p2.id_menu = @idMenu AND p2.id_rol = p.id_rol
    );
');

COMMIT;
