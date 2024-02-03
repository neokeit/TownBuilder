using System.Windows.Input;
using Ikc5.TypeLibrary;
using TownBuilder.Converters;
using TownBuilder.Models;

namespace TownBuilder.ViewModels
{
	public class CasillasViewModel : BaseNotifyPropertyChanged
    {
        private readonly GameViewModel _parent;
		public CasillasViewModel(Casilla cell, GameViewModel parent)
		{
			ChangeCellStateCommand = new DelegateCommand(ChangeCellState, CanChangeCellState);
			if (cell != null)
			{
				Cell = cell;
			}

            _parent = parent;
        }

        private string _importe;

        public string Importe
        {
            get => _importe;
            set => SetProperty(ref _importe, value);
        }

        private string _tipo;
        public string Tipo
        {
            get => _tipo;
            set => SetProperty(ref _tipo, value);
        }

        private string _tipoDescripcion;
        public string TipoDescripcion
        {
            get => _tipoDescripcion;
            set => SetProperty(ref _tipoDescripcion, value);
        }
        

		#region Cell model

		private Casilla _cell;

		public Casilla Cell
		{
			get => _cell;
            set => SetProperty(ref _cell, value);
        }

		#endregion Cell model

		#region Commands

		public ICommand ChangeCellStateCommand { get; }

        private void ChangeCellState(object obj)
		{
            if (Cell.State == CasillasEstados.Vacio)
            {
                _parent.Comprar(Cell.Heigh,Cell.Width);
            }
            if (Cell.State == CasillasEstados.Comprado && Cell.Carta!=null)
            {
                _parent.AplicarCarta(Cell.Heigh,Cell.Width);
            }
        }

		private static bool CanChangeCellState(object obj)
		{
			return true;
		}

		#endregion

	}
}
