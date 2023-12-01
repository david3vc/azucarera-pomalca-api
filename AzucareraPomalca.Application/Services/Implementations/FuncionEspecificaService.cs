using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.FuncionesEspecificas;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class FuncionEspecificaService : IFuncionEspecificaService
    {
        private readonly IFuncionEspecificaRepository _funcionEspecificaRepository;
        private readonly IMapper _mapper;

        public FuncionEspecificaService(IMapper mapper, IFuncionEspecificaRepository funcionEspecificaRepository)
        {
            _mapper = mapper;
            _funcionEspecificaRepository = funcionEspecificaRepository;
        }

        public async Task<FuncionEspecificaDto> CreateAsync(FuncionEspecificaSaveDto saveDto)
        {
            FuncionEspecifica funcionEspecifica = _mapper.Map<FuncionEspecifica>(saveDto);
            funcionEspecifica.CreatedAt = DateTime.UtcNow;
            funcionEspecifica.State = true;

            await _funcionEspecificaRepository.SaveAsync(funcionEspecifica);

            return _mapper.Map<FuncionEspecificaDto>(funcionEspecifica);
        }

        public async Task<FuncionEspecificaDto> DisabledAsync(int id)
        {
            FuncionEspecifica? funcionEspecifica = await _funcionEspecificaRepository.FindByIdAsync(id);

            if (funcionEspecifica is null) throw FuncionEspecificaNotFound(id);

            funcionEspecifica.State = !funcionEspecifica.State;

            await _funcionEspecificaRepository.SaveAsync(funcionEspecifica);

            return _mapper.Map<FuncionEspecificaDto>(funcionEspecifica);
        }

        public async Task<FuncionEspecificaDto> EditAsync(int id, FuncionEspecificaSaveDto saveDto)
        {
            FuncionEspecifica? funcionEspecifica = await _funcionEspecificaRepository.FindByIdAsync(id);

            if (funcionEspecifica is null) throw FuncionEspecificaNotFound(id);

            _mapper.Map<FuncionEspecificaSaveDto, FuncionEspecifica>(saveDto, funcionEspecifica);

            funcionEspecifica.UpdatedAt = DateTime.UtcNow;

            await _funcionEspecificaRepository.SaveAsync(funcionEspecifica);

            return _mapper.Map<FuncionEspecificaDto>(funcionEspecifica);
        }

        public Task<IReadOnlyList<FuncionEspecificaDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<FuncionEspecificaDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException FuncionEspecificaNotFound(int id)
        {
            return new NotFoundCoreException("Funcion especifica no encontrada para el id: " + id);
        }
    }
}
