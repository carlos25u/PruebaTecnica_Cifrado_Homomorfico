using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Cifrado_Homomorfico.Service
{
    internal class DependencyContainer
    {
        private static EncriptadoService encriptadoService;

        public static EncriptadoService GetEncriptadoService()
        {
            if (encriptadoService == null)
            {
                encriptadoService = new EncriptadoService();
            }
            return encriptadoService;
        }

    }
}
