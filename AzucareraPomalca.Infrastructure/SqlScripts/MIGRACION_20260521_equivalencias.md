# Migración 20260521 — Equivalencias horas↔nivel + brecha en horas

Guía para aplicar el script `20260521_equivalencias.sql` contra la base de datos
de producción (Somee). Pensada para correrse una sola vez; el script es
**idempotente** y se puede re-ejecutar sin error.

> ⚠️ **Importante.** El contenedor `api` ya fue rebuildeado con los modelos
> nuevos. Si está corriendo apuntando a la BD de producción **antes** de aplicar
> esta migración, cualquier consulta a `GradoDominio`, `PerfilCompetenciaEmpleado`,
> `EmpleadoCurso`, `PuestoCurso` o `CursoCompetencia` va a fallar porque EF Core
> espera columnas/tablas que aún no existen. **Detener el api durante la migración.**

---

## 0. Contexto

- **Script:** `D:\tesis\proyecto\azucarera-pomalca-api\AzucareraPomalca.Infrastructure\SqlScripts\20260521_equivalencias.sql`
- **BD destino:** producción Somee
  - Server: `DB_AZUCARERAPOMALCA.mssql.somee.com,1433`
  - Database: `DB_AZUCARERAPOMALCA`
  - User: `azucarerapomalca_SQLLogin_1`
  - Password: `3wxv9fp4t4`
  - Opciones de conexión: `TrustServerCertificate=true; Encrypt=true`
- **Origen del connection string:** `D:\tesis\proyecto\.env` (variable `DB_CONNECTION_STRING`),
  inyectada al contenedor `api` por `docker-compose.yml`.
- **¿Por qué no migraciones EF?** El repositorio no usa migraciones; el equipo
  mantiene el esquema con scripts SQL manuales. Esta carpeta `SqlScripts/`
  centraliza esos scripts a partir de esta fecha.

---

## 1. Pre-flight (antes de tocar la BD)

### 1.1 Parar el api para evitar tráfico durante la migración

```powershell
docker stop proyecto-api-1
```

Mientras esté parado, el frontend no podrá hacer peticiones y ninguna llamada en
vuelo va a romperse a mitad de la migración.

### 1.2 Backup de la BD (muy recomendado)

Somee no expone `BACKUP DATABASE` por SQL directamente. Opciones:

- **Panel web Somee:** Databases → `DB_AZUCARERAPOMALCA` → "Backup database"
  → descargar el `.bak` o `.bacpac`.
- **SSMS:** click derecho sobre la BD → Tasks → Back Up… (si tu plan de Somee
  lo permite).
- **Alternativa rápida:** exportar a script las tablas que se van a tocar
  (`grado_dominio`, `puesto_curso`, `empleado_curso`, `perfil_competencia_empleado`)
  con datos, por si hay que reinsertarlas.

Sin backup, el plan de rollback de §5 sigue siendo válido pero solo te recupera
del esquema; los datos modificados quedan donde estén.

---

## 2. Conectarse a la BD de producción

Elige UNA de las tres opciones.

### Opción A — Panel web de Somee (sin instalar nada)

1. Login en `https://somee.com`.
2. Databases → `DB_AZUCARERAPOMALCA` → abrir "MSSQL Online Manager" o
   "Query Analyzer" (el nombre exacto depende del plan).
3. Abrir el archivo `20260521_equivalencias.sql` y pegar el contenido completo.
4. Ejecutar. Saltar a §3 para verificar.

### Opción B — SSMS / Azure Data Studio (recomendado)

1. New Connection:
   - Server type: Database Engine
   - Server name: `DB_AZUCARERAPOMALCA.mssql.somee.com,1433`
   - Authentication: SQL Server Authentication
   - Login: `azucarerapomalca_SQLLogin_1`
   - Password: `3wxv9fp4t4`
2. En **Connection Properties / Advanced**: marcar **Encrypt** y
   **Trust Server Certificate**.
3. En **Connect to database** elegir `DB_AZUCARERAPOMALCA`.
4. File → Open → `20260521_equivalencias.sql` → F5 para ejecutar.

### Opción C — sqlcmd (CLI)

Si tienes `sqlcmd` instalado localmente:

```powershell
sqlcmd `
  -S "DB_AZUCARERAPOMALCA.mssql.somee.com,1433" `
  -U "azucarerapomalca_SQLLogin_1" `
  -P "3wxv9fp4t4" `
  -d "DB_AZUCARERAPOMALCA" `
  -C `
  -i "D:\tesis\proyecto\azucarera-pomalca-api\AzucareraPomalca.Infrastructure\SqlScripts\20260521_equivalencias.sql"
```

Si NO tienes `sqlcmd` local, usa el del contenedor `proyecto-db-1`:

```powershell
docker cp `
  "D:\tesis\proyecto\azucarera-pomalca-api\AzucareraPomalca.Infrastructure\SqlScripts\20260521_equivalencias.sql" `
  proyecto-db-1:/tmp/mig.sql

docker exec proyecto-db-1 /opt/mssql-tools18/bin/sqlcmd `
  -S "DB_AZUCARERAPOMALCA.mssql.somee.com,1433" `
  -U "azucarerapomalca_SQLLogin_1" `
  -P "3wxv9fp4t4" `
  -d "DB_AZUCARERAPOMALCA" `
  -C `
  -i /tmp/mig.sql
```

Flags:
- `-C` ⇒ Trust Server Certificate (equivale a `TrustServerCertificate=true`).
- `-i` ⇒ archivo de entrada.

---

## 3. Qué hace el script (referencia)

Todo va dentro de `SET XACT_ABORT ON; BEGIN TRAN ... COMMIT`. Si algo falla a
mitad de camino, **se hace rollback automático**.

1. **`grado_dominio`** ⇒ agrega `horas_requeridas INT NOT NULL DEFAULT 0`.
2. **`puesto_curso`** ⇒ agrega `horas_requeridas INT NOT NULL DEFAULT 0`.
3. **`empleado_curso`** ⇒ agrega:
   - `horas_acumuladas DECIMAL(10,2) NOT NULL DEFAULT 0`
   - `fecha_calculo DATETIME2 NULL`
4. **`perfil_competencia_empleado`**:
   1. `id_competencia INT NULL` (se agrega como nullable).
   2. Backfill: `UPDATE pce SET id_competencia = gd.id_competencia FROM ... INNER JOIN grado_dominio gd ON gd.id_grado_dominio = pce.id_grado_dominio WHERE pce.id_competencia IS NULL;`
   3. Si **TODAS** las filas quedaron con `id_competencia` no nulo ⇒ pasa la
      columna a `NOT NULL` + agrega FK `FK_perfil_competencia_empleado_competencia`
      + crea índice único `IX_perfil_competencia_empleado_emp_comp (id_empleado, id_competencia)`.
      Si quedan huérfanas (filas con `id_grado_dominio` NULL), se deja nullable
      y se omite el índice único, **sin abortar**.
   4. Agrega `horas_reales DECIMAL(10,2) NOT NULL DEFAULT 0`.
   5. Agrega `horas_equivalentes DECIMAL(10,2) NOT NULL DEFAULT 0`.
   6. Agrega `fecha_calculo DATETIME2 NULL`.
5. **Nueva tabla `curso_competencia`**:
   - PK `id_curso_competencia INT IDENTITY`.
   - `id_curso INT NOT NULL` ⇒ FK `FK_curso_competencia_curso → curso(id_curso)`.
   - `id_competencia INT NOT NULL` ⇒ FK `FK_curso_competencia_competencia → competencia(id_competencia)`.
   - `factor DECIMAL(5,2) NOT NULL DEFAULT 1`.
   - `created_at DATETIME2 NOT NULL`, `updated_at DATETIME2 NULL`, `state BIT NOT NULL DEFAULT 1`.
   - Índice único `IX_curso_competencia_curso_comp (id_curso, id_competencia)`.

Cada `ALTER` está protegido por `IF COL_LENGTH(...) IS NULL` y la tabla nueva
por `IF OBJECT_ID(...) IS NULL`, así que **re-ejecutarlo es seguro**.

---

## 4. Verificación post-migración

Pega esto y ejecuta en la misma conexión. Debes ver 8 filas, **todas con `OK`**:

```sql
SELECT 'grado_dominio.horas_requeridas' AS check_name,
       CASE WHEN COL_LENGTH('grado_dominio','horas_requeridas') IS NOT NULL THEN 'OK' ELSE 'FAIL' END AS status
UNION ALL SELECT 'puesto_curso.horas_requeridas',
       CASE WHEN COL_LENGTH('puesto_curso','horas_requeridas') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'empleado_curso.horas_acumuladas',
       CASE WHEN COL_LENGTH('empleado_curso','horas_acumuladas') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'empleado_curso.fecha_calculo',
       CASE WHEN COL_LENGTH('empleado_curso','fecha_calculo') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'perfil_competencia_empleado.id_competencia',
       CASE WHEN COL_LENGTH('perfil_competencia_empleado','id_competencia') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'perfil_competencia_empleado.horas_reales',
       CASE WHEN COL_LENGTH('perfil_competencia_empleado','horas_reales') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'perfil_competencia_empleado.horas_equivalentes',
       CASE WHEN COL_LENGTH('perfil_competencia_empleado','horas_equivalentes') IS NOT NULL THEN 'OK' ELSE 'FAIL' END
UNION ALL SELECT 'curso_competencia table',
       CASE WHEN OBJECT_ID('curso_competencia') IS NOT NULL THEN 'OK' ELSE 'FAIL' END;
```

Verifica también que no haya filas huérfanas en `perfil_competencia_empleado`:

```sql
SELECT COUNT(*) AS huerfanas_sin_competencia
FROM perfil_competencia_empleado WHERE id_competencia IS NULL;
```

Resultado esperado: `0`. Si es `> 0`, son filas viejas con `id_grado_dominio NULL`
que el script no pudo mapear — el índice único y el `NOT NULL` quedaron sin
aplicarse (sin romper nada). En ese caso, decidir si esas filas se borran
manualmente (eran "soft delete" implícito) o se les asigna una competencia, y
luego re-ejecutar el script para que cierre el `NOT NULL` y el índice único.

Verifica los índices y FKs nuevos:

```sql
SELECT name, object_id FROM sys.indexes
WHERE name IN ('IX_perfil_competencia_empleado_emp_comp', 'IX_curso_competencia_curso_comp');

SELECT name FROM sys.foreign_keys
WHERE name IN ('FK_perfil_competencia_empleado_competencia',
               'FK_curso_competencia_curso',
               'FK_curso_competencia_competencia');
```

Deberías ver los 2 índices y 3 FKs (o 2 FKs + el de PCE si no quedó por huérfanas).

---

## 5. Levantar el api con la nueva versión

```powershell
docker start proyecto-api-1
docker logs -f proyecto-api-1
```

Logs esperados:
```
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: http://[::]:8080
info: Microsoft.Hosting.Lifetime[0]
      Application started.
```

Sin excepciones de EF en los logs ⇒ el modelo C# coincide con el esquema migrado.

Smoke test rápido (necesitas un token JWT válido; si no, basta con verificar que
Swagger expone los nuevos endpoints):

```powershell
Invoke-WebRequest -Uri "http://localhost:5246/swagger/v1/swagger.json" -UseBasicParsing |
    Select-Object -ExpandProperty Content |
    ConvertFrom-Json |
    Select-Object -ExpandProperty paths |
    ForEach-Object PSObject |
    Select-Object -ExpandProperty Properties |
    Select-Object -ExpandProperty Name |
    Where-Object { $_ -like '*cursocompetencia*' -or $_ -like '*equivalencias*' }
```

Deberías ver:
```
/api/cursocompetencia/{id}
/api/cursocompetencia/competencia/{idCompetencia}
/api/cursocompetencia
/api/equivalencias/regularizar
/api/cursocompetencia/paginatedsearch
```

Smoke funcional end-to-end (cuando tengas datos):

1. Login → obtener token JWT.
2. Crear una competencia "Comunicación" con grados 10/30/60/100 h
   (desde la UI: `Competencias` → editar → tabla "Grado Dominio" → escribir
   las horas requeridas por nivel).
3. En la misma modal, sección **"Cursos que aportan (habilidades blandas)"**
   → Agregar curso → seleccionar 2 cursos blandos (TipoCurso = 3) → poner factor.
4. Crear una `Capacitacion` de uno de esos cursos, asignar un empleado aprobado,
   evaluar.
5. Ver al empleado en `Empleados → Perfil de competencias` → columna **Progreso**
   debe mostrar `NivelProgress` con horas equivalentes y nivel derivado.

---

## 6. Regularización (poblar snapshots históricos)

Después de migrar, los empleados con nivel asignado a mano antes del cambio
tendrán `horas_equivalentes = 0`. Para sembrarlos con el umbral de su nivel
actual sin que nadie pierda nivel, llamar al endpoint admin:

```http
POST http://localhost:5246/api/equivalencias/regularizar
Authorization: Bearer <token>
```

Respuesta JSON:
```json
{
  "procesadosBlandos": 12,
  "procesadosDuros": 34,
  "omitidos": 0
}
```

- **procesadosBlandos:** PCE con `id_grado_dominio != null` y `horas_equivalentes = 0`
  ⇒ se les seteó `horas_equivalentes = horas_reales = grado_dominio.horas_requeridas`
  + `fecha_calculo = UtcNow`.
- **procesadosDuros:** EmpleadoCurso con `horas_acumuladas = 0` ⇒ se les seteó
  `horas_acumuladas = puesto_curso.horas_requeridas` del puesto del empleado.
- **omitidos:** filas con datos faltantes (sin GradoDominio, sin PuestoCurso del
  puesto del empleado, o empleado borrado).

El endpoint es **idempotente**: re-ejecutarlo no duplica nada; las filas ya
sembradas (`horas_* > 0`) se saltan.

---

## 7. Plan de rollback

> **Archivo ejecutable directo:** `20260521_equivalencias_rollback.sql` (en
> esta misma carpeta). Lo puedes correr exactamente con los mismos
> mecanismos del Paso 2 (panel Somee, SSMS, sqlcmd). Si prefieres copiar y
> pegar, el contenido se reproduce abajo.

**Antes de ejecutar el rollback:**

1. **Parar el api** (`docker stop proyecto-api-1`) — la imagen actual usa los
   modelos nuevos y va a fallar en cuanto se quiten las columnas.
2. **Backup**. Si las columnas nuevas ya tenían datos (`horas_acumuladas`,
   `horas_reales`, `horas_equivalentes`, `factor` de `curso_competencia`, etc.),
   este script los pierde. Restaurar desde backup si necesitas conservarlos.
3. **Plan posterior:** desplegar la versión PREVIA del backend (sin los
   modelos nuevos) o EF Core seguirá esperando columnas que ya no existen.

```sql
SET XACT_ABORT ON;
BEGIN TRAN;

-- 1. Tabla nueva
IF OBJECT_ID('curso_competencia') IS NOT NULL DROP TABLE curso_competencia;

-- 2. Quitar índice/FK de perfil_competencia_empleado
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
    ALTER TABLE perfil_competencia_empleado
        DROP CONSTRAINT DF_perfil_competencia_empleado_horas_reales;
    ALTER TABLE perfil_competencia_empleado DROP COLUMN horas_reales;
END

IF COL_LENGTH('perfil_competencia_empleado','horas_equivalentes') IS NOT NULL
BEGIN
    ALTER TABLE perfil_competencia_empleado
        DROP CONSTRAINT DF_perfil_competencia_empleado_horas_equivalentes;
    ALTER TABLE perfil_competencia_empleado DROP COLUMN horas_equivalentes;
END

IF COL_LENGTH('perfil_competencia_empleado','fecha_calculo') IS NOT NULL
    ALTER TABLE perfil_competencia_empleado DROP COLUMN fecha_calculo;

-- 4. Columnas empleado_curso
IF COL_LENGTH('empleado_curso','horas_acumuladas') IS NOT NULL
BEGIN
    ALTER TABLE empleado_curso DROP CONSTRAINT DF_empleado_curso_horas_acumuladas;
    ALTER TABLE empleado_curso DROP COLUMN horas_acumuladas;
END

IF COL_LENGTH('empleado_curso','fecha_calculo') IS NOT NULL
    ALTER TABLE empleado_curso DROP COLUMN fecha_calculo;

-- 5. Columnas puesto_curso
IF COL_LENGTH('puesto_curso','horas_requeridas') IS NOT NULL
BEGIN
    ALTER TABLE puesto_curso DROP CONSTRAINT DF_puesto_curso_horas_requeridas;
    ALTER TABLE puesto_curso DROP COLUMN horas_requeridas;
END

-- 6. Columnas grado_dominio
IF COL_LENGTH('grado_dominio','horas_requeridas') IS NOT NULL
BEGIN
    ALTER TABLE grado_dominio DROP CONSTRAINT DF_grado_dominio_horas_requeridas;
    ALTER TABLE grado_dominio DROP COLUMN horas_requeridas;
END

COMMIT;
```

> Después del rollback, **también** hay que revertir el api a la imagen
> anterior (sin los modelos nuevos), porque el binario actual va a seguir
> esperando las columnas que acabamos de eliminar.
>
> Si el rollback se hace **después** de poblar datos en las columnas nuevas,
> esos datos se pierden — restaurar desde el backup de §1.2.

---

## 8. Solución de problemas comunes

| Síntoma | Causa probable | Acción |
|---|---|---|
| `Invalid column name 'horas_requeridas'` en logs del api | Migración no aplicada o se aplicó a otra BD | Reverificar §4; confirmar que `ConnectionStrings__DbConnection` apunta a la BD que migraste |
| `Cannot insert duplicate key row in object 'curso_competencia' with unique index 'IX_curso_competencia_curso_comp'` | Intentar vincular el mismo `(idCurso, idCompetencia)` dos veces | Es la protección por diseño; eliminar el vínculo previo antes de re-crear |
| `Cannot insert NULL into column 'id_competencia'` al crear PCE | Cliente no envía `idCompetencia` | El frontend lo envía cuando se asigna competencia; revisar payload del request |
| `huerfanas_sin_competencia > 0` después del script | Filas viejas con `id_grado_dominio NULL` en `perfil_competencia_empleado` | Decidir: borrar las filas (`DELETE WHERE id_competencia IS NULL`) o asignarles una competencia, luego re-ejecutar el script |
| Logs del api: `Connection Timeout` apuntando a `somee.com` | Latencia o caída temporal de Somee | Reintentar; si persiste, revisar status de Somee |
| `Login failed for user 'azucarerapomalca_SQLLogin_1'` | Password rotado o IP bloqueada | Verificar credenciales en `.env` y permisos en panel Somee |

---

## 9. Resumen de archivos relacionados

- **Script SQL:** `azucarera-pomalca-api/AzucareraPomalca.Infrastructure/SqlScripts/20260521_equivalencias.sql`
- **Esta guía:** `azucarera-pomalca-api/AzucareraPomalca.Infrastructure/SqlScripts/MIGRACION_20260521_equivalencias.md`
- **Modelos C# afectados:** `AzucareraPomalca.Domain/Models/{GradoDominio,PuestoCurso,EmpleadoCurso,PerfilCompetenciaEmpleado,CursoCompetencia}.cs`
- **Configuraciones EF:** `AzucareraPomalca.Infrastructure/Configurations/{GradoDominio,PuestoCurso,EmpleadoCurso,PerfilCompetenciaEmpleado,CursoCompetencia}Configuration.cs`
- **Servicio que aplica las reglas:** `AzucareraPomalca.Application/Services/Implementations/EquivalenciaService.cs`
- **Hook automático:** `AzucareraPomalca.Application/Services/Implementations/CapacitacionService.cs` (método `EvaluarAsync`)
- **Endpoint regularizar:** `AzucareraPomalca.Api/Controllers/EquivalenciasController.cs`
- **Endpoints CursoCompetencia:** `AzucareraPomalca.Api/Controllers/CursoCompetenciaController.cs`
- **UI configuración (frontend):** `azucarera-pomalca-front/src/views/competencias/components/ModalSaveCompetencia.tsx`
- **UI puesto-curso (frontend):** `azucarera-pomalca-front/src/views/mof/components/Capacitacion.tsx`
- **UI empleado (frontend):** `azucarera-pomalca-front/src/views/empleados/components/PerfilCompetencias.tsx`
- **Componente progreso:** `azucarera-pomalca-front/src/core/components/general/NivelProgress.tsx`

---

## 10. Checklist final (marca cada paso al ejecutarlo)

- [ ] `docker stop proyecto-api-1`
- [ ] Backup de `DB_AZUCARERAPOMALCA` (panel Somee o equivalente)
- [ ] Conexión a la BD elegida (Opción A / B / C)
- [ ] Ejecutar `20260521_equivalencias.sql`
- [ ] Correr la verificación de §4 ⇒ 8 filas en `OK`, `huerfanas_sin_competencia = 0`
- [ ] `docker start proyecto-api-1` y revisar logs sin excepciones de EF
- [ ] Swagger expone `/api/cursocompetencia/*` y `/api/equivalencias/regularizar`
- [ ] `POST /api/equivalencias/regularizar` ⇒ respuesta `{ procesadosBlandos, procesadosDuros, omitidos }`
- [ ] Smoke funcional desde el frontend (crear competencia → vincular cursos → evaluar capacitación → ver progreso)
