//___________________________________________________________________________
// Projet		 : XWPF
// Nom			 : XHtmlPresentationCell.cs
// Description : Présentation de cellule tableau
//___________________________________________________________________________

using System.Windows;

using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;

namespace Divaltohtml
{
	public class XHtmlPresentationCell
	{
		public bool OnALuQuelqueChose = false;

		internal Cadrage    Cadrage;
		internal bool TextAlignmentFlag;
		internal TextAlignment TextAlignment;
		
//		internal string Background;
	
		public ushort? idFond;
		internal bool BackgroundFlag;

		public ushort idPolice;
		internal bool FontFlag;

		internal string ToolTip;
		internal bool ToolTipFlag;
		internal int CodePageBulle;

		internal bool IsEnabled;

		internal Visibilites? Visibilite; //  = Visibilite.Aucun;
		internal Visibility Visibility;
		internal bool VisibilityFlag;
	


		#region Lecture propriétés
		/// <summary>
		/// Reads the object's properties from the buffer
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		internal void ReadProperties(DVBuffer buffer)
		{
			ProprietesWpf property;

			byte flag; // flag pour chaque propriété, indique quelle valeur prendre pour la propriété concernée (0 / 1)
			ushort id; // variables temporaires pour les récupérations de couleurs/bordures/polices

			OnALuQuelqueChose = true;

			buffer.Get(out property);
			while (property != ProprietesWpf.CELLULE_PRESENTATION_FIN)
			{
				switch (property)
				{
					#region Fond
					case ProprietesWpf.CELLULE_PRESENTATION_FOND:						// Couleur du Fond (ushort)
						buffer.Get(out id);
						this.idFond = id;
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_DYN_FOND:
						buffer.Get(out flag);
						BackgroundFlag = (flag == 1);
						break;
					#endregion Fond

					#region Police
					case ProprietesWpf.CELLULE_PRESENTATION_POLICE:					// Police (ushort)
						buffer.Get(out id);
						idPolice = id; // Application.Current.Resources["Font-" + id] as XHtmlFont;
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_DYN_POLICE:
						buffer.Get(out flag);
						FontFlag = (flag == 1);
						break;
					#endregion Police

					#region Bulle
					case ProprietesWpf.CODE_PAGE_BULLE:										// Code page bulle
						buffer.Get(out CodePageBulle);
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_BULLE:						// Contenu de l'infobulle (string)
						buffer.GetStringCP(out ToolTip, CodePageBulle);
						ToolTip = ToolTip.Replace("|", "\n"); // "|" = multi-ligne
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_DYN_BULLE:
						buffer.Get(out flag);
						ToolTipFlag = (flag == 1);
						break;
					#endregion Bulle

					#region Visibilité
					case ProprietesWpf.CELLULE_PRESENTATION_VISIBILITE:				// Visibilité (byte)
						byte visibilite;
						buffer.Get(out visibilite);
						Visibilite = (Visibilites)visibilite;
						Visibility = (Visibilite == Visibilites.Cache) ? Visibility.Hidden : Visibility.Visible;
						IsEnabled = (Visibilite != Visibilites.Grise);
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_DYN_VISIBILITE:
						buffer.Get(out flag);
						VisibilityFlag = (flag == 1);
						break;
					#endregion Visibilité

					#region Cadrage
					case ProprietesWpf.CELLULE_PRESENTATION_CADRAGE:					// Alignement du contenu (byte)
						byte cadrageCode;
						buffer.Get(out cadrageCode);
						Cadrage = (Cadrage)cadrageCode;
						switch (Cadrage)
						{
							case Cadrage.Defaut: // Default = Left
							case Cadrage.Gauche: TextAlignment = TextAlignment.Left; break;
							case Cadrage.Droite: TextAlignment = TextAlignment.Right; break;
							case Cadrage.Centre: TextAlignment = TextAlignment.Center; break;
						}
						break;

					case ProprietesWpf.CELLULE_PRESENTATION_DYN_CADRAGE:
						buffer.Get(out flag);
						TextAlignmentFlag = (flag == 1);
						break;
					#endregion Cadrage

					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.DataGridPresentationCell, property.ToString());
				}
				buffer.Get(out property);
			}
		}
		#endregion Lecture propriétés


	}
}
