
using System;
using System.Collections.ObjectModel;

/*
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
*/
using System.Windows.Media;

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
//using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;

namespace Divaltohtml
{
	[DataContract]
	public class FrameworkElement
	{
		public string ToolTip;
		public double Width;
		public double Height;
		public int ActualHeight;
		public double ActualWidth;
		// !!!!!!!!!!!!!! voir si elle sert
	}

	public class XHtmlPresentation
	{
		internal Cadrage Cadrage;
		internal Visibilites? Visibilite;
		internal double? VerticalPadding, HorizontalPadding;
		internal double? Left, Top, OriginalWidth, OriginalHeight;
		internal bool AttachementDroite, AttachementBas;						// nécessairement public pour les radioBoutons qui doivent pouvoir récupérer les valeurs du groupe parent
		internal bool LargeurVariable, HauteurVariable;							// nécessairement public pour les cadres et les traits

		private readonly FrameworkElement frameworkElement;
		private XHtmlFont font;
		private Brush background, stroke;							// Stroke pour l'objet Cadre uniquement
//		private SolidColorBrush borderBrush;
		private double? borderThickness;
		private int codePageBulle;
		public  ushort? angle;						// bh : je le met ici et non pas dans l'objet
		public ushort? idPolice;
		public ushort? idFond;
		public ushort? idBordure;
		public ushort? idPadding;

		internal uint GridId;
		internal ushort CellIndex;


		#region Constructeur
		/// <summary>
		/// Initializes a new instance of the XHtmlPresentation class.
		/// </summary>
		public XHtmlPresentation()
		{
			frameworkElement = new FrameworkElement();
		}

		public XHtmlPresentation(FrameworkElement element)
		{
			frameworkElement = element;
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

			ushort id; // variables temporaires pour les récupérations de couleurs/bordures/polices

			buffer.Get(out property);
			while (property != ProprietesWpf.PRESENTATION_FIN)
			{
				switch (property)
				{
					#region Présentation

					case ProprietesWpf.PRESENTATION_GRILLE_MERE:				// Ident unique de la grille (unint) + Numéro de la cellule (début à 0)
						uint gridId;
						ushort cellIndex;
						buffer.Get(out gridId);
						buffer.Get(out cellIndex);
						GridId = gridId;
						CellIndex = cellIndex;
						break;

					case ProprietesWpf.PRESENTATION_POSITION:					// Position en x (ushort) et position en y (ushort)
						ushort left, top;
						buffer.Get(out left);
						buffer.Get(out top);
						Left = left;
						Top = top;
						break;

					case ProprietesWpf.PRESENTATION_TAILLE:					// Largeur (ushort) et hauteur (ushort)
						ushort width, height;
						buffer.Get(out width);
						buffer.Get(out height);
						OriginalWidth = width;
						OriginalHeight = height;
						break;

					case ProprietesWpf.PRESENTATION_VISIBILITE:				// Visibilité (byte)
						byte visibilite;
						buffer.Get(out visibilite);
						Visibilite = (Visibilites)visibilite;
						SetVisibility();
						break;

					case ProprietesWpf.PRESENTATION_CADRAGE:					// Alignement du contenu (byte)
						byte cadrageCode;
						buffer.Get(out cadrageCode);
						Cadrage = (Cadrage)cadrageCode;
						SetCadrage();
						break;

					case ProprietesWpf.CODE_PAGE_BULLE:							// Code page bulle
						buffer.Get(out codePageBulle);
						break;

					case ProprietesWpf.PRESENTATION_BULLE:						// Contenu de l'infobulle (string)
						string toolTip;
						buffer.GetStringCP(out toolTip, codePageBulle);
						frameworkElement.ToolTip = string.IsNullOrEmpty(toolTip) ? null : toolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;

					case ProprietesWpf.PRESENTATION_ATTACHEMENT_DROITE:	// (Uniquement si attachement à droite)
						AttachementDroite = true;
						break;

					case ProprietesWpf.PRESENTATION_ATTACHEMENT_BAS:		// (Uniquement si attachement en bas)
						AttachementBas = true;
						break;

					case ProprietesWpf.PRESENTATION_LARGEUR_EXTENSIBLE:	// (Uniquement si largeur variable)
						LargeurVariable = true;
						break;

					case ProprietesWpf.PRESENTATION_HAUTEUR_EXTENSIBLE:	// (Uniquement si hauteur variable)
						HauteurVariable = true;
						break;
					#endregion Présentation

					#region Style
					case ProprietesWpf.PRESENTATION_TRAIT:						// Couleur du Cadre (ushort)
						buffer.Get(out id);
//!!!!!!!!!!!!!						stroke = Application.Current.Resources["Brush-" + id] as SolidColorBrush;
						break;

					case ProprietesWpf.PRESENTATION_FOND:						// Couleur du Fond (ushort)
						buffer.Get(out id);
						idFond = id;
//!!!!!!!!!!!!!!!						background = Application.Current.Resources["Brush-" + id] as SolidColorBrush;
						break;

					case ProprietesWpf.PRESENTATION_BORDURE:					// Bordure (ushort)
						buffer.Get(out id);
						idBordure = id;
//!!!!!!!!!!!!!						borderBrush = Application.Current.Resources["BorderBrush-" + id] as SolidColorBrush;
//!!!!!!!!!!!!!						borderThickness = Application.Current.Resources["BorderThickness-" + id] as double?;
						break;

					case ProprietesWpf.PRESENTATION_PADDING:					// Bordure (ushort)
						buffer.Get(out id);
						idPadding = id;
//!!!!!!!!!!!!						VerticalPadding = Application.Current.Resources["VerticalPadding-" + id] as double?;
//!!!!!!!!!!!!						HorizontalPadding = Application.Current.Resources["HorizontalPadding-" + id] as double?;
						break;

					case ProprietesWpf.PRESENTATION_POLICE:					// Police (ushort)
						buffer.Get(out id);
						idPolice = id;
						//!!!!!!!!!!!!							font = Application.Current.Resources["Font-" + id] as XHtmlFont;
						break;
					#endregion Style

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Presentation, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


		#region Affectation des propriétés à l'objet parent
		/// <summary>
		/// Sets the properties of the parent object (layout/style)
		/// </summary>
		internal void SetProperties()
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//XHtmlPage page = frameworkElement is PasswordBox ? ((IXHtmlObject)frameworkElement.DataContext).Page : ((IXHtmlObject)frameworkElement).Page;

			//#region fond / bordure
			//if (background != null) frameworkElement.SetValue(Control.BackgroundProperty, background);
			//else frameworkElement.ClearValue(Control.BackgroundProperty);

			//if (borderBrush != null) frameworkElement.SetValue(Control.BorderBrushProperty, borderBrush);
			//else frameworkElement.ClearValue(Control.BorderBrushProperty);

			//if (borderThickness != null) frameworkElement.SetValue(Control.BorderThicknessProperty, new Thickness(borderThickness.Value));
			//else frameworkElement.ClearValue(Control.BorderThicknessProperty);

			//// cas spécial des traits / cadres
			//if (stroke != null) frameworkElement.SetValue(Shape.StrokeProperty, stroke);
			//else frameworkElement.ClearValue(Shape.StrokeProperty);
			//#endregion fond / bordure

			//#region police
			//if (font != null)
			//{
			//   if (font.Family != null) frameworkElement.SetValue(Control.FontFamilyProperty, font.Family);
			//   else frameworkElement.ClearValue(Control.FontFamilyProperty);

			//   if (font.Size != null) frameworkElement.SetValue(Control.FontSizeProperty, font.Size.Value);
			//   else frameworkElement.ClearValue(Control.FontSizeProperty);

			//   if (font.Style != null) frameworkElement.SetValue(Control.FontStyleProperty, font.Style.Value);
			//   else frameworkElement.ClearValue(Control.FontStyleProperty);

			//   if (font.Weight != null) frameworkElement.SetValue(Control.FontWeightProperty, font.Weight.Value);
			//   else frameworkElement.ClearValue(Control.FontWeightProperty);

			//   if (font.Decorations != null) frameworkElement.SetValue(TextBlock.TextDecorationsProperty, font.Decorations);
			//   else frameworkElement.ClearValue(TextBlock.TextDecorationsProperty);

			//   if (font.Brush != null) frameworkElement.SetValue(Control.ForegroundProperty, font.Brush);
			//   else frameworkElement.ClearValue(Control.ForegroundProperty);

			//}
			//#endregion police

			//#region Padding
			//frameworkElement.ClearValue(Control.PaddingProperty);
			//Thickness padding = ((Thickness)frameworkElement.GetValue(Control.PaddingProperty));
			//if (VerticalPadding.HasValue) padding.Top = padding.Bottom = VerticalPadding.Value;
			//if (HorizontalPadding.HasValue) padding.Left = padding.Right = HorizontalPadding.Value;
			//frameworkElement.SetValue(Control.PaddingProperty, padding);
			//#endregion Padding

			//#region Taille + Largeur / Hauteur Extensible
			//frameworkElement.Width = OriginalWidth;
			//frameworkElement.Height = OriginalHeight;

			//// initialisation lors du premier affichage de la page :
			//if (StretchWidth) frameworkElement.Width = Math.Max(OriginalWidth, OriginalWidth + page.ActualWidth - page.OriginalWidth);
			//if (StretchHeight) frameworkElement.Height = Math.Max(OriginalHeight, OriginalHeight + page.ActualHeight - page.OriginalHeight);

			//// Largeur / Hauteur variable
			//if (StretchWidth || StretchHeight)
			//{
			//   page.SizeChanged -= SizeChangedHandler; // garde-fou pour ne pas dupliquer les handlers
			//   page.SizeChanged += SizeChangedHandler;
			//}
			//#endregion Taille + Largeur / Hauteur Extensible

			//if (StickRight) Canvas.SetRight(frameworkElement, page.OriginalWidth - Left - OriginalWidth);
			//else Canvas.SetLeft(frameworkElement, Left);
			//if (StickBottom) Canvas.SetBottom(frameworkElement, page.OriginalHeight - Top - OriginalHeight);
			//else Canvas.SetTop(frameworkElement, Top);
		}
		#endregion Affectation des propriétés à l'objet parent


		#region Fonctions
		/// <summary>
		/// Sets the object's Visibility and IsEnable properties
		/// </summary>
		private void SetVisibility()
		{
			int i = 0;
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//switch (Visibilite) // 0 = visible, 1 = grisé, 2 = illisible, 3 = caché
			//{
			//   case Visibilite.Visible:
			//      frameworkElement.IsEnabled = true;
			//      frameworkElement.Visibility = Visibility.Visible;
			//      break;

			//   case Visibilite.Grise:
			//      frameworkElement.IsEnabled = false;
			//      frameworkElement.Visibility = Visibility.Visible;
			//      break;

			//   case Visibilite.Illisible:
			//      frameworkElement.IsEnabled = false;
			//      frameworkElement.Visibility = Visibility.Visible;
			//      break;

			//   case Visibilite.Cache:
			//      frameworkElement.IsEnabled = false;
			//      frameworkElement.Visibility = Visibility.Hidden;
			//      break;
			//}
		}

		/// <summary>
		/// Sets the object's TextAlignment property
		/// </summary>
		public void SetCadrage()
		{
			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			//switch (Cadrage)
			//{
			//   case Cadrage.Defaut:
			//      frameworkElement.ClearValue(TextBox.TextAlignmentProperty);
			//      frameworkElement.ClearValue(Control.VerticalContentAlignmentProperty);
			//      break;

			//   case Cadrage.Gauche:
			//      frameworkElement.SetValue(TextBox.TextAlignmentProperty, TextAlignment.Left);
			//      break;

			//   case Cadrage.Droite:
			//      frameworkElement.SetValue(TextBox.TextAlignmentProperty, TextAlignment.Right);
			//      break;

			//   case Cadrage.Centre:
			//      frameworkElement.SetValue(TextBox.TextAlignmentProperty, TextAlignment.Center);
			//      frameworkElement.SetValue(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center);
			//      break;
			//}
		}
		#endregion Fonctions


		/// <summary>
		/// génération des commandes pour le navigateur
		/// </summary>
		/// <param name="pvs"></param>
		/// <param name="html"></param>
		/// <returns></returns>
		public string GenererHtml(ListeParametresEnvoyes pvs,HtmlGlobal html,int codePage,bool ignorerCorrectifCausePadding)
		{
			string ret = "";
			string classes = "";
			int offsetX=0, offsetY=0;

// version ou on crée une commande par propriété
///*
			UnParametreEnvoye pv;
			XHtmlBorder wbordure= null;
			XHtmlPadding wpadding = null;

			if (this.idBordure.HasValue)
				wbordure = html.App.BorduresCss.FirstOrDefault(e => e.Id == this.idBordure);

			if (this.idPadding.HasValue)
				wpadding = html.App.PaddingCss.FirstOrDefault(e => e.Id == this.idPadding);


			pv = new UnParametreEnvoye();
			pv.commande = "css-position";
			pv.valeur = "absolute";
			pvs.commandes.Add(pv);

			// avant tout les positionnements
			if (this.AttachementBas || this.AttachementDroite || this.LargeurVariable || this.HauteurVariable)
			{
				pv = new UnParametreEnvoye();
				pv.commande = "attachements";
				pv.valeur = (AttachementBas ? "1" : "0") + "," +
								(AttachementDroite ? "1" : "0") + "," +
								(HauteurVariable ? "1" : "0") + "," +
								(LargeurVariable ? "1" : "0");

				pvs.commandes.Add(pv);
			}





			// A FINALISER !!!!!!!!!!!!!!!!
			// 
			// En HTML les bordures et paddings sont à l'extérieur de la boite xwin
			// en wpf c'est l'inverse, la boite de xwin contient tout

			// HAUTEUR
			//------------------------------------
			if (this.OriginalHeight.HasValue)
			{
				double? val = this.OriginalHeight;
				pv = new UnParametreEnvoye();
				pv.commande = "css-hauteur";
				//pv.valeur = this.OriginalHeight.ToString();
				pvs.commandes.Add(pv);

				//----------------------------------------------------------------
				// - traitement padding et border
				if (wbordure != null && wbordure.Vide == false && wbordure.Epaisseur != 0)
				{
					val -= (wbordure.Epaisseur * 2);
					offsetX += (int)wbordure.Epaisseur;
					offsetY += (int)wbordure.Epaisseur;
				}

				if (ignorerCorrectifCausePadding == false)
				{
					if (wpadding != null && wpadding.Vide == false && wpadding.Vertical != 0)
					{
						val -= (wpadding.Vertical * 2);
						offsetY += (int)wpadding.Vertical;
					}
				}
				//----------------------------------------------------------------
				
				pv.valeur = val.ToString();
				// modifie également line-heigth coté navigazteur
			}

			// LARGEUR
			//------------------------------------
			if (this.OriginalWidth.HasValue)
			{
				double? val = this.OriginalWidth;

				pv = new UnParametreEnvoye();
				pv.commande = "css-largeur";
				pv.valeur = this.OriginalWidth.ToString();
				pvs.commandes.Add(pv);
				//--------------------------------------------------------------------------------
				if (wbordure != null && wbordure.Vide == false && wbordure.Epaisseur != 0)
					val -= (wbordure.Epaisseur * 2);

				if (ignorerCorrectifCausePadding == false)
				{
					if (wpadding != null && wpadding.Vide == false && wpadding.Horizontal != 0)
						val -= (wpadding.Horizontal * 2);
				}
				//--------------------------------------------------------------------------------
				pv.valeur = val.ToString();

			}


			// POSITION XY
			//------------------------------------
			if (this.Top.HasValue || this.Left.HasValue)
			{
				UnParametreEnvoyeAvecCompl pvc = new UnParametreEnvoyeAvecCompl();
				pvc.commande = "css-xy";
				offsetX = 0;
				offsetY = 0;
					

				pvc.valeur = (this.Left + offsetX)  .ToString();
				pvc.compl  = (this.Top  + offsetY)  .ToString();
				pvs.commandes.Add(pvc);
			}


			// ANGLE
			//--------------------------------------
			if (this.angle.HasValue)
			{
				UnParametreEnvoye pvc = new UnParametreEnvoye();
				pvc.commande = "css-angle";

				if (angle == 0)
					pvc.valeur = this.angle.ToString() + "deg";
				else
				{
					pvc.valeur = (this.angle * -1).ToString() + "deg";
				}
				pvs.commandes.Add(pvc);
			}


			// POLICE
			//--------------------------------------
			if (idPolice.HasValue)
			{
				XHtmlFont  i;
				i = html.App.PolicesCss.Find(e => e.Id == idPolice);
				if (i != null)
				{
					// classes += XHtmlFont.PrefixeCss + idPolice.ToString() + " ";
					UnParametreEnvoye pvc = new UnParametreEnvoye();
					pvc.commande = "css-police";
					if (i.Vide)
						pvc.valeur = "xxxx";
					else
						pvc.valeur = XHtmlFont.PrefixeCss + idPolice.ToString();
					pvs.commandes.Add(pvc);
				}
			}

			// FOND
			//------------------------------------------
			if (idFond.HasValue)
			{
				UnParametreEnvoye pvc = new UnParametreEnvoye();
				pvc.valeur = html.App.GenererUneValeurDeCouleur(idFond);
				if (pvc.valeur != null)
				{
					pvc.commande = "css-fond";
					pvs.commandes.Add(pvc);
				}

				//if (i != null && i.Vide == false && i.Transparente == false)
				//{
				//   classes += XHtmlColor.PrefixeCssFond + idFond.ToString() + " ";
				//}
			}

			// BORDURE
			//--------------------------------------------
			if (idBordure.HasValue)
			{
				XHtmlBorder i;
				i = html.App.BorduresCss.Find(e => e.Id == idBordure);
				if (i != null)
				{
					UnParametreEnvoye pvc = new UnParametreEnvoye();
					pvc.commande = "css-bordure";
					if (i.Vide)
						pvc.valeur = "xxxx";
					else
						pvc.valeur = XHtmlBorder.PrefixeCss + idBordure.ToString();
					pvs.commandes.Add(pvc);

				}
//				if (i != null && i.Vide == false)
//					classes += XHtmlBorder.PrefixeCss + idBordure.ToString() + " ";
			}


			// PADDING
			//----------------------------------------------
			if (idPadding.HasValue)
			{
				XHtmlPadding i;
				i = html.App.PaddingCss.Find(e => e.Id == idPadding);
				if (i != null)
				{
					UnParametreEnvoye pvc = new UnParametreEnvoye();
					pvc.commande = "css-padding";
					if (i.Vide)
						pvc.valeur = "xxxx";
					else
						pvc.valeur = XHtmlPadding.PrefixeCss + idPadding.ToString() + " ";
					pvs.commandes.Add(pvc);
					//	classes += XHtmlPadding.PrefixeCss + idPadding.ToString() + " ";
				}
			}

			if (codePage != 0)
			{
				pv = new UnParametreEnvoye();
				pv.commande = "code-page";
				pv.valeur = codePage.ToString();
				pvs.commandes.Add(pv);
			}


			// INFO BULLE
			//--------------------------------------------------
			if (this.frameworkElement.ToolTip != null)
			{
				pv = new UnParametreEnvoye();
				pv.commande = "info-bulle";
				pv.valeur = this.frameworkElement.ToolTip;
				pvs.commandes.Add(pv);
			}

			// Visibilite
			//---------------------------------------------------
			if (Visibilite.HasValue)
			{
				pv = new UnParametreEnvoye();
				pv.commande = "visibilite";
				pv.valeur = Visibilite.ToString();
				pvs.commandes.Add(pv);
			}



//*/

		///////////////////////////////////////////////////////
			//////////////////////////////////////// MORCEAU qui génére tout le code HTML : beaucoup plus rapide a l'exec
			/*	
					ret = "style='";
					// ret += "font-size:50pt;";

					ret += "position:absolute;";
					ret += "top:" + this.Top.ToString() + "px;";
					ret += "left:" + this.Left.ToString() + "px;";
					ret += "width:" + this.OriginalWidth.ToString() + "px;";
					ret += "height:" + this.OriginalHeight.ToString() + "px;";

					if (this.angle.HasValue)
					{
						ret += "transform:rotate(" + angle.ToString() + "deg)";
					}


					ret += "'";
					*/

			if (classes.Length > 0)
			{
				UnParametreEnvoye pvc = new UnParametreEnvoye();
				pvc.commande = "css-police-fond-padding";
				pvc.valeur = classes;
				pvs.commandes.Add(pvc);
			}
			return ret;
			//////////////////////////////////////////////
//			return ret;
		}

	}



	public interface IXHtmlObject
	{
		uint Id { get; set; }
		XHtmlPage Page { get; set; }
		XHtmlPresentation Presentation { get; set; }

		void ReadProperties(DVBuffer buffer);

		void AjouterEnvoisUnObjet(ListeParametresEnvoyes envois,XHtmlPage page,int niveau);
	}

	public interface IXHtmlEditableObject : IXHtmlObject
	{
		ushort SeqPoint { get; set; }
		Collection<string> ListOfValidButtons { get; }
	}

	internal interface IXHtmlDataGridCell
	{
		XHtmlPresentationCell Presentation { get; set; }
//		Brush  Background { get; set; }  //!!!!!!!!!!!!!!!!!!!!!!!! Brush
//		Brush Foreground { get; set; }	//!!!!!!!!!!!!!!!!!!!!!!!! Brush
		string StringValue { get; set; }

		void ReadProperties(DVBuffer buffer);
		void SetColors();
		void SetAlternationBackground();
	}

	public interface IXHtmlDataGridColumn : IXHtmlEditableObject
	{
		ColumnType ColumnType { get; set; }
		XHtmlDataGrid DataGrid { get; set; }
		XHtmlDataGridColumnHeader ColumnHeader { get; set; }
		string ToolTip { get; set; }
		ushort ColumnBackground { get; set; }
		ushort ColumnFont { get; set; }
		string RecordName { get; set; }
		string DataName { get; set; }
		int[] Indexes { get; set; }
		bool IsMandatory { get; set; }
		bool IsHiddenForced { get; set; }
		int Width { get; set; }

	}

}