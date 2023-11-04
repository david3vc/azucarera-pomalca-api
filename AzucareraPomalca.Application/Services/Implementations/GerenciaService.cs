using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GerenciaService : IGerenciaService
    {
        private readonly IGerenciaRepository _gerenciaRepository;
        private readonly IMapper _mapper;

        public GerenciaService(IGerenciaRepository gerenciaRepository, IMapper mapper)
        {
            _gerenciaRepository = gerenciaRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<GerenciaDto>> FindAllAsync()
        {
            IReadOnlyList<Gerencia> gerencias = await _gerenciaRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<GerenciaDto>>(gerencias);
        }

        public async Task<GerenciaDto> FindByIdAsync(int id)
        {
            Gerencia? gerencia = await _gerenciaRepository.FindByIdAsync(id);

            if (gerencia is null) throw GerenciaNotFound(id);

            return _mapper.Map<GerenciaDto>(gerencia);
        }

        private NotFoundCoreException GerenciaNotFound(int id)
        {
            return new NotFoundCoreException("Gerencia no encontrada para el id: " + id);
        }
    }
}
