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
        private readonly EncriptadoService encriptar;

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

                MessageBox.Show($"{encriptar.Desencriptar(id)}\n{encriptar.Desencriptar(cedula)}\n"+
                    $"{encriptar.Desencriptar(nombres)}\n{encriptar.Desencriptar(direccion)}\n"+
                    $"{encriptar.Desencriptar(telefono)}\n{encriptar.Desencriptar(limite)}", "Entidad descriptada");
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

            try
            {
                var listaOriginal = contexto.Clientes.ToList();

                var listaDesencriptada = new List<Clientes>();

                foreach (var cliente in listaOriginal)
                {
                    var clienteDesencriptado = new Clientes
                    {
                        IdCliente = encriptar.Desencriptar(cliente.IdCliente),
                        CedulaSerial = encriptar.Desencriptar(cliente.CedulaSerial),
                        Nombres = encriptar.Desencriptar(cliente.Nombres),
                        Direccion = encriptar.Desencriptar(cliente.Direccion),
                        Telefono = encriptar.Desencriptar(cliente.Telefono),
                        LimiteCredito = encriptar.Desencriptar(cliente.LimiteCredito),
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

        public static Clientes buscar()
        {
            Contexto contexto = new Contexto();
            var cliente = new Clientes();
            try
            {
                 cliente = contexto.Clientes.First();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return cliente;
        }
    }
}
