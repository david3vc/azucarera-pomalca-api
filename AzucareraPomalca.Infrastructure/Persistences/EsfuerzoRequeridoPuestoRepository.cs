using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class EsfuerzoRequeridoPuestoRepository : CrudRepository<EsfuerzoRequeridoPuesto, int>, IEsfuerzoRequeridoPuestoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EsfuerzoRequeridoPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<EsfuerzoRequeridoPuesto>> GetEsfuerzoRequeridoPuestosByIdPuesto(int idPuesto)
        {
            List<EsfuerzoRequeridoPuesto> data = null;

            var sql = "sp_esfuerzoRqueridoDelPuestoByIdPuesto";

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
                var item = new EsfuerzoRequeridoPuesto
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_esfuerzo_requerido_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_esfuerzo_requerido_puesto")) : 0,
                    IdEsfuerzoRequerido = !reader.IsDBNull(reader.GetOrdinal("id_esfuerzo_requerido")) ? reader.GetInt32(reader.GetOrdinal("id_esfuerzo_requerido")) : 0,
                    IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
                    IdNivel = !reader.IsDBNull(reader.GetOrdinal("id_nivel")) ? reader.GetInt32(reader.GetOrdinal("id_nivel")) : 0,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    EsfuerzoRequerido = new EsfuerzoRequerido
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("id_esfuerzo_requerido")) ? reader.GetInt32(reader.GetOrdinal("id_esfuerzo_requerido")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                        IdTipoEsfuerzoRequerido = !reader.IsDBNull(reader.GetOrdinal("id_tipo_esfuerzo_requerido")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_esfuerzo_requerido")) : 0,
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        TipoEsfuerzoRequerido = new TipoEsfuerzoRequerido
                        {
                            Id = !reader.IsDBNull(reader.GetOrdinal("id_tipo_esfuerzo_requerido")) ? reader.GetInt32(reader.GetOrdinal("id_tipo_esfuerzo_requerido")) : 0,
                            Descripcion = !reader.IsDBNull(reader.GetOrdinal("descripcion")) ? reader.GetString(reader.GetOrdinal("descripcion")) : "",
                            CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                            UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                            State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                        }
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
