using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TownBuilder.UserControlsViews
{
	/// <summary>
	/// Interaction logic for CellControl
	/// </summary>
	public partial class CellView : UserControl
	{
		public CellView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Start color - the darkest color of the cell.
		/// </summary>
		public Color BloqueadoColor
		{
			get { return (Color)GetValue(BloqueadoColorProperty); }
			set { SetValue(BloqueadoColorProperty, value); }
		}

		public static readonly DependencyProperty BloqueadoColorProperty = DependencyProperty.Register("BloqueadoColor", typeof(Color), typeof(CellView));

        /// <summary>
        /// Start color - the darkest color of the cell.
        /// </summary>
        public Color VacioColor
        {
            get { return (Color)GetValue(VacioColorProperty); }
            set { SetValue(VacioColorProperty, value); }
        }

        public static readonly DependencyProperty VacioColorProperty = DependencyProperty.Register("VacioColor", typeof(Color), typeof(CellView));

		/// <summary>
		/// Finish color - the lightest color of the cell.
		/// </summary>
		public Color CompradoColor
		{
			get { return (Color)GetValue(CompradoColorProperty); }
			set { SetValue(CompradoColorProperty, value); }
		}

		public static readonly DependencyProperty CompradoColorProperty = DependencyProperty.Register("CompradoColor", typeof(Color), typeof(CellView));
    }
}
