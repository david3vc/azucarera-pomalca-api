using AutoMapper;
using AzucareraPomalca.Application.Dtos.CondicionTrabajos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CondicionTrabajoService : ICondicionTrabajoService
    {
        private readonly ICondicionTrabajoRepository _condicionTrabajoRepository;
        private readonly IMapper _mapper;

        public CondicionTrabajoService(ICondicionTrabajoRepository condicionTrabajoRepository, IMapper mapper)
        {
            _condicionTrabajoRepository = condicionTrabajoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CondicionTrabajoDto>> SimpleListAsync()
        {
            List<Expression<Func<CondicionTrabajo, object>>>? includes = new List<Expression<Func<CondicionTrabajo, object>>>()
            {
                t => t.TipoCondicionTrabajo
            };

            IReadOnlyList<CondicionTrabajo> condicionTrabajos = await _condicionTrabajoRepository.FindAllAsync(includes: includes);

            return _mapper.Map<IReadOnlyList<CondicionTrabajoDto>>(condicionTrabajos);
        }
    }
}
