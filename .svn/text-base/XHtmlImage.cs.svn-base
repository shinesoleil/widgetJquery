using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Divalto.Systeme;
using Divalto.Systeme.DVOutilsSysteme;

namespace Divaltohtml
{
	/// <summary>
	/// Classe XHtmlImage = Une image
	/// </summary>
	public class XHtmlImage : FrameworkElement, IXHtmlObject
	{
		public uint Id { get; set; }
		public XHtmlPage Page { get; set; }
//		public FrameworkElement FrameworkElement { get { return this; } }
		public XHtmlPresentation Presentation { get; set; }

		private StretchMode stretchMode = StretchMode.Aucun;
		public HorizontalAlignment horizontalAlignment;
		public VerticalAlignment verticalAlignment;
		public bool SnapsToDevicePixels;
		public Stretch stretch;

		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlImage class.
		/// </summary>
		public XHtmlImage(XHtmlPage page)
		{
			Page = page;

			horizontalAlignment = HorizontalAlignment.Left;
			verticalAlignment = VerticalAlignment.Top;
			SnapsToDevicePixels = true;
			stretch = Stretch.None;

			//IsEnabledChanged += (sender, e) => Opacity = (bool)e.NewValue ? 1 : 0.4;
			//SizeChanged += (sender, e) => SetStretch(); // pour mettre à jour le stretch au redimensionnement
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
			while (property != ProprietesWpf.IMAGE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PRESENTATION_DEBUT:								//début présentation
						if (Presentation == null) Presentation = new XHtmlPresentation(this);
						Presentation.ReadProperties(buffer);
						Presentation.SetProperties();
						break;

					case ProprietesWpf.IMAGE_IMAGE_DEBUT:
						XHtmlImageFile imageFile = new XHtmlImageFile();
						imageFile.ReadProperties(buffer);
						//Source = GetImage(imageFile);
						break;

					case ProprietesWpf.IMAGE_TRAITEMENT:								//type de traitement (byte)
						byte stretchCode;
						buffer.Get(out stretchCode);
						stretchMode = (StretchMode)stretchCode;
						SetStretch();
						break;
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions
		/// <summary>
		/// Sets the stretch property of the image depending on the stretch mode
		/// </summary>
		private void SetStretch()
		{
			//switch (stretchMode)
			//{
			//   case StretchMode.Defaut:					//pas de traitement
			//      Stretch = Stretch.None;
			//      break;

			//   case StretchMode.Reduire:					//Reduire
			//      Stretch = Stretch.Uniform;
			//      StretchDirection = StretchDirection.DownOnly;
			//      break;

			//   case StretchMode.PleineBoite:				//PleineBoite
			//      Stretch = Stretch.Fill;
			//      break;

			//   case StretchMode.PleineLargeur:			//PleineLargeur
			//      Stretch = Stretch.UniformToFill;
			//      if ((Width * Source.Height / Source.Width) < Height) Stretch = Stretch.Uniform;
			//      break;

			//   case StretchMode.PleineHauteur:			//PleineHauteur
			//      Stretch = Stretch.UniformToFill;
			//      if ((Height * Source.Width / Source.Height) < Width) Stretch = Stretch.Uniform;
			//      break;

			//   case StretchMode.TailleMax:				//TailleMax
			//      Stretch = Stretch.Uniform;
			//      break;

			//   //TODO Virer la mosaïque
			//   case StretchMode.Mosaique:					//Mosaique
			//      Stretch = Stretch.None;
			//      break;

			//   case StretchMode.Centre:					//Centrer
			//      Stretch = Stretch.None;
			//      HorizontalAlignment = HorizontalAlignment.Center;
			//      VerticalAlignment = VerticalAlignment.Center;
			//      break;
			//}
		}

		/// <summary>
		/// Gets the BitmapImage from the given XHtmlImageFile
		/// </summary>
		/// <param name="imageFile">XHtmlImageFile containing all the parameters used to retrieve the image</param>
		/// <returns>BitmapImage corresponding to the given XHtmlImageFile</returns>
		//internal static ImageSource GetImage(XHtmlImageFile imageFile)
		//{
		//   if (imageFile == null) return null;

		//   // path et source par défaut si la récupération de l'image échoue : on affiche un ?
		//   string path = "pack://application:,,,/Resources/QuestionMark.png";
		//   ImageSource source = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
		//   ClientLegerCacheFichiers cacheFichiers = ((App)Application.Current).Client.CacheFichiers;

		//   #region type d'image
		//   switch (imageFile.Type)
		//   {
		//      case 0: return null; // image vide

		//      case 1:
		//      case 2:
		//      case 3:
		//      case 4: return source;

		//      case 5:		//ressource
		//         path = "pack://application:,,,/images/" + imageFile.FileName + ".png";
		//         if (!ResourceHelper.ResourceExists("images/" + imageFile.FileName + ".png")) return source;
		//         break;

		//      case 6:		//fichierImage image
		//         if (!cacheFichiers.TransfererUnFichier(imageFile.FileName.TrimEnd(), false, true, true, out path, false)) return source;
		//         break;

		//      case 7:		//icône dans .exe, .dll ou .ico 
		//         if (!cacheFichiers.TransfererUnFichier(imageFile.FileName.TrimEnd(), false, true, true, out path, false)) return source;
		//         break;

		//      case 8:		//icône incluse dans XHtml.exe
		//         path = AppDomain.CurrentDomain.BaseDirectory + @"\XHtml.exe";
		//         break;

		//      case 9:		//icône shell ou dll dans rep \windows
		//         path = Environment.GetFolderPath(Environment.SpecialFolder.Windows) + imageFile.FileName;
		//         if (!File.Exists(path)) return source;
		//         break;

		//      case 10:		//icône shell ou dll dans rep \program files
		//         path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + imageFile.FileName;
		//         if (!File.Exists(path)) return source;
		//         break;

		//      case 11:		//icône shell ou dll dans rep \program files (x86)
		//         path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + imageFile.FileName;
		//         if (!File.Exists(path)) return source;
		//         break;

		//      case 12:		//icône shell ou dll dans rep \system32
		//         path = Environment.GetFolderPath(Environment.SpecialFolder.System) + imageFile.FileName;
		//         if (!File.Exists(path)) return source;
		//         break;

		//      case 13:		//icône shell ou dll dans rep \system
		//         path = Environment.GetFolderPath(Environment.SpecialFolder.System) + imageFile.FileName;
		//         if (!File.Exists(path)) return source;
		//         break;

		//   }
		//   #endregion type d'image

		//   if (imageFile.Type < 7) // image
		//   {
		//      var b = new BitmapImage();
		//      b.BeginInit();
		//      b.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;	// indispensable pour ne pas sortir en exception WPF si le profil est corrompu
		//      b.UriSource = new Uri(path, UriKind.RelativeOrAbsolute);
		//      b.EndInit();
		//      source = b;
		//   }
		//   else // icône
		//   {
		//      IntPtr[] smallIcon = new[] { IntPtr.Zero };
		//      IntPtr[] bigIcon = new[] { IntPtr.Zero };

		//      if (NativeMethods.ExtractIconEx(path, imageFile.IconIndex, bigIcon, smallIcon, 2) > 0) // si on a récupéré au moins une icône dans le fichier
		//      {
		//         if (imageFile.IsSmallIcon && smallIcon[0] != IntPtr.Zero) // petite icône
		//            source = Imaging.CreateBitmapSourceFromHIcon(smallIcon[0], Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

		//         else if (bigIcon[0] != IntPtr.Zero) // grande icône (standard)
		//            source = Imaging.CreateBitmapSourceFromHIcon(bigIcon[0], Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
		//      }
		//   }

		//   return source;
		//}
		#endregion Fonctions



		#region IXHtmlObject Membres


		public void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois,XHtmlPage page,int niveau)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}