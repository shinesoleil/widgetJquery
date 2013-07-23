using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

using Microsoft.Win32;

using Divalto.Systeme.DVOutilsSysteme;
using System.Windows.Documents;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Text;
using System.Web;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlDataGrid = Un tableau
	/// </summary>
	/// 

	public partial class XHtmlDataGrid : FrameworkElement, IXHtmlEditableObject
	{
		public List<XHtmlGenericColumn> Columns = new List<XHtmlGenericColumn>();
//		public List<UneColonne> Columns = new List<UneColonne>();

		public double RowHeaderActualWidth { get; set; }
		public object CurrentItem { get; set; }


		public uint Id { get; set; }
		public ushort SeqPoint { get; set; }
		public XHtmlPage Page { get; set; }
		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }
		public Collection<string> ListOfValidButtons { get; private set; }

		#region Dependency properties
		private static readonly DependencyProperty VerticalScrollBarToolTipProperty =
			DependencyProperty.Register("VerticalScrollBarToolTip", typeof(string), typeof(XHtmlDataGrid));


		public string VerticalScrollBarToolTip;
		//internal string VerticalScrollBarToolTip
		//{
		//   get { return (string)(GetValue(VerticalScrollBarToolTipProperty)); }
		//   set { SetValue(VerticalScrollBarToolTipProperty, value); }
		//}

		//private static readonly DependencyProperty HorizontalScrollBarToolTipProperty =
		//   DependencyProperty.Register("HorizontalScrollBarToolTip", typeof(string), typeof(XHtmlDataGrid));

		internal string HorizontalScrollBarToolTip;
		//internal string HorizontalScrollBarToolTip
		//{
		//   get { return (string)(GetValue(HorizontalScrollBarToolTipProperty)); }
		//   set { SetValue(HorizontalScrollBarToolTipProperty, value); }
		//}


		//private static readonly DependencyProperty SelectAllButtonToolTipProperty =
		//    DependencyProperty.Register("SelectAllButtonToolTip", typeof(object), typeof(XHtmlDataGrid), new UIPropertyMetadata(null));


		private object SelectAllButtonToolTip;
		//private object SelectAllButtonToolTip
		//{
		//   get { return GetValue(SelectAllButtonToolTipProperty); }
		//   set { SetValue(SelectAllButtonToolTipProperty, value); }
		//}


		//private static readonly DependencyProperty SelectAllButtonContentProperty =
		//	 DependencyProperty.Register("SelectAllButtonContent", typeof(object), typeof(XHtmlDataGrid), new UIPropertyMetadata(null));

		private object SelectAllButtonContent;
		//private object SelectAllButtonContent
		//{
		//   get { return GetValue(SelectAllButtonContentProperty); }
		//   set { SetValue(SelectAllButtonContentProperty, value); }
		//}

		public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(XHtmlDataGrid));

		public bool IsActive;
		//public bool IsActive
		//{
		//   get { return (bool)GetValue(IsActiveProperty); }
		//   set { SetValue(IsActiveProperty, value); }
		//}
		#endregion Dependency properties

		internal ObservableCollection<XHtmlRow> Rows = new ObservableCollection<XHtmlRow>(); // liste des lignes du tableau (les lignes contiennent des XHtmlObjets)
		XHtmlRow uneRow;

		internal bool IsEditing;
		internal byte PageNum, CurrentLineOptions;
		internal uint ItemsOffset = 1;
		internal ushort StopPoint, RowAlternation;
		internal int CodepageBulleAscHor, CodepageBulleAscVer;
		internal DataGridCellInfo CurrentCellInfo;

		private readonly List<bool> columnDisplayOptions = new List<bool>();
		private readonly List<int>
			columnDisplayIndexes = new List<int>(),
			columnWidths = new List<int>();

		private bool enableLineCreation, useCustomHeaderColor, useFirstColumnWidth, showActive = true;
		private string name, mask;
		private uint itemsOffsetTemp;
		private int codePage;

		private ScrollBar verticalScrollBar;
		private bool verticalScrollBarForced;
		private double scrollBarOldValue;

		private ContextMenu columnsMenu;
		private MouseButtonEventArgs mouseButtonEventArgs;	// pour enregistrer les paramètres du mouseDown et les comparer au mouseUp pour constituer un Click


		public DataGridGridLinesVisibility GridLinesVisibility { get; set; }
		public Style RowHeaderStyle { get; set; }
		public Style ColumnHeaderStyle { get; set; }

		// Drag & Drop
		private bool
			isDragging,
			allowDrag = true,
			allowDrop = true,
			allowExternalDrop,
			allowEmptyDrop,
			dataMove,
			dataInsert,
			dragRow,
			dropRow,
			isList;
		private string dragName, dragInfo;
		private string[] dropNames;
		private uint listId;
		private Point startPoint;
		private Adorner adorner;
		private DataObject draggedData;
		public double ColumnHeaderHeight;
		public double RowHeight;
		public int RowHeaderWidth;
		public Thickness BorderThickness;
		public bool CanUserReorderColumns;
		public ushort FrozenColumnCount;
		public DataGridHeadersVisibility HeadersVisibility;
		private bool CanUserResizeColumns;
		private DependencyProperty BackgroundProperty;
		private bool CanUserAddRows;
		private DataGridCellInfo CurrentCell;


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlDataGrid class.
		/// </summary>
		/// <param name="page">page containing the XHtmlDataGrid instance</param>
		public XHtmlDataGrid(XHtmlPage page)
		{
			//!!!!!!!!!!!!!!!!InitializeComponent();

			Page = page;
//!!!!!!!!!!!!!!			ItemsSource = Rows;
//!!!!!!!!!!!!!!			RowHeaderTemplateSelector = new RowHeaderTemplateSelector();

			#region Ecouteurs
			/* !!!!!!!!!!!!!!!!!!!!!!!!
			LoadingRow += LoadingRowHandler; // pour la personnalisation du bouton de création de nouvelle ligne
			PreparingCellForEdit += PreparingCellForEditHandler; // pour donner le focus à la case dans une dataGridTemplateColumn
			CellEditEnding += CellEditEndingHandler; // pour réaffecter le bon itemsSource à une comboBoxCell le cas échéant
			SizeChanged += SizeChangedHandler;
			ColumnReordered += ColumnReorderedHandler;

			PreviewMouseWheel += PreviewMouseWheelHandler;
			PreviewMouseDown += previewMouseDownHandler;

			DragEnter += DragEnterHandler;		// pour ajouter l'adorner le cas échéant
			DragOver += DragOverHandler;			// pour déplacer l'adorner le cas échéant
			DragEnter += DragEnterOverHandler;	// pour le curseur
			DragOver += DragEnterOverHandler;	// pour le curseur
			DragLeave += DragLeaveHandler;		// pour supprimer l'adorner le cas échéant
			Drop += DropHandler;

			PreviewMouseUp += (s, e) => draggedData = null;
			MouseLeave += (s, e) => draggedData = null;
			 * */
			#endregion Ecouteurs

			LinkRowHeightsToUserChange(); // pour redimensionner toutes les lignes en même temps.

			/*
			ApplyTemplate();
			ScrollViewer scrollViewer = Template.FindName("DG_ScrollViewer", this) as ScrollViewer;
			if (scrollViewer == null) return;
			scrollViewer.ApplyTemplate();

			verticalScrollBar = scrollViewer.Template.FindName("PART_VerticalScrollBar", scrollViewer) as ScrollBar;
			if (verticalScrollBar == null) return;
			verticalScrollBar.PreviewMouseUp += ScrollBarPreviewMouseUpHandler;			// pour ne traiter le changement de valeur qu'au relachement de la souris
			verticalScrollBar.PreviewMouseDown += ScrollBarPreviewMouseDownHandler;
			verticalScrollBar.ApplyTemplate();
			*/
			// on ajoute le gestionnaire de Click sur les repeatButton de la scrollBar vertical pour pouvoir les piloter manuellement
			/* !!!!!!!!!!!!
			foreach (RepeatButton repeatButton in TreeHelper.GetControlsDecendant<RepeatButton>(verticalScrollBar))
				repeatButton.Click += RepeatButtonClickHandler;
			 */
		}
		#endregion Constructeur


		#region Lecture propriétés
		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		public void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;


			StringBuilder options = new StringBuilder();
			StringBuilder nomsColonnes = new StringBuilder();
			StringBuilder modelesColonnes = new StringBuilder();


			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_FIN)
			{
				switch (property)
				{

					#region propriétés tableau
					case ProprietesWpf.TABLEAU_IDENT_COMPLET:
						// ces trois valeurs servent au calcul des clés dans le registre pour la sauvegarde de l’ordre et des positions/tailles des colonnes :
						buffer.GetString(out mask);							// Nom du masque (string)
						buffer.Get(out PageNum);								// Numéro de page (byte)
						buffer.GetString(out name);							// Nom du tableau (string)
						GetUserSettings();
						break;

					case ProprietesWpf.TABLEAU_LISTE:									// uniquement si tableau de type "liste" (pour le search)
						isList = true;
//!!!!!!!!						Template = Application.Current.Resources["DataGridListTemplate"] as ControlTemplate;
//						ApplyTemplate();
//						ScrollViewer scrollViewer = Template.FindName("DG_ScrollViewer", this) as ScrollViewer;
//						scrollViewer.ApplyTemplate();
//						verticalScrollBar = scrollViewer.Template.FindName("PART_VerticalScrollBar", scrollViewer) as ScrollBar;
//						verticalScrollBar.PreviewMouseUp += ScrollBarPreviewMouseUpHandler;			// pour ne traiter le changement de valeur qu'au relachement de la souris
//						verticalScrollBar.PreviewMouseDown += ScrollBarPreviewMouseDownHandler;
//						verticalScrollBar.ApplyTemplate();

						// on ajoute le gestionnaire de Click sur les repeatButton de la scrollBar vertical pour pouvoir les piloter manuellement
						/*!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
						foreach (RepeatButton repeatButton in TreeHelper.GetControlsDecendant<RepeatButton>(verticalScrollBar))
							repeatButton.Click += RepeatButtonClickHandler;
						*/

						HeadersVisibility = DataGridHeadersVisibility.Column; // pas de colonne de contrôle en mode liste
						break;

					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						if (isList) BorderThickness = new Thickness(0);
						break;

					case ProprietesWpf.TABLEAU_ARRET:
						buffer.Get(out StopPoint);											// Numéro de point d’arrêt (ushort)
						break;

					case ProprietesWpf.TABLEAU_TRAIT_ENTRE_LIGNES:					// Si flag "Trait de séparation des lignes" 
						GridLinesVisibility = DataGridGridLinesVisibility.Horizontal;
						break;

					case ProprietesWpf.TABLEAU_SANS_COLONNE_CONTROLE:				// Si flag "Cacher la colonne de contrôle"
						HeadersVisibility = DataGridHeadersVisibility.Column;
						break;

					case ProprietesWpf.TABLEAU_LARGEUR_COLONNE_CONTROLE:			// Largeur de la colonne de contrôle (ushort)
						ushort rowHeaderWidth;
						buffer.Get(out rowHeaderWidth);
						RowHeaderWidth = (HeadersVisibility == DataGridHeadersVisibility.Column) ? 0 : rowHeaderWidth;
						break;

					case ProprietesWpf.TABLEAU_HAUTEUR_LIGNES:						// Hauteur des lignes (ushort)
						ushort rowHeight;
						buffer.Get(out rowHeight);
						if (double.IsNaN(RowHeight)) RowHeight = rowHeight;
						break;

					case ProprietesWpf.TABLEAU_HAUTEUR_ENTETE:						// Hauteur (initiale) de l’en-tête (ushort)
						ushort headerHeight;
						buffer.Get(out headerHeight);
						if (double.IsNaN(ColumnHeaderHeight) && !isList) ColumnHeaderHeight = headerHeight;
						break;

					case ProprietesWpf.TABLEAU_COULEUR_COLONNE_UN:					// SI flag "Prendre la couleur de la colonne 1"
						useCustomHeaderColor = true;
						break;

					case ProprietesWpf.TABLEAU_ZONAGE:									// Info pour le zonage (short)
						buffer.Get(out RowAlternation);
						//  0 = zonage par défaut (choisi par l’utilisateur au menu système)
						// -1 = pas de zonage
						//	autre = numéro de colonne (inverser le zonage à chaque fois que cette colonne change de valeur)
						break;

					case ProprietesWpf.TABLEAU_OPTION_LIGNE_COURANTE:				// options de la ligne courante (byte)
						buffer.Get(out CurrentLineOptions);
						break;

					case ProprietesWpf.TABLEAU_OPTION_CREATION:						// options de création de nouvelle ligne (byte)
						byte optionCreation;
						buffer.Get(out optionCreation);
						enableLineCreation = (optionCreation == 1);
						break;

					case ProprietesWpf.TABLEAU_PAS_DE_HALO_SI_ACTIF:				// flag pour oranger le tableau actif ou non
						BorderThickness = new Thickness(0); // toujours APRES la présentation, pour pouvoir supprimer la bordure dans ce cas (sinon la présentation raz)
						showActive = false;
						break;
					#endregion propriétés tableau


					#region scrollBars
					case ProprietesWpf.CODE_PAGE_BULLE_ASCHOR:						// Code page asc. hor.
						buffer.Get(out CodepageBulleAscHor);
						break;

					case ProprietesWpf.CODE_PAGE_BULLE_ASCVER:						// Code page asc. ver.
						buffer.Get(out CodepageBulleAscVer);
						break;

					case ProprietesWpf.TABLEAU_ASCHOR_BULLE:
						string horizontalScrollBarToolTip;
						buffer.GetStringCP(out horizontalScrollBarToolTip, CodepageBulleAscHor);		// Bulle de l’ascenseur horizontal (string)
						HorizontalScrollBarToolTip = string.IsNullOrEmpty(horizontalScrollBarToolTip) ? null : horizontalScrollBarToolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;

					case ProprietesWpf.TABLEAU_ASCVER_BULLE:
						string verticalScrollBarToolTip;
						buffer.GetStringCP(out verticalScrollBarToolTip, CodepageBulleAscVer);		// Bulle de l’ascenseur vertical (string)
						VerticalScrollBarToolTip = string.IsNullOrEmpty(verticalScrollBarToolTip) ? null : verticalScrollBarToolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;

					case ProprietesWpf.TABLEAU_ASCVER_SET:								// positionnement ascenceur tableau
						uint scrollMin, scrollMax, scrollSmallChange, scrollLargeChange, scrollValue;
						buffer.Get(out scrollMin);				//Position début (uint)
						buffer.Get(out scrollMax);				//Position fin (uint)
						buffer.Get(out scrollSmallChange);	//Pas (uint)
						buffer.Get(out scrollLargeChange);	//Pas « page » (uint)
						buffer.Get(out scrollValue);			//Position courante (uint)

						// attention au problème aux limites, comme on parle de cellules et non de distances : scrollmin = scrollmin - 1 (on compte à partir de zéro)
						// mais scrollMax reste inchangé pour avoir un total de taille réelle = taille en nombre de cellules + 1 (problème de clôture / piquets)
						verticalScrollBar.Set(scrollMin - 1, scrollMax, scrollSmallChange, scrollLargeChange, scrollValue - 1, GetRowCount());
						break;

					case ProprietesWpf.TABLEAU_ASCVER_VALIDER:			// invalidation de la scrollbar
						byte valide;
						buffer.Get(out valide);	//Flag valide (1) ou invalide (0) (byte)
						verticalScrollBar.SetEnable(valide == 1, verticalScrollBarForced, verticalScrollBarForced = (valide == 0));
						break;
					#endregion scrollBars


					#region colonnes
					case ProprietesWpf.COLONNE_DEBUT:									// Lecture des propriétés des colonnes
						ReadColumnProperties(buffer);
						break;

					case ProprietesWpf.TABLEAU_COLONNES_PERMANENTES:				// Nombre de colonnes figées (ushort)
						ushort frozenColumnsCount;
						buffer.Get(out frozenColumnsCount);
						FrozenColumnCount = frozenColumnsCount;
						break;

					case ProprietesWpf.TABLEAU_COLONNES_NON_DEPLACABLES:			// uniquement si les colonnes dans leur ensemble ne sont pas déplaçables
						CanUserReorderColumns = false;
						break;

					case ProprietesWpf.TABLEAU_COLONNE_UN_TAILLE_TOTALE:			// Si flag "Prendre la taille du tableau pour la colonne 1"
						useFirstColumnWidth = true;
						break;
					#endregion colonnes


					#region Drag & Drop
					case ProprietesWpf.TABLEAU_OPTIONS_DEBUT:							// XmeListSetOptions
						ReadDragDropOptions(buffer);
						break;

					case ProprietesWpf.TABLEAU_IDENT_LISTE:							// identifiant diva de la liste correspondant au tableau (uint)
						buffer.Get(out listId);
						break;
					#endregion Drag & Drop


					case ProprietesWpf.TABLEAU_RAZ_LIGNES:								// Pour vider le tableau le cas échéant
						// SelectedItem = null sur tous les multichoix images, sinon memory leak !!
						//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
						//foreach (var cell in Rows.SelectMany(row => row.OfType<XHtmlComboBoxCell>().Where(c => c.ImageComboBox)))
						//   cell.SelectedItem = null;
						Rows.Clear();
						break;

					case ProprietesWpf.BOUTONS_VALIDES_DEBUT:							// Boutons valides dans le tableau en cours
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
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
				}

				buffer.Get(out property);
			}

			SetLayoutProperties(); // Mise à jour du Layout

		}

		/// <summary>
		/// Reads a column's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		private void ReadColumnProperties(DVBuffer buffer)
		{
			ushort idColumn;
			buffer.Get(out idColumn);

			if (idColumn == 0) // Colonne d'état
			{
				int codepageBulle = 0;
				ProprietesWpf property;
				buffer.Get(out property);
				while (property != ProprietesWpf.COLONNE_FIN)
				{
					switch (property)
					{
						case ProprietesWpf.CODE_PAGE_BULLE:								// Code page
							buffer.Get(out codepageBulle);
							break;

						case ProprietesWpf.COLONNE_BULLE:
							string toolTip;
							buffer.GetStringCP(out toolTip, codepageBulle);
							SelectAllButtonToolTip = string.IsNullOrEmpty(toolTip) ? null : toolTip.Replace("|", "\n"); // "|" = multi-ligne
							break;

						case ProprietesWpf.COLONNE_ENTETE_BITMAP_AVANT:
							byte flagBitmapLeft;
							buffer.Get(out flagBitmapLeft);

							Image selectAllButtonImage;

							if (flagBitmapLeft == 0) selectAllButtonImage = null;	// on efface l'image
							else																	// on récupère l'image et on l'affecte
							{
								XHtmlImageFile imageFile = new XHtmlImageFile();
								imageFile.ReadProperties(buffer);
								selectAllButtonImage = new Image
								{
									// !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! Source = XHtmlImage.GetImage(imageFile),
									Stretch = Stretch.Uniform,
									StretchDirection = StretchDirection.DownOnly
								};
							}
							SelectAllButtonContent = selectAllButtonImage;
							break;

						default:
							throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
					}
					buffer.Get(out property);
				}
			}

			else
			{
				byte nature;
				buffer.Get(out nature);

				switch ((ColumnType)nature)
				{
					case ColumnType.Champ: 
						ProcessColumn<XHtmlTextBoxColumn>(idColumn, buffer);
						break;
				
					/*!!!!!!!!!!!!!!!!
					case ColumnType.ChampCache: ProcessColumn<XHtmlPasswordBoxColumn>(idColumn, buffer); break;
					case ColumnType.ChampDate: ProcessColumn<XHtmlDatePickerColumn>(idColumn, buffer); break;
					case ColumnType.ObjetGraphique: ProcessColumn<XHtmlHogColumn>(idColumn, buffer); break;
					case ColumnType.Multichoix: ProcessColumn<XHtmlComboBoxColumn>(idColumn, buffer); break;
					case ColumnType.Case: ProcessColumn<XHtmlCheckBoxColumn>(idColumn, buffer); break;
					case ColumnType.Image: ProcessColumn<XHtmlImageColumn>(idColumn, buffer); break;
					case ColumnType.Arbre: ProcessColumn<XHtmlTreeColumn>(idColumn, buffer); break;
					 */
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="idColumn"></param>
		/// <param name="buffer"></param>
		private void ProcessColumn<T>(ushort idColumn, DVBuffer buffer) where T : XHtmlGenericColumn, IXHtmlDataGridColumn
		{
			T column = (T)GetColumn(idColumn);
			if (column == null)
			{
				column = (T)Activator.CreateInstance(typeof(T), this);
				column.Id = idColumn;
				Columns.Add(column);
			}
			column.ReadProperties(buffer);
		}

		/// <summary>
		/// Reads the options for drag & drop
		/// </summary>
		/// <param name="buffer">DVBuffer where the options are read</param>
		private void ReadDragDropOptions(DVBuffer buffer)
		{
			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_OPTIONS_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.TABLEAU_OPTIONS_DRAG:
						byte dragMode, dragEffect;
						buffer.Get(out dragMode);
						buffer.Get(out dragEffect);	// cellule = 1, ligne = 2, objet HOG = 3 (objet HOG pas possible en l'état)
						buffer.GetString(out dragName);
						buffer.GetString(out dragInfo);

						allowDrag = (dragMode > 0);
						dragRow = (dragEffect == 2);
						break;

					case ProprietesWpf.TABLEAU_OPTIONS_DROP:
						byte dropMode, dropExt, dropEmpty, dropEffect, dropInsert, dropMove;
						string dropNamesList;
						buffer.Get(out dropMode);
						buffer.Get(out dropExt);
						buffer.Get(out dropEmpty);
						buffer.Get(out dropEffect);	// cellule = 1, ligne = 2, objet HOG = 3 (objet HOG pas possible en l'état)
						buffer.Get(out dropInsert);
						buffer.Get(out dropMove);
						buffer.GetString(out dropNamesList);

						allowDrop = (dropMode > 0);
						allowExternalDrop = (dropExt > 0);
						allowEmptyDrop = (dropEmpty > 0);
						dropNames = dropNamesList.Split(',').Except(new string[] { "" }).ToArray();
						dataMove = (dropMove > 0);
						dataInsert = (dropInsert > 0);
						dropRow = (dropEffect == 2);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region remplissage
		/// <summary>
		/// Fills the dataGrid
		/// </summary>
		/// <param name="buffer">global DVBuffer for client/server data exchange</param>
		internal void Fill(DVBuffer buffer)
		{
			ProprietesWpf property;
			ushort rowIndex;		// temporaire pour stockage des numéros de colonne et de ligne

			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_REMPLISSAGE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.MULTICHOIX_LIBELLES:							// Remplissage des libellés pour une colonne multichoix
						FillComboBoxItemsSource(buffer);
						break;

					case ProprietesWpf.TABLEAU_LIGNES_DEBUT:							// Remplissage des lignes
						FillDataGridItemsSource(buffer);
						break;

					case ProprietesWpf.TABLEAU_LIGNE_MARQUEE_DEBUT:					// Marquage (= sélection) d'une ligne
						buffer.Get(out rowIndex); // Index de la ligne à marquer (ushort)

						byte selected;
						buffer.Get(out selected); // Flag « ligne marquée » (0 ou 1 : byte)

						if (selected == 1) Rows[rowIndex - 1].ReadSelectionStyle(buffer);
						Rows[rowIndex - 1].IsSelected = (selected == 1);
						break;

					case ProprietesWpf.TABLEAU_LIGNE_COURANTE:						// Affectation de la ligne courante
						buffer.Get(out rowIndex); // Index de la ligne courante (ushort) (début à 1, 0 = ne sélectionner aucune ligne)
						CurrentItem = (0 < rowIndex && rowIndex < Rows.Count + 1) ? Rows[rowIndex - 1] : null;
						break;

					case ProprietesWpf.TABLEAU_OFFSET_DEBUT:							// indice du premier item dans la collection source (ushort) (pour calculer décalage pour le zonage)
						buffer.Get(out itemsOffsetTemp);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
				}

				buffer.Get(out property);
			}

			// mise à jour des styles de lignes (lignes marquées + zonage + ligne courante)
			foreach (XHtmlRow row in Rows) row.SetStyle();

		}

		/// <summary>
		/// Fills a dataGridComboBoxColumn's itemsSource
		/// </summary>
		/// <param name="buffer">global DVBuffer for client/server data exchange</param>
		private void FillComboBoxItemsSource(DVBuffer buffer)
		{
			/* !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			ushort colIndex;

			ushort numberOfItems;
			int codepageChoix;
			buffer.Get(out colIndex);
			buffer.Get(out codepageChoix);
			buffer.Get(out numberOfItems);

			XHtmlComboBoxColumn column = Columns[colIndex - 1] as XHtmlComboBoxColumn;
			if (column == null) throw new ArgumentException("la colonne n°" + colIndex + " n'est pas une colonne multichoix");

			//construction de la liste d'items : attention, il faut passer par une liste intermédaire (sinon plantage au changement)
			BindingList<ComboBoxItem> comboBoxItems = new BindingList<ComboBoxItem>();

			for (int k = 0; k < numberOfItems; k++)
			{
				ComboBoxItem item = new ComboBoxItem();
				if (column.ImageComboBox)
				{
					ProprietesWpf property;
					buffer.Get(out property); // MULTICHOIX_IMAGE
					XHtmlImageFile imageFile = new XHtmlImageFile();
					imageFile.ReadProperties(buffer);
					item.Content = new Image { Source = XHtmlImage.GetImage(imageFile), Stretch = Stretch.None };
				}
				else
				{
					string itemContent;
					buffer.GetStringCP(out itemContent, codepageChoix);
					item.Content = itemContent;
				}
				comboBoxItems.Add(item);
			}

			//	Sauve l'index courant actuel pour toutes les lignes
			foreach (XHtmlComboBoxCell wcell in Rows.Select(row => row[colIndex - 1] as XHtmlComboBoxCell))
				wcell.WorkSelectedIndex = wcell.SelectedIndex;

			column.ComboBoxItems = comboBoxItems;

			// Restaure les index courants (cassés par l'affectation précédente)
			foreach (XHtmlComboBoxCell wcell in Rows.Select(row => row[colIndex - 1] as XHtmlComboBoxCell))
				wcell.SelectedIndex = wcell.WorkSelectedIndex;
			 * 
			 * */
		}

		/// <summary>
		/// Fills the dataGrid's ItemsSource collection
		/// </summary>
		/// <param name="buffer">global DVBuffer for client/server data exchange</param>
		private void FillDataGridItemsSource(DVBuffer buffer)
		{
			// TEST starts the stopwatch
			//var sw = Stopwatch.StartNew();
			//UpdateLayout();

			ProprietesWpf property;
			ushort rowIndex;					// temporaire pour stockage des numéros de colonne et de ligne

			buffer.Get(out property);
			while (property != ProprietesWpf.TABLEAU_LIGNES_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.TABLEAU_LIGNE_DEBUT:				// Remplissage d'une ligne donnée
						byte operation;											// type d'opération (insertion/modification)
						buffer.Get(out operation);								// 1 = insertion avant ou ajout fin; 2 = remplacement (byte)
						buffer.Get(out rowIndex);								// Index de la ligne à remplir (ushort)

						ItemsOffset = itemsOffsetTemp;						// pour la gestion du zonage

//						if (operation == 1) // Insertion (ou ajout si rowIndex = listOfRows.Count)
//							Rows.Insert(rowIndex++, new XHtmlRow(this));	// le ++ est obligatoire pour mettre le rowIndex à jour en vue de remplir la bonne ligne ensuite

//						Rows[rowIndex - 1].Fill(buffer);						// remplissage de la ligne




						this.uneRow = new XHtmlRow(this);
						uneRow.Operation = (XHTmlTableauOperationLigne)operation;
						uneRow.IndicePourInsertion = rowIndex;

						if (operation == 1)
							rowIndex++;

						uneRow.Indice = rowIndex;
						
						Rows.Add(uneRow);
						uneRow.Fill(buffer);



						//TODISCUSS affichage tableau ligne par ligne ?
						//Action emptyDelegate = delegate() { };
						//Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, emptyDelegate);
						break;

					case ProprietesWpf.TABLEAU_LIGNES_SUPPRIMER:			// suppression de lignes
						ushort nblig;
						buffer.Get(out rowIndex);
						buffer.Get(out nblig);
						if (nblig == 0) // suppression des dernières lignes
						{
							// SelectedItem = null sur tous les multichoix images, sinon memory leak !!
							//!!!!!!!!!!!!!!!!!!!
							/*
							for (int i = rowIndex - 1; i < Rows.Count; i++)
								foreach (var cell in Rows[i].OfType<XHtmlComboBoxCell>().Where(c => c.ImageComboBox))
									cell.SelectedItem = null;
							*/
//!!!!!!!!!!							Rows.RemoveRange(rowIndex - 1, Rows.Count - rowIndex + 1);
						}
						else // suppression de nblig lignes (en pratique, rowIndex = 1 => suppression des nblig premières lignes)
						{
							// SelectedItem = null sur tous les multichoix images, sinon memory leak !!
							/* !!!!!!!!!!!!!!!!!!!!!!
							for (int i = rowIndex - 1; i < nblig; i++)
								foreach (var cell in Rows[i].OfType<XHtmlComboBoxCell>().Where(c => c.ImageComboBox))
									cell.SelectedItem = null;
							*/

	//!!!!!!!!!!!!!!!						Rows.RemoveRange(rowIndex - 1, nblig);
						}
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
				}

				ItemsOffset = itemsOffsetTemp;

				buffer.Get(out property);
			}

			SetCanUserAddRows();

			// TEST stops the stopwatch and writes the elapsed time.
			//sw.Stop();
			//Console.WriteLine(sw.Elapsed);

			// TEST affichage uniquement après remplissage (updatelayout forcé)
			//var sw2 = Stopwatch.StartNew();
			//UpdateLayout();
			//Action emptyDelegate = delegate() { };
			//Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Render, emptyDelegate);
			//sw2.Stop();
			//Console.WriteLine(sw2.Elapsed);
		}
		#endregion remplissage


		#region input
		/// <summary>
		/// Reads the buffer to give the input to the proper object
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		/// <returns>Object getting the input</returns>
		internal void ReadInput(DVBuffer buffer)
		{
			ushort rowIndex, colIndex;

			buffer.Get(out rowIndex);
			XHtmlRow row = Rows[rowIndex - 1];

			ProprietesWpf property;
			buffer.Get(out property);
			switch (property)
			{

				case ProprietesWpf.CASE_A_COCHER_DEBUT:
				case ProprietesWpf.CHAMP_DEBUT:
				case ProprietesWpf.CHAMP_CACHE_DEBUT:
				case ProprietesWpf.CHAMP_DATE_DEBUT:
					colIndex = ProcessInput(row, buffer);
					break;

				// traitement différent pour la comboBoxCell par rapport aux autres types : on ne met pas à jour la cellule dans la source directement,
				// c'est fait lors de la validation (passage à une autre cellule ou validation de la ligne), à cause du changement potentiel de liste source
				#region MultiChoix
				case ProprietesWpf.MULTICHOIX_DEBUT:
					uint id;

					buffer.Get(out colIndex);			// Index de la cellule à remplir, au sein de la ligne courante (ushort)
					buffer.Get(out id);					// Identifiant de l'objet représentant la cellule (uint)

/*!!!!!!!!!!!!!!!!!!
					XHtmlComboBoxCell comboBoxCell = (XHtmlComboBoxCell)row[colIndex - 1];
					comboBoxCell.ComboBoxItems = ((XHtmlComboBoxColumn)Columns[colIndex - 1]).ComboBoxItems;
					comboBoxCell.ReadProperties(buffer);

					ScrollIntoView(row, Columns[colIndex - 1]);
					CurrentCell = new DataGridCellInfo(row, Columns[colIndex - 1]);

					var cell = GetDataGridCell(rowIndex - 1, colIndex - 1);
					if (cell == null) // ne devrait jamais arriver, mais arrive quand même lorsque l'affichage est trop lent
					{
						UpdateLayout();
						cell = GetDataGridCell(rowIndex - 1, colIndex - 1);
					}

					BeginEdit();

					ComboBox comboBox = (ComboBox)cell.Content;
					comboBox.SetCurrentValue(ItemsSourceProperty, comboBoxCell.ComboBoxItems);

					// MAJ de la valeur directement dans la cellule
					if (comboBoxCell.SelectedIndex < comboBoxCell.ComboBoxItems.Count)
						comboBox.SetCurrentValue(SelectedIndexProperty, comboBoxCell.SelectedIndex);

					if (comboBoxCell.AutoOpen) comboBox.IsDropDownOpen = true;
 */
					break;
				#endregion MultiChoix

				default:
					throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGrid, property.ToString());
			}

			IXHtmlDataGridColumn column = Columns[colIndex - 1] as IXHtmlDataGridColumn;
//!!!!!!!!!!!!!			XHtmlTreeColumn.ResetCircles(Page.Window);
//!!!!!!!!!!!!!			XHtmlGroupBox.ResetActiveGroup();

			//!!!!!!!!!!!!!				var application = ((App)Application.Current).Appli;
			//!!!!!!!!!!!!!	application.ManageValidMenuItems(column);
			//!!!!!!!!!!!!!	application.ManageValidToolBarItems(column);
			//!!!!!!!!!!!!!	application.ManageValidButtons(column);
			//!!!!!!!!!!!!!	application.ManageValidDataGrids(Page.Id);
			//!!!!!!!!!!!!!	SetIsActive(true); // nécessairement APRES le ManageValidDataGrids() car ce dernier RAZ toutes les DataGrids affichées

			//!!!!!!!!!!!!!	application.AttenteInput = true;	// pas avant, sinon pb de notif sur les multichoix dans le cas d'une itemsSource qui change à l'input
			Page.Window.ActiveControl = this;	// obligatoire ici car on ne passe pas par une fonction Focus() comme dans le cas des autres contrôles
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="row"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
		private ushort ProcessInput(XHtmlRow row, DVBuffer buffer)
		{
			//ushort colIndex;
			//uint id;	// variable de stockage temporaire des identifiants d'objets

			//buffer.Get(out colIndex);			// Index de la cellule à remplir, au sein de la ligne courante (ushort)
			//buffer.Get(out id);					// Identifiant de l'objet représentant la cellule (uint)

			//row[colIndex - 1].ReadProperties(buffer);

			//ScrollIntoView(row, Columns[colIndex - 1]);
			//CurrentCell = new DataGridCellInfo(row, Columns[colIndex - 1]);
			//BeginEdit();

			//return colIndex;
			return 0;
		}

		private void ScrollIntoView(XHtmlRow row, DataGridColumn dataGridColumn)
		{
			throw new NotImplementedException();
		}
		#endregion input


		#region fonctions
		public void BeginEdit()
		{
			IsEditing = true;
			CurrentCellInfo = CurrentCell;
		//	base.BeginEdit();
		}

		public void CommitEdit(DataGridEditingUnit editingUnit, bool exitEditingMode)
		{
			IsEditing = false;
		//	base.CommitEdit(editingUnit, exitEditingMode);
		}

		/// <summary>
		/// redimensionnement de toutes les lignes lors du redimensionnement d'une ligne, si option activée
		/// </summary>
		private void LinkRowHeightsToUserChange()
		{
			/*

			bool userTriggered = false;
			double? heightToApply = null;

			RowStyle.Setters.Add(new EventSetter
			{
				Event = SizeChangedEvent,
				Handler = new SizeChangedEventHandler((r, sizeArgs) =>
				{
					if (userTriggered && sizeArgs.HeightChanged && sizeArgs.NewSize.Height != RowHeight)
						heightToApply = sizeArgs.NewSize.Height;
				})
			});

			RowHeaderStyle.Setters.Add(new EventSetter
			{
				Event = PreviewMouseDownEvent,
				Handler = new MouseButtonEventHandler((rh, e) =>
				{
					if (IsEditing) e.Handled = true;
					else userTriggered = true;
				})
			});

			RowHeaderStyle.Setters.Add(new EventSetter
			{
				Event = PreviewMouseUpEvent,
				Handler = new MouseButtonEventHandler((rh, e) =>
				{
					if (!heightToApply.HasValue) return;
					ushort contentHeight = (ushort)(ActualHeight - BorderThickness.Top - BorderThickness.Bottom - ColumnHeaderHeight - SystemParameters.HorizontalScrollBarHeight);
					if (heightToApply > contentHeight) heightToApply = contentHeight;

					userTriggered = false;

					RowHeight = heightToApply.Value;
					Items.Refresh();
					heightToApply = null;


					// enregistrement dans les paramètres utilisateur
					RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
					if (key != null)
					{
						key.SetValue("RowHeight", RowHeight.ToString(CultureInfo.InvariantCulture));
						key.Close();
					}


					// envoi notification

					var response = new DVBuffer();
					var application = ((App)Application.Current).Appli;

					response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);					//début de l'acquittement ou de la réponse

					if (Page.Window.ActiveControl != this) application.SetInputBuffer(response);

					PrepareResponse(response);

					response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);					// Type de clic tableau (ici : LIST_CLICK_HEIGHT_CHANGING = 19)
					response.Put((byte)DataGridClick.ChangeRowCount);

					response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);				// Fin de l'envoi des évenements souris

					application.Send(response);			// envoi de la réponse et attente de la suite
				})
			});
			*/
		}

		/// <summary>
		/// Sets the common properties in the DVBuffer before sending them back to the programm
		/// </summary>
		/// <remarks>
		/// properties set up :
		/// CLIENTLEGER_DEBUT
		/// EVENEMENT_SOURIS_DEBUT
		/// SOURIS_TYPE_EVENEMENT
		/// PAGE_NUMERO
		/// MASQUE_NUMERO
		/// TABLEAU_IDENTIFIANT
		/// TABLEAU_ARRET
		/// </remarks>
		/// <returns>DVBuffer filled with the common properties</returns>
		private void PrepareResponse(DVBuffer response)
		{
			CommitEdit(DataGridEditingUnit.Row, true);	// garde-fou pour sortir de l'input

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);				// Début de l'envoi des évenements souris

			response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);					// Type d'évènement souris (byte)
			response.Put((byte)MouseEvent.ClickDataGrid);
			response.Put(ProprietesWpf.PAGE_NUMERO);								// Numéro de la page contenant le tableau cliqué (byte)
			response.Put(Page.NumPage);
			response.Put(ProprietesWpf.MASQUE_NUMERO);							// Numéro du masque (ushort)
			response.Put(Page.NumMasque);

			response.Put(ProprietesWpf.TABLEAU_IDENTIFIANT);					// Identifiant du tableau (uint)
			response.Put(Id);
			response.Put(ProprietesWpf.TABLEAU_ARRET);							// Numéro du point d'arrêt du tableau
			response.Put(StopPoint);
		}

		/// <summary>
		/// Sets the property and value corresponding to the active cell for the input callback
		/// </summary>
		/// <param name="cell">Cell whose property and value have to be sent</param>
		/// <param name="property">Property to send</param>
		/// <param name="valueString">String value to send</param>
		/// <param name="valueUshort">Ushort value to send</param>
		internal void PrepareInputCallBack(DataGridCell cell, out ProprietesWpf property, out string valueString, out ushort? valueUshort)
		{
			valueString = null;
			valueUshort = null;
			property = ProprietesWpf.CHAMP_VALEUR;
			//codePage = 0;

			//int rowIndex = ItemContainerGenerator.IndexFromContainer(DataGridRow.GetRowContainingElement(cell));
			//int colIndex = Columns.IndexOf(cell.Column);

			//switch (((IXHtmlDataGridColumn)cell.Column).ColumnType)
			//{
			//   case ColumnType.Champ:
			//      property = ProprietesWpf.CHAMP_VALEUR;
			//      CommitEdit(DataGridEditingUnit.Row, true);	// met à jour la valeur dans la DataTable
			//      valueString = ((XHtmlTextBoxCell)Rows[rowIndex][colIndex]).Text;
			//      codePage = ((XHtmlTextBoxCell)Rows[rowIndex][colIndex]).CodePage;
			//      ((XHtmlTextBoxCell)Rows[rowIndex][colIndex]).SetValue(valueString);
			//      break;

			//   case ColumnType.ChampCache:
			//      property = ProprietesWpf.CHAMP_VALEUR;
			//      CommitEdit(DataGridEditingUnit.Row, true);	// met à jour la valeur dans la DataTable
			//      /*!!!!!!!!!!!!!!!!!!!!!!!
			//      valueString = ((XHtmlPasswordBoxCell)Rows[rowIndex][colIndex]).Password;
			//      codePage = ((XHtmlPasswordBoxCell)Rows[rowIndex][colIndex]).CodePage;
			//       * */
			//      break;

			//   case ColumnType.ChampDate:
			//      property = ProprietesWpf.CHAMP_VALEUR;
			//      CommitEdit(DataGridEditingUnit.Row, true);	// met à jour la valeur dans la DataTable
			//      /*
			//      valueString = ((XHtmlDatePickerCell)Rows[rowIndex][colIndex]).Text;
			//      codePage = ((XHtmlDatePickerCell)Rows[rowIndex][colIndex]).CodePage;
			//      ((XHtmlDatePickerCell)Rows[rowIndex][colIndex]).SetValue(valueString);
			//       * */
			//      break;

			//   case ColumnType.Multichoix:
			//      property = ProprietesWpf.MULTICHOIX_VALEUR;
			//      /*
			//      valueUshort = (ushort)(((ComboBox)cell.Content).SelectedIndex + 1);
			//      CommitEdit(DataGridEditingUnit.Row, true);	// met à jour la valeur dans la DataTable
			//       * */
			//      break;

			//   case ColumnType.Case:
			//      /*
			//      property = ProprietesWpf.CASE_A_COCHER_ETAT;
			//      CommitEdit(DataGridEditingUnit.Row, true);	// met à jour la valeur dans la DataTable
			//      valueUshort = ((XHtmlCheckBoxCell)Rows[rowIndex][colIndex]).IsChecked.Value ? (ushort)1 : (ushort)0;
			//       * */
			//      break;

			//   default:
			//      throw new ArgumentException("wrong column type");
			//}
			
		}
		#endregion fonctions


		#region Fonctions Get()
		/// <summary>
		/// Returns the DataGridColumn corresponding to the given id
		/// </summary>
		/// <param name="id">Id of the column to retrieve</param>
		/// <returns>DataGridColumn corresponding to the given id</returns>
		internal IXHtmlObject GetColumn(ushort id)
		{
			return (from IXHtmlObject col in Columns where col.Id == id select col).FirstOrDefault();
		}

		/// <summary>
		/// Returns the dataGridCell corresponding to the given columnIndex in a dataGridRow
		/// </summary>
		/// <param name="rowIndex">index of the dataGridRow containing the dataGridCell to retrieve</param>
		/// <param name="colIndex">index of the dataGridColumn containing the dataGridCell to retrieve</param>
		/// <returns>DataGridCell corresponding to the given rowIndex and columnIndex</returns>
		internal DataGridCell GetDataGridCell(int rowIndex, int colIndex)
		{
			return null;  //§!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			/*
			DataGridRow row = ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
			if (row == null) return null;

			DataGridCellsPresenter presenter = TreeHelper.GetVisualChild<DataGridCellsPresenter>(row);
			return (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(colIndex);
			 */
		}

		/// <summary>
		/// Returns the maximum number of rows that can be entirely displayed in the dataGrid, given the dataGrid's rowHeight and the dataGrid's height
		/// </summary>
		/// <returns>Maximum number of rows that can be entirely displayed in the dataGrid</returns>
		internal ushort GetRowCount()
		{
			return 32;	//!!!!!!!!!!!!!!!!!!!!!!! a voir !!!!!!!!!!!!!!!!!!!!
			/*
			if (Template == null) ApplyTemplate();	// garde-fou

			var scrollViewer = Template.FindName("DG_ScrollViewer", this) as ScrollViewer;
			if (scrollViewer.Template == null) scrollViewer.ApplyTemplate();	// garde-fou
			var horizontalScrollBar = scrollViewer.Template.FindName("PART_HorizontalScrollBar", scrollViewer) as ScrollBar;

			double columnheaderheight = double.IsNaN(ColumnHeaderHeight) ? 0 : ColumnHeaderHeight;
			double horizontalScrollBarHeight = isList ? horizontalScrollBar.ActualHeight : SystemParameters.HorizontalScrollBarHeight;
			double contentHeight = ActualHeight - BorderThickness.Top - BorderThickness.Bottom - columnheaderheight - horizontalScrollBarHeight;

			return (ushort)(contentHeight / Math.Max(RowHeight, MinRowHeight));
			 */
		}


		/// <summary>
		/// récupération des paramètres utilisateurs
		/// </summary>
		private void GetUserSettings()
		{
			// si les paramètres utilisateurs ne sont pas désactivés...
			//!!!!!!!!!!!!!!if ((bool)Application.Current.Properties["IgnoreUserSettings"]) return;


			RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
			if (key == null) return;


			// hauteur des lignes
			double regRowHeight;
			string regRowHeightValue = key.GetValue("RowHeight") as string;
			if (regRowHeightValue != null && double.TryParse(regRowHeightValue, out regRowHeight))
				RowHeight = regRowHeight;


			// hauteur des entêtes de colonnes
			double regColumnHeaderHeight;
			string regColumnHeaderHeightValue = key.GetValue("ColumnHeaderHeight") as string;
			if (regColumnHeaderHeightValue != null && double.TryParse(regColumnHeaderHeightValue, out regColumnHeaderHeight))
				ColumnHeaderHeight = regColumnHeaderHeight;


			// ordre des colonnes
			string regColumnDisplayIndexes = key.GetValue("ColumnDisplayIndexes") as string;
			if (regColumnDisplayIndexes != null)
			{
				columnDisplayIndexes.Clear(); // garde-fou pour ne pas concaténer les valeurs à chaque passage
				string[] storedColumnDisplayIndexes = regColumnDisplayIndexes.Split(';');
				foreach (string s in storedColumnDisplayIndexes)
				{
					int displayIndex;
					if (int.TryParse(s, out displayIndex)) columnDisplayIndexes.Add(displayIndex);
					else columnDisplayIndexes.Add(-1); // garde-fou (testé et ignoré lors de l'affectation)
				}
			}


			// largeur des colonnes
			string regColumnWidths = key.GetValue("ColumnWidths") as string;
			if (regColumnWidths != null)
			{
				columnWidths.Clear(); // garde-fou pour ne pas concaténer les valeurs à chaque passage
				string[] storedColumnWidths = regColumnWidths.Split(';');
				foreach (string s in storedColumnWidths)
				{
					int width;
					if (int.TryParse(s, out width)) columnWidths.Add(width);
					else columnWidths.Add(-1); // garde-fou (testé et ignoré lors de l'affectation)
				}
			}


			// affichage des colonnes
			string regColumnDisplayOptions = key.GetValue("columnDisplayOptions") as string;
			if (regColumnDisplayOptions != null)
			{
				columnDisplayOptions.Clear(); // garde-fou pour ne pas concaténer les valeurs à chaque passage
				string[] storedColumnDisplayOptions = regColumnDisplayOptions.Split(';');
				foreach (string s in storedColumnDisplayOptions)
				{
					int displayed;
					if (int.TryParse(s, out displayed)) columnDisplayOptions.Add(displayed == 1);
					else columnDisplayOptions.Add(true); // garde-fou
				}
			}

			key.Close();
		}


		/// <summary>
		/// Tests if a dataGridCell is suitable for input (visible, editable etc...)
		/// </summary>
		/// <param name="row">XRow containing the cell</param>
		/// <param name="column">DataGridColumn containing the cell</param>
		/// <returns>true if the cell is suitable for input</returns>
		private bool TestCellForInput(XHtmlRow row, XHtmlGenericColumn column)
		{
			var cell = row[Columns.IndexOf(column)];

			// la cellule doit être visible ou sa visibilité ignorée
			return (!cell.Presentation.VisibilityFlag || cell.Presentation.Visibilite == Visibilites.Visible);
		}

		/// <summary>
		/// Tests if a dataGridColumn is suitable for input (visible, editable etc...)
		/// </summary>
		/// <param name="column">DataGridColumn to test</param>
		/// <returns>true if the column is suitable for input</returns>
		private static bool TestColumnForInput(XHtmlGenericColumn column)
		{
			var col = (IXHtmlDataGridColumn)column;

			// propriétés de la colonne
			return (column.Visibility == Visibility.Visible				// la colonne doit être visible (au sens WPF, au cas où l'utilisateur l'ai masquée par choix)
				&& col.Presentation.Visibilite == Visibilites.Visible	// la colonne doit être visible (au sens Diva, pour le grisage notamment)
				&& !column.IsReadOnly											// la colonne doit être éditable
				&& col.ColumnType != ColumnType.Image						// une image ne peut pas passer en saisie
				&& col.ColumnType != ColumnType.ObjetGraphique			// un hog ne peut pas passer en saisie
				&& col.ColumnType != ColumnType.Arbre);					// un arbre ne peut pas passer en saisie
		}

		/// <summary>
		/// Returns the (REAL) index of the first editable cell in the row (!! NOT the DISPLAY index)
		/// </summary>
		/// <param name="buffer">DVBuffer where the row and column indexes are read</param>
		/// <returns>Index of the first editable cell in the row</returns>
		internal ushort GetFirstColumnIndex(DVBuffer buffer)
		{
			ushort rowIndex;
			buffer.Get(out rowIndex);	//numéro de ligne (ushort) (début à 1)

			// on parcourt les colonnes dans leur ordre d'affichage (potentiellement différent de l'ordre de sauvegarde)
			for (int i = 0; i < Columns.Count; i++)
			{
				XHtmlGenericColumn column = ColumnFromDisplayIndex(i);
				if (TestColumnForInput(column))	// on teste d'abord la colonne
					if (rowIndex > 0 && rowIndex <= Rows.Count)	// si on est sur une ligne valide
					{
						if (TestCellForInput(Rows[rowIndex - 1], column)) // on teste la cellule correspondante
							return (ushort)(Columns.IndexOf(column) + 1);
					}
					else // si on est sur une ligne invalide (pas à l'écran, inexistante etc...)
						return (ushort)(Columns.IndexOf(column) + 1); // on renvoie l'indice de la colonne
			}

			return 0;	// si aucune cellule saisissable
		}

		private XHtmlGenericColumn ColumnFromDisplayIndex(int i)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns the (REAL) index of the last editable cell in the row (!! NOT the DISPLAY index)
		/// </summary>
		/// <param name="buffer">DVBuffer where the row and column indexes are read</param>
		/// <returns>Index of the last editable cell in the row</returns>
		internal ushort GetLastColumnIndex(DVBuffer buffer)
		{
			ushort rowIndex;
			buffer.Get(out rowIndex);	//numéro de ligne (ushort) (début à 1)

			// on parcourt les colonnes dans leur ordre d'affichage (potentiellement différent de l'ordre de sauvegarde)
			for (int i = Columns.Count - 1; i >= 0; i--)
			{
				XHtmlGenericColumn column = ColumnFromDisplayIndex(i);
				if (TestColumnForInput(column))	// on teste d'abord la colonne
					if (rowIndex > 0 && rowIndex <= Rows.Count)	// si on est sur une ligne valide
					{
						if (TestCellForInput(Rows[rowIndex - 1], column)) // on teste la cellule correspondante
							return (ushort)(Columns.IndexOf(column) + 1);
					}
					else // si on est sur une ligne invalide (pas à l'écran, inexistante etc...)
						return (ushort)(Columns.IndexOf(column) + 1); // on renvoie l'indice de la colonne
			}

			return 0;	// si aucune cellule saisissable
		}

		/// <summary>
		/// Returns the (REAL) index of the previous editable cell in the row (!! NOT the DISPLAY index)
		/// </summary>
		/// <param name="buffer">DVBuffer where the row and column indexes are read</param>
		/// <returns>Index of the previous editable cell in the row</returns>
		internal ushort GetPreviousColumnIndex(DVBuffer buffer)
		{
			ushort rowIndex, colIndex;
			buffer.Get(out rowIndex);	//numéro de ligne (ushort) (début à 1)
			buffer.Get(out colIndex);	//numéro de colonne (ushort) (début à 1)

			// on parcourt les colonnes dans leur ordre d'affichage (potentiellement différent de l'ordre de sauvegarde)
			for (int i = Columns[colIndex - 1].DisplayIndex - 1; i >= 0; i--)
			{
				XHtmlGenericColumn column = ColumnFromDisplayIndex(i);
				if (TestColumnForInput(column))	// on teste d'abord la colonne
					if (rowIndex > 0 && rowIndex <= Rows.Count)	// si on est sur une ligne valide
					{
						if (TestCellForInput(Rows[rowIndex - 1], column)) // on teste la cellule correspondante
							return (ushort)(Columns.IndexOf(column) + 1);
					}
					else // si on est sur une ligne invalide (pas à l'écran, inexistante etc...)
						return (ushort)(Columns.IndexOf(column) + 1); // on renvoie l'indice de la colonne
			}

			return 0;	// si aucune cellule saisissable
		}

		/// <summary>
		/// Returns the (REAL) index of the next editable cell in the row (!! NOT the DISPLAY index)
		/// </summary>
		/// <param name="buffer">DVBuffer where the row and column indexes are read</param>
		/// <returns>Index of the next editable cell in the row</returns>
		internal ushort GetNextColumnIndex(DVBuffer buffer)
		{
			ushort rowIndex, colIndex;
			buffer.Get(out rowIndex);	//numéro de ligne (ushort) (début à 1)
			buffer.Get(out colIndex);	//numéro de colonne (ushort) (début à 1)

			// on parcourt les colonnes dans leur ordre d'affichage (potentiellement différent de l'ordre de sauvegarde)
			for (int i = Columns[colIndex - 1].DisplayIndex + 1; i < Columns.Count; i++)	// colIndex - 1 + 1
			{
				XHtmlGenericColumn column = ColumnFromDisplayIndex(i);
				if (TestColumnForInput(column))	// on teste d'abord la colonne
					if (rowIndex > 0 && rowIndex <= Rows.Count)	// si on est sur une ligne valide
					{
						if (TestCellForInput(Rows[rowIndex - 1], column)) // on teste la cellule correspondante
							return (ushort)(Columns.IndexOf(column) + 1);
					}
					else // si on est sur une ligne invalide (pas à l'écran, inexistante etc...)
						return (ushort)(Columns.IndexOf(column) + 1); // on renvoie l'indice de la colonne
			}

			return 0;	// si aucune cellule saisissable
		}
		#endregion Fonctions Get()


		#region Fonctions Set()
		/// <summary>
		/// Sets the Grid's visual properties
		/// </summary>
		private void SetLayoutProperties()
		{
			// largeur de la colonne adaptée à la largeur du tableau
			if (useFirstColumnWidth)
			{
				Columns[0].MinWidth = Width - BorderThickness.Left - BorderThickness.Right - SystemParameters.VerticalScrollBarWidth - RowHeaderActualWidth;
				CanUserResizeColumns = false;
				CanUserReorderColumns = false;
			}

			// utilisation de la couleur de fond de la première colonne pour le reste du tableau
			if (useCustomHeaderColor)
			{
				Brush backgroundBrush = ((IXHtmlDataGridColumn)Columns[0]).ColumnHeader.Background;
				if (backgroundBrush != null)	// garde-fou dans le cas où l'option est combinée à la couleur par défaut (non-envoyée)
				{
					Style rowHeaderStyleTemp = new Style(typeof(DataGridRowHeader), RowHeaderStyle);
					rowHeaderStyleTemp.Setters.Add(new Setter(BackgroundProperty, backgroundBrush));
					RowHeaderStyle = rowHeaderStyleTemp;

					Style columnHeaderStyleTemp = new Style(typeof(DataGridColumnHeader), ColumnHeaderStyle);
					columnHeaderStyleTemp.Setters.Add(new Setter(BackgroundProperty, backgroundBrush));
					ColumnHeaderStyle = columnHeaderStyleTemp;
				}
			}


			return;
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//if ((bool)Application.Current.Properties["IgnoreUserSettings"]) return; // on ne fait rien si les paramètres utilisateurs sont ignorés

			//RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);

			//// mise à jour des displayIndexes de colonnes
			//if (columnDisplayIndexes.Count == Columns.Count) // sinon, le registre est corompu >> on reset
			//   foreach (DataGridColumn column in Columns)
			//   {
			//      int displayIndex = columnDisplayIndexes[Columns.IndexOf(column)];
			//      if (displayIndex > -1 && displayIndex < Columns.Count) column.DisplayIndex = displayIndex;
			//   }
			//else if (key != null && key.GetValue("ColumnDisplayIndexes") != null)
			//   key.DeleteValue("ColumnDisplayIndexes");

			//// mise à jour des largeurs de colonnes
			//if (columnWidths.Count == Columns.Count) // sinon, le registre est corompu >> on reset
			//   foreach (DataGridColumn column in Columns)
			//   {
			//      int width = columnWidths[Columns.IndexOf(column)];
			//      if (width > -1) column.Width = width;
			//   }
			//else if (key != null && key.GetValue("ColumnWidths") != null)
			//   key.DeleteValue("ColumnWidths");

			//// mise à jour des options d'affichage de colonnes
			//if (columnDisplayOptions.Count == Columns.Count) // sinon, le registre est corompu >> on reset
			//   foreach (DataGridColumn column in Columns.Cast<IXHtmlDataGridColumn>().Where(column => !column.IsHiddenForced && !column.IsMandatory))
			//      column.Visibility = columnDisplayOptions[Columns.IndexOf(column)] ? Visibility.Visible : Visibility.Collapsed;
			//else if (key != null && key.GetValue("columnDisplayOptions") != null)
			//   key.DeleteValue("columnDisplayOptions");

			//if (key != null) key.Close();

		}

		/// <summary>
		/// Affecte la valeur passée en paramètre à la propriété IsEnabled de la scrollBar verticale du Tableau
		/// </summary>
		/// <param name="enable">valeur à donner à la propriété IsEnabled de la scroolBar</param>
		internal void SetEnableVerticalScrollBar(bool enable)
		{
			verticalScrollBar.SetEnable(enable, verticalScrollBarForced, false);
		}

		/// <summary>
		/// Sets the value of "CanUserAddRows"
		/// </summary>
		internal void SetCanUserAddRows()
		{
			CanUserAddRows = enableLineCreation && Rows.Count < GetRowCount();
		}

		/// <summary>
		/// Sets the columnHeaders' and ColumnHeadersPresenter's Background to the 'active' color
		/// </summary>
		internal void SetIsActive(bool isActive)
		{
			if (showActive) IsActive = isActive;
		}
		#endregion Fonctions Set()


		#region Fonctions d'envoi au serveur (pour les clics souris)
		/// <summary>
		/// Envoie les informations de clic sur une DataGridCell au serveur le cas échéant
		/// </summary>
		/// <param name="dataGridCell">cellule cliquée</param>
		/// <param name="clickedButton">bouton de souris cliqué</param>
		/// <param name="clickCount">nombre de clics sur la cellule</param>
		internal void SendCellClick(DataGridCell dataGridCell, byte clickedButton, byte clickCount)
		{
			/*
			DataGridRow row = DataGridRow.GetRowContainingElement(dataGridCell);
			if (row == null) return;

			if (row.Item == CollectionView.NewItemPlaceholder) //return;	// on ne traite pas le clic sur une cellule de la ligne d'ajout
			{
				Debug.WriteLine("Clic sur une cellule de la ligne d'ajout");
				return;
			}

			// recupère l'index de la ligne et de la colonne
			int rowIndex = ItemContainerGenerator.IndexFromContainer(row);
			int colIndex = Columns.IndexOf(dataGridCell.Column);
			ushort seqPoint = ((IXHtmlDataGridColumn)dataGridCell.Column).SeqPoint;

			ProprietesWpf property = new ProprietesWpf();
			string valueString = null;
			ushort? valueUshort = null;
			codePage = 0;

			if (IsEditing)	// on est en cours de saisie sur une ligne
			{
				if (row.DataContext != CurrentItem) //return;	// l'objet cliqué n'est pas une cellule de la ligne en cours de saisie
				{
					Debug.WriteLine("clic hors de la zone de saisie");
					return;
				}

				// sinon, l'objet cliqué est une cellule dans la ligne en saisie : on envoie la valeur de la cellule active
				if (Rows[rowIndex][colIndex].Presentation.Visibilite != Visibilite.Visible) return;	// une cellule grisée ne peut passer pas en input
				if (((IXHtmlEditableObject)Columns[colIndex]).Presentation.Visibilite != Visibilite.Visible) return;	// une cellule d'une colonne grisée ne peut pas passer en input
				if (Columns[colIndex].IsReadOnly) return;	// une cellule d'une colonne en affichage ne peut pas passer en input

				int currentColIndex = Columns.IndexOf(CurrentCell.Column);
				DataGridCell currentCell = GetDataGridCell(rowIndex, currentColIndex);
				PrepareInputCallBack(currentCell, out property, out valueString, out valueUshort);
			}
			Debug.WriteLine(((clickCount > 1) ? "double-" : "") + "clic sur cellule : ligne : " + (rowIndex + 1) + " - colonne : " + (colIndex + 1));

			#region envoi réponse
			DataGridMouseEventArgs mouseEventArgs = new DataGridMouseEventArgs
			{
				ColIndex = colIndex,
				RowIndex = rowIndex,

				ClickCount = clickCount,
				ClickedButton = clickedButton,
				Key = XHtmlApplication.GetPressedKey(),

				Click = DataGridClick.Cell,
				ActiveControl = Page.Window.ActiveControl,
				SeqPoint = seqPoint,

				Property = property,
				ValueString = valueString,
				ValueUshort = valueUshort

			};
			SendMouseEvent(mouseEventArgs);
			#endregion envoi réponse
			*/
		}

		/// <summary>
		/// Envoie les informations de clic sur un DataGridRowHeader au serveur le cas échéant
		/// </summary>
		/// <param name="dataGridRowHeader">DataGridRowHeader Cliqué</param>
		/// <param name="clickedButton">Bouton de la souris avec lequel le DataGridRowHeader a été cliqué</param>
		/// <param name="clickCount">Nombre de clics</param>
		private void SendRowHeaderClick(DataGridRowHeader dataGridRowHeader, byte clickedButton, byte clickCount)
		{
			DataGridClick click;
			int? rowIndex = null;

			XHtmlRow row = dataGridRowHeader.DataContext as XHtmlRow;

			if (row != null) // ligne "normale"
			{
				click = DataGridClick.RowHeader; // (LIST_CLICK_ROW_CONTROL = 6)
				rowIndex = Rows.IndexOf(row);
				Debug.WriteLine(((clickCount > 1) ? "double-" : "") + "clic sur ligne n°" + (ushort)(rowIndex + 1));
			}
			else // bouton de création de nouvelle ligne
			{
				click = DataGridClick.NewLineButton; // (LIST_CLICK_CREATE_CONTROL = 15)
				Debug.WriteLine("clic sur bouton nouvelle ligne");
			}

			#region envoi réponse
			DataGridMouseEventArgs mouseEventArgs = new DataGridMouseEventArgs
			{
				//!!!!!!!!!!!!!!!!!!!!!
				//RowIndex = rowIndex,

				//ClickCount = clickCount,
				//ClickedButton = clickedButton,
				//Key = XHtmlApplication.GetPressedKey(),

				//Click = click,
				//ActiveControl = Page.Window.ActiveControl
			};
			SendMouseEvent(mouseEventArgs);
			#endregion envoi réponse
		}

		/// <summary>
		/// Envoie les informations de clic sur un DataGridColumnHeader au serveur le cas échéant
		/// </summary>
		/// <param name="dataGridColumnHeader">DataGridColumnHeader Cliqué</param>
		/// <param name="clickedButton">Bouton de la souris avec lequel le DataGridColumnHeader a été cliqué</param>
		/// <param name="clickCount">Nombre de clics</param>
		private void SendColumnHeaderClick(DataGridColumnHeader dataGridColumnHeader, byte clickedButton, byte clickCount)
		{
			/*
			int colIndex = Columns.IndexOf(dataGridColumnHeader.Column);
			Debug.WriteLine(((clickCount > 1) ? "double-" : "") + "clic sur colonne n° " + (ushort)(colIndex + 1));

			#region envoi réponse
			DataGridMouseEventArgs mouseEventArgs = new DataGridMouseEventArgs
			{
				ColIndex = colIndex,

				ClickCount = clickCount,
				ClickedButton = clickedButton,
				Key = XHtmlApplication.GetPressedKey(),

				Click = DataGridClick.ColumnHeader,
				ActiveControl = Page.Window.ActiveControl
			};
			SendMouseEvent(mouseEventArgs);
			#endregion envoi réponse
			 * */
		}

		/// <summary>
		/// Sends a mouseEvent to the server with the values given in the DataGridMouseEventArg
		/// </summary>
		/// <param name="e">Structure containing the values to send to the server</param>
		private void SendMouseEvent(DataGridMouseEventArgs e)
		{
			/*
			var application = ((App)Application.Current).Appli;

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);						//début de l'acquittement ou de la réponse

			if (e.ActiveControl != this) application.SetInputBuffer(response);
			else if (e.ValueString != null)
			{
				response.Put(e.Property);
				response.PutStringCP(e.ValueString, codePage);
			}
			else if (e.ValueUshort.HasValue)
			{
				response.Put(e.Property);
				response.Put((ushort)e.ValueUshort);
			}

			PrepareResponse(response);

			response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);				// Type de clic tableau
			response.Put((byte)e.Click);

			// Informations sur le clic lui-même, à savoir (pour le moment) :
			response.Put(ProprietesWpf.SOURIS_CLIC);						// 1 = simple clic; 2 = double clic (byte)
			response.Put(e.ClickCount);
			response.Put(ProprietesWpf.SOURIS_BOUTON);					// 1 = gauche; 2 = milieu; 3 = droite (byte)
			response.Put(e.ClickedButton);
			response.Put(ProprietesWpf.SOURIS_TOUCHE);					// touche(s) du clavier enfoncée(s) simultanément (byte)
			response.Put(e.Key);

			if (e.SeqPoint.HasValue)
			{
				response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);		// Numéro du point de séquence de la donnée cliqué
				response.Put(e.SeqPoint.Value);
			}

			if (e.RowIndex.HasValue)
			{
				response.Put(ProprietesWpf.TABLEAU_NUMERO_LIGNE);		// n° de ligne cliquée le cas échéant (ushort)
				response.Put((ushort)(e.RowIndex.Value + 1));
			}

			if (e.ColIndex.HasValue)
			{
				response.Put(ProprietesWpf.TABLEAU_NUMERO_COLONNE);	// n° de colonne cliquée le cas échéant (ushort)
				response.Put((ushort)(e.ColIndex.Value + 1));
			}

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);			// Fin de l'envoi des évenements souris

			if (application.AttenteConsult || application.AttenteInput)
				application.Send(response);						// envoi de la réponse et attente de la suite
			else // on reporte l'envoi si le retour du premier clic n'a pas encore été reçu
				application.AsynchronousResponse = response;
			*/
		}

		/// <summary>
		/// sends information to the server on a DataGridCell's PreviewKeyDownEvent
		/// </summary>
		/// <param name="cell">Cell getting the PreviewKeyDownEvent</param>
		/// <param name="key">striken key</param>
		internal void SendCellKeyDown(DataGridCell cell, string key)
		{
			/*
			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				// début de l'acquittement ou de la réponse

			#region Envoi valeur
			ProprietesWpf property;
			string valueString;
			ushort? valueUshort;

			PrepareInputCallBack(cell, out property, out valueString, out valueUshort);

			response.Put(property);												// envoi de la valeur de la case en cours
			if (valueString != null) response.PutStringCP(valueString, codePage);
			else response.Put(valueUshort.Value);
			#endregion Envoi valeur

			if ((Keyboard.Modifiers & ModifierKeys.Control) != 0)		// CTRL (AVANT la touche)
				response.Put(ProprietesWpf.TOUCHE_CTRL);
			if ((Keyboard.Modifiers & ModifierKeys.Shift) != 0)		// SHIFT (AVANT la touche)
				response.Put(ProprietesWpf.TOUCHE_SHIFT);
			if ((Keyboard.Modifiers & ModifierKeys.Alt) != 0)			// ALT (AVANT la touche)
				response.Put(ProprietesWpf.TOUCHE_ALT);

			response.Put(ProprietesWpf.TOUCHE_CLAVIER);					// touche tapée (APRES les modificateurs)
			response.PutString(key);

			((App)Application.Current).Appli.Send(response);		// envoi de la réponse et attente de la suite

			CommitEdit(DataGridEditingUnit.Row, true);	// garde-fou pour sortir de l'input
			 * */
		}
		#endregion Fonctions d'envoi au serveur (pour les clics souris)


		//#region Ecouteurs

		#region Ecouteurs généraux
		/// <summary>
		/// Redimensionnement de la colonne dans le cas d'une colonne unique
		/// </summary>
		private void SizeChangedHandler(object sender, SizeChangedEventArgs e)
		{
			/*
			if (!Page.Window.IsEnabled) return; // on ne notifie rien si la dg n'est pas dans la fenêtre active (redimensionnement d'une fenêtre sous la fenêtre active)

			if (useFirstColumnWidth)
				Columns[0].Width = ActualWidth - BorderThickness.Left - BorderThickness.Right - SystemParameters.VerticalScrollBarWidth - RowHeaderActualWidth;

			if (Page.Window.CurrentPage != Page && Page.Window.CurrentPage != null && Page.Window.CurrentPage.ListOfObjects.OfType<XHtmlDataGrid>().Any())
				return; // on ne notifie que sur un tableau de la page en cours si celle-ci contient un tableau

			var application = ((App)Application.Current).Appli;

			if (e.PreviousSize != new Size(0, 0) && GetRowCount() != Rows.Count + (CanUserAddRows ? 1 : 0))
			{
				// envoi notification pour affichage de nouvelle ligne ou suppression de ligne existante

				var response = new DVBuffer();
				response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);						//début de l'acquittement ou de la réponse

				if (Page.Window.ActiveControl != this) application.SetInputBuffer(response);

				PrepareResponse(response);

				response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);						// Type de clic tableau (ici : LIST_CLICK_HEIGHT_CHANGING = 19)
				response.Put((byte)DataGridClick.ChangeRowCount);

				response.Put(ProprietesWpf.IDENT_UNIQUE);								// Id du tableau cliquée (uint)
				response.Put(Id);
				response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);					// Numéro du point de séquence du tableau cliqué
				response.Put(SeqPoint);

				response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);					// Fin de l'envoi des évenements souris

				if (application.AttenteInput || application.AttenteConsult)
					application.Send(response);			// envoi de la réponse et attente de la suite
				else
					application.AsynchronousResponse = response;
			}
			e.Handled = true;	// pour empêcher le sizeChanged() sur le ScrollViewer de redimensionner le thumb de la scrollBar verticale
			 * */
		}

		/// <summary>
		/// Déplacement de colonne : sauvegarde dans les paramètres utilisateur
		/// </summary>
		private void ColumnReorderedHandler(object sender, DataGridColumnEventArgs e)
		{
			// mise à jour de la liste des DisplayIndexes
			columnDisplayIndexes.Clear();
			foreach (XHtmlGenericColumn column in Columns)
				columnDisplayIndexes.Add(column.DisplayIndex);

			// sauvegarde de la liste de DisplayIndexes dans les paramètres utilisateurs
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
			if (key == null) return;

			string regColumnDisplayIndexes = columnDisplayIndexes.Aggregate("", (current, columnIndex) => current + (columnIndex.ToString(CultureInfo.InvariantCulture) + ";"));
			key.SetValue("ColumnDisplayIndexes", regColumnDisplayIndexes.Trim(';')); // on enlève le dernier ';'
			key.Close();
		}

		/// <summary>
		/// Déplacement de colonne : sauvegarde dans les paramètres utilisateur
		/// </summary>
		public void ColumnWidthChangedHandler(object sender, SizeChangedEventArgs e)
		{
			//// mise à jour de la liste des largeurs
			//columnWidths.Clear();
			//foreach (DataGridColumn column in Columns)
			//   columnWidths.Add((int)column.ActualWidth);

			//// sauvegarde de la liste de DisplayIndexes dans les paramètres utilisateurs
			//RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
			//if (key == null) return;

			//string regColumnWidths = columnWidths.Aggregate("", (current, width) => current + (width.ToString(CultureInfo.InvariantCulture) + ";"));
			//key.SetValue("ColumnWidths", regColumnWidths.Trim(';')); // on enlève le dernier ';'
			//key.Close();
		}

		/// <summary>
		/// récupération du bouton de création de nouvelle ligne pour changement de template
		/// </summary>
		private static void LoadingRowHandler(object sender, DataGridRowEventArgs e)
		{
			// gestion à part de la ligne permettant d'ajouter une nouvelle ligne:
			// on affecte le style contenant le template de la ligne de création (pour virer les checkBoxes et ajouter le + au header dans le style par défaut notamment...)
			if (e.Row.Item == CollectionView.NewItemPlaceholder)
				e.Row.Style = (Style)Application.Current.Resources["NewItemRowStyle"];
		}

		/// <summary>
		/// gestion du focus lors du BeginEdit() pour les dataGridTemplateColumn (pas fait automatiquement)
		/// </summary>
		private static void PreparingCellForEditHandler(object sender, DataGridPreparingCellForEditEventArgs e)
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//PasswordBox passwordBox = TreeHelper.GetVisualChild<PasswordBox>(e.EditingElement);
			//if (passwordBox == null) return;

			//passwordBox.Focus();
			//passwordBox.SelectAll();
		}

		/// <summary>
		/// rétablissement de la bonne source d'items pour les cellules comboBox le cas échéant
		/// </summary>
		private static void CellEditEndingHandler(object sender, DataGridCellEditEndingEventArgs e)
		{
			/* !!!!!!!!!!!!!!!!!!!
			XHtmlComboBoxColumn column = e.Column as XHtmlComboBoxColumn;
			if (column == null) return; // on ne veut traiter que les cellules comboBox
			column.IgnoreValueChanged = true; // pour bloquer la notif lors de la réaffectation d'itemsSource en sortie d'input multichoix
			ComboBox comboBox = e.EditingElement as ComboBox;
			comboBox.ItemsSource = ((XHtmlComboBoxColumn)e.Column).ComboBoxItems;
			column.IgnoreValueChanged = false;
			 * */
		}
		#endregion Ecouteurs généraux

		#region Ecouteurs scrollbars
		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour la scrollbar verticale de la dataGrid. Stocke l'ancienne position de la scrollBar
		/// </summary>
		private void ScrollBarPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			ScrollBar scrollBar = sender as ScrollBar;
			scrollBarOldValue = scrollBar.GetThumbCenter() - scrollBar.GetThumbLength() / 2;
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseUp" pour la scrollbar verticale de la dataGrid. Envoie les infos en cas de changement de valeur
		/// </summary>
		private void ScrollBarPreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			var application = ((App)Application.Current).Appli;

			if (!application.AttenteConsult && !application.AttenteInput)
			{
				e.Handled = true;
				return;
			}

			ScrollBar scrollBar = (ScrollBar)sender;

			//e.Handled = true; // ne pas mettre : fait planter le programme (il semble que la souris soit capturée mais pas relâchée => devient inutilisable

			int oldValue = (int)Math.Round(scrollBarOldValue);
			int newValue = (int)Math.Round(scrollBar.GetThumbCenter() - scrollBar.GetThumbLength() / 2);

			if (oldValue == newValue) return;	// clic sans changement de valeur

			Debug.WriteLine("position scrollBar changée - old : " + oldValue + " / new : " + newValue);

			if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
			if (!application.AttenteConsult && !application.AttenteInput) return;

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);					//début de l'acquittement ou de la réponse

			if (Page.Window.ActiveControl != this) application.SetInputBuffer(response);

			PrepareResponse(response);

			response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);					// Type de clic tableau (ici : LIST_CLICK_SCROLLBAR = 13)
			response.Put((byte)DataGridClick.ScrollBar);

			response.Put(ProprietesWpf.TABLEAU_ARRET);						// Numéro du point d'arrêt du tableau
			response.Put(StopPoint);

			response.Put(ProprietesWpf.TABLEAU_ASCVER_TYPE_CLIC);			// Détails sur le click ascenseur (2x byte)
			byte location = 0;														// partie de la scrollbar cliquée
			if (e.OriginalSource is Thumb) location = 3;								//3 = zone centrale
			else
			{
				RepeatButton rb = e.OriginalSource as RepeatButton;
				if (rb != null)
				{
					if (rb.Command == ScrollBar.LineUpCommand) location = 1;		//1 = flèche du haut
					if (rb.Command == ScrollBar.PageUpCommand) location = 2;		//2 = zone intermédiaire haute
					if (rb.Command == ScrollBar.PageDownCommand) location = 4;	//4 = zone intermédiaire basse
					if (rb.Command == ScrollBar.LineDownCommand) location = 5;	//5 = flèche du bas
				}
			}
			response.Put(location);

			byte position = 0;														// position de la scrollbar
			if (Math.Round(scrollBar.Value) == 0) position = 1;							//0 = position intermédiaire
			if (Math.Round(scrollBar.Value) == scrollBar.Maximum) position = 2;		//1 = position début
			response.Put(position);																	//2 = position fin


			response.Put(ProprietesWpf.TABLEAU_ASCVER_ANCIENNE_POS);		// ancienne position de l’ascenseur (uint)
			response.Put((uint)(oldValue + 1));

			response.Put(ProprietesWpf.TABLEAU_ASCVER_NOUVELLE_POS);		// nouvelle position de l’ascenseur (uint)
			response.Put((uint)(newValue + 1));

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);				// Fin de l'envoi des évenements souris

			application.Send(response);											// envoi de la réponse et attente de la suite
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement "Click" sur les repeatButtons de la scrollbar verticale
		/// </summary>
		private void RepeatButtonClickHandler(object sender, RoutedEventArgs e)
		{
			RepeatButton repeatButton = sender as RepeatButton;
			if (repeatButton == null) return;

			if (repeatButton.Command == ScrollBar.LineUpCommand)
				verticalScrollBar.SetThumbCenter(verticalScrollBar.GetThumbCenter() - verticalScrollBar.SmallChange);
			else if (repeatButton.Command == ScrollBar.PageUpCommand)
				verticalScrollBar.SetThumbCenter(verticalScrollBar.GetThumbCenter() - verticalScrollBar.LargeChange);
			else if (repeatButton.Command == ScrollBar.LineDownCommand)
				verticalScrollBar.SetThumbCenter(verticalScrollBar.GetThumbCenter() + verticalScrollBar.SmallChange);
			else if (repeatButton.Command == ScrollBar.PageDownCommand)
				verticalScrollBar.SetThumbCenter(verticalScrollBar.GetThumbCenter() + verticalScrollBar.LargeChange);
		}
		#endregion Ecouteurs scrollbars

		#region Ecouteurs souris
		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseWheel" pour la dataGrid.
		/// </summary>
		private void PreviewMouseWheelHandler(object sender, MouseWheelEventArgs e)
		{
			/*
			e.Handled = true;

			if (IsEditing || !verticalScrollBar.IsEnabled) return;

			var application = ((App)Application.Current).Appli;

			if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
			if (!application.AttenteConsult && !application.AttenteInput) return;

			int delta = -e.Delta / Mouse.MouseWheelDeltaForOneLine;

			int oldValue = (int)Math.Round(verticalScrollBar.GetThumbCenter() - verticalScrollBar.GetThumbLength() / 2);
			int newValue = oldValue + delta;

			if (newValue < verticalScrollBar.Minimum)
				newValue = (int)verticalScrollBar.Minimum;
			if (newValue > verticalScrollBar.Maximum - verticalScrollBar.GetThumbLength())
				newValue = (int)(verticalScrollBar.Maximum - verticalScrollBar.GetThumbLength());

			verticalScrollBar.Set(
				(uint)verticalScrollBar.Minimum,
				(uint)verticalScrollBar.Maximum,
				(uint)verticalScrollBar.SmallChange,
				(uint)verticalScrollBar.LargeChange,
				(uint)newValue,
				GetRowCount());

			var response = new DVBuffer();

			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);					//début de l'acquittement ou de la réponse

			if (Page.Window.ActiveControl != this) application.SetInputBuffer(response);

			PrepareResponse(response);

			response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);					// Type de clic tableau (ici : LIST_CLICK_SCROLLBAR = 13)
			response.Put((byte)DataGridClick.ScrollBar);

			response.Put(ProprietesWpf.TABLEAU_ARRET);						// Numéro du point d'arrêt du tableau
			response.Put(StopPoint);

			response.Put(ProprietesWpf.TABLEAU_ASCVER_ANCIENNE_POS);		// ancienne position de l’ascenseur (uint)
			response.Put((uint)(oldValue + 1));

			response.Put(ProprietesWpf.TABLEAU_ASCVER_NOUVELLE_POS);		// nouvelle position de l’ascenseur (uint)
			response.Put((uint)(newValue + 1));

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);				// Fin de l'envoi des évenements souris

			application.Send(response);			// envoi de la réponse et attente de la suite
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour empêcher le clic sur le fond de la DataGrid (sur le ScrollViewer)
		/// </summary>
		private static void previewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			if (e.OriginalSource is ScrollViewer) e.Handled = true; // on bloque les clics dans le fond de la dataGrid
		}


		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour le selectAllButton
		/// </summary>
		private void SelectAllButtonPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			//§!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//e.Handled = true;	// clic sur le SelectAllButton : on ne doit pas laisser wpf gérer, sinon il sélectionne toutes les lignes

			//if (IsEditing) //return;	// le tableau est actif et on est en cours de saisie sur une ligne
			//{
			//   Debug.WriteLine("clic hors de la zone de saisie");
			//   return;
			//}
			//if (e.ClickCount == 1)
			//{
			//   var application = ((App)Application.Current).Appli;
			//   if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
			//   if (!application.AttenteConsult && !application.AttenteInput) return;
			//}

			//mouseButtonEventArgs = e;
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseUp" pour le selectAllButton
		/// </summary>
		private void SelectAllButtonPreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
		{
			if (mouseButtonEventArgs == null || e.OriginalSource != mouseButtonEventArgs.OriginalSource)
				return;	// garde-fou (si pas de MouseDown enregistré ou si la souris a bougé entre-temps)

			Debug.WriteLine(((mouseButtonEventArgs.ClickCount > 1) ? "double-" : "") + "clic sur le SelectAllButton");

			SendMouseEvent(new DataGridMouseEventArgs
			{
				ClickCount = (byte)((mouseButtonEventArgs.ClickCount > 1) ? 2 : 1),
				Click = DataGridClick.SelectAllButton,
				ClickedButton = XHtmlApplication.GetClickedButton(e),
				Key = XHtmlApplication.GetPressedKey(),
				// !!!!!!!!!!!!!!!!!!!! ActiveControl = Page.Window.ActiveControl
			});

			mouseButtonEventArgs = null;
		}


		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour les cellules
		/// </summary>
		internal void CellPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			var application = ((App)Application.Current).Appli;

			if (e.ClickCount >= 2 && application.AttenteInput)		// pas de double-clic en input
			{
				e.Handled = true;
				return;
			}

			DataGridCell cell = (DataGridCell)sender;


			//!!!!!!!!!!!!!!!!!!! revoir
			//!!!!!!!!!!!!!!if (((FrameworkElement)e.OriginalSource).Tag != null && cell.Tag is XHtmlTreeCell) return; // clic sur [+] ou [-] dans un arbre

			byte clickedButton = XHtmlApplication.GetClickedButton(e); // bouton cliqué

			if (cell.IsEditing && clickedButton == 1 && e.ClickCount == 1) // clic gauche sur une cellule en cours d'édition, on laisse wpf gérer
			{
				if (e.Source is DataGridCell) e.Handled = true;	// garde-fou : on ne doit jamais pouvoir cliquer sur le fond d'une cellule en input (risque de perte de focus)
				return;
			}

			// Si clic dans une cellule de colonne HOG, on laisse passer le clic
			if (cell.Column is XHtmlHogColumn) return;
			e.Handled = true;

			if (e.ClickCount == 1)
			{
				if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
				if (!application.AttenteConsult && !application.AttenteInput) return;
			}

			mouseButtonEventArgs = e;

			if (clickedButton == 1) SendCellClick(cell, clickedButton, (byte)((e.ClickCount > 1) ? 2 : 1)); // click autre que gauche traité au MouseUp
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseUp" pour les cellules
		/// </summary>
		internal void CellPreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
		{
			DataGridCell cell = (DataGridCell)sender;

			byte clickedButton = XHtmlApplication.GetClickedButton(e); // bouton cliqué

			if (cell.IsEditing && clickedButton == 1 && e.ClickCount == 1) return;	// clic gauche sur la cellule en cours d'édition : on laisse wpf gérer

			// Si clic dans une cellule de colonne HOG, on laisse passer le clic
//!!!!!!!!!!!!!!!!!!			if (cell.Column is XHtmlHogColumn) return;
			e.Handled = true;

			// garde-fou (si pas de MouseDown enregistré ou si la souris a bougé entre-temps)
			if (mouseButtonEventArgs == null || e.OriginalSource != mouseButtonEventArgs.OriginalSource) return;

			if (clickedButton != 1) SendCellClick(cell, clickedButton, (byte)((mouseButtonEventArgs.ClickCount > 1) ? 2 : 1)); // click gauche traité au MouseDown

			mouseButtonEventArgs = null;
		}


		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour les RowHeaders
		/// </summary>
		private void RowHeaderPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			e.Handled = true;	// on ne laisse jamais wpf gérer sinon il sélectionne la ligne

			if (IsEditing) //return;	// le tableau est actif et on est en cours de saisie sur une ligne >> on refuse le changement de ligne
			{
				Debug.WriteLine("clic hors de la zone de saisie");
				return;
			}

			var application = ((App)Application.Current).Appli;

			if (e.ClickCount == 1)
			{
				if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
				if (!application.AttenteConsult && !application.AttenteInput) return;
			}

			byte clickedButton = XHtmlApplication.GetClickedButton(e); // bouton cliqué
			if (clickedButton != 1)
			{
				mouseButtonEventArgs = e;
				return; // click autre que gauche traité au MouseUp
			}

			SendRowHeaderClick((DataGridRowHeader)((Border)sender).TemplatedParent, clickedButton, (byte)((e.ClickCount > 1) ? 2 : 1));
			 */
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseUp" pour les RowHeaders
		/// </summary>
		private void RowHeaderPreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			e.Handled = true;

			if (mouseButtonEventArgs == null || e.OriginalSource != mouseButtonEventArgs.OriginalSource)
				return;	// garde-fou (si pas de MouseDown enregistré ou si la souris a bougé entre-temps)

			SendRowHeaderClick((DataGridRowHeader)((Border)sender).TemplatedParent, XHtmlApplication.GetClickedButton(e), (byte)((mouseButtonEventArgs.ClickCount > 1) ? 2 : 1));

			mouseButtonEventArgs = null;
			 * */
		}


		/// <summary>
		/// Gestionnaire d'évènement "Click" pour les ColumnHeaders
		/// </summary>
		internal void ColumnHeaderClickHandler(object sender, RoutedEventArgs e)
		{
			/*
			e.Handled = true;

			if (IsEditing) //return;	// le tableau est actif et on est en cours de saisie sur une ligne
			{
				Debug.WriteLine("clic hors de la zone de saisie");
				return;
			}

			var application = ((App)Application.Current).Appli;
			if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
			if (!application.AttenteConsult && !application.AttenteInput) return;

			SendColumnHeaderClick((DataGridColumnHeader)sender, 1, 1);
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement "Double Click" pour les entêtes de ColumnHeaders
		/// </summary>
		internal void ColumnHeaderDoubleClickHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			e.Handled = true;

			if (IsEditing) //return;	// le tableau est actif et on est en cours de saisie sur une ligne
			{
				Debug.WriteLine("clic hors de la zone de saisie");
				return;
			}
			SendColumnHeaderClick((DataGridColumnHeader)sender, 1, 2);
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown" pour les ColumnHeaders
		/// </summary>
		internal void ColumnHeaderPreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			/*
			byte clickedButton = XHtmlApplication.GetClickedButton(e); // bouton cliqué
			if (clickedButton == 1) return; // click gauche traité séparément

			e.Handled = true;

			if (IsEditing) //return;	// le tableau est actif et on est en cours de saisie sur une ligne
			{
				Debug.WriteLine("clic hors de la zone de saisie");
				return;
			}
			if (e.ClickCount == 1)
			{
				var application = ((App)Application.Current).Appli;
				if (application.AttentePopupMenu) return; // traité dans un handler séparé (PopupMenuMouseDownHandler)
				if (!application.AttenteConsult && !application.AttenteInput) return;
			}

			SendColumnHeaderClick((DataGridColumnHeader)sender, clickedButton, (byte)((e.ClickCount > 1) ? 2 : 1));
			 * */
		}

		internal void ColumnHeaderPreviewMouseUpHandler(object sender, MouseButtonEventArgs e)
		{
			/* !!!!!!!!!!!!!!!!!
			foreach (XHtmlHogColumn column in Columns.OfType<XHtmlHogColumn>().Where(column => column.SizeChanged))
			{
				column.SizeChanged = false;
				foreach (XHtmlHogCell hogCell in Rows.Select(row => row[Columns.IndexOf(column)]))
					hogCell.UpdateHogCommand();
			}
			 * */
		}

		#endregion Ecouteurs souris

		#region Ecouteurs options d'affichage
		private void OptionsButtonClickHandler(object sender, RoutedEventArgs e)
		{
			//if (columnsMenu == null)
			//{
			//   columnsMenu = new ContextMenu();
			//   columnsMenu.Closed += OptionMenuClosedHandler;
			//}

			//columnsMenu.Items.Clear();
			//foreach (IXHtmlDataGridColumn column in Columns.Cast<IXHtmlDataGridColumn>().Where(column => !column.IsMandatory && !column.IsHiddenForced))
			//{
			//   columnsMenu.Items.Add(new MenuItem
			//   {
			//      Style = Resources["OptionsContextMenuItemStyle"] as Style,
			//      Header = (!column.ColumnHeader.IsHog && !string.IsNullOrEmpty(column.ColumnHeader.Text)) ? column.ColumnHeader.Text : column.ToolTip,
			//      Tag = Columns.IndexOf(((DataGridColumn)column)),
			//      StaysOpenOnClick = true,
			//      IsCheckable = true,
			//      IsChecked = ((DataGridColumn)column).Visibility == Visibility.Visible,
			//   });
			//}

			//if (columnsMenu.Items.Count > 0)	// on n'affiche pas le popup s'il est vide (bordure visible dans ce cas = moche)
			//   columnsMenu.IsOpen = true;

		}

		private void OptionsMenuItemCheckedHandler(object sender, RoutedEventArgs e)
		{
			//MenuItem item = sender as MenuItem;
			//if (item == null) return; // garde-fou

			//int i = (int)item.Tag;
			//Columns[i].Visibility = item.IsChecked ? Visibility.Visible : Visibility.Collapsed;
			//if (columnDisplayOptions.Count > i)
			//   columnDisplayOptions[i] = item.IsChecked;
			//else // columnDisplayOptions est cassé >> on raz
			//{
			//   columnDisplayOptions.Clear();
			//   foreach (DataGridColumn column in Columns)
			//      columnDisplayOptions.Add(column.Visibility == Visibility.Visible);
			//}
		}

		private void OptionMenuClosedHandler(object sender, RoutedEventArgs e)
		{
			// sauvegarde de la liste de DisplayIndexes dans les paramètres utilisateurs
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
			if (key == null) return;

			string regColumnDisplayOptions = columnDisplayOptions.Aggregate("", (current, displayed) => current + (displayed ? "1;" : "0;"));
			key.SetValue("ColumnDisplayOptions", regColumnDisplayOptions.Trim(';')); // on enlève le dernier ';'
			key.Close();
		}
		#endregion Ecouteurs options d'affichage


		/// <summary>
		/// Gestionnaire d'évènement pour le "SizeChanged" des HeaderHog
		/// </summary>
		private void HeaderHogSizeChangedHandler(object sender, SizeChangedEventArgs e)
		{
			/*
			var objGraph = sender as DhObjetGraphique.ObjetGraphique;
			if (objGraph == null)
				return;
			objGraph.ExecuteCommand();
			 * */
		}

		/// <summary>
		/// Gestionnaire d'évènement pour le redimensionnement des entêtes de colonnes
		/// </summary>
		private void ThumbDragDelta(object sender, DragDeltaEventArgs e)
		{
			/*
			Thumb thumb = sender as Thumb;
			if (thumb == null) return;

			double yadjust = ColumnHeaderHeight + e.VerticalChange;
			if (yadjust >= 20 && yadjust < ActualHeight - SystemParameters.HorizontalScrollBarHeight) ColumnHeaderHeight = yadjust;


			// sauvegarde de la nouvelle hauteur d'entête dans les paramètres utilisateurs
			RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Tableaux\" + PageNum + "-" + mask + "-" + name);
			if (key != null)
			{
				key.SetValue("ColumnHeaderHeight", ColumnHeaderHeight.ToString(CultureInfo.InvariantCulture));
				key.Close();
			}


			// envoi notification pour affichage de nouvelle ligne ou suppression de ligne existante
			if (GetRowCount() != Rows.Count)
			{
				var application = ((App)Application.Current).Appli;

				var response = new DVBuffer();
				response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);						//début de l'acquittement ou de la réponse

				if (Page.Window.ActiveControl != this) application.SetInputBuffer(response);

				PrepareResponse(response);

				response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);						// Type de clic tableau (ici : LIST_CLICK_HEIGHT_CHANGING = 19)
				response.Put((byte)DataGridClick.ChangeRowCount);

				response.Put(ProprietesWpf.IDENT_UNIQUE);								// Id du tableau cliquée (uint)
				response.Put(Id);
				response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);					// Numéro du point de séquence du tableau cliqué
				response.Put(SeqPoint);

				response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);					// Fin de l'envoi des évenements souris

				if (application.AttenteInput || application.AttenteConsult)
					application.Send(response);			// envoi de la réponse et attente de la suite
				else application.AsynchronousResponse = response;
			}
			 * */
		}

		//#endregion Ecouteurs


		#region Drag & Drop
		public void CellPreviewMouseLeftButtonDownHandler(object sender, MouseEventArgs e)
		{
			//if (e == null) throw new ArgumentNullException("e");

			//startPoint = e.GetPosition(null);

			//// on remplit la donnée de drag sur le mouseDown pour que la valeur stockée soit celle de la cellule sur laquelle le mouseDown a été fait
			//// et non celle sur laquelle le drag s'est déclenché. (elles peuvent être différentes si on fait le mouseDown initial proche de la bordure de cellule)

			//var cell = (DataGridCell)sender;
			//var row = cell.DataContext as XHtmlRow;
			//var cellDesc = cell.ToString().Split(':');

			//draggedData = new DataObject(
			//   cellDesc.Length > 1 && !string.IsNullOrEmpty(cellDesc[1])
			//   ? cellDesc[1].Trim()
			//   : row[Columns.IndexOf(cell.Column)].StringValue
			//); // pour ajouter une donner texte dans le D&D (D&D vers word etc... contenu de la cellule uniquement)

			//if (allowDrag)
			//   draggedData.SetData("DataGridDataFormat", new DataGridDataContainer
			//   {
			//      ProcessId = Process.GetCurrentProcess().Id,
			//      SourceId = listId,
			//      DragName = dragName,
			//      DragNameDefault = mask.ToLowerInvariant() + "_" + name.ToLowerInvariant() + "_" + PageNum,
			//      DragData = GetDragDropData(row),
			//      DragRow = dragRow,
			//      ColIndex = (ushort)(Columns.IndexOf(cell.Column) + 1),
			//      RowIndex = (ushort)(Rows.IndexOf(row) + 1)
			//   });
		}
		public void CellPreviewMouseMoveHandler(object sender, MouseEventArgs e)
		{
			/*
			if (e == null) throw new ArgumentNullException("e");

			if (isDragging) return;
			if (draggedData == null) return;
			if (e.LeftButton != MouseButtonState.Pressed) return;

			// on ne déclenche le drag&drop que si la souris a été suffisament déplacée
			Point mousePos = e.GetPosition(null);
			Vector diff = startPoint - mousePos;

			DataGridCell cell = (DataGridCell)sender;
			XHtmlRow row = cell.DataContext as XHtmlRow;
			if (row == null || (row == CurrentItem && ((App)Application.Current).Appli.AttenteInput)) return; // ListInput: d&d interdit sur la ligne en cours

			if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
			{
				isDragging = true;
				DragDrop.DoDragDrop(cell, draggedData, DragDropEffects.All);
				isDragging = false;
			}
			 * */
		}

		/// <summary>
		/// crée l'adorner et l'attache au curseur le cas échéant
		/// </summary>
		public void CellDragEnterHandler(object sender, DragEventArgs e)
		{
			/*
			FrameworkElement cell = (FrameworkElement)sender;

			// si D&D cellule tableau dans ligne de création ou pas drop tableau, on sort
			if (!e.Data.GetDataPresent("DataGridDataFormat") || (cell.DataContext as XHtmlRow == null)) return;

			FrameworkElement dragScope = dropRow ? DataGridRow.GetRowContainingElement(cell) : cell;
			adorner = new DataGridDragAdorner(dragScope);
			if (dataInsert) ((DataGridDragAdorner)adorner).Offset = e.GetPosition(cell).Y;
			AdornerLayer layer = AdornerLayer.GetAdornerLayer(dragScope);
			layer.Add(adorner);
			 * */
		}

		/// <summary>
		/// modifie l'adorner en fonction du DropInsert
		/// </summary>
		public void CellDragOverHandler(object sender, DragEventArgs e)
		{
			/*
			if (e.Data.GetDataPresent("DataGridDataFormat") && (adorner is DataGridDragAdorner) && dataInsert)
				((DataGridDragAdorner)adorner).Offset = e.GetPosition((FrameworkElement)sender).Y;
			 * */
		}

		public void CellDragEnterOverHandler(object sender, DragEventArgs e)
		{
			/*
			var application = ((App)Application.Current).Appli;

			// Drop fichier
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effects = application.StackOfWindows.Peek().AllowDrop ? DragDropEffects.All : DragDropEffects.None;
				e.Handled = true;
				return;
			}

			// Drop tableau
			DataGridDataContainer data = e.Data.GetData("DataGridDataFormat") as DataGridDataContainer;

			if (!allowDrop || data == null || (application.AttenteInput && Page.Window.ActiveControl is DataGrid))
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}

			// gestion du dropEmpty sur la ligne de création (autorisé ou non)
			if (!allowEmptyDrop && (((FrameworkElement)sender).DataContext as XHtmlRow == null))
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}


			bool local = (data.ProcessId == Process.GetCurrentProcess().Id);

			// gestion du drop externe (autorisé ou non)
			if (!allowExternalDrop && !local)
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}

			if (dropNames != null && dropNames.Any() && !(dropNames.Contains(data.DragName) || dropNames.Contains(data.DragNameDefault)))
				e.Effects = DragDropEffects.None; // gestion des sources autorisées
			else if (local && dataMove && !(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
				e.Effects = DragDropEffects.Move; // DropMove: curseur de déplacement plutôt que copie
			else
				e.Effects = DragDropEffects.Copy; // curseur par défaut: copie

			e.Handled = true; // sinon WPF rechange le curseur derrière
			 * */
		}

		/// <summary>
		/// Garde-fou: suppression du adorner en cas de drag-drop tableau sur le DragLeave
		/// </summary>
		public void CellDragLeaveHandler(object sender, DragEventArgs e)
		{
			/* !!!!!!!!
			FrameworkElement dragScope = sender as FrameworkElement;
			if (dragScope != null && adorner != null) AdornerLayer.GetAdornerLayer(dragScope).Remove(adorner);
			 * */
		}


		/// <summary>
		/// crée l'adorner et l'attache au curseur le cas échéant
		/// </summary>
		private void DragEnterHandler(object sender, DragEventArgs e)
		{
			/* !!!!!!!!!!!!!!!!!!
			if (!e.Data.GetDataPresent("DataGridDataFormat")) return;

			FrameworkElement dragScope = this;
			adorner = new DragAdorner(dragScope, dropRow);
			((DragAdorner)adorner).LeftOffset = e.GetPosition(dragScope).X;
			((DragAdorner)adorner).TopOffset = e.GetPosition(dragScope).Y;
			AdornerLayer layer = AdornerLayer.GetAdornerLayer(dragScope);
			layer.Add(adorner);
			 * */
		}

		/// <summary>
		/// déplace l'adorner avec le curseur le cas échéant
		/// </summary>
		private void DragOverHandler(object sender, DragEventArgs e)
		{
			/*
			if (!e.Data.GetDataPresent("DataGridDataFormat") || (adorner == null)) return;

			FrameworkElement dragScope = this;
			((DragAdorner)adorner).LeftOffset = e.GetPosition(dragScope).X;
			((DragAdorner)adorner).TopOffset = e.GetPosition(dragScope).Y;
			 * */
		}

		private void DragEnterOverHandler(object sender, DragEventArgs e)
		{
			/*
			var application = ((App)Application.Current).Appli;

			// Drop fichier
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effects = application.StackOfWindows.Peek().AllowDrop ? DragDropEffects.All : DragDropEffects.None;
				e.Handled = true;
				return;
			}

			// Drop tableau
			DataGridDataContainer data = e.Data.GetData("DataGridDataFormat") as DataGridDataContainer;

			if (!allowDrop || data == null || (application.AttenteInput && Page.Window.ActiveControl is DataGrid))
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}

			bool local = (data.ProcessId == Process.GetCurrentProcess().Id);

			// gestion du drop externe (autorisé ou non)
			if (!allowExternalDrop && !local)
			{
				e.Effects = DragDropEffects.None;
				e.Handled = true;
				return;
			}

			if (!allowEmptyDrop || dropNames != null && dropNames.Any() && !(dropNames.Contains(data.DragName) || dropNames.Contains(data.DragNameDefault)))
				e.Effects = DragDropEffects.None; // DropEmpty: drop géré au niveau des cellules uniquement + gestion des sources autorisées
			else if (local && dataMove && !(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
				e.Effects = DragDropEffects.Move; // DropMove: curseur de déplacement plutôt que copie
			else
				e.Effects = DragDropEffects.Copy; // curseur par défaut: copie

			e.Handled = true; // sinon WPF rechange le curseur derrière
			 * */
		}

		private void DragLeaveHandler(object sender, DragEventArgs e)
		{
			/*
			FrameworkElement dragScope = sender as FrameworkElement;
			if (adorner != null) AdornerLayer.GetAdornerLayer(dragScope).Remove(adorner);
			 * */
		}

		private void DropHandler(object sender, DragEventArgs e)
		{
			/*
			e.Handled = true;

			RemoveAdorner(this);

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
				if (data == null) return;

				// gestion du drop externe (autorisé ou non)
				if (!allowExternalDrop && (data.ProcessId != Process.GetCurrentProcess().Id)) return;

				// gestion des sources autorisées
				if (dropNames != null && dropNames.Any() && !(dropNames.Contains(data.DragName) || dropNames.Contains(data.DragNameDefault))) return;

				SendDropEvent(data);
			}
			 * */
		}


		/// <summary>
		/// Garde-fou: suppression du adorner en cas de drag-drop tableau (si l'on droppe dans la fenêtre)
		/// </summary>
		/// <param name="dragScope">conteneur du Adorner</param>
		public void RemoveAdorner(Visual dragScope)
		{
			// !!!!!!!!!! if (adorner != null) AdornerLayer.GetAdornerLayer(dragScope).Remove(adorner);
		}

		public void SendDropEvent(DataGridDataContainer data)
		{
			if (allowEmptyDrop) SendDropEvent(data, null, null, null); // gestion du dropEmpty (pour la ligne de création)
		}
		public void SendDropEvent(DataGridDataContainer data, int? colIndex, int? rowIndex, double? relativeDropPosition)
		{
			/*
			var application = ((App)Application.Current).Appli;

			if (!application.AttenteInput && !application.AttenteConsult) return; // garde-fou

			// gestion du drop externe (autorisé ou non)
			if (!allowExternalDrop && (data.ProcessId != Process.GetCurrentProcess().Id)) return;

			// gestion des sources autorisées
			if (dropNames != null && dropNames.Any() && !(dropNames.Contains(data.DragName) || dropNames.Contains(data.DragNameDefault))) return;

			byte local = (byte)((data.ProcessId == Process.GetCurrentProcess().Id) ? 1 : 0);
			byte mode;
			if (dataInsert) // mode "insertion"
			{
				if (local == 1 && dataMove && !(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))) // déplacement
				{
					if (relativeDropPosition < 1.0 / 3) mode = 21;			// déplacement avant
					else if (relativeDropPosition > 2.0 / 3) mode = 22;	// déplacement après
					else mode = 2;														// déplacement normal
				}
				else // copie
				{
					if (relativeDropPosition < 1.0 / 3) mode = 11;			// copie avant
					else if (relativeDropPosition > 2.0 / 3) mode = 12;	// copie après
					else mode = 1;														// copie normal
				}
			}
			else // mode "normal"
			{
				if (local == 1 && dataMove && !(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))) mode = 2; // déplacement
				else mode = 1; // copie
			}

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);					//début de l'acquittement ou de la réponse

			application.SetInputBuffer(response);
			PrepareResponse(response);

			response.Put(ProprietesWpf.TABLEAU_TYPE_CLIC);					// Type de clic tableau
			response.Put((byte)DataGridClick.Drop);

			// Informations sur le clic lui-même, à savoir (pour le moment) :
			response.Put(ProprietesWpf.SOURIS_CLIC);						// 1 = simple clic; 2 = double clic (byte)
			response.Put((byte)1);
			response.Put(ProprietesWpf.SOURIS_BOUTON);					// 1 = gauche; 2 = milieu; 3 = droite (byte)
			response.Put((byte)1);
			response.Put(ProprietesWpf.SOURIS_TOUCHE);					// touche(s) du clavier enfoncée(s) simultanément (byte)
			response.Put(XHtmlApplication.GetPressedKey());

			if (rowIndex.HasValue)
			{
				response.Put(ProprietesWpf.TABLEAU_NUMERO_LIGNE);			// n° de ligne de la cellule réceptrice le cas échéant (ushort)
				response.Put((ushort)(rowIndex.Value + 1));
			}

			if (colIndex.HasValue)
			{
				response.Put(ProprietesWpf.TABLEAU_NUMERO_COLONNE);		// n° de colonne de la cellule réceptrice le cas échéant (ushort)
				response.Put((ushort)(colIndex.Value + 1));

				response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);			// n° du point de séquence de la cellule réceptrice le cas échéant (ushort)
				response.Put(((IXHtmlDataGridColumn)Columns[colIndex.Value]).SeqPoint);
			}

			response.Put(ProprietesWpf.TABLEAU_DRAG_AND_DROP);				// propriétés de drag & drop du tableau éméteur
			response.Put(local);						// Flag "drop local" (byte = 1 si local, 0 sinon)
			response.Put(data.SourceId);			// Ident DIVA du tableau émetteur (uint)
			response.Put(data.ColIndex);			// N° de colonne cellule de départ (ushort)
			response.Put(data.RowIndex);			// N° de ligne cellule de départ (ushort)
			response.Put(mode);						// Mode (byte) voir la doc = valeurs rendues dans Harmony.DropMode

			response.Put(ProprietesWpf.TABLEAU_DRAG_AND_DROP_MEMORY);	// données dragées
			response.PutString(data.DragData);

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);				// fin de l'envoi des évenements souris

			application.Send(response);											// envoi de la réponse et attente de la suite

			Debug.WriteLine("Drag & Drop tableau. source_id:" + data.SourceId + " - col: " + data.ColIndex + " - lig:" + data.RowIndex);
			 * */
		}

		private string GetDragDropData(XHtmlRow row)
		{
			return "";
			/* !!!!!!!!!!!!!!!!!!!!!!!!!
			string result = "";

			result += "{DivaltoDragDrop}" + "1";
			result += "{DragName}" + dragName;
			result += "{DragNameDefaut}" + mask.ToLowerInvariant() + "_" + name.ToLowerInvariant() + "_" + PageNum;
			result += "{DragInfo}" + dragInfo;
			result += "\n";

			foreach (IXHtmlDataGridColumn column in Columns)
			{
				int colIndex = Columns.IndexOf((DataGridColumn)column);
				if (column.Presentation.Visibilite == Visibilite.Cache
					 || column.Presentation.Visibilite == Visibilite.Illisible
					 || row[colIndex].Presentation.Visibilite == Visibilite.Cache
					 || row[colIndex].Presentation.Visibilite == Visibilite.Illisible
					 || column is XHtmlPasswordBoxColumn)
					continue;

				result += "{Col}" + (colIndex + 1);
				result += "{Record}" + column.RecordName;
				result += "{Data}" + column.DataName;
				if (column.Indexes != null)
					for (int i = 0; i < 4; i++)
						if (column.Indexes[i] > 0) result += "{Index" + (i + 1) + "}" + column.Indexes[i];
				result += "{Value}" + row[colIndex].StringValue;

				XHtmlTreeCell treeCell = row[colIndex] as XHtmlTreeCell;
				if (treeCell != null)
				{
					result += "{Niveau}" + treeCell.CurrentLevel;							// n° de niveau
					result += "{Suite}" + (treeCell.IsNotTheLastItem ? "1" : "0");		// 0 ou 1
					result += "{SousNiveau}" + (treeCell.HasSubLevels ? "1" : "0");	// 0 ou 1
					result += "{Expanse}" + (treeCell.Expanded ? "1" : "0");				// 0 ou 1
					result += "{BitmapRed}" + treeCell.RowReducedImageSource;			// nom ou vide
					result += "{BitmapExp}" + treeCell.RowExpandedImageSource;			// nom ou vide
					result += "{Libel}" + treeCell.StringValue;								// libellé
				}

				result += "\n";
			}
			return result;
			 * */
		}
		#endregion Drag & Drop


		public void AjouterEnvoisUnObjetRemplissageTableau(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			int numlig = 1;
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
			ListeParametresEnvoyes envoisPourPresentation = new ListeParametresEnvoyes();

			envois.Ajouter("setObjetCourant", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			foreach (XHtmlRow row in this.Rows)
			{
				// row.Indice est le numero de ligne
				StringBuilder ligne = new StringBuilder();
				int numcol = 1;
				ligne.Append("{");
				foreach (XHtmlGenericCell cell in row)
				{
					// cell.Indice = l'ident de la colonne
					if (cell.ValeurSpecifiee)
						ligne.Append("\"c").Append(cell.Indice.ToString()).Append("\":").Append('"').Append(cell.StringValue).Append('"').Append(",");


					// VOIR 		public void SetStyle() !!!!!!!!!!!!!!!!!!

					if (cell.Presentation.OnALuQuelqueChose == true)
					{
						if (cell.Presentation.BackgroundFlag)
							envoisPourPresentation.Ajouter("css-fond-cell", row.Indice.ToString() + "," + cell.Indice.ToString(),
																		XHtmlColor.PrefixeCssFond + cell.Presentation.idFond.ToString());

						if (cell.Presentation.FontFlag)
							envoisPourPresentation.Ajouter("css-police-cell", row.Indice.ToString() + "," + cell.Indice.ToString(),
																		XHtmlFont.PrefixeCss + cell.Presentation.idPolice.ToString());


					}
					numcol++;
				}
				if (ligne[ligne.Length - 1] == ',')
					ligne.Remove(ligne.Length - 1, 1);
				ligne.Append("}");
				paramsValeurs.Ajouter("uneligne", row.Indice.ToString() + "," +
															row.Operation.ToString() + "," +
															row.IndicePourInsertion.ToString()
															, ligne.ToString());
				numlig++;
			}
			Rows.Clear();				// sinon on va le faire en double

			envois.Ajouter("ajouterLignesTableau", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false),
				HtmlGlobal.CalculerId(this.Id, page.Id, niveau));

			envois.commandes.AddRange(envoisPourPresentation.commandes);

		}


		// VOIR au dessus pour les cellules
		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			string css = "";
			string idtab = HtmlGlobal.CalculerId(this.Id, page.Id, niveau);
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();

			if (Page.Html.ListeIdentsTablesExistantesCoteClient.FirstOrDefault(e => e == idtab) == null)
			{
				DescriptionJqGrid jqGrid = new DescriptionJqGrid();

				paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id));

				if (this.Presentation != null)
				{
					jqGrid.width = (double) this.Presentation.OriginalWidth;
					jqGrid.height = (double)this.Presentation.OriginalHeight;
				}
				UnParametreEnvoye p = new UnParametreEnvoye();


				// voir l'option gridview:true qui permet d'accélerer !!!!!!!!!!!!!!!!


				foreach (IXHtmlDataGridColumn col in this.Columns)
				{
					ColonneJqGrid cc = new ColonneJqGrid();
					jqGrid.colModel.Add(cc);

					jqGrid.colNames.Add(HttpUtility.HtmlEncode(col.ColumnHeader.Text));

					cc.name = "c" + col.Id;
					cc.width = col.Width;

					if (col.ColumnBackground != 0 || col.ColumnFont != 0)			//!!!!!!!!!!!!!!!!!! a revoir, HasValue
					{
						if (col.ColumnBackground != 0)
							cc.classes += XHtmlColor.PrefixeCssFond + col.ColumnBackground.ToString();
						if (col.ColumnFont != 0)
							cc.classes += " " + XHtmlFont.PrefixeCss + col.ColumnFont.ToString();
					}

				//	if (col.Id > 15) 
				//		cc.hidden = true;

					string js = HtmlGlobal.ToJsonString(cc,Page.Html.JsonCreationColonnes,false);
				}

				string jsmodeles = HtmlGlobal.ToJsonString(jqGrid, this.Page.Html.JsonListeCreationColonnes, false);

				paramsValeurs.Ajouter("tout", jsmodeles);

				envois.Ajouter("creerObjetTableau", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false), HtmlGlobal.CalculerId(this.Id, page.Id, niveau));


				paramsValeurs = new ListeParametresEnvoyes();
				paramsValeurs.Ajouter("idPage", this.Page.Html.CalculerIdPage(page.Id));
				if (this.Presentation != null)
				{
					css = this.Presentation.GenererHtml(paramsValeurs,this.Page.Html,this.codePage,false);

					UnParametreEnvoye ppe = paramsValeurs.commandes.FirstOrDefault(e => e.commande == "css-hauteur");
					if (ppe != null)
						paramsValeurs.commandes.Remove(ppe);
					ppe = paramsValeurs.commandes.FirstOrDefault(e => e.commande == "css-largeur");
					if (ppe != null)
						paramsValeurs.commandes.Remove(ppe);


					// si je veux jouer sur la position, il faut passer par l'objet id = gbox_xxxx qui est celui qui englobe tout le tableau
					envois.Ajouter("setObjetCourant", "gbox_" + HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
					envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, page.Html.JsonParamsEnvoyes, false));
				}

			}
			else
			{

				envois.Ajouter("setObjetCourant", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));

				foreach (IXHtmlDataGridColumn col in this.Columns)
				{
					if (string.IsNullOrEmpty(col.ColumnHeader.Text) == false)
					{
						envois.Ajouter("txtEnteteCol", "c" + col.Id.ToString(),HttpUtility.HtmlEncode(col.ColumnHeader.Text));
					}
				}
				Columns.Clear();	// sinon on va le faire en double
			}
		}




	}



}