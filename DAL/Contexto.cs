using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Research.SEAL;
using Newtonsoft.Json;
using PruebaTecnica_Cifrado_Homomorfico.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica_Cifrado_Homomorfico.DAL
{
    internal class Contexto: DbContext
    {
        public DbSet<Clientes> Clientes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source = CARLOS25U\\SQLEXPRESS; Initial Catalog=GestionClientesDb; TrustServerCertificate= true; Trusted_Connection = True");
        }
    }
}
