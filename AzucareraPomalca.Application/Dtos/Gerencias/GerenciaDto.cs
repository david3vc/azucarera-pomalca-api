using AzucareraPomalca.Application.Dtos.Departamentos;
using AzucareraPomalca.Application.Dtos.Divisiones;

namespace AzucareraPomalca.Application.Dtos.Gerencias
{
    public class GerenciaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool State { get; set; }

        public List<DivisionDto>? Divisiones { get; set; }
        public List<DepartamentoDto>? Departamentos { get; set; }
    }
}
