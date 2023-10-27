using Microsoft.Research.SEAL;
using PruebaTecnica_Cifrado_Homomorfico.DAL;
using PruebaTecnica_Cifrado_Homomorfico.Modelos;
using PruebaTecnica_Cifrado_Homomorfico.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            encriptar = DependencyContainer.GetEncriptadoService(); ;
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

        public Clientes buscar(String id)
        {
            var contexto = new Contexto();
            var cliente = new Clientes();
            var clienteDecifrado = new Clientes();
            var clientesCifrado = contexto.Clientes.ToList();

            try
            {
                cliente = clientesCifrado.FirstOrDefault(c => encriptar.Desencriptar(c.IdCliente) == id);

                if (cliente != null)
                {
                    clienteDecifrado.IdCliente = encriptar.Desencriptar(cliente.IdCliente);
                    clienteDecifrado.CedulaSerial = encriptar.Desencriptar(cliente.CedulaSerial);
                    clienteDecifrado.Nombres = encriptar.Desencriptar(cliente.Nombres);
                    clienteDecifrado.Direccion = encriptar.Desencriptar(cliente.Direccion);
                    clienteDecifrado.Telefono = encriptar.Desencriptar(cliente.Telefono);
                    clienteDecifrado.LimiteCredito = encriptar.Desencriptar(cliente.LimiteCredito);
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return clienteDecifrado;
        }

        public bool eliminar(String id)
        {
            var contexto = new Contexto();
            bool paso = false;
            var cliente = new Clientes();
            var clienteDecifrado = new Clientes();
            var clientesCifrado = contexto.Clientes.ToList();

            try
            {
                cliente = clientesCifrado.FirstOrDefault(c => encriptar.Desencriptar(c.IdCliente) == id);

                if (cliente != null)
                {
                    contexto.Clientes.Remove(cliente);
                    paso = contexto.SaveChanges() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }

            return paso;
        }

        public List<Clientes> GetListCriterio(Expression<Func<Clientes, bool>> criterio)
        {
            var listaDecencriptrada = new List<Clientes>();
            Contexto contexto = new Contexto();
            try
            {
                // Recupera la lista cifrada de la base de datos
                var listaCifrada = contexto.Clientes.ToList();

                // Itera sobre cada elemento cifrado y desencripta los valores relevantes
                foreach (var clienteCifrado in listaCifrada)
                {
                    var clienteDesencriptado = new Clientes
                    {
                        IdCliente = encriptar.Desencriptar(clienteCifrado.IdCliente),
                        CedulaSerial = encriptar.Desencriptar(clienteCifrado.CedulaSerial),
                        Nombres = encriptar.Desencriptar(clienteCifrado.Nombres),
                        Direccion = encriptar.Desencriptar(clienteCifrado.Direccion),
                        Telefono = encriptar.Desencriptar(clienteCifrado.Telefono),
                        LimiteCredito = encriptar.Desencriptar(clienteCifrado.LimiteCredito),
                    };

                    // Aplica el criterio a los datos desencriptados
                    if (criterio.Compile().Invoke(clienteDesencriptado))
                    {
                        listaDecencriptrada.Add(clienteDesencriptado);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return listaDecencriptrada;
        }
    }
}
