using AutoMapper;
using AzucareraPomalca.Application.Dtos.Responsabilidades;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ResponsabilidadService : IResponsabilidadService
    {
        private readonly IResponsabilidadRepository _responsabilidadRepository;
        private readonly IMapper _mapper;

        public ResponsabilidadService(IResponsabilidadRepository responsabilidadRepository, IMapper mapper)
        {
            _responsabilidadRepository = responsabilidadRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<ResponsabilidadSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Responsabilidad> responsabilidades = await _responsabilidadRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ResponsabilidadSimpleDto>>(responsabilidades);
        }
    }
}
