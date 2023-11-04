using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Divisiones;
using AzucareraPomalca.Application.Dtos.Gerencias;

namespace AzucareraPomalca.Application.Dtos.Secciones
{
    public class SeccionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public GerenciaSimpleDto? Gerencia { get; set; }
        public DivisionSimpleDto? Division { get; set; }
        public DepartamentoSimpleDto? Departamento { get; set; }
    }
}
