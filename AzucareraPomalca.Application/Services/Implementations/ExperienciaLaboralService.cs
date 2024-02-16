using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.ExperienciaLaborales;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ExperienciaLaboralService : IExperienciaLaboralService
    {
        private readonly IExperienciaLaboralRepository _experienciaLaboralRepository;
        private readonly IMapper _mapper;

        public ExperienciaLaboralService(IMapper mapper, IExperienciaLaboralRepository experienciaLaboralRepository)
        {
            _mapper = mapper;
            _experienciaLaboralRepository = experienciaLaboralRepository;
        }

        public async Task<ExperienciaLaboralDto> CreateAsync(ExperienciaLaboralSaveDto saveDto)
        {
            ExperienciaLaboral experienciaLaboral = _mapper.Map<ExperienciaLaboral>(saveDto);
            experienciaLaboral.CreatedAt = DateTime.UtcNow;
            experienciaLaboral.State = true;

            await _experienciaLaboralRepository.SaveAsync(experienciaLaboral);

            return _mapper.Map<ExperienciaLaboralDto>(experienciaLaboral);
        }

        public async Task<ExperienciaLaboralDto> DisabledAsync(int id)
        {
            ExperienciaLaboral? experienciaLaboral = await _experienciaLaboralRepository.FindByIdAsync(id);

            if (experienciaLaboral is null) throw ExperienciaLaboralNotFound(id);

            experienciaLaboral.State = false;

            await _experienciaLaboralRepository.SaveAsync(experienciaLaboral);

            return _mapper.Map<ExperienciaLaboralDto>(experienciaLaboral);
        }

        public async Task<ExperienciaLaboralDto> EditAsync(int id, ExperienciaLaboralSaveDto saveDto)
        {
            ExperienciaLaboral? experienciaLaboral = await _experienciaLaboralRepository.FindByIdAsync(id);

            if (experienciaLaboral is null) throw ExperienciaLaboralNotFound(id);

            _mapper.Map<ExperienciaLaboralSaveDto, ExperienciaLaboral>(saveDto, experienciaLaboral);

            experienciaLaboral.UpdatedAt = DateTime.UtcNow;

            await _experienciaLaboralRepository.SaveAsync(experienciaLaboral);

            return _mapper.Map<ExperienciaLaboralDto>(experienciaLaboral);
        }

        public Task<IReadOnlyList<ExperienciaLaboralDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ExperienciaLaboralDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException ExperienciaLaboralNotFound(int id)
        {
            return new NotFoundCoreException("Experiencia Laboral no encontrado para el id: " + id);
        }
    }
}
