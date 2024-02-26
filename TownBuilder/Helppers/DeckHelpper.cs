using System.Collections.ObjectModel;
using TownBuilder.Models;

namespace TownBuilder.Helppers
{
    internal class DeckHelpper
    {

        internal static ObservableCollection<Carta?> GenerateDeck()
        {
            var deck = new ObservableCollection<Carta?>();
            var allCartas = AllCartas(0);
            for (var posRow = 0; posRow < Constantes.CartasMazo; posRow++)
            {
                var carta = allCartas.OrderBy(x => Guid.NewGuid()).First();
                carta.Id = Guid.NewGuid(); 
                deck.Add(carta);
            }

            var casas = deck.Count(e => e.Tipo == CartasTipos.Casas);
            if (casas > 2 || casas < 1)
                deck = GenerateDeck();
            return deck;
        }
        internal static ObservableCollection<Carta?> CartasRandom(ObservableCollection<Carta?> listaCartas)
        {
            foreach (var carta in listaCartas)
            {
                carta.Combo = false;
            }
            return new ObservableCollection<Carta?>(listaCartas.OrderBy(x => Guid.NewGuid()).Take(Constantes.CartasPorTurno));
        }

        internal static Carta GetBasicBuild()
        {
            return new() { Nombre = "Cabaña", Tipo = CartasTipos.Casas, Image = CartasImages.Casa1, Importe = 10, Level = 0, Trabajadores = 5 };
        }

        internal static List<Carta> AllCartas(int level)
        {
            var deck = new List<Carta>
            {
                new() { Nombre = "Vaca", Tipo = CartasTipos.Animal, Image = CartasImages.Vaca, Importe = 10, Level = 0 , Trabajadores = -1, Recurso = RecursosTipos.Leche},
                new() { Nombre = "Oveja", Tipo = CartasTipos.Animal, Image = CartasImages.Oveja, Importe = 10, Level = 0 , Trabajadores = -1, Recurso = RecursosTipos.Lana},
                new() { Nombre = "Gallinas",Tipo = CartasTipos.Animal,Image = CartasImages.Pollo, Importe = 10, Level=0, Trabajadores = -1, Recurso = RecursosTipos.Huevos},
                new() { Nombre = "Trigo",Tipo = CartasTipos.Cultivo, Image = CartasImages.Trigo, Importe = 10, Level=0, Trabajadores = -1, Recurso = RecursosTipos.Trigo},
                new() { Nombre = "Maiz",Tipo = CartasTipos.Cultivo,Image = CartasImages.Maiz, Importe = 10, Level=0, Trabajadores = -1, Recurso = RecursosTipos.Maiz},
                new() { Nombre = "Patata",Tipo = CartasTipos.Cultivo,Image = CartasImages.Patata, Importe = 10, Level=0, Trabajadores = -1, Recurso = RecursosTipos.Patatas},
                new() { Nombre = "Cabaña",Tipo = CartasTipos.Casas,Image = CartasImages.Casa1, Importe = 10, Level=0, Trabajadores = 5},
                new() { Nombre = "Casa",Tipo = CartasTipos.Casas,Image = CartasImages.Casa2, Importe = 10, Level=1, Trabajadores = 10},
                new() { Nombre = "edificio",Tipo = CartasTipos.Casas,Image = CartasImages.Casa3, Importe = 10, Level=2, Trabajadores = 15}
            };
            return deck.Where(e=> e.Level<=level).ToList();
        }
    }
}
