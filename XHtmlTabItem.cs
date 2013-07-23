//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlTabItem.xaml.cs
// Description : Objet Onglet
//___________________________________________________________________________

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Divalto.Systeme;
using Divalto.Systeme.DVOutilsSysteme;
using System.Linq;
using Divalto.Systeme.XHtml;

namespace Divaltohtml

{
	/// <summary>
	/// Classe XHtmlTabItem = Un onglet
	/// </summary>
	public partial class XHtmlTabItem : FrameworkElement, IXHtmlObject 
	{
		public uint Id { get; set; }
//		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }
		public XHtmlPage Page { get; set; }

		public string Header;
//		public string ToolTip;
		public int? IdFond;
		public byte Visibilite=0;
		XHtmlImageFile imageFile;
		public bool IsSelected;

		public byte PageNumber { get; set; }

		internal static DependencyProperty ImageProperty = DependencyProperty.Register("Image", typeof(ImageSource), typeof(XHtmlTabItem));
		//internal ImageSource Image
		//{
		//	get { return (ImageSource)(GetValue(ImageProperty)); }
		//	set { SetValue(ImageProperty, value); }
		//}

		private int codePage, codePageBulle;


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlTabItem class.
		/// </summary>
		public XHtmlTabItem(XHtmlTabControl tabControl)
		{
//			InitializeComponent();
//			DataContext = this;
//			PreviewMouseDown += tabControl.TabMouseDownHandler;
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
			while (property != ProprietesWpf.ONGLET_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								// Début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						// Presentation.SetProperties();	// pas de présentation pour les onglets (ignoré)
						break;

					case ProprietesWpf.CODE_PAGE:											// Code page
						buffer.Get(out codePage);
						break;

					case ProprietesWpf.ONGLET_LIBELLE:									// Libellé (string)
						string text;
						buffer.GetStringCP(out text, codePage);
						Header = text;
						break;

					case ProprietesWpf.ONGLET_IMAGE_DEBUT:								// Libellé (string)
						imageFile = new XHtmlImageFile();
						imageFile.ReadProperties(buffer);
//						Image = XHtmlImage.GetImage(imageFile);

						// voir si c utile !!!!!!!!!!!!!!!!!
						if (this.Page.Html.App.ImagesCss.FirstOrDefault(e => e.FileName == imageFile.FileName) == null)
						{
							imageFile.Css = imageFile.GenererCss();
							this.Page.Html.App.ImagesCss.Add(imageFile);
						}


						break;

					case ProprietesWpf.ONGLET_NUMERO_PAGE:								// Numéro de la page liée à l'onglet (byte)
						byte pageNumber;
						buffer.Get(out pageNumber);
						PageNumber = pageNumber;
						break;

					case ProprietesWpf.CODE_PAGE_BULLE:									// Code page bulle
						buffer.Get(out codePageBulle);
						break;

					case ProprietesWpf.ONGLET_BULLE:										// Texte de la bulle (string)
						string toolTip;
						buffer.GetStringCP(out toolTip, codePageBulle);
						ToolTip = string.IsNullOrEmpty(toolTip) ? null : toolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;

					case ProprietesWpf.ONGLET_COULEUR_FOND:							// Couleur du Fond (ushort)
						ushort id;
						buffer.Get(out id);
//						Brush background = Application.Current.Resources["Brush-" + id] as Brush;
						IdFond = id;
//						if (background != null) ContentBorder.Background = background;
//						else ContentBorder.ClearValue(Border.BackgroundProperty);
						break;

					case ProprietesWpf.ONGLET_VISIBILITE:								// visibilité
						byte visibiliteValue;
						buffer.Get(out visibiliteValue);
						Visibilite = visibiliteValue;
						//Visibilites visibilite = (Visibilites)visibiliteValue;
						//switch (visibilite)	// 0 = visible, 1 = grisé, 2 = illisible, 3 = caché
						//{
						//	case Visibilites.Visible:
						//		IsEnabled = true;
						//		Visibility = Visibility.Visible;
						//		break;

						//	case Visibilites.Grise:
						//		IsEnabled = false;
						//		Visibility = Visibility.Visible;
						//		break;

						//	case Visibilites.Cache:
						//		IsEnabled = false;
						//		Visibility = Visibility.Collapsed;
						//		break;
						//}
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.TabItem, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés

		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			
		}


		public ItemOngletVersJson AjouterEnvoisUnItem(ListeParametresEnvoyes envois, XHtmlPage page, int niveau)
		{
			ItemOngletVersJson io = new ItemOngletVersJson();

			io.texte = this.Header;
			io.bulle = this.ToolTip;

			if (imageFile != null && string.IsNullOrEmpty(imageFile.FileName) == false)
			{
				io.bitmap = imageFile.FileName.Replace('.', '_');
			}


			//if (this.Presentation != null && this.Presentation.idFond.HasValue)
			//{
			//	io.fond = this.Page.Html.App.GenererUneValeurDeCouleur(this.Presentation.idFond);
			//}
			if (this.IdFond.HasValue)
			{
				io.fond = this.Page.Html.App.GenererUneValeurDeCouleur((ushort)this.IdFond);
			}

			io.numeroPage = this.PageNumber;
			io.visibilite = (byte)this.Visibilite;

			return io;
		}
	}

}
