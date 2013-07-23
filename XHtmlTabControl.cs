//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlTabControl.xaml.cs
// Description : Objets Groupe d'onglets
//___________________________________________________________________________

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Divalto.Systeme;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlTabControl = Un groupe d'onglets
	/// </summary>
	public partial class XHtmlTabControl : FrameworkElement, IXHtmlObject
	{
		public uint Id { get; set; }
//		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }
		public XHtmlPage Page { get; set; }

		public bool IsMultiLine;
		public int? ImageWidth;
		public int? ImageHeight;
		private ushort stopPoint;
		public ushort? idPolice;
		//double? VerticalPadding;
		//double? HorizontalPadding;
		ushort? idPadding;
		byte currentTabPageNum;
		
		public List<XHtmlTabItem>Items = new List<XHtmlTabItem>();



		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlTabControl class.
		/// </summary>
		public XHtmlTabControl(XHtmlPage page)
		{
			//!!!!!!!!!!!!!!! mettre un zindex pour qu'on soit derriere
			Page = page;
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
			ushort id;

			buffer.Get(out property);
			while (property != ProprietesWpf.GROUPE_ONGLET_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.ONGLET_POLICE:									// Police des headers d'onglets
						ushort idFont;
						buffer.Get(out idFont);
						idPolice = idFont;
//						XHtmlFont font = Application.Current.Resources["Font-" + idFont] as XHtmlFont;

						//if (font == null)
						//{
						//	Debug.WriteLine("police onglet non-chargée (ressource à null)");
						//	break;
						//}
						//if (font.Family != null) FontFamily = font.Family;
						//if (font.Size != null) FontSize = font.Size.Value;
						//if (font.Style != null) FontStyle = font.Style.Value;
						//if (font.Weight != null) FontWeight = font.Weight.Value;
						//if (font.Brush != null) Foreground = font.Brush;
						break;

					case ProprietesWpf.ONGLET_PADDING:									// padding dans les headers d'onglets
						buffer.Get(out id);
						idPadding = id;

						//double? verticalPadding = Application.Current.Resources["VerticalPadding-" + idPadding] as double?;
						//double? horizontalPadding = Application.Current.Resources["HorizontalPadding-" + idPadding] as double?;

						//Thickness padding = Padding;
						//if (verticalPadding.HasValue) padding.Top = padding.Bottom = verticalPadding.Value;
						//if (horizontalPadding.HasValue) padding.Left = padding.Right = horizontalPadding.Value;
						//Padding = padding;
						break;

					case ProprietesWpf.ONGLET_TAILLE_IMAGE:							// Image du bouton (ushort x ushort)
						ushort imageHeight, imageWidth;
						buffer.Get(out imageHeight);
						buffer.Get(out imageWidth);
						ImageHeight = imageHeight;
						ImageWidth = imageWidth;
						break;

					case ProprietesWpf.ONGLET_MULTILIGNE:								// (Uniquement si multiligne)
						IsMultiLine = true;
						break;

					case ProprietesWpf.ONGLET_COURANT:									// Onglet courant (byte)
						buffer.Get(out currentTabPageNum);
						//XHtmlTabItem currentTab = GetTabByPageNum(currentTabPageNum);
						//if (currentTab != null)
						//{
						//	currentTab.IsSelected = true;
						//	//UpdateLayout(); // nécessaire pour que le BringIntoView fonctionne au tout premier affichage
						//	//currentTab.BringIntoView();
						//}
						break;

					case ProprietesWpf.ONGLET_ARRET:										// Numéro du point d'arrêt (ushort)
						buffer.Get(out stopPoint);
						break;

					case ProprietesWpf.ONGLET_DEBUT:
						uint idTab;
						buffer.Get(out idTab);
						XHtmlTabItem tab = GetTabById(idTab);
						if (tab == null)
						{
							tab = new XHtmlTabItem(this) { Id = idTab };
							tab.Page = this.Page;
							Items.Add(tab);
						}
						tab.ReadProperties(buffer);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.TabControl, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions
		/// <summary>
		/// Récupération de l'onglet actif
		/// </summary>
		private XHtmlTabItem GetTabByPageNum(uint pageNum)
		{
			return Items.OfType<XHtmlTabItem>().FirstOrDefault(tab => tab.PageNumber == pageNum);
		}

		/// <summary>
		/// Recherche d'un objet dans la liste d'objets de la page
		/// </summary>
		private XHtmlTabItem GetTabById(uint id)
		{
			return Items.OfType<XHtmlTabItem>().FirstOrDefault(tab => tab.Id == id);
		}

		//internal void SendCtrlN(int n)
		//{
		//	XHtmlTabItem tabItem;

		//	if (n > Items.Count && n != 9) return; // garde-fou : si num > nb d'onglets dans le groupe, on ne fait rien

		//	if (n == 9) // CTRL + 9 active le dernier tabItem, quel qu'il soit.
		//	{
		//		n = Items.Count;
		//		foreach (var t in Items)	// mais décompter les onglets cachés
		//		{
		//			tabItem = (XHtmlTabItem)t;
		//			if (tabItem.Visibility != Visibility.Visible)
		//				n--;
		//		}
		//	}
		//	if (n == 0) return;							// garde-fou : si tous les onglets sont cachés

		//	for (int i = 0; i < n; i++)				// il faut ignorer les onglets cachés
		//	{
		//		tabItem = (XHtmlTabItem)Items[i];
		//		if (tabItem.Visibility != Visibility.Visible)
		//			if (++n > Items.Count) return;
		//	}

		//	tabItem = (XHtmlTabItem)Items[n - 1];
		//	if (tabItem.IsEnabled == false) return;	// si l'onglet demandé est grisé

		//	if (tabItem == SelectedItem) return; // on ne notifie pas le serveur si N correspond à l'onglet courrant

		//	var application = ((App)Application.Current).Appli;

		//	var response = new DVBuffer();
		//	response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);			//début de l'acquittement ou de la réponse

		//	application.SetInputBuffer(response);

		//	response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris

		//	response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
		//	response.Put((byte)MouseEvent.ClickTab);
		//	response.Put(ProprietesWpf.SOURIS_CLIC);					// 1 = simple clic; 2 = double clic (byte)
		//	response.Put((byte)1);
		//	response.Put(ProprietesWpf.SOURIS_BOUTON);				// 1 = gauche; 2 = milieu; 3 = droite (byte)
		//	response.Put((byte)1);
		//	response.Put(ProprietesWpf.SOURIS_TOUCHE);				// touche(s) du clavier enfoncée(s) simultanément (byte)
		//	response.Put((byte)0);

		//	// Informations sur le changement d'onglet :
		//	response.Put(ProprietesWpf.ONGLET_ARRET);					// Numéro du point d'arrêt (ushort)
		//	response.Put(stopPoint);
		//	response.Put(ProprietesWpf.ONGLET_NOUVELLE_PAGE);		// numéro de page de l'onglet à afficher (byte)
		//	response.Put(tabItem.PageNumber);
		//	response.Put(ProprietesWpf.ONGLET_ANCIENNE_PAGE);		// numéro de page de l'onglet précédemment affiché (byte)
		//	response.Put(((XHtmlTabItem)SelectedItem).PageNumber);

		//	response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris

		//	application.Send(response);
		//}
		#endregion Fonctions


		#region Ecouteurs
		/// <summary>
		/// Gestionnaire d'évènement "MouseDown" pour la capture de souris sur l'onglet
		/// </summary>
		//internal void TabMouseDownHandler(object sender, MouseButtonEventArgs e)
		//{
		//	e.Handled = true;

		//	if (e.ClickCount > 1 || e.ChangedButton != MouseButton.Left) return; // pas de clic autre que simple clic gauche sur un onglet

		//	XHtmlTabItem tabItem = (XHtmlTabItem)sender; // l'écouteur est sur le tabitem

		//	if (tabItem == SelectedItem) return; // on ne fait rien si le clic est sur l'onglet courant

		//	var application = ((App)Application.Current).Appli;

		//	if (!application.AttenteConsult && !application.AttenteInput) return; // garde-fou

		//	var response = new DVBuffer();
		//	response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);			//début de l'acquittement ou de la réponse

		//	application.SetInputBuffer(response);

		//	response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris

		//	response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
		//	response.Put((byte)MouseEvent.ClickTab);
		//	response.Put(ProprietesWpf.SOURIS_CLIC);					// 1 = simple clic; 2 = double clic (byte)
		//	response.Put((byte)1);
		//	response.Put(ProprietesWpf.SOURIS_BOUTON);				// 1 = gauche; 2 = milieu; 3 = droite (byte)
		//	response.Put((byte)1);
		//	response.Put(ProprietesWpf.SOURIS_TOUCHE);				// touche(s) du clavier enfoncée(s) simultanément (byte)
		//	response.Put((byte)0);

		//	// Informations sur le changement d'onglet :
		//	response.Put(ProprietesWpf.ONGLET_ARRET);					// Numéro du point d'arrêt (ushort)
		//	response.Put(stopPoint);
		//	response.Put(ProprietesWpf.ONGLET_NOUVELLE_PAGE);		// numéro de page de l'onglet à afficher (byte)
		//	response.Put(tabItem.PageNumber);
		//	response.Put(ProprietesWpf.ONGLET_ANCIENNE_PAGE);		// numéro de page de l'onglet précédemment affiché (byte)
		//	response.Put(((XHtmlTabItem)SelectedItem).PageNumber);

		//	response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris

		//	application.Send(response);
		//}
		#endregion Ecouteurs

		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
			ListeParametresEnvoyes paramsCreation = new ListeParametresEnvoyes();

			OngletVersJson cc = new OngletVersJson();


			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id));
			 
			if (this.idPolice.HasValue)
				cc.police = XHtmlFont.PrefixeCss + idPolice.ToString();

			if (this.idPadding.HasValue)
				cc.padding = XHtmlPadding.PrefixeCss + idPadding.ToString();

			if (this.ImageHeight.HasValue)
				cc.hauteurImage = this.ImageHeight;

			if (this.ImageWidth.HasValue)
				cc.largeurImage = this.ImageWidth;

			cc.multiLigne = this.IsMultiLine;

			cc.pointArret = this.stopPoint;
			cc.pageCourante = this.currentTabPageNum;

			
			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs, this.Page.Html, 0, false);


			foreach (XHtmlTabItem item in this.Items)
			{
				cc.items.Add(item.AjouterEnvoisUnItem(envois,page,niveau));
			}

			string parametresJson = HtmlGlobal.ToJsonString(cc, this.Page.Html.JsonParametresOnglet, false);
			paramsCreation.Ajouter("paramsCreation", parametresJson);

			//UnParametreEnvoye p = new UnParametreEnvoye();

			envois.Ajouter("creerOnglet", HtmlGlobal.ToJsonString(paramsCreation, this.Page.Html.JsonParamsEnvoyes, false), HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));
		}
	}

	// attention, ce sont les memes coté JavaScript
	public class ItemOngletVersJson
	{
		public string texte;
		public string bulle;
		public string bitmap;
		public string fond;
		public byte visibilite;			// 0 = visible, 1 = grisé, 2 = illisible, 3 = caché
		public ushort numeroPage;
	}

	[DataContract]
	class OngletVersJson
	{
		[DataMember]
		public List<ItemOngletVersJson> items = new List<ItemOngletVersJson>();
		[DataMember]
		public string police;
		[DataMember]
		public string padding;
		[DataMember]
		public int? hauteurImage;
		[DataMember]
		public int? largeurImage;
		[DataMember]
		public bool multiLigne;
		[DataMember]
		public ushort pointArret;
		[DataMember]
		public ushort pageCourante;
	}

	public class ClicOnglet
	{
		public string idOnglet;
		public ushort pageOngletClick;
		public ushort anciennePage;
		public ushort arretOnglet;
	};



}