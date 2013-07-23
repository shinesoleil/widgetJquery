using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using System.Windows;
using Divalto.Systeme.XHtml;
using System.Runtime.Serialization;

namespace Divaltohtml
{
	[DataContract]
	public class XHtmlWindow
	{
		[DataMember]
		internal Collection<XHtmlPage> ListOfPages; //  = new Collection<XHtmlPage>();
		public string MaskName;
		[DataMember]
		public byte PageNum;
		[DataMember]
		public ushort Left, Top;
		public ResizeMode ModeResize;
		public ushort CouleurFond;
		public string Title;
		public XHtmlImageFile icon;
		[DataMember]
		public ushort minContentWidth, minContentHeight;
		[DataMember]
		public string Id;
		
		
		public FrameworkElement ActiveControl;
		public XHtmlPage CurrentPage;

		internal XHtmlPage GetPage(string id)
		{
			return ListOfPages.FirstOrDefault(page => page.Id == id);
		}

		public XHtmlWindow()
		{
			Init();
		}

		void Init()
		{
			ListOfPages = new Collection<XHtmlPage>();
		}

		[OnDeserializing]
		public void OnDeserialisation(StreamingContext c)
		{
			Init();
		}


		#region Lecture propriétés

		/// <summary>
		/// Sets the size of the window
		/// </summary>
		/// <param name="buffer">Buffer where the properties are read</param>
		internal void SetMinContentSize(DVBuffer buffer)
		{

			buffer.Get(out minContentWidth);
			buffer.Get(out minContentHeight);
			//MainCanvas.MinWidth = minContentWidth;
			//MainCanvas.MinHeight = minContentHeight;
		}

		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		internal void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.FENETRE_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.FENETRE_POSITION:					// position de la fenêtre (x: ushort, y: ushort)
						// Window lastWindow = ((App)Application.Current).Appli.StackOfWindows.Peek();
						ushort left, top;
						buffer.Get(out left);
						buffer.Get(out top);
						Left = left; //  + lastWindow.Left;
						Top = top; //  + lastWindow.Top;
						break;

					case ProprietesWpf.FENETRE_TAILLE:						// taille de la fenêtre (x: ushort, y: ushort)
						SetMinContentSize(buffer);
						break;

					case ProprietesWpf.FENETRE_STYLE:						// style de fenêtre (byte) : 1 = taille variable; 2 = taille fixe
						byte resizeMode;
						buffer.Get(out resizeMode);
						ModeResize = (resizeMode == 2) ? ResizeMode.NoResize : ResizeMode.CanResize;
						break;

					case ProprietesWpf.FENETRE_COULEUR_FOND:				// identifiant de couleur (ushort)
						ushort id;
						buffer.Get(out id);
						CouleurFond = id; // Application.Current.Resources["Brush-" + id] as SolidColorBrush;
						break;

					case ProprietesWpf.FENETRE_TITRE:						// titre de la fenêtre (string) - peut être vide
						string title;
						int codePage;
						buffer.Get(out codePage);
						buffer.GetStringCP(out title, codePage);
						Title = title;
						break;

					case ProprietesWpf.FENETRE_ICONE:						// titre de la fenêtre (string)
						icon = new XHtmlImageFile();
						icon.ReadProperties(buffer);
						// SetIcon(icon);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Window, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés

	}
}