

//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : XHtmlRessources.cs
//
// Description : ressources pour XHtml
//___________________________________________________________________________

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

/*
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
*/
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Text;
using System.Runtime.Serialization;

namespace Divaltohtml
{

	public enum Cadrage
	{
		Aucun = 0,	// jamais utilisé
		Defaut = 1,
		Gauche = 2,
		Droite = 3,
		Centre = 4
	}

	// NE PAS CHANGER CES NOMS, ILS SONT UTILISES EN JAVASCRIPT
	public enum Visibilites
	{
//		Aucun = 9,
		Visible = 0,
		Grise = 1,
		Illisible = 2,
		Cache = 3
	}
	public enum ColumnType
	{
		Aucun = 0,	// jamais utilisé
		Champ = 2,
		ChampDate = 251,
		ChampCache = 253,
		ObjetGraphique = 252,
		Case = 7,
		Multichoix = 3,
		Image = 9,
		Arbre = 17
	}
	public enum StretchMode
	{
		Aucun = 0,	// jmais utilisé
		Defaut = 1,
		Reduire = 2,
		PleineBoite = 3,
		PleineLargeur = 4,
		PleineHauteur = 5,
		TailleMax = 6,
		Mosaique = 7,
		Centre = 8
	}
	public enum MouseEvent
	{
		None = 0,
		ClickControl = 1,
		ClickTab = 2,
		ClickButton = 3,
		Notification = 4,
		ClickMenu = 10,
		ClickToolBar = 11,
		ClickDataGrid = 20,
		ClickHogFlash = 30,
		ZoomCall = 40,
		ZoomComboCall = 41,
		CloseWindow = 50,
		DropFile = 60,
		ClickCalendar = 70
	}
	public enum DataGridClick
	{
		None = 0,	// jamais utilisé
		SelectAllButton = 5,
		RowHeader = 6,
		ColumnHeader = 7,
		Cell = 8,
		ScrollBar = 13,
		NewLineButton = 15,
		ChangeRowCount = 19,
		ExpandTree = 21,
		SetTreeLevel = 22,
		Drop = 32
	}
	public enum CalendarAction
	{
		None = 0,	// jamais utilisé
		VisibleRangeChanged = 1,
		AppointmentCreation = 2,
		AppointmentDeletion = 3,
		AppointmentEdition = 4,
		AppointmentDrag = 5,
		AppointmentDetailsDisplay = 6
	}

	[DataContract]
	public class XHtmlFont
	{
		byte? R, G, B;
		string Family;
		bool HasFamily;
		[DataMember]
		ushort? Size;
		byte? Bold;
		byte? Italic;
		byte? Underline;
		byte? Strikethrough;
		public string Css;
		[DataMember]
		public ushort Id;
		[DataMember]
		public bool Vide = true;
		[DataMember]
		public bool Transmi = false;

		static public string PrefixeCss = "pol_";

		internal void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.POLICE_CREATION_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.POLICE_COULEUR:			// Couleur de la police : Rouge (byte); Vert (byte); Bleu (byte)
						byte r, g, b;
						buffer.Get(out r);
						buffer.Get(out g);
						buffer.Get(out b);
						R = r;
						G = g;
						B = b;
						Vide = false;
						// Brush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
						// Brush.Freeze();
						break;

					case ProprietesWpf.POLICE_FONTE:				// Nom de la police (string)
						string name;
						buffer.GetString(out name);
						Family = name; // !!!!!!!!!!new FontFamily(name);
						HasFamily = true;
						Vide = false;
						break;

					case ProprietesWpf.POLICE_TAILLE:			// Taille de la police (ushort)
						ushort size;
						buffer.Get(out size);
						Size = size;
						Vide = false;
						break;

					case ProprietesWpf.POLICE_GRAS:				// (Uniquement si gras modifiée)
						byte bold;
						buffer.Get(out bold);
						Bold = bold;
						Vide = false;
						// Weight = (bold == 1) ? FontWeights.Bold : SystemFonts.MessageFontWeight;
						break;

					case ProprietesWpf.POLICE_ITALIQUE:			// (Uniquement si italique modifié)
						byte italic;
						buffer.Get(out italic);
					//	Style = (italic == 1) ? FontStyles.Italic : SystemFonts.MessageFontStyle;
						Italic = italic;
						Vide = false;
						break;

					case ProprietesWpf.POLICE_SOULIGNE:			// (Uniquement si souligné modifié)
						byte underline;
						buffer.Get(out underline);
						//if (Decorations == null) Decorations = new TextDecorationCollection();
						//if (underline == 1) Decorations.Add(TextDecorations.Underline);
						Underline = underline;
						Vide = false;
						break;

					case ProprietesWpf.POLICE_BARRE:				// (Uniquement si barré modifié)
						byte strikethrough;
						buffer.Get(out strikethrough);
						Strikethrough = strikethrough;
						Vide = false;
//						if (Decorations == null) Decorations = new TextDecorationCollection();
	//					if (strikethrough == 1) Decorations.Add(TextDecorations.Strikethrough);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Font, property.ToString());
				}
				buffer.Get(out property);
			}
		}

		internal string GenererCss()
		{
			StringBuilder	r = new StringBuilder();

			r.Append("." + XHtmlFont.PrefixeCss + this.Id.ToString());
			r.Append(" { ");


			if (this.HasFamily)
				r.Append("font-family:" + this.Family + ";");

			if (this.Bold.HasValue)
			{
				if (this.Bold == 1)
					r.Append("font-weight:700" + ";");
				else
					r.Append("font-weight:400" + ";");
			}

			if (this.Italic.HasValue)
			{
				if (this.Italic == 1)
					r.Append("font-style:italic;");
				else
					r.Append("font-style:normal;");
			}

			if (this.Size.HasValue)
			{
				r.Append("font-size:" + (Size-1).ToString() + "px;"); //!!!!!!!!!!!!!!!!!!! a voir
			}

			if (this.Strikethrough.HasValue)
			{
				if (this.Strikethrough == 1)
					r.Append("text-decoration:line-through;");
				else
					r.Append("text-decoration:none;");
			}

			if (this.Underline.HasValue)
			{
				if (this.Underline == 1)
					r.Append("text-decoration:underline;");
				else
					r.Append("text-decoration:none;");
			}

			if (this.R.HasValue)
			{
				r.Append("color:rgb(" + 
					R.ToString() + "," +
					G.ToString() + "," +
					B.ToString()  + 
					");");

			}


			r.Append(" } ");
			return r.ToString();

		}

		internal static void Create(DVBuffer buffer, HtmlGlobal html)
		{
			XHtmlFont font = new XHtmlFont();
			
			buffer.Get(out font.Id);
			font.ReadProperties(buffer);
			font.Css = font.GenererCss(); 

			html.App.PolicesCss.Add(font);


			//if (font.R.HasValue)
			//{
			//   XHtmlColor color = new XHtmlColor();
			//   color.R = font.R;
			//   color.G = font.G;
			//   color.B = font.B;
			//   color.Id = font.Id;
			//   color.Prefixe = XHtmlFont.PrefixeCss;			// meme prfixe que la fonte

			//   HtmlGlobal.App.CouleursCss.Add(color);



			//}

//!!!!!!!!!!!!			Application.Resources.Add("Font-" + id, font);
		}


	}


	[DataContract]
	public class XHtmlPadding
	{
		[DataMember]
		public byte? Vertical;
		[DataMember]
		public byte? Horizontal;
		[DataMember]
		public ushort Id;
		public string Css;
		static public string PrefixeCss = "padd_";
		[DataMember]
		public bool Vide = true;
		[DataMember]
		public bool Transmi = false;


		public string GenererCss()
		{
			StringBuilder r = new StringBuilder();

		//	r.Append("." + XHtmlPadding.PrefixeCss + this.Id.ToString());

			r.Append("." + XHtmlPadding.PrefixeCss + this.Id.ToString() + ", .cssdivalto ." + XHtmlPadding.PrefixeCss + this.Id.ToString());

			
			r.Append(" { ");


			if (this.Vertical.HasValue && this.Horizontal.HasValue)
			{
				r.Append("padding:" + this.Vertical.ToString() + "px " + this.Horizontal.ToString() + "px");
			}

			r.Append(" } ");
			return r.ToString();


		}



		/// <summary>
		/// Création d'un Padding
		/// </summary>
		internal static void Create(DVBuffer buffer,HtmlGlobal html)
		{
			XHtmlPadding padd = new XHtmlPadding();
			html.App.PaddingCss.Add(padd);

			buffer.Get(out padd.Id);

			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.PADDING_CREATION_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.PADDING_VERTICAL:		// Epaisseur de la bordure (byte)
						byte verticalPadding;
						buffer.Get(out verticalPadding);
						padd.Vertical = verticalPadding;
						padd.Vide = false;
						//XHtmlApplication.Resources.Add("VerticalPadding-" + id, (double?)verticalPadding);
						break;

					case ProprietesWpf.PADDING_HORIZONTAL:		// Epaisseur de la bordure (byte)
						byte horizontalPadding;
						buffer.Get(out horizontalPadding);
						padd.Horizontal = horizontalPadding;
						padd.Vide = false;
						//XHtmlApplication.Resources.Add("HorizontalPadding-" + id, (double?)horizontalPadding);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Padding, property.ToString());
				}
				buffer.Get(out property);
			}
			padd.Css = padd.GenererCss();
		}
	}

	[DataContract]
	public class XHtmlBorder
	{

		public byte? R, G, B;
		[DataMember]
		public byte? Epaisseur;
		public static string PrefixeCss = "bord_";
		public string Css;
		[DataMember]
		public ushort Id;
		[DataMember]
		public bool Vide = true;
		[DataMember]
		public bool Transmi = false;




		/*
		 *			
		 * XHtmlFont font = new XHtmlFont();
			
			buffer.Get(out font.Id);
			font.ReadProperties(buffer);
			font.Css = font.GenererCss(); 

			HtmlGlobal.App.PolicesCss.Add(font);
 
		 * 
		 */

		internal string GenererCss()
		{
			StringBuilder r = new StringBuilder();

			r.Append("." + XHtmlBorder.PrefixeCss + this.Id.ToString());
			r.Append(" { ");


			if (this.Epaisseur.HasValue)
			{
				r.Append("border-width:" + this.Epaisseur + "px;");
			}

			if (this.R.HasValue)
			{
				r.Append("border-style:solid;");
				r.Append("border-color:rgb(" +
					R.ToString() + "," +
					G.ToString() + "," +
					B.ToString() +
					");");

			}


			r.Append(" } ");
			return r.ToString();

		}

		/// <summary>
		/// Création d'une Bordure
		/// </summary>
		/// 
		internal static void Create(DVBuffer buffer,HtmlGlobal html)
		{

			XHtmlBorder bordure = new XHtmlBorder();
			html.App.BorduresCss.Add(bordure);

			buffer.Get(out bordure.Id);

			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.BORDURE_CREATION_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.BORDURE_COULEUR:			// Couleur de la bordure : Rouge (byte); Vert (byte); Bleu (byte)
						byte r, g, b;
						buffer.Get(out r);
						buffer.Get(out g);
						buffer.Get(out b);
						bordure.B = b;
						bordure.G = g;
						bordure.R = r;
						bordure.Vide = false;
						//!!!!!!!!!!!!!!!!!!!!!!!!
						//var brush = new SolidColorBrush(Color.FromArgb(255, r, g, b));
						//brush.Freeze();
						//Application.Current.Resources.Add("BorderBrush-" + id, brush);
						break;

					case ProprietesWpf.BORDURE_EPAISSEUR:		// Epaisseur de la bordure (byte)
						byte thickness;
						buffer.Get(out thickness);
						bordure.Epaisseur = thickness;
						bordure.Vide = false;
//!!!!!!!!!!						Application.Current.Resources.Add("BorderThickness-" + id, (double?)thickness);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Border, property.ToString());
				}
				buffer.Get(out property);
			}
			bordure.Css = bordure.GenererCss();
		}

		
	}

	[DataContract]
	public class XHtmlColor
	{
		public byte? A, R, G, B;
		[DataMember]
		public ushort Id;
		[DataMember]
		public bool Vide = true;
		[DataMember]
		public bool Transparente = false;
		
		public string Css;
		public static string PrefixeCss = "coul_";
		public static string PrefixeCssFond = "bgcoul_";
		[DataMember]
		public bool Transmi = false;



		public string GenererCss()
		{
			StringBuilder r = new StringBuilder();
			string s="";

			if (this.R.HasValue)
			{
				s = 
					R.ToString() + "," +
					G.ToString() + "," +
					B.ToString() + // "," +
					//A.ToString() +
					")";

			}

			r.Append("." + XHtmlColor.PrefixeCss + this.Id.ToString());
			r.Append(" { ");
			if (s != "")
			{
				r.Append("color:rgb("); 
				r.Append(s);
				r.Append(";");
			}
			r.Append("}");


			// .cssdivalto permet d'ajouter du poids et de ne pas etre écrasé par autre chose

			r.Append("." + XHtmlColor.PrefixeCssFond + this.Id.ToString() + ", .cssdivalto ." + XHtmlColor.PrefixeCssFond + this.Id.ToString());
			r.Append(" { ");
			if (s != "")
			{
				r.Append("background-color:rgb(");
				r.Append(s);
			}
			//r.Append(" !important");
			r.Append(";");
			r.Append("}");




			return r.ToString();
		}

		/// <summary>
		/// Création d'une XHtmlCouleur
		/// </summary>
		internal static void Create(DVBuffer buffer,HtmlGlobal html)
		{
		
			XHtmlColor color = new XHtmlColor();

			html.App.CouleursCss.Add(color);

			buffer.Get(out color.Id);
			

			ProprietesWpf property;
			buffer.Get(out property);
			while (property != ProprietesWpf.COULEUR_CREATION_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.COULEUR_RVB:		// Couleur : Rouge (byte); Vert (byte); Bleu (byte)
						byte a, r, g, b;
						color.Vide = false;
						buffer.Get(out a);
						buffer.Get(out r);
						buffer.Get(out g);
						buffer.Get(out b);
						color.A = a;
						color.R = r;
						color.G = g;
						color.B = b;
						color.Transparente = (a == 0);

						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Color, property.ToString());
				}
				buffer.Get(out property);
			}
			color.Css = color.GenererCss();
		}
	}

	[DataContract]
	public class XHtmlImageFile
	{
		[DataMember]
		public bool Transmi = false;
		[DataMember]
		internal string FileName;
		internal byte Type { get; set; }
		internal int IconIndex { get; set; }
		internal bool IsSmallIcon { get; set; }
		public string Css;

		#region Lecture propriétés
		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		internal void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.IMAGE_FICHIER_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.IMAGE_FICHIER_TYPE:								// Type d’image (byte)
						byte type;
						buffer.Get(out type);
						Type = type;
						break;

					case ProprietesWpf.IMAGE_FICHIER_NOM:								// Nom du fichier image (string)
						string fileName;
						buffer.GetString(out fileName);
						FileName = fileName;
						break;

					case ProprietesWpf.IMAGE_FICHIER_INDICE:							// Indice d’icône (int)
						int iconIndex;
						buffer.Get(out iconIndex);
						IconIndex = iconIndex;
						break;

					case ProprietesWpf.IMAGE_FICHIER_SMALL_ICON:						// petite icône (byte = 0 ou 1)
						byte iconSize;
						buffer.Get(out iconSize);
						IsSmallIcon = (iconSize == 1);
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.ImageFile, property.ToString());
				}

				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés

		public string GenererCss()
		{
			StringBuilder r = new StringBuilder();
			// ca bazard, c'est pour donner plus de poids a mon icone css
			r.Append(".ui-state-default.ui-icon.icodivalto_" + this.FileName.Replace('.', '_') + "{"); // 
		//	r.Append(".icodivalto_" + this.FileName.Replace('.', '_') + "{"); // 
			r.Append("background-image: url(" + this.FileName + ");");
			r.Append("background-position: 0;");
			r.Append("}");
			return r.ToString();
		}


	}



}
