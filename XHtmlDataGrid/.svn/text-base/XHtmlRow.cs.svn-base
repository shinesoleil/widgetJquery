//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlRow.cs
// Description : Ligne de tableau
//___________________________________________________________________________

using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using System.ComponentModel;
using System;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	/// <summary>
	/// Class XHtmlRow : represents a row of the dataGrid.
	/// Contains the style of the row when it is selected, and enumerates the content of the row as a List
	/// </summary>
	/// 
	public enum XHTmlTableauOperationLigne
	{
		Nouvo = 1,
		Remplacement = 2,
	}

	internal class XHtmlRow : Collection<XHtmlGenericCell>, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public int Indice;
		public XHTmlTableauOperationLigne Operation;
		public ushort IndicePourInsertion;

		public Brush Background
		{
			get
			{
				return background;
			}

			set
			{
				if (value == background) return;
				background = value;
				NotifyPropertyChanged("Background");
			}
		}
		private Brush background;

		public Object Header
		{
			get
			{
				return header;
			}

			set
			{
				if (value == header) return;
				header = value;
				NotifyPropertyChanged("Header");
			}
		}
		private Object header;

		internal bool IsSelected, IsAlternated;

		private readonly XHtmlDataGrid dataGrid;
		private string selectedItemBackground;

		private FrameworkElement
			selectedItemHeader,
			currentItemHeader; // !!!!!!!!!!!!!!!!!!!!!!!!!!! = new Label { Content = ">" };


		#region Constructeur
		public XHtmlRow() 
		{
			int i = 0;
		} // INDISPENSABLE sinon dataGrid.CanUserAddRow reste à false quoi qu'on lui affecte

		public XHtmlRow(XHtmlDataGrid dg)
		{
			dataGrid = dg;
		}
		#endregion Constructeur


		#region Remplissage
		/// <summary>
		/// Fills the dataGridRow
		/// </summary>
		/// <param name="buffer">global DVBuffer for client/server data exchange</param>
		internal void Fill(DVBuffer buffer)
		{
			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_LIGNE_FIN)
			{
				ushort colIndex;
				uint id;
				buffer.Get(out colIndex);			// Index de la cellule à remplir, au sein de la ligne courante (ushort)
				buffer.Get(out id);					// Identifiant de l'objet représentant la cellule (uint)

				switch (property)
				{
					case ProprietesWpf.CHAMP_DEBUT: 
					//	if (colIndex > Count) Add(new XHtmlTextBoxCell(dataGrid.Columns[colIndex - 1])); 
						break;
						
					
					/* !!!!!!!!!!!
					case ProprietesWpf.CHAMP_CACHE_DEBUT: if (colIndex > Count) Add(new XHtmlPasswordBoxCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.CHAMP_DATE_DEBUT: if (colIndex > Count) Add(new XHtmlDatePickerCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.OBJET_GRAPHIQUE_DEBUT: if (colIndex > Count) Add(new XHtmlHogCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.MULTICHOIX_DEBUT: if (colIndex > Count) Add(new XHtmlComboBoxCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.CASE_A_COCHER_DEBUT: if (colIndex > Count) Add(new XHtmlCheckBoxCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.IMAGE_DEBUT: if (colIndex > Count) Add(new XHtmlImageCell(dataGrid.Columns[colIndex - 1])); break;
					case ProprietesWpf.ARBRE_DEBUT: if (colIndex > Count) Add(new XHtmlTreeCell(dataGrid.Columns[colIndex - 1])); break;
						 */
					default: throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Row, property.ToString());
				}

				XHtmlGenericCell cell = new XHtmlGenericCell();
				cell.Indice = colIndex;
				this.Add(cell);

				cell.ReadProperties(buffer);
				// this[colIndex - 1].ReadProperties(buffer);
				buffer.Get(out property);
			}
		}
		#endregion Remplissage


		#region Lecture des propriétés du style de sélection
		/// <summary>
		/// Reads the properties of a row's selection style from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		internal void ReadSelectionStyle(DVBuffer buffer)
		{
			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_LIGNE_MARQUEE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.TABLEAU_LIGNE_MARQUEE_TYPE_FOND:			// type de fond (ushort)
						byte bg;
						buffer.Get(out bg);
						switch (bg)
						{
							case 0:	// R.A.Z couleur de fond (pas de couleur)
								selectedItemBackground = null;
								break;

							case 1:	// couleur windows
								selectedItemBackground = "HighlightedDataGridBackground";
								break;

							case 2:	// Infos couleur habituelles
								// traité dans le "case ProprietesWpf.TABLEAU_LIGNE_MARQUEE_FOND"
								break;
						}
						break;

					case ProprietesWpf.TABLEAU_LIGNE_MARQUEE_FOND:					// couleur de fond personalisée
						ushort colorId;
						buffer.Get(out colorId);
						selectedItemBackground = "Brush-" + colorId;
						break;

					case ProprietesWpf.TABLEAU_LIGNE_MARQUEE_IMAGE:					// type d'image (byte)
						byte bitmap;
						buffer.Get(out bitmap);
						switch (bitmap)
						{
							case 0:	// R.A.Z. de la bitmap
								selectedItemHeader = null;
								break;

							case 1:	// Infos bitmap habituelles
								XHtmlImageFile imageFile = new XHtmlImageFile();
								imageFile.ReadProperties(buffer);
								// !!!!!!!!!!!!!!!!!! selectedItemHeader = new Image { Source = XHtmlImage.GetImage(imageFile) };
								break;

							case 2:	// Bitmap prédéfinie (string = "1", "2", ... > voir liste dans la doc)
								string imageCode;
								buffer.GetString(out imageCode);
								// !!!!!!!!!!!!!!!! selectedItemHeader = new Label { Content = imageCode }; // l'image prédéfinie est sélectionnée dans le dataTemplateSelector en fonciton du code
								break;
						}
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Row, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture des propriétés du style de sélection


		/// <summary>
		/// Sets the row's style
		/// </summary>
		internal void SetStyle()
		{
			// options de ligne courrante :
			// 1 > ligne courante affichée avec les couleurs de la sélection Windows (valeur par défaut)
			// 2 > forme XAML (en forme de flèche) affichée dans la colonne d'état en regard de cette ligne
			// 3 > ligne courante affichée avec les couleurs de la sélection Windows ET image bitmap

			// Si ligne courante ET sélectionnée :
			// si option de ligne courante == 2 :	fond sélection + header courant
			// sinon:										fond courant + header sélection

			#region Ligne courante
			if (dataGrid.CurrentItem == this || (dataGrid.CurrentItem == null && dataGrid.CurrentCellInfo.Item == this))
			{
				if (IsSelected)
				{
					if (dataGrid.CurrentLineOptions != 2)
					{
						SetCurrentItemBackground();
						Header = selectedItemHeader;
					}
					else
					{
						if (selectedItemBackground != null) SetSelectedItemBackground();
						else Background = Brushes.Transparent;
						Header = currentItemHeader;
					}
				}
				else // !IsSelected
				{
					if (dataGrid.CurrentLineOptions != 2) SetCurrentItemBackground();
					else
					{
						Background = Brushes.Transparent;
						foreach (IXHtmlDataGridCell cell in this) cell.SetColors();
					}
					Header = dataGrid.CurrentLineOptions != 1 ? currentItemHeader : null;
				}
			}
			#endregion Ligne courante

			#region ligne hors ligne courante
			else // dataGrid.CurrentItem != this
			{
				if (IsSelected)
				{
					Header = selectedItemHeader;
					foreach (IXHtmlDataGridCell cell in this) cell.SetColors(); // on y passe dans tous les cas pour remettre le bon Foreground

					if (selectedItemBackground != null) SetSelectedItemBackground();
					else Background = Brushes.Transparent;

				}
				else // !IsSelected
				{
					Header = null;
					Background = Brushes.Transparent;
					foreach (IXHtmlDataGridCell cell in this) cell.SetColors();
				}
			}
			#endregion ligne hors ligne courante

			SetAlternation();
		}

		private void SetAlternation()
		{
			// zonage classique
			if (dataGrid.RowAlternation == 0 && (dataGrid.Rows.IndexOf(this) + dataGrid.ItemsOffset & 0x01) == 1)
				SetAlternationBackground();

			// zonage par valeur de colonne
			else if (dataGrid.RowAlternation > 0 && dataGrid.RowAlternation - 1 < Count)
			{
				int rowIndex = dataGrid.Rows.IndexOf(this);
				int colIndex = dataGrid.RowAlternation - 1;

				if (rowIndex == 0)
				{
					if (dataGrid.Rows.Count <= 1) // premier remplissage, on zone la toute première ligne par défaut (initialization du zonage)
					{
						IsAlternated = true;
						SetAlternationBackground();
					}
					else // pour récupérer la bonne couleur quand on scroll de 1: on regarde la couleur de la deuxième ligne pour déduire celle de la première
					{
						XHtmlGenericCell cell = this[colIndex];
						XHtmlGenericCell previousCell = dataGrid.Rows[rowIndex + 1][colIndex];
						if (cell.StringValue == previousCell.StringValue)
							IsAlternated = dataGrid.Rows[rowIndex + 1].IsAlternated;
						else
							IsAlternated = !dataGrid.Rows[rowIndex + 1].IsAlternated;
						if (IsAlternated) SetAlternationBackground();
					}
				}
				else // toutes les autres lignes sont déduites en fonction de la précédente
				{
					XHtmlGenericCell cell = this[colIndex];
					XHtmlGenericCell previousCell = dataGrid.Rows[rowIndex - 1][colIndex];
					if (cell.StringValue == previousCell.StringValue)
						IsAlternated = dataGrid.Rows[rowIndex - 1].IsAlternated;
					else
						IsAlternated = !dataGrid.Rows[rowIndex - 1].IsAlternated;
					if (IsAlternated) SetAlternationBackground();
				}
			}
		}


		private void SetSelectedItemBackground()
		{
//			Background = Application.Current.Resources[selectedItemBackground] as Brush;	// couleur de fond de la ligne = couleur de sélection par défaut (orange)
//!!!!!!!!!!!!			foreach (var cell in this) cell.Background = Brushes.Transparent; // on rend le fond de chaque cellule transparent pour pouvoir voir celui de la ligne
		}

		private void SetCurrentItemBackground()
		{
//			Background = Application.Current.Resources["DataGridCurrentLineBackground"] as Brush;	// couleur de fond de ligne courrante

			foreach (var cell in this) // mise à jour du style de chaque cellule séparément
			{
//!!!!!!!!!!!!!!				cell.Background = Brushes.Transparent; // on rend le fond de chaque cellule transparent pour pouvoir voir celui de la ligne
//!!!!!!!!!!!!!!				cell.Foreground = SystemColors.HighlightTextBrush; // on passe le texte en blanc
			}
		}

		private void SetAlternationBackground()
		{
			if (Background != Brushes.Transparent) return;

			Background = new SolidColorBrush { Color = Color.FromArgb(16, 0, 0, 0) };
			Background.Freeze();
			foreach (IXHtmlDataGridCell cell in this) cell.SetAlternationBackground();
		}


		private void NotifyPropertyChanged(string info)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(info));
		}

	}


	/// <summary>
	/// Represents a TemplateSelector to select which DataTemplate to apply to a DataGridRowHeader depending on its content (predefined image, image etc...)
	/// </summary>
	public class RowHeaderTemplateSelector : DataTemplateSelector
	{
		/*
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement rowHeader = container as FrameworkElement;

			if (rowHeader != null)
			{
				Label rowHeaderLabel = item as Label;
				if (rowHeaderLabel != null && rowHeaderLabel.Content != null)
				{
					switch (rowHeaderLabel.Content.ToString())
					{
						case "1": return Application.Current.Resources["CheckMarkTemplate"] as DataTemplate;
						case "2": return Application.Current.Resources["CrossTemplate"] as DataTemplate;
						case "3": return Application.Current.Resources["UpArrowTemplate"] as DataTemplate;
						case "4": return Application.Current.Resources["DownArrowTemplate"] as DataTemplate;
						case "5": return Application.Current.Resources["KeyTemplate"] as DataTemplate;
						case "6": return Application.Current.Resources["StarTemplate"] as DataTemplate;
						case "7": return Application.Current.Resources["RightArrowTemplate"] as DataTemplate;
						case "8": return Application.Current.Resources["PencilTemplate"] as DataTemplate;
						case "9": return Application.Current.Resources["FilterTemplate"] as DataTemplate;
						case ">": return Application.Current.Resources["CurrentLineHeaderTemplate"] as DataTemplate;
						default: return null;
					}
				}

				if (item is Image) return Application.Current.Resources["ImageTemplate"] as DataTemplate;
			}
			return null;
		}
		 * */
	}

}
