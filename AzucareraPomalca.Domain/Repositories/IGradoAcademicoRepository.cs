using AzucareraPomalca.Domain.Cores.Repositories;
using AzucareraPomalca.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Domain.Repositories
{
    public interface IGradoAcademicoRepository : ICrudRepository<GradoAcademico, int>
    {
    }
}
