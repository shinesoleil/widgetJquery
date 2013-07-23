//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlLabel.xaml.cs
// Description : Objet Texte
//___________________________________________________________________________

/*
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
*/

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Divaltohtml
{

	/// <summary>
	/// Classe XHtmlLabel = Un texte
	/// </summary>
	public partial class XHtmlLabel : FrameworkElement ,IXHtmlObject 
	{
		public uint Id { get; set; }
		public XHtmlPage Page { get; set; }
		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }

		private string _text;
		private string text
		{
			get
			{
				return _text;
			}
			set {
				_text = value;
				textHasValue = true;
			}
		}
		private bool textHasValue = false;

		private int codePage;
		string Content;


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlLabel class.
		/// </summary>
		public XHtmlLabel(XHtmlPage page)
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
			while (property != ProprietesWpf.TEXTE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:						// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.CODE_PAGE:									// Code page
						buffer.Get(out codePage);
						break;

					case ProprietesWpf.TEXTE_LIBELLE:							// Contenu du Label (string avec code page)
						{
							string t;
							buffer.GetStringCP(out t, codePage);
							text = t;
							Content = text;
						}
						break;

					case ProprietesWpf.TEXTE_ANGLE:								// Angle au sens trigo en ° (ushort)
						ushort ang;

						buffer.Get(out  ang); this.Presentation.angle = ang;

						// HACK inversion des hauteurs et largeur si angle multiple de 90
						if ((Presentation.angle + 90) % 180 == 0)
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

//!!!!!!!!!!!!!!!!!						RenderTransform = new RotateTransform(-angle, Width / 2, Height / 2);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Label, property.ToString());
				}

				buffer.Get(out property);
			}

			// gestion de l'illisibilité
			Content = (Presentation.Visibilite == Visibilites.Illisible) ? XHtmlApplication.Resources["UnreadableString"] as string : text;
		}
		#endregion Lecture propriétés


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
			
			if (this.textHasValue)
				paramsValeurs.Ajouter("texteLabel", this.text);

			if (Presentation != null)
				this.Presentation.GenererHtml(paramsValeurs,this.Page.Html,this.codePage,false);

			UnParametreEnvoye p = new UnParametreEnvoye();

			envois.Ajouter("creerObjet", "<P></P>", HtmlGlobal.CalculerId(this.Id, page.Id, niveau));
			envois.Ajouter("propsObjet", HtmlGlobal.ToJsonString(paramsValeurs, this.Page.Html.JsonParamsEnvoyes, false));
			envois.Ajouter("ajoutObjetCourant","");


///////////////////////////////////////// MORCEAU qui génére tout le code HTML : beaucoup plus rapide a l'exec
////////////////////////////////////////  on devrait faire comme ca pour les objets les plus utilisés (label champ)
/*
			ListeParametresEnvoyes paramsValeurs = new ListeParametresEnvoyes();
		

			string css = this.Presentation.GenererHtml(paramsValeurs);


			css += "id='" + this.Id.ToString() + "'";

			int i = 0;
			UnParametreEnvoye p = new UnParametreEnvoye();
			envois.commandes.Add(p);
			p.commande = "html";
			p.valeur = "<P " + css + ">" + this.text + "</P>";

			p = new UnParametreEnvoye();
			envois.commandes.Add(p);
			p.commande = "script";
			p.valeur = "AjouterObjet(codeHtml,bodyCourant);";
			*/
//////////////////////////////////////////////////////////////


		}



	}
}
