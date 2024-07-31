using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoRepository : CrudRepository<Empleado, int>, IEmpleadoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmpleadoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Empleado>> FindByIdPuestoAsync(int id)
        {
            return await _dbContext.Set<Empleado>()
                .Where(t => t.IdPuesto == id)
                .ToListAsync();
        }

        public override async Task<Empleado?> FindByIdAsync(int id)
        {
            return await _dbContext.Set<Empleado>()
                .Include(t => t.CondicionEmpleado)
                .Include(t => t.Puesto).ThenInclude(t => t.ClaseOcupacional).ThenInclude(t => t.GrupoOcupacional)
                .Include(t => t.Puesto).ThenInclude(t => t.Gerencia)
                .Include(t => t.Puesto).ThenInclude(t => t.Division)
                .Include(t => t.Puesto).ThenInclude(t => t.Departamento)
                .Include(t => t.Puesto).ThenInclude(t => t.Seccion)
                //.Include(t => t.Puesto).ThenInclude(t => t.PuestosProfesiones.Where(t => t.State == true)).ThenInclude(t => t.Profesion)
                .Include(t => t.Puesto).ThenInclude(t => t.PuestosCursos.Where(t => t.State == true)).ThenInclude(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.Puesto).ThenInclude(t => t.PerfilCompetencias.Where(t => t.State == true)).ThenInclude(t => t.GradoDominio).ThenInclude(t => t.CompetenciaSimple).ThenInclude(t => t.TipoCompetencia)
                //.Include(t => t.EmpleadoProfesiones.Where(t => t.State == true)).ThenInclude(t => t.Profesion)
                //.Include(t => t.EmpleadoProfesiones.Where(t => t.State == true)).ThenInclude(t => t.GradoAcademico)
                .Include(t => t.CondicionEmpleado)
                //.Include(t => t.ExperienciaLaborales.Where(t => t.State == true))
                .Include(t => t.EmpleadoCursos.Where(t => t.State == true)).ThenInclude(t => t.Curso).ThenInclude(t => t.TipoCurso)
                .Include(t => t.PerfilCompetenciaEmpleados.Where(t => t.State == true)).ThenInclude(t => t.GradoDominio).ThenInclude(t => t.CompetenciaSimple).ThenInclude(t => t.TipoCompetencia)
                .Include(t => t.EstadoCivil)
                .Include(t => t.Sexo)
                .Include(t => t.TipoDocumentoIdentidad)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Empleado?> FindByNumeroDocumentoAsync(string numeroDocumento)
        {
            //Empleado? data = null;

            //var sql = "sp_findEmpleadoByNumeroDocumento";

            //DbConnection connection = _dbContext.Database.GetDbConnection();

            //DbCommand command = connection.CreateCommand();
            //command.CommandText = sql;
            //command.CommandType = CommandType.StoredProcedure;

            //#region "Parameters"
            //var p_numeroDocumento = command.CreateParameter();
            //p_numeroDocumento.ParameterName = "@numeroDocumento";
            //p_numeroDocumento.Value = numeroDocumento;
            //command.Parameters.Add(p_numeroDocumento);
            //#endregion

            //await connection.OpenAsync();

            //using IDataReader reader = await command.ExecuteReaderAsync();

            //while (reader.Read())
            //{
            //    data = new Empleado()
            //    {
            //        Id = !reader.IsDBNull(reader.GetOrdinal("id_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_empleado")) : 0,
            //        Nombres = !reader.IsDBNull(reader.GetOrdinal("nombres")) ? reader.GetString(reader.GetOrdinal("nombres")) : "",
            //        NumeroDocumento = !reader.IsDBNull(reader.GetOrdinal("numero_documento")) ? reader.GetString(reader.GetOrdinal("numero_documento")) : null,
            //        AppellidoPaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_paterno")) ? reader.GetString(reader.GetOrdinal("apellido_paterno")) : "",
            //        AppellidoMaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? reader.GetString(reader.GetOrdinal("apellido_materno")) : "",
            //        InicioPeriodo = !reader.IsDBNull(reader.GetOrdinal("inicio_periodo")) ? reader.GetDateTime(reader.GetOrdinal("inicio_periodo")) : null,
            //        IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
            //        IdCondicionEmpleado = !reader.IsDBNull(reader.GetOrdinal("id_condicion_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_condicion_empleado")) : 0,
            //        IdEstadoCivil = !reader.IsDBNull(reader.GetOrdinal("id_estado_civil")) ? reader.GetInt32(reader.GetOrdinal("id_estado_civil")) : null,
            //        IdSexo = !reader.IsDBNull(reader.GetOrdinal("id_sexo")) ? reader.GetInt32(reader.GetOrdinal("id_sexo")) : null,
            //        IdTipoDocumentoIdentidad = !reader.IsDBNull(reader.GetOrdinal("id_tipo_documento_identidad")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_documento_identidad")) : null,
            //        FinPeriodo = !reader.IsDBNull(reader.GetOrdinal("fin_periodo")) ? reader.GetDateTime(reader.GetOrdinal("fin_periodo")) : null,
            //        FechaNacimiento = !reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")) ? reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")) : null,
            //        Direccion = !reader.IsDBNull(reader.GetOrdinal("direccion")) ? reader.GetString(reader.GetOrdinal("direccion")) : null,
            //        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
            //        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
            //        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
            //    };
            //}

            ////await connection.CloseAsync();
            //return data;



            using (var cnx = _dbContext.Database.GetDbConnection())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@numeroDocumento", numeroDocumento);

                using (var reader = await cnx.ExecuteReaderAsync(
                    "sp_findEmpleadoByNumeroDocumento",
                    param: parameters,
                    commandType: CommandType.StoredProcedure))
                {
                    var result = new Empleado();

                    while (reader.Read())
                    {
                        result = new Empleado()
                        {
                            Id = !reader.IsDBNull(reader.GetOrdinal("id_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_empleado")) : 0,
                            Nombres = !reader.IsDBNull(reader.GetOrdinal("nombres")) ? reader.GetString(reader.GetOrdinal("nombres")) : "",
                            NumeroDocumento = !reader.IsDBNull(reader.GetOrdinal("numero_documento")) ? reader.GetString(reader.GetOrdinal("numero_documento")) : null,
                            AppellidoPaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_paterno")) ? reader.GetString(reader.GetOrdinal("apellido_paterno")) : "",
                            AppellidoMaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? reader.GetString(reader.GetOrdinal("apellido_materno")) : "",
                            InicioPeriodo = !reader.IsDBNull(reader.GetOrdinal("inicio_periodo")) ? reader.GetDateTime(reader.GetOrdinal("inicio_periodo")) : null,
                            IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
                            IdCondicionEmpleado = !reader.IsDBNull(reader.GetOrdinal("id_condicion_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_condicion_empleado")) : 0,
                            IdEstadoCivil = !reader.IsDBNull(reader.GetOrdinal("id_estado_civil")) ? reader.GetInt32(reader.GetOrdinal("id_estado_civil")) : null,
                            IdSexo = !reader.IsDBNull(reader.GetOrdinal("id_sexo")) ? reader.GetInt32(reader.GetOrdinal("id_sexo")) : null,
                            IdTipoDocumentoIdentidad = !reader.IsDBNull(reader.GetOrdinal("id_tipo_documento_identidad")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_documento_identidad")) : null,
                            FinPeriodo = !reader.IsDBNull(reader.GetOrdinal("fin_periodo")) ? reader.GetDateTime(reader.GetOrdinal("fin_periodo")) : null,
                            FechaNacimiento = !reader.IsDBNull(reader.GetOrdinal("fecha_nacimiento")) ? reader.GetDateTime(reader.GetOrdinal("fecha_nacimiento")) : null,
                            Direccion = !reader.IsDBNull(reader.GetOrdinal("direccion")) ? reader.GetString(reader.GetOrdinal("direccion")) : null,
                            CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                            UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                            State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        };
                    }
                    return result;
                }
            }
        }

        public async void GuardarMasivoAsync(List<DtEmpleado> empleados)
        {
            string connectionString = "Data Source=DB_AZUCARERA_POMALCA.mssql.somee.com;Initial Catalog=DB_AZUCARERA_POMALCA; User ID=azucarerapomalca_SQLLogin_1;Password=3wxv9fp4t4;TrustServerCertificate=true";

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "sp_guardarMasivoEmpleado";
                    command.CommandType = CommandType.StoredProcedure;

                    // Crea la tabla de empleados
                    DataTable dtEmpleados = new DataTable();
                    dtEmpleados.Columns.Add("Id", typeof(int));
                    dtEmpleados.Columns.Add("Nombres", typeof(string));
                    dtEmpleados.Columns.Add("AppellidoPaterno", typeof(string));
                    dtEmpleados.Columns.Add("AppellidoMaterno", typeof(string));
                    dtEmpleados.Columns.Add("CreatedAt", typeof(DateTime));
                    dtEmpleados.Columns["CreatedAt"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("UpdatedAt", typeof(DateTime));
                    dtEmpleados.Columns["UpdatedAt"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("State", typeof(bool));
                    dtEmpleados.Columns.Add("InicioPeriodo", typeof(DateTime));
                    dtEmpleados.Columns["InicioPeriodo"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("IdCondicionEmpleado", typeof(int));
                    dtEmpleados.Columns.Add("IdPuesto", typeof(int));
                    dtEmpleados.Columns.Add("IdEstadoCivil", typeof(int));
                    dtEmpleados.Columns["IdEstadoCivil"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("IdSexo", typeof(int));
                    dtEmpleados.Columns["IdSexo"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("IdTipoDocumentoIdentidad", typeof(int));
                    dtEmpleados.Columns["IdTipoDocumentoIdentidad"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("NumeroDocumento", typeof(string));
                    dtEmpleados.Columns["NumeroDocumento"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("FechaNacimiento", typeof(DateTime));
                    dtEmpleados.Columns["FechaNacimiento"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("Direccion", typeof(string));
                    dtEmpleados.Columns["Direccion"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("FinPeriodo", typeof(DateTime));
                    dtEmpleados.Columns["FinPeriodo"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("CodigoArea", typeof(string));
                    dtEmpleados.Columns["CodigoArea"].AllowDBNull = true;
                    dtEmpleados.Columns.Add("CodigoCargo", typeof(string));
                    dtEmpleados.Columns["CodigoCargo"].AllowDBNull = true;

                    foreach (DtEmpleado empleado in empleados)
                    {
                        dtEmpleados.Rows.Add(
                            empleado.Id,
                            empleado.Nombres,
                            empleado.AppellidoPaterno,
                            empleado.AppellidoMaterno,
                            //empleado.CreatedAt,
                            null,
                            //empleado.UpdatedAt,
                            null,
                            empleado.State,
                            //empleado.InicioPeriodo,
                            empleado.InicioPeriodoString,
                            //null,
                            empleado.IdCondicionEmpleado,
                            empleado.IdPuesto,
                            empleado.IdEstadoCivil,
                            empleado.IdSexo,
                            empleado.IdTipoDocumentoIdentidad,
                            empleado.NumeroDocumento,
                            //empleado.FechaNacimiento,
                            empleado.FechaNacimientoString,
                            //null,
                            empleado.Direccion,
                            //empleado.FinPeriodo
                            empleado.FinPeriodoString,
                            //null
                            empleado.CodigoArea,
                            empleado.CodigoCargo
                            );
                    }

                    // Agrega el parámetro para la lista de empleados
                    SqlParameter parameter = command.Parameters.AddWithValue("@EmployeeList", dtEmpleados);
                    parameter.SqlDbType = SqlDbType.Structured;
                    parameter.TypeName = "dbo.EmployeeList";

                    // Ejecuta el comando
                    await command.ExecuteNonQueryAsync();
                }

                // Cierra la conexión
                await connection.CloseAsync();
            }
        }

        public async Task<PagedResult<EmpleadoSugerido>> ListarEmpleadosSugeridosAsync(Paging pagin, EmpleadoSugerido request)
        {
            List<EmpleadoSugerido> data = new List<EmpleadoSugerido>();

            var sql = "sp_listarEmpleadosSugeridos";

            DbConnection connection = _dbContext.Database.GetDbConnection();

            DbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            #region "Parameters"
            var p_nombre = command.CreateParameter();
            p_nombre.ParameterName = "@nombre";
            p_nombre.Value = request.Nombre;
            command.Parameters.Add(p_nombre);

            var p_id_puesto = command.CreateParameter();
            p_id_puesto.ParameterName = "@id_puesto";
            p_id_puesto.Value = request.IdPuesto;
            command.Parameters.Add(p_id_puesto);

            var p_id_gerencia = command.CreateParameter();
            p_id_gerencia.ParameterName = "@id_gerencia";
            p_id_gerencia.Value = request.IdGerencia;
            command.Parameters.Add(p_id_gerencia);

            var p_id_division = command.CreateParameter();
            p_id_division.ParameterName = "@id_division";
            p_id_division.Value = request.IdDivision;
            command.Parameters.Add(p_id_division);

            var p_id_departamento = command.CreateParameter();
            p_id_departamento.ParameterName = "@id_departamento";
            p_id_departamento.Value = request.IdDepartamento;
            command.Parameters.Add(p_id_departamento);

            var p_id_seccion = command.CreateParameter();
            p_id_seccion.ParameterName = "@id_seccion";
            p_id_seccion.Value = request.IdSeccion;
            command.Parameters.Add(p_id_seccion);
            #endregion

            await connection.OpenAsync();

            using IDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var item = new EmpleadoSugerido()
                {
                    IdEmpleado = !reader.IsDBNull(reader.GetOrdinal("id_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_empleado")) : null,
                    Nombre = !reader.IsDBNull(reader.GetOrdinal("nombres")) ? reader.GetString(reader.GetOrdinal("nombres")) : null,
                    ApellidoPaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_paterno")) ? reader.GetString(reader.GetOrdinal("apellido_paterno")) : null,
                    ApellidoMaterno = !reader.IsDBNull(reader.GetOrdinal("apellido_materno")) ? reader.GetString(reader.GetOrdinal("apellido_materno")) : null,
                    Puesto = !reader.IsDBNull(reader.GetOrdinal("puesto")) ? reader.GetString(reader.GetOrdinal("puesto")) : null,
                    Gerencia = !reader.IsDBNull(reader.GetOrdinal("gerencia")) ? reader.GetString(reader.GetOrdinal("gerencia")) : null,
                    Division = !reader.IsDBNull(reader.GetOrdinal("division")) ? reader.GetString(reader.GetOrdinal("division")) : null,
                    Departamento = !reader.IsDBNull(reader.GetOrdinal("departamento")) ? reader.GetString(reader.GetOrdinal("departamento")) : null,
                    Seccion = !reader.IsDBNull(reader.GetOrdinal("seccion")) ? reader.GetString(reader.GetOrdinal("seccion")) : null,
                    CursosFaltantes = !reader.IsDBNull(reader.GetOrdinal("cursos_faltantes")) ? reader.GetInt32(reader.GetOrdinal("cursos_faltantes")) : null
                };

                data.Add(item);
            }

            await connection.CloseAsync();

            return GetPagedResultCursoDuroSugerido(data, pagin);
        }

        private PagedResult<EmpleadoSugerido> GetPagedResultCursoDuroSugerido(List<EmpleadoSugerido> data, Paging pagin)
        {
            var totalElements = data.Count;
            var skip = (pagin.PageNumber - 1) * pagin.PageSize;
            var paginatedItems = data.Skip(skip)
                .Take(pagin.PageSize)
                .ToList();

            return new PagedResult<EmpleadoSugerido>(paginatedItems, pagin, totalElements);
        }
    }
}
