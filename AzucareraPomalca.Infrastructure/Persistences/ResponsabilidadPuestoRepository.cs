using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class ResponsabilidadPuestoRepository : CrudRepository<ResponsabilidadPuesto, int>, IResponsabilidadPuestoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ResponsabilidadPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ResponsabilidadPuesto>> GetResponsabilidadPuestosByIdPuesto(int idPuesto)
        {
            List<ResponsabilidadPuesto> data = null;

            var sql = "sp_responsabilidadesDelPuestoByIdPuesto";

            DbConnection connection = _dbContext.Database.GetDbConnection();

            DbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            #region "Parameters"
            var p_id = command.CreateParameter();
            p_id.ParameterName = "@idPuesto";
            p_id.Value = idPuesto;
            command.Parameters.Add(p_id);
            #endregion

            await connection.OpenAsync();

            using IDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var item = new ResponsabilidadPuesto()
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_responsabilidad_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_responsabilidad_puesto")) : 0,
                    IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
                    IdResponsabilidad = !reader.IsDBNull(reader.GetOrdinal("id_responsabilidad")) ? reader.GetInt32(reader.GetOrdinal("id_responsabilidad")) : 0,
                    IdNivel = !reader.IsDBNull(reader.GetOrdinal("id_nivel")) ? reader.GetInt32(reader.GetOrdinal("id_nivel")) : null,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    Responsabilidad = new Responsabilidad
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_responsabilidad")) ? reader.GetInt32(reader.GetOrdinal("id_responsabilidad")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    },
                    Nivel = new Nivel
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_nivel")) ? reader.GetInt32(reader.GetOrdinal("id_nivel")) : 0,
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
