using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using Ikc5.TypeLibrary;
using TownBuilder.Converters;
using TownBuilder.Models;

namespace TownBuilder.ViewModels
{
    public class GameViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<CasillasViewModel>> _cells = null!;
        private int _gridWidth;
        private int _gridHeight;

        private Color _bloqueadoColor;
        private Color _vacioColor;
        private Color _compradoColor;
        private int _importeBase = 10;
        private readonly double _importeIncremento = 1.25;
        private int _importeCobro = 100;
        private Carta _seleccionado = null;

        [DefaultValue(typeof(Color), "#FFF0F8FF")]
        public Color BloqueadoColor
        {
            get => _bloqueadoColor;
            set => SetProperty(ref _bloqueadoColor, value);
        }

        [DefaultValue(typeof(Color), "#FF1E90FF")]
        public Color VacioColor
        {
            get => _vacioColor;
            set => SetProperty(ref _vacioColor, value);
        }
        
        [DefaultValue(typeof(Color), "#FFCD853F")]
        public Color CompradoColor
        {
            get => _compradoColor;
            set => SetProperty(ref _compradoColor, value);
        }

        public int GridWidth
        {
            get => _gridWidth;
            set => SetProperty(ref _gridWidth, value);
        }

        public int GridHeight
        {
            get => _gridHeight;
            set => SetProperty(ref _gridHeight, value);
        }
        
        private int _dinero = 10000;
        public int Dinero
        {
            get => _dinero;
            set => SetProperty(ref _dinero, value);
        }
        private int _dia = 1;
        public int Dia
        {
            get => _dia;
            set => SetProperty(ref _dia, value);
        }
        private int _cartasCount = 30;
        public int CartasCount
        {
            get => _cartasCount;
            set => SetProperty(ref _cartasCount, value);
        }

        private ObservableCollection<string> _dineroHistorico = new ObservableCollection<string>();
        public ObservableCollection<string> DineroHistorico
        {
            get => _dineroHistorico;
            set => SetProperty(ref _dineroHistorico, value);
        }
        public ObservableCollection<ObservableCollection<CasillasViewModel>> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }
        
        public GameViewModel(int height, int width)
        {
            this.SetDefaultValues();
            GridHeight = height;
            GridWidth = width;
            CreateCells();
        }

        private void CreateCells()
        {
            Cells = new ObservableCollection<ObservableCollection<CasillasViewModel>>();
            for (var posRow = 0; posRow < GridHeight; posRow++)
            {
                var row = new ObservableCollection<CasillasViewModel>();
                for (var posCol = 0; posCol < GridWidth; posCol++)
                {
                    var casillasViewModel = new CasillasViewModel(new Casilla(posRow, posCol), this);
                    row.Add(casillasViewModel);
                }
                Cells.Add(row);
            }
            SetInitialCasillas();
        }

        private void SetInitialCasillas()
        {
            var halfHeight = GridHeight / 2;
            var halfWidth = GridWidth / 2;
            Cells[halfHeight][halfWidth].Cell.State = CasillasEstados.Comprado;
            Recalcule(halfHeight, halfWidth);
        }

        public void Recalcule(int cellHeigh, int cellWidth)
        {
            SetStateComprable(cellHeigh - 1, cellWidth);
            SetStateComprable(cellHeigh + 1, cellWidth);
            SetStateComprable(cellHeigh, cellWidth - 1);
            SetStateComprable(cellHeigh, cellWidth + 1);
            foreach (var cellList in Cells)
            {
                foreach (var model in cellList)
                {
                    if (model.Cell.State == CasillasEstados.Vacio)
                    {
                        model.Importe = _importeBase + " $";
                    }
                }
            }
        }

        private void SetStateComprable(int cellHeigh, int cellWidth)
        {
            if (cellWidth < 0) return;
            if (cellHeigh < 0) return;
            if (Cells.Count <= cellHeigh) return;
            if (Cells[cellHeigh].Count <= cellWidth) return;
            if (Cells[cellHeigh][cellWidth].Cell.State == CasillasEstados.Bloqueado)
            {
                Cells[cellHeigh][cellWidth].Cell.State = CasillasEstados.Vacio;
            }
        }

        public void Comprar(int cellHeigh, int cellWidth)
        {
            if (_importeBase < _dinero)
            {
                Cobrar(_importeBase);
                IncrementarImporte();
                Cells[cellHeigh][cellWidth].Cell.State = CasillasEstados.Comprado;
                Cells[cellHeigh][cellWidth].Importe = "";
                Recalcule(cellHeigh, cellWidth);
            }
        }

        private void IncrementarImporte()
        {
            _importeBase = (int)(_importeBase * _importeIncremento);
        }

        public void FinalizarTurno()
        {
            Cobrar(_importeCobro);
            _importeCobro = (int)(_importeCobro * _importeIncremento);
            Dia++;
        }

        public void SeleccionarCarta(int i)
        {
            switch (i)
            {
                case 1:
                    _seleccionado = new Carta() { Nombre = "Vaca", Tipo = "Vaca" };
                    break;
                case 2:
                    _seleccionado = new Carta() { Nombre = "Trigo", Tipo = "Trigo" };
                    break;
                case 3:
                    _seleccionado = new Carta() { Nombre = "Gallina", Tipo = "Gallina" };
                    break;
                case 4:
                    _seleccionado = new Carta() { Nombre = "Toro", Tipo = "Toro" };
                    break;
                case 5:
                    _seleccionado = new Carta() { Nombre = "Casa", Tipo = "Casa" };
                    break;
                case 6:
                    _seleccionado = new Carta() { Nombre = "Silo", Tipo = "Silo" };
                    break;
                default:
                    _seleccionado = null;
                    break;
            }
        }

        private void Cobrar(int cobro)
        {
            var dineroRestate =Dinero-cobro;
            DineroHistorico.Add(Dinero + "-" + cobro + ": " + dineroRestate);
            Dinero = dineroRestate;
        }

        public void AplicarCarta(int cellHeigh, int cellWidth)
        {
            if (_seleccionado!=null)
            {
                Cobrar(_seleccionado.Importe);
                Cells[cellHeigh][cellWidth].Cell.Carta = _seleccionado;
                _seleccionado = null;
            }
        }
    }
}