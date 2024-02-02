using AutoMapper;
using AzucareraPomalca.Application.Dtos.CondicionEmpleados;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class CondicionEmpleadoService : ICondicionEmpleadoService
    {
        private readonly ICondicionEmpleadoRepository _condicionEmpleadoRepository;
        private readonly IMapper _mapper;

        public CondicionEmpleadoService(ICondicionEmpleadoRepository condicionEmpleadoRepository, IMapper mapper)
        {
            _condicionEmpleadoRepository = condicionEmpleadoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<CondicionEmpleadoDto>> SimpleListAsync()
        {
            IReadOnlyList<CondicionEmpleado> condicionEmpleados = await _condicionEmpleadoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<CondicionEmpleadoDto>>(condicionEmpleados);
        }
    }
}
