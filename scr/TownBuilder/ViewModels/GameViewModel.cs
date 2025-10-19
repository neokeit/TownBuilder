using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Ikc5.TypeLibrary;
using TownBuilder.Core;
using TownBuilder.Helppers;
using TownBuilder.Models;
using TownBuilder.Views;

namespace TownBuilder.ViewModels
{
    public class GameViewModel : BaseNotifyPropertyChanged
    {
        private ObservableCollection<ObservableCollection<CasillasViewModel>> _cells = null!;

        private int _gridWidth;
        private int _gridHeight;
        private int _level = 1;

        private Color _bloqueadoColor;
        private Color _vacioColor;
        private Color _compradoColor;

        private Carta? _seleccionada;
        public Carta? Seleccionada
        {
            get => _seleccionada;
            set => SetProperty(ref _seleccionada, value);
        }

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

        private int _dinero = Constantes.DineroInicio;
        public int Dinero
        {
            get => _dinero;
            set => SetProperty(ref _dinero, value);
        }
        private int _dineroGanado;
        public int DineroGanado
        {
            get => _dineroGanado;
            set => SetProperty(ref _dineroGanado, value);
        }
        private int _dineroGastado;
        public int DineroGastado
        {
            get => _dineroGastado;
            set => SetProperty(ref _dineroGastado, value);
        }

        private string _diaActual = "Lunes";
        public string DiaActual
        {
            get => _diaActual;
            set => SetProperty(ref _diaActual, value);
        }

        private int _energia = Constantes.Energia;
        public int Energia
        {
            get => _energia;
            set => SetProperty(ref _energia, value);
        }
        private int _energiaRestante = Constantes.Energia;
        public int EnergiaRestante
        {
            get => _energiaRestante;
            set => SetProperty(ref _energiaRestante, value);
        }

        private int _trabajadores;
        public int Trabajadores
        {
            get => _trabajadores;
            set => SetProperty(ref _trabajadores, value);
        }
        private int _trabajadoresDisponibles;
        public int TrabajadoresDisponibles
        {
            get => _trabajadoresDisponibles;
            set => SetProperty(ref _trabajadoresDisponibles, value);
        }

        private int _diaSemana = 1;
        private int _dia = 1;
        public int Dia
        {
            get => _dia;
            set => SetProperty(ref _dia, value);
        }
        private int _diaCobro = Constantes.DiasCobro;
        public int DiaCobro
        {
            get => _diaCobro;
            set => SetProperty(ref _diaCobro, value);
        }
        private ObservableCollection<string> _dineroHistorico = new ObservableCollection<string>();
        public ObservableCollection<string> DineroHistorico
        {
            get => _dineroHistorico;
            set => SetProperty(ref _dineroHistorico, value);
        }

        private ObservableCollection<Carta?> _cartasSeleccionables = new();
        public ObservableCollection<Carta?> CartasSeleccionables
        {
            get => _cartasSeleccionables;
            set => SetProperty(ref _cartasSeleccionables, value);
        }
        private ObservableCollection<Carta?> _listaCartas = new();
        public ObservableCollection<Carta?> ListaCartas
        {
            get => _listaCartas;
            set => SetProperty(ref _listaCartas, value);
        }
        public ObservableCollection<ObservableCollection<CasillasViewModel>> Cells
        {
            get => _cells;
            set => SetProperty(ref _cells, value);
        }

        private ObservableCollection<Recursos> _recursosValores = new();
        public ObservableCollection<Recursos> RecursosValores
        {
            get => _recursosValores;
            set => SetProperty(ref _recursosValores, value);
        }

        private ObservableCollection<Carta?> _listaMejoras = new();
        public ObservableCollection<Carta?> ListaMejoras
        {
            get => _listaMejoras;
            set => SetProperty(ref _listaMejoras, value);
        }

        public Action<object, RoutedEventArgs> EndGame { get; set; }
        public bool Error { get; set; }
        private bool _modoDestruir;
        public bool ModoDestruir
        {
            get => _modoDestruir;
            set => SetProperty(ref _modoDestruir, value);
        }

        private bool _showGanancia;
        public bool ShowGanancia
        {
            get => _showGanancia;
            set => SetProperty(ref _showGanancia, value);
        }
        private bool _showGasto;
        public bool ShowGasto
        {
            get => _showGasto;
            set => SetProperty(ref _showGasto, value);
        }
        public bool Mute
        {
            get => SoundController.Mute;
            set => SetProperty(ref SoundController.Mute, value);
        }
        private SoundController SoundController;
        public GameViewModel(ConfigModel config)
        {
            this.SetDefaultValues();
            GridHeight = config.Rows;
            GridWidth = config.Columns;
            CreateCells();
            CreateDeck();
            SoundController = new SoundController();
        }

        private void CreateCells()
        {
            Cells = new ObservableCollection<ObservableCollection<CasillasViewModel>>();
            var random = new Random();
            for (var posRow = 0; posRow < GridHeight; posRow++)
            {
                var row = new ObservableCollection<CasillasViewModel>();
                for (var posCol = 0; posCol < GridWidth; posCol++)
                {
                    // Replace with valid enum values and random logic
                    // If you want "Hierba" to be the default empty type, and "Piedra" as the alternative:
                    var probabilidadVacio = 70; // You should move this to Constantes.ProbabilidadVacio if possible.
                    var probabilidadPiedra = 90; // You should move this to Constantes.ProbabilidadVacio if possible.
                    var randomValue = random.Next(0, 100);
                    var valueEmpty = randomValue < probabilidadVacio ? CasillasEmptyTypes.Hierba : (randomValue < probabilidadPiedra ?CasillasEmptyTypes.Tronco : CasillasEmptyTypes.Piedra);
                    var casillasViewModel = new CasillasViewModel(new Casilla(posRow, posCol, valueEmpty), this);
                    row.Add(casillasViewModel);
                }
                Cells.Add(row);
            }
            SetInitialCasillas();
        }        
        private void CreateDeck()
        {
            ListaCartas = DeckHelpper.GenerateDeck();
            CartasSeleccionables=DeckHelpper.CartasRandom(ListaCartas);
            SetInitialCasillas();
            RecursosValores = new ObservableCollection<Recursos>(RecursosHelper.RecursosValores());
        }
        
        private void SetInitialCasillas()
        {
            var halfHeight = GridHeight / 2;
            var halfWidth = GridWidth / 2;
            Cells[halfHeight][halfWidth].Cell.State = CasillasEstados.Comprado;
            Cells[halfHeight][halfWidth].Cell.Carta = DeckHelpper.GetBasicBuild();
            Recalcule(halfHeight, halfWidth);
            CalculeResources();
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
                        model.Importe = Constantes.ImporteBase.ToString("n0");
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

        public int Comprar(int cellHeigh, int cellWidth)
        {
            if (Constantes.ImporteBase < _dinero)
            {
                Cobrar((int)Constantes.ImporteBase);
                IncrementarImporte();
                Cells[cellHeigh][cellWidth].Cell.State = CasillasEstados.Comprado;
                Cells[cellHeigh][cellWidth].Importe = "";
                Recalcule(cellHeigh, cellWidth);
                SoundController.Play(SoundsTipos.Pay);
            }
            else
            {
                Error = true;
                return 4;
            }

            return 0;
        }
        private void IncrementarImporte()
        {
            Constantes.ImporteBase = (Constantes.ImporteBase * Constantes.ImporteIncremento);
        }

        public void FinalizarTurno()
        {
            Dia++;
            _diaSemana++;
            if (_diaSemana == 8) _diaSemana = 1;
            DiaActual = DiasHelper.GetDiaSemanaTexto (_diaSemana);
            EnergiaRestante = Energia;
            MostrarVentas();
            CobrarImpuestos();
            CartasSeleccionables=DeckHelpper.CartasRandom(ListaCartas);
        }


        private void MostrarVentas()
        {
            var recursosTotales = RecursosHelper.GetTotales(Cells);
            double dineroGanado=0;
            foreach (var recursos in recursosTotales)
            {
                dineroGanado+= recursos.Total;
            }

            var view = new ResurceView(recursosTotales,(int)dineroGanado);
            view.ShowDialog();
      
            Venta((int)dineroGanado);
        }

        private void Venta(int valor)
        {
            DineroGanado= valor;
            ShowGasto = false;
            ShowGanancia = true;
            var dineroFinal = Dinero + DineroGanado;
            DineroHistorico.Add(Dinero + "+" + DineroGanado + ": " + dineroFinal);
            Dinero = dineroFinal;
        }

        private void CobrarImpuestos()
        {
            DiaCobro--;
            if (DiaCobro>0) return;
            Cobrar(Constantes.ImporteCobro);
            if (Dinero > 0)
            {
                Constantes.ImporteCobro = (int)(Constantes.ImporteCobro * Constantes.ImporteIncremento);
                DiaCobro = Constantes.DiasCobro;
                MostrarMejora();
            }
        }

        private void MostrarMejora()
        {
            var view = new MejorasView(_level, _listaCartas);
            view.ShowDialog();
            if (view.mejoras.Mejora != null)
            {
                ListaMejoras.Add(view.mejoras.Mejora);
            }else if (view.mejoras.Eliminar != null)
            {
                ListaCartas.Remove(view.mejoras.Eliminar);
            }else if (view.mejoras.Añadir != null)
            {
                ListaCartas.Add(view.mejoras.Añadir);
            }
        }

        private void Cobrar(int valor)
        {
            DineroGastado = valor;
            var dineroRestate =Dinero-DineroGastado;
            DineroHistorico.Add(Dinero + "-" + DineroGastado + ": " + dineroRestate);
            Dinero = dineroRestate;
            ShowGanancia = false;
            ShowGasto = true;
            if (Dinero < 0)
            {
                GameOver();
            }
        }

        public int AplicarCarta(int cellHeigh, int cellWidth)
        {
            if (Seleccionada!=null && EnergiaRestante>0 && Dinero>=Seleccionada.Importe)
            {
                if (Seleccionada.Tipo != CartasTipos.Casas && TrabajadoresDisponibles == 0)
                {
                    Error = true;
                    return 2;
                }
                Cobrar(Seleccionada.Importe);
                Cells[cellHeigh][cellWidth].Cell.Carta = (Carta)Seleccionada.Clone();
                Cells[cellHeigh][cellWidth].Cell.Carta!.RecursoCantidad = 1;
                if (Seleccionada.Consumible)
                {
                    ListaCartas.Remove(Seleccionada);
                }
                CartasSeleccionables.Remove(Seleccionada);
                Seleccionada = null;
                SoundController.Play(SoundsTipos.Card);
                EnergiaRestante--;
                CheckCombo( cellHeigh,  cellWidth);
                CalculeResources();
            }
            else
            {
                Error = true;
                if (EnergiaRestante <= 0)
                {
                    return 1;
                }
                
                if (Seleccionada == null)
                {
                    return 3;
                }
                if (Dinero<Seleccionada.Importe)
                {
                    return 4;
                }
            }

            return 0;
        }
        public class CasillasCombo
        {
            public int Height { get; set; }
            public int Width { get; set; }
            public RecursosTipos Recurso { get; set; }
        }

        private void CheckCombo(int cellHeigh, int cellWidth)
        {
            var recurso = Cells[cellHeigh][cellWidth].Cell.Carta!.Recurso;
            var casillas = CogerCasillasArribaIzquierda(cellHeigh, cellWidth,null);
            CheckRecursoCombo(casillas, recurso);
            casillas = CogerCasillasArribaDerecha(cellHeigh, cellWidth,null);
            CheckRecursoCombo(casillas, recurso);
            casillas = CogerCasillasAbajoIzquierda(cellHeigh, cellWidth,null);
            CheckRecursoCombo(casillas, recurso);
            casillas = CogerCasillasAbajoDerecha(cellHeigh, cellWidth,null);
            CheckRecursoCombo(casillas, recurso);
        }

        private List<CasillasCombo> CogerCasillasAbajoIzquierda(int cellHeigh, int cellWidth, RecursosTipos? recurso)
        {
            var casillas = new List<CasillasCombo?>
            {
                Generate(cellHeigh, cellWidth,recurso),
                Generate(cellHeigh + 1, cellWidth - 1,recurso),
                Generate(cellHeigh + 1, cellWidth,recurso),
                Generate(cellHeigh , cellWidth-1,recurso)
            };
            return casillas.Where(e=> e!=null).ToList()!;
        }
        private List<CasillasCombo> CogerCasillasAbajoDerecha(int cellHeigh, int cellWidth, RecursosTipos? recurso){
            var casillas = new List<CasillasCombo?>
            {
                Generate(cellHeigh, cellWidth,recurso),
                Generate(cellHeigh + 1, cellWidth + 1,recurso),
                Generate(cellHeigh + 1, cellWidth,recurso),
                Generate(cellHeigh , cellWidth+1,recurso)
            };
            return casillas.Where(e=> e!=null).ToList()!;
        }
        private List<CasillasCombo> CogerCasillasArribaIzquierda(int cellHeigh, int cellWidth, RecursosTipos? recurso)
        {
            var casillas = new List<CasillasCombo?>
            {
                Generate(cellHeigh, cellWidth,recurso),
                Generate(cellHeigh - 1, cellWidth - 1,recurso),
                Generate(cellHeigh - 1, cellWidth,recurso),
                Generate(cellHeigh, cellWidth-1,recurso)
            };
            return casillas.Where(e=> e!=null).ToList()!;
        }
        private List<CasillasCombo> CogerCasillasArribaDerecha(int cellHeigh, int cellWidth, RecursosTipos? recurso)
        {
            var casillas = new List<CasillasCombo?>
            {
                Generate(cellHeigh, cellWidth, recurso),
                Generate(cellHeigh - 1, cellWidth + 1, recurso),
                Generate(cellHeigh - 1, cellWidth,recurso),
                Generate(cellHeigh, cellWidth+1,recurso)
            };

            return casillas.Where(e=> e!=null).ToList()!;
        }
        private CasillasCombo? Generate(int cellHeigh, int cellWidth,RecursosTipos? cartasTipos)
        {
            if (Cells[cellHeigh][cellWidth].Cell.Carta != null && Cells[cellHeigh][cellWidth].Cell.Carta.Recurso != null
                && (cartasTipos==null || Cells[cellHeigh][cellWidth].Cell.Carta.Recurso ==cartasTipos.Value)
                )
            {
                return new CasillasCombo
                {
                    Height = cellHeigh, Width = cellWidth, Recurso = Cells[cellHeigh][cellWidth].Cell.Carta!.Recurso
                };
            }
            return null;
        }

        private void CheckRecursoCombo(List<CasillasCombo> casillas, RecursosTipos recurso)
        {
            bool combo=false;
            if (casillas.Count(e => e.Recurso == recurso) == 4)
            {
                foreach (var casilla in casillas)
                {
                    if (!Cells[casilla.Height][casilla.Width].Cell.Carta!.Combo)
                    {
                        Cells[casilla.Height][casilla.Width].Cell.Carta!.Combo = true;
                        Cells[casilla.Height][casilla.Width].MostrarCombo();
                        combo = true;
                    }
                }

                if (combo)
                {
                    foreach (var casilla in casillas)
                    {
                        CheckCombo(casilla.Height, casilla.Width);
                    }
                }
            }
        }

        private void EliminarCombo(int cellHeigh, int cellWidth, RecursosTipos recurso)
        {
            var casillas = CogerCasillasArribaIzquierda(cellHeigh, cellWidth,recurso);
            casillas.AddRange(CogerCasillasArribaDerecha(cellHeigh, cellWidth,recurso));
            casillas.AddRange(CogerCasillasAbajoIzquierda(cellHeigh, cellWidth,recurso));
            casillas.AddRange(CogerCasillasAbajoDerecha(cellHeigh, cellWidth,recurso));
            casillas = casillas.Distinct().ToList();
            foreach (var casilla in casillas.Distinct())
            {
                Cells[casilla.Height][casilla.Width].Cell.Carta!.Combo = false;
            }
            foreach (var casilla in casillas.Distinct())
            {
                CheckCombo(casilla.Height, casilla.Width);
            }
        }

        private void GameOver()
        {
            EndGame(null,null);
        }
        private void CalculeResources()
        {
            int trabajadoresUsados = 0;
            int trabajadoresTotales = 0;
            foreach (var cellList in Cells)
            {
                foreach (var cell in cellList)
                {
                    if (cell.Cell.Carta != null )
                    {
                        if (cell.Cell.Carta.Tipo == CartasTipos.Casas)
                        {
                            trabajadoresTotales += cell.Cell.Carta.Trabajadores;
                        }
                        else
                        {
                            trabajadoresUsados += cell.Cell.Carta.Trabajadores;
                        }
                    }
                }
            }
            Trabajadores = trabajadoresTotales;
            TrabajadoresDisponibles= Trabajadores+trabajadoresUsados;
        }
        
        public void Destruir(int cellHeigh, int cellWidth)
        {
            var cartaAEliminar = Cells[cellHeigh][cellWidth].Cell.Carta;
            if (Cells[cellHeigh][cellWidth].Cell.Carta!.Tipo == CartasTipos.Casas)
            {
                if (Cells[cellHeigh][cellWidth].Cell.Carta!.Trabajadores > TrabajadoresDisponibles)
                {
                    Error = true;
                    return;
                }
                cartaAEliminar = null;
            }
            Seleccionada = null;
            Cobrar(Constantes.ImporteDestruir);
            
            Cells[cellHeigh][cellWidth].Cell.Carta = null;
            SoundController.Play(SoundsTipos.Destruir);
            CalculeResources();
            if (cartaAEliminar != null)
            {
                EliminarCombo(cellHeigh, cellWidth, cartaAEliminar!.Recurso);
            }
        }

        internal void SoundChange()
        {
            if (!SoundController.Mute)
            {
                SoundController.Mute = true;
                SoundController.PauseMusic();
            }
            else
            {
                SoundController.Mute = false;
                SoundController.PlayMusic();
            }
        }
    }
}