using System.Collections.ObjectModel;
using System.Windows;
using TownBuilder.Models;

namespace TownBuilder.Views
{
    /// <summary>
    /// Interaction logic for DeckView.xaml
    /// </summary>
    public partial class DeckView : Window
    {
        public Deck Deck = new Deck();

        public DeckView(ObservableCollection<Carta?> listaCartas)
        {
            InitializeComponent();
            Deck.ListaCartas = new ObservableCollection<Carta>(listaCartas.OrderBy(e=> e.Tipo));
            DataContext = Deck;
        }

    }

    public class Deck
    {
        public ObservableCollection<Carta> ListaCartas { get; set; }
    }
}
