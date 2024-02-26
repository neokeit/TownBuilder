using System.Collections.ObjectModel;
using System.Windows;
using TownBuilder.Helppers;
using TownBuilder.Models;

namespace TownBuilder.Views
{
    /// <summary>
    /// Interaction logic for MejorasView.xaml
    /// </summary>
    public partial class MejorasView : Window
    {
        public Mejoras mejoras = new Mejoras();


        public MejorasView(int level, ObservableCollection<Carta> deck)
        {
            InitializeComponent();
            
            var allCartas = DeckHelpper.AllCartas(level);
            mejoras.ListaCartasNuevas = new ObservableCollection<Carta>
            {
                allCartas.OrderBy(x => Guid.NewGuid()).First(),
                allCartas.OrderBy(x => Guid.NewGuid()).First(),
                allCartas.OrderBy(x => Guid.NewGuid()).First()
            };
            
            mejoras.DestruirCarta = new ObservableCollection<Carta>(deck.OrderBy(e=> e.Tipo));;
            mejoras.ListaMejoras = mejoras.ListaCartasNuevas;
            DataContext = mejoras;
        }

        private void NuevasCartas_Click(object sender, RoutedEventArgs e)
        {
            SelectorBotones.Visibility = Visibility.Collapsed;
            lsNuevasCartas.Visibility = Visibility.Visible;
        }

        private void Mejora_Click(object sender, RoutedEventArgs e)
        {
            SelectorBotones.Visibility = Visibility.Collapsed;
            lsMejoras.Visibility = Visibility.Visible;
        }
        private void Destruir_Click(object sender, RoutedEventArgs e)
        {
            SelectorBotones.Visibility = Visibility.Collapsed;
            lsDestruirCartas.Visibility = Visibility.Visible;
        }

        private void Atras_Click(object sender, RoutedEventArgs e)
        {
            SelectorBotones.Visibility = Visibility.Visible;
            lsNuevasCartas.Visibility = Visibility.Collapsed;
            lsMejoras.Visibility = Visibility.Collapsed;
            lsDestruirCartas.Visibility = Visibility.Collapsed;
            mejoras.Mejora = null;
            mejoras.Eliminar = null;
            mejoras.Añadir = null;
        }
    }

    public class Mejoras
    {
        public ObservableCollection<Carta> ListaCartasNuevas { get; set; }
        public ObservableCollection<Carta> ListaMejoras { get; set; }
        public ObservableCollection<Carta> DestruirCarta { get; set; }
        
        public Carta? Mejora { get; set; } = null;
        public Carta? Eliminar { get; set; }= null;
        public Carta? Añadir { get; set; } = null;
    }
}
