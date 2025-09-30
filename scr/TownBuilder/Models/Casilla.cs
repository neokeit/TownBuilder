using System.ComponentModel;
using Ikc5.TypeLibrary;
using TownBuilder.Helppers;

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

        private CasillasEmptyTypes _emptyType;
        [DefaultValue(0)]
        public CasillasEmptyTypes EmptyType
        {
            get => _emptyType;
            set => SetProperty(ref _emptyType, value);
        }
              

        public int Heigh { get; set; }
        public int Width { get; set; }

        private Carta? _carta;
        public Carta? Carta
        {
            get => _carta;
            set => SetProperty(ref _carta, value);
        }

        public Casilla(int heigh, int width, CasillasEmptyTypes emptyValue)
		{
			this.SetDefaultValues();
            Heigh=heigh;
            Width=width;
            _emptyType = emptyValue;
        }
    }
}
