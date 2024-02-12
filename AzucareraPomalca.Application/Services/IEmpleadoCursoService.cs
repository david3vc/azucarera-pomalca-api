using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.EmpleadoCursos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoCursoService : ICrudService<EmpleadoCursoDto, EmpleadoCursoSaveDto, int>
    {
    }
}
