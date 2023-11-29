using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Puestos;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IMisionService _misionService;
        private readonly IMapper _mapper;

        public PuestoService(IPuestoRepository puestoRepository, IMapper mapper, IMisionService misionService)
        {
            _puestoRepository = puestoRepository;
            _mapper = mapper;
            _misionService = misionService;
        }

        public async Task<PuestoDto> CreateAsync(PuestoSaveDto saveDto)
        {
            Puesto puesto = _mapper.Map<Puesto>(saveDto);
            puesto.CreatedAt = DateTime.UtcNow;
            puesto.State = true;

            await _puestoRepository.SaveAsync(puesto);

            #region MISION
            if(saveDto.MisionesSave != null && saveDto.MisionesSave.Count > 0)
            {
                foreach(var mision in saveDto.MisionesSave)
                {
                    await _misionService.CreateAsync(mision);
                }
            }
            #endregion

            return _mapper.Map<PuestoDto>(puesto);
        }

        public async Task<PuestoDto> DisabledAsync(int id)
        {
            Puesto? puesto = await _puestoRepository.FindByIdAsync(id);

            if (puesto is null) throw PuestoNotFound(id);

            puesto.State = false;

            await _puestoRepository.SaveAsync(puesto);

            return _mapper.Map<PuestoDto>(puesto);
        }

        public async Task<PuestoDto> EditAsync(int id, PuestoSaveDto saveDto)
        {
            Puesto? puesto = await _puestoRepository.FindByIdAsync(id);

            if (puesto is null) throw PuestoNotFound(id);

            _mapper.Map<PuestoSaveDto, Puesto>(saveDto, puesto);

            puesto.UpdatedAt = DateTime.UtcNow;

            await _puestoRepository.SaveAsync(puesto);

            #region MISION
            if (saveDto.MisionesSave != null && saveDto.MisionesSave.Count > 0)
            {
                foreach (var mision in saveDto.MisionesSave)
                {
                    if(mision.Id != null && mision.Id != 0)
                    {
                        mision.IdPuesto = puesto.Id;
                        await _misionService.EditAsync((int)mision.Id, mision);
                    }
                    else
                    {
                        mision.IdPuesto = puesto.Id;
                        await _misionService.CreateAsync(mision);
                    }
                }
            }
            #endregion

            return _mapper.Map<PuestoDto>(puesto);
        }

        public async Task<IReadOnlyList<PuestoDto>> FindAllAsync()
        {
            IReadOnlyList<Puesto> puestos = await _puestoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<PuestoDto>>(puestos);
        }

        public async Task<PuestoDto> FindByIdAsync(int id)
        {
            Puesto? puesto = await _puestoRepository.FindByIdAsync(id);

            if (puesto is null) throw PuestoNotFound(id);

            return _mapper.Map<PuestoDto>(puesto);
        }

        private NotFoundCoreException PuestoNotFound(int id)
        {
            return new NotFoundCoreException("Puesto no encontrado para el id: " + id);
        }
    }
}
