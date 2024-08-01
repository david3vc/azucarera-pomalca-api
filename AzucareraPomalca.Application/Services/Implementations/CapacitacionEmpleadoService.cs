using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CapacitacionEmpleadoService : ICapacitacionEmpleadoService
    {
        private readonly ICapacitacionEmpleadoRepository _capacitacionEmpleadoRepository;
        private readonly IMapper _mapper;

        public CapacitacionEmpleadoService(ICapacitacionEmpleadoRepository capacitacionEmpleadoRepository, IMapper mapper)
        {
            _capacitacionEmpleadoRepository = capacitacionEmpleadoRepository;
            _mapper = mapper;
        }

        public Task<IReadOnlyList<CapacitacionEmpleadoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CapacitacionEmpleadoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CapacitacionEmpleadoDto> CreateAsync(CapacitacionEmpleadoSaveDto saveDto)
        {
            CapacitacionEmpleado capacitacionEmpleado = _mapper.Map<CapacitacionEmpleado>(saveDto);
            capacitacionEmpleado.CreatedAt = DateTime.UtcNow;
            capacitacionEmpleado.State = true;

            await _capacitacionEmpleadoRepository.SaveAsync(capacitacionEmpleado);

            return _mapper.Map<CapacitacionEmpleadoDto>(capacitacionEmpleado);
        }

        public async Task<CapacitacionEmpleadoDto> EditAsync(int id, CapacitacionEmpleadoSaveDto saveDto)
        {
            CapacitacionEmpleado? capacitacionEmpleado = await _capacitacionEmpleadoRepository.FindByIdAsync(id);

            if (capacitacionEmpleado is null) throw CapacitacionEmpleadoNotFound(id);

            _mapper.Map<CapacitacionEmpleadoSaveDto, CapacitacionEmpleado>(saveDto, capacitacionEmpleado);

            capacitacionEmpleado.UpdatedAt = DateTime.UtcNow;

            await _capacitacionEmpleadoRepository.SaveAsync(capacitacionEmpleado);

            return _mapper.Map<CapacitacionEmpleadoDto>(capacitacionEmpleado);
        }

        public Task<CapacitacionEmpleadoDto> DisabledAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException CapacitacionEmpleadoNotFound(int id)
        {
            return new NotFoundCoreException("CapacitacionEmpleado no encontrado para el id: " + id);
        }
    }
}
