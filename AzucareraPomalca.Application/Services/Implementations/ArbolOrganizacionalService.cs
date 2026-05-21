using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.ArbolOrganizacional;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using AzucareraPomalca.Utils.Constants;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class ArbolOrganizacionalService : IArbolOrganizacionalService
    {
        private readonly IGerenciaRepository _gerenciaRepository;
        private readonly IDivisionRepository _divisionRepository;
        private readonly IDepartamentoRepository _departamentoRepository;
        private readonly ISeccionRepository _seccionRepository;
        private readonly IPuestoRepository _puestoRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public ArbolOrganizacionalService(
                                             IGerenciaRepository gerenciaRepository,
                                             IPuestoRepository puestoRepository,
                                             IMapper mapper,
                                             IDivisionRepository divisionRepository,
                                             IDepartamentoRepository departamentoRepository,
                                             ISeccionRepository seccionRepository,
                                             IEmpleadoRepository empleadoRepository
                                         )
        {
            _gerenciaRepository = gerenciaRepository;
            _puestoRepository = puestoRepository;
            _mapper = mapper;
            _divisionRepository = divisionRepository;
            _departamentoRepository = departamentoRepository;
            _seccionRepository = seccionRepository;
            _empleadoRepository = empleadoRepository;
        }

        public async Task<NodoDto> FindNodoPrincipalPadre()
        {
            List<Gerencia> gerenciasSubalternas = await _gerenciaRepository.FindGerenciasSubalternasAsync();
            Gerencia? gerenciaGeneral = await _gerenciaRepository.FindByNombreAsync("general");

            if (gerenciaGeneral is null) throw NotFound("general", "Gerencia General");

            var nodoGerenciaGeneral = new NodoDto()
            {
                Id = gerenciaGeneral.Id,
                Key = $"{gerenciaGeneral.Id}-{gerenciaGeneral.Nombre}",
                Nombre = gerenciaGeneral.Nombre,
                TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                TipoUnidadOrganizacional = TiposUnidadOrganizacional.GERENCIA,
                Children = new List<NodoDto>()
            };

            foreach (var child in gerenciaGeneral.Puestos)
            {
                Puesto? puestoJefe = FindNodoPuestoJefe(gerenciaGeneral.Puestos);
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre ?? "",
                    TipoNodo = TiposNodo.PUESTO,
                    EsJefe = child.IdPuestoSupervisor == puestoJefe?.Id ? false : true,
                };
                nodoGerenciaGeneral.Children.Add(childNodo);
            }

            foreach (var child in gerenciasSubalternas)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.GERENCIA,
                };
                nodoGerenciaGeneral.Children.Add(childNodo);
            }

            return nodoGerenciaGeneral;
        }

        public async Task<NodoDto> FindNodoUnidadOrganizacionByIdUnidadOrganizacionalAsync(NodoFilterDto filter)
        {
            switch (filter.TipoUnidadOrganizacional)
            {
                case TiposUnidadOrganizacional.GERENCIA:
                    var nodoGerencia = await FindNodoGerenciaById(filter.Id);
                    return nodoGerencia;
                case TiposUnidadOrganizacional.DIVISION:
                    var nodoDivision = await FindNodoDivisionById(filter.Id);
                    return nodoDivision;
                case TiposUnidadOrganizacional.DEPARTAMENTO:
                    var nodoDepartamento = await FindNodoDepartamentoById(filter.Id);
                    return nodoDepartamento;
                case TiposUnidadOrganizacional.SECCION:
                    var nodoSeccion = await FindNodoSeccionById(filter.Id);
                    return nodoSeccion;

                default: throw new NotImplementedException();
            }
        }

        private async Task<NodoDto> FindNodoGerenciaById(int id)
        {
            Gerencia? gerencia = await _gerenciaRepository.FindByIdAsync(id);
            if (gerencia is null) throw NotFound(id.ToString(), "Gerencia");
            List<Division> divisiones = await _divisionRepository.FindByIdGerenciaAsync(gerencia.Id);

            var departamentoRequest = new Departamento()
            {
                IdGerencia = gerencia.Id,
            };
            List<Departamento> departamentos = await _departamentoRepository.SearchByUnidadOrganizacional(departamentoRequest);

            var seccionRequest = new Seccion()
            {
                IdGerencia = gerencia.Id,
            };
            List<Seccion> secciones = await _seccionRepository.SearchByUnidadOrganizacional(seccionRequest);

            var puestoRequest = new Puesto()
            {
                IdGerencia = gerencia.Id
            };
            List<Puesto> puestos = await _puestoRepository.SearchByUnidadOrganizacionalAsync(puestoRequest);

            var nodoGerencia = new NodoDto()
            {
                Id = gerencia.Id,
                Key = $"{gerencia.Id}-{gerencia.Nombre}",
                Nombre = gerencia.Nombre,
                TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                TipoUnidadOrganizacional = TiposUnidadOrganizacional.GERENCIA,
                Children = new List<NodoDto>()
            };

            foreach (var child in puestos)
            {
                Puesto? puestoJefe = FindNodoPuestoJefe(gerencia.Puestos);
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre ?? "",
                    TipoNodo = TiposNodo.PUESTO,
                    EsJefe = child.IdPuestoSupervisor == puestoJefe?.Id ? false : true,
                };
                nodoGerencia.Children.Add(childNodo);
            }

            foreach (var child in secciones)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.SECCION
                };
                nodoGerencia.Children.Add(childNodo);
            }

            foreach (var child in departamentos)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.DEPARTAMENTO
                };
                nodoGerencia.Children.Add(childNodo);
            }

            foreach (var child in divisiones)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.DIVISION
                };
                nodoGerencia.Children.Add(childNodo);
            }

            return nodoGerencia;
        }

        private async Task<NodoDto> FindNodoDivisionById(int id)
        {
            Division? division = await _divisionRepository.FindByIdAsync(id);
            if (division is null) throw NotFound(id.ToString(), "Division");
            List<Departamento> departamentos = await _departamentoRepository.FindByIdDivisionAsync(division.Id);

            var seccionRequest = new Seccion()
            {
                IdGerencia = division.IdGerencia,
                IdDivision = division.Id
            };
            List<Seccion> secciones = await _seccionRepository.SearchByUnidadOrganizacional(seccionRequest);

            var puestoRequest = new Puesto()
            {
                IdGerencia = division.IdGerencia,
                IdDivision = division.Id
            };
            List<Puesto> puestos = await _puestoRepository.SearchByUnidadOrganizacionalAsync(puestoRequest);

            var nodoDivision = new NodoDto()
            {
                Id = division.Id,
                Key = $"{division.Id}-{division.Nombre}",
                Nombre = division.Nombre,
                TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                TipoUnidadOrganizacional = TiposUnidadOrganizacional.DIVISION,
                Children = new List<NodoDto>()
            };

            foreach (var child in puestos)
            {
                Puesto? puestoJefe = FindNodoPuestoJefe(division.Puestos);
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre ?? "",
                    TipoNodo = TiposNodo.PUESTO,
                    EsJefe = child.IdPuestoSupervisor == puestoJefe?.Id ? false : true,
                };
                nodoDivision.Children.Add(childNodo);
            }

            foreach (var child in secciones)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.SECCION
                };
                nodoDivision.Children.Add(childNodo);
            }

            foreach (var child in departamentos)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.DEPARTAMENTO
                };
                nodoDivision.Children.Add(childNodo);
            }

            return nodoDivision;
        }

        private async Task<NodoDto> FindNodoDepartamentoById(int id)
        {
            Departamento? departamento = await _departamentoRepository.FindByIdAsync(id);
            if (departamento is null) throw NotFound(id.ToString(), "Departamento");
            List<Seccion> secciones = await _seccionRepository.FindByIdDepartamentoAsync(departamento.Id);

            var puestoRequest = new Puesto()
            {
                IdGerencia = departamento.IdGerencia,
                IdDivision = departamento.IdDivision,
                IdDepartamento = departamento.Id
            };
            List<Puesto> puestos = await _puestoRepository.SearchByUnidadOrganizacionalAsync(puestoRequest);

            var nodoDepartamento = new NodoDto()
            {
                Id = departamento.Id,
                Key = $"{departamento.Id}-{departamento.Nombre}",
                Nombre = departamento.Nombre,
                TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                TipoUnidadOrganizacional = TiposUnidadOrganizacional.DEPARTAMENTO,
                Children = new List<NodoDto>()
            };

            foreach (var child in puestos)
            {
                Puesto? puestoJefe = FindNodoPuestoJefe(departamento.Puestos);
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre ?? "",
                    TipoNodo = TiposNodo.PUESTO,
                    EsJefe = child.IdPuestoSupervisor == puestoJefe?.Id ? false : true,
                };
                nodoDepartamento.Children.Add(childNodo);
            }

            foreach (var child in secciones)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre,
                    TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                    TipoUnidadOrganizacional = TiposUnidadOrganizacional.SECCION
                };
                nodoDepartamento.Children.Add(childNodo);
            }

            return nodoDepartamento;
        }

        private async Task<NodoDto> FindNodoSeccionById(int id)
        {
            Seccion? seccion = await _seccionRepository.FindByIdAsync(id);
            if (seccion is null) throw NotFound(id.ToString(), "Seccion");

            var puestoRequest = new Puesto()
            {
                IdGerencia = seccion.IdGerencia,
                IdDivision = seccion.IdDivision,
                IdDepartamento = seccion.IdDepartamento,
                IdSeccion = seccion.Id
            };
            List<Puesto> puestos = await _puestoRepository.SearchByUnidadOrganizacionalAsync(puestoRequest);

            var nodoSeccion = new NodoDto()
            {
                Id = seccion.Id,
                Key = $"{seccion.Id}-{seccion.Nombre}",
                Nombre = seccion.Nombre,
                TipoNodo = TiposNodo.UNIDAD_ORGANIZACIONAL,
                TipoUnidadOrganizacional = TiposUnidadOrganizacional.SECCION,
                Children = new List<NodoDto>()
            };

            foreach (var child in puestos)
            {
                Puesto? puestoJefe = FindNodoPuestoJefe(seccion.Puestos);
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.Nombre}",
                    Nombre = child.Nombre ?? "",
                    TipoNodo = TiposNodo.PUESTO,
                    EsJefe = child.IdPuestoSupervisor == puestoJefe?.Id ? false : true,
                };
                nodoSeccion.Children.Add(childNodo);
            }

            return nodoSeccion;
        }

        public async Task<List<NodoDto>> FindNodoPuestoById(int id)
        {
            Puesto? puesto = await _puestoRepository.FindByIdAsync(id);
            List<Empleado> empleados = await _empleadoRepository.FindByIdPuestoAsync(id);

            // Determinar si el puesto es jefe de su unidad organizacional
            bool esJefe = false;
            if (puesto != null)
            {
                var puestoRequest = new Puesto()
                {
                    IdGerencia = puesto.IdGerencia,
                    IdDivision = puesto.IdDivision,
                    IdDepartamento = puesto.IdDepartamento,
                    IdSeccion = puesto.IdSeccion
                };
                List<Puesto> puestosHermanos = await _puestoRepository.SearchByUnidadOrganizacionalAsync(puestoRequest);
                Puesto? puestoJefe = FindNodoPuestoJefe(puestosHermanos);
                // Misma logica que en los demas metodos: es jefe si su supervisor no es el puesto jefe
                esJefe = puesto.IdPuestoSupervisor != puestoJefe?.Id;
            }

            List<NodoDto> nodosEmpleados = new List<NodoDto>();

            foreach (var child in empleados)
            {
                var childNodo = new NodoDto()
                {
                    Id = child.Id,
                    Key = $"{child.Id}-{child.AppellidoPaterno}-{child.AppellidoMaterno}-{child.Nombres}",
                    Nombre = $"{child.AppellidoPaterno} {child.AppellidoMaterno} {child.Nombres}",
                    TipoNodo = TiposNodo.EMPLEADO,
                    EsJefe = esJefe
                };
                nodosEmpleados.Add(childNodo);
            }

            return nodosEmpleados;
        }

        private NotFoundCoreException NotFound(string id, string message)
        {
            return new NotFoundCoreException($"{message} no encontrada para el id: {id}");
        }

        private Puesto? FindNodoPuestoJefe(ICollection<Puesto> puestos)
        {
            var frecuenciaIdSupervisor = puestos
                                                .Where(x => x.IdPuestoSupervisor != null)
                                                .GroupBy(x => x.IdPuestoSupervisor)
                                                .ToDictionary(group => group.Key, group => group.Count());

            Puesto puesto = puestos.FirstOrDefault(puesto => puesto.IdPuestoSupervisor == null || frecuenciaIdSupervisor.GetValueOrDefault(puesto.IdPuestoSupervisor, 0) == 1);

            return puesto;
        }
    }
}
