//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlButton.xaml.cs
// Description : Objet Bouton
//___________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
/*
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
*/

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using System.Web;
using System.Linq;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlButton = Un bouton
	/// </summary>
	public partial class XHtmlButton : FrameworkElement, IXHtmlEditableObject
	{
		public uint Id { get; set; }
		public ushort SeqPoint { get; set; }
		public XHtmlPresentation Presentation { get; set; }
		public Collection<string> ListOfValidButtons { get; private set; }
		public XHtmlPage Page { get; set; }

		internal char Shortcut;
		internal string Selection;
		internal bool IsLocal, IsValid = true;

		private byte action;
		private string generatedString;
		private ushort pointTraitement, pointArret;

		private String text;
//!!!!!!!!!!		private XHtmlImage image;
//!!!!!!!!!!		private Image imageMouseOver;
//!!!!!!!!!		private Cursor cursorMouseOver;
		private Uri soundMouseOver, soundClick;
//!!!!!!!!!!		private MediaPlayer player;
		private int codePage;

		public string Content;
		public XHtmlImage image;
		public string mouseOverCursorFile;
		public string mouseOverSoundFile;
		public string clickSoundFile;
		public XHtmlImageFile imageDuBouton;


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlButton class.
		/// </summary>
		public XHtmlButton(XHtmlPage page)
		{
//!!!!!!!!!!!			InitializeComponent();
			Page = page;

			//!!!!!!!!!!!			Click += ClickHandler;
			//!!!!!!!!!!!PreviewMouseDown += PreviewMouseDownEffectsHandler;
			//!!!!!!!!!!!MouseEnter += MouseOverEffectsHandler;
			//!!!!!!!!!!!MouseLeave += MouseOverEffectsHandler;
			//!!!!!!!!!!!PreviewKeyDown += PreviewKeyDownHandler;
			//!!!!!!!!!!!LostFocus += LostFocusEventHandler;
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

			buffer.Get(out property);
			while (property != ProprietesWpf.BOUTON_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.CODE_PAGE:											// Code page
						buffer.Get(out codePage);
						break;

					case ProprietesWpf.PARAM_SAISIE_SEQUENCE:							// Point de séquence (ushort)
						ushort pointSequence;
						buffer.Get(out pointSequence);
						SeqPoint = pointSequence;
						break;

					case ProprietesWpf.OBJET_RACCOURCI:									// touche de raccourcis (byte)
						byte shortcut;
						buffer.Get(out shortcut);
						Shortcut = (char)shortcut;
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

					case ProprietesWpf.BOUTON_LIBELLE:									// Libellé du bouton (string)
						buffer.GetStringCP(out text, codePage);
						Content = text;
						break;

					case ProprietesWpf.BOUTON_IMAGE_DEBUT:								// Image du bouton
						imageDuBouton = new XHtmlImageFile();
						imageDuBouton.ReadProperties(buffer);

						if (this.Page.Html.App.ImagesCss.FirstOrDefault(e => e.FileName == imageDuBouton.FileName) == null)
						{
							imageDuBouton.Css = imageDuBouton.GenererCss();
							this.Page.Html.App.ImagesCss.Add(imageDuBouton);
						}

						//Content = image = new XHtmlImage(Page)
						//{
						//   Source = XHtmlImage.GetImage(imageFile),
						//   HorizontalAlignment = HorizontalAlignment.Center,
						//   VerticalAlignment = VerticalAlignment.Center,
						//   Stretch = Stretch.Uniform,
						//   StretchDirection = StretchDirection.DownOnly
						//};
						break;

					case ProprietesWpf.BOUTON_NOM_SELECTION:							// Nom de la sélection (string)
						buffer.GetString(out Selection);
						break;

					case ProprietesWpf.BOUTON_LOCAL:										// Uniquement si le bouton est local à la page
						IsLocal = true;
						break;

					case ProprietesWpf.BOUTON_ACTION:									// Type d'action (byte = 1, 2 ou 3)
						buffer.Get(out action);
						switch (action)
						{
							case 1: buffer.Get(out pointTraitement); break;				// Type = 1 : point de traitement
							case 2: buffer.Get(out pointArret); break;					// Type = 2 : point d'arrêt
							case 3: buffer.GetString(out generatedString); break;		// Type = 3 : chaîne à générer
						}
						break;

					case ProprietesWpf.BOUTON_IMAGES_ET_TITRE:
						var backgroundImageFile = new XHtmlImageFile();							// Image fond
						backgroundImageFile.ReadProperties(buffer);
						//BackgroundImage = XHtmlImage.GetImage(backgroundImageFile);

						var backgroundClickedImageFile = new XHtmlImageFile();				// Image fond au clic
						backgroundClickedImageFile.ReadProperties(buffer);
						//BackgroundClickedImage = XHtmlImage.GetImage(backgroundClickedImageFile);

						buffer.GetString(out text);																// Libellé (string)
						Content = text;

						ushort leftMargin, topMargin, rightMargin, bottomMargin;							// Marges (ushort x 4)
						buffer.Get(out leftMargin);
						buffer.Get(out topMargin);
						buffer.Get(out rightMargin);
						buffer.Get(out bottomMargin);
//!!!!!!!!!!!!						Padding = new Thickness(leftMargin, topMargin, rightMargin, bottomMargin);
						break;

					#region image survol 
					case ProprietesWpf.BOUTON_IMAGE_SURVOL_DEBUT:					// Image au survol du bouton (string)
						var mouseOverImageFile = new XHtmlImageFile();
						mouseOverImageFile.ReadProperties(buffer);

						//imageMouseOver = (string.IsNullOrEmpty(mouseOverImageFile.FileName)) ? null : new Image
						//{
						//   Source = XHtmlImage.GetImage(mouseOverImageFile),
						//   HorizontalAlignment = HorizontalAlignment.Center,
						//   VerticalAlignment = VerticalAlignment.Center,
						//   Stretch = Stretch.Uniform,
						//   StretchDirection = StretchDirection.DownOnly
						//};
						break;
					#endregion image survol

					#region curseur survol
					case ProprietesWpf.BOUTON_CURSEUR_SURVOL:							// Curseur au survol (string)
						buffer.GetString(out mouseOverCursorFile);
						//cursorMouseOver = GetCursor(mouseOverCursorFile);
						break;
					#endregion curseur survol

					#region son survol
					case ProprietesWpf.BOUTON_SON_SURVOL:								// Son au survol (string)
						buffer.GetString(out mouseOverSoundFile);

						//player = new MediaPlayer();
						//soundMouseOver = GetSound(mouseOverSoundFile);
						break;
					#endregion son survol

					#region son au clic
					case ProprietesWpf.BOUTON_SON_CLIC:									// Son au clic (string)
						buffer.GetString(out clickSoundFile);

						//player = new MediaPlayer();
						//soundClick = GetSound(clickSoundFile);
						break;
					#endregion son au clic

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Button, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions
		/// <summary>
		/// Gets a cursor from a SoundFile
		/// </summary>
		/// <param name="cursorFile">Name of the cursorFile</param>
		/// <returns>cursor</returns>
		//internal static Cursor GetCursor(string cursorFile)		//!!!!!!!!!!!!!!!!!!!!!!!!!
		//{
		//   var cacheFichiers = ((App)Application.Current).Client.CacheFichiers;
		//   string path;
		//   return cacheFichiers.TransfererUnFichier(cursorFile.TrimEnd(), false, true, true, out path, false)
		//      ? new Cursor(path)
		//      : null;
		//}

		/// <summary>
		/// Gets the URI of a given SoundFile
		/// </summary>
		/// <param name="soundFile">Name of the soundFile</param>
		/// <returns>URI of the soundFile</returns>
		//internal static Uri GetSound(string soundFile) !!!!!!!!!!!!!!!!!!!!
		//{
		//   var cacheFichiers = ((App)Application.Current).Client.CacheFichiers;
		//   string path;
		//   return cacheFichiers.TransfererUnFichier(soundFile.TrimEnd(), false, true, true, out path, false)
		//      ? new Uri(path, UriKind.Relative)
		//      : null;
		//}

		/// <summary>
		/// implémentation de IDisposable pour disposer du curseur (type non-managé)
		/// </summary>
		//protected virtual void Dispose(bool disposing) //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		//{
		//   if (!disposing || cursorMouseOver == null) return;

		//   // dispose managed resources
		//   cursorMouseOver.Dispose();
		//   cursorMouseOver = null;
		//}

		/// <summary>
		/// implémentation de IDisposable pour disposer du curseur (type non-managé)
		/// </summary>
		//public void Dispose()
		//{
		//   Dispose(true);
		//   GC.SuppressFinalize(this);
		//}
		#endregion Fonctions


		//#region Ecouteurs
		///// <summary>
		///// Gestionnaire d'évènement "PreviewKeyDown" pour la fonction XMEInput
		///// </summary>
		//private static void PreviewKeyDownHandler(object sender, KeyEventArgs e)
		//{
		//   var application = ((App)Application.Current).Appli;

		//   if (!application.AttenteInput)
		//   {
		//      e.Handled = true;
		//      return;
		//   }

		//   string key = "";

		//   // Touches de fonction (Fn)
		//   if (e.Key == Key.System && e.SystemKey == Key.F10) key = "F10";
		//   else if (XHtmlApplication.FnKeys.Contains(e.Key)) key = e.Key.ToString();

		//   // Autres touches
		//   else // pas la peine de faire les tests spécifiques si une touche générique (Fn) a déjà été détetée
		//   {
		//      if (ValidKeys.Contains(e.Key) || ((Keyboard.Modifiers & ModifierKeys.Control) != 0 && ValidModifiedKeys.Contains(e.Key)))
		//         key = e.Key.ToString();
		//      else if (e.Key != Key.Left && e.Key != Key.Right) return; // les touches restantes ne sont pas bloquées, excepté flèches droite et gauche
		//   }

		//   e.Handled = true;
		//   application.SendInputKeyDown(key);
		//}

		///// <summary>
		///// Gestionnaire d'évènement "Click" du bouton
		///// </summary>
		//private void ClickHandler(object sender, RoutedEventArgs e)
		//{
		//   e.Handled = true;

		//   var application = ((App)Application.Current).Appli;
		//   var client = ((App)Application.Current).Client;

		//   if (application.AttenteInput || application.AttenteConsult || (application.AttenteGetb && action == 3))	// input ou consult ou get bloquant (traité AVANT le GetBegin car on peut être en GetBegin et faire un input/consult/get bloquant)
		//   {
		//      var response = new DVBuffer();
		//      response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse

		//      application.SetInputBuffer(response);

		//      response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);		// Début de l'envoi des évenements souris

		//      response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);			// Type d'évènement souris (byte)
		//      response.Put((byte)MouseEvent.ClickButton);

		//      response.Put(ProprietesWpf.BOUTON_ACTION);					// Type d'action (byte = 1, 2 ou 3)
		//      response.Put(action);
		//      switch (action)
		//      {
		//         case 1: response.Put(pointTraitement); break;				// Type = 1 : point de traitement
		//         case 2: response.Put(pointArret); break;						// Type = 2 : point d'arrêt
		//         case 3: response.PutString(generatedString); break;		// Type = 3 : chaîne à générer
		//      }

		//      response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);			// Fin de l'envoi des évenements souris

		//      application.Send(response);
		//      return;
		//   }

		//   if (client != null && client.GetBegin > 0 && action == 3)	// get non bloquant sur bouton "chaîne" (les autres types sont ignorés)
		//      client.Transport.EnvoyerUnGet("##" + OutilsString.ToHexa(generatedString));
		//}

		///// <summary>
		///// Gestionnaire d'effets du clic bouton (pour sons, curseurs et images)
		///// </summary>
		//private void PreviewMouseDownEffectsHandler(object sender, RoutedEventArgs e)
		//{
		//   if (soundClick == null || !IsEnabled) return;

		//   player.Open(soundClick);
		//   player.Play();
		//}

		///// <summary>
		///// Gestionnaire d'effets du mouseOver bouton (pour sons, curseurs et images)
		///// </summary>
		//private void MouseOverEffectsHandler(object sender, RoutedEventArgs e)
		//{
		//   if (IsMouseOver && IsEnabled)
		//   {
		//      if (imageMouseOver != null) Content = imageMouseOver;
		//      if (cursorMouseOver != null) Mouse.OverrideCursor = cursorMouseOver;
		//      if (soundMouseOver != null)
		//      {
		//         player.Open(soundMouseOver);
		//         player.Play();
		//      }
		//   }
		//   else if (!IsMouseOver)
		//   {
		//      if (text != null) Content = text;
		//      else if (image != null) Content = image;
		//      Mouse.OverrideCursor = null;
		//   }
		//}

		///// <summary>
		///// 
		///// </summary>
		//private void LostFocusEventHandler(object sender, RoutedEventArgs e)
		//{
		//   Focusable = false; // garde-fou. Le Focusable est repassé à true lors du passage en input (pour éviter qu'un bouton ne prenne le focus sans qu'on ne l'y autorise)
		//}
		//#endregion Ecouteurs

		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();

			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs,this.Page.Html,this.codePage,false);

			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id)); //   this.Id.ToString());

			if (string.IsNullOrEmpty(this.text) == false)
				paramsValeurs.Ajouter("textBouton", this.text);
			//else
			//	paramsValeurs.Ajouter("textBouton", "pas de texte");


			if (imageDuBouton != null && string.IsNullOrEmpty(imageDuBouton.FileName) == false)
			{
				paramsValeurs.Ajouter("imagebouton", imageDuBouton.FileName.Replace('.','_'));
			}

			if (this.IsLocal)
				paramsValeurs.Ajouter("boutonLocal", "true");

			if (string.IsNullOrEmpty(this.Selection) == false)
				paramsValeurs.Ajouter("boutonNomSelection", Selection);


			UnParametreComplementaire compl;
			ListeParametresComplementaires compls = new ListeParametresComplementaires();
			compls.Ajouter("action:",action.ToString());
			compl = compls.Ajouter("param", "0");
			switch (action)
			{
				case 1: compl.v = pointTraitement.ToString(); break;				// Type = 1 : point de traitement
				case 2: compl.v = pointArret.ToString(); break;					// Type = 2 : point d'arrêt
				case 3: compl.v = generatedString; break;							// Type = 3 : chaîne à générer
			}

			string chaineAction = HtmlGlobal.ToJsonString(compls, this.Page.Html.JsonParamCompl,false);

			paramsValeurs.Ajouter("actionBouton", chaineAction.Replace("'", @"#quote"));

			envois.Ajouter("creerBouton", "<button></button>", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));
			envois.Ajouter("ajoutObjetCourant", "");



//			chaineAction = "data-harmony=" + "'" + chaineAction.Replace("'",@"#quote") + "'";
			//string id = "id=" + this.Id.ToString();
			//UnParametreEnvoye p = new UnParametreEnvoye();
			//envois.commandes.Add(p);
			//p.commande = "bouton";
			//p.valeur = "<button " + id + " " + css + " " + chaineAction +">" + this.text + "</button>";
			
		}

	}
}
