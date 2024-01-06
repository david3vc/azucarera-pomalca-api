using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.CondicionTrabajoPuestos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CondicionTrabajoPuestoService : ICondicionTrabajoPuestoService
    {
        private readonly ICondicionTrabajoPuestoRepository _condicionTrabajoPuestoRepository;
        private readonly IMapper _mapper;

        public CondicionTrabajoPuestoService(ICondicionTrabajoPuestoRepository condicionTrabajoPuestoRepository, IMapper mapper)
        {
            _condicionTrabajoPuestoRepository = condicionTrabajoPuestoRepository;
            _mapper = mapper;
        }

        public async Task<CondicionTrabajoPuestoDto> CreateAsync(CondicionTrabajoPuestoSaveDto saveDto)
        {
            CondicionTrabajoPuesto condicionTrabajoPuesto = _mapper.Map<CondicionTrabajoPuesto>(saveDto);
            condicionTrabajoPuesto.CreatedAt = DateTime.UtcNow;
            condicionTrabajoPuesto.State = true;

            await _condicionTrabajoPuestoRepository.SaveAsync(condicionTrabajoPuesto);

            return _mapper.Map<CondicionTrabajoPuestoDto>(condicionTrabajoPuesto);
        }

        public async Task<CondicionTrabajoPuestoDto> DisabledAsync(int id)
        {
            CondicionTrabajoPuesto? condicionTrabajoPuesto = await _condicionTrabajoPuestoRepository.FindByIdAsync(id);

            if (condicionTrabajoPuesto is null) throw CondicionTrabajoPuestoNotFound(id);

            condicionTrabajoPuesto.State = !condicionTrabajoPuesto.State;

            await _condicionTrabajoPuestoRepository.SaveAsync(condicionTrabajoPuesto);

            return _mapper.Map<CondicionTrabajoPuestoDto>(condicionTrabajoPuesto);
        }

        public async Task<CondicionTrabajoPuestoDto> EditAsync(int id, CondicionTrabajoPuestoSaveDto saveDto)
        {
            CondicionTrabajoPuesto? condicionTrabajoPuesto = await _condicionTrabajoPuestoRepository.FindByIdAsync(id);

            if (condicionTrabajoPuesto is null) throw CondicionTrabajoPuestoNotFound(id);

            _mapper.Map<CondicionTrabajoPuestoSaveDto, CondicionTrabajoPuesto>(saveDto, condicionTrabajoPuesto);

            condicionTrabajoPuesto.UpdatedAt = DateTime.UtcNow;

            await _condicionTrabajoPuestoRepository.SaveAsync(condicionTrabajoPuesto);

            return _mapper.Map<CondicionTrabajoPuestoDto>(condicionTrabajoPuesto);
        }

        public Task<IReadOnlyList<CondicionTrabajoPuestoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CondicionTrabajoPuestoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CondicionTrabajoPuestoDto>> GetCondicionTrabajoPuestosByIdPuesto(int idPuesto)
        {
            List<CondicionTrabajoPuesto> condicionTrabajoPuestos = await _condicionTrabajoPuestoRepository.GetCondicionTrabajoPuestosByIdPuesto(idPuesto);

            if (condicionTrabajoPuestos is null) throw CondicionTrabajoPuestoNotFound(idPuesto);

            return _mapper.Map<List<CondicionTrabajoPuestoDto>>(condicionTrabajoPuestos);
        }

        private NotFoundCoreException CondicionTrabajoPuestoNotFound(int id)
        {
            return new NotFoundCoreException("Condicion Trabajo Puesto no encontrado para el id: " + id);
        }
    }
}
