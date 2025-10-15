using System.Windows;
using TownBuilder.Helppers;
using TownBuilder.Models;
using TownBuilder.ViewModels;

namespace TownBuilder.Views
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
    {
        private GameViewModel _vm;
        private ConfigModel _config;
        public MainWindow()
        {
            _config= ConfigHelper.Load();
            InitializeComponent();
            WindowState = WindowState.Maximized;
            Initializer();
        }       

        private void Initializer()
        {
            _vm = new(_config);
            _vm.EndGame = EndGame_Click;
            DataContext = _vm;
        }

        private void EndTurn_Click(object sender, RoutedEventArgs e)
        {
            _vm.FinalizarTurno();
        }
    
        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            Tutorial view = new Tutorial();
            view.Show();
        }
        private void EndGame_Click(object sender, RoutedEventArgs e)
        {
            EndGameView view = new EndGameView();
            view.ShowDialog();
            Initializer();
        }  
        private void Deck_Click(object sender, RoutedEventArgs e)
        {
            DeckView view = new DeckView(_vm.ListaCartas);
            view.ShowDialog();
        }
        private void Sound_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.SoundActive)
            {
                _vm.SoundOff();
            }
            else
            {
                _vm.SoundOn();
            }
        }
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            DeckView view = new DeckView(_vm.ListaCartas);
            view.ShowDialog();
        }
    }
}
