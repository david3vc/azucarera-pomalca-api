using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Infrastructure.Cores.Contexts;
using AzucareraPomalca.Infrastructure.Cores.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace AzucareraPomalca.Infrastructure.Persistences
{
    public class CursoRepository : CrudRepository<Curso, int>, ICursoRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CursoRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<CursoDuroSugerido>> ListarCursosDurosSugeridosAsync(Paging pagin, CursoDuroSugerido request)
        {
            List<CursoDuroSugerido> data = new List<CursoDuroSugerido>();

            var sql = "sp_listarCursosDurosSugeridos";

            DbConnection connection = _dbContext.Database.GetDbConnection();

            DbCommand command = connection.CreateCommand();
            command.CommandText = sql;
            command.CommandType = CommandType.StoredProcedure;

            #region "Parameters"
            var p_codigo = command.CreateParameter();
            p_codigo.ParameterName = "@codigo";
            p_codigo.Value = request.Codigo;
            command.Parameters.Add(p_codigo);

            var p_curso = command.CreateParameter();
            p_curso.ParameterName = "@curso";
            p_curso.Value = request.Curso;
            command.Parameters.Add(p_curso);

            var p_id_tipo = command.CreateParameter();
            p_id_tipo.ParameterName = "@id_tipo";
            p_id_tipo.Value = request.IdTipoCurso;
            command.Parameters.Add(p_id_tipo);

            var p_gerencia = command.CreateParameter();
            p_gerencia.ParameterName = "@gerencia";
            p_gerencia.Value = request.Gerencia;
            command.Parameters.Add(p_gerencia);
            #endregion

            await connection.OpenAsync();

            using IDataReader reader = await command.ExecuteReaderAsync();

            while (reader.Read())
            {
                var item = new CursoDuroSugerido()
                {
                    IdCurso = !reader.IsDBNull(reader.GetOrdinal("id_curso")) ? reader.GetInt32(reader.GetOrdinal("id_curso")) : null,
                    Codigo = !reader.IsDBNull(reader.GetOrdinal("codigo")) ? reader.GetString(reader.GetOrdinal("codigo")) : null,
                    Curso = !reader.IsDBNull(reader.GetOrdinal("curso")) ? reader.GetString(reader.GetOrdinal("curso")) : null,
                    TipoCurso = !reader.IsDBNull(reader.GetOrdinal("tipo")) ? reader.GetString(reader.GetOrdinal("tipo")) : null,
                    Gerencia = !reader.IsDBNull(reader.GetOrdinal("gerencia")) ? reader.GetString(reader.GetOrdinal("gerencia")) : null,
                    EmpleadosNoCapacitados = !reader.IsDBNull(reader.GetOrdinal("empleados_no_capacitados")) ? reader.GetInt32(reader.GetOrdinal("empleados_no_capacitados")) : null
                };

                data.Add(item);
            }

            await connection.CloseAsync();

            return GetPagedResultCursoDuroSugerido(data, pagin);
        }

        private PagedResult<CursoDuroSugerido> GetPagedResultCursoDuroSugerido(List<CursoDuroSugerido> data, Paging pagin)
        {
            var totalElements = data.Count;
            var skip = (pagin.PageNumber - 1) * pagin.PageSize;
            var paginatedItems = data.Skip(skip)
                .Take(pagin.PageSize)
                .ToList();

            return new PagedResult<CursoDuroSugerido>(paginatedItems, pagin, totalElements);
        }
    }
}
