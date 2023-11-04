using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Secciones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

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

        private NotFoundCoreException SeccionNotFound(int id)
        {
            return new NotFoundCoreException("Seccion no encontrado para el id: " + id);
        }
    }
}
