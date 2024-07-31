using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.GradoDominios;

namespace AzucareraPomalca.Application.Services
{
    public interface IGradoDominioService : ICrudService<GradoDominioDto, GradoDominioSaveDto, int>, IPageService<GradoDominioDto, GradoDominioFilterDto>
    {
        Task<GradoDominioDto> FindByNivelAndIdCompetenciaAsync(GradoDominioFilterDto request);
    }
}
