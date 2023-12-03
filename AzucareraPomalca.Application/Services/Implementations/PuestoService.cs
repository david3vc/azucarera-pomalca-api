using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.Coordinaciones;
using AzucareraPomalca.Application.Dtos.Puestos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class PuestoService : IPuestoService
    {
        private readonly IPuestoRepository _puestoRepository;
        private readonly IMisionService _misionService;
        private readonly IFuncionEspecificaService _funcionEspecificaService;
        private readonly ICoordinacionService _coordinacionService;
        private readonly IPuestoProfesionService _puestoProfesionService;
        private readonly IMapper _mapper;

        public PuestoService(IPuestoRepository puestoRepository,
                             IMapper mapper, IMisionService misionService,
                             IFuncionEspecificaService funcionEspecificaService,
                             ICoordinacionService coordinacionService,
                             IPuestoProfesionService puestoProfesionService
                            )
        {
            _puestoRepository = puestoRepository;
            _mapper = mapper;
            _misionService = misionService;
            _funcionEspecificaService = funcionEspecificaService;
            _coordinacionService = coordinacionService;
            _puestoProfesionService = puestoProfesionService;
        }

        public async Task<PuestoDto> CreateAsync(PuestoSaveDto saveDto)
        {
            Puesto puesto = _mapper.Map<Puesto>(saveDto);
            puesto.CreatedAt = DateTime.UtcNow;
            puesto.State = true;

            await _puestoRepository.SaveAsync(puesto);

            #region MISION
            if (saveDto.MisionesSave != null && saveDto.MisionesSave.Count > 0)
            {
                foreach (var mision in saveDto.MisionesSave)
                {
                    mision.IdPuesto = puesto.Id;
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
                    if (mision.Id != null && mision.Id != 0)
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

            #region FUNCION ESPECIFICA
            if (saveDto.FuncionesEspecificasSave != null && saveDto.FuncionesEspecificasSave.Count > 0)
            {
                foreach (var funcionEspecifica in saveDto.FuncionesEspecificasSave)
                {
                    if (funcionEspecifica.Id != null && funcionEspecifica.Id != 0)
                    {
                        funcionEspecifica.IdPuesto = puesto.Id;
                        await _funcionEspecificaService.EditAsync((int)funcionEspecifica.Id, funcionEspecifica);
                    }
                    else
                    {
                        funcionEspecifica.IdPuesto = puesto.Id;
                        await _funcionEspecificaService.CreateAsync(funcionEspecifica);
                    }
                }
            }
            #endregion

            #region COORDINACION CON OTRAS AREAS
            if (saveDto.CoordinacionesMismaGerenciaSave != null && saveDto.CoordinacionesMismaGerenciaSave.Count > 0)
            {
                foreach (var mismaGerencia in saveDto.CoordinacionesMismaGerenciaSave)
                {
                    if (mismaGerencia.Id != null && mismaGerencia.Id != 0)
                    {
                        await _coordinacionService.EditAsync((int)mismaGerencia.Id, mismaGerencia);
                    }
                    else
                    {
                        await _coordinacionService.CreateAsync(mismaGerencia);
                    }
                }
            }
            if (saveDto.CoordinacionesOtraGerenciaSave != null && saveDto.CoordinacionesOtraGerenciaSave.Count > 0)
            {
                foreach (var otraGerencia in saveDto.CoordinacionesOtraGerenciaSave)
                {
                    if (otraGerencia.Id != null && otraGerencia.Id != 0)
                    {
                        await _coordinacionService.EditAsync((int)otraGerencia.Id, otraGerencia);
                    }
                    else
                    {
                        await _coordinacionService.CreateAsync(otraGerencia);
                    }
                }
            }
            if (saveDto.CoordinacionesExternasSave != null && saveDto.CoordinacionesExternasSave.Count > 0)
            {
                foreach (var externa in saveDto.CoordinacionesExternasSave)
                {
                    if (externa.Id != null && externa.Id != 0)
                    {
                        await _coordinacionService.EditAsync((int)externa.Id, externa);
                    }
                    else
                    {
                        await _coordinacionService.CreateAsync(externa);
                    }
                }
            }

            #endregion

            #region PROFESION
            if(saveDto.PuestosProfesionesSave != null && saveDto.PuestosProfesionesSave.Count > 0)
            {
                foreach (var puestoProfesion in saveDto.PuestosProfesionesSave)
                {
                    if (puestoProfesion.Id != null && puestoProfesion.Id != 0)
                    {
                        puestoProfesion.IdPuesto = puesto.Id;
                        await _puestoProfesionService.EditAsync((int)puestoProfesion.Id, puestoProfesion);
                    }
                    else
                    {
                        puestoProfesion.IdPuesto = puesto.Id;
                        await _puestoProfesionService.CreateAsync(puestoProfesion);
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

        public async Task<PageResponse<PuestoDto>> FindAllPaginatedAsync(PageRequest<PuestoFilterDto> request)
        {
            var filter = request.Filter ?? new PuestoFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Puesto, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombre) || x.Nombre.ToUpper().Contains(filter.Nombre.ToUpper()))
                && (!filter.IdGerencia.HasValue || x.IdGerencia == filter.IdGerencia);

            List<Expression<Func<Puesto, object>>> includes = new List<Expression<Func<Puesto, object>>>()
            {
                t => t.Gerencia
            };

            var response = await _puestoRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<PuestoDto>>(response);
        }

        public async Task<PuestoDto> FindByIdAsync(int id)
        {
            Puesto? puesto = await _puestoRepository.FindByIdAsync(id);

            if (puesto is null) throw PuestoNotFound(id);

            var response = _mapper.Map<PuestoDto>(puesto);

            if (puesto.CoordinacionesPuestoCoordinador != null && puesto.CoordinacionesPuestoCoordinador.Count > 0)
            {
                List<CoordinacionDto> mismaGerencia = new List<CoordinacionDto>();
                List<CoordinacionDto> otraGerencia = new List<CoordinacionDto>();

                foreach (var coordinacion in puesto.CoordinacionesPuestoCoordinador)
                {
                    Puesto? puestoCoordinado = await _puestoRepository.FindByIdAsync(coordinacion.IdPuestoCoordinado);
                    if (puestoCoordinado != null && puestoCoordinado.IdGerencia == puesto.IdGerencia)
                    {
                        var coordinacionDto = _mapper.Map<CoordinacionDto>(coordinacion);
                        coordinacionDto.PuestoCoordinado = _mapper.Map<PuestoDto>(puestoCoordinado);
                        mismaGerencia.Add(coordinacionDto);
                    }
                    else
                    {
                        var coordinacionDto = _mapper.Map<CoordinacionDto>(coordinacion);
                        coordinacionDto.PuestoCoordinado = _mapper.Map<PuestoDto>(puestoCoordinado);
                        otraGerencia.Add(coordinacionDto);
                    }
                }

                response.CoordinacionesMismaGerencia = mismaGerencia;
                response.CoordinacionesOtraGerencia = otraGerencia;
            }

            return response;
        }

        private NotFoundCoreException PuestoNotFound(int id)
        {
            return new NotFoundCoreException("Puesto no encontrado para el id: " + id);
        }
    }
}
