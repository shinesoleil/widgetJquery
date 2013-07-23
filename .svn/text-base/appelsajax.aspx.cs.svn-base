//___________________________________________________________________________
// Projet		 : XHtml
// Nom			 : AppelsAjax.aspx.cs
//
// Description : code la page appelée en ajax
//___________________________________________________________________________
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Text;
using Divalto.Systeme.DVOutilsSysteme;
using Divalto.Systeme;
using System.Runtime.InteropServices;
using Divalto.Systeme.DHTransportLC;
using Divalto.XWeb;


namespace Divaltohtml
{
	/// <summary>
	/// Classe pour récupérer les parametres venant du client
	/// Attention, si on change les noms il faut aussi les changer en JavaScript
	/// </summary>
	[DataContract]
	public class UnParametreRecu
	{
		[DataMember]
		public string commande;
		[DataMember]
		public string valeur;
		[DataMember]
		public string compl;
	};

	//	[KnownType(typeof(UneCommandeHeritee))]
	[DataContract]
	public class ListeParametresRecus
	{
		[DataMember]
		public List<UnParametreRecu> commandes = new List<UnParametreRecu>();

	};


	/// <summary>
	/// Class pour les parametres que j'envoie.
	/// Il y en a une avec compl et une sans compl pour ne pas générer des champs vides
	/// </summary>
	[DataContract]
	public class UnParametreEnvoye
	{
		[DataMember]
		public string commande;
		[DataMember]
		public string valeur;
	};

	[DataContract]
	public class UnParametreEnvoyeAvecCompl : UnParametreEnvoye
	{
		[DataMember]
		public string compl;
	};

	[DataContract]
	public class UnParametreEnvoyeAvec2Compl : UnParametreEnvoye
	{
		[DataMember]
		public string compl;
		[DataMember]
		public string compl2;
	};


	[KnownType(typeof(UnParametreEnvoyeAvecCompl))]				// gestion de l'héritage
	[KnownType(typeof(UnParametreEnvoyeAvec2Compl))]				// gestion de l'héritage
	[DataContract]
	public class ListeParametresEnvoyes
	{
		[DataMember]
		public List<UnParametreEnvoye> commandes = new List<UnParametreEnvoye>();

		public void Ajouter(string commande, string valeur)
		{
			UnParametreEnvoye p = new UnParametreEnvoye();
			p.valeur = valeur;
			p.commande = commande;
			this.commandes.Add(p);
		}

		public void Ajouter(string commande, string valeur, string compl)
		{
			UnParametreEnvoyeAvecCompl p = new UnParametreEnvoyeAvecCompl();
			p.valeur = valeur;
			p.commande = commande;
			p.compl = compl;
			this.commandes.Add(p);
		}

		public void Ajouter(string commande, string valeur, string compl1, string compl2)
		{
			UnParametreEnvoyeAvec2Compl p = new UnParametreEnvoyeAvec2Compl();
			p.valeur = valeur;
			p.commande = commande;
			p.compl = compl1;
			p.compl2 = compl2;
			this.commandes.Add(p);
		}

	};


	/// <summary>
	/// Parametres complémentaires pour l'envoi.
	/// Un parametre 'normal' peut avoir comme valeur une autre liste de parametres
	/// </summary>
	[DataContract]
	public class UnParametreComplementaire
	{
		[DataMember]
		public string n;
		[DataMember]
		public string v;
	};

	[DataContract]
	public class ListeParametresComplementaires
	{
		[DataMember]
		public List<UnParametreComplementaire> p = new List<UnParametreComplementaire>();
		public UnParametreComplementaire Ajouter(string nom, string valeur)
		{
			UnParametreComplementaire pa = new UnParametreComplementaire();
			pa.n = nom;
			pa.v = valeur;
			p.Add(pa);
			return pa;
		}
	};


	/// <summary>
	/// La page ajax
	/// </summary>
	public partial class DivaltoAjax : System.Web.UI.Page
	{
		public UTF8Encoding utf8Encoder = new UTF8Encoding();
		string chaineAEnvoyer = "";


		//***********************************************************************************************
		//***********************************************************************************************
		// C'EST ICI QUE CA SE PASSE
		//***********************************************************************************************
		//***********************************************************************************************
		protected unsafe void Page_Load(object sender, EventArgs e)
		{
			string chaineJson = "";

			// récupération des parametres envoyés en ajax
			//-----------------------------------------------------------------
			foreach (string id in Request.Form)
			{
				string[] valeurs = Request.Form.GetValues(id);
				if (valeurs.Length == 1)
				{
					string val = Request.Form[id];
					if (id == "divaltoParametresAjax")
						chaineJson = val;
				}
				else 
				{
					//§!!!!!!!!!!!!!!!!!!!!!!!!
					for (int cpt = 0; cpt < valeurs.Length; cpt++)
					{
						string val = valeurs[cpt];
					}
				}
			}


			// je n'ai aucune variable statique CAR (OH SURPRISE) elle serait globale a toutes les sessions
			//----------------------------------------------------------------------------------------------
			XHtmlApplication app = new XHtmlApplication();
			app.Html = new HtmlGlobal();
			app.Html.App = app;


			// je recupere ce que j'ai stocké dans les variables de session
			// Les polices,bordures,couleurs,paddings,pile des fenetres
			//----------------------------------------------------------------------------------------------
			byte[] toutEnJson = (byte[])this.Session["application"];
			if (toutEnJson != null)
			{
				// je passe par une donnée de travail CAR LES CONSTRUCTEURS NE SONT PAS APPELES par les fonctions JSON
				XHtmlApplication wapp = (XHtmlApplication)app.Html.JsonApplication.ReadObject(new MemoryStream(toutEnJson));

				app.BorduresCss = wapp.BorduresCss;
				app.PaddingCss = wapp.PaddingCss;
				app.PolicesCss = wapp.PolicesCss;
				app.CouleursCss = wapp.CouleursCss;
				app.StackOfWindows = wapp.StackOfWindows;

				// pour chaque page, remet le pointeur vers la fenetre parent
				foreach (XHtmlWindow w in app.StackOfWindows)
				{
					foreach (XHtmlPage p in w.ListOfPages)
					{
						p.Html = app.Html;
						p.Window = w;
					}
				}
			}

			// - au moins une fenetre sur le stack
			if (app.StackOfWindows.Count == 0)
				app.StackOfWindows.Push(new XHtmlWindow());

			// - création d'une liste de commandes a partir des données recues
			byte[] tablo = app.Html.UniEncoding.GetBytes(chaineJson);
			ListeParametresRecus recus = (ListeParametresRecus)app.Html.JsonParamsRecus.ReadObject(new MemoryStream(tablo));


			// - Balayage des données transmises par le client
			//---------------------------------------------------------------------------------
			foreach (UnParametreRecu recu in recus.commandes)
			{
				switch (recu.commande)
				{
					case "premierappel":
						{
							byte[] tablop = app.Html.UniEncoding.GetBytes(recu.compl);
							XHtmlParametresLancement lancement = (XHtmlParametresLancement)app.Html.JsonParametresLancement.ReadObject(new MemoryStream(tablop));
							app.LancementDuProgramme(true, lancement, this, sender, e);
						}
						break;


					case "cmdSuite":					// il n'y avait rien a faire coté client, juste a réveiller le serveur
						{
							var response = new DVBuffer();
							response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse
							EnvoyerEtAttendre(response,app);
						}
						break;


					case "transmettreBufferGarde":	// transmission du buffer précédemment gardé
						{
							DVBuffer b;
							byte[] t = (byte[])this.Session["DVBufferATransmettreImmediatement"];
							b = new DVBuffer(t);
							EnvoyerEtAttendre(b, app);
						}
						break;


					case "tabGetNbLig":					// transmettre le nombre de lignes d'un tableau
						{
							ushort us;
							DVBuffer b = new DVBuffer();
							b.Put(ProprietesWpf.CLIENTLEGER_DEBUT);
							b.Put(ProprietesWpf.TABLEAU_NOMBRE_LIGNES);
							us = Convert.ToUInt16(recu.valeur);
							b.Put(us);
							EnvoyerEtAttendre(b, app);

						}
						break;

					case "identsTables":					//	reception de la liste des idents des objets tableaux,
																// comme ca je peux tester si un tableau existe ou non
						{
							string[] idents = recu.valeur.Split(',');
							app.Html.ListeIdentsTablesExistantesCoteClient = idents;
						}
						break;


					case "toucheFonction":
						{
							// - création d'une liste de commandes a partir des données recues
							string [] p;
							string key = "";
							string shift = "false";
							string ctrl = "false";
							string alt = "false";
							string typeNode = "";
							string typeInput = "";
							string valeurObjet = "";
							int codePage=0;

							byte[] stablo = app.Html.UniEncoding.GetBytes(recu.compl);
							ListeParametresRecus srecus = (ListeParametresRecus)app.Html.JsonParamsRecus.ReadObject(new MemoryStream(stablo));
							foreach (UnParametreRecu sp in srecus.commandes)
							{
								switch (sp.commande)
								{
									case "typeTouche" :
										p = sp.valeur.Split(',');
										key = p[0];
										shift = p[1];
										ctrl = p[2];
										alt = p[3];
										break;
									case "typeObjet" :
										typeNode = sp.valeur;
										typeInput = sp.compl.ToUpper();
										break;
									case	"valeurObjet" :
										valeurObjet = sp.valeur;
										codePage = Convert.ToInt32(sp.compl);
										break;
								}
							}

							var response = new DVBuffer();
							response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse

							app.SetInputBuffer(response,typeNode,typeInput,valeurObjet,codePage);

							if (ctrl == "true")
								response.Put(ProprietesWpf.TOUCHE_CTRL);
							if (shift == "true")
								response.Put(ProprietesWpf.TOUCHE_SHIFT);
							if (alt == "true")	
								response.Put(ProprietesWpf.TOUCHE_ALT);

							response.Put(ProprietesWpf.TOUCHE_CLAVIER);					// touche tapée (APRES les modificateurs)
							response.PutString(key);

							EnvoyerEtAttendre(response, app);

						}
						break;


					case "clicSouris":
						{
							string[] p;
							int nombreClic = 1;
							int boutonSouris = 1;
							ushort arretDemandeSaisie = 0;
							ushort numeroPage = 0;
							string shift = "false";
							string ctrl = "false";
							string alt = "false";
							string typeNode = "";
							string typeInput = "";
							string valeurObjet = "";
							int sequence = 0;
							int codePage = 0;
							ClicOnglet onglet=null;

							byte[] stablo = app.Html.UniEncoding.GetBytes(recu.compl);
							ListeParametresRecus srecus = (ListeParametresRecus)app.Html.JsonParamsRecus.ReadObject(new MemoryStream(stablo));
							foreach (UnParametreRecu sp in srecus.commandes)
							{
								switch (sp.commande)
								{
									case "typeClic" :
										p = sp.valeur.Split(',');
										nombreClic = Convert.ToInt32(p[0]);
										boutonSouris = Convert.ToInt32(p[1]);
										shift = p[2];
										ctrl = p[3];
										alt = p[4];
										break;

									case "typeObjet" :
										typeNode = sp.valeur;
										typeInput = sp.compl.ToUpper();
										break;

									case "infosPage" :
										p = sp.valeur.Split(',');
										sequence = Convert.ToInt32(p[0]);
										numeroPage = (ushort)Convert.ToInt16(p[1]);
										arretDemandeSaisie = (ushort)Convert.ToInt16(p[2]);
										break;
									case "valeurObjet" :
										valeurObjet = sp.valeur;
										codePage = Convert.ToInt32(sp.compl);
										break;
									case "clicOnglet":
										{
											byte[] stablong = app.Html.UniEncoding.GetBytes(sp.valeur);
											onglet = (ClicOnglet)app.Html.JsonClicOnglet.ReadObject(new MemoryStream(stablong));
										}
										break;
								}
							}
							var response = new DVBuffer();
							response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse
							app.SetInputBuffer(response, typeNode, typeInput, valeurObjet, codePage);

							response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris

							if (onglet == null)
							{

								response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
								response.Put((byte)MouseEvent.ClickControl);
								response.Put(ProprietesWpf.PAGE_NUMERO);					// Numéro de la page contenant la donnée cliquée
								response.Put((byte)numeroPage);
								response.Put(ProprietesWpf.PAGE_ARRET_SAISIE);			// Numéro du point d'arrêt "Demande de saisie" de la page contenant la donnée cliquée
								response.Put(arretDemandeSaisie);

								p = recu.valeur.Split('_');
								response.Put(ProprietesWpf.IDENT_UNIQUE);					// Id de la donnée cliquée (uint)
								response.Put(Convert.ToInt32(p[0]));

								response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);		// Numéro du point de séquence de la donnée cliquée
								response.Put((ushort)sequence);


								// Informations sur le clic lui-même, à savoir (pour le moment) :
								response.Put(ProprietesWpf.SOURIS_CLIC);					// 1 = simple clic; 2 = double clic (byte)
								response.Put((byte)nombreClic);
								response.Put(ProprietesWpf.SOURIS_BOUTON);				// 1 = gauche; 2 = milieu; 3 = droite (byte)
								response.Put((byte)boutonSouris);
								response.Put(ProprietesWpf.SOURIS_TOUCHE);				// touche(s) du clavier enfoncée(s) simultanément (byte)
								response.Put(XHtmlApplication.GetPressedKey(shift, ctrl, alt));

								//							if (e.ClickCount < 2) Send(response);						// envoi de la réponse et attente de la suite
								//							else if (!AttenteInput) AsynchronousResponse = response;
							}
							else
							{
								response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
								response.Put((byte)MouseEvent.ClickTab);
								response.Put(ProprietesWpf.SOURIS_CLIC);					// 1 = simple clic; 2 = double clic (byte)
								response.Put((byte)1);
								response.Put(ProprietesWpf.SOURIS_BOUTON);				// 1 = gauche; 2 = milieu; 3 = droite (byte)
								response.Put((byte)1);
								response.Put(ProprietesWpf.SOURIS_TOUCHE);				// touche(s) du clavier enfoncée(s) simultanément (byte)
								response.Put((byte)0);

								// Informations sur le changement d'onglet :
								response.Put(ProprietesWpf.ONGLET_ARRET);					// Numéro du point d'arrêt (ushort)
								response.Put(onglet.arretOnglet);
								response.Put(ProprietesWpf.ONGLET_NOUVELLE_PAGE);		// numéro de page de l'onglet à afficher (byte)
								response.Put((byte)onglet.pageOngletClick);
								response.Put(ProprietesWpf.ONGLET_ANCIENNE_PAGE);		// numéro de page de l'onglet précédemment affiché (byte)
								response.Put((byte)onglet.anciennePage);
							}
							response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris
							EnvoyerEtAttendre(response, app);
						}
						break;


					case "notificationCaseACocher":
						{
							string[] p;
							ushort arretDemandeSaisie = 0;
							ushort numeroPage = 0;
							string valeurObjet = "";
							int sequence = 0;

							byte[] stablo = app.Html.UniEncoding.GetBytes(recu.compl);
							ListeParametresRecus srecus = (ListeParametresRecus)app.Html.JsonParamsRecus.ReadObject(new MemoryStream(stablo));
							foreach (UnParametreRecu sp in srecus.commandes)
							{
								switch (sp.commande)
								{
									case "infosPage":
										p = sp.valeur.Split(',');
										sequence = Convert.ToInt32(p[0]);
										numeroPage = (ushort)Convert.ToInt16(p[1]);
										arretDemandeSaisie = (ushort)Convert.ToInt16(p[2]);
										break;
									case "valeurCase":
										valeurObjet = sp.valeur;
										break;
								}
							}
							var response = new DVBuffer();
							response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse

							response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);	// Début de l'envoi des évenements souris
							response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);		// Type d'évènement souris (byte)
							response.Put((byte)MouseEvent.Notification);
							response.Put(ProprietesWpf.PAGE_NUMERO);					// Numéro de la page contenant la donnée cliquée
							response.Put((byte)numeroPage);

							p = recu.valeur.Split('_');
							response.Put(ProprietesWpf.IDENT_UNIQUE);					// Id de la donnée cliquée (uint)
							response.Put(Convert.ToInt32(p[0]));

							response.Put(ProprietesWpf.PARAM_SAISIE_SEQUENCE);		// Numéro du point de séquence de la donnée cliquée
							response.Put((ushort)sequence);
							response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);		// Fin de l'envoi des évenements souris

							response.Put(ProprietesWpf.CASE_A_COCHER_ETAT);
							response.Put((ushort)Convert.ToInt16(valeurObjet));

							EnvoyerEtAttendre(response, app);
						}
						break;


					case "clicbouton":					// transfert des infos du clic a JS
						{
							byte[] tablop = app.Html.UniEncoding.GetBytes(recu.compl);
							ListeParametresComplementaires recusp = (ListeParametresComplementaires)app.Html.JsonParamCompl.ReadObject(new MemoryStream(tablop));
							UnParametreComplementaire p1, p2;
							string idBouton = recu.valeur;
							p1 = recusp.p.First();
							byte action = Convert.ToByte(p1.v);
							p2 = recusp.p[1];


							var response = new DVBuffer();
							response.Put(ProprietesWpf.CLIENTLEGER_DEBUT);				//début de l'acquittement ou de la réponse
							response.Put(ProprietesWpf.EVENEMENT_SOURIS_DEBUT);		// Début de l'envoi des évenements souris
							response.Put(ProprietesWpf.SOURIS_TYPE_EVENEMENT);			// Type d'évènement souris (byte)
							response.Put((byte)MouseEvent.ClickButton);
							response.Put(ProprietesWpf.BOUTON_ACTION);					// Type d'action (byte = 1, 2 ou 3)
							response.Put(action);
							switch (action)
							{
								case 1: response.Put(Convert.ToInt16(p2.v)); break;	// Type = 1 : point de traitement
								case 2: response.Put(Convert.ToInt16(p2.v)); break;	// Type = 2 : point d'arrêt
								case 3: response.PutString(p2.v); break;					// Type = 3 : chaîne à générer
							}

							response.Put(ProprietesWpf.EVENEMENT_SOURIS_FIN);			// Fin de l'envoi des évenements souris

							EnvoyerEtAttendre(response,app);
						}

						break;



					case "clicmultichoix":
						//envoi = new UnParametreEnvoye();
						//envoi.commande = "status";
						//envoi.valeur = "true";
						//envois.commandes.Add(envoi);

						//envoi = new UnParametreEnvoye();
						//envoi.commande = "script";
						//envoi.valeur = "laisserPasserClickMultiChoix=true;";
						//envoi.valeur += "$(\"#dropdownlist\").data(\"kendoDropDownList\").open();";
						//envois.commandes.Add(envoi);

						break;


				}
			}


			//*****************************************************************************************
			// Analyse des données provenant de JS
			//*****************************************************************************************
			app.CodeEcran = new DVBuffer(app.DonneesRecuesDeYwpf, Marshal.SizeOf(typeof(EnteteTrame)), app.DonneesRecuesDeYwpf.Length);

			app.Analyse(app.CodeEcran);



			//*****************************************************************************************
			// Préparation des envois vers le client
			//*****************************************************************************************
			app.FaireLaListeDesEnvoisPourStyles();

			// - utilisé pour les erreurs détectées dans la couche IIS
			if (app.DVBufferATransmettreImmediatement != null)
			{
				DVBuffer b = app.DVBufferATransmettreImmediatement;
				byte[] t = new byte[b.GetTaille()];
				Array.Copy(b.LeBuffer, t, b.GetTaille());
				this.Session["DVBufferATransmettreImmediatement"] = t;
				app.Html.Envois.Ajouter("transmettreBufferGarde", "session");
			}
			else
				this.Session["DVBufferATransmettreImmediatement"] = null;

			chaineAEnvoyer = HtmlGlobal.ToJsonString(app.Html.Envois, app.Html.JsonParamsEnvoyes, false);

			// - je garde tout ce qui m'intéresse dans une donnée de session
			//--------------------------------------------------------------------------------
			this.Session["application"] = HtmlGlobal.ToJson(app, app.Html.JsonApplication);
			byte[] wtout = (byte [])this.Session["application"];
		}


		//*************************************************************************************
		// Envoyer les données vers JS et attendre sa réponse
		//*************************************************************************************
		private void EnvoyerEtAttendre(DVBuffer response, XHtmlApplication app)
		{
			int sessionWeb = (int)this.Session["SessionDivaltoWeb"];

			response.Put(ProprietesWpf.CLIENTLEGER_FIN);


			// emballe le DVBuffer comme si c'était une trame client léger
			byte[] tout = XHtmlOutils.AjouterEnteteAuDVBuffer(response);

			// envoi + attente
			DialogueX2Y.WebSendDataAndWaitResponse(sessionWeb, tout, out  app.DonneesRecuesDeYwpf);

			//	dans tous les cas on crée un DVBuffer avec le code de la page
			if (app.DonneesRecuesDeYwpf == null)
			{
				string message = "codeecran null dans XWebPageLoad";
				throw new NullReferenceException(message);
			}
		}


		/// <summary>
		/// Surcharge de la fonction dotnet pour envoyer les datas vers le client
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write(chaineAEnvoyer);
		}


	}
}
