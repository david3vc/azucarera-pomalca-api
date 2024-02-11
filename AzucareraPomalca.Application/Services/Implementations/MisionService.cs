using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Misiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class MisionService : IMisionService
    {
        private readonly IMapper _mapper;
        private readonly IMisionRepository _misionRepository;

        public MisionService(IMisionRepository misionRepository, IMapper mapper)
        {
            _misionRepository = misionRepository;
            _mapper = mapper;
        }

        public async Task<MisionDto> CreateAsync(MisionSaveDto saveDto)
        {
            Mision mision = _mapper.Map<Mision>(saveDto);
            mision.CreatedAt = DateTime.UtcNow;
            mision.State = true;

            await _misionRepository.SaveAsync(mision);

            return _mapper.Map<MisionDto>(mision);
        }

        public async Task<MisionDto> DisabledAsync(int id)
        {
            Mision? mision = await _misionRepository.FindByIdAsync(id);

            if (mision is null) throw MisionNotFound(id);

            mision.State = false;

            await _misionRepository.SaveAsync(mision);

            return _mapper.Map<MisionDto>(mision);
        }

        public async Task<MisionDto> EditAsync(int id, MisionSaveDto saveDto)
        {
            Mision? mision = await _misionRepository.FindByIdAsync(id);

            if (mision is null) throw MisionNotFound(id);

            _mapper.Map<MisionSaveDto, Mision>(saveDto, mision);

            mision.UpdatedAt = DateTime.UtcNow;

            await _misionRepository.SaveAsync(mision);

            return _mapper.Map<MisionDto>(mision);
        }

        public Task<IReadOnlyList<MisionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MisionDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        private NotFoundCoreException MisionNotFound(int id)
        {
            return new NotFoundCoreException("Mision no encontrado para el id: " + id);
        }
    }
}
