using PruebaTecnica_Cifrado_Homomorfico.Modelos;
using PruebaTecnica_Cifrado_Homomorfico.Repository;
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
    /// Interaction logic for Consultas.xaml
    /// </summary>
    public partial class Consultas : Window
    {
        public Consultas()
        {
            InitializeComponent();
        }

        private void buscar_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Clientes>();
            var consulta = new ClientesRepository();


            listado = consulta.GetClientes();

            DatosDataGrid.ItemsSource = listado;
        }
    }
}
