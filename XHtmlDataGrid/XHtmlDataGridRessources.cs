//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : xwpfDataGridResources.cs
//
// Description : ressources pour la dataGrid
//___________________________________________________________________________

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;
using Divalto.Systeme;

namespace Divaltohtml
{

	internal struct DataGridMouseEventArgs
	{
		internal int? RowIndex;
		internal int? ColIndex;

		internal byte ClickCount;
		internal byte ClickedButton;
		internal byte Key;

		internal DataGridClick Click;
		internal Control ActiveControl;
		internal ushort? SeqPoint;

		internal ProprietesWpf Property;
		internal string ValueString;
		internal ushort? ValueUshort;
	}

	internal static class ScrollBarExtensions
	{
		internal static double GetThumbCenter(this ScrollBar scrollBar)
		{
			if (scrollBar == null) return 0;

			double thumbLength = GetThumbLength(scrollBar);
			double trackLength = scrollBar.Maximum - scrollBar.Minimum;

			return thumbLength / 2 + scrollBar.Minimum + (scrollBar.Value - scrollBar.Minimum) * (trackLength - thumbLength) / trackLength;
		}

		internal static void SetThumbCenter(this ScrollBar scrollBar, double thumbCenter)
		{
			if (scrollBar == null) return;

			double thumbLength = GetThumbLength(scrollBar);
			double trackLength = scrollBar.Maximum - scrollBar.Minimum;
			double value;

			if (thumbCenter >= scrollBar.Maximum - thumbLength / 2) value = scrollBar.Maximum;
			else if (thumbCenter <= scrollBar.Minimum + thumbLength / 2) value = scrollBar.Minimum;
			else if (thumbLength >= trackLength) value = scrollBar.Minimum;
			else value = scrollBar.Minimum + trackLength * ((thumbCenter - scrollBar.Minimum - thumbLength / 2) / (trackLength - thumbLength));

			scrollBar.SetCurrentValue(RangeBase.ValueProperty, value);
		}

		internal static double GetThumbLength(this ScrollBar scrollBar)
		{
			if (scrollBar == null) return 0;

			double trackLength = scrollBar.Maximum - scrollBar.Minimum;
			return trackLength * scrollBar.ViewportSize / (trackLength + scrollBar.ViewportSize);
		}

		internal static void SetThumbLength(this ScrollBar scrollBar, double thumbLength)
		{
			if (scrollBar == null) return;

			double trackLength = scrollBar.Maximum - scrollBar.Minimum;
			double viewportSize;

			if (thumbLength < 0) viewportSize = 0;
			else if (thumbLength < trackLength) viewportSize = trackLength * thumbLength / (trackLength - thumbLength);
			else viewportSize = double.MaxValue;

			scrollBar.SetCurrentValue(ScrollBar.ViewportSizeProperty, viewportSize);
		}

		/// <summary>
		/// paramètre la scrollbar
		/// </summary>
		/// <param name="scrollBar">ScrollBar whose property are set</param>
		/// <param name="scrollMin">Position début</param>
		/// <param name="scrollMax">Position fin</param>
		/// <param name="scrollSmallChange">Pas</param>
		/// <param name="scrollLargeChange">Pas "page"</param>
		/// <param name="scrollValue">Position courante</param>
		/// <param name="thumbLength">Taille du curseur</param>
		internal static void Set(this ScrollBar scrollBar, uint scrollMin, uint scrollMax, uint scrollSmallChange, uint scrollLargeChange, uint scrollValue, double thumbLength)
		{
			if (scrollBar == null) return;

			double trackLength = scrollMax - scrollMin;

			scrollBar.SmallChange = scrollSmallChange;
			scrollBar.LargeChange = scrollLargeChange;

			if (thumbLength < trackLength)
			{
				scrollBar.Visibility = Visibility.Visible;
				scrollBar.Minimum = scrollMin;
				scrollBar.Maximum = scrollMax;

				scrollBar.SetThumbLength(thumbLength);
				scrollBar.SetThumbCenter(thumbLength / 2 + scrollValue);
			}
			else
			{
				scrollBar.Visibility = Visibility.Collapsed;
				scrollBar.Minimum = 0;
				scrollBar.Maximum = 0;
			}
		}

		/// <summary>
		/// valide/invalide la scrollbar
		/// </summary>
		/// <param name="scrollBar">scrollBar to enable/disable</param>
		/// <param name="enabled">indique si la scroolbar doit être validée (true) ou invalidée (false)</param>
		/// <param name="forcedBefore">disabled forced by code behind before the set</param>
		/// <param name="forcedAfter">disabled forced by code behind after the set</param>
		internal static void SetEnable(this ScrollBar scrollBar, bool enabled, bool forcedBefore, bool forcedAfter)
		{
			if (scrollBar == null) return;

			// on ne dégrise pas automatiquement si la scrollBar avait été grisée par programme (le dégrisage ne peut se faire que par programme aussi)
			if (!(!scrollBar.IsEnabled && forcedBefore && !forcedAfter)) scrollBar.IsEnabled = enabled;
		}
	}

	internal static class ObservableCollectionExtensions
	{
		internal static void RemoveRange<T>(this ObservableCollection<T> observableCollection, int index, int count)
		{
			if (observableCollection == null)
				throw new ArgumentNullException("observableCollection");

			for (int i = 0; i < count; i++)
				observableCollection.RemoveAt(index);
		}
	}


	/// <summary>
	/// conversion des propriétés IsEnabled + brush en brush (gris ou la brush originale)
	/// </summary>
	public class IsEnabledBrushToBrushConverter : IMultiValueConverter  // MarkupExtension, !!!!!!!!!!!!!!!!!!!!
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			if (values == null) return null;
			bool isEnabled = (values[0] != DependencyProperty.UnsetValue && (bool)values[0]); // garde-fou en cas de propriété non initialisée
			Brush brush = (values[1] == DependencyProperty.UnsetValue || values[1] == null) ? (Brush)Application.Current.Resources["Default" + parameter + "Brush"] : (Brush)values[1]; // garde-fou en cas de propriété non initialisée
			return (isEnabled) ? brush : Application.Current.Resources["Disabled" + parameter + "Brush"];
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;	// pas utilisé normalement
		}

		/*
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new IsEnabledBrushToBrushConverter();
		}
		 * */
	}


	/// <summary>
	/// Convertisseur: affecte la CellViewModel d'une cellule à sa propriété Tag (pour permettre à la cellule de binder des propriétés directement sur son cellViewModel
	/// </summary>
	public class CellViewModelToTagConverter : IMultiValueConverter  // MarkupExtension, !!!!!!!!!!!!!!!!!!!
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			return null;
		}
		//{
		//   var row = values[0] as XHtmlRow;
		//   var cell = values[1] as DataGridCell;

		//   if (row != null && cell != null)
		//   {
		//      var column = cell.Column as IXHtmlDataGridColumn;

		//      if (column != null)
		//         cell.SetBinding(FrameworkElement.TagProperty, new Binding
		//         {
		//            Source = row[column.DataGrid.Columns.IndexOf((DataGridColumn)column)],
		//            BindsDirectlyToSource = true
		//         });
		//   }

		//   return DependencyProperty.UnsetValue;
		//}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
		   throw new NotSupportedException();
		}

		
		//public override object ProvideValue(IServiceProvider serviceProvider)
		//{
		//   return new CellViewModelToTagConverter();
		//}
		
	}


}
