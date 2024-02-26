using System.Collections.ObjectModel;
using TownBuilder.Models;
using TownBuilder.ViewModels;

namespace TownBuilder.Helppers
{
    public class RecursosHelper
    {
        internal static List<Recursos> RecursosValores()
        {
            var recursos = new List<Recursos>
            {
                new() { Nombre = "Leche", Importe = 5, Recurso = RecursosTipos.Leche },
                new() { Nombre = "Lana", Importe = 10, Recurso = RecursosTipos.Lana},
                new() { Nombre = "Huevos", Importe = 10, Recurso = RecursosTipos.Huevos},
                new() { Nombre = "Trigo", Importe = 10, Recurso = RecursosTipos.Trigo},
                new() { Nombre = "Maiz", Importe = 10, Recurso = RecursosTipos.Maiz},
                new() { Nombre = "Patata", Importe = 10, Recurso = RecursosTipos.Patatas},
                new() { Nombre = "Manzanas", Importe = 10, Recurso = RecursosTipos.Manzanas},
                new() { Nombre = "Tomates", Importe = 10, Recurso = RecursosTipos.Tomates},
                new() { Nombre = "Peras", Importe = 10, Recurso = RecursosTipos.Peras},
                new() { Nombre = "Peces", Importe = 10, Recurso = RecursosTipos.Peces},
                new() { Nombre = "Lino", Importe = 10, Recurso = RecursosTipos.Lino},
                new() { Nombre = "Madera", Importe = 10, Recurso = RecursosTipos.Madera},
                new() { Nombre = "Piedra", Importe = 10, Recurso = RecursosTipos.Piedra},
                new() { Nombre = "Metal", Importe = 10, Recurso = RecursosTipos.Metal},
                new() { Nombre = "Tela", Importe = 10, Recurso = RecursosTipos.Tela}
            };
            return recursos;
        }

        internal static List<RecursosCartas> GetTotales(ObservableCollection<ObservableCollection<CasillasViewModel>> cells)
        {
            var cartaList = new List<Carta>();
            foreach (var cellList in cells)
            {
                cartaList.AddRange(cellList.Where(e => e.Cell.Carta != null && e.Cell.Carta.Tipo!= CartasTipos.Casas).Select(e=> e.Cell.Carta).ToList()!);
            }
            
            var recursosValores = RecursosValores();

            var recursosCarta = new List<RecursosCartas>();

            foreach (var carta in cartaList)
            {
                var cantidad = carta.Combo ? carta.RecursoCantidad * Constantes.ImporteIncremento: carta.RecursoCantidad;
                if (recursosCarta.Any(e => e.Recursos.Recurso == carta.Recurso))
                {
                    recursosCarta.Where(e => e.Recursos.Recurso == carta.Recurso).ToList()[0].Cantidad += cantidad;
                }
                else
                {
                    var recurso = new RecursosCartas
                    {
                        Recursos = recursosValores.Where(e=> e.Recurso == carta.Recurso).ToList()[0],
                        Cantidad = cantidad
                    };
                    recursosCarta.Add(recurso);
                }
            }

            foreach (var recurso in recursosCarta)
            {
                recurso.Total = recurso.Cantidad * recurso.Recursos.Importe;
            }

            return recursosCarta;
        }
    }
}
