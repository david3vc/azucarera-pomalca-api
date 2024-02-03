using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.EmpleadoProfesiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services
{
    public interface IEmpleadoProfesionService : ICrudService<EmpleadoProfesionDto, EmpleadoProfesionSaveDto, int>
    {
    }
}
