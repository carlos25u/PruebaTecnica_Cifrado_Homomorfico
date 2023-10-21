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

            Console.WriteLine("debugger");

        }

        private void limpiar_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void guardar_Click(object sender, RoutedEventArgs e)
        {
             ClientesRepository cliente = new ClientesRepository();

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

        }

        private void Limpiar()
        {
            this.clientes = new Clientes();
            this.DataContext = clientes;
        }
    }
}
