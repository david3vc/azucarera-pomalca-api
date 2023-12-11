using AutoMapper;
using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EsfuerzoRequeridoService : IEsfuerzoRequeridoService
    {
        private readonly IEsfuerzoRequeridoRepository _esfuerzoRequeridoRepository;
        private readonly IMapper _mapper;

        public EsfuerzoRequeridoService(IEsfuerzoRequeridoRepository esfuerzoRequeridoRepository, IMapper mapper)
        {
            _esfuerzoRequeridoRepository = esfuerzoRequeridoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<EsfuerzoRequeridoDto>> SimpleListAsync()
        {
            List<Expression<Func<EsfuerzoRequerido, object>>>? includes = new List<Expression<Func<EsfuerzoRequerido, object>>>()
            {
                t => t.TipoEsfuerzoRequerido
            };

            IReadOnlyList<EsfuerzoRequerido> esfuerzoRequeridoes = await _esfuerzoRequeridoRepository.FindAllAsync(includes: includes);

            return _mapper.Map<IReadOnlyList<EsfuerzoRequeridoDto>>(esfuerzoRequeridoes);
        }
    }
}
