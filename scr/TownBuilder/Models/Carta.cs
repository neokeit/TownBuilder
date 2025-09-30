using Ikc5.TypeLibrary;
using TownBuilder.Helppers;

namespace TownBuilder.Models
{
    public class Carta: BaseNotifyPropertyChanged, ICloneable
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = "";
        public CartasTipos Tipo { get; set; }
        public CartasImages Image { get; set; }
        public RecursosTipos Recurso { get; set; }
        public double RecursoCantidad { get; set; }
        public int Importe { get; set; }
        public int Level { get; set; }
        private int _trabajadores;
        private bool _combo=false;
        public bool Combo
        {
            get => _combo;
            set => SetProperty(ref _combo, value);
        }
        public int Trabajadores
        {
            get => _trabajadores + (_trabajadores*_trabajadoresMod);
            set => _trabajadores = value;
        }
        
        private int _trabajadoresMod;

        public int TrabajadoresMod
        {
            get => _trabajadoresMod;
            set => _trabajadoresMod = value;
        }

        public bool Consumible { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
