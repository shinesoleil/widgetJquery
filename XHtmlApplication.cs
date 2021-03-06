﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using Divalto.Systeme.XHtml;
using System.Runtime.Serialization;
using System.Text;
//using Divalto.Systeme.DHTransportLC;
using System.Windows.Input;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Divalto.XWeb;
using System.Collections.Specialized;
using Divalto.XWeb;
//using MaquetteOnglet;
//using Divalto.Systeme.DHTransportLC;

namespace Divaltohtml
{


	[DataContract]
	public class XHtmlApplication
	{
		internal static List<Key> FnKeys = new List<Key> { Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12, Key.F13, Key.F14, Key.F15, Key.F16 };
		public int SessionWeb;
		public byte[] DonneesRecuesDeYwpf;
		public DVBuffer CodeEcran;		// Masque écran envoyé par YWeb 
		public Page PageWeb;			// la page Web en cours
		//public Control FormulaireWeb;
		//public XHtmlApplication Application;
		//		public Application app = new Application();

		public HtmlGlobal Html;


		public XHtmlApplication()
		{
			Init();
		}

		public void Init()
		{
			StackOfWindows = new Stack<XHtmlWindow>();
			//FenetresADepiler = new Stack<XHtmlWindow>();
			PolicesCss = new List<XHtmlFont>();
			CouleursCss = new List<XHtmlColor>();
			BorduresCss = new List<XHtmlBorder>();
			PaddingCss = new List<XHtmlPadding>();
			ImagesCss = new List<XHtmlImageFile>();
			listOfMenus = new List<XHtmlMenu>();
			listOfToolBars = new List<XHtmlToolBar>();
			interTaskResponses = new Queue<DVBuffer>();
//			ParametresFinaux = new ListeParametresEnvoyes();
			SendWindowhandler = true;
		}


		[OnDeserializing]
		public void OnDeserialisation(StreamingContext c)
		{
			Init();
		}

		public static Dictionary<string, object> Resources;
		//	internal static List<Key> FnKeys = new List<Key> { Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12, Key.F13, Key.F14, Key.F15, Key.F16 };
//		public List<XHtmlPage> ListePages = new List<XHtmlPage>();

		[DataMember]
		internal Stack<XHtmlWindow> StackOfWindows; //  = new Stack<XHtmlWindow>();

//		internal Stack<XHtmlWindow> FenetresADepiler; // = new Stack<XHtmlWindow>();


		[DataMember]
		public List<XHtmlFont> PolicesCss; //  = new List<XHtmlFont>();
		[DataMember]
		public List<XHtmlColor> CouleursCss; // = new List<XHtmlColor>();
		[DataMember]
		public List<XHtmlBorder> BorduresCss; //  = new List<XHtmlBorder>();
		[DataMember]
		public List<XHtmlPadding> PaddingCss; // = new List<XHtmlPadding>();
		[DataMember]
		public List<XHtmlImageFile> ImagesCss; // = new List<XHtmlImageFile>();


		internal DVBuffer AsynchronousResponse;
		internal bool
			AttenteConsult,
			AttenteInput,
			AttentePopupMenu,	
			AttenteGetb,
			IsPrintPreviewEnabled,
			IsPrintPreviewSaved,
			IsPrintPreviewUpdated,
			SendWindowhandler;
		internal string
			PrintPreviewText,
			SavePrintPreviewText,
			PrintCloseAllText,
			CloseAllText;

		private  List<XHtmlMenu> listOfMenus; // = new List<XHtmlMenu>();
		private  List<XHtmlToolBar> listOfToolBars; //  = new List<XHtmlToolBar>();
		private  Queue<DVBuffer> interTaskResponses; // = new Queue<DVBuffer>();
		public bool responseAlreadySent;
		public DVBuffer DVBufferATransmettreImmediatement;

		// private readonly FlashModeHelper flashModeHelper = FlashModeHelper.GetInstance();


		internal void Send(DVBuffer response)
		{
			return; //!!!!!!!!!!!!!!!
			/*
			App appp = (App)Application.Current;
			var app = (App)Application.Current;

			// TODO mettre dans les logs harmony ? Ce message est intéressant car il signale une anomalie qui va mettre le bazar
			if (app.AttenteReponseEnCours)
			{
				// !!!!!!!!!! MessageBox.Show("Attention : Désynchronisation entre Xwpf et Xrtdiva");
				return;
			}

			app.AttenteReponseEnCours = true;
			AttenteConsult = false;
			AttenteInput = false;
			AttentePopupMenu = false;
			AttenteGetb = false;
			*/
//!!!!!!!!!!!			if (consultTimer != null) consultTimer.Stop();	// arrêt du timer le cas échéant
//!!!!!!!!!!!			if (!busyTimer.IsEnabled) busyTimer.Start();		// lancement du timer pour le curseur d'attente le cas échéant.

//!!!!!!!!!!!			XwpfWindow window = StackOfWindows.Peek();	//abandonner le focus si input en cours
//!!!!!!!!!!!			Keyboard.Focus(window.MainCanvas);

//!!!!!!!!!!			window.ActiveControl = null; // garde-fou: on r.à.z le contrôle actif lorsque l'appli à la main (et donc hors input).

			/* !!!!!!!!!!!!!!!!
			if (SendWindowhandler)
			{
				IntPtr hwnd = app.GetTopWindow();
				if (hwnd != (IntPtr)0)
				{
					SendWindowhandler = false;
					response.Put((ushort)ClientLegerProprietes.PROPCLIENTLEGER_HFENETRE);
					response.Put((int)hwnd);
					app.Client.FenetreMere = hwnd;
					app.Client.Tunnel.EcrireHandlerFenetreClient(app.Client.SessionProgrammeCourant, hwnd, app.Client.IdentifiantServeur);
				}
			}
			*/

			//si le menu pour l'impression d'écran a été (dé)coché, on envoie la nouvelle valeur:
			if (IsPrintPreviewUpdated)
			{
				IsPrintPreviewUpdated = false;

				response.Put(ProprietesWpf.MENU_SYSTEME_APERCU_ETAT);
				response.Put((byte)(IsPrintPreviewEnabled ? 1 : 0));	// Flag "aperçu avant impression actif ?" (byte)
				response.Put((byte)(IsPrintPreviewSaved ? 2 : 0));		// Flag "sauver aperçu avant impression ?" (byte)
			}

			response.Put(ProprietesWpf.CLIENTLEGER_FIN);

//!!!!!!!!!!!!!!			app.Client.Transport.BufferEmission = response;
//!!!!!!!!!!!!!!			app.Client.Transport.EnvoyerEtDemanderReveil();
		}


		internal void SendError(XHtmlErrorCodes errorCode, XHtmlErrorLocations errorLocation, string errorParameter)
		{
			var errorBuffer = new DVBuffer();
			errorBuffer.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
			errorBuffer.Put(ProprietesWpf.ERREUR_XWPF);
			errorBuffer.Put((ushort)errorCode);
			errorBuffer.Put((ushort)errorLocation);
			errorBuffer.PutString(errorParameter);
			errorBuffer.PutString("ApplicationVersion");  //!!!!!!!!!!!!!!!!!!!!!!!!!

			// Send(errorBuffer); Il faut d'abord rendre la main au navigateur qui va immédiatement revenir pour emettre ce dvbuffer
			responseAlreadySent = true;

			// je garde le DVBuffer, j'en fait une copie en profondeur
			DVBufferATransmettreImmediatement = new DVBuffer(errorBuffer.LeBuffer, 0, errorBuffer.GetTaille());

		}

		public void GetProperties(DVBuffer buffer)
		{
			string idPage; //stockage temporaire des id page, menuItem et toolbarItem
			ProprietesWpf property;
			XHtmlWindow window;
			XHtmlPage page = null;
			XHtmlDataGrid dataGrid;
			ushort? index = null; //pour renvoi d'un index colonne dans un tableau
			ushort? nbRows = null; //pour renvoi du nombre de lignes affichées dans un tableau
			string horizontalScrollbarTooltip = null; //pour renvoi de la bulle de l'ascenseur horizontal dans un tableau
			string verticalScrollbarTooltip = null; //pour renvoi de la bulle de l'ascenseur vertical dans un tableau
			int codepageBulleAscHor = 0, codepageBulleAscVer = 0;

			buffer.Get(out property);

			while (property != ProprietesWpf.CLIENTLEGER_FIN)
			{
				switch (property)
				{
					case ProprietesWpf.CLIENTLEGER_DEBUT:
						break;

					case ProprietesWpf.MENU_SYSTEME_APERCU_ETAT:
						byte printPreview;
						buffer.Get(out printPreview);
						IsPrintPreviewEnabled = (printPreview == 1);
//						if (StackOfWindows.Any()) StackOfWindows.ToList()[0].UpdatePrintPreviewItem(IsPrintPreviewEnabled);
						break;

					case ProprietesWpf.MENU_SYSTEME_TRADUCTIONS:
						buffer.GetString(out PrintPreviewText);
						buffer.GetString(out CloseAllText);
						buffer.GetString(out PrintCloseAllText);
						buffer.GetString(out SavePrintPreviewText);
						break;

					case ProprietesWpf.COULEUR_CREATION_DEBUT:
						XHtmlColor.Create(buffer,this.Html);
						break;

					case ProprietesWpf.POLICE_CREATION_DEBUT:
						XHtmlFont.Create(buffer,this.Html);
						break;

					case ProprietesWpf.BORDURE_CREATION_DEBUT:
						XHtmlBorder.Create(buffer,this.Html);
						break;

					case ProprietesWpf.PADDING_CREATION_DEBUT:
						XHtmlPadding.Create(buffer,this.Html);
						break;

					#region Fenêtre
					case ProprietesWpf.FENETRE_DEBUT:					// Id Masque (ushort) et Id Page (byte)
						// ne concerne que les "vraies" fenêtres (i.e. : les fenêtres autres que la toute première, ouverte dans app.xaml.cs)

						//Window lastWindow = StackOfWindows.Peek();
						//if (!lastWindow.IsVisible) lastWindow.Show();
						//lastWindow.IsEnabled = false;

						window = new XHtmlWindow(); //  { Owner = lastWindow, ShowInTaskbar = false };
						buffer.GetString(out window.MaskName);
						buffer.Get(out window.PageNum);
						window.ReadProperties(buffer);
						window.Id = window.MaskName.Replace('.', '_') + "_" + window.PageNum.ToString() + "_" + (StackOfWindows.Count + 1).ToString();
						StackOfWindows.Push(window);

						ListeParametresComplementaires lc = new ListeParametresComplementaires();
						lc.Ajouter("ty", window.minContentHeight.ToString());
						lc.Ajouter("tx", window.minContentWidth.ToString());
						lc.Ajouter("idFen", window.Id);
						lc.Ajouter("libelle", window.MaskName);
						lc.Ajouter("numpage", window.PageNum.ToString());

						lc.Ajouter("tailleFixe", (window.ModeResize == System.Windows.ResizeMode.NoResize) ? "true" : "false");
						lc.Ajouter("couleurFond", XHtmlColor.PrefixeCssFond + window.CouleurFond.ToString());
						lc.Ajouter("titre", window.Title);
						if (window.icon != null)
							lc.Ajouter("icone", window.icon.Css);

						string chaineFenetre = HtmlGlobal.ToJsonString(lc, this.Html.JsonParamCompl, false);
						{
							UnParametreEnvoye p = new UnParametreEnvoye();
							p.commande = "ouvrirFenetre";
							this.Html.Envois.commandes.Add(p);
							p.valeur = chaineFenetre;
						}
						break;

					case ProprietesWpf.FENETRE_TITRE:					// Titre de la fenêtre (string)
						{
							string title;
							int codePage;
							buffer.Get(out codePage);
							buffer.GetStringCP(out title, codePage);
							StackOfWindows.Peek().Title = title;

							UnParametreEnvoye p = new UnParametreEnvoye();
							p.commande = "xmeTitle";
							this.Html.Envois.commandes.Add(p);
							p.valeur = title;
						}
						break;

					case ProprietesWpf.FENETRE_ICONE:					// Titre de la fenêtre (string)
						var icon = new XHtmlImageFile();
						icon.ReadProperties(buffer);
						// StackOfWindows.Peek().SetIcon(icon);
						break;

					case ProprietesWpf.FENETRE_TAILLE_INITIALE:		// Taille de référence pour les calculs d'attachement - Hauteur (ushort) x Largeur (ushort)
						window = StackOfWindows.Peek();
						buffer.GetString(out window.MaskName);
						buffer.Get(out window.PageNum);
						window.SetMinContentSize(buffer);

						this.Html.Envois.Ajouter("fenetreTailleInitiale",
								window.MaskName + "," + 
								window.PageNum.ToString() + "," + 
								window.minContentWidth.ToString() + "," +
								window.minContentHeight.ToString()
							);

						break;

					case ProprietesWpf.FENETRE_FERMETURE:				// Pour fermer la fenêtre en cours
						window = StackOfWindows.Pop();

						this.Html.Envois.Ajouter("fermerFenetre", window.Id);

						//FenetresADepiler.Push(window);
						//window.Closing -= window.ClosingHandler;
						//StackOfWindows.Peek().IsEnabled = true; // ne pas remplacer par "window": le StackOfWindow.Peek a changé de valeur à cause du pop() !
						// window.Close();

						break;
					#endregion Fenêtre





					case ProprietesWpf.PAGE_DEBUT:
						buffer.GetString(out idPage); // Id Page (string)

						window = StackOfWindows.Peek();
						page = window.GetPage(idPage);
						if (page == null)
						{
							window.ListOfPages.Add(page = new XHtmlPage(window,this.Html) { Id = idPage });
							this.Html.Envois.Ajouter("nouvellePage", this.Html.CalculerIdPage(page.Id));
							page.JeSuisAffichee = true;
						}
						// une page peut être supprimée (dans la gestion de l'effacement d'une autre page), il faut donc la remettre dans la fenêtre le cas échéant
						//if (!window.MainCanvas.Children.Contains(page)) window.MainCanvas.Children.Add(page);
						if (page.JeSuisAffichee == false)
						{
							this.Html.Envois.Ajouter("remettrePage", this.Html.CalculerIdPage(page.Id));
						}
						// cette ligne était plus bas, mais trop tard
						this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(page.Id), page.StopPoint.ToString(), page.NumPage.ToString());
						page.JeSuisAffichee = true;
						// - lecture de la page et des objets
						page.ReadProperties(buffer);

						// je la remet aussi ici car avec les panels ca bouge !
						this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(page.Id), page.StopPoint.ToString(), page.NumPage.ToString());
						page.EnvoyerCouleurPositionAttachement(false);
						
						// on affiche la fenêtre si c'est la toute première page et si la fenêtre n'est pas masquée (application sans fenêtre)
						// !!!!!!!!!!if (!page.Window.IsVisible && application.Client.FenetreMereHide == 0)
						// !!!!!!!!!!	page.Window.Show();
						break;



					case ProprietesWpf.TABLEAU_DEBUT:								// Mise à jour d'une propriété du tableau

						dataGrid = GetDataGrid(buffer,out page);
						dataGrid.ReadProperties(buffer);
						dataGrid.AjouterEnvoisUnObjet(Html.Envois, page, Html.App.StackOfWindows.Count());
						break;

					case ProprietesWpf.TABLEAU_ASCHOR_A_GAUCHE:					// retour au début de la ligne d'un tableau
						dataGrid = GetDataGrid(buffer,out page);
						// ScrollIntoView sur la colonne dont le DisplayIndex est = FrozenColumnCount pour mettre l'ascenseur à gauche.
						// (en tenant compte des colonnes figées)
						// attention : le tableau peut être vide, d'où le "if Rows.Count > 0"
						var column = dataGrid.Columns.FirstOrDefault(col => col.DisplayIndex == dataGrid.FrozenColumnCount);
						//!!!!!!!!!!if (dataGrid.Rows.Count > 0 && column != null)
						//!!!!!!!!!!	dataGrid.ScrollIntoView(dataGrid.Rows[0], column);
						break;

					case ProprietesWpf.TABLEAU_REMPLISSAGE_DEBUT:				// Remplissage du tableau
						dataGrid = GetDataGrid(buffer,out page);
						dataGrid.Fill(buffer);
						dataGrid.AjouterEnvoisUnObjetRemplissageTableau(Html.Envois, page, Html.App.StackOfWindows.Count());
						break;


					#region Fonctions Get du tableau
					case ProprietesWpf.TABLEAU_ASCHOR_GET_BULLE:					// Demande le texte de la bulle de l'ascenseur horizontal
						dataGrid = GetDataGrid(buffer,out page);
						horizontalScrollbarTooltip = dataGrid.HorizontalScrollBarToolTip;
						codepageBulleAscHor = dataGrid.CodepageBulleAscHor;
						break;

					case ProprietesWpf.TABLEAU_ASCVER_GET_BULLE:					// Demande le texte de la bulle de l'ascenseur vertical
						dataGrid = GetDataGrid(buffer,out page);
						verticalScrollbarTooltip = dataGrid.VerticalScrollBarToolTip;
						codepageBulleAscVer = dataGrid.CodepageBulleAscVer;
						break;

					case ProprietesWpf.TABLEAU_GET_NOMBRE_LIGNES:				// Demande du nombre de lignes affichables dans le tableau
						{
							string id = GetIdentDataGrid(buffer);
							// dataGrid = GetDataGrid(buffer);
							//!!!!!!!!!! dataGrid.UpdateLayout();	// indispensable, sinon le nombre de lignes envoyé au premier affichage du tableau est potentiellement faux (peut poser pb par la suite)
							nbRows = 32; //  dataGrid.GetRowCount();		
							Html.Envois.Ajouter("tabGetNbLig", id);
						}
						break;

					case ProprietesWpf.TABLEAU_GET_COL_SAISIE_PREMIERE:		// Demande le numéro de la première colonne en saisie dans la ligne donnée
						dataGrid = GetDataGrid(buffer,out page);
						index = dataGrid.GetFirstColumnIndex(buffer);
						break;

					case ProprietesWpf.TABLEAU_GET_COL_SAISIE_DERNIERE:		// Demande le numéro de la dernière colonne en saisie dans la ligne donnée
						dataGrid = GetDataGrid(buffer,out page);
						index = dataGrid.GetLastColumnIndex(buffer);
						break;

					case ProprietesWpf.TABLEAU_GET_COL_SAISIE_PRECEDENTE:		// Demande le numéro de la colonne précédente en saisie dans la ligne donnée
						dataGrid = GetDataGrid(buffer,out page);
						index = dataGrid.GetPreviousColumnIndex(buffer);
						break;

					case ProprietesWpf.TABLEAU_GET_COL_SAISIE_SUIVANTE:		// Demande le numéro de la colonne suivante en saisie dans la ligne donnée
						dataGrid = GetDataGrid(buffer,out page);
						index = dataGrid.GetNextColumnIndex(buffer);
						break;
					#endregion Fonctions Get du tableau


					#region Input
					case ProprietesWpf.XMEINPUT:
						AsynchronousResponse = null; // garde-fou
						buffer.GetString(out idPage);
						window = StackOfWindows.Peek();
						window.CurrentPage = window.GetPage(idPage);

					//!!!!!!!!!!!!!!!!!!!!!!! idem pour consult etc....
//						this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(window.CurrentPage.Id), window.CurrentPage.StopPoint.ToString(), page.NumPage.ToString());
						this.Html.Envois.Ajouter("pageCourante", Html.CalculerIdPage(window.CurrentPage.Id), window.CurrentPage.StopPoint.ToString(), window.CurrentPage.NumPage.ToString());


						ReadInput(buffer, window.CurrentPage);
//						busyTimer.Stop();
//						window.Cursor = null;
						AttenteInput = true; // nécessairement à la fin pour que le HeightChanging potentiellement
						// envoyé lors du UpdateLayout() contenu dans le readInput soit pris en compte et envoyé en différé
						break;
					#endregion Input

					case ProprietesWpf.XMECONSULT:
						buffer.GetString(out idPage);
						Consult(idPage, null);
						Html.Envois.Ajouter("xmeConsult", idPage);
						break;

					#region List Consult
					case ProprietesWpf.XMELISTCONSULT:
						{
							uint idDataGrid;
							buffer.GetString(out idPage);
							buffer.Get(out idDataGrid);
							Consult(idPage, null);
							Html.Envois.Ajouter("xmeConsult", idPage);
							
							string id = Html.CalculerIdDataGrid(idDataGrid, idPage);

							//dataGrid = Consult(idPage, idDataGrid) as XHtmlDataGrid;
							//dataGrid.SetIsActive(true); // nécessairement APRES le ManageValidDataGrids() dans le consult car ce dernier RAZ toutes les DataGrids affichées

							// traitement des ronds dans l'entête de la colonne arbre le cas échéant
							byte treeCount;
							buffer.Get(out treeCount);
							if (treeCount > 0)
							{
							   byte treeCurrent;
							   buffer.Get(out treeCurrent);
							//   XHtmlTreeColumn treeColumn = dataGrid.Columns.FirstOrDefault(col => ((IXHtmlDataGridColumn)col).ColumnType == ColumnType.Arbre) as XHtmlTreeColumn;
							//   if (treeColumn != null) treeColumn.SetCircles(treeCount, treeCurrent);
							}
						}
						break;
					#endregion List Consult



					case (ProprietesWpf)161 : 
			//		ClientLegerProprietes.PROPCLIENTLEGER_AFFICHEERREUR:
						AfficherErreurTransmise(buffer);
						break;

					case ProprietesWpf.FENETRE_ATTACHER_TOOLBARS_DEBUT:		// Bordel des toolbars
						buffer.Get(out property);
						while (property != ProprietesWpf.FENETRE_ATTACHER_TOOLBARS_FIN)
						{
							//switch (property)
							{
								//case ProprietesWpf.TOOLBAR_DEBUT:							// Création de toolbar

								//   buffer.Get(out idToolBar);
								//   toolBar = GetXwpfToolBar(idToolBar);

								//   if (toolBar == null) listOfToolBars.Add(toolBar = new XwpfToolBar { Id = idToolBar });

								//   toolBar.ReadProperties(buffer);
								//   break;

								//case ProprietesWpf.TOOLBAR_RECONSTRUCTION_DEBUT:							// Reconstruction de toolbar
								//   buffer.Get(out idToolBar);
								//   toolBar = GetXwpfToolBar(idToolBar);

								//   if (toolBar == null) listOfToolBars.Add(toolBar = new XwpfToolBar { Id = idToolBar });

								//   toolBar.ItemsList.Clear();
								//   toolBar.ReadProperties(buffer);
								//   foreach (XwpfWindow windowFromStack in StackOfWindows)
								//   {
								//      ToolBarTray toolBarTray = (toolBar.Primary) ? windowFromStack.PrimaryToolBarTray : windowFromStack.SecondaryToolBarTray;

								//      ToolBar toolBarInList = toolBarTray.ToolBars.FirstOrDefault(tb => tb.DataContext == toolBar);
								//      if (toolBarInList != null) toolBar.Display(windowFromStack, toolBarInList);
								//   }
								//   break;

								//case ProprietesWpf.FENETRE_ATTACHER_TOOLBAR:				// Pour attacher une toolbar (identifiant menu: ushort)
								//   buffer.Get(out idToolBar);
								//   GetXwpfToolBar(idToolBar).Display(StackOfWindows.Peek());
								//   break;

								//case ProprietesWpf.FENETRE_DETTACHER_TOOLBAR:			// Pour supprimer une toolbar (identifiant menu: ushort)
								//   buffer.Get(out idToolBar);
								//   XwpfToolBar.Remove(idToolBar);
								//   break;

								//default:
								//	throw new XwpfException(XwpfErrorCodes.UnknownProperty, XwpfErrorLocations.Application, property.ToString());
							}
							buffer.Get(out property);
						}
						break;



					default:
						throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Application, property.ToString());
				}
				buffer.Get(out property);
			}

			var response = new DVBuffer();


			#region Réponse asynchrone
			//if (AsynchronousResponse != null && AttenteInput)	 // impossible d'avoir un double-clic en input
			//   AsynchronousResponse = null;
			//if (AsynchronousResponse != null && AttenteConsult)
			//{
			//   SetInputBuffer(AsynchronousResponse);
			//   Send(AsynchronousResponse); // response déjà replie dans le Handler mais pas encore envoyée pour synchronisation
			//   AsynchronousResponse = null;
			//   responseAlreadySent = true; // Pour bloquer le Send(response) vide dans Analyse()
			//   return;
			//}
			#endregion Réponse asynchrone

			#region Réponse asynchrone intertâche
			//if (interTaskResponses.Count > 0) // si réveil intertâche en attente
			//{
			//   if (AttenteInput) interTaskResponses.Clear(); // en input : r.à.z. de la liste des dialogues intertâches en attente
			//   else if (AttenteConsult || AttenteGetb)
			//   {
			//      SendInterTaskResponse();
			//      responseAlreadySent = true; // Pour bloquer le Send(response) vide dans Analyse()
			//      return;
			//   }
			//}
			#endregion Réponse asynchrone intertâche

			#region nb lignes tableau

		// positionne responseAlreadySend et c'est tout
			
			if (nbRows.HasValue)
			{
				//response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
				//response.Put(ProprietesWpf.TABLEAU_NOMBRE_LIGNES);
				//response.Put(nbRows.Value);
				//Send(response);
				responseAlreadySent = true;					// Pour bloquer le Emettre(response) vide dans Analyser()
				return;
			}
			#endregion nb lignes tableau

			#region index colonne tableau
			if (index.HasValue)
			{
				response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
				response.Put(ProprietesWpf.TABLEAU_COLONNE_SAISIE);
				response.Put(index.Value);
				Send(response);
				responseAlreadySent = true;							// Pour bloquer le Emettre(response) vide dans Analyser()
				return;
			}
			#endregion index colonne tableau

			#region bulle scrollbar horizontale tableau
			if (horizontalScrollbarTooltip != null)
			{
				response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
				response.Put(ProprietesWpf.TABLEAU_ASCHOR_BULLE);
				response.PutStringCP(horizontalScrollbarTooltip, codepageBulleAscHor);
				Send(response);
				responseAlreadySent = true;						// Pour bloquer le Emettre(response) vide dans Analyser()
				return;
			}
			#endregion bulle scrollbar horizontale tableau

			#region bulle scrollbar verticale tableau
			if (verticalScrollbarTooltip != null)
			{
				response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
				response.Put(ProprietesWpf.TABLEAU_ASCVER_BULLE);
				response.PutStringCP(verticalScrollbarTooltip, codepageBulleAscVer);
				Send(response);
				responseAlreadySent = true;						// Pour bloquer le Emettre(response) vide dans Analyser()
				return;
			}
			#endregion bulle scrollbar verticale tableau

			#region Retour Fonction YGraph (FA)
			//retour normal (ident ou retour par defaut)
			//if (yDrawingOperationRet.HasValue)
			//{
			//   response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
			//   response.Put(yDrawingIsCreation ? yDrawingOperationRet.Value : (int)yDrawingOperationRet.Value);					//	pas de propriétéWPF identifiant la valeur de retour (14/09/2011)
			//   Send(response);
			//   responseAlreadySent = true;					// Pour bloquer le Emettre(response) vide dans Analyser()
			//   return;
			//}
			////retour de la taille ecran
			//if (yScreenSize.HasValue)
			//{
			//   response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
			//   response.Put((int)yScreenSize.Value.Width);					//	pas de propriétéWPF identifiant la valeur de retour (21/09/2011)
			//   response.Put((int)yScreenSize.Value.Height);

			//   Send(response);
			//   responseAlreadySent = true;					// Pour bloquer le Emettre(response) vide dans Analyser()
			//   return;
			//}
			#endregion //Retour Fonction YGraph (FA)

			#region Paramètres Initiaux Agenda
			//if (responseCalendarInit != null)
			//{
			//   Send(responseCalendarInit);
			//   responseAlreadySent = true;					// Pour bloquer le Emettre(response) vide dans Analyser()
			//}
			#endregion Paramètres Initiaux Agenda

		}  // fin fonction


		/// <summary>
		/// Affichage de la boite des erreurs standard A finaliser
		/// </summary>
		private void AfficherErreurTransmise(DVBuffer rec)
		{
			bool fatale;
			ushort code;
			ushort codesys;
			ushort codecompljour;
			ushort lieujour;
			uint adresse;
			string details;
			string prog;
			string module;
			string texte;
			string titre;
			StructLangueEtCode langue = new StructLangueEtCode();
			string date, heure;


			rec.Get(out fatale);
			rec.GetString(out date);
			rec.GetString(out heure);
			rec.GetString(out texte);
			rec.Get(out lieujour);
			rec.Get(out codecompljour);
			rec.Get(out code);
			rec.Get(out codesys);
			rec.Get(out adresse);
			rec.GetString(out prog);
			rec.GetString(out module);
			rec.GetString(out titre);
			rec.GetString(out details);

			if (module == prog)
				module = "---";

//			this.DelegueAfficherErreur(fatale, titre, texte, code, codesys, codecompljour, lieujour, adresse, prog, module, date + " " + heure, details);

		}


		internal void Analyse(DVBuffer buffer)
		{
			try { GetProperties(buffer); }
			catch (XHtmlException e) { 
				SendError(e.ErrorCode, e.ErrorLocation, e.ErrorParameter); }

			//!!!!!!!!!!!!!!!!!!!! revoir tous ce cas
			if (AttenteInput) return ;								// input : attente opérateur (sauf notif immédiate)
			if (AttenteConsult) return ;							// consult : attente opérateur
			if (AttentePopupMenu) return;							// popup menu : attente opérateur
			if (AttenteGetb) return;								// get bloquant : attente opérateur
			if (responseAlreadySent) return;						// double clic / nb lignes tableau / index colonne / bulle scrollbar tableau envoyé

			//autres cas (display, ...)

			var response = new DVBuffer();
			response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);

			Send(response);											// acquittement (réponse "vide")
			return;
		}



		private IXHtmlEditableObject Consult(string idPage, uint? idObj)
		{

			/*
					XHtmlWindow window = StackOfWindows.Peek();
					XHtmlPage page = window.GetPage(idPage);
					IXHtmlEditableObject obj = (idObj.HasValue ? page.GetObject(idObj.Value) : page) as IXHtmlEditableObject;
					XHtmlGroupBox.ResetActiveGroup();
					XHtmlTreeColumn.ResetCircles(window);
					ManageValidButtons(obj);
					ManageValidMenuItems(obj);
					ManageValidToolBarItems(obj);
					ManageValidDataGrids(idPage);

					// Start timer
					if (XHtmlTimer.PileOfTimers.Count > 0)
					{
						consultTimer = new DispatcherTimer { Interval = new TimeSpan(0, 0, XHtmlTimer.PileOfTimers.Peek().Delay) };
						consultTimer.Tick += XHtmlTimer.TimerHandler;
						consultTimer.Start();
					}
					busyTimer.Stop();

					Keyboard.Focus(window.MainCanvas);
					window.CurrentPage = page;
					window.Cursor = null;
					AttenteConsult = true;
					return obj;
					*/
			return null;
		}
		
		public void FaireLaListeDesEnvoisPourStyles()
		{

			if (Html.App.PolicesCss.Count > 0)
			{
				UnParametreEnvoye p = new UnParametreEnvoye();
				p.commande = "cssAjouts";
				Html.Envois.commandes.Insert(0, p);
				p.valeur = "<style type='text/css'>\r\n";

				foreach (XHtmlFont pol in Html.App.PolicesCss)
				{
					if (pol.Transmi == false)
					{
						pol.Transmi = true;
						//if (pol.Vide == true)
						//   Html.App.ListeIdsPolicesVides.l.Add(pol.Id);
						p.valeur += pol.Css + "\r\n";
					}

				}

				foreach (XHtmlColor col in Html.App.CouleursCss)
				{
					if (col.Transmi == false)
					{
						col.Transmi = true;
						//if (col.Vide == true)
						//   Html.App.ListeIdsCouleursVides.l.Add(col.Id);
						//if (col.Transparente == true)
						//   Html.App.ListeIdsCouleursTransparentes.l.Add(col.Id);
						p.valeur += col.Css + "\r\n";
					}
				}

				foreach (XHtmlBorder bord in Html.App.BorduresCss)
				{
					if (bord.Transmi == false)
					{
						bord.Transmi = true;
						//if (bord.Vide == true)
						//   Html.App.ListeIdsBorduresVides.l.Add(bord.Id);
						p.valeur += bord.Css + "\r\n";
					}
				}

				foreach (XHtmlPadding padd in Html.App.PaddingCss)
				{
					if (padd.Transmi == false)
					{
						padd.Transmi = true;
						//if (padd.Vide == true)
						//   Html.App.ListeIdsPaddingsVides.l.Add(padd.Id);
						p.valeur += padd.Css + "\r\n";
					}
				}

				// - AJOUTER LES IMAGES POUR LES BOUTONS
				foreach (XHtmlImageFile ima in Html.App.ImagesCss)
				{
					if (ima.Transmi == false)
					{
						ima.Transmi = true;
						//if (padd.Vide == true)
						//   Html.App.ListeIdsPaddingsVides.l.Add(padd.Id);
						p.valeur += ima.Css + "\r\n";
					}
				}
				p.valeur += "</style>";
			}
		}


		/// <summary>
		/// Gets the mouseButton that has been clicked during a MouseButtonEvent
		/// </summary>
		/// <param name="e">MouseButtonEventArgs concerned</param>
		/// <returns>byte representing the button : Left = 1, Middle = 2, Right = 3, error = 0</returns>
		internal static byte GetClickedButton(MouseButtonEventArgs e)
		{
			switch (e.ChangedButton)
			{
				case MouseButton.Left: return 1;
				case MouseButton.Middle: return 2;
				case MouseButton.Right: return 3;
				default: return 0;
			}
		}

		/// <summary>
		/// Gets the key combination pressed during a MouseButtonEvent (ctrl, SHIFT, both)
		/// </summary>
		/// <returns>byte representing the key combination : CTRL = 1, SHIFT = 2, CTRL + SHIFT = 3, error = 0</returns>
		internal static byte GetPressedKey()
		{
			/* !!!!!!!!!!!!!!!!! a faire !!!!!!!!!!!!!!!!!
			// Le CTRL + SHIFT doit être testé en premier pour ne pas être masqué par un des deux autres.
			if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) &&
				(Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))) return 3;	// CTRL + SHIFT
			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) return 1;		// CTRL
			if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) return 2;		// SHIFT
			// + si windows sans souris :	////???? WTF ?
			// bit 0x04 = touche bouton gauche
			// bit 0x08 = touche bouton milieu
			// bit 0x10 = touche bouton droit
			 * */
			return 0;
		}

		#region Fonctions Get
		/// <summary>
		/// Reads the buffer for a page ID and a XwpfDataGrid Id and returns the corresponding XwpfDataGrid instance
		/// </summary>
		/// <param name="buffer">DVBuffer where the page and XwpfDataGrid IDs are read</param>
		/// <returns>instance of XwpfDataGrid matching the given page and XwpfDataGrid IDs</returns>
		private XHtmlDataGrid GetDataGrid(DVBuffer buffer, out XHtmlPage page)
		{
			XHtmlDataGrid retour,dataGrid;
			string idPage;
			uint idDataGrid;
			buffer.GetString(out idPage);			// Identifiant de la page (string)
			buffer.Get(out idDataGrid);			// Identifiant du tableau (uint)

			page = StackOfWindows.Peek().GetPage(idPage);

			retour = page.GetObject(idDataGrid) as XHtmlDataGrid;
			if (retour != null)
				return retour;


			// BH, si on ne le trouve pas, on le créée
			page = StackOfWindows.Peek().GetPage(idPage);

			dataGrid = new XHtmlDataGrid(page) { Id = idDataGrid };        // est ce que c'est le bon id ?
			page.Children.Add(dataGrid);
			page.ListOfObjects.Add(dataGrid);
			return dataGrid;
		}

		public string GetIdentDataGrid(DVBuffer buffer)
		{
			string idPage;
			uint idDataGrid;
			buffer.GetString(out idPage);			// Identifiant de la page (string)
			buffer.Get(out idDataGrid);			// Identifiant du tableau (uint)

//			return idPage + "_" + idDataGrid.ToString() + "_" + StackOfWindows.Count.ToString();

			return Html.CalculerIdDataGrid(idDataGrid,idPage);

			//return StackOfWindows.Peek().GetPage(idPage).GetObject(idDataGrid) as XHtmlDataGrid;
		}


		#endregion

		public void LancementDuProgramme(bool premiereFois, XHtmlParametresLancement parametresRecus, Page page, object sender, System.EventArgs e)
		{

			PageWeb = page;
			if (page == null)
			{
				string message = "page null dans XWebPageLoad";
				throw new NullReferenceException(message);
			}

			string parametres = null;
			//	on recupere la session
			SessionWeb = (int)page.Session["SessionDivaltoWeb"];
			//	la première fois

			if (page.Session == null)
			{
				string message = "page.Session null dans XWebPageLoad";
				throw new NullReferenceException(message);
			}
			if (PageWeb.Request == null)
			{
				string message = "PageWeb.Request null dans XWebPageLoad";
				throw new NullReferenceException(message);
			}

			//			if (page.Session.IsNewSession)
			if (premiereFois)
			{
				//	on recupere les paramètres dans la page générique
				Label cparametres = (Label)page.FindControl("parametres");

				if (cparametres != null)
					parametres = cparametres.Text +
									 "<BrowserName>" + PageWeb.Request.Browser.Browser +
									 "<BrowserVersion>" + PageWeb.Request.Browser.Version +
									 "<HomeDirectory>" + page.MapPath(page.TemplateSourceDirectory) +  // bh23052006
									 "<HostName>" + page.Request.UserHostName +							// bh24052007
									 "<HostAddress>" + page.Request.UserHostAddress +						// bh24052007
									 "<UserAgent>" + page.Request.UserAgent;								// bh24052007

				foreach (string lang in page.Request.UserLanguages)
					parametres += ("<Language>" + lang);


				//	ajout de l'url
				parametres += this.CreerHmpQueryString(PageWeb.Request);

				//	ajout des cookies
				parametres += ("<cookies>" + this.CreerHmpCookies(PageWeb.Request));

				//	on lance le programme et on attend la reponse
				//				DialogueX2Y.WebRunProgram(SessionWeb, parametres, out codeecran);


				/////////////////////////////////////////////////
				// Parametres en dur, en attendant mieux
				{
					ushort numeroExecutable = 0;
					string domaine = "";
					string utilisateur = "hao";
					string motDePasse = "hao";
					string utilisateurHarmony = "root";
					string motDePasseHarmony = "hao";
					string programme = parametresRecus.program; //  "tablohtml5.dhop";  // "testhtml5.dhop"; 
					string fenetreMere = "";
					string autres = "";
					string numeroMachine = "123456";
					string Environnement = "";
					string CodeLangueEcran = "";
					string CodeLangueImprimante = "";
					string HARMONY_PARAMetAutres = "";
					int UserPointExclamation = 0;
					string messageErreur;

					{
						//						DVBuffer bufferReception;
						//						byte[] reception;
						messageErreur = "";
						string mpw = "";
						string mph = "";

						if (DialogueX2Y.VerifierSiServeurArrete(utilisateurHarmony) == false)
						{
							//!!!!!!!!!!!!!!!!!!! a finaliser 
							messageErreur = "Le service DhsTerminalServer est arrêté";
							SessionWeb = -1;
							return;
						}

						//SessionWeb = DialogueX2Y.WebOpenSession(); deja fait dans Session_Start


						// A revoir !!!!!!!!!!!!!!!!!!!!!!!!!
						mpw = motDePasse;
						mph = motDePasseHarmony;
						//if (motDePasse != "")
						//	mpw = DVCrypt.Decrypter(motDePasse, DVCrypt.CalculerCle(utilisateur));
						//if (motDePasseHarmony != "")
						//	mph = DVCrypt.Decrypter(motDePasseHarmony, DVCrypt.CalculerCle(utilisateurHarmony));


						// je considère que seul le transport local positionne ce flag
						// cela permet de tester les autres transports en réel meme dans une seule machine
						//----------------------------------------------------------------------------------
						//			bool isLocal = false; //  this.Context.Request.IsLocal;			// dit que la cx est locale

						parametres += "<program>" + programme + "<user>" + utilisateur + "<pw>" + mpw +
											"<userharmony>" + utilisateurHarmony + "<pwHarmony>" + mph + "<transport>serviceweb<pid>0" +
											"<hwndFen>" + fenetreMere + "<domaine>" + domaine + "<numeromachine>" + numeroMachine;

						if (autres.Length > 0)
							parametres += "<other>" + autres;

						if (UserPointExclamation == 1)
							parametres += "<UserPointExclamation>1";

						if (Environnement.Length > 0)
							parametres += "<environnement>" + Environnement;

						if (CodeLangueEcran.Length > 0)
							parametres += "<codelangueecran>" + CodeLangueEcran;

						if (CodeLangueImprimante.Length > 0)
							parametres += "<codelangueimprimante>" + CodeLangueImprimante;

						if (HARMONY_PARAMetAutres.Length > 0)
							parametres += HARMONY_PARAMetAutres;

						DialogueX2Y.WebRunClientLegerHtml(SessionWeb, numeroExecutable, parametres, out DonneesRecuesDeYwpf);
						//						DialogueX2Y.WebRunClientLegerViaServiceWeb(SessionWeb, numeroExecutable, parametres, out codeecran);
					}
				}
			}
			else
			{
				////	on récupère la réponse du navigateur pour la mettre dans un DVBuffer
				//DVBuffer Reponse = new DVBuffer();
				//Reponse.Put(Proprietes.ECRAN_DEBUT);
				//foreach (string id in page.Request.Form)
				//{
				//   // bh22122009 Si c'est une variable indicée je retourne les différentes valeurs

				//   if (id != "__VIEWSTATE")
				//   {
				//      string[] valeurs = page.Request.Form.GetValues(id);
				//      if (valeurs.Length == 1)
				//      {
				//         Reponse.Put(Proprietes.VALEUR_SAISIE);
				//         Reponse.Put(id);
				//         string val = page.Request.Form[id];
				//         Reponse.Put(val);
				//      }
				//      else
				//      {
				//         for (int cpt = 0; cpt < valeurs.Length; cpt++)
				//         {
				//            Reponse.Put(Proprietes.VALEUR_SAISIE);
				//            Reponse.Put(id.Substring(0, id.Length - 1) + (cpt + 1).ToString() + "]");
				//            string val = valeurs[cpt];
				//            Reponse.Put(val);
				//         }
				//      }
				//   }
				//}
				//{																								//JS5 : récup de __YWEBCTRLBYPASS
				//   string id = "__YWEBCTRLBYPASS";
				//   string val = page.Request.QueryString[id];
				//   if (val != null)
				//   {
				//      Reponse.Put(Proprietes.VALEUR_SAISIE);
				//      Reponse.Put(id);
				//      Reponse.Put(val);
				//   }
				//}

				//if (HttpContext.Current == null || HttpContext.Current.Request == null)
				//{
				//   string message = "HttpContext.Current.Request null dans XWebPageLoad";
				//   throw new NullReferenceException(message);
				//}

				//this.AjouterFichiersJoints(Reponse, HttpContext.Current.Request.Files); // bhficjoint


				//Reponse.Put(Proprietes.VALEUR_SAISIE);
				//Reponse.Put("__YWEBCOOKIES");
				//string valc = this.CreerHmpCookies(PageWeb.Request);
				//Reponse.Put(valc);

				//Reponse.Put(Proprietes.ECRAN_FIN);
				////	on l'envoie à Yweb et on attend le nouvel écran

				//DialogueX2Y.WebSendDataAndWaitResponse(SessionWeb, Reponse.LeBuffer, out DonneesRecuesDuNavigateur);
			}


			//	dans tous les cas on crée un DVBuffer avec le code de la page
			if (DonneesRecuesDeYwpf == null)
			{
				string message = "codeecran null dans XWebPageLoad";
				throw new NullReferenceException(message);
			}



			//	On recherche le formulaire par défaut "form1" dans la page
			//	Les objets de la page seront ajoutés dans ce formulaire
			//FormulaireWeb = page.FindControl("form1");
			//if (FormulaireWeb == null)
			//{
			//   string message = "FormulaireWeb null dans XWebPageLoad";
			//   throw new NullReferenceException(message);
			//}
			//FormulaireWeb.EnableViewState = false;				//JS5 : corrige le problème "Failed to load Viewstate"

			// La génération des objets a lieu dans la fonction XWebOnPreRender ci dessous

			//	ATTENTION : ici DotNet va restaurer les valeurs saisies et publiées au coup d'avant par l'utilisateur !!
			//					il faut charger les nouvelles valeurs fournies par le pg Diva plus tard
			//					(au OnLoadComplete ou au OnPreRender ; éventuellement aussi dans une procédure de trt d'un
			//					 événement exécutée côté serveur car celle-ci s'exécute après le OnLoad et avant le OnLoadComplete)
		}

		public void XWebOnPreRender(System.EventArgs e)
		{
			////	On crée la page html
			//ConserverPositionAscenseur = false;					//JS5
			//TraiterEcran(Proprietes.ECRAN_FIN);
			////	On donne le focus à la première donnée à saisir

			//PageWeb.MaintainScrollPositionOnPostBack =		//JS5 : on garde la position précédente de l'ascenseur
			//   ConserverPositionAscenseur;						//		  (sur demande)

			//if (AuMoinsUnBoutonDefaut)								//JS5 : s'il existe un bouton / défaut, on surcharge la
			//   PageWeb.ClientScript.RegisterStartupScript	//méthode standard WebForm_FireDefaultButton (pour que le
			//      (PageWeb.GetType(),								//bouton / défaut ne soit pas valide sous FireFox durant la
			//       "Divalto_WebForm_FireDefaultButton_js",	//saisie d'un texte multi-ligne) ; ici et par RegisterStartupScript
			//       Surcharge_WebForm_FireDefaultButton);		//car il faut impérativement que cette surcharge soit placée APRES
			////la standard (webresource.axd inclusion)

			//if (AuMoinsUnOnChange)									//JS5 : s'il existe un arrêt "OnChange",
			//{																//ajouter une donnée Hidden (le script
			//   HHidden Hidden = new HHidden();					//client la garnira avec le point d'arrêt
			//   Hidden.Id = "__YWEBONCHANGE";						//correspondant en cas de chgt de valeur)
			//   Hidden.Valeur = " ;0";
			//   AjouterControlDansLaPage(Hidden);
			//}
		}





		private string CreerHmpQueryString(HttpRequest request)
		{
			string q;
			int loop1, loop2;

			q = "<qryall>" + request.Url.AbsoluteUri;
			q += "<qryvalues>";

			// Load NameValueCollection object.
			NameValueCollection coll = request.QueryString;
			// Get names of all keys into a string array.
			String[] arr1 = coll.AllKeys;
			for (loop1 = 0; loop1 < arr1.Length; loop1++)
			{
				String[] arr2 = coll.GetValues(arr1[loop1]);
				for (loop2 = 0; loop2 < arr2.Length; loop2++)
				{
					//					if (arr2[loop2].Length == 1)
					q += "[" + arr1[loop1] + "]" + arr2[loop2];
					// SI ON VEUT faire remonter les variables indicées
					//else
					//{
					//   for (int loop3 = 0 ; loop3 < arr2.Length ; loop3++)
					//   {
					//      q += "[" + arr1[loop1] + "(" + loop3.ToString() + ")" + "]" + arr2[loop2][loop3];
					//   }
					//}
				}

			}
			return q;
		}


		/// <summary>
		/// creation d'un morceau de hmp pour faire remonter les cookies vers le pg diva
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		private string CreerHmpCookies(HttpRequest request)
		{
			string q = "";
			int loop1;
			HttpCookie MyCookie;
			HttpCookieCollection MyCookieColl;

			MyCookieColl = request.Cookies;

			// Capture all cookie names into a string array.
			String[] arr1 = MyCookieColl.AllKeys;

			// Grab individual cookie objects by cookie name.
			for (loop1 = 0; loop1 < arr1.Length; loop1++)
			{
				MyCookie = MyCookieColl[arr1[loop1]];

				q += "[" + MyCookie.Name + "]";
				q += "{valeur}" + MyCookie.Value;
				//				q += "{expire}" + MyCookie.Expires.ToString("yyyyMMddhhmmss");
				//				q += "{secure}" + MyCookie.Secure.ToString();
			}
			return q;
		}


		/// <summary>
		/// Creation d'un cookie
		/// </summary>
		private HttpCookie CreerUnCookie(string snom, string svaleur, string sdate, string sdomaine, string schemin)
		{
			int an = 0, mois = 0, jour = 0, heure = 0, minute = 0, seconde = 0;

			try
			{
				an = Convert.ToInt32(sdate.Substring(0, 4), 10);
				mois = Convert.ToInt32(sdate.Substring(4, 2), 10);
				jour = Convert.ToInt32(sdate.Substring(6, 2), 10);
				heure = Convert.ToInt32(sdate.Substring(8, 2), 10);
				minute = Convert.ToInt32(sdate.Substring(10, 2), 10);
				seconde = Convert.ToInt32(sdate.Substring(12, 2), 10);
			}
			catch (System.FormatException)
			{
			}

			HttpCookie c = new HttpCookie(snom, svaleur);
			c.Domain = sdomaine;
			c.Path = schemin;
			try
			{
				c.Expires = new DateTime(an, mois, jour, heure, minute, seconde);
			}
			catch (ArgumentOutOfRangeException)
			{
			}
			catch (ArgumentException)
			{
			}
			return c;
		}

		/// <summary>
		/// Ajout dans les données de retour des fichiers joints
		/// </summary> bhficjoint
		/// <param name="reponse">dvbuffer à completer</param>
		/// <param name="fichiers">fichiers</param>
		void AjouterFichiersJoints(DVBuffer reponse, HttpFileCollection fichiers) // bh1
		{
			string nomreel, nomtemp;
			HttpPostedFile fichierposté;
			string id;

			for (int i = 0; i < fichiers.Count; i++)
			{
				id = fichiers.AllKeys[i];
				fichierposté = fichiers[i];
				nomreel = fichierposté.FileName;
				if (nomreel != "")
				{
					nomtemp = Path.GetTempFileName();
					fichierposté.SaveAs(nomtemp);

					reponse.Put(Proprietes.VALEUR_SAISIE);
					reponse.Put(id);
					string val = "__YWEBFICJOINT1__" + nomtemp +
									 "__YWEBFICJOINT2__" + nomreel;
					reponse.Put(val);
				}
			}
		}



				/// <summary>
		/// Fills the DVBuffer with the value of the given control
		/// </summary>
		/// <param name="response">Buffer containing the prepared response to the Diva program</param>
		/// <param name="typeNode"> DANS default.aspx, voir la fonction envoyerClic</param>
		internal void SetInputBuffer(DVBuffer response,string typeNode,string typeObjet,string valeurObjet,int cp)
		{
			FrameworkElement activeControl = StackOfWindows.Peek().ActiveControl;	//
			int codePage = 0;

			if (response == null) throw new ArgumentNullException("response");
			//if (activeControl == null) return; // on ne fait rien si pas de controle actif

			ProprietesWpf property = new ProprietesWpf();
			string valueString = null;
			ushort? valueUshort = null;

			//if (!AttenteInput) return; // garde-fou : on ne fait rien si l'on n'est pas en attenteInput (ne devrait pas arriver)

			if (typeNode == "BUTTON")	//!!!!!!!!!!!!!!!!tester!!!!!!!!!
				return;		// on n'envoie rien pour le bouton


			//// type d'objet + codepage + valeur
			//if (activeControl is TextBox) !!!!!!!!!!!!!!!!!!!!
			//{
			//   property = ProprietesWpf.CHAMP_VALEUR;
			//   valueString = ((XHtmlTextBox)activeControl).Text;
			//   codePage = ((XHtmlTextBox)activeControl).CodePage;		// !!!!!!!!!!!!!! stocker le codePage dans l'html
			//}
			
			if (typeNode == "INPUT" && typeObjet == "TEXT")
			{
				property = ProprietesWpf.CHAMP_VALEUR;
				valueString = valeurObjet;
				codePage = cp;
			}

			else if (typeNode == "INPUT" && typeObjet == "SELECT") // (activeControl is ComboBox)
			{
			   property = ProprietesWpf.MULTICHOIX_VALEUR;
				valueUshort = (ushort)Convert.ToInt16(valeurObjet);
			}


			else if (typeNode == "DIV" && typeObjet == "CHECKBOX")
			{
			   property = ProprietesWpf.CASE_A_COCHER_ETAT;
				valueUshort = (ushort)Convert.ToInt16(valeurObjet);
					//(((XHtmlCheckBox)activeControl).IsChecked == true) ? (ushort)1 : (ushort)0;
			}

			// DANS default.aspx, voir la fonction envoyerClic

			//else if (activeControl is PasswordBox)
			//{
			//   property = ProprietesWpf.CHAMP_VALEUR;
			//   valueString = ((PasswordBox)activeControl).Password;
			//   codePage = ((XHtmlPasswordBox)(activeControl.DataContext)).CodePage;
			//}

			//else if (activeControl is DatePicker)
			//{
			//   property = ProprietesWpf.CHAMP_VALEUR;
			//   valueString = ((XHtmlDatePicker)activeControl).DisplayText;
			//   codePage = ((XHtmlDatePicker)activeControl).CodePage;
			//}


			//else if (activeControl is ListBox)
			//{
			//   property = ProprietesWpf.MULTICHOIX_VALEUR;
			//   valueUshort = (ushort)(((XHtmlListBox)activeControl).SelectedIndex + 1);
			//}

			//else if (activeControl is GroupBox)
			//{
			//   property = ProprietesWpf.GROUPE_RADIO_BOUTON_ACTIF;
			//   valueUshort = (ushort)(((XHtmlRadioGroup)activeControl).CurrentIndex + 1);
			//}

			//else if (activeControl is RichTextBox)
			//{
			//   property = ProprietesWpf.RICHTEXT_TEXTE;
			//   valueString = ((XHtmlRichTextBox)activeControl).GetValue();
			//}

			//else if (activeControl is DataGrid)
			//{
			//   XHtmlDataGrid dataGrid = (XHtmlDataGrid)activeControl;
			//   int currentRowIndex = dataGrid.Rows.IndexOf((XHtmlRow)dataGrid.CurrentItem);
			//   int currentColIndex = dataGrid.Columns.IndexOf(dataGrid.CurrentCellInfo.Column);
			//   dataGrid.PrepareInputCallBack(dataGrid.GetDataGridCell(currentRowIndex, currentColIndex), out property, out valueString, out valueUshort);
			//}

			response.Put(property);
			if (valueString != null) response.PutStringCP(valueString, codePage);
			else response.Put(valueUshort.Value);
		}

		/// <summary>
		/// Reads the buffer to give the input to the proper object
		/// </summary>
		/// <param name="buffer">DVBuffer where the properties are read</param>
		/// <param name="page">Page containing the object getting the input</param>
		internal void ReadInput(DVBuffer buffer, XHtmlPage page)
		{
			IXHtmlEditableObject obj;

			uint id;	// variable de stockage temporaire des identifiants d'objets

			//page.UpdateLayout();

			ProprietesWpf property;
			buffer.Get(out property);
			switch (property)
			{
				#region Bouton
				case ProprietesWpf.BOUTON_DEBUT:						// Bouton (Xwin :  objet bouton)!!!!!!!!!!!!!!!!!!!!!!!!!
					buffer.Get(out id);
					var button = (XHtmlButton)page.GetObject(id);

					if (button == null)
					{
						button = new XHtmlButton(page) { Id = id };
						page.Children.Add(button);
						page.ListOfObjects.Add(button);
					}

					button.ReadProperties(buffer);
					page.Window.ActiveControl = button;
//					button.IsEnabled = true;	// impératif pour être sûr que le bouton aura bien le focus (pas évident à cause de la gestion des boutons valides non faite côté serveur)
//					button.Focusable = true;	// garde-fou. Le Focusable est repassé à false lors du lostFocus() (pour éviter qu'un bouton ne prenne le focus sans qu'on ne l'y autorise)
//					button.Focus();
					button.AjouterEnvoisUnObjet(this.Html.Envois, page, StackOfWindows.Count());
					Html.Envois.Ajouter("xmeInput", page.Id.ToString(), HtmlGlobal.CalculerId(button.Id, page.Id, StackOfWindows.Count()));

					obj = button;
					break;
				#endregion Bouton

				#region Case à cocher		
				case ProprietesWpf.CASE_A_COCHER_DEBUT:			// CheckBox (Xwin : objet case à cocher)
					buffer.Get(out id);
					var checkBox = (XHtmlCheckBox)page.GetObject(id);

					if (checkBox == null)
					{
						checkBox = new XHtmlCheckBox(page) { Id = id };
						page.Children.Add(checkBox);
						page.ListOfObjects.Add(checkBox);
					}

					checkBox.ReadProperties(buffer);
					page.Window.ActiveControl = checkBox;
					checkBox.AjouterEnvoisUnObjet(this.Html.Envois, page, StackOfWindows.Count());
					Html.Envois.Ajouter("xmeInput", page.Id.ToString(), HtmlGlobal.CalculerId(checkBox.Id, page.Id, StackOfWindows.Count()));
//					checkBox.Focus();
					obj = checkBox;
					break;
				#endregion Case à cocher

				#region Champ
				case ProprietesWpf.CHAMP_DEBUT:						// TextBox (Xwin : objet champ)
					buffer.Get(out id);
					var textBox = (XHtmlTextBox)page.GetObject(id);

					if (textBox == null)
					{
						textBox = new XHtmlTextBox(page) { Id = id };
						page.Children.Add(textBox);
						page.ListOfObjects.Add(textBox);
					}
					textBox.ReadProperties(buffer);
					page.Window.ActiveControl = textBox;
					textBox.AjouterEnvoisUnObjet(this.Html.Envois, page, StackOfWindows.Count());
					Html.Envois.Ajouter("xmeInput", page.Id.ToString(), HtmlGlobal.CalculerId(textBox.Id, page.Id, StackOfWindows.Count()));
//					textBox.EnFocus();
					obj = textBox;
					break;
				#endregion Champ

				#region MultiChoix
				case ProprietesWpf.MULTICHOIX_DEBUT:				// ComboBox (Xwin : objet multichoix)
					buffer.Get(out id);
					var comboBox = (XHtmlComboBox)page.GetObject(id);
					if (comboBox == null)
					{
						comboBox = new XHtmlComboBox(page) { Id = id };
						page.Children.Add(comboBox);
						page.ListOfObjects.Add(comboBox);
					}

					comboBox.ReadProperties(buffer);
					page.Window.ActiveControl = comboBox;
					comboBox.AjouterEnvoisUnObjet(this.Html.Envois, page, StackOfWindows.Count());
					Html.Envois.Ajouter("xmeInput", page.Id.ToString(), HtmlGlobal.CalculerId(comboBox.Id, page.Id, StackOfWindows.Count()));
//					comboBox.Focus();
					obj = comboBox;
					break;
				#endregion MultiChoix


				//#region Champ Caché	!!!!!!!!!!!!!!!!!!!!!!
				//case ProprietesWpf.CHAMP_CACHE_DEBUT:				// PasswordBox (Xwin : objet champ caché)
				//   buffer.Get(out id);
				//   var passwordBox = (XHtmlPasswordBox)page.GetObject(id);
				//   passwordBox.ReadProperties(buffer);
				//   page.Window.ActiveControl = passwordBox.PasswordBox;
				//   passwordBox.Focus();
				//   obj = passwordBox;
				//   break;
				//#endregion Champ Caché

				//#region Champ Date
				//case ProprietesWpf.CHAMP_DATE_DEBUT:						// TextBox (Xwin : objet champ)
				//   buffer.Get(out id);
				//   var datePicker = (XHtmlDatePicker)page.GetObject(id);
				//   datePicker.ReadProperties(buffer);
				//   page.Window.ActiveControl = datePicker;
				//   datePicker.Focus();
				//   obj = datePicker;
				//   break;
				//#endregion Champ Date

				//#region MultiChoix
				//case ProprietesWpf.MULTICHOIX_DEBUT:				// ComboBox (Xwin : objet multichoix)
				//   buffer.Get(out id);
				//   var comboBox = (XHtmlComboBox)page.GetObject(id);
				//   comboBox.ReadProperties(buffer);
				//   page.Window.ActiveControl = comboBox;
				//   comboBox.Focus();
				//   obj = comboBox;
				//   break;
				//#endregion MultiChoix

				//#region Liste
				//case ProprietesWpf.LISTECHOIX_DEBUT:				// ListBox (Xwin : objet multichoix ouvert)
				//   buffer.Get(out id);
				//   var listBox = (XHtmlListBox)page.GetObject(id);
				//   listBox.ReadProperties(buffer);
				//   page.Window.ActiveControl = listBox;
				//   listBox.Focus();
				//   obj = listBox;
				//   break;
				//#endregion Liste

				//#region Groupe Radio
				//case ProprietesWpf.GROUPE_RADIO_DEBUT:				// Radiobutton (Xwin : objet groupe radio)
				//   buffer.Get(out id);
				//   var radioGroup = (XHtmlRadioGroup)page.GetObject(id);
				//   radioGroup.ReadProperties(buffer);
				//   page.Window.ActiveControl = radioGroup;
				//   radioGroup.Focus();
				//   obj = radioGroup;
				//   break;
				//#endregion Groupe Radio

				//#region RichText
				//case ProprietesWpf.RICHTEXT_DEBUT:					// RichTextBox (Xwin : objet texte riche)
				//   buffer.Get(out id);
				//   var richText = (XHtmlRichTextBox)page.GetObject(id);
				//   richText.ReadProperties(buffer);
				//   richText.Input();
				//   page.Window.ActiveControl = richText;
				//   richText.Focus();
				//   obj = richText;
				//   break;
				//#endregion RichText

				default:
					throw new XHtmlException(XHtmlErrorCodes.UnknownProperty, XHtmlErrorLocations.Application, property.ToString());
			}

			//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!  a faire !!!!!!!!!!!!!!!!!
			//StackOfWindows.Peek().UpdateLayout();			// force un recalcul des hauteurs/largeur pour être sûr que les valeurs dans le SetActiveGroup sont à jour
			//XHtmlTreeColumn.ResetCircles(StackOfWindows.Peek());
			//XHtmlGroupBox.SetActiveGroup((Control)obj);
			//ManageValidButtons(obj);
			//ManageValidMenuItems(obj);
			//ManageValidToolBarItems(obj);
			//ManageValidDataGrids(page.Id);
		}

		/// <summary>
		/// Gets the key combination pressed during a MouseButtonEvent (ctrl, SHIFT, both)
		/// </summary>
		/// <returns>byte representing the key combination : CTRL = 1, SHIFT = 2, CTRL + SHIFT = 3, error = 0</returns>
		static public  byte GetPressedKey(string shift,string ctrl,string alt)
		{
			// Le CTRL + SHIFT doit être testé en premier pour ne pas être masqué par un des deux autres.

			if (ctrl == "true" && shift == "true") return 3;
			if (ctrl == "true") return 1;
			if (shift == "true") return 2;

			//if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) &&
			//   (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))) return 3;	// CTRL + SHIFT

			//if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) return 1;		// CTRL
			
			//if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) return 2;		// SHIFT
			return 0;
		}




		/// <summary>
		/// Generation de la chaine correspondant a une couleur
		/// </summary>
		/// <param name="idFond"></param>
		/// <returns></returns>
		public string GenererUneValeurDeCouleur(ushort? idFond)
		{
			XHtmlColor i;
			i = CouleursCss.Find(e => e.Id == idFond);
			if (i != null)
			{
				if (i.Vide || i.Transparente)
					return "xxxx";
				else
					return XHtmlColor.PrefixeCssFond + idFond.ToString();
			}
			return null;
		}
	}
}