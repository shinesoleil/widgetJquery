//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlTextBoxCell.cs
// Description : Objet Champ pour description de cellule texte dans un tableau
//___________________________________________________________________________

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using System;
using Divaltohtml;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	class XHtmlGenericCell : IXHtmlDataGridCell
	{
		public int Indice;
		public XHtmlPresentationCell Presentation { get; set; }
		public string StringValue { get { return rowText; } set { } }

		internal int CodePage;

		private readonly XHtmlGenericColumn column;
		private string text, rowText;
		public bool ValeurSpecifiee;
		private string Text;
		private ushort MaxLength;
		private string myToolTip;
		private TextAlignment myTextAlignment;


		#region Constructeur
		static XHtmlGenericCell()
		{
			//// fond de cellule transparent par défaut
			//BackgroundProperty.OverrideMetadata(typeof(XHtmlTextBoxCell), new FrameworkPropertyMetadata(Brushes.Transparent));
			////BackgroundProperty.OverrideMetadata(typeof(XHtmlTextBoxCell), new FrameworkPropertyMetadata(Application.Current.Resources["ContentBackgroundBrush"]));
			//ForegroundProperty.OverrideMetadata(typeof(XHtmlTextBoxCell), new FrameworkPropertyMetadata(Application.Current.Resources["DefaultForegroundBrush"]));
			//FontFamilyProperty.OverrideMetadata(typeof(XHtmlTextBoxCell), new FrameworkPropertyMetadata(Application.Current.Resources["DefaultFontFamily"]));
			//FontSizeProperty.OverrideMetadata(typeof(XHtmlTextBoxCell), new FrameworkPropertyMetadata(Application.Current.Resources["DefaultFontSize"]));
		}

		/// <summary>
		/// Initializes a new instance of the XHtmlTextBoxCell class.
		/// </summary>
		public XHtmlGenericCell(DataGridColumn column)   // !!!!!!!!!DataGridColumn
		{
			Presentation = new XHtmlPresentationCell();

//			this.column = column as XHtmlTextBoxColumn;

			SetStyle();
		}

		public XHtmlGenericCell()   // !!!!!!!!!DataGridColumn
		{
			Presentation = new XHtmlPresentationCell();

//			this.column = column as XHtmlTextBoxColumn;
//			SetStyle();
		}

		
		#endregion Constructeur


		#region Lecture propriétés
		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		public void ReadProperties(DVBuffer buffer)
		{
			if (buffer == null) throw new ArgumentNullException("buffer");

			ProprietesWpf property;

			buffer.Get(out property);
			while (property != ProprietesWpf.CHAMP_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.CELLULE_PRESENTATION_DEBUT:								// Début présentation
						Presentation.ReadProperties(buffer);
						SetStyle();
						break;

					case ProprietesWpf.CODE_PAGE:														// Code page
						buffer.Get(out CodePage);
						break;

					case ProprietesWpf.CHAMP_VALEUR:													// Contenu du champ (string)
						buffer.GetStringCP(out text, CodePage);
				//		if ((!Presentation.VisibilityFlag || Presentation.Visibilite != Visibilite.Illisible) && column.Presentation.Visibilite != Visibilite.Illisible)
							Text = text;
						rowText = text;
						ValeurSpecifiee = true;
						break;

					case ProprietesWpf.CHAMP_VALEUR_SANS_FORMAT:									// donnée non-formatée (string)
						buffer.GetString(out rowText);
						ValeurSpecifiee = true;
						break;

					case ProprietesWpf.CHAMP_LONGUEUR_SAISIE:										// Longueur de saisie autorisée (ushort)
						ushort maxLength;
						buffer.Get(out maxLength);
						MaxLength = maxLength;
						break;

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGridTextBoxCell, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Fonctions
		//public void SetValue(string s)
		//{
		//   rowText = text = s;
		//}

		public void SetStyle()
		{
			return;
			SetColors();
			SetFont();

			myToolTip = (Presentation.ToolTipFlag) ? Presentation.ToolTip : column.ToolTip;
			myTextAlignment = (Presentation.TextAlignmentFlag) ? Presentation.TextAlignment : column.TextAlignment;

			if (Presentation.VisibilityFlag)
			{
				myVisibility = Presentation.Visibility;
				IsEnabled = column.IsEnabled && Presentation.IsEnabled;
				Text = (Presentation.Visibilite == Visibilites.Illisible) ? Application.Current.Resources["UnreadableString"] as string : text;
			}
			else
			{
				myVisibility = Visibility.Visible;
				IsEnabled = column.IsEnabled;
				Text = (column.Presentation.Visibilite == Visibilites.Illisible) ? Application.Current.Resources["UnreadableString"] as string : text;
			}
		}

		public void SetColors()
		{
			/* !!!!!!!!!!! a revoir
			Background = Application.Current.Resources[(Presentation.BackgroundFlag) ? Presentation.Background : column.ColumnBackground] as Brush;
			if (Background == null) ClearValue(BackgroundProperty);

			XHtmlFont columnFont = Application.Current.Resources[column.ColumnFont] as XHtmlFont;
			Debug.Assert(columnFont != null, "XHtmlTextBoxCell - column.ColumnFont.Brush non-chargée (ressource à null)");

			if (Presentation.FontFlag && Presentation.Font.Brush != null) Foreground = Presentation.Font.Brush;
			else if (columnFont != null && columnFont.Brush != null) Foreground = columnFont.Brush;
			else ClearValue(ForegroundProperty);
			 * */
		}

		public void SetFont()
		{
			/* !!!!!!!!!!!!!!!!!!!!!  a revoir
			XHtmlFont columnFont = Application.Current.Resources[column.ColumnFont] as XHtmlFont;
			Debug.Assert(columnFont != null, "XHtmlTextBoxCell - column.ColumnFont non-chargée (ressource à null)");

			if (Presentation.FontFlag && Presentation.Font.Family != null) FontFamily = Presentation.Font.Family;
			else if (columnFont != null && columnFont.Family != null) FontFamily = columnFont.Family;
			else ClearValue(FontFamilyProperty);

			if (Presentation.FontFlag && Presentation.Font.Size != null) FontSize = Presentation.Font.Size.Value;
			else if (columnFont != null && columnFont.Size != null) FontSize = columnFont.Size.Value;
			else ClearValue(FontSizeProperty);

			if (Presentation.FontFlag && Presentation.Font.Style != null) FontStyle = Presentation.Font.Style.Value;
			else if (columnFont != null && columnFont.Style != null) FontStyle = columnFont.Style.Value;
			else ClearValue(FontStyleProperty);

			if (Presentation.FontFlag && Presentation.Font.Weight != null) FontWeight = Presentation.Font.Weight.Value;
			else if (columnFont != null && columnFont.Weight != null) FontWeight = columnFont.Weight.Value;
			else ClearValue(FontWeightProperty);

			if (Presentation.FontFlag && Presentation.Font.Decorations != null) TextDecorations = Presentation.Font.Decorations;
			else if (columnFont != null && columnFont.Decorations != null) TextDecorations = columnFont.Decorations;
			else ClearValue(TextDecorationsProperty);
			 * */
		}

		/// <summary>
		/// Sets the background color depending on the row alternation (normal or darkened color)
		/// </summary>
		public void SetAlternationBackground()
		{
			/* revoir !!!!!!!!!!!!!!!!!
			if (Background == Brushes.Transparent) return; // optimisation: pas la peine de zoner un fond transparent (cas où le fond de la ligne est visible)
			Background = new SolidColorBrush { Color = XHtmlColor.DarkenColor(((SolidColorBrush)Background).Color) };
			Background.Freeze();
			 * */
		}
		#endregion Fonctions



		public Visibility myVisibility { get; set; }

		public bool IsEnabled { get; set; }
	}
}

