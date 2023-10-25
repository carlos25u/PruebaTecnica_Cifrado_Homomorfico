using Microsoft.Research.SEAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Cifrado_Homomorfico.Resource
{
    [Serializable]
    internal class Claves
    {
        public SecretKey SecretKey { get; set; }
        public PublicKey PublicKey { get; set; }
    }
}
