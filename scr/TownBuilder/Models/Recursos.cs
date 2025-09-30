using TownBuilder.Helppers;

namespace TownBuilder.Models
{
    public class Recursos
    {
        public string Nombre { get; set; }
        public int Importe { get; set; }
        public RecursosTipos Recurso { get; set; }
    }
}
