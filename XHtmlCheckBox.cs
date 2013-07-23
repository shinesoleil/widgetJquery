//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlCheckBox.xaml.cs
// Description : Objet Case à cocher
//___________________________________________________________________________

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Text;

namespace Divaltohtml
{

	public class CaseACocherVersJson
	{
		public bool texteAGauche;			// c'est les memes noms que dans le widget !!
		public bool notification;
	}

	
	/// <summary>
	/// Classe XHtmlCheckBox = Une case à cocher
	/// </summary>
	public partial class XHtmlCheckBox : FrameworkElement, IXHtmlEditableObject
	{
		#region Liste des touches valides en input
		internal static List<Key> ValidKeys = new List<Key>						// Touches seules
		{
			Key.Return,
			Key.Enter,
			Key.Escape,
			Key.Back,
			Key.Tab,
			//Key.Space,		// ne pas traiter : wpf gère le cochage/décochage

			Key.Insert,

			Key.Next,
			Key.Prior,
			Key.PageDown,
			Key.PageUp,
			Key.Down,
			Key.Up
		};

		internal static List<Key> ValidModifiedKeys = new List<Key>				// Touches combinées avec CTRL
		{
			Key.A,
			Key.B,
			//Key.C,		// jamais traité par xrtdiva
			Key.D,
			Key.E,
			Key.F,
			//Key.G,		// jamais traité par xrtdiva
			//Key.H,		// jamais traité par xrtdiva
			//Key.I,		// jamais traité par xrtdiva
			//Key.J,		// jamais traité par xrtdiva
			//Key.K,		// jamais traité par xrtdiva
			//Key.L,		// jamais traité par xrtdiva
			//Key.M,		// jamais traité par xrtdiva
			Key.N,
			Key.O,
			Key.P,
			Key.Q,
			Key.R,
			Key.S,
			Key.T,
			Key.U,
			//Key.V,		// jamais traité par xrtdiva
			Key.W,
			//Key.X,		// jamais traité par xrtdiva
			Key.Y,
			//Key.Z		// jamais traité par xrtdiva
		};
		#endregion Liste des touches valides en input

		public uint Id { get; set; }
		public ushort SeqPoint { get; set; }
		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }
		public Collection<string> ListOfValidButtons { get; private set; }
		public XHtmlPage Page { get; set; }

		public static readonly DependencyProperty IsReadOnlyProperty = TextBoxBase.IsReadOnlyProperty.AddOwner(typeof(CheckBox));
		public bool? IsReadOnly;

		//{
		//   get { return (bool)GetValue(IsReadOnlyProperty); }
		//   set { SetValue(IsReadOnlyProperty, value); }
		//}

		private int codePage;
		public bool? TexteAGauche;
		public bool NotificationSiModif;
		public string Libelle;
		public bool? IsChecked;

		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlCheckBox class.
		/// </summary>
		public XHtmlCheckBox(XHtmlPage page)
		{
//			InitializeComponent();
			Page = page;

//			PreviewKeyDown += PreviewKeyDownHandler;
//			PreviewMouseDown += PreviewMouseDownHandler;
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
			while (property != ProprietesWpf.CASE_A_COCHER_FIN)
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
						ushort seqPoint;
						buffer.Get(out seqPoint);
						SeqPoint = seqPoint;
						break;

					case ProprietesWpf.CASE_A_COCHER_CADRAGE_TEXTE:					// Position du texte par rapport à la case (byte)
						byte cadrage;
						buffer.Get(out cadrage);
						TexteAGauche = cadrage == 1;
						break;

					case ProprietesWpf.OBJET_NOTIFIER_SI_MODIF:						// Envoyé si la notification est demandée ==> il faudra "réveiller" Ymeg si l'utilisateur clique sur la case
						//Checked += ValueChangedHandler;		// traitement de la notification
						//Unchecked += ValueChangedHandler;	// traitement de la notification
						NotificationSiModif = true;
						break;

					case ProprietesWpf.CASE_A_COCHER_LIBELLE:							// Libellé de la case (string)
						string content;
						buffer.GetStringCP(out content, codePage);
						Libelle = content;
						break;

					case ProprietesWpf.OBJET_EN_AFFICHAGE:								// (Uniquement si la case est en affichage seulement)
						IsReadOnly = true;
						break;

					case ProprietesWpf.CASE_A_COCHER_ETAT:								// Etat de la case (byte : 0=non cochée ; 1=cochée)
						ushort etat;
						buffer.Get(out etat);
						IsChecked = (etat == 1);
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
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.CheckBox, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		//#region Ecouteurs
		/// <summary>
		/// Gestionnaire d'évènement "PreviewKeyDown" pour la fonction XMEInput
		/// </summary>
		//private static void PreviewKeyDownHandler(object sender, KeyEventArgs e)
		//{
		//   var application = ((App)Application.Current).Appli;

		//   if (!application.AttenteInput) // cas impossible en théorie: un contrôle ne peut pas avoir le focus sans que l'on soit en Input
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
		//      if (ValidKeys.Contains(e.Key) || (((Keyboard.Modifiers & ModifierKeys.Control) != 0) && ValidModifiedKeys.Contains(e.Key)))
		//         key = e.Key.ToString();
		//      else if (e.Key != Key.Left && e.Key != Key.Right) return; // les touches restantes ne sont pas bloquées, excepté flèches droite et gauche
		//   }

		//   e.Handled = true;
		//   application.SendInputKeyDown(key);
		//}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown"
		/// </summary>
		//private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		//{
		//   if (IsReadOnly) // refusé si IsReadOnly
		//   {
		//      e.Handled = true;
		//      return;
		//   }

		//   // on laisse passer les clics gauches (simples & doubles) en input (géré par wpf)
		//   if (Page.Window.ActiveControl == this && e.ChangedButton == MouseButton.Left) return;

		//   ((App)Application.Current).Appli.SendMouseDown(this, e);
		//}

		/// <summary>
		/// Gestionnaire d'évènement "Checked ou Unchecked" pour la notification
		/// </summary>
		//private void ValueChangedHandler(object sender, RoutedEventArgs e)
		//{
		//   var application = ((App)Application.Current).Appli;

		//   // on ne notifie pas si la case n'est pas le contrôle actif en input
		//   if (!(application.AttenteInput && Page.Window.ActiveControl == this)) return;

		//   var response = new DVBuffer();
		//   response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);			//début de l'acquittement ou de la réponse

		//   response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris

		//   response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
		//   response.Put((byte)MouseEvent.Notification);
		//   response.Put(ProprietesWpf.PAGE_NUMERO);					// Numéro de la page contenant la donnée cliquée (byte)
		//   response.Put(Page.NumPage);
		//   response.Put(ProprietesWpf.IDENT_UNIQUE);					// Id de la donnée cliquée (uint)
		//   response.Put(Id);
		//   response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);		// Numéro du point de séquence de la donnée cliquée (ushort)
		//   response.Put(SeqPoint);

		//   response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris

		//   response.Put(ProprietesWpf.CASE_A_COCHER_ETAT);			// Nouvelle valeur (ushort)
		//   response.Put((IsChecked.HasValue && IsChecked.Value) ? (ushort)1 : (ushort)0);

		//   application.Send(response);
		//}
		//#endregion Ecouteurs

		/// ajouter les commandes necessaires pour le client
		/// </summary>
		/// <param name="envois"></param>
		/// <param name="page"></param>
		/// <param name="niveau"></param>
		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
			ListeParametresEnvoyes paramsCreation = new ListeParametresEnvoyes();

			CaseACocherVersJson cc = new CaseACocherVersJson();


			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id));


			if (this.IsChecked.HasValue)
				paramsValeurs.Ajouter("caseChecked", (bool)this.IsChecked ? "true" : "false");

			if (this.TexteAGauche.HasValue)
				cc.texteAGauche = (bool)this.TexteAGauche;

			if (string.IsNullOrEmpty(this.Libelle) == false)
				paramsValeurs.Ajouter("caseLibelle", this.Libelle);

			if (NotificationSiModif)
				cc.notification = true;

			if (IsReadOnly.HasValue)
				paramsValeurs.Ajouter("lectureSeule", (bool)IsReadOnly ? "true" : "false");

			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs, this.Page.Html,0,false);

			paramsValeurs.Ajouter("pointSequence", this.SeqPoint.ToString(), page.NumPage.ToString());


			string parametresJson = HtmlGlobal.ToJsonString(cc, this.Page.Html.JsonParametresCaseACocher, false);
			paramsCreation.Ajouter("paramsCreation", parametresJson);


			//UnParametreEnvoye p = new UnParametreEnvoye();

			envois.Ajouter("creerCaseACocher", HtmlGlobal.ToJsonString(paramsCreation, this.Page.Html.JsonParamsEnvoyes, false), HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
//			envois.Ajouter("ajoutObjetCourant", "");
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));

		}
	}
}
