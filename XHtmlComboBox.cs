//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlComboBox.xaml.cs
// Description : Objet Multichoix
//___________________________________________________________________________

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	public class UnItemMultiChoix
	{
		public string Libelle;
	}

	public class UnItemJson
	{
		public string id;
		public string text;
	}

	public class DatasComboVersJson
	{
		public List<UnItemJson> results = new List<UnItemJson>();
		public string text = "text";


	}

	public class ComboBoxVersJson
	{
		// champs utilisés par le constructeur de select2
		public DatasComboVersJson data = new DatasComboVersJson();
		public string formatSelection;// = "function(item) { return item.text; }";
		public string formatResult;// = "function(item) { return item.text; }";
		public string escapeMarkup = "_escapeMarkup";
		//		public string initSelection = "function (element, callback) {}";

		// a moi
		public bool autoOpen = false;
		public string position = "-1";
	
	}


	/// <summary>
	/// Classe XHtmlComboBox = Un multichoix
	/// </summary>
	/// 
	public partial class XHtmlComboBox : FrameworkElement, IXHtmlEditableObject
	{
		#region Liste des touches valides en input
		internal static List<Key> ValidModifiedKeys = new List<Key>				// touches combinées à CTRL
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

		public bool Notification = false;
		public bool? IsReadOnly;
		public int SelectedIndex;
		public HorizontalAlignment HorizontalContentAlignment = HorizontalAlignment.Left;
		public int? PositionRecue;

		public static readonly DependencyProperty IsZoomCallerProperty = DependencyProperty.Register("IsZoomCaller", typeof(bool), typeof(XHtmlComboBox));
		public bool IsZoomCaller;
		//{
		//   get { return (bool)GetValue(IsZoomCallerProperty); }
		//   set { SetValue(IsZoomCallerProperty, value); }
		//}
		public List<UnItemMultiChoix> Items = new List<UnItemMultiChoix>();


		private bool
			autoOpen,
			imageComboBox,
			ignoreValueChanged; // pour ne pas notifier lors du réaffichage de la liste


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlComboBox class.
		/// </summary>
		public XHtmlComboBox(XHtmlPage page)
		{
//			InitializeComponent();
			Page = page;

	//		PreviewKeyDown += PreviewKeyDownHandler;
	//		PreviewMouseDown += PreviewMouseDownHandler;
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
			while (property != ProprietesWpf.MULTICHOIX_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								//début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.PARAM_SAISIE_SEQUENCE:							// Point de séquence (ushort)
						ushort pointSequence;
						buffer.Get(out pointSequence);
						SeqPoint = pointSequence;
						break;

					case ProprietesWpf.PARAM_SAISIE_TABLE_ASSOCIEE:					// (Uniquement si le multichoix peut appeler un zoom)
						IsZoomCaller = true;
						break;

					case ProprietesWpf.PARAM_SAISIE_TABLE_ASSOCIEE_EXT:			// (Uniquement si le multichoix peut appeler un zoom)
						byte isZoomCaller;
						buffer.Get(out isZoomCaller);
						IsZoomCaller = (isZoomCaller != 0);
						break;

					case ProprietesWpf.OBJET_NOTIFIER_SI_MODIF:						// Envoyé si la notification est demandée ==> il faudra "réveiller" Ymeg si l'utilisateur change de choix actif
						this.Notification = true;
						break;

					case ProprietesWpf.OBJET_EN_AFFICHAGE:								// (Uniquement si le champ est en affichage seulement)
						IsReadOnly = true;
						break;

					case ProprietesWpf.MULTICHOIX_BITMAP:								// Uniquement si c'est un multichoix bitmap
						imageComboBox = true;
						HorizontalContentAlignment = HorizontalAlignment.Center;
						break;

					case ProprietesWpf.MULTICHOIX_LIBELLES:							// Liste des libellés
						ignoreValueChanged = true; // pour ne pas notifier lors du réaffichage de la liste en Input
						int codepageChoix;
						buffer.Get(out codepageChoix);
						ushort nbItems;
						buffer.Get(out nbItems);
						int valueSaved = SelectedIndex;

						//construction de la liste d'items :
						Items.Clear(); // Garde-fou

						for (int i = 0; i < nbItems; i++)
						{
							var item = new UnItemMultiChoix();
							if (imageComboBox)
							{
								buffer.Get(out property); // MULTICHOIX_IMAGE
								var imageFile = new XHtmlImageFile();
								imageFile.ReadProperties(buffer);
								item.Libelle = imageFile.FileName;
							//!!!!!!!!!!!	item.Content = new Image { Source = XHtmlImage.GetImage(imageFile), Stretch = Stretch.None };
							}
							else
							{
								string itemContent;
								buffer.GetStringCP(out itemContent, codepageChoix);
								item.Libelle = itemContent;
							}
							Items.Add(item);
						}
						SelectedIndex = valueSaved;
						ignoreValueChanged = false;
						break;

					case ProprietesWpf.MULTICHOIX_VALEUR:								// Valeur sélectionnée (ushort)
						ushort valeur;
						buffer.Get(out valeur);
						ignoreValueChanged = true;											// on ne notifie pas le chagement de valeur
						PositionRecue = valeur - 1;
						//SetValue(SelectedIndexProperty, valeur - 1);
						ignoreValueChanged = false;
						break;

					case ProprietesWpf.MULTICHOIX_OUVERTURE_AUTO:					// Si la comboBox doit s'ouvrir lors du passage en Input
						autoOpen = true;
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
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.ComboBox, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions
		/// <summary>
		/// Focus pour input
		/// </summary>
		//protected override void OnGotFocus(RoutedEventArgs e)
		//{
		//   IsDropDownOpen = autoOpen;
		//   autoOpen = false;
		//}
		#endregion Fonctions


		/*
		#region Ecouteurs
		/// <summary>
		/// Gestionnaire d'évènement "PreviewKeyDown" pour la fonction XMEInput
		/// </summary>
		/// 
		private void PreviewKeyDownHandler(object sender, KeyEventArgs e)
		{
			var application = ((App)Application.Current).Appli;

			if (!application.AttenteInput) // cas impossible en théorie: un contrôle ne peut pas avoir le focus sans que l'on soit en Input
			{
				e.Handled = true;
				return;
			}

			string key;

			// Touches de fonction (Fn)
			if (e.Key == Key.System && e.SystemKey == Key.F10) key = "F10";
			else if (XHtmlApplication.FnKeys.Contains(e.Key)) key = e.Key.ToString();

			// Autres touches
			else // pas la peine de faire les tests spécifiques si une touche générique (Fn) a déjà été détetée
			{
				if ((e.Key == Key.Return && !IsDropDownOpen) ||			// ne pas traiter quand combobox ouverte : géré par wpf
						(e.Key == Key.Enter && !IsDropDownOpen) ||		// ne pas traiter quand combobox ouverte : géré par wpf
						(e.Key == Key.Escape && !IsDropDownOpen) ||		// ne pas traiter quand combobox ouverte : géré par wpf
						(e.Key == Key.Back && !IsDropDownOpen) ||			// ne pas traiter quand combobox ouverte : géré par wpf
						(e.Key == Key.Tab && !IsDropDownOpen) ||			// ne pas traiter quand combobox ouverte : géré par wpf
					//e.Key == Key.Space ||		// ne pas traiter : géré par wpf

						e.Key == Key.Insert ||

					//e.Key == Key.Next ||		// ne pas traiter : géré par wpf
					//e.Key == Key.Prior ||		// ne pas traiter : géré par wpf
					//e.Key == Key.PageDown ||	// ne pas traiter : géré par wpf
					//e.Key == Key.PageUp ||	// ne pas traiter : géré par wpf
					//e.Key == Key.Down ||		// ne pas traiter : géré par wpf
					//e.Key == Key.Up ||			// ne pas traiter : géré par wpf

						(((Keyboard.Modifiers & ModifierKeys.Control) != 0) && ValidModifiedKeys.Contains(e.Key))
					)
				{
					key = e.Key.ToString();
				}
				else return; // les touches restantes ne sont pas bloquées
			}

			e.Handled = true;
			application.SendInputKeyDown(key);
		}

		/// <summary>
		/// Gestionnaire d'évènement "PreviewMouseDown"
		/// </summary>
		private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		{
			if (IsReadOnly) // refusé si IsReadOnly
			{
				e.Handled = true;
				return;
			}

			// on laisse passer les clics gauches (simples & doubles) en input (géré par wpf)
			if (Page.Window.ActiveControl == this && e.ChangedButton == MouseButton.Left) return;

			((App)Application.Current).Appli.SendMouseDown(this, e);
		}

		/// <summary>
		/// Gestionnaire d'évènement "SelectionChanged" pour la notification
		/// </summary>
		private void SelectionChangedHandler(object sender, SelectionChangedEventArgs e)
		{
			var application = ((App)Application.Current).Appli;

			// on ne notifie pas si le multichoix n'est pas le contrôle actif en input ou si le changement de valeur est fait par programme
			if (ignoreValueChanged || !(application.AttenteInput && Page.Window.ActiveControl == this)) return;

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);			//début de l'acquittement ou de la réponse

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris

			response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
			response.Put((byte)MouseEvent.Notification);
			response.Put(ProprietesWpf.PAGE_NUMERO);					// Numéro de la page contenant la donnée cliquée (byte)
			response.Put(Page.NumPage);
			response.Put(ProprietesWpf.IDENT_UNIQUE);					// Id de la donnée cliquée (uint)
			response.Put(Id);
			response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);		// Numéro du point de séquence de la donnée cliquée (ushort)
			response.Put(SeqPoint);

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris

			response.Put(ProprietesWpf.MULTICHOIX_VALEUR);			// Nouvelle valeur (ushort)
			response.Put((ushort)(SelectedIndex + 1));

			application.Send(response);
		}

		/// <summary>
		/// Envoi des infos à l'appli pour ouverture de zoom
		/// </summary>
		private void ZoomButtonClickHandler(object sender, RoutedEventArgs e)
		{
			e.Handled = true;

			var application = ((App)Application.Current).Appli;

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse

			application.SetInputBuffer(response);

			response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);		// Début de l'envoi des évenements souris

			response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);			// Type d'évènement souris (byte)
			if (((App)Application.Current).Client.VersionInterneServeur <= 1)
				response.Put((byte)MouseEvent.ZoomCall);
			else
				response.Put((byte)(MouseEvent.ZoomComboCall));


			response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);			// Fin de l'envoi des évenements souris

			application.Send(response);
		}
		#endregion Ecouteurs
*/
		/// <summary>
		/// ajouter les commandes necessaires pour le client
		/// </summary>
		/// <param name="envois"></param>
		/// <param name="page"></param>
		/// <param name="niveau"></param>
		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ComboBoxVersJson cb = new ComboBoxVersJson();

			if (this.imageComboBox)
			{
				cb.formatSelection = "imageMultiChoix";
				cb.formatResult = "imageMultiChoix";
			}
			else
			{
				cb.formatSelection = "texteMultiChoix";
				cb.formatResult = "texteMultiChoix";
			}

			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
			ListeParametresEnvoyes paramsCreation = new ListeParametresEnvoyes();

			// surtout pas, car ca casse le s2id			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));


			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs, this.Page.Html, 0,false);

			paramsValeurs.Ajouter("pointSequence", this.SeqPoint.ToString(), page.NumPage.ToString());

			if (IsReadOnly.HasValue)
				paramsValeurs.Ajouter("lectureSeule", (bool)IsReadOnly ? "true" : "false");


			int cpt = 0;
			foreach (UnItemMultiChoix item in Items)
			{
				UnItemJson j = new UnItemJson();
				j.id = cpt.ToString();
				cpt++;
				j.text = item.Libelle;
				cb.data.results.Add(j);
			}

			cb.autoOpen = autoOpen;

			if (this.PositionRecue.HasValue)
				cb.position = this.PositionRecue.Value.ToString();

//			string datasJson = HtmlGlobal.ToJsonString(datas, this.Page.Html.JsonDataComboBox, false);
			string parametresJson = HtmlGlobal.ToJsonString(cb, this.Page.Html.JsonParametresComboBox, false);

	//		paramsCreation.Ajouter("liste", datasJson);
			paramsCreation.Ajouter("paramsCreation", parametresJson);

			envois.Ajouter("creerMultiChoix", HtmlGlobal.ToJsonString(paramsCreation, this.Page.Html.JsonParamsEnvoyes, false), HtmlGlobal.CalculerId(this.Id, page.Id, niveau));


			// je met les coordonnées + couleurs etc sur l'objet hidden, comme ca si on recrée l'objet (liste de choix modifiée), on reprend
			// les bonnes caractéristiques
			paramsValeurs.Ajouter("idPage", this.Page.Html.CalculerIdPage(page.Id));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));

			// j'envoie la meme chose pour le conteneur du combo
			envois.Ajouter("setObjetCourant", "s2id_" + HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			paramsValeurs.Ajouter("idPage", this.Page.Html.CalculerIdPage(page.Id));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));

		}


	}
}
