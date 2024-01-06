using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PuestoCursoService : IPuestoCursoService
    {
        private readonly IPuestoCursoRepository _puestoCursoRepository;
        private readonly IMapper _mapper;

        public PuestoCursoService(IPuestoCursoRepository puestoCursoRepository, IMapper mapper)
        {
            _puestoCursoRepository = puestoCursoRepository;
            _mapper = mapper;
        }

        public async Task<PuestoCursoDto> CreateAsync(PuestoCursoSaveDto saveDto)
        {
            PuestoCurso puestoCurso = _mapper.Map<PuestoCurso>(saveDto);
            puestoCurso.CreatedAt = DateTime.UtcNow;
            puestoCurso.State = true;

            await _puestoCursoRepository.SaveAsync(puestoCurso);

            return _mapper.Map<PuestoCursoDto>(puestoCurso);
        }

        public async Task<PuestoCursoDto> DisabledAsync(int id)
        {
            PuestoCurso? puestoCurso = await _puestoCursoRepository.FindByIdAsync(id);

            if (puestoCurso is null) throw PuestoCursoNotFound(id);

            puestoCurso.State = !puestoCurso.State;

            await _puestoCursoRepository.SaveAsync(puestoCurso);

            return _mapper.Map<PuestoCursoDto>(puestoCurso);
        }

        public async Task<PuestoCursoDto> EditAsync(int id, PuestoCursoSaveDto saveDto)
        {
            PuestoCurso? puestoCurso = await _puestoCursoRepository.FindByIdAsync(id);

            if (puestoCurso is null) throw PuestoCursoNotFound(id);

            _mapper.Map<PuestoCursoSaveDto, PuestoCurso>(saveDto, puestoCurso);

            puestoCurso.UpdatedAt = DateTime.UtcNow;

            await _puestoCursoRepository.SaveAsync(puestoCurso);

            return _mapper.Map<PuestoCursoDto>(puestoCurso);
        }

        public Task<IReadOnlyList<PuestoCursoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PuestoCursoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PuestoCursoDto>> GetPuestoCursosByIdPuesto(int idPuesto)
        {
            List<PuestoCurso> puestoCursos = await _puestoCursoRepository.GetPuestoCursosByIdPuesto(idPuesto);

            if (puestoCursos is null) throw PuestoCursoNotFound(idPuesto);

            return _mapper.Map<List<PuestoCursoDto>>(puestoCursos);
        }

        private NotFoundCoreException PuestoCursoNotFound(int id)
        {
            return new NotFoundCoreException("Puesto curso no encontrado para el id: " + id);
        }
    }
}
