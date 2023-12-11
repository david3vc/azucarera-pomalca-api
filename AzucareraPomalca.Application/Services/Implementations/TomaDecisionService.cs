using AutoMapper;
using AzucareraPomalca.Application.Dtos.TomaDecisiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TomaDecisionService : ITomaDecisionService
    {
        private readonly ITomaDecisionRepository _tomaDecisionRepository;
        private readonly IMapper _mapper;

        public TomaDecisionService(ITomaDecisionRepository tomaDecisionRepository, IMapper mapper)
        {
            _tomaDecisionRepository = tomaDecisionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<TomaDecisionDto>> SimpleListAsync()
        {
            List<Expression<Func<TomaDecision, object>>>? includes = new List<Expression<Func<TomaDecision, object>>>()
            {
                t => t.TipoTomaDecision
            };

            IReadOnlyList<TomaDecision> tomaDecisiones = await _tomaDecisionRepository.FindAllAsync(includes: includes);

            return _mapper.Map<IReadOnlyList<TomaDecisionDto>>(tomaDecisiones);
        }
    }
}
