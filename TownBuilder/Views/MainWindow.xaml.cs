using System.Windows;
using TownBuilder.ViewModels;

namespace TownBuilder.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
    {
        private GameViewModel _vm = new GameViewModel(18,30);

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
            DataContext = _vm;
        }

        private void Carta1_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(1);
        }
        
        private void Carta2_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(2);
        }
        
        private void Carta3_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(3);
        }
        
        private void Carta4_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(4);
        }
        
        private void Carta5_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(5);
        }
        
        private void Carta6_Click(object sender, RoutedEventArgs e)
        {
            _vm.SeleccionarCarta(6);
        }

        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            _vm.FinalizarTurno();
        }
    }
}
