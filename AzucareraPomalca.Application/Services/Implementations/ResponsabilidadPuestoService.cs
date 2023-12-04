using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.ResponsabilidadesPuestos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ResponsabilidadPuestoService : IResponsabilidadPuestoService
    {
        private readonly IResponsabilidadPuestoRepository _responsabilidadPuestoRepository;
        private readonly IMapper _mapper;

        public ResponsabilidadPuestoService(IResponsabilidadPuestoRepository responsabilidadPuestoRepository, IMapper mapper)
        {
            _responsabilidadPuestoRepository = responsabilidadPuestoRepository;
            _mapper = mapper;
        }

        public async Task<ResponsabilidadPuestoDto> CreateAsync(ResponsabilidadPuestoSaveDto saveDto)
        {
            ResponsabilidadPuesto responsabilidadPuesto = _mapper.Map<ResponsabilidadPuesto>(saveDto);
            responsabilidadPuesto.CreatedAt = DateTime.UtcNow;
            responsabilidadPuesto.State = true;

            await _responsabilidadPuestoRepository.SaveAsync(responsabilidadPuesto);

            return _mapper.Map<ResponsabilidadPuestoDto>(responsabilidadPuesto);
        }

        public async Task<ResponsabilidadPuestoDto> DisabledAsync(int id)
        {
            ResponsabilidadPuesto? responsabilidadPuesto = await _responsabilidadPuestoRepository.FindByIdAsync(id);

            if (responsabilidadPuesto is null) throw ResponsabilidadPuestoNotFound(id);

            responsabilidadPuesto.State = !responsabilidadPuesto.State;

            await _responsabilidadPuestoRepository.SaveAsync(responsabilidadPuesto);

            return _mapper.Map<ResponsabilidadPuestoDto>(responsabilidadPuesto);
        }

        public async Task<ResponsabilidadPuestoDto> EditAsync(int id, ResponsabilidadPuestoSaveDto saveDto)
        {
            ResponsabilidadPuesto? responsabilidadPuesto = await _responsabilidadPuestoRepository.FindByIdAsync(id);

            if (responsabilidadPuesto is null) throw ResponsabilidadPuestoNotFound(id);

            _mapper.Map<ResponsabilidadPuestoSaveDto, ResponsabilidadPuesto>(saveDto, responsabilidadPuesto);

            responsabilidadPuesto.UpdatedAt = DateTime.UtcNow;

            await _responsabilidadPuestoRepository.SaveAsync(responsabilidadPuesto);

            return _mapper.Map<ResponsabilidadPuestoDto>(responsabilidadPuesto);
        }

        public Task<IReadOnlyList<ResponsabilidadPuestoDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponsabilidadPuestoDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException ResponsabilidadPuestoNotFound(int id)
        {
            return new NotFoundCoreException("Responsabilidad Puesto no encontrado para el id: " + id);
        }
    }
}
