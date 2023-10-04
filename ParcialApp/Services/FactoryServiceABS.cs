using ParcialApp.Services.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParcialApp.Services
{
    internal abstract class FactoryServiceABS
    {
        public abstract IService GetService();
    }
}
