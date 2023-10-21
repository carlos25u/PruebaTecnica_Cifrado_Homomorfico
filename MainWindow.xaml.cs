using PruebaTecnica_Cifrado_Homomorfico.UI;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PruebaTecnica_Cifrado_Homomorfico
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RegistroCliente_Click(object sender, RoutedEventArgs e)
        {
            Registro r = new Registro();
            r.Show();
        }

        private void ConsultasClientes_Click(object sender, RoutedEventArgs e)
        {
            new Consultas().Show();
        }
    }
}
