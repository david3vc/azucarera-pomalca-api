using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class TomaDecisionPuestoRepository : CrudRepository<TomaDecisionPuesto, int>, ITomaDecisionPuestoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TomaDecisionPuestoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TomaDecisionPuesto>> GetTomaDecisionPuestosByIdPuesto(int idPuesto)
        {
            List<TomaDecisionPuesto> data = new List<TomaDecisionPuesto>();

            var sql = "sp_tomaDecisionDelPuestoByIdPuesto";

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
                var item = new TomaDecisionPuesto
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_toma_decision_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_toma_decision_puesto")) : 0,
                    IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
                    IdNivel = !reader.IsDBNull(reader.GetOrdinal("id_nivel")) ? reader.GetInt32(reader.GetOrdinal("id_nivel")) : 0,
                    IdTomaDecision = !reader.IsDBNull(reader.GetOrdinal("id_toma_decision")) ? reader.GetInt32(reader.GetOrdinal("id_toma_decision")) : 0,
                    CreatedAt = !reader.IsDBNull(reader.GetOrdinal("created_at")) ? reader.GetDateTime(reader.GetOrdinal("created_at")) : DateTime.UtcNow,
                    UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("updated_at")) ? reader.GetDateTime(reader.GetOrdinal("updated_at")) : null,
                    State = !reader.IsDBNull(reader.GetOrdinal("state")) ? reader.GetBoolean(reader.GetOrdinal("state")) : false,
                    TomaDecision = new TomaDecision
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("td_id_toma_decision")) ? reader.GetInt32(reader.GetOrdinal("td_id_toma_decision")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("td_descripcion")) ? reader.GetString(reader.GetOrdinal("td_descripcion")) : "",
                        IdTipoTomaDecision = !reader.IsDBNull(reader.GetOrdinal("td_id_tipo_toma_decision")) ? reader.GetInt32(reader.GetOrdinal("td_id_tipo_toma_decision")) : 0,
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("td_created_at")) ? reader.GetDateTime(reader.GetOrdinal("td_created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("td_updated_at")) ? reader.GetDateTime(reader.GetOrdinal("td_updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("td_state")) ? reader.GetBoolean(reader.GetOrdinal("td_state")) : false,
                        TipoTomaDecision = new TipoTomaDecision
                        {
                            Id = !reader.IsDBNull(reader.GetOrdinal("ttd_id_tipo_toma_decision")) ? reader.GetInt32(reader.GetOrdinal("ttd_id_tipo_toma_decision")) : 0,
                            Descripcion = !reader.IsDBNull(reader.GetOrdinal("ttd_descripcion")) ? reader.GetString(reader.GetOrdinal("ttd_descripcion")) : "",
                            CreatedAt = !reader.IsDBNull(reader.GetOrdinal("ttd_created_at")) ? reader.GetDateTime(reader.GetOrdinal("ttd_created_at")) : DateTime.UtcNow,
                            UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("ttd_updated_at")) ? reader.GetDateTime(reader.GetOrdinal("ttd_updated_at")) : null,
                            State = !reader.IsDBNull(reader.GetOrdinal("ttd_state")) ? reader.GetBoolean(reader.GetOrdinal("ttd_state")) : false,
                        }
                    },
                    Nivel = new Nivel
                    {
                        Id = !reader.IsDBNull(reader.GetOrdinal("n_id_nivel")) ? reader.GetInt32(reader.GetOrdinal("n_id_nivel")) : 0,
                        Descripcion = !reader.IsDBNull(reader.GetOrdinal("n_descripcion")) ? reader.GetString(reader.GetOrdinal("n_descripcion")) : "",
                        CreatedAt = !reader.IsDBNull(reader.GetOrdinal("n_created_at")) ? reader.GetDateTime(reader.GetOrdinal("n_created_at")) : DateTime.UtcNow,
                        UpdatedAt = !reader.IsDBNull(reader.GetOrdinal("n_updated_at")) ? reader.GetDateTime(reader.GetOrdinal("n_updated_at")) : null,
                        State = !reader.IsDBNull(reader.GetOrdinal("n_state")) ? reader.GetBoolean(reader.GetOrdinal("n_state")) : false,
                    }
                };

                data.Add(item);
            }

            await connection.CloseAsync();
            return data;
        }
    }
}
