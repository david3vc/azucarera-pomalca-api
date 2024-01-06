using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class PuestoCursoRepository : CrudRepository<PuestoCurso, int>, IPuestoCursoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PuestoCursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<PuestoCurso>> GetPuestoCursosByIdPuesto(int idPuesto)
        {
            List<PuestoCurso> data = new List<PuestoCurso>();

            var sql = "sp_cursosDelPuestoByIdPuesto";

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
                var item = new PuestoCurso
                {
                    Id = !reader.IsDBNull(reader.GetOrdinal("id_puesto_curso")) ? reader.GetInt32(reader.GetOrdinal("id_puesto_curso")) : 0,
                    IdPuesto = !reader.IsDBNull(reader.GetOrdinal("id_puesto")) ? reader.GetInt32(reader.GetOrdinal("id_puesto")) : 0,
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
