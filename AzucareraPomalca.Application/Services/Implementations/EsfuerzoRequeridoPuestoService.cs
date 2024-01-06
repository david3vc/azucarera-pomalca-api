using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.EsfuerzoRequeridoPuestos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EsfuerzoRequeridoPuestoService : IEsfuerzoRequeridoPuestoService
    {
        private readonly IEsfuerzoRequeridoPuestoRepository _esfuerzoRequeridoPuestoRepository;
        private readonly IMapper _mapper;

        public EsfuerzoRequeridoPuestoService(IEsfuerzoRequeridoPuestoRepository esfuerzoRequeridoPuestoRepository, IMapper mapper)
        {
            _esfuerzoRequeridoPuestoRepository = esfuerzoRequeridoPuestoRepository;
            _mapper = mapper;
        }

        public async Task<EsfuerzoRequeridoPuestoDto> CreateAsync(EsfuerzoRequeridoPuestoSaveDto saveDto)
        {
            EsfuerzoRequeridoPuesto esfuerzoRequeridoPuesto = _mapper.Map<EsfuerzoRequeridoPuesto>(saveDto);
            esfuerzoRequeridoPuesto.CreatedAt = DateTime.UtcNow;
            esfuerzoRequeridoPuesto.State = true;

            await _esfuerzoRequeridoPuestoRepository.SaveAsync(esfuerzoRequeridoPuesto);

            return _mapper.Map<EsfuerzoRequeridoPuestoDto>(esfuerzoRequeridoPuesto);
        }

        public Task<EsfuerzoRequeridoPuestoDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<EsfuerzoRequeridoPuestoDto> EditAsync(int id, EsfuerzoRequeridoPuestoSaveDto saveDto)
        {
            EsfuerzoRequeridoPuesto? esfuerzoRequeridoPuesto = await _esfuerzoRequeridoPuestoRepository.FindByIdAsync(id);

            if (esfuerzoRequeridoPuesto is null) throw EsfuerzoRequeridoPuestoNotFound(id);

            _mapper.Map<EsfuerzoRequeridoPuestoSaveDto, EsfuerzoRequeridoPuesto>(saveDto, esfuerzoRequeridoPuesto);

            esfuerzoRequeridoPuesto.UpdatedAt = DateTime.UtcNow;

            await _esfuerzoRequeridoPuestoRepository.SaveAsync(esfuerzoRequeridoPuesto);

            return _mapper.Map<EsfuerzoRequeridoPuestoDto>(esfuerzoRequeridoPuesto);
        }

        public Task<IReadOnlyList<EsfuerzoRequeridoPuestoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<EsfuerzoRequeridoPuestoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EsfuerzoRequeridoPuestoDto>> GetEsfuerzoRequeridoPuestosByIdPuesto(int idPuesto)
        {
            List<EsfuerzoRequeridoPuesto> esfuerzoRequeridoPuestos = await _esfuerzoRequeridoPuestoRepository.GetEsfuerzoRequeridoPuestosByIdPuesto(idPuesto);

            if (esfuerzoRequeridoPuestos is null) throw EsfuerzoRequeridoPuestoNotFound(idPuesto);

            return _mapper.Map<List<EsfuerzoRequeridoPuestoDto>>(esfuerzoRequeridoPuestos);
        }

        private NotFoundCoreException EsfuerzoRequeridoPuestoNotFound(int id)
        {
            return new NotFoundCoreException("Toma Decision Puesto Puesto no encontrado para el id: " + id);
        }
    }
}
