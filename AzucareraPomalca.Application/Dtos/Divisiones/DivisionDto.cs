using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Gerencias;
using AzucareraPomalca.Application.Dtos.Secciones;

namespace AzucareraPomalca.Application.Dtos.Divisiones
{
    public class DivisionDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public GerenciaSimpleDto? Gerencia { get; set; }
        public List<DepartamentoDto>? Departamentos { get; set; }
        public List<SeccionDto>? Secciones { get; set; }
    }
}
