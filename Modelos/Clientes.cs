using Microsoft.Research.SEAL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Cifrado_Homomorfico.Modelos
{
    internal class Clientes
    {
        [Key] 
        public String? IdCliente { get; set; }
        public String? CedulaSerial { get; set; }
        public String? Nombres { get; set; }
        public String? Direccion { get; set; }
        public String? Telefono { get; set; }
        public String? LimiteCredito { get; set; }
    }
}
