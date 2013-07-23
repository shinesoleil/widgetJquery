//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlTextBox.xaml.cs
// Description : Objet Champ
//___________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Input;
//using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using System.Windows.Input;
using System.Drawing;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Windows;
using System.Text;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlTextBox = Un champ
	/// </summary>
	public partial class XHtmlTextBox : FrameworkElement, IXHtmlEditableObject
	{
		#region Liste des touches valides en input
		internal static List<Key> ValidKeys = new List<Key>						// touches seules
		{
			Key.Return,
			Key.Enter,
			Key.Escape,
			//Key.Back,			// ne pas traiter : utilisé dans le texte
			Key.Tab,
			//Key.Space,		// ne pas traiter : utilisé dans le texte

			//Key.Insert,		// ne pas traiter : utilisé dans le texte

			//Key.Next,			// ne pas traiter : utilisé dans le texte
			//Key.Prior,		// ne pas traiter : utilisé dans le texte
			//Key.PageDown,	// ne pas traiter : utilisé dans le texte
			//Key.PageUp,		// ne pas traiter : utilisé dans le texte
			Key.Down,			//TODISCUSS pas standard (= tab)
			Key.Up				//TODISCUSS pas standard (= tab arrière)
		};

		internal static List<Key> ValidModifiedKeys = new List<Key>				// touches combinées à CTRL
		{
			//Key.A,		//TODISCUSS ne devrait pas être traitée : selectionne tout le texte
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

		//public static readonly DependencyProperty IsMandatoryProperty = DependencyProperty.Register("IsMandatory", typeof(bool), typeof(XHtmlTextBox));
		//public bool IsMandatory
		//{
		//   get { return (bool)GetValue(IsMandatoryProperty); }
		//   set { SetValue(IsMandatoryProperty, value); }
		//}

		public bool IsZoomCaller;
		//public static readonly DependencyProperty IsZoomCallerProperty = DependencyProperty.Register("IsZoomCaller", typeof(bool), typeof(XHtmlTextBox));
		//public bool IsZoomCaller
		//{
		//   get { return (bool)GetValue(IsZoomCallerProperty); }
		//   set { SetValue(IsZoomCallerProperty, value); }
		//}

		internal int CodePage;

		private bool isNumerical, isDragging;
		private ushort angle;
		
		private string _text;
		private string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
				textHasValue = true;
			}
		}
		private bool textHasValue = false;



//		private Point startPoint;
		private bool? IsReadOnly;
		public bool IsMandatory;
//		public  string Text;
		public  int MaxLengthProperty;
		public TextAlignment TextAlignment;

		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlTextBox class.
		/// </summary>
		public XHtmlTextBox(XHtmlPage page)
		{
//			InitializeComponent();
			Page = page;

//			PreviewKeyDown += PreviewKeyDownHandler;
//			PreviewMouseDown += PreviewMouseDownHandler;
			//GotFocus += (sender, e) =>
			//{
			//   TextAlignment = TextAlignment.Left;
			//   SelectAll();
			//};

			//LostFocus += (sender, e) =>
			//{
			//   text = Text;
			//   Presentation.SetCadrage();
			//   if (isNumerical && Presentation != null && Presentation.Cadrage == Cadrage.Defaut)
			//      TextAlignment = TextAlignment.Right;
			//};
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
			while (property != ProprietesWpf.CHAMP_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
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

					case ProprietesWpf.CHAMP_OBLIGATOIRE:								// (Uniquement si le champ est obligatoire)
						byte isMandatory;
						buffer.Get(out isMandatory);
						IsMandatory = (isMandatory != 0);
						break;

					case ProprietesWpf.CHAMP_NUMERIQUE:									// (Uniquement si le champ n'accepte que des valeurs numériques)
						isNumerical = true;
//						PreviewTextInput += (s, e) => { e.Handled = !IsValidNumericChar(e.Text); }; // Limitations aux caractères numériques + opérateurs pour la saisie
						break;

					case ProprietesWpf.CODE_PAGE:											// Code page
						buffer.Get(out CodePage);
						break;

					case ProprietesWpf.CHAMP_VALEUR:										// Contenu du champ (string)
						string wt;
						buffer.GetStringCP(out wt, CodePage);
						Text = wt;
						break;

					case ProprietesWpf.CHAMP_LONGUEUR_SAISIE:							// Longueur de saisie autorisée (ushort)
						ushort maxLength;
						buffer.Get(out maxLength);
						MaxLengthProperty = (int)maxLength;
	//					SetCurrentValue(MaxLengthProperty, (int)maxLength);
						break;

					case ProprietesWpf.TEXTE_ANGLE:										// Angle au sens trigo en ° (ushort)
						buffer.Get(out angle);

						// HACK inversion des hauteurs et largeur si angle multiple de 90
						if ((angle + 90) % 180 == 0)
						{
							Presentation.Left += (Width - Height) / 2;
							Presentation.Top -= (Width - Height) / 2;

							var horizontalPaddingTemp = Presentation.HorizontalPadding;
							Presentation.HorizontalPadding = Presentation.VerticalPadding;
							Presentation.VerticalPadding = horizontalPaddingTemp;

							var widthTemp = Presentation.OriginalWidth;
							Presentation.OriginalWidth = Presentation.OriginalHeight;
							Presentation.OriginalHeight = widthTemp;
							Presentation.SetProperties();
						}

//						RenderTransform = new RotateTransform(-angle, Width / 2, Height / 2);
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
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.TextBox, property.ToString());
				}

				buffer.Get(out property);
			}

			// numérique aligné à droite en affichage
			if (isNumerical && Presentation != null && Presentation.Cadrage == Cadrage.Defaut)
				TextAlignment = TextAlignment.Right;

			// gestion de l'illisibilité
			//if (textHasValue && Presentation != null && Presentation.Visibilite == Visibilites.Illisible)
			//	Text = "xxxxxxxxxxxxxx";
		}
		#endregion Lecture propriétés


		/// <summary>
		/// Checks if the given string is a valid number (0-9 , . - +)
		/// </summary>
		/// <param name="str">String to check</param>
		/// <returns>True if the string is a valid number</returns>
		public static bool IsValidNumericChar(string str)
		{
			if (string.IsNullOrEmpty(str)) return true; // garde-fou

			if (str == NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator |
				 str == NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator |
				 str == NumberFormatInfo.CurrentInfo.CurrencySymbol |
				 str == NumberFormatInfo.CurrentInfo.NegativeSign |
				 str == NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol |
				 str == NumberFormatInfo.CurrentInfo.NumberDecimalSeparator |
				 str == NumberFormatInfo.CurrentInfo.NumberGroupSeparator |
				 str == NumberFormatInfo.CurrentInfo.PercentDecimalSeparator |
				 str == NumberFormatInfo.CurrentInfo.PercentGroupSeparator |
				 str == NumberFormatInfo.CurrentInfo.PercentSymbol |
				 str == NumberFormatInfo.CurrentInfo.PerMilleSymbol |
				 str == NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol |
				 str == NumberFormatInfo.CurrentInfo.PositiveSign)
				return true;

			bool ret = true;
			int l = str.Length;
			for (int i = 0; i < l; i++)
			{
				char ch = str[i];
				ret &= (Char.IsDigit(ch) || ch == '.');
			}
			return ret;
		}


		//#region Ecouteurs
		///// <summary>
		///// Gestionnaire d'évènement "PreviewKeyDown" pour la fonction XMEInput
		///// </summary>
		//private static void PreviewKeyDownHandler(object sender, KeyEventArgs e)
		//{
		//   var application = ((App)Application.Current).Appli;

		//   if (!application.AttenteInput) // cas impossible en théorie: un contrôle ne peut pas avoir le focus sans que l'on soit en Input
		//   {
		//      e.Handled = true;
		//      return;
		//   }

		//   if (e.Key == Key.Apps)
		//   {
		//      // TODO simuler un clic droit sur touche windows 2
		//      e.Handled = true;
		//   }

		//   string key;

		//   // Touches de fonction (Fn)
		//   if (e.Key == Key.System && e.SystemKey == Key.F10) key = "F10";
		//   else if (XHtmlApplication.FnKeys.Contains(e.Key)) key = e.Key.ToString();

		//   // Autres touches
		//   else // pas la peine de faire les tests spécifiques si une touche générique (Fn) a déjà été détetée
		//   {
		//      if (ValidKeys.Contains(e.Key) || (
		//            (Keyboard.Modifiers & ModifierKeys.Control) != 0
		//            && (Keyboard.Modifiers & ModifierKeys.Alt) == 0
		//            && ValidModifiedKeys.Contains(e.Key))
		//         )
		//         key = e.Key.ToString();
		//      else return; // les touches restantes ne sont pas bloquées
		//   }

		//   e.Handled = true;
		//   application.SendInputKeyDown(key);
		//}

		///// <summary>
		///// Gestionnaire d'évènement "PreviewMouseDown"
		///// </summary>
		//private void PreviewMouseDownHandler(object sender, MouseButtonEventArgs e)
		//{
		//   if (IsReadOnly) // si IsReadOnly, on ne notifie pas l'appli
		//   {
		//      e.Handled = true;
		//      return;
		//   }

		//   // on laisse passer les clics gauches (simples & doubles) en input (géré par wpf)
		//   if (Page.Window.ActiveControl == this && e.ChangedButton == MouseButton.Left) return;

		//   ((App)Application.Current).Appli.SendMouseDown(this, e);
		//}

		///// <summary>
		///// Envoi des infos à l'appli pour ouverture de zoom
		///// </summary>
		//private void ZoomButtonClickHandler(object sender, RoutedEventArgs e)
		//{
		//   e.Handled = true;

		//   var application = ((App)Application.Current).Appli;

		//   var response = new DVBuffer();
		//   response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse

		//   application.SetInputBuffer(response);

		//   response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);		// Début de l'envoi des évenements souris

		//   response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);			// Type d'évènement souris (byte)
		//   if (((App)Application.Current).Client.VersionInterneServeur <= 1)
		//      response.Put((byte)MouseEvent.ZoomCall);
		//   else
		//      response.Put((byte)(MouseEvent.ZoomComboCall));


		//   response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);			// Fin de l'envoi des évenements souris

		//   application.Send(response);
		//}
		//#endregion Ecouteurs

		//// D & D non implémenté pour cause d'incompatibilité avec la gestion des mouseUp et MouseDown
		//#region Drag & Drop
		//public void PreviewMouseLeftButtonDownHandler(object sender, MouseEventArgs e)
		//{
		//   if (e == null) throw new ArgumentNullException("e");

		//   startPoint = e.GetPosition(null);
		//}
		//public void PreviewMouseMoveHandler(object sender, MouseEventArgs e)
		//{
		//   if (e == null) throw new ArgumentNullException("e");

		//   if (isDragging) return;
		//   if (e.LeftButton != MouseButtonState.Pressed) return;

		//   // on ne déclenche le drag&drop que si la souris a été suffisament déplacée
		//   Vector diff = startPoint - e.GetPosition(null);

		//   if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
		//       Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
		//   {
		//      isDragging = true;
		//      DataObject data = new DataObject(DataFormats.UnicodeText, Text);

		//      DragDrop.DoDragDrop(this, data, DragDropEffects.All);
		//      isDragging = false;
		//   }
		//}
		//#endregion Drag & Drop
		/// <summary>
		/// ajouter les commandes necessaires pour le client
		/// </summary>
		/// <param name="envois"></param>
		/// <param name="page"></param>
		/// <param name="niveau"></param>
		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();

			paramsValeurs.Ajouter("idObjet", HtmlGlobal.CalculerId(this.Id, page.Id, niveau), this.Page.Html.CalculerIdPage(page.Id));


			if (IsReadOnly.HasValue)
				paramsValeurs.Ajouter("lectureSeule", (bool)IsReadOnly ? "true" : "false");

			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs, this.Page.Html,this.CodePage,false);

			if (this.textHasValue)											// pour le caché etc, apres la presentation
				paramsValeurs.Ajouter("texteChamp", this.Text);

			paramsValeurs.Ajouter("pointSequence", this.SeqPoint.ToString(), page.NumPage.ToString());
			
			// n'est envoyé que pourle champ en cours de saisie
			if (this.MaxLengthProperty != 0)
				paramsValeurs.Ajouter("tailleSaisie", this.MaxLengthProperty.ToString());

			if (this.ListOfValidButtons != null && this.ListOfValidButtons.Count > 0)
			{
				StringBuilder vb = new StringBuilder();
				vb.Append("@");
				foreach (string n in ListOfValidButtons)
				{
					vb.Append(n);
					vb.Append("@");
				}
				paramsValeurs.Ajouter("boutonsValides", vb.ToString());
			}

			UnParametreEnvoye p = new UnParametreEnvoye();

			envois.Ajouter("creerObjet", "<input type='text'/>", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));
			envois.Ajouter("ajoutObjetCourant", "");

		}

	}
}
