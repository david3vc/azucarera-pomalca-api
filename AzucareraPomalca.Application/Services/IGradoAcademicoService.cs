using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.GradosAcademicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services
{
    public interface IGradoAcademicoService : IListService<GradoAcademicoSimpleDto>
    {
    }
}
