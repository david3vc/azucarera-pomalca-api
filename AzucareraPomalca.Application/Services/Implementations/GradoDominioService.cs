using AutoMapper;
using AzucareraPomalca.Application.Cores.Exceptions;
using AzucareraPomalca.Application.Dtos.GradoDominios;
using AzucareraPomalca.Domain.Models;
using AzucareraPomalca.Domain.Repositories;
using System.Linq.Expressions;

namespace AzucareraPomalca.Application.Services.Implementations
{
    public class GradoDominioService : IGradoDominioService
    {
        private readonly IMapper _mapper;
        private readonly IGradoDominioRepository _gradoDominioRepository;

        public GradoDominioService(IMapper mapper, IGradoDominioRepository gradoDominioRepository)
        {
            _mapper = mapper;
            _gradoDominioRepository = gradoDominioRepository;
        }

        public async Task<GradoDominioDto> FindByNivelAndIdCompetenciaAsync(GradoDominioFilter request)
        {
            Expression<Func<GradoDominio, bool>> predicate = x => x.Nivel == request.Nivel && x.IdCompetencia == request.IdCompetencia;

            List<Expression<Func<GradoDominio, object>>>? includes = new List<Expression<Func<GradoDominio, object>>>()
            {
                t => t.CompetenciaSimple.TipoCompetencia
            };

            GradoDominio? gradoDominio = await _gradoDominioRepository.FindByIdAsync(predicate: predicate, includes: includes);

            if (gradoDominio is null) throw GradoDominioNotFound(request);

            return _mapper.Map<GradoDominioDto>(gradoDominio);
        }

        private NotFoundCoreException GradoDominioNotFound(GradoDominioFilter request)
        {
            return new NotFoundCoreException("Grado Dominio no encontrado para el niel: " + request.Nivel + " e id_competencia: idCompetencia: " + request.IdCompetencia);
        }
    }
}
