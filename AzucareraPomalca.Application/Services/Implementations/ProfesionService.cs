using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Profesiones;
using AzucareraPomalca.Core.Paginations;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ProfesionService : IProfesionService
    {
        private readonly IProfesionRepository _profesionRepository;
        private readonly IMapper _mapper;

        public ProfesionService(IProfesionRepository profesionRepository, IMapper mapper)
        {
            _profesionRepository = profesionRepository;
            _mapper = mapper;
        }

        public async Task<ProfesionDto> CreateAsync(ProfesionSaveDto saveDto)
        {
            Profesion profesion = _mapper.Map<Profesion>(saveDto);
            profesion.CreatedAt = DateTime.UtcNow;
            profesion.State = true;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<ProfesionDto> DisabledAsync(int id)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            profesion.State = !profesion.State;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<ProfesionDto> EditAsync(int id, ProfesionSaveDto saveDto)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            _mapper.Map<ProfesionSaveDto, Profesion>(saveDto, profesion);

            profesion.UpdatedAt = DateTime.UtcNow;

            await _profesionRepository.SaveAsync(profesion);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<IReadOnlyList<ProfesionDto>> FindAllAsync()
        {
            IReadOnlyList<Profesion> profesiones = await _profesionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<ProfesionDto>>(profesiones);
        }

        public async Task<ProfesionDto> FindByIdAsync(int id)
        {
            Profesion? profesion = await _profesionRepository.FindByIdAsync(id);

            if (profesion is null) throw ProfesionNotFound(id);

            return _mapper.Map<ProfesionDto>(profesion);
        }

        public async Task<ResponsePagination<ProfesionDto>> PaginatedSearch(RequestPagination<ProfesionFilterDto> request)
        {
            var entity = _mapper.Map<RequestPagination<Profesion>>(request);
            var response = await _profesionRepository.PaginatedSearch(entity);

            return _mapper.Map<ResponsePagination<ProfesionDto>>(response);
        }

        private NotFoundCoreException ProfesionNotFound(int id)
        {
            return new NotFoundCoreException("Profesion no encontrada para el id: " + id);
        }
    }
}
