using System.Windows.Input;
using Ikc5.TypeLibrary;
using TownBuilder.Helppers;
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

        private bool _startAnimation;
        public bool StartAnimation
        {
            get => _startAnimation;
            set => SetProperty(ref _startAnimation, value);
        }

        private bool _starAnimation;
        public bool StarAnimation
        {
            get => _starAnimation;
            set => SetProperty(ref _starAnimation, value);
        }
        private bool _errorEnergia;
        public bool ErrorEnergia
        {
            get => _errorEnergia;
            set => SetProperty(ref _errorEnergia, value);
        }       
        private bool _errorTrabajadores;
        public bool ErrorTrabajadores
        {
            get => _errorTrabajadores;
            set => SetProperty(ref _errorTrabajadores, value);
        }
        private bool _errorNoCarta;

        public bool ErrorNoCarta
        {
            get => _errorNoCarta;
            set => SetProperty(ref _errorNoCarta, value);
        }

        private bool _errorNoDinero;
        public bool ErrorNoDinero
        {
            get => _errorNoDinero;
            set => SetProperty(ref _errorNoDinero, value);
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
            var error = 0;
            if (_parent.ModoDestruir)
            {
                if (Cell.Carta!=null)
                {
                    _parent.Destruir(Cell.Heigh, Cell.Width);
                }
            }
            else
            {
                if (Cell.State == CasillasEstados.Comprado && Cell.Carta == null)
                {
                    error=_parent.AplicarCarta(Cell.Heigh, Cell.Width);
                }

                if (Cell.State == CasillasEstados.Vacio)
                {
                    error=_parent.Comprar(Cell.Heigh, Cell.Width);
                }
            }

            MostrarErrores(error);
        }

        private void MostrarErrores(int tipo)
        {
            ErrorEnergia = false;
            ErrorTrabajadores = false;
            ErrorNoCarta = false;
            ErrorNoDinero = false;
            if (_parent.Error)
            {
                if (tipo == 1)
                {
                    ErrorEnergia = true;
                }
                if (tipo == 2)
                {
                    ErrorTrabajadores = true;
                }
                if (tipo == 3)
                {
                    ErrorNoCarta = true;
                }
                if (tipo == 4)
                {
                    ErrorNoDinero = true;
                }
                
                
                StartAnimation = true;
                StartAnimation = false;
                _parent.Error = false;
            }
        }
        internal void MostrarCombo()
        {
            StarAnimation = true;
            StarAnimation = false;
        }
        private static bool CanChangeCellState(object obj)
		{
			return true;
		}

		#endregion

	}
}
