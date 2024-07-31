using AutoMapper;
using AzucareraPomalca.Application.Cores.Dtos;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using AzucareraPomalca.Application.Dtos.Empleados;
using AzucareraPomalca.Application.Dtos.PuestosCursos;
using AzucareraPomalca.Domain.Cores.Models;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Utils.Constants;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IMapper _mapper;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IEmpleadoProfesionService _empleadoProfesionService;
        private readonly IExperienciaLaboralService _experienciaLaboralService;
        private readonly IEmpleadoCursoService _empleadoCursoService;
        private readonly IPerfilCompetenciaEmpleadoService _perfilCompetenciaEmpleadoService;
        private readonly IPuestoService _puestoService;

        public EmpleadoService(IMapper mapper,
                               IEmpleadoRepository empleadoRepository,
                               IEmpleadoProfesionService empleadoProfesionService,
                               IExperienciaLaboralService experienciaLaboralService,
                               IEmpleadoCursoService empleadoCursoService,
                               IPerfilCompetenciaEmpleadoService perfilCompetenciaEmpleadoService,
                               IPuestoService puestoService)
        {
            _mapper = mapper;
            _empleadoRepository = empleadoRepository;
            _empleadoProfesionService = empleadoProfesionService;
            _experienciaLaboralService = experienciaLaboralService;
            _empleadoCursoService = empleadoCursoService;
            _perfilCompetenciaEmpleadoService = perfilCompetenciaEmpleadoService;
            _puestoService = puestoService;
        }

        public async Task<EmpleadoDto> CreateAsync(EmpleadoSaveDto saveDto)
        {
            Empleado empleado = _mapper.Map<Empleado>(saveDto);
            empleado.CreatedAt = DateTime.UtcNow;
            empleado.State = true;

            var response = await _empleadoRepository.SaveAsync(empleado);

            var result = await FindByIdAsync(response.Id);

            return _mapper.Map<EmpleadoDto>(result);
        }

        public Task<RespuestaSimpleDto> CreateMassiveAsync(List<EmpleadoSaveDto> listSaveDto)
        {
            var empleado = _mapper.Map<List<DtEmpleado>>(listSaveDto);
            _empleadoRepository.GuardarMasivoAsync(empleado);

            return Task.FromResult(new RespuestaSimpleDto()
            {
                Mensaje = "Se guardó éxito."
            });
        }

        public async Task<EmpleadoDto> DisabledAsync(int id)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);

            if (empleado is null) throw EmpleadoNotFound(id);

            empleado.State = !empleado.State;

            await _empleadoRepository.SaveAsync(empleado);

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<EmpleadoDto> EditAsync(int id, EmpleadoSaveDto saveDto)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);
            //var validarEmpleado = await _empleadoRepository.FindByNumeroDocumentoAsync(saveDto.NumeroDocumento);

            if (empleado is null) throw EmpleadoNotFound(id);

            _mapper.Map<EmpleadoSaveDto, Empleado>(saveDto, empleado);

            empleado.UpdatedAt = DateTime.UtcNow;

            await _empleadoRepository.SaveAsync(empleado);

            #region PROFESIONES
            if (saveDto.EmpleadoProfesionesSave != null && saveDto.EmpleadoProfesionesSave.Count > 0)
            {
                foreach (var empleadoProfesion in saveDto.EmpleadoProfesionesSave)
                {
                    if (empleadoProfesion.Id != null && empleadoProfesion.Id != 0)
                    {
                        empleadoProfesion.IdEmpleado = empleado.Id;
                        await _empleadoProfesionService.EditAsync((int)empleadoProfesion.Id, empleadoProfesion);
                    }
                    else
                    {
                        empleadoProfesion.IdEmpleado = empleado.Id;
                        await _empleadoProfesionService.CreateAsync(empleadoProfesion);
                    }
                }
            }
            #endregion

            #region EXPERIENCIA LABORAL
            if (saveDto.ExperienciaLaboralesSave != null && saveDto.ExperienciaLaboralesSave.Count > 0)
            {
                foreach (var experienciaLaboral in saveDto.ExperienciaLaboralesSave)
                {
                    if (experienciaLaboral.Id != null && experienciaLaboral.Id != 0)
                    {
                        experienciaLaboral.IdEmpleado = empleado.Id;
                        await _experienciaLaboralService.EditAsync((int)experienciaLaboral.Id, experienciaLaboral);
                    }
                    else
                    {
                        experienciaLaboral.IdEmpleado = empleado.Id;
                        await _experienciaLaboralService.CreateAsync(experienciaLaboral);
                    }
                }
            }
            #endregion

            #region CAPACITACIÓN
            if (saveDto.EmpleadoCursosEspecificosSave != null && saveDto.EmpleadoCursosEspecificosSave.Count > 0)
            {
                foreach (var puestoCurso in saveDto.EmpleadoCursosEspecificosSave)
                {
                    if (puestoCurso.Id != null && puestoCurso.Id != 0)
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.EditAsync((int)puestoCurso.Id, puestoCurso);
                    }
                    else
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.CreateAsync(puestoCurso);
                    }
                }
            }
            if (saveDto.EmpleadoCursosHabilidadesBlandasSave != null && saveDto.EmpleadoCursosHabilidadesBlandasSave.Count > 0)
            {
                foreach (var puestoCurso in saveDto.EmpleadoCursosHabilidadesBlandasSave)
                {
                    if (puestoCurso.Id != null && puestoCurso.Id != 0)
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.EditAsync((int)puestoCurso.Id, puestoCurso);
                    }
                    else
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.CreateAsync(puestoCurso);
                    }
                }
            }
            if (saveDto.EmpleadoCursosSSOMMASave != null && saveDto.EmpleadoCursosSSOMMASave.Count > 0)
            {
                foreach (var puestoCurso in saveDto.EmpleadoCursosSSOMMASave)
                {
                    if (puestoCurso.Id != null && puestoCurso.Id != 0)
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.EditAsync((int)puestoCurso.Id, puestoCurso);
                    }
                    else
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.CreateAsync(puestoCurso);
                    }
                }
            }
            if (saveDto.EmpleadoCursosRSESave != null && saveDto.EmpleadoCursosRSESave.Count > 0)
            {
                foreach (var puestoCurso in saveDto.EmpleadoCursosRSESave)
                {
                    if (puestoCurso.Id != null && puestoCurso.Id != 0)
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.EditAsync((int)puestoCurso.Id, puestoCurso);
                    }
                    else
                    {
                        puestoCurso.IdEmpleado = empleado.Id;
                        await _empleadoCursoService.CreateAsync(puestoCurso);
                    }
                }
            }
            #endregion

            #region PERFIL COMPETENCIAS
            if (saveDto.PerfilCompetenciaEmpleadosSave != null && saveDto.PerfilCompetenciaEmpleadosSave.Count > 0)
            {
                foreach (var perfilCompetenciaEmpleado in saveDto.PerfilCompetenciaEmpleadosSave)
                {
                    if (perfilCompetenciaEmpleado.Id != null && perfilCompetenciaEmpleado.Id != 0)
                    {
                        perfilCompetenciaEmpleado.IdEmpleado = empleado.Id;
                        await _perfilCompetenciaEmpleadoService.EditAsync((int)perfilCompetenciaEmpleado.Id, perfilCompetenciaEmpleado);
                    }
                    else
                    {
                        perfilCompetenciaEmpleado.IdEmpleado = empleado.Id;
                        await _perfilCompetenciaEmpleadoService.CreateAsync(perfilCompetenciaEmpleado);
                    }
                }
            }
            #endregion

            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<EmpleadoDto> EditByDocumentoAsync(Empleado empleado, EmpleadoSaveDto saveDto)
        {
            _mapper.Map<EmpleadoSaveDto, Empleado>(saveDto, empleado);
            empleado.UpdatedAt = DateTime.UtcNow;

            await _empleadoRepository.SaveAsync(empleado);
            return _mapper.Map<EmpleadoDto>(empleado);
        }

        public async Task<IReadOnlyList<EmpleadoDto>> FindAllAsync()
        {
            IReadOnlyList<Empleado> empleados = await _empleadoRepository.FindAllAsync();

            return _mapper.Map<IReadOnlyList<EmpleadoDto>>(empleados);
        }

        public async Task<PageResponse<EmpleadoDto>> FindAllPaginatedAsync(PageRequest<EmpleadoFilterDto> request)
        {
            var filter = request.Filter ?? new EmpleadoFilterDto();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            Expression<Func<Empleado, bool>> predicate = x =>
                (string.IsNullOrWhiteSpace(filter.Nombres) || x.Nombres.ToUpper().Contains(filter.Nombres.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.AppellidoPaterno) || x.AppellidoPaterno.ToUpper().Contains(filter.AppellidoPaterno.ToUpper()))
                && (string.IsNullOrWhiteSpace(filter.AppellidoMaterno) || x.AppellidoMaterno.ToUpper().Contains(filter.AppellidoMaterno.ToUpper()))
                && (!filter.IdCondicionEmpleado.HasValue || x.IdCondicionEmpleado == filter.IdCondicionEmpleado)
                && (!filter.State.HasValue || x.State == filter.State);

            List<Expression<Func<Empleado, object>>>? includes = new List<Expression<Func<Empleado, object>>>()
            {
                t => t.Puesto,
                t => t.Puesto.Gerencia,
                t => t.Puesto.Division,
                t => t.Puesto.Departamento,
                t => t.Puesto.Seccion
            };

            var response = await _empleadoRepository.FindAllPaginatedAsync(paging: paging, predicate: predicate, includes: includes);

            return _mapper.Map<PageResponse<EmpleadoDto>>(response);
        }

        public async Task<EmpleadoDto> FindByIdAsync(int id)
        {
            Empleado? empleado = await _empleadoRepository.FindByIdAsync(id);

            if (empleado is null) throw EmpleadoNotFound(id);

            var response = _mapper.Map<EmpleadoDto>(empleado);

            if (empleado.EmpleadoCursos != null && empleado.EmpleadoCursos.Count() > 0)
            {
                List<EmpleadoCursoDto> EmpleadoCursosEspecificos = new List<EmpleadoCursoDto>();
                List<EmpleadoCursoDto> EmpleadoCursosSSOMMA = new List<EmpleadoCursoDto>();
                List<EmpleadoCursoDto> EmpleadoCursosHabilidadesBlandas = new List<EmpleadoCursoDto>();
                List<EmpleadoCursoDto> EmpleadoCursosRSE = new List<EmpleadoCursoDto>();

                foreach (var puestoCurso in empleado.EmpleadoCursos)
                {
                    switch (puestoCurso.Curso.TipoCurso.Descripcion)
                    {
                        case TiposCursos.ESPECIFICO:
                            EmpleadoCursosEspecificos.Add(_mapper.Map<EmpleadoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.SSOMMA:
                            EmpleadoCursosSSOMMA.Add(_mapper.Map<EmpleadoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.HABILIDADES_BLANDAS:
                            EmpleadoCursosHabilidadesBlandas.Add(_mapper.Map<EmpleadoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.RSE:
                            EmpleadoCursosRSE.Add(_mapper.Map<EmpleadoCursoDto>(puestoCurso));
                            break;
                    }
                }

                response.EmpleadoCursosEspecificos = EmpleadoCursosEspecificos;
                response.EmpleadoCursosSSOMMA = EmpleadoCursosSSOMMA;
                response.EmpleadoCursosHabilidadesBlandas = EmpleadoCursosHabilidadesBlandas;
                response.EmpleadoCursosRSE = EmpleadoCursosRSE;
            }

            if (empleado.Puesto.PuestosCursos != null && empleado.Puesto.PuestosCursos.Count() > 0)
            {
                List<PuestoCursoDto> PuestosCursosEspecificos = new List<PuestoCursoDto>();
                List<PuestoCursoDto> PuestosCursosSSOMMA = new List<PuestoCursoDto>();
                List<PuestoCursoDto> PuestosCursosHabilidadesBlandas = new List<PuestoCursoDto>();
                List<PuestoCursoDto> PuestosCursosRSE = new List<PuestoCursoDto>();

                foreach (var puestoCurso in empleado.Puesto.PuestosCursos)
                {
                    switch (puestoCurso.Curso.TipoCurso.Descripcion)
                    {
                        case TiposCursos.ESPECIFICO:
                            PuestosCursosEspecificos.Add(_mapper.Map<PuestoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.SSOMMA:
                            PuestosCursosSSOMMA.Add(_mapper.Map<PuestoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.HABILIDADES_BLANDAS:
                            PuestosCursosHabilidadesBlandas.Add(_mapper.Map<PuestoCursoDto>(puestoCurso));
                            break;
                        case TiposCursos.RSE:
                            PuestosCursosRSE.Add(_mapper.Map<PuestoCursoDto>(puestoCurso));
                            break;
                    }
                }

                response.Puesto.PuestosCursosEspecificos = PuestosCursosEspecificos;
                response.Puesto.PuestosCursosSSOMMA = PuestosCursosSSOMMA;
                response.Puesto.PuestosCursosHabilidadesBlandas = PuestosCursosHabilidadesBlandas;
                response.Puesto.PuestosCursosRSE = PuestosCursosRSE;
            }

            return response;
        }

        private NotFoundCoreException EmpleadoNotFound(int id)
        {
            return new NotFoundCoreException("Empleado no encontrada para el id: " + id);
        }

        public async Task<PageResponse<EmpleadoSugeridoDto>> EmpleadosSugeridosPaginatedAsync(PageRequest<EmpleadoSugeridoFilterDto> request)
        {
            var filter = request.Filter != null ? _mapper.Map<EmpleadoSugerido>(request.Filter) : new EmpleadoSugerido();
            var paging = new Paging() { PageNumber = request.Page, PageSize = request.PerPage };

            var response = await _empleadoRepository.ListarEmpleadosSugeridosAsync(paging: paging, filter);

            var res = _mapper.Map<PageResponse<EmpleadoSugeridoDto>>(response);

            return res;
        }
    }
}
