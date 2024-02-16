using AzucareraPomalca.Application.Dtos.GradoDominios;

namespace AzucareraPomalca.Application.Services
{
    public interface IGradoDominioService
    {
        Task<GradoDominioDto> FindByNivelAndIdCompetenciaAsync(GradoDominioFilter request);
    }
}
