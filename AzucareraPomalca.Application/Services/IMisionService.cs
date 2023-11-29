using AzucareraPomalca.Application.Cores.Services;
using AzucareraPomalca.Application.Dtos.Misiones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzucareraPomalca.Application.Services
{
    public interface IMisionService : ICrudService<MisionDto, MisionSaveDto, int>
    {
    }
}
