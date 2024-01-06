using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.TomaDecisionPuestos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class TomaDecisionPuestoService : ITomaDecisionPuestoService
    {
        private readonly ITomaDecisionPuestoRepository _tomaDecisionPuestoRepository;
        private readonly IMapper _mapper;

        public TomaDecisionPuestoService(ITomaDecisionPuestoRepository tomaDecisionPuestoRepository, IMapper mapper)
        {
            _tomaDecisionPuestoRepository = tomaDecisionPuestoRepository;
            _mapper = mapper;
        }

        public async Task<TomaDecisionPuestoDto> CreateAsync(TomaDecisionPuestoSaveDto saveDto)
        {
            TomaDecisionPuesto tomaDecisionPuesto = _mapper.Map<TomaDecisionPuesto>(saveDto);
            tomaDecisionPuesto.CreatedAt = DateTime.UtcNow;
            tomaDecisionPuesto.State = true;

            await _tomaDecisionPuestoRepository.SaveAsync(tomaDecisionPuesto);

            return _mapper.Map<TomaDecisionPuestoDto>(tomaDecisionPuesto);
        }

        public Task<TomaDecisionPuestoDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<TomaDecisionPuestoDto> EditAsync(int id, TomaDecisionPuestoSaveDto saveDto)
        {
            TomaDecisionPuesto? tomaDecisionPuesto = await _tomaDecisionPuestoRepository.FindByIdAsync(id);

            if (tomaDecisionPuesto is null) throw TomaDecisionPuestoNotFound(id);

            _mapper.Map<TomaDecisionPuestoSaveDto, TomaDecisionPuesto>(saveDto, tomaDecisionPuesto);

            tomaDecisionPuesto.UpdatedAt = DateTime.UtcNow;

            await _tomaDecisionPuestoRepository.SaveAsync(tomaDecisionPuesto);

            return _mapper.Map<TomaDecisionPuestoDto>(tomaDecisionPuesto);
        }

        public Task<IReadOnlyList<TomaDecisionPuestoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TomaDecisionPuestoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TomaDecisionPuestoDto>> GetTomaDecisionPuestosByIdPuesto(int idPuesto)
        {
            List<TomaDecisionPuesto> tomaDecisionPuestos = await _tomaDecisionPuestoRepository.GetTomaDecisionPuestosByIdPuesto(idPuesto);

            if (tomaDecisionPuestos is null) throw TomaDecisionPuestoNotFound(idPuesto);

            return _mapper.Map<List<TomaDecisionPuestoDto>>(tomaDecisionPuestos);
        }

        private NotFoundCoreException TomaDecisionPuestoNotFound(int id)
        {
            return new NotFoundCoreException("Toma Decision Puesto Puesto no encontrado para el id: " + id);
        }
    }
}
