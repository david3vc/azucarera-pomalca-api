using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly IMapper _mapper;

        public DepartamentoService(IDepartamentoRepository departamentoRepository, IMapper mapper)
        {
            _departamentoRepository = departamentoRepository;
            _mapper = mapper;
        }

        public async Task<IReadOnlyList<DepartamentoDto>> FindAllAsync()
        {
            IReadOnlyList<Departamento> departamentos = await _departamentoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<DepartamentoDto>>(departamentos);
        }

        public async Task<DepartamentoDto> FindByIdAsync(int id)
        {
            Departamento? departamento = await _departamentoRepository.FindByIdAsync(id);

            if (departamento is null) throw DepartamentoNotFound(id);

            return _mapper.Map<DepartamentoDto>(departamento);
        }

        private NotFoundCoreException DepartamentoNotFound(int id)
        {
            return new NotFoundCoreException("Departamento no encontrado para el id: " + id);
        }
    }
}
