using AutoMapper;
using AzucareraPomalca.Application.Dtos.GradosAcademicos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GradoAcademicoService : IGradoAcademicoService
    {
        private readonly IGradoAcademicoRepository _gradoAcademicoRepository;
        private readonly IMapper _mapper;

        public GradoAcademicoService(IGradoAcademicoRepository gradoAcademicoRepository, IMapper mapper)
        {
            _gradoAcademicoRepository = gradoAcademicoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GradoAcademicoSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<GradoAcademico> gradoAcademicos = await _gradoAcademicoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GradoAcademicoSimpleDto>>(gradoAcademicos);
        }
    }
}
