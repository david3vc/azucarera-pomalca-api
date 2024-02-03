using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.PuestosProfesiones;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PuestoProfesionService : IPuestoProfesionService
    {
        private readonly IPuestoProfesionRepository _puestoProfesionRepository;
        private readonly IMapper _mapper;

        public PuestoProfesionService(IMapper mapper, IPuestoProfesionRepository puestoProfesionRepository)
        {
            _mapper = mapper;
            _puestoProfesionRepository = puestoProfesionRepository;
        }

        public async Task<PuestoProfesionDto> CreateAsync(PuestoProfesionSaveDto saveDto)
        {
            PuestoProfesion puestoProfesion = _mapper.Map<PuestoProfesion>(saveDto);
            puestoProfesion.CreatedAt = DateTime.UtcNow;
            puestoProfesion.State = true;

            Expression<Func<PuestoProfesion, bool>> predicate = x => x.IdProfesion == saveDto.IdProfesion && x.IdPuesto == saveDto.IdPuesto;

            var validar = await _puestoProfesionRepository.FindByIdAsync(predicate);

            if (validar != null)
            {
                if (validar.State == false)
                {
                    var save = _mapper.Map<PuestoProfesionSaveDto>(validar);
                    return await EditAsync(validar.Id, save);
                }
                else throw new BadRequestCoreException("Ya se registó la misma carrera con el mismo grado académico.");
            }
            else
            {
                await _puestoProfesionRepository.SaveAsync(puestoProfesion);
                return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
            }
        }

        public async Task<PuestoProfesionDto> DisabledAsync(int id)
        {
            PuestoProfesion? puestoProfesion = await _puestoProfesionRepository.FindByIdAsync(id);

            if (puestoProfesion is null) throw PuestoProfesionNotFound(id);

            puestoProfesion.State = false;

            await _puestoProfesionRepository.SaveAsync(puestoProfesion);

            return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
        }

        public async Task<PuestoProfesionDto> EditAsync(int id, PuestoProfesionSaveDto saveDto)
        {
            PuestoProfesion? puestoProfesion = await _puestoProfesionRepository.FindByIdAsync(id);

            if (puestoProfesion is null) throw PuestoProfesionNotFound(id);

            _mapper.Map<PuestoProfesionSaveDto, PuestoProfesion>(saveDto, puestoProfesion);

            puestoProfesion.UpdatedAt = DateTime.UtcNow;
            puestoProfesion.State = true;

            Expression<Func<PuestoProfesion, bool>> predicate = x => x.IdProfesion == saveDto.IdProfesion && x.IdPuesto == saveDto.IdPuesto;

            var validar = await _puestoProfesionRepository.FindByIdAsync(predicate);

            if (validar != null)
            {
                if (validar.State == true && validar.Id != puestoProfesion.Id) throw new BadRequestCoreException("Ya se registó la misma carrera con el mismo grado académico.");
                else if(validar.Id == puestoProfesion.Id) await _puestoProfesionRepository.SaveAsync(puestoProfesion);
            }
            else
            {
                await _puestoProfesionRepository.SaveAsync(puestoProfesion);
            }

            return _mapper.Map<PuestoProfesionDto>(puestoProfesion);
        }

        public Task<IReadOnlyList<PuestoProfesionDto>> FindAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PuestoProfesionDto> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PuestoProfesionDto>> ProfesionesPuestoByIdPuesto(int idPuesto)
        {
            List<PuestoProfesion> puestoProfesiones = await _puestoProfesionRepository.ProfesionesPuestoByIdPuesto(idPuesto);

            if (puestoProfesiones is null) throw PuestoProfesionNotFound(idPuesto);

            return _mapper.Map<List<PuestoProfesionDto>>(puestoProfesiones);
        }

        private NotFoundCoreException PuestoProfesionNotFound(int id)
        {
            return new NotFoundCoreException("PuestoProfesion no encontrado para el id: " + id);
        }
    }
}
