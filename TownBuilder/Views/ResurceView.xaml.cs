using System.Collections.ObjectModel;
using System.Windows;
using TownBuilder.Models;

namespace TownBuilder.Views
{
    /// <summary>
    /// Interaction logic for ResurceView.xaml
    /// </summary>
    public partial class ResurceView : Window
    {
        public model Deck = new model();

        public ResurceView(List<RecursosCartas> listaRecursos, int dineroGanado)
        {
            InitializeComponent();
            Deck.ListaRecursos = new ObservableCollection<RecursosCartas>(listaRecursos);
            Deck.Total = dineroGanado;
            DataContext = Deck;
        }
        
    }

    public class model
    {
        public ObservableCollection<RecursosCartas> ListaRecursos { get; set; }
        public int Total { get; set; }
    }
}
