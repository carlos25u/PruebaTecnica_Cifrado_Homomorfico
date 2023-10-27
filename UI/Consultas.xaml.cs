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


            if (criterio.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        listado = consulta.GetListCriterio(e => e.IdCliente.ToLower().Contains(criterio.Text));
                        break;

                    case 1:
                        listado = consulta.GetListCriterio(e => e.CedulaSerial.ToLower().Contains(criterio.Text));
                        break;
                    case 2:
                        listado = consulta.GetListCriterio(e => e.Nombres.ToLower().Contains(criterio.Text));
                        break;
                    case 3:
                        listado = consulta.GetListCriterio(e => e.Direccion.ToLower().Contains(criterio.Text));
                        break;
                    case 4:
                        listado = consulta.GetListCriterio(e => e.Telefono.ToLower().Contains(criterio.Text));
                        break;
                    case 5:
                        listado = consulta.GetListCriterio(e => e.LimiteCredito.ToLower().Contains(criterio.Text));
                        break;
                }
            }
            else
            {
                listado = consulta.GetClientes();
            }

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}
