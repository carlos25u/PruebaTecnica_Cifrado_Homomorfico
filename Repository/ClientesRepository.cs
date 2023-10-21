using Microsoft.Research.SEAL;
using PruebaTecnica_Cifrado_Homomorfico.DAL;
using PruebaTecnica_Cifrado_Homomorfico.Modelos;
using PruebaTecnica_Cifrado_Homomorfico.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PruebaTecnica_Cifrado_Homomorfico.Repository
{
    internal class ClientesRepository
    {
        private EncriptadoService encriptar;

        public ClientesRepository()
        {
            encriptar = new EncriptadoService();
        }

        public bool guardar(Clientes clientes)
        {
            bool paso = false;
            var contexto = new Contexto();

            try
            {
                var id = encriptar.Encriptar(clientes.IdCliente);
                var cedula = encriptar.Encriptar(clientes.CedulaSerial);
                var nombres = encriptar.Encriptar(clientes.Nombres);
                var direccion = encriptar.Encriptar(clientes.Direccion);
                var telefono = encriptar.Encriptar(clientes.Telefono);
                var limite = encriptar.Encriptar(clientes.LimiteCredito);

                var clientesGuardar = new Clientes()
                {
                    IdCliente = id,
                    CedulaSerial = cedula,
                    Nombres = nombres,
                    Direccion = direccion,
                    Telefono = telefono,
                    LimiteCredito = limite,
                };

                contexto.Clientes.Add(clientesGuardar);
                paso = contexto.SaveChanges() > 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al encriptar: {ex.ToString()}");
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public List<Clientes> GetClientes()
        {
            var contexto = new Contexto();
            var listaOriginal = new List<Clientes>();

            try
            {
                listaOriginal = contexto.Clientes.ToList();

                var listaDesencriptada = new List<Clientes>();

                foreach (var cliente in listaOriginal)
                {
                    var clienteDesencriptado = new Clientes
                    {
                        IdCliente = cliente.IdCliente,
                        CedulaSerial = encriptar.Desencriptar(cliente.CedulaSerial),
                        Nombres = encriptar.Desencriptar(cliente.Nombres),
                        Direccion = encriptar.Desencriptar(cliente.Direccion),
                        Telefono = encriptar.Desencriptar(cliente.Telefono),
                        LimiteCredito = cliente.LimiteCredito
                    };

                    listaDesencriptada.Add(clienteDesencriptado);
                }

                return listaDesencriptada;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
        }
    }
}
