using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoCursoRepository : CrudRepository<EmpleadoCurso, int>, IEmpleadoCursoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmpleadoCursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmpleadoCurso>> CursosEmpleadoByIdEmpleado(int idEmpleado)
        {
            List<EmpleadoCurso> data = new List<EmpleadoCurso>();

            var sql = "sp_cursosDelEmpleadoByIdEmpleado";

            DbConnection connection = _dbContext.Database.GetDbConnection();

            DbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            #region "Parameters"
            var p_id = command.CreateParameter();
            p_id.ParameterName = "@idEmpleado";
            p_id.Value = idEmpleado;
            command.Parameters.Add(p_id);
            #endregion

            await connection.OpenAsync();

            using IDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var item = new EmpleadoCurso()
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_empleado_curso")) ? reader.GetInt32(reader.GetOrdinal("id_empleado_curso")) : 0,
                    IdEmpleado = !reader.IsDBNull(reader.GetOrdinal("id_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_empleado")) : 0,
                    IdCurso = !reader.IsDBNull(reader.GetOrdinal("id_curso")) ? reader.GetInt32(reader.GetOrdinal("id_curso")) : 0,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    Curso = new Curso
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_curso")) ? reader.GetInt32(reader.GetOrdinal("id_curso")) : 0,
                        Codigo = !reader.IsDBNull(reader.GetOrdinal("codigo")) ? reader.GetString(reader.GetOrdinal("codigo")) : "",
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                        IdTipoCurso = !reader.IsDBNull(reader.GetOrdinal("id_tipo_curso")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_curso")) : 0,
                        Gerencia = !reader.IsDBNull(reader.GetOrdinal("gerencia")) ? reader.GetString(reader.GetOrdinal("gerencia")) : null,
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        TipoCurso = new TipoCurso
                        {
                            Id = !reader.IsDBNull(reader.GetOrdinal("id_tipo_curso")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_curso")) : 0,
                            Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion_tipo_curso")) ? reader.GetString(reader.GetOrdinal("descripcion_tipo_curso")) : "",
                            CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                            UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                            State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        }
                    }
                };

                data.Add(item);
            }

            await connection.CloseAsync();
            return data;
        }
    }
}
