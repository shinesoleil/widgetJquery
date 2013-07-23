using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using System.Globalization;
using System.Collections.ObjectModel;
using Divalto.Systeme.XHtml;
using System.Runtime.Serialization;
//using MaquetteOnglet;

namespace Divaltohtml
{

	[DataContract]
	public class XHtmlPage : FrameworkElement, IXHtmlObject
	{
		public HtmlGlobal Html;

		[DataMember]
		uint IXHtmlObject.Id { get; set; }
		public ushort SeqPoint { get; set; }
		public FrameworkElement FrameworkElement { get { return this; } }

		[DataMember]
		public XHtmlPresentation Presentation { get; set; }
		public Collection<string> ListOfValidButtons { get; private set; }
		public XHtmlPage Page { get; set; }
		public Collection<object> Children; //  = new Collection<object>();	//!!!!!!!!!!!!!!!! a voir !!!!!!!!!!!!

		//[DataMember]
		//public bool Transmi; // = false;

		[DataMember]
		public string Id
		{
			get { return id; }
			set
			{
				id = string.IsNullOrEmpty(value) ? "" : value;
				string[] idSplit = id.Split(new[] { 'M', 'P', 'L', 'C' }, StringSplitOptions.RemoveEmptyEntries);
				NumMasque = byte.Parse(idSplit[0], CultureInfo.InvariantCulture);
				NumPage = byte.Parse(idSplit[1], CultureInfo.InvariantCulture);
			}
		}
		private string id;

		internal Collection<IXHtmlObject> ListOfObjects; // = new Collection<IXHtmlObject>();
		internal XHtmlWindow Window;

		internal byte NumPage;
		internal ushort NumMasque;
		[DataMember]
		internal ushort StopPoint;
		[DataMember]
		public ushort OriginalWidth, OriginalHeight;
		[DataMember]
		public ushort left, top;
		public bool attachementDroite, attachementBas, largeurVariable, hauteurVariable;
		[DataMember]
		public byte Effacement;
		[DataMember]
		public bool JeSuisAffichee;

		[DataMember]
		public ushort? CouleurDeFond;

		private readonly Collection<Grid> listOfGrids = new Collection<Grid>();									// pour les grilles
		private readonly Collection<string> listOfExtendablePageIds = new Collection<string>();			// pour le volet


		public bool PositionLue, TailleLue;
		public XHtmlPage ParentPage;

		public void Init()
		{
			Children = new Collection<object>();	//!!!!!!!!!!!!!!!! a voir !!!!!!!!!!!!
			//Transmi = false;
			ListOfObjects = new Collection<IXHtmlObject>();
			Presentation = new XHtmlPresentation(this);

		}
		[OnDeserializing]
		public void OnDeserialisation(StreamingContext c)
		{
			Init();
		}

		/// <summary>
		/// Initializes a new instance of the XwpfPage class.
		/// </summary>
		public XHtmlPage(XHtmlWindow window,HtmlGlobal html)
		{
		// !!!!!!!	InitializeComponent();
			Init();
			Window = window;
			Html = html;
			Page = this;

			// écouteurs sur le "MainCanvas" de la fenêtre pour largeur / hauteur variable et attachements
			// !!!!!!!!!!!! Window.MainCanvas.SizeChanged += MainCanvasSizeChangedHandler;

			// on empêche tout clic (IsHitTestVisible = false inutilisable car hérité par tous les enfants)
			// !!!!!!!!!!!!!!!! MouseDown += (sender, e) => { e.Handled = true; };
		}


		public XHtmlPage(HtmlGlobal html)
		{
			// !!!!!!!	InitializeComponent();
			Html = html;
			Init();
			Page = this;
			// écouteurs sur le "MainCanvas" de la fenêtre pour largeur / hauteur variable et attachements
			// !!!!!!!!!!!! Window.MainCanvas.SizeChanged += MainCanvasSizeChangedHandler;

			// on empêche tout clic (IsHitTestVisible = false inutilisable car hérité par tous les enfants)
			// !!!!!!!!!!!!!!!! MouseDown += (sender, e) => { e.Handled = true; };
		}

		/// <summary>
		/// Removes the pages below the current page is necessary, depending on the overlapping code
		/// </summary>
		/// <param name="overlapping">overlapping code</param>
		private void SetOverlapping(byte overlapping)
		{
//			XHtmlDrawing drawing = Window.MainCanvas.Children.OfType<XHtmlDrawing>().FirstOrDefault();

			switch (overlapping)
			{
				case 1:	// effacement partiel
					// on parcourt la liste des pages de la fenêtre et on efface les pages recouvertes totalement par la page en cours
					foreach (XHtmlPage page in Window.ListOfPages.Where(page => page != this && page.JeSuisAffichee == true))
					{
						if (page.left >= left
							&& page.top >= top
							&& (page.left + page.OriginalWidth) <= (left + OriginalWidth)
							&& (page.top + page.OriginalHeight) <= (top + OriginalHeight))
						{
							// On ne peut pas supprimer les Handlers sinon on ne les a plus lors d'un réaffichage de la page (ajoutés uniquement dans le constructeur)
							//Window.MainCanvas.SizeChanged -= page.SizeChangedHandler;
							//Window.MainCanvas.SizeChanged -= page.PositionChangedHandler;
							
							// Window.MainCanvas.Children.Remove(page);
							
							UnParametreEnvoye p = new UnParametreEnvoye();
							Html.Envois.Ajouter("retirerPage", Html.CalculerIdPage(page.Id));
							page.JeSuisAffichee = false;

							////Effacement de la zone recouverte par la page dans Ygraph
							//if (drawing != null)
							//   //drawing.ClearScreenArea(this);
							//   drawing.ClearScreenArea(new Rect(new Point(page.left, page.top), new Size(page.OriginalWidth, page.OriginalHeight)));
						}
					}
					break;

				case 2:	// effacement complet
					// on parcourt la liste des pages de la fenêtre et on efface toutes les pages trouvées
					foreach (XHtmlPage page in Window.ListOfPages.Where(page => page != this && page.JeSuisAffichee == true))
					{
						// On ne peut pas supprimer les Handlers sinon on ne les a plus lors d'un réaffichage de la page (ajoutés uniquement dans le constructeur)
						//Window.MainCanvas.SizeChanged -= page.SizeChangedHandler;
						//Window.MainCanvas.SizeChanged -= page.PositionChangedHandler;
//						Window.MainCanvas.Children.Remove(page);
						Html.Envois.Ajouter("retirerPage", this.Html.CalculerIdPage(page.Id));
						page.JeSuisAffichee = false;
					}

					////RAZ du canvas de dessin de Ygraph
					//if (drawing != null) drawing.ClearDrawing();

					// en effacement complet, la fenêtre prend la couleur de fond de la page
					//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! a faire !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//					Window.SetBinding(BackgroundProperty, new Binding("Background") { Source = this, Mode = BindingMode.OneWay });
					break;

				case 3:	// pas d'effacement
					// on ne fait rien vu qu'on n'efface rien.
					// en fait si : on met le fond à null (et vive la bidouille !!!) - "null" et non pas "transparent" car avec "transparent" le canvas trappe les clics quand même
//					Style = Resources["PageTransStyle"] as Style;
					break;
			}
		}


		public void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;
			uint idObj;	// variable de stockage temporaire des identifiants d'objets

			buffer.Get(out property);
			while (property != ProprietesWpf.PAGE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_GRILLE_MERE:	// Ident unique de la grille (unint) + Numéro de la cellule (début à 0)
						uint gridId;
						ushort cellIndex;
						buffer.Get(out gridId);
						buffer.Get(out cellIndex);
						Presentation.GridId = gridId;
						Presentation.CellIndex = cellIndex;
						break;

					case ProprietesWpf.PAGE_POSITION:				// position en x (ushort) et position en y (ushort)
						PositionLue = true;
						buffer.Get(out left);
						buffer.Get(out top);
						break;

					case ProprietesWpf.PAGE_TAILLE:					// largeur (ushort) et hauteur (ushort)
						TailleLue = true;
						buffer.Get(out OriginalWidth);
						buffer.Get(out OriginalHeight);
						break;

					case ProprietesWpf.PAGE_EFFACEMENT:				// type d'effacement (byte)
						byte overlapping;
						buffer.Get(out overlapping);
						this.Effacement = overlapping;
						SetOverlapping(overlapping);
						break;

					case ProprietesWpf.PAGE_ATTACHEMENT_DROITE:	// (uniquement si attachement à droite)
						attachementDroite = true;
						break;

					case ProprietesWpf.PAGE_ATTACHEMENT_BAS:		// (uniquement si attachement en bas)
						attachementBas = true;
						break;

					case ProprietesWpf.PAGE_LARGEUR_EXTENSIBLE:	// (uniquement si largeur variable)
						largeurVariable = true;
						break;

					case ProprietesWpf.PAGE_HAUTEUR_EXTENSIBLE:	// (uniquement si hauteur variable)
						hauteurVariable = true;
						break;

					case ProprietesWpf.PAGE_ARRET_SAISIE:			// numéro de point d'arrêt "Demande de saisie" (ushort)
						buffer.Get(out StopPoint);
						break;

					case ProprietesWpf.PAGE_COULEUR_FOND:			// identifiant de couleur (ushort)
						ushort idColor;
						buffer.Get(out idColor);
						CouleurDeFond = idColor;
//!!!!!!!!!!!						Brush background = Application.Current.Resources["Brush-" + idColor] as Brush;
//!!!!!!!!!!!!!						if (background != null) Background = background;
						break;


					case ProprietesWpf.BOUTON_DEBUT:						// Bouton (Xwin :  objet bouton)
						buffer.Get(out idObj);
						//!!!!!!!!!!!!!!!!!!!!!!!!!
						XHtmlButton button = (XHtmlButton)GetObject(idObj);
						if (button == null)
						{
							button = new XHtmlButton(this) { Id = idObj };
							Children.Add(button);
							ListOfObjects.Add(button);
						}
						button.ReadProperties(buffer);

						button.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());

						break;


					case ProprietesWpf.TEXTE_DEBUT:						// Label (Xwin : objet texte)
						{
							buffer.Get(out idObj);
							XHtmlLabel label = (XHtmlLabel)GetObject(idObj);
							if (label == null)
							{
								label = new XHtmlLabel(this) { Id = idObj };
								Children.Add(label);
								ListOfObjects.Add(label);
							}
							label.ReadProperties(buffer);
							label.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						}
						break;

					case ProprietesWpf.GROUPBOX_DEBUT:
						{
							buffer.Get(out idObj);
							XHtmlGroupBox groupe = (XHtmlGroupBox)GetObject(idObj);
							if (groupe == null)
							{
								groupe = new XHtmlGroupBox(this) { Id = idObj };
								Children.Add(groupe);
								ListOfObjects.Add(groupe);
							}
							groupe.ReadProperties(buffer);
							groupe.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						}
						break;

					#region Champ
					case ProprietesWpf.CHAMP_DEBUT:						// TextBox (Xwin : objet champ)
						buffer.Get(out idObj);
						XHtmlTextBox textBox = (XHtmlTextBox)GetObject(idObj);
						if (textBox == null)
						{
							textBox = new XHtmlTextBox(this) { Id = idObj };
							Children.Add(textBox);
							ListOfObjects.Add(textBox);
						}
						textBox.ReadProperties(buffer);
						textBox.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						break;
					#endregion Champ

					#region Multichoix
					case ProprietesWpf.MULTICHOIX_DEBUT:				// ComboBox (Xwin : objet multichoix)
						buffer.Get(out idObj);
						XHtmlComboBox comboBox = (XHtmlComboBox)GetObject(idObj);
						if (comboBox == null)
						{
							comboBox = new XHtmlComboBox(this) { Id = idObj };
							Children.Add(comboBox);
							ListOfObjects.Add(comboBox);
						}
						comboBox.ReadProperties(buffer);
						comboBox.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						break;
					#endregion Multichoix

					#region Case à cocher
					case ProprietesWpf.CASE_A_COCHER_DEBUT:			// CheckBox (Xwin : objet case à cocher)
						buffer.Get(out idObj);
						XHtmlCheckBox checkBox = (XHtmlCheckBox)GetObject(idObj);
						if (checkBox == null)
						{
							checkBox = new XHtmlCheckBox(this) { Id = idObj };
							Children.Add(checkBox);
							ListOfObjects.Add(checkBox);
						}
						checkBox.ReadProperties(buffer);
						checkBox.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						break;
					#endregion Case à cocher

					#region Groupe d'onglets
					case ProprietesWpf.GROUPE_ONGLET_DEBUT:					
						uint idTc;
						buffer.Get(out idTc);
						XHtmlTabControl tabControl; //  = Window.ListOfTabControls.FirstOrDefault(tc => (tc.Page.NumMasque == NumMasque && tc.Id == idTc));
//						if (tabControl == null)
						{
							tabControl = new XHtmlTabControl(this) { Id = idTc };
							// Window.ListOfTabControls.Add(tabControl);
						}
						//else
						//{
						//	tabControl.Page.Children.Remove(tabControl);
						//	tabControl.Page = this;
						//}
						Children.Add(tabControl); // on ajoute les groupes d'onglets à la page elle-même et non à InternalGrid pour pouvoir justement décaler InternalGrid par rapport aux onglets
						ListOfObjects.Add(tabControl); // !!!!!!!!!!!!!! bh je l'ai ajouté mais je ne sais pas si c utile
						tabControl.ReadProperties(buffer);
						tabControl.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						break;
					#endregion Groupe d'onglets


					case ProprietesWpf.GRILLE_DEBUT:									// Grille(s)
						SetGrid(buffer);
						break;

					#region Page panel
					case ProprietesWpf.PAGE_DEBUT:
						{
							string idPage;
							bool nouvelle = false;

							buffer.GetString(out idPage); // Id Page (string)

							XHtmlPage page = Window.GetPage(idPage);

							if (page == null)
							{
								nouvelle = true;
								page = new XHtmlPage(Window,this.Html) { Id = idPage, ParentPage = this };
								this.Html.Envois.Ajouter("nouveauPanel", this.Html.CalculerIdPage(page.Id));
								this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(page.Id)) ; // , page.StopPoint.ToString(), page.NumPage.ToString());
								page.ReadProperties(buffer);
								Window.ListOfPages.Add(page);
								page.JeSuisAffichee = true;

							}
							else
							{
								nouvelle = false;
								this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(page.Id)); // , page.StopPoint.ToString(), page.NumPage.ToString());
								page.ReadProperties(buffer);
								//!!!!!!!!!!!!!!!!!! que faut il faire ici ?
								// rien je pense : je envoie les objets modifiés
							}

							page.EnvoyerCouleurPositionAttachement(true);
							this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(page.Id), page.StopPoint.ToString(), page.NumPage.ToString());




							//ajout de la page panel à la cellule de grille (elle peut avoir été supprimée : cas des onglets)
							// bh on le fait de l'autre coté
							//if (page.Parent == null) AddToGrid(page);
							string p = "{" +
								        "\"idPanel\":\"" + this.Html.CalculerIdPage(page.Id) + "\"," +
										  "\"idMere\":\"" + Html.CalculerIdPage(this.Id) + "\"," +
										  "\"idGrille\":\"" + HtmlGlobal.CalculerId(page.Presentation.GridId, this.Id, this.Html.App.StackOfWindows.Count()) + "\"," +
										  "\"iCellule\":" + page.Presentation.CellIndex +
										  "}";

							this.Html.Envois.Ajouter("ajouterPanelAGrilleSiPasDedans",p);



						}
						break;
					#endregion Page panel



					#region Tableau
					case ProprietesWpf.TABLEAU_DEBUT:					// DataGrid (Xwin : objet tableau)
						buffer.Get(out idObj);
						XHtmlDataGrid dataGrid = (XHtmlDataGrid)GetObject(idObj);
						if (dataGrid == null)
						{
							dataGrid = new XHtmlDataGrid(this) { Id = idObj };
							Children.Add(dataGrid);
							ListOfObjects.Add(dataGrid);
						}
						dataGrid.ReadProperties(buffer);
						dataGrid.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());
						break;
					#endregion Tableau



					case ProprietesWpf.BOUTONS_VALIDES_DEBUT:						// Boutons valides dans la page en cours
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
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Page, property.ToString());
				}

				buffer.Get(out property);
			}

			SetSize(); // Mise à jour de la taille
			SetPosition(); // Mise à jour de la position
		}


		public void EnvoyerCouleurPositionAttachement(bool pourPanel)
		{

			if (this.CouleurDeFond.HasValue)
				Html.Envois.Ajouter("couleurPageCourante", XHtmlColor.PrefixeCssFond + this.CouleurDeFond.ToString());

			if (this.attachementBas || this.attachementDroite || this.largeurVariable || this.hauteurVariable)
			{
				Html.Envois.Ajouter("attachementsPageCourante", Html.CalculerIdPage(this.Id),
					(this.attachementBas ? "1" : "0") + "," +
					(this.attachementDroite ? "1" : "0") + "," +
					(this.hauteurVariable ? "1" : "0") + "," +
					(this.largeurVariable ? "1" : "0") 
					);
			}
			if (this.PositionLue || this.TailleLue)
			{
				Html.Envois.Ajouter("positionPageCourante", Html.CalculerIdPage(this.Id),
					this.left.ToString() + "," +
					this.top.ToString() + "," +
					this.OriginalWidth.ToString()  + "," +
					this.OriginalHeight.ToString() + "," +
					(pourPanel ? "1" : "0")
					);

			}
		}


		internal IXHtmlObject GetObject(uint idObj)
		{
			return ListOfObjects.FirstOrDefault(obj => obj.Id == idObj);
		}
		/// <summary>
		/// Sets the page's size depending on the stretch and stick properties
		/// </summary>
		private void SetSize()
		{
//!!!!!!!!!!!!!!!!			Width = (stretchWidth) ? Math.Max(OriginalWidth, OriginalWidth + Window.MainCanvas.ActualWidth - Window.MainCanvas.MinWidth) : OriginalWidth;
//			Height = (stretchHeight) ? Math.Max(OriginalHeight, OriginalHeight + Window.MainCanvas.ActualHeight - Window.MainCanvas.MinHeight) : OriginalHeight;
		}

		/// <summary>
		/// Sets the page's position depending on the stretch and stick properties
		/// </summary>
		private void SetPosition()
		{
//!!!!!!!!!!!!!!!!			SetLeft(this, (stickRight) ? Math.Max(left, left + Window.MainCanvas.ActualWidth - Window.MainCanvas.MinWidth) : left);
//			SetTop(this, (stickBottom) ? Math.Max(top, top + Window.MainCanvas.ActualHeight - Window.MainCanvas.MinHeight) : top);
		}


		// pour implémenter l'interface
		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage p, int niveau)
		{
		}

		/// <summary>
		/// Finds the Grid in ListOfGrids whose Id matches the id given as parameter
		/// </summary>
		/// <param name="idGrid">id of the grid to look for</param>
		/// <returns>Grid matching the corresponding Id</returns>
		private Grid GetGrid(uint idGrid)
		{
			return listOfGrids.FirstOrDefault(grid => grid.Id == idGrid);
		}

		/// <summary>
		/// initialise la grille
		/// </summary>
		/// <param name="buffer"></param>
		private void SetGrid(DVBuffer buffer)
		{
			ProprietesWpf property;
			XHtmlPresentation gridPresentation = new XHtmlPresentation();
			bool newGrid = false;
			uint gridId;
			ushort cellsCount;
			byte orientation;

			buffer.Get(out gridId);					// Ident Unique de la grille (uint)
			buffer.Get(out cellsCount);			// Nb de cellules (ushort)
			buffer.Get(out orientation);			// Sens (byte : 1 = Vertical, 2 = Horizontal)

			buffer.Get(out property);				// PRESENTATION_DEBUT
			gridPresentation.ReadProperties(buffer);

			Grid grid=null; 

			//grid = GetGrid(gridId); BHGRILLE
			// BH c'est inutle si je ne garde pas les grilles coté serveur

			if (grid == null)	// les pages et leur contenu peuvent être perdues côté XrtDiva => pour ne pas recréer systématiquement ici, on teste
			{
				grid = new Grid (this)
				{
					Id = gridId,
					Tag = gridPresentation,
					Orientation = orientation
				};

				//// pour résoudre le pb de rebarres qui étaient bloquées par la taille min des cellules (idem pour les pages panel ci-dessous)
				//grid.Loaded += (s, e) =>
				//{
				//	var sv = TreeHelper.FindAncestor<ScrollViewer>(grid);
				//	if (sv.Name != "ScrollViewer") // pour exclure le scrollviewer de la fenêtre (sinon comportement non désiré)
				//	{
				//		grid.SetBinding(MaxHeightProperty, new Binding("ActualHeight") { Source = sv });
				//		grid.SetBinding(MaxWidthProperty, new Binding("ActualWidth") { Source = sv });
				//	}
				//};

				//	Grid parent = GetGrid(gridPresentation.GridId);

				//	if (parent != null)	// on ajoute dans la grille trouvée
				//	{
				//		// placement dans la cellule de la grille parent le cas échéant (en ligne ou colonne)
				//		ScrollViewer sv = null;
				//		// grille verticale
				//		if (parent.RowDefinitions.Count > 0 && gridPresentation.CellIndex < parent.RowDefinitions.Count)
				//			sv = parent.Children.OfType<ScrollViewer>().FirstOrDefault(s => GetRow(s) == gridPresentation.CellIndex);
				//		// grille horizontale
				//		else if (parent.ColumnDefinitions.Count > 0 && gridPresentation.CellIndex < parent.ColumnDefinitions.Count)
				//			sv = parent.Children.OfType<ScrollViewer>().FirstOrDefault(s => GetColumn(s) == gridPresentation.CellIndex);

				//		var children = ((Grid)sv.Content).Children;
				//		if (children.Count > 0 && children[0] is XHtmlPage) children.Clear();	// garde-fou
				//		children.Add(grid);
				//	}
				//	else
				//	{
				//		// on doit recalculer les marges pour le cas des onglets (uniquement lorsque la grille est enfant d'une page)
				//		grid.Margin = new Thickness(
				//			gridPresentation.OriginalLeft,
				//			gridPresentation.OriginalTop,
				//			Presentation.OriginalWidth - gridPresentation.OriginalLeft - gridPresentation.OriginalWidth,
				//			Presentation.OriginalHeight - gridPresentation.OriginalTop - gridPresentation.OriginalHeight
				//		);
				//		Children.Add(grid);	// sinon on ajoute dans la page en cours
				//	}
				//	listOfGrids.Add(grid);
				//	newGrid = true;
				//}

				// je saute tout le passage au dessus. je traite ca de l'autre coté.
				newGrid = true;
				listOfGrids.Add(grid);
				Children.Add(grid);

			}

			// ajout des cellules
			for (int i = 0; i < cellsCount; i++)
			{
				XHtmlPresentation cellPresentation = new XHtmlPresentation();
				byte canResizeCell;

				buffer.Get(out property);				// GRILLE_CELLULE_DEBUT
				buffer.Get(out canResizeCell);		// Redimensionnable (byte : 0 ou 1)
				buffer.Get(out property);				// PRESENTATION_DEBUT
				cellPresentation.ReadProperties(buffer);
				buffer.Get(out property);				// GRILLE_CELLULE_FIN

				if (newGrid)
					SetGridCell(gridPresentation, cellPresentation, orientation, grid, canResizeCell, i == cellsCount - 1);
			}

			grid.AjouterEnvoisUnObjet(Html.Envois, this, Html.App.StackOfWindows.Count());


			buffer.Get(out property);				// GRILLE_FIN

			#region restauration des paramètres utilisateur stockés dans le registre
			//if (newGrid && !(bool)Application.Current.Properties["IgnoreUserSettings"])
			//{
			//	RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Divalto\V7\Grilles\" + Window.MaskName + "-" + NumPage + "-" + gridId);
			//	if (key == null) return;

			//	// si l'orienation de la grille ne correspond pas à ce qui est stocké dans le registre : on r.à.z.
			//	var gridOrientation = key.GetValue("GridOrientation") as string;
			//	if (gridOrientation != null && gridOrientation != orientation.ToString(CultureInfo.InvariantCulture))
			//	{
			//		key.DeleteValue("GridOrientation");
			//		return;
			//	}

			//	// si la taille de la grille ne correspond pas à ce qui est stocké dans le registre : on r.à.z.
			//	var gridSize = key.GetValue("GridSize") as string;
			//	if (gridSize != null && gridSize != ((orientation == 1) ? gridPresentation.OriginalHeight : gridPresentation.OriginalWidth).ToString(CultureInfo.InvariantCulture))
			//	{
			//		key.DeleteValue("GridSize");
			//		return;
			//	}

			//	// récupération des données en elles-mêmes si tout est ok
			//	var gridCellSizes = key.GetValue("GridCellSizes") as string;
			//	if (gridCellSizes != null)
			//	{
			//		// si le nombre de cellules ne correspond pas à ce qui est stocké dans le registre : on r.à.z.
			//		string[] storedGridCellSizes = gridCellSizes.Split(';');
			//		if (storedGridCellSizes.Length != cellsCount)
			//		{
			//			key.DeleteValue("GridCellSizes");
			//			return;
			//		}

			//		if (orientation == 1) // grille verticale
			//		{
			//			for (int i = 0; i < storedGridCellSizes.Length; i++)
			//			{
			//				try { grid.RowDefinitions[i].Height = (GridLength)new GridLengthConverter().ConvertFromString(storedGridCellSizes[i]); }
			//				catch (FormatException)
			//				{
			//					key.DeleteValue("GridCellSizes");
			//					return;
			//				}
			//			}
			//		}
			//		else // grille horizontale
			//		{
			//			for (int i = 0; i < storedGridCellSizes.Length; i++)
			//			{
			//				try { grid.ColumnDefinitions[i].Width = (GridLength)new GridLengthConverter().ConvertFromString(storedGridCellSizes[i]); }
			//				catch (FormatException)
			//				{
			//					key.DeleteValue("GridCellSizes");
			//					return;
			//				}
			//			}
			//		}
			//	}

			//}
			#endregion restauration des paramètres utilisateur stockés dans le registre

		}



		/// <summary>
		/// initialise une cellule de la grille
		/// </summary>
		/// <param name="gridPresentation"></param>
		/// <param name="cellPresentation"></param>
		/// <param name="orientation"></param>
		/// <param name="grid"></param>
		/// <param name="canResizeCell"></param>
		private void SetGridCell(XHtmlPresentation gridPresentation, XHtmlPresentation cellPresentation, byte orientation, Grid grid, byte canResizeCell, bool lastCell)
		{
			XHtmlCelluleGrille cellule;
			cellule = new XHtmlCelluleGrille();
			cellule.AvecResize = canResizeCell;
			cellule.DerniereCellule = lastCell;
			cellule.Presentation = cellPresentation;
			grid.Cellules.Add(cellule);


			//ScrollViewer sv = new ScrollViewer
			//{
			//	Tag = cellPresentation,
			//	Content = new Grid(),
			//	VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
			//	HorizontalScrollBarVisibility = ScrollBarVisibility.Auto
			//};
			//sv.SizeChanged += (s, e) => { sv.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden; sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto; };

			//grid.Children.Add(sv);

			//if (orientation == 1)	// grille verticale
			//{
			//	var heightRatio = cellPresentation.OriginalHeight * 100 / gridPresentation.OriginalHeight;
			//	grid.RowDefinitions.Add(new RowDefinition { Height = (GridLength)new GridLengthConverter().ConvertFromString(heightRatio + "*"), MinHeight = 20 });
			//	if (canResizeCell == 1 && !lastCell)
			//	{
			//		GridSplitter splitter = new GridSplitter
			//		{
			//			Height = 3,
			//			HorizontalAlignment = HorizontalAlignment.Stretch,
			//			VerticalAlignment = VerticalAlignment.Bottom,
			//			Background = Brushes.Transparent,
			//			Focusable = false
			//		};
			//		splitter.PreviewMouseDown += (s, e) => { if (((App)Application.Current).Appli.AttenteInput) e.Handled = true; };	// on bloque les rebarres en input pour le pas perdre le focus
			//		splitter.DragCompleted += GridSplitter_DragCompleted; // pour la sauvegarde de la taille des cellules de la grille dans le registre sur le mouseUp
			//		SetRow(splitter, grid.RowDefinitions.Count - 1);
			//		SetZIndex(splitter, 5);
			//		grid.Children.Add(splitter);
			//	}
			//	SetRow(sv, grid.RowDefinitions.Count - 1);
			//}
			//else	// grille horizontale
			//{
			//	var widthRatio = cellPresentation.OriginalWidth * 100 / gridPresentation.OriginalWidth;
			//	grid.ColumnDefinitions.Add(new ColumnDefinition { Width = (GridLength)new GridLengthConverter().ConvertFromString(widthRatio + "*"), MinWidth = 20 });
			//	if (canResizeCell == 1 && !lastCell)
			//	{
			//		GridSplitter splitter = new GridSplitter
			//		{
			//			Width = 3,
			//			HorizontalAlignment = HorizontalAlignment.Right,
			//			VerticalAlignment = VerticalAlignment.Stretch,
			//			Background = Brushes.Transparent,
			//			Focusable = false
			//		};
			//		splitter.PreviewMouseDown += (s, e) => { if (((App)Application.Current).Appli.AttenteInput) e.Handled = true; };	// on bloque les rebarres en input pour le pas perdre le focus
			//		splitter.DragCompleted += GridSplitter_DragCompleted; // pour la sauvegarde de la taille des cellules de la grille dans le registre sur le mouseUp
			//		SetColumn(splitter, grid.ColumnDefinitions.Count - 1);
			//		SetZIndex(splitter, 5);
			//		grid.Children.Add(splitter);
			//	}
			//	SetColumn(sv, grid.ColumnDefinitions.Count - 1);
			//}
		}

	}


	public class XHtmlCelluleGrille
	{
		public XHtmlPresentation Presentation;
		public byte AvecResize;
		public bool DerniereCellule;
	}
	
	public class Grid
	{
		public XHtmlPresentation Tag;				// pour garder le meme nom que dd
		public uint Id;
		public List<XHtmlCelluleGrille> Cellules = new List<XHtmlCelluleGrille>();
		public XHtmlPage Page;
		public byte Orientation;     // Sens (byte : 1 = Vertical, 2 = Horizontal)

		public Grid(XHtmlPage p)
		{
			this.Page = p;
		}

		public class CellJson
		{
			public int posX, posY, tailleX, tailleY;
	//		public string fond;
			public byte avecResize;
		}

		[DataContract]
		public class GridJson
		{
			[DataMember]
			public bool vertical;
			[DataMember]
			public int nbCellules;
			[DataMember]
			public List<CellJson> cellules = new List<CellJson>();
			[DataMember]
			public bool modePourcent;
			[DataMember]
			public int tailleX;
			[DataMember]
			public int tailleY; 

		}

		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
			ListeParametresEnvoyes paramsGrille = new ListeParametresEnvoyes();

			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id));

			// presentation
			if (Tag != null)
				this.Tag.GenererHtml(paramsValeurs, this.Page.Html, 0, false);

			GridJson gj = new GridJson();
			gj.nbCellules = this.Cellules.Count();
			gj.vertical = this.Orientation == 1;
			gj.modePourcent = true;
			gj.tailleX = (int)this.Tag.OriginalWidth;
			gj.tailleY = (int)this.Tag.OriginalHeight;

			foreach (XHtmlCelluleGrille cell in this.Cellules)
			{
				CellJson gc = new CellJson();
				gj.cellules.Add(gc);
				gc.avecResize = cell.AvecResize;
//				if (cell.Presentation.idFond.HasValue)
//					gc.fond = page.Html.App.GenererUneValeurDeCouleur(cell.Presentation.idFond);
				gc.posX = (int)cell.Presentation.Left;
				gc.posY = (int)cell.Presentation.Top;
				gc.tailleX = (int)cell.Presentation.OriginalWidth;
				gc.tailleY = (int)cell.Presentation.OriginalHeight;
			}

			paramsGrille.Ajouter("paramsGrille", HtmlGlobal.ToJsonString(gj, this.Page.Html.JsonGrille, false));

			envois.Ajouter("creerGrille", HtmlGlobal.ToJsonString(paramsGrille, this.Page.Html.JsonParamsEnvoyes, false), HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));

			string p;
			if (this.Tag.GridId != 0)
			{
				p = "{" +
							"\"idGrille\":\"" + HtmlGlobal.CalculerId(this.Tag.GridId, this.Page.Id, niveau) + "\"," +
							"\"iCellule\":" + this.Tag.CellIndex +
							"}";
			}
			else
			{
				p = "{" +
							"\"idGrille\":\"" + "0" + "\"," +
							"\"iCellule\":" + 0 +
							"}";
			}
			
			envois.Ajouter("ajouterGrille", p); 
		}

	}


}

