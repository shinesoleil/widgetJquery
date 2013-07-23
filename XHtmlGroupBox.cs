//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlGroupBox.xaml.cs
// Description : Objet groupe d'objets
//___________________________________________________________________________

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Divalto.Systeme;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	/// <summary>
	/// Logique d'interaction pour XHtmlGroupBox.xaml
	/// </summary>
	public partial class XHtmlGroupBox : FrameworkElement, IXHtmlObject
	{
		public uint Id { get; set; }
		public XHtmlPage Page { get; set; }
		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }

		public ushort? idFondGroupe;
		public string LibelleGroupe;


		//public static readonly DependencyProperty ContentBackgroundProperty = DependencyProperty.Register("ContentBackground", typeof(Brush), typeof(XHtmlGroupBox));
		//public Brush ContentBackground
		//{
		//	get { return (Brush)GetValue(ContentBackgroundProperty); }
		//	set { SetValue(ContentBackgroundProperty, value); }
		//}

		//public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(XHtmlGroupBox));
		//public bool IsActive
		//{
		//	get { return (bool)GetValue(IsActiveProperty); }
		//	set { SetValue(IsActiveProperty, value); }
		//}

		private int codePage;



		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlGroupBox class.
		/// </summary>
		public XHtmlGroupBox(XHtmlPage page)
		{
//			InitializeComponent();
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

			buffer.Get(out property);
			while (property != ProprietesWpf.GROUPBOX_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:									// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.CODE_PAGE:												// Code page
						buffer.Get(out codePage);
						break;

					case ProprietesWpf.GROUPBOX_FOND_GROUPE:								// fond du groupe
						ushort id;
						buffer.Get(out id);
						idFondGroupe = id;
						break;

					case ProprietesWpf.GROUPBOX_LIBELLE:									// libellé du titre (string)
						string header;
						buffer.GetStringCP(out header, codePage);
						LibelleGroupe = header;
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.GroupBox, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions statiques
		///// <summary>
		///// Sets the active groupBox's depending on the focused object
		///// </summary>
		///// <param name="activeControl">Focused control</param>
		//internal static void SetActiveGroup(IXHtmlObject activeControl)
		//{
		//	var window = activeControl.Page.Window;

		//	ResetActiveGroup(window); // garde-fou cas du passage input page A vers input page B : il faut razer la page A avant d'affecter le groupe courant page B

		//	var controlPresentation = activeControl.Presentation;

		//	foreach (var groupBox in window.ListOfGroupBoxes.Where(gb => gb.Page == activeControl.Page))
		//	{
		//		var groupBoxPresentation = groupBox.Presentation;
		//		groupBox.IsActive = controlPresentation.OriginalLeft >= groupBoxPresentation.OriginalLeft
		//			&& controlPresentation.OriginalTop >= groupBoxPresentation.OriginalTop
		//			&& controlPresentation.OriginalLeft + controlPresentation.OriginalWidth <= groupBoxPresentation.OriginalLeft + groupBoxPresentation.OriginalWidth
		//			&& controlPresentation.OriginalTop + controlPresentation.OriginalHeight <= groupBoxPresentation.OriginalTop + groupBoxPresentation.OriginalHeight;
		//	}
		//}

		///// <summary>
		///// resets the active groupBox (to kill the style when in consult or eq)
		///// </summary>
		//internal static void ResetActiveGroup(XHtmlWindow window)
		//{
		//	window.ListOfGroupBoxes.ForEach(gb => gb.IsActive = false);
		//}
		#endregion Fonctions statiques

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

			if (this.LibelleGroupe != null)
				paramsValeurs.Ajouter("texteGroupe", this.LibelleGroupe);

			if (Presentation != null)
			{
				this.Presentation.GenererHtml(paramsValeurs, this.Page.Html, this.codePage,true);

				UnParametreEnvoye pp = paramsValeurs.commandes.FirstOrDefault(c => c.commande == "css-fond");
				if (pp != null)
					pp.commande = "couleurTitreGroupe";

				pp = paramsValeurs.commandes.FirstOrDefault(c => c.commande == "css-police");
				if (pp != null)
					pp.commande = "policeTitreGroupe";

				pp = paramsValeurs.commandes.FirstOrDefault(c => c.commande == "css-padding");
				if (pp != null)
					pp.commande = "paddingTitreGroupe";

			}

			if (this.idFondGroupe.HasValue)
			{
				string ct = this.Page.Html.App.GenererUneValeurDeCouleur(this.idFondGroupe);
				if (ct != null)
				{
					paramsValeurs.Ajouter("css-fond", ct);
				}
			}


			UnParametreEnvoye p = new UnParametreEnvoye();

//			envois.Ajouter("creerObjet", "<P></P>", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));

			envois.Ajouter("creerGroupBox", " ", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("ajoutObjetCourant", "");


			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));

		}


	}
}
