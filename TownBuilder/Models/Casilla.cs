using System.ComponentModel;
using Ikc5.TypeLibrary;
using TownBuilder.Converters;

namespace TownBuilder.Models
{
	public class Casilla : BaseNotifyPropertyChanged
	{
        private CasillasEstados _state;

        [DefaultValue(0)]
        public CasillasEstados State
        {
            get => _state;
            set => SetProperty(ref _state, value);
        }
        
        public int Heigh { get; set; }
        public int Width { get; set; }

        public Carta Carta { get; set; }
		public Casilla(int heigh, int width)
		{
			this.SetDefaultValues();
            Heigh=heigh;
            Width=width;
		}
    }
}
