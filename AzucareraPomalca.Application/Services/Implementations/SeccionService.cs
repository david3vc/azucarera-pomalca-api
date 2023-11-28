using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Secciones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class SeccionService : ISeccionService
    {
        private readonly ISeccionRepository _seccionRepository;
        private readonly IMapper _mapper;

        public SeccionService(ISeccionRepository seccionRepository, IMapper mapper)
        {
            _seccionRepository = seccionRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<SeccionDto>> FindAllAsync()
        {
            IReadOnlyList<Seccion> secciones = await _seccionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<SeccionDto>>(secciones);
        }

        public async Task<SeccionDto> FindByIdAsync(int id)
        {
            Seccion? seccion = await _seccionRepository.FindByIdAsync(id);

            if (seccion is null) throw SeccionNotFound(id);

            return _mapper.Map<SeccionDto>(seccion);
        }

        public async Task<IReadOnlyList<SeccionSimpleDto>> SimpleListAsync()
        {
            IReadOnlyList<Seccion> secciones = await _seccionRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<SeccionSimpleDto>>(secciones);
        }

        public async Task<IReadOnlyList<SeccionSimpleDto>> SimpleListByIdsAsync(SeccionSimpleFilterDto request)
        {
            Expression<Func<Seccion, bool>>? predicate = x => (!request.IdGerencia.HasValue || x.IdGerencia == request.IdGerencia)
                                                               && (!request.IdDivision.HasValue || x.IdDivision == request.IdDivision)
                                                               && (!request.IdDepartamento.HasValue || x.IdDepartamento == request.IdDepartamento);

            IReadOnlyList<Seccion> claseOcupacionales = await _seccionRepository.FindAllAsync(predicate: predicate);

            return _mapper.Map<IReadOnlyList<SeccionSimpleDto>>(claseOcupacionales);
        }

        private NotFoundCoreException SeccionNotFound(int id)
        {
            return new NotFoundCoreException("Seccion no encontrado para el id: " + id);
        }
    }
}
