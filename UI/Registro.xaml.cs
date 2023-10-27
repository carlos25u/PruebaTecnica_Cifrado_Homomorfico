using Microsoft.Research.SEAL;
using PruebaTecnica_Cifrado_Homomorfico.Modelos;
using PruebaTecnica_Cifrado_Homomorfico.Repository;
using PruebaTecnica_Cifrado_Homomorfico.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PruebaTecnica_Cifrado_Homomorfico.UI
{
    /// <summary>
    /// Interaction logic for Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        private Clientes clientes = new Clientes();
        public Registro()
        {
            InitializeComponent();
            this.DataContext = clientes;
        }

        private void buscar_Click(object sender, RoutedEventArgs e)
        {
            var clienteBuscado = new ClientesRepository();

            var cliente = clienteBuscado.buscar(Id.Text);

            if (cliente != null)
            {
                this.clientes = cliente;
            }
            else
            {
                this.clientes = new Clientes();
                MessageBox.Show("No encontrado", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            this.DataContext = clientes;

        }

        private void limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
             ClientesRepository cliente = new ClientesRepository();

            if (!validar())
                return;

             var paso = cliente.guardar(clientes);

             if (paso)
             {
                 Limpiar();
                 MessageBox.Show("Guardado con exito", "Exito",
                     MessageBoxButton.OK, MessageBoxImage.Information);
             }
             else
             {
                 MessageBox.Show("No se pudo guardar", "Fallo",
                     MessageBoxButton.OK, MessageBoxImage.Error);
             }
        }

        private void eliminar_Click(object sender, RoutedEventArgs e)
        {
            var cliente = new ClientesRepository();

            if (cliente.eliminar(Id.Text))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Exito",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No fue posible Eliminar", "Fallo",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Limpiar()
        {
            this.clientes = new Clientes();
            this.DataContext = clientes;
        }

        private bool validar()
        {
            String mensajeValidacion = "";

            if (string.IsNullOrWhiteSpace(Id.Text))
                mensajeValidacion = "El Id no puede estar vacio";

            if (string.IsNullOrWhiteSpace(cedula.Text))
                mensajeValidacion = "La Cedula no puede estar vacia";

            if (string.IsNullOrWhiteSpace(Nombres.Text))
                mensajeValidacion = "El Nombres no puede estar vacio";

            if (string.IsNullOrWhiteSpace(Direecion.Text))
                mensajeValidacion = "la Dirreccion no puede estar vacia";

            if (string.IsNullOrWhiteSpace(Telefono.Text))
                mensajeValidacion = "El Telefono no puede estar vacio";

            if (string.IsNullOrWhiteSpace(Limite.Text))
                mensajeValidacion = "El Limite de credito no puede estar vacio";

            if (mensajeValidacion.Length > 0)
                MessageBox.Show(mensajeValidacion, "Fallo", MessageBoxButton.OK, MessageBoxImage.Warning);
            

            return mensajeValidacion.Length == 0;
        }
    }
}
