using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CondicionTrabajoPuestoRepository : CrudRepository<CondicionTrabajoPuesto, int>, ICondicionTrabajoPuestoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CondicionTrabajoPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<CondicionTrabajoPuesto>> GetCondicionTrabajoPuestosByIdPuesto(int idPuesto)
        {
            List<CondicionTrabajoPuesto> data = new List<CondicionTrabajoPuesto>();

            var sql = "sp_condicionesTrabajoDelPuestoByIdPuesto";

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
                var item = new CondicionTrabajoPuesto
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_condicion_trabajo_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_condicion_trabajo_puesto")) : 0,
                    IsMarked = !reader.IsDBNull(reader.GetOrdinal("is_marked")) ? reader.GetBoolean(reader.GetOrdinal("is_marked")) : false,
                    IdCondicionTrabajo = !reader.IsDBNull(reader.GetOrdinal("id_condicion_trabajo")) ? reader.GetInt32(reader.GetOrdinal("id_condicion_trabajo")) : 0,
                    IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    CondicionTrabajo = new CondicionTrabajo
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_condicion_trabajo")) ? reader.GetInt32(reader.GetOrdinal("id_condicion_trabajo")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                        IdTipoCondicionTrabajo = !reader.IsDBNull(reader.GetOrdinal("id_tipo_condicion_trabajo")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_condicion_trabajo")) : 0,
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        TipoCondicionTrabajo = new TipoCondicionTrabajo
                        {
                            Id = !reader.IsDBNull(reader.GetOrdinal("id_tipo_condicion_trabajo")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_condicion_trabajo")) : 0,
                            Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion_tipo_condicion_trabajo")) ? reader.GetString(reader.GetOrdinal("descripcion_tipo_condicion_trabajo")) : "",
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
