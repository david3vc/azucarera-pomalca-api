using AutoMapper;
using AzucareraPomalca.Domain.Models;

namespace AzucareraPomalca.Application.Dtos.CapacitacionesEmpleados.Profiles
{
    public class CapacitacionEmpleadoProfileAction : IMappingAction<CapacitacionEmpleado, CapacitacionEmpleadoDto>
    {
        public void Process(CapacitacionEmpleado source, CapacitacionEmpleadoDto destination, ResolutionContext context)
        {
            destination.NombreCompleto = $"{source.Empleado?.Nombres} {source.Empleado?.AppellidoPaterno} {source.Empleado?.AppellidoMaterno}";
            destination.Gerencia = source.Empleado?.Puesto?.Gerencia?.Nombre;
            destination.Division = source.Empleado?.Puesto?.Division?.Nombre;
            destination.Departamento = source.Empleado?.Puesto?.Departamento?.Nombre;
            destination.Seccion = source.Empleado?.Puesto?.Seccion?.Nombre;
            destination.Puesto = source.Empleado?.Puesto?.Nombre;
        }
    }
}
