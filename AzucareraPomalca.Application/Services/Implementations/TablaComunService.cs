using AutoMapper;
using AzucareraPomalca.Application.Dtos.TablaComunes;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TablaComunService : ITablaComunService
    {
        private readonly IMapper _mapper;
        private readonly ITablaComunRepository _tablaComunRepository;

        public TablaComunService(IMapper mapper, ITablaComunRepository tablaComunRepository)
        {
            _mapper = mapper;
            _tablaComunRepository = tablaComunRepository;
        }

        public async Task<IReadOnlyList<TablaComunDto>> FindAllByIdsAsync(TablaComunFilterDto filter)
        {
            Expression<Func<TablaComun, bool>> predicate = x =>
                (!filter.IdTabla.HasValue || x.IdTabla == filter.IdTabla)
                && (string.IsNullOrWhiteSpace(filter.Codigo) || x.Codigo.ToUpper().Contains(filter.Codigo.ToUpper()))
                && (x.IdFila != 0);

            IReadOnlyList<TablaComun> response = await _tablaComunRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<TablaComunDto>>(response);
        }
    }
}
