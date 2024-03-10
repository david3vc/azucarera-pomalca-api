using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EmpleadoProfesionRepository : CrudRepository<EmpleadoProfesion, int>, IEmpleadoProfesionRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public EmpleadoProfesionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EmpleadoProfesion>> ProfesionesEmpleadoByIdEmpleado(int idEmpleado)
        {
            List<EmpleadoProfesion> data = new List<EmpleadoProfesion>();

            var sql = "sp_profesionesDelEmpleadoByIdEmpleado";

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
                var item = new EmpleadoProfesion()
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_empleado_profesion")) ? reader.GetInt32(reader.GetOrdinal("id_empleado_profesion")) : 0,
                    IdEmpleado = !reader.IsDBNull(reader.GetOrdinal("id_empleado")) ? reader.GetInt32(reader.GetOrdinal("id_empleado")) : 0,
                    IdProfesion = !reader.IsDBNull(reader.GetOrdinal("id_profesion")) ? reader.GetInt32(reader.GetOrdinal("id_profesion")) : 0,
                    IdGradoAcademico = !reader.IsDBNull(reader.GetOrdinal("id_grado_academico")) ? reader.GetInt32(reader.GetOrdinal("id_grado_academico")) : 0,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    Profesion = new Profesion
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_profesion")) ? reader.GetInt32(reader.GetOrdinal("id_profesion")) : 0,
                        Codigo = !reader.IsDBNull(reader.GetOrdinal("codigo")) ? reader.GetString(reader.GetOrdinal("codigo")) : "",
                        Nombre = !reader.IsDBNull(reader.GetOrdinal("nombre")) ? reader.GetString(reader.GetOrdinal("nombre")) : "",
                        IdTipoProfesion = !reader.IsDBNull(reader.GetOrdinal("id_tipo_profesion")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_profesion")) : 0,
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    },
                    GradoAcademico = new GradoAcademico
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_grado_academico")) ? reader.GetInt32(reader.GetOrdinal("id_grado_academico")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    }
                };

                data.Add(item);
            }

            await connection.CloseAsync();
            return data;
        }
    }
}
