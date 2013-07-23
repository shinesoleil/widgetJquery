//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlTextBoxColumn.cs
// Description : Colonne champ
//___________________________________________________________________________

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	class XHtmlTextBoxColumn : XHtmlGenericColumn  // : IXHtmlDataGridColumn 
	{
		//public uint Id { get; set; }
		//public ushort SeqPoint { get; set; }
		//public FrameworkElement FrameworkElement { get { return null; } }
		//public XHtmlPresentation Presentation { get; set; }
		//public Collection<string> ListOfValidButtons { get; private set; }
		//public XHtmlPage Page { get; set; }

		//// propriétés de colonne
		//public ColumnType ColumnType { get; set; }
		//public XHtmlDataGrid DataGrid { get; set; }
		//public string ColumnBackground { get; set; }
		//public ushort ColumnFont { get; set; }
		//public string RecordName { get; set; }
		//public string DataName { get; set; }
		//public int[] Indexes { get; set; }
		//public bool IsMandatory { get; set; }
		//public bool IsHiddenForced { get; set; }


		//!!!!!!!!!!!!!!!!!!!!!!! a revoir, 
		bool WidthIsAuto { get; set; }
		//int Width { get; set; }

		//int _width;
		//public int Width
		//{ get 
		//   { return _width; }
		//   set
		//   { _width = value; }
		//}

		//private static readonly DependencyProperty ToolTipProperty = FrameworkElement.ToolTipProperty.AddOwner(typeof(XHtmlTextBoxColumn));

		//string _toolTip;
		//public string ToolTip
		//{
		//   get { return _toolTip; }
		//   set { _toolTip = value; }
		//}

		private static readonly DependencyProperty BorderThicknessProperty = Control.BorderThicknessProperty.AddOwner(typeof(XHtmlTextBoxColumn));
		//internal Thickness BorderThickness
		//{
		//   //get { return (Thickness)(GetValue(BorderThicknessProperty)); }
		//   //set { SetValue(BorderThicknessProperty, value); }
		//   get;
		//   set;
		//}

		//private static readonly DependencyProperty ColumnHeaderProperty = DependencyProperty.Register("ColumnHeader", typeof(XHtmlDataGridColumnHeader), typeof(XHtmlTextBoxColumn));
		//public XHtmlDataGridColumnHeader ColumnHeader
		//{
		//   //get { return (XHtmlDataGridColumnHeader)(GetValue(ColumnHeaderProperty)); }
		//   //set { SetValue(ColumnHeaderProperty, value); }
		//   get;
		//   set;

		//}

		//public static readonly DependencyProperty IsZoomCallerProperty = DependencyProperty.Register("IsZoomCaller", typeof(bool), typeof(XHtmlTextBoxColumn));
		//public bool IsZoomCaller
		//{
		//   //get { return (bool)GetValue(IsZoomCallerProperty); }
		//   //set { SetValue(IsZoomCallerProperty, value); }
		//   get;
		//   set;

		//}


		//internal TextAlignment TextAlignment;
		//internal bool IsEnabled = true;

		private bool isNumerical, isHiddenByDefault;
		private int codePage, codePageBulle;
		private Visibility maVisibility;
		//public bool IsReadOnly;
		


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlTextBoxColumn class.
		/// </summary>
		public XHtmlTextBoxColumn(XHtmlDataGrid dataGrid)
		{
			if (dataGrid == null) throw new ArgumentNullException("dataGrid");
			DataGrid = dataGrid;

			Page = dataGrid.Page;
			ColumnType = ColumnType.Champ;
			Presentation = new XHtmlPresentation();
			ColumnHeader = new XHtmlDataGridColumnHeader();
			//!!!!!!!!!!Header = this;	// pour mettre la colonne en DataContext du Header (pour les bindings dans le style)

			string source = String.Format(CultureInfo.InvariantCulture, "[{0}].", dataGrid.Columns.Count);
			//§!!!!!!!!!!!base.Binding = new Binding(source + "Text");	// "base" permet d'éviter un pb en cas de surcharge de "Set_Binding"

			SetCellStyle(source);
		}
		#endregion Constructeur


		#region Lecture propriétés
		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		new public void ReadProperties(DVBuffer buffer)
		{
			ushort id;		//temporaire pour stockage des identifiants de propriétés (polices, couleurs, etc...)

			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.COLONNE_FIN)
			{
				switch (property)
				{
					#region header
					case ProprietesWpf.COLONNE_ENTETE_POLICE:						// paramètres habituels pour la police de l’en-tête
						buffer.Get(out id);
						ColumnHeader.idPolice = id; //  Application.Current.Resources["Font-" + id] as XHtmlFont;
						break;

					case ProprietesWpf.COLONNE_ENTETE_FOND:						// paramètres habituels pour la couleur de fond de l’en-tête
						buffer.Get(out id);
//						ColumnHeader.Background = Application.Current.Resources["Brush-" + id] as SolidColorBrush;
						ColumnHeader.idFond = id; // Application.Current.Resources["Brush-" + id] as SolidColorBrush;
						break;

					case ProprietesWpf.CODE_PAGE:										// Code page
						buffer.Get(out codePage);
						break;

					case ProprietesWpf.COLONNE_ENTETE_LIBELLE:					// libellé de l’en-tête (string)
						string headerText;
						buffer.GetStringCP(out headerText, codePage);
						ColumnHeader.Text = headerText;
						if (headerText.StartsWith("<hog>", StringComparison.OrdinalIgnoreCase))
						{
							//Ajout FA 13.05.2011, recupération des images, nouvelle mouture
							string command = headerText.Remove(0, 5);
							//!!!!!!!!!!!!!! XHtmlObjetGraphique.LoadHogImagesInCommand(command);

							ColumnHeader.HogCommand = command;
							ColumnHeader.IsHog = true;
						}
						else ColumnHeader.IsHog = false;
						ColumnHeader.IsImage = false;
						break;

					case ProprietesWpf.COLONNE_ENTETE_BITMAP_DEBUT:				// image d'entête colonne (pour rempalcer le texte)
						XHtmlImageFile imageFile = new XHtmlImageFile();
						imageFile.ReadProperties(buffer);
						// !!!!!!!!!!!!!!!! a voir ColumnHeader.Image = XHtmlImage.GetImage(imageFile);
						ColumnHeader.IsImage = true;
						break;

					case ProprietesWpf.COLONNE_ENTETE_IMAGE_FILTRE:				// colonne filtrée ou non
						byte filtered;
						buffer.Get(out filtered);
						ColumnHeader.IsFiltered = (filtered > 0);
						break;

					case ProprietesWpf.COLONNE_ENTETE_IMAGE_TRI:					// colonne triée ou non + ordre et sens de tri
						byte sorted, sortOrder, sortDirection;
						buffer.Get(out sorted);
						buffer.Get(out sortOrder);
						buffer.Get(out sortDirection);
						ColumnHeader.IsSorted = (sorted > 0);
						ColumnHeader.SortOrder = sortOrder;
						ColumnHeader.SortDescending = (sortDirection == 2);
						break;

					case ProprietesWpf.COLONNE_ENTETE_BITMAP_AVANT:				// image de gauche
						byte flagImageLeft;
						buffer.Get(out flagImageLeft);

						if (flagImageLeft == 0) ColumnHeader.LeftImage = null;	// on efface l'image
						else																		// on récupère l'image et on l'affecte
						{
							XHtmlImageFile leftImageFile = new XHtmlImageFile();
							leftImageFile.ReadProperties(buffer);
							//!!!!!!!!!!!!!!!!!! a voir ColumnHeader.LeftImage = XHtmlImage.GetImage(leftImageFile);
						}
						break;

					case ProprietesWpf.COLONNE_ENTETE_BITMAP_APRES:				// image de droite
						byte flagImageRight;
						buffer.Get(out flagImageRight);

						if (flagImageRight == 0) ColumnHeader.RightImage = null;	// on efface l'image
						else																		// on récupère l'image et on l'affecte
						{
							XHtmlImageFile rightImageFile = new XHtmlImageFile();
							rightImageFile.ReadProperties(buffer);
							//!!!!!!!!!!!!!!!!!!! a voir ColumnHeader.RightImage = XHtmlImage.GetImage(rightImageFile);
						}
						break;
					#endregion header

					#region colonne
					case ProprietesWpf.COLONNE_TRAIT_AUTOUR:						// Si trait entre les colonnes (avant et après colonne en cours)
						BorderThickness = new Thickness(1, 0, 1, 0);
						break;

					case ProprietesWpf.COLONNE_FLAG_AFFICHAGE:					//flag affichage (byte)
						byte flagDisplay;
						buffer.Get(out flagDisplay);

						// 1 = « Toujours » (la colonne est toujours affichée, l’utilisateur ne peut pas l’ôter)
						// 2 = « Oui par défaut » (la colonne est affichée au départ, l’utilisateur peut l’ôter)
						// 3 = « Non par défaut » (la colonne n’est pas affichée au départ, l’utilisateur peut l’ajouter)

						IsMandatory = (flagDisplay == 1);
						isHiddenByDefault = (flagDisplay == 3);
						break;

					case ProprietesWpf.COLONNE_LARGEUR:								// Largeur de la colonne (ushort)
						ushort width;
						buffer.Get(out width);
						Width = width + 2;
						//!!!!!!!!!!!!!if ((bool)Application.Current.Properties["IgnoreUserSettings"] || WidthIsAuto) Width = width + 2; // + 2 pour la bordure
						break;

					case ProprietesWpf.COLONNE_TYPE_FOND:							// paramètres habituels pour la couleur de fond de la colonne
						byte type;
						buffer.Get(out type);
//						if (type == 1) ColumnBackground = "HighlightedDataGridBackground";		// couleur windows
						// sinon : traité dans le "case ProprietesWpf.COLONNE_FOND"
						break;

					case ProprietesWpf.COLONNE_FOND:
						buffer.Get(out id);
						ColumnBackground = id;
						break;

					case ProprietesWpf.COLONNE_POLICE:								// paramètres habituels pour la police de la colonne
						buffer.Get(out id);
						ColumnFont = id; // "Font-" + id;
						break;

					case ProprietesWpf.CODE_PAGE_BULLE:								// Code page bulle
						buffer.Get(out codePageBulle);
						break;

					case ProprietesWpf.COLONNE_BULLE:								// Bulle sur la colonne (string)
						string toolTip;
						buffer.GetStringCP(out toolTip, codePageBulle);
						ToolTip = string.IsNullOrEmpty(toolTip) ? null : toolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;
					#endregion colonne

					#region Drag & Drop
					case ProprietesWpf.COLONNE_NOMS_ENREG_DONNEE:				// nom de l’enregistrement et de la donnée (2 string)
						string recordName, dataName;
						buffer.GetString(out recordName);
						buffer.GetString(out dataName);
						RecordName = recordName;
						DataName = dataName;
						break;

					case ProprietesWpf.COLONNE_INDICES_DONNEE:					// indices donnée (4 ushort)
						ushort index1, index2, index3, index4;
						buffer.Get(out index1);
						buffer.Get(out index2);
						buffer.Get(out index3);
						buffer.Get(out index4);
						Indexes = new int[] { index1, index2, index3, index4 };
						break;
					#endregion Drag & Drop

					case ProprietesWpf.CHAMP_DEBUT:
						ushort numCol;
						uint idCol;
						buffer.Get(out numCol);
						buffer.Get(out idCol);

						ReadSpecialProperties(buffer);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGridTextBoxColumn, property.ToString());
				}
				buffer.Get(out property);
			}

			SetVisibility(); // à la fin pour pouvoir tenir compte de l'attribut ET du choix de l'utilisateur

			/*/!!!!!!!!!!!!!!!!
			foreach (XHtmlRow row in ((XHtmlDataGrid)DataGridOwner).Rows)
				((XHtmlTextBoxCell)row[DataGridOwner.Columns.IndexOf(this)]).SetStyle();
			*/
		}

		/// <summary>
		/// Reads the column's "special" (unshared with the other column types) properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		private void ReadSpecialProperties(DVBuffer buffer)
		{
			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.CHAMP_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						Presentation.ReadProperties(buffer);
						break;

					case ProprietesWpf.PARAM_SAISIE_SEQUENCE:							// Point de séquence (ushort)
						ushort pointSequence;
						buffer.Get(out pointSequence);
						SeqPoint = pointSequence;
						break;

					case ProprietesWpf.PARAM_SAISIE_TABLE_ASSOCIEE:					// (Uniquement si le champ peut appeler un zoom)
						IsZoomCaller = true;
						break;

					case ProprietesWpf.PARAM_SAISIE_TABLE_ASSOCIEE_EXT:			// (Uniquement si le champ peut appeler un zoom)
						byte isZoomCaller;
						buffer.Get(out isZoomCaller);
						IsZoomCaller = (isZoomCaller != 0);
						break;

					case ProprietesWpf.OBJET_EN_AFFICHAGE:								// (Uniquement si le champ est en affichage seulement)
						IsReadOnly = true;
						break;

					case ProprietesWpf.CHAMP_NUMERIQUE:									// (Uniquement si le champ n'accepte que des valeurs numériques)
						isNumerical = true;
						break;

					case ProprietesWpf.BOUTONS_VALIDES_DEBUT:							// Boutons valides dans la page en cours
						ListOfValidButtons = new Collection<string>();
						buffer.Get(out property);
						while (property != ProprietesWpf.BOUTONS_VALIDES_FIN)
						{
							string buttonName;
							buffer.GetString(out buttonName);
							ListOfValidButtons.Add(buttonName);
							buffer.Get(out property);
						}
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGridTextBoxColumn, property.ToString());
				}
				buffer.Get(out property);
			}
			SetTextAlignment();	// impossible ailleurs car on a besoin de "Presentation" & "IsNumerical"
		}
		#endregion Lecture propriétés


		#region Fonctions
		private void SetCellStyle(string source)
		{
			/*
			CellStyle = new Style(typeof(DataGridCell), DataGrid.Resources["CellStyle"] as Style);
			CellStyle.Setters.Add(new EventSetter(UIElement.KeyDownEvent, (KeyEventHandler)KeyDownHandler));
			CellStyle.Setters.Add(new EventSetter(UIElement.DropEvent, (DragEventHandler)DropHandler));

			ElementStyle = new Style(typeof(TextBlock));
			EditingElementStyle = new Style(typeof(TextBox));

			// décorations
			ElementStyle.Setters.Add(new Setter(TextBlock.TextDecorationsProperty, new Binding(source + "TextDecorations") { Mode = BindingMode.OneWay }));

			// cadrage
			ElementStyle.Setters.Add(new Setter(TextBlock.TextAlignmentProperty, new Binding(source + "TextAlignment") { Mode = BindingMode.OneWay }));

			// padding (figé)
			ElementStyle.Setters.Add(new Setter(FrameworkElement.MarginProperty, new Thickness(3)));
			EditingElementStyle.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(0, 1, 0, 1)));

			// Longueur Maximale
			EditingElementStyle.Setters.Add(new Setter(TextBox.MaxLengthProperty, new Binding(source + "MaxLength") { Mode = BindingMode.OneWay }));

			// couleurs
			EditingElementStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(1.001))); // HACK pour que la couleur grise soit prise en compte
			EditingElementStyle.Setters.Add(new Setter(Control.BorderBrushProperty, Application.Current.Resources["DefaultBorderBrush"] as Brush));
			EditingElementStyle.Setters.Add(new Setter(TextBoxBase.SelectionBrushProperty, Application.Current.Resources["SelectionBrush"] as Brush));
			 * */
		}

		private void SetVisibility()
		{
			maVisibility = (Presentation.Visibilite == Visibilites.Cache) ? Visibility.Collapsed : Visibility.Visible;
			IsEnabled = (Presentation.Visibilite != Visibilites.Grise);
			IsHiddenForced = (maVisibility == Visibility.Collapsed);

			if (maVisibility == Visibility.Visible && isHiddenByDefault) maVisibility = Visibility.Collapsed;
		}

		private void SetTextAlignment()
		{
			switch (Presentation.Cadrage)
			{
				case Cadrage.Defaut: TextAlignment = (isNumerical) ? TextAlignment.Right : TextAlignment.Left; break;
				case Cadrage.Gauche: TextAlignment = TextAlignment.Left; break;
				case Cadrage.Droite: TextAlignment = TextAlignment.Right; break;
				case Cadrage.Centre: TextAlignment = TextAlignment.Center; break;
			}
		}
		#endregion Fonctions


		#region Ecouteurs
		private void KeyDownHandler(object sender, KeyEventArgs e)
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//string key = "";

			//// Touches de fonction (Fn)
			//if (e.Key == Key.System && e.SystemKey == Key.F10) key = "F10";
			//else if (XHtmlApplication.FnKeys.Contains(e.Key)) key = e.Key.ToString();

			//// Autres touches
			//else if (XHtmlTextBox.ValidKeys.Contains(e.Key) || (
			//         (Keyboard.Modifiers & ModifierKeys.Control) != 0
			//         && (Keyboard.Modifiers & ModifierKeys.Alt) == 0
			//         && XHtmlTextBox.ValidModifiedKeys.Contains(e.Key))
			//      )
			//   key = e.Key.ToString();

			//// Envoi des infos
			//if (!string.IsNullOrEmpty(key))
			//{
			//   e.Handled = true;
			//   DataGrid.SendCellKeyDown((DataGridCell)sender, key);
			//}
		}
		#endregion Ecouteurs


		private void DropHandler(object sender, DragEventArgs e)
		{
			/*
			e.Handled = true;

			FrameworkElement cell = sender as FrameworkElement;
			if (cell == null) return;

			DataGrid.RemoveAdorner(cell);

			if (e.Data == null) return; // drop vide

			var application = ((App)Application.Current).Appli;

			if (application.AttenteInput && Page.Window.ActiveControl is DataGrid) return; // drop en ListInput interdit

			// Drop fichier
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				application.StackOfWindows.Peek().SendDropFile(e.Data as DataObject);
				return;
			}

			// Drop tableau
			if (e.Data.GetDataPresent("DataGridDataFormat"))
			{
				DataGridDataContainer data = e.Data.GetData("DataGridDataFormat") as DataGridDataContainer;

				XHtmlRow row = cell.DataContext as XHtmlRow;

				if (row != null) DataGrid.SendDropEvent(data, DataGrid.Columns.IndexOf(this), DataGrid.Rows.IndexOf(row), e.GetPosition(cell).Y / cell.ActualHeight);
				else DataGrid.SendDropEvent(data);
			}
			 * */
		}
	}

}