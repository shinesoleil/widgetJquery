<%@ Page Title="Page d'accueil" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
	Inherits="Divaltohtml._Default" 
	validateRequest="false" 
	%>
<!DOCTYPE html >   

<!--
 PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
 -->


<html> 
<!--
 xmlns="http://www.w3.org/1999/xhtml">
 -->
<head id="Head1" runat="server">
	<link rel="stylesheet" type="text/css" media="screen" href="styles/ResetStyles.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="styles/DivaltoDefault.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="Scripts/jQuery/css/smoothness/jquery-ui-1.10.1.custom.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="Scripts/jqgrid/css/ui.jqgrid.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="Scripts/select2/select2.css" />

   	<link rel="stylesheet" type="text/css" media="screen" href="styles/divaltoWidgets/groupBox.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="styles/divaltoWidgets/caseACocher.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="styles/divaltoWidgets/onglet.css" />
   	<link rel="stylesheet" type="text/css" media="screen" href="styles/divaltoWidgets/grille.css" />


	<script type="text/javascript" src="scripts/jquery/jquery-1.9.1.js"></script>


	<script type="text/javascript" src="scripts/jquery/jquery-ui-1.10.1.custom.js"></script>
	<script src="Scripts/jqgrid/js/i18n/grid.locale-en.js" type="text/javascript"></script>
	<script src="Scripts/jqgrid/js/jquery.jqGrid.min.js" type="text/javascript"></script>
	<script src="Scripts/select2/select2.js" type="text/javascript"></script>
	<script src="Scripts/localStorage/jquery.storage.js" type="text/javascript"></script>
	<script src="Scripts/divaltoWidgets/groupBox.js" type="text/javascript"></script>
   	<script src="Scripts/divaltoWidgets/caseACocher.js" type="text/javascript"></script>
   	<script src="Scripts/divaltoWidgets/onglet.js" type="text/javascript"></script>
   	<script src="Scripts/divaltoWidgets/grille.js" type="text/javascript"></script>

<!--
   <link href="http://ivaynberg.github.com/select2/select2-3.3.0/select2.css" rel="stylesheet" />
   <script src="http://ivaynberg.github.com/select2/select2-3.3.0/select2.js" type="text/javascript"/>
-->


	<!-- Pour être certain que IE execute la bonne version du fichier. Penser a effacer le cache une fois qu'on active cette option
	-->
	<meta http-equiv="Pragma" content="no-cache">		
	<script src="Scripts/divaltoHtml5.js" type="text/javascript" ></script>
	<!--
.ui-icon.test-icon {
	background-image: url(favoris.png);
	background-position: 0;
}

	<style type="text/css">
.ui-icon.divalto_favoris_png {
	background-image: url(favoris.png);
	background-position: 0;
}
-->
<title>Your new title here</title>

</head>


<script>

    /*
    Je piege la fonction val de jquery
    Lorsqu'elle est appelée pour modifier une valeur, j'appelle la fonction standard et ensuite je declenche changeval
    */
    (function ($) {
        var originalVal = $.fn.val;
        $.fn.val = function (value) {
            if (typeof value != 'undefined') {
                var ret = originalVal.call(this, value);
                this.trigger("changeval");
                return ret;
            }
            return originalVal.call(this);
        };
    })(jQuery);


	var parametresAppli = {
		program:"testhtml5.dhop",
		user:"js",
		div:"autre=valeurautre"
	}

</script>

<body>
	
	<input id="divaltoParametres" type="hidden" value="[programme]tablohtml5.dhop" />
	
    <!-- 
        important de mettre class='divalto' pour donner du poids
        -->
	<div id="monbody" class="cssdivalto" >
	</div>

<script>




	//************************************************************************************
	// Extension sur les tableaux pour avoir le dernier élément
	//************************************************************************************
	Array.prototype.Sommet = function () {
		if (this.length == 0) return null;
		return this[this.length - 1];
	}


	//************************************************************************************
	// classes unePage  
	//************************************************************************************
	function unePage(cj, ident) {					// une page = son ident + son code
		this.codeJquery = cj;
		this.id = ident;
		this.htmlDeLaPage = ""; 					// code html de la page quand on la sauvegarde
		this.arretDemandeDeSaisie = 0;
		this.numeroDePage = 0;
		this.attachementBas    = false;
		this.hauteurVariable   = false;
		this.attachementDroite = false;
		this.largeurVariable = false;
		this.positionPageXOrg = 0;
		this.positionPageYOrg = 0;


		this.taillePageX = cj.width();
		this.taillePageY = cj.height();
		this.taillePageXOrg = this.taillePageX;
		this.taillePageYOrg = this.taillePageY;

	}

	//************************************************************************************
	// classes uneFenetre  
	//************************************************************************************
	function uneFenetre(_id) {					
		this.listePages = new Array;
		this.id = _id;
		this.codeJquery = $(" ");
		this.tailleFenetreXOrg = 0;
		this.tailleFenetreYOrg = 0;
//		this.tailleFenetreX = 0;
//		this.tailleFenetreY = 0;
		this.libellePourTailleFenetre = "";
		this.numeroPourTailleFenetre = "";
		this.tailleFixeFenetre = false;


		this.setTailleOrg = function (lgx,lgy)	{
//			this.tailleFenetreX = lgx;
			this.tailleFenetreXOrg = lgx;
//			this.tailleFenetreY = lgy;
			this.tailleFenetreYOrg = lgy;
		}

		// recherche d'une page à l'intérieur d'une fenetre
		this.chercherPage = function (ident) {
			var i;
			for (i = 0; i < this.listePages.length; i++) {
				if (this.listePages[i].id == ident)
					return this.listePages[i];
			}
			return null;
		}

		this.chercherPageParNumero = function (pp) {
			var i;
			for (i = 0; i < this.listePages.length; i++) {
				if (this.listePages[i].numeroDePage == pp)
					return this.listePages[i];
			}
			return null;
		}

		this.balayerPages = function (callBack) {
			var i;
			for (i = 0; i < this.listePages.length; i++) {
				callBack(i, this.listePages[i]);
			}
		}
	}


    // remplace le stockage en registre
	function RechercherTaillePositionFenetre(libelle, num) {
		var x = 10;
		var y = 20;
		var lgx = 100;
		var lgy = 200;

		return null;   //!!!!!!!!!!!!!!!!!!!!!!!!remettre!!!!!!!!!!!!!!!!!!!!!!!


		var p = $.localStorage().getItem(libelle + "_" + num.toString());

		if (p == null) {
			return null;
		}

		t = p.split(",");

		return ({ x: parseInt(t[0]), y: parseInt(t[1]), lgx: parseInt(t[2]), lgy: parseInt(t[3]) });
	}


	function SauverTaillePositionFenetre(libelle, num, left, top, width, height) {

		left = parseInt(left);
		top = parseInt(top);
		width = parseInt(width);
		height = parseInt(height);

		if (left == "NaN" || top == "NaN" || width == "NaN" || height == "NaN")
			alert("Nan");


		var valeur = left.toString() + "," + top.toString() + "," + width.toString() + "," + height.toString();

		// alert(valeur);

		$.localStorage().setItem(libelle + "_" + num.toString(), valeur);
	}

	window.onbeforeunload = function (e) {
		var w = $(window); //  divaltoGlob.pileFenetres.Sommet();
		var fenetre = divaltoGlob.pileFenetres.Sommet();
		if (typeof (window.screenLeft) == "undefined")
			// firefox et autres
			SauverTaillePositionFenetre(fenetre.libellePourTailleFenetre, fenetre.numeroPourTailleFenetre, window.screenX, window.screenY, window.outerWidth, window.outerHeight);
		else
			// IE tout seul
			SauverTaillePositionFenetre(fenetre.libellePourTailleFenetre, fenetre.numeroPourTailleFenetre, window.screenLeft, window.screenTop, window.outerWidth, window.outerHeight);
	}


	function ResizeFenetreCourante() {
		if (divaltoGlob.pileFenetres.length == 1)
			$(window).trigger("resize");
		else {
			var hh = divaltoGlob.pileFenetres.Sommet().codeJquery;
			var ty = hh.dialog("option", "height");
			var tx = hh.dialog("option", "width");
			TraiterResizeFenetre(divaltoGlob.pileFenetres.Sommet(),tx, ty);
		}
	}

	function TraiterResizeFenetre(fenetre,nouvelleLargeur, nouvelleHauteur) {
	    var largeurFenetreXwinOrg = fenetre.tailleFenetreXOrg;
	    var hauteurFenetreXwinOrg = fenetre.tailleFenetreYOrg;

	    // je balaye toutes les pages de la fenetre courante
	    fenetre.balayerPages(function (indice, page) {

	        TraiterResizeUnePage(page, largeurFenetreXwinOrg, hauteurFenetreXwinOrg, nouvelleLargeur, nouvelleHauteur);
	    });
	}

	$(document).on("resizeDivaltoGrille", function (event, param) {
	    // evenent resize grille
	    // var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);
	    var $target = $(event.target);
	    var idpage = event.target.id;
	    var type = $target.attr("data-divalto-type");

	    if (type == "grille") {
	        $target.divaltoGrille("modifierTailleGrille",param.originalSize.width,param.originalSize.height,param.size.width,param.size.height);
	    }
	    else {
	        var page = divaltoGlob.pileFenetres.Sommet().chercherPage(idpage);
	        TraiterResizeUnePage(page, page.taillePageXOrg, page.taillePageYOrg, param.size.width, param.size.height);
	    }


	});


	function TraiterResizeUnePage(page, largeurFenetreXwinOrg, hauteurFenetreXwinOrg, nouvelleLargeur, nouvelleHauteur)
    {
	    var offsetLargeur = nouvelleLargeur - largeurFenetreXwinOrg;
	    var offsetHauteur = nouvelleHauteur - hauteurFenetreXwinOrg;

		if (offsetHauteur != 0) {

			// si la page est attachée en bas, je la décale et tous les objets qu'elle contient sont décalés automatiquement
			if (page.attachementBas == true) {
				var origine = page.positionPageYOrg;
				page.codeJquery.css("top", Math.max(origine, origine + nouvelleHauteur - hauteurFenetreXwinOrg).toString() + "px");
				//page.codeJquery.animate({ top: nouvo }, 100,"easeInOutQuint");
			}

			// si la page est en hauteur variable, il faut déplacer les objets attachés et agrandir les objets en hauteur variable
			else if (page.hauteurVariable == true) {
				var origine = page.taillePageYOrg;
				page.codeJquery.height(Math.max(origine, origine + nouvelleHauteur - hauteurFenetreXwinOrg).toString() + "px");
				//page.codeJquery.animate({ top: nouvo }, 100,"easeInOutQuint");

				var grilles = page.codeJquery.find("*[data-divalto-type=grille][data-divalto-idpage=" + page.id + "]");
				if (grilles.length > 0) {
				    grilles.each(function () {
				        $this = $(this);
				        $this.divaltoGrille("modifierTailleGrille", largeurFenetreXwinOrg, hauteurFenetreXwinOrg, nouvelleLargeur, nouvelleHauteur);
				    });
				}
				    // une page avec des grilles ne peut contenir rien d'autre
				else {
				    var objets = $("*[data-divaltoAttachementBas][data-divalto-idpage=" + page.id + "]");
				    objets.each(function () {
				        $this = $(this);
				        var origine = parseInt(($this.attr("data-divalto-xy-org").split(","))[1]);
				        $this.css("top", (Math.max(origine, origine + nouvelleHauteur - hauteurFenetreXwinOrg)).toString() + "px");
				    });

				    objets = $("*[data-divaltoHauteurVariable][data-divalto-idpage=" + page.id + "]");
				    objets.each(function () {
				        $this = $(this);
				        var origine = parseInt($this.attr("data-divalto-hauteur-org"));
				        $this.height(Math.max(origine, origine + nouvelleHauteur - hauteurFenetreXwinOrg));
				    });
				}
				page.taillePageY = page.codeJquery.height();

			}
		}

		if (offsetLargeur != 0) {

			// si la page est attachée a droite je la décale et son contenu suit automatiquement
			if (page.attachementDroite == true) {
				var origine = page.positionPageXOrg;
				page.codeJquery.css("left", Math.max(origine, origine + nouvelleLargeur - largeurFenetreXwinOrg).toString() + "px");
				//page.codeJquery.animate({ top: nouvo }, 100,"easeInOutQuint");
			}

			// si la apge est en largeur variable, il faut décaler les objets attachés à droite et agrandir ceux qui sont en largeur variable
			else if (page.largeurVariable == true) {
				var origine = page.taillePageXOrg;
				page.codeJquery.width(Math.max(origine, origine + nouvelleLargeur - largeurFenetreXwinOrg).toString() + "px");
				//page.codeJquery.animate({ top: nouvo }, 100,"easeInOutQuint");


                // si une page contient une grille elle ne contient rien d'autre
				var grilles = page.codeJquery.find("*[data-divalto-type=grille][data-divalto-idpage=" + page.id + "]");
				if (grilles.length > 0) {
				    grilles.each(function () {
				        $this = $(this);
				        $this.divaltoGrille("modifierTailleGrille", largeurFenetreXwinOrg, hauteurFenetreXwinOrg, nouvelleLargeur, nouvelleHauteur);
				    });
				}
				else {
				    var objets = $("*[data-divaltoAttachementDroite][data-divalto-idpage=" + page.id + "]");
				    objets.each(function () {
				        $this = $(this);
				        var origine = parseInt(($this.attr("data-divalto-xy-org").split(","))[0]);
				        $this.css("left", (Math.max(origine, origine + nouvelleLargeur - largeurFenetreXwinOrg)).toString() + "px");
				    });

				    objets = $("*[data-divaltoLargeurVariable][data-divalto-idpage=" + page.id + "]");
				    objets.each(function () {
				        $this = $(this);
				        var origine = parseInt($this.attr("data-divalto-largeur-org"));
				        $this.width(Math.max(origine, origine + nouvelleLargeur - largeurFenetreXwinOrg));
				    });
				}
				page.taillePageX = page.codeJquery.width();
			}
		}
	}

	//********************************************************************************
	// Commandes émises vers le serveur
	// Attention, on en fait du JSON donc les noms sont les memes ici et dans le c#
	//********************************************************************************
	function uneCommande(_comm, _valeur, _compl) {
		this.commande = _comm;
		this.valeur = _valeur;
		this.compl = _compl;
	}

	function listeCommandes() {
		this.commandes = new Array;
	}


	//***********************************************************************************
	// faire la liste des idents de tableaux et l'ajouter en tete de la liste des params
	//***********************************************************************************
	function AjouterIdentsTableaux(listeParams) {
		var idents = "";
		$("table").each(function () {
			if (this.id != "")
				idents += this.id + ",";
		});
		if (idents != "") {
			idents = idents.substring(0, idents.length - 1);  // derniere ,
			var unecomm = new uneCommande("identsTables", idents, "");
			listeParams.commandes.unshift(unecomm);
		}
	}


	//**************************************************************************************
	// classe pour toutes mes variables globales
	//**************************************************************************************
	function classeDivaltoGlobal() {
		this.rappelerLeServeur = false;		// indique s'il faut immédiatement rappeler le serveur, 
														// c'est à dire qu'on n'attends pas une action utilisateur
		this.commandeRappelServeur = "";
		this.paramCommandeRappelServeur = "";

		this.bodyCourant = undefined;					// pointe sur l'element "page en cours". On y fait les insertions
		this.pileFenetres = new Array; 				// pile des fenetres. Une fenetre = liste de pages

		this.attenteInput   = false;
		this.attenteConsult = false;

		this.$objEnSaisie = null; 					// objet en saisie (a voir si pour les radio bouton on en met plusieurs)
		this.laisserPasserOpenMultiChoix = false;

		this.idMultiChoixOuvert = "0";
		this.idObjetFocusASupprimer = "0";
		this.nouveauPanelEnCoursDeCreation = null;
		this.nouvelleGrilleEnCoursDeCreation = null;
	}

	var divaltoGlob = new classeDivaltoGlobal;

//	var largeurFenetreXwinOrg = $(window).width();
//	var hauteurFenetreXwinOrg = $(window).height();


    /*
    		internal static void SetActiveGroup(IXwpfObject activeControl)
		{
			var window = activeControl.Page.Window;

			ResetActiveGroup(window); // garde-fou cas du passage input page A vers input page B : il faut razer la page A avant d'affecter le groupe courant page B

			var controlPresentation = activeControl.Presentation;

			foreach (var groupBox in window.ListOfGroupBoxes.Where(gb => gb.Page == activeControl.Page))
			{
				var groupBoxPresentation = groupBox.Presentation;
				groupBox.IsActive = controlPresentation.OriginalLeft >= groupBoxPresentation.OriginalLeft
					&& controlPresentation.OriginalTop >= groupBoxPresentation.OriginalTop
					&& controlPresentation.OriginalLeft + controlPresentation.OriginalWidth <= groupBoxPresentation.OriginalLeft + groupBoxPresentation.OriginalWidth
					&& controlPresentation.OriginalTop + controlPresentation.OriginalHeight <= groupBoxPresentation.OriginalTop + groupBoxPresentation.OriginalHeight;
			}
		}

    */

	function ResetGroupBoxesActifs(idpage) {
	    var objets = $("[data-DivaltoGroupBox][data-divalto-idpage=" + idpage + "]");
	    objets.each(function () {
	        var groupe = $(this);
	        groupe.divaltoGroupbox("groupBoxInactif");
	    });
	}

	function getPositionTailleOriginales($obj) {
	    var o = { top: 0, left: 0, height: 0, width: 0 };
	    var w = $obj.attr("data-divalto-xy-org").split(",");
	    o.top = parseInt(w[1]);
	    o.left = parseInt(w[0]);
	    o.height = parseInt($obj.attr("data-divalto-hauteur-org"));
	    o.width = parseInt($obj.attr("data-divalto-largeur-org"));
	    return o;
	}

	function getPositionTailleCourantes($obj) {
	    var o = { top: 0, left: 0, height: 0, width: 0 };
	    var off = $obj.offset();
	    o.top = off.top;
	    o.left = off.left;
	    o.width = $obj.width();
	    o.height = $obj.height();
	    return o;
	};


	function GestionGroupBoxes(obj) {

	    var pageDeLobjet = obj.attr("data-divalto-idpage");
	    ResetGroupBoxesActifs(pageDeLobjet);

        // tous les group box de la page active
//	    var objets = $("div.divaltoGroupBox-cadre"); //  [data-divalto-idpage=" + pageDeLobjet + "]");
	    var objets = $("[data-DivaltoGroupBox][data-divalto-idpage=" + pageDeLobjet + "]");
	    var o;
	    var g;
	    var w;

	    o = getPositionTailleCourantes(obj);

	    objets.each(function () {
	        var groupe = $(this);

	        g = getPositionTailleCourantes(groupe);

	        if (o.left >= g.left
                && o.top >= g.top
                && o.left + o.width <= g.left + g.width
                && o.top + o.height <= g.top + g.height) {
	            groupe.divaltoGroupbox("groupBoxActif");
	        }
	    });


	}


	// gestion des boutons valides
	// obj = objet à saisir
	// c'est la transcription de ManageValidButtons de DD
	//----------------------------------------------------------------------
	function GestionBoutonsValides(obj) {

		var pageDeLobjet = obj.attr("data-divalto-idpage");
		var pageDuBouton;
		var nomDuBouton;
		var listeBoutonsValides = obj.attr("data-divalto-boutonsValides");

		$("button[data-divalto-boutonDeMasque]").each(function () {
			var bouton = $(this);

			pageDuBouton = bouton.attr("data-divalto-idpage");
			nomDuBouton = bouton.attr("data-divalto-boutonNomSelection");

			if (pageDeLobjet == pageDuBouton) {

				if (nomDuBouton == undefined || (listeBoutonsValides != undefined && listeBoutonsValides.indexOf("@" + nomDuBouton + "@") != -1)) bouton.attr("boutonValide", "true");
				else bouton.attr("boutonValide", "false");

			}
			else {
				if (bouton.attr("data-divalto-boutonLocal") == "true") {
					bouton.attr("boutonValide", "false");
				}
				else {
					if (nomDuBouton == undefined)
						bouton.attr("boutonValide", "true");
					else {
						if (listeBoutonsValides != undefined && listeBoutonsValides.indexOf("@" + nomDuBouton + "@") != -1)
							bouton.attr("boutonValide", "true");
						else
							bouton.attr("boutonValide", "false");
					}
				}
			}
			var valide = (bouton[0] == obj[0]) || (bouton.attr("boutonValide") == "true"  && bouton.attr("data-divaltoVisibilite") != "Grise")
			bouton.button("option", "disabled", valide ? false : true);
		});
	}


	//******************************************************************************************
	// fonctions pour jqGrid
	//******************************************************************************************
	//http: //stackoverflow.com/questions/6575192/jqgrid-change-background-color-of-row-based-on-row-cell-value-by-column-name
	// retourne l'indice d'une colonne d'apres son nom
	var getColumnIndexByName = function (grid, columnName) {
		var cm = grid.jqGrid('getGridParam', 'colModel');
		for (var i = 0, l = cm.length; i < l; i++) {
			if (cm[i].name === columnName) {
				return i; // return the index
			}
		}
		return -1;
	}

	// retourne l'objet jquery pour une cellule de tableau
	var getCellJquery = function (grid, ligne, colonne) {
		var colIndex = getColumnIndexByName(grid, "c" + colonne);
		var tr = grid[0].rows.namedItem(ligne); // grid is defined as grid=$("#grid_id")
		var td = tr.cells[colIndex];
		var cell = $(td);
		return cell;
	}


	//******************************************************************************************
	// fonction principale de traitement des données recues
	// data = le buffer recu. C'est une structure JSON
	//******************************************************************************************
	function traiterDonneesRecuesDuServeur(data) {

		var commandes = $.parseJSON(data);					// on récupere une liste de commandes
		var $objetCourant;											// jquery de l'objet courant
		var i = 0;
		var s;
		var retour = true;
		var nblabels = 0;

		divaltoGlob.rappelerLeServeur = true;
		divaltoGlob.commandeRappelServeur = "cmdSuite";
		divaltoGlob.paramCommandeRappelServeur = "";
		divaltoGlob.attenteConsult = false;
		divaltoGlob.attenteInput = false;

		//		alert("debut");
		var hd = new Date();		// lecture de l'heure pour mesurer perfs

		// - pour chaque commande
		$.each(commandes.commandes, function (el, idx) {

			switch (this.commande) {

				// - nombre de lignes d'un tableau                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                               
				case "tabGetNbLig":
					divaltoGlob.rappelerLeServeur = true;
					divaltoGlob.commandeRappelServeur = this.commande;
					divaltoGlob.paramCommandeRappelServeur = "32"; //§!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! a faire !!!!!!!!!!!!!
					break;

				// en cas d'erreur détéctée dans la couche IIS.                                                                                                                                                                                                                                                 
				// la couche IIS garde le DVBuffer dans les données de session et me demande juste de le rappeler                                                                                                                                                                                                                                                
				// pour qu'il puisse transmettre le buffer à la couche xrtdiva                                                                                                                                                                                                                                                
				//------------------------------------------------------------------------------------------------                                                                                                                                                                                                                                               
				case "transmettreBufferGarde":
					divaltoGlob.rappelerLeServeur = true;
					divaltoGlob.commandeRappelServeur = this.commande;
					divaltoGlob.paramCommandeRappelServeur = "";
					break;

				// - entrée en consultation. !!!!!!!!!!!! a finaliser                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "xmeConsult":
					divaltoGlob.rappelerLeServeur = false;
					divaltoGlob.attenteConsult = true;
					break;

				case "xmeInput":
					{
						ResizeFenetreCourante(); // test pour que les objets soient bien repositionnés/agrandis si fenetre agrandie

						// - si un multi choix est encore ouvert, on le ferme
						if (divaltoGlob.idMultiChoixOuvert != "0") {
							var $afermer = $("#" + divaltoGlob.idMultiChoixOuvert);
							if ($afermer.length > 0)
								$afermer.select2("close");
							divaltoGlob.idMultiChoixOuvert = "0";
						}

						if (divaltoGlob.idObjetFocusASupprimer != "0") {
							var o = $("#" + divaltoGlob.idObjetFocusASupprimer);
							if (o.length > 0)
								o.removeClass("divaltoSaisie");
							divaltoGlob.idObjetFocusASupprimer = "0";
						}


						divaltoGlob.rappelerLeServeur = false;
						divaltoGlob.attenteInput = true;
						var idObj = this.compl;
						$objetCourant = $("#" + idObj);

						var type = $objetCourant.attr("data-divaltoType");
						if (type == 'multichoix') {
							var conteneur = $objetCourant.select2("container");
							conteneur.focus();
							conteneur.addClass("divaltoSaisie");

							console.log("divaltoSaisie sur " + conteneur.attr("id"));

							divaltoGlob.idObjetFocusASupprimer = conteneur.attr("id");
							divaltoGlob.$objEnSaisie = $objetCourant;
							if (divaltoGlob.laisserPasserOpenMultiChoix == true)
								$objetCourant.select2("open");
						}

						else {
							$objetCourant.addClass("divaltoSaisie");
							console.log("divaltoSaisie sur " + $objetCourant.attr("id"));
							divaltoGlob.idObjetFocusASupprimer = $objetCourant.attr("id");
							$objetCourant.focus();
							divaltoGlob.$objEnSaisie = $objetCourant;
						}
						GestionBoutonsValides($objetCourant);
						GestionGroupBoxes($objetCourant);
					}
					break;



				// - ajouter des classes de styles                                                                                                                                                                                                                                                
				//   a chaque fois qu'un nouveau style est rencontré JS l'ajoute a ma liste                                                                                                                                                                                                                                                
				//   Coté IIS je garde cette liste pour connaitre les paddings etc...                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "cssAjouts":

					$("#Head1").append($(this.valeur));
					break;

				// - Création d'un objet                                                                                                                                                                                                                                                
				//   en parametre on a le code HTML de l'objet                                                                                                                                                                                                                                                
				//   En sortie $objetCourant pointe sur l'objet (s'il existe déjà, pas de création)                                                                                                                                                                                                                                                
				//   Cette commande est suivie de propsObjet pour placer toutes les proprietes de l'objet                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "creerObjet":
					$objetCourant = $("#" + this.compl);
					if ($objetCourant.length == 0) {
					    $objetCourant = $(this.valeur);
					}
					break;

				// - création d'un bouton, on passe par .button qui est un objet jqueryUi                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "creerBouton":
					$objetCourant = $("#" + this.compl);
					if ($objetCourant.length == 0) {
						$objetCourant = $(this.valeur).button();
						$objetCourant.attr("data-divalto-boutonValide", "true");
						$objetCourant.attr("data-divalto-boutonDeMasque", "true"); 	// pour distinguer des autres boutons (multichoix etc)
					}
					break;


			    case "creerGroupBox":
			        {
			            $objetCourant = $("#" + this.compl);

			            // - c'est une création
			            //-------------------------------------------------------


                        // je pense que c'est pas utile de mettre le id, il sera mis apres !!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			            if ($objetCourant.length == 0) {
			                $objetCourant = $("<div id='" + this.compl + "'></div>").divaltoGroupbox();
			            }
			        }
			        break;


			    case "creerOnglet":
			        {
			            console.log("creation onglet");
			            var listeParams = $.parseJSON(this.valeur);
			            $.each(listeParams.commandes, function () {
			                switch (this.commande) {
			                    case "paramsCreation":
			                        params = this.valeur;
			                        break;
			                }
			            });
			            var objetParam = $.parseJSON(params);

			            $objetCourant = $("#" + this.compl);
			            if ($objetCourant.length != 0) {
			                //alert("création d'un onglet existant");
			                $objetCourant.remove();
			            }
			            var c = $("<div></div>");
			            c.attr("id", this.compl);
			            AjouterObjet(c, divaltoGlob.bodyCourant); 	// j'ajoute d'abord l'objet au dom 
			            $objetCourant = c.divaltoOnglet(objetParam);

			        }
			        break;

			    case "creerCaseACocher":
                    {
                        var nouvo = false;
			            $objetCourant = $("#" + this.compl);

			            console.log("debut creation case " + this.compl);

			            // - c'est une création
			            //-------------------------------------------------------
			            if ($objetCourant.length == 0) {
			                var listeParams = $.parseJSON(this.valeur);
			                $.each(listeParams.commandes, function () {
			                    switch (this.commande) {
			                        case "paramsCreation":
			                            params = this.valeur;
			                            break;
			                    }
			                });
			                var objetParam = $.parseJSON(params);

			                var c = $("<div></div>");
			                c.attr("id", this.compl);
			                AjouterObjet(c, divaltoGlob.bodyCourant); 	// j'ajoute d'abord l'objet au dom (sinon ca marche pas)

			                $mc = c.divaltoCaseACocher(objetParam); 	// je le transforme 
			                $objetCourant = c; 					        // les modifs suivantes (position, style iront sur celui là)
			                nouvo = true;
			                console.log("nouvelle case " + this.compl);
			            }
			            else {
			                console.log("case a cocher existe : " + this.compl);
			            }


			        }
                    break;

			    case "creerGrille":
			        {
			            var params;
			            $objetCourant = $("#" + this.compl);
			            if ($objetCourant.length == 0) {
			                var listeParams = $.parseJSON(this.valeur);
			                $.each(listeParams.commandes, function () {
			                    switch (this.commande) {
			                        case "paramsGrille":
			                            params = $.parseJSON(this.valeur);
			                            break;
			                    }
			                });

			                $objetCourant = $("<div></div>");
			                $objetCourant.divaltoGrille(params);
			                divaltoGlob.nouvelleGrilleEnCoursDeCreation = $objetCourant;
			            }
			            else
			                alert("grille existante");
			        }
			        break;

			    case "ajouterPanelAGrilleSiPasDedans":
			        {
			            var params = $.parseJSON(this.valeur);
			            //AjouterObjet(j, divaltoGlob.pileFenetres.Sommet().codeJquery);
			            var $grille = $("#" + params.idGrille);
			            var $cell = $grille.divaltoGrille("getCellule", params.iCellule);

			            var $old = $cell.find("#" + params.idPanel);
			            if ($old.length > 0) {
			                // alert("panel deja en grille"); // c'est pour voir si je passe ici, mais c'est un cas normal
			            }
                        else
			                AjouterObjet(divaltoGlob.nouveauPanelEnCoursDeCreation, $cell);
			            divaltoGlob.nouveauPanelEnCoursDeCreation = null;
                    }
			        break;

			    case "ajouterGrille":
			        {
			            var params = $.parseJSON(this.valeur);
			            //AjouterObjet(j, divaltoGlob.pileFenetres.Sommet().codeJquery);
			            if (params.idGrille != "0") {
			                var $grille = $("#" + params.idGrille);
			                var $cell = $grille.divaltoGrille("getCellule", params.iCellule);
			                AjouterObjet(divaltoGlob.nouvelleGrilleEnCoursDeCreation, $cell);
			            }
			            else
			                AjouterObjet(divaltoGlob.nouvelleGrilleEnCoursDeCreation, divaltoGlob.bodyCourant);

			            divaltoGlob.nouvelleGrilleEnCoursDeCreation = null;
			        }
			        break;

			    case "nouveauPanel":
			        {
			            var html = '<div id="' + this.valeur + '" style="position:absolute" ></div>';
			            var j = $(html);
			            //AjouterObjet(j, divaltoGlob.pileFenetres.Sommet().codeJquery);
			            //divaltoGlob.bodyCourant = j;
			            divaltoGlob.pileFenetres.Sommet().listePages.push(new unePage(j, this.valeur));
			            j.css("overflow", "auto");
			            j.css("height", "inherit").css("width","inherit");

			            divaltoGlob.nouveauPanelEnCoursDeCreation = j;
//			            divaltoGlob.bodyCourant = divaltoGlob.nouveauPanelEnCoursDeCreation;
			            console.log("nouveau panel : " + this.valeur);
			        }
			        break;




				// - création d'un multi choix, on passe par .select2 qui est un objet jqueryUi                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "creerMultiChoix":
					var noms, modeles, options, nouvoparam, params, $mc;
					var nouvo = false;
					$objetCourant = $("#" + this.compl);

					// - c'est une création
					//-------------------------------------------------------
					if ($objetCourant.length == 0) {
						var listeParams = $.parseJSON(this.valeur);
						$.each(listeParams.commandes, function () {
							switch (this.commande) {
								case "paramsCreation":
									params = this.valeur;
									break;
							}
						});

						// tous les parametres sont dans cette chaine au format JSON
						var objetParam = $.parseJSON(params);

						objetParam.formatSelection = eval(objetParam.formatSelection);
						objetParam.formatResult = eval(objetParam.formatResult);
						objetParam.escapeMarkup = eval(objetParam.escapeMarkup);

						var c = $("<input type='hidden' data-divaltoType='multichoix'/>");
						c.attr("id", this.compl);
						AjouterObjet(c, divaltoGlob.bodyCourant); 	// j'ajoute d'abord l'objet au dom (sinon ca marche pas)

						$mc = c.select2(objetParam); 		// je le transforme 
						$objetCourant = c; 					// les modifs suivantes (position, style iront sur celui là)
						nouvo = true;
					}
					else {
						// - c'est une modification
						//$objetCourant = $objetCourant.select2("container");
						var listeParams = $.parseJSON(this.valeur);
						$.each(listeParams.commandes, function () {
							switch (this.commande) {
								case "paramsCreation":
									params = this.valeur;
									break;
							}
						});

						// tous les parametres sont dans cette chaine au format JSON
						var objetParam = $.parseJSON(params);

						objetParam.formatSelection = eval(objetParam.formatSelection);
						objetParam.formatResult = eval(objetParam.formatResult);
						objetParam.escapeMarkup = eval(objetParam.escapeMarkup);


						// bidouille, si je dois recreer la liste des parametres, je recree l'objet
						// comme les proprietes de l'objet de référence (le input hidden) ont été mises à jour en meme temps
						// que celles du conteneur, ca marche. Sauf pour top et left, et tous les autres attachements !!!!!!!!!!!!!!!!!!! a finaliser
						if (objetParam.data.results.length > 0) {
							var toto = { data: "" };
							toto.data = objetParam.data.results;
							var offsetAvant = $objetCourant.offset();
							var top = $objetCourant.css("top");
							var left = $objetCourant.css("left");
							var height = $objetCourant.css("height");
							var width = $objetCourant.css("width");
							$objetCourant.removeClass('select2-offscreen'); 	// vu sur le net

							toto.formatSelection = objetParam.formatSelection;
							toto.formatResult = objetParam.formatResult;
							toto.escapeMarkup = objetParam.escapeMarkup;

							var mc = $objetCourant.select2(toto);
							$mc = mc.select2("container");
							$mc.css("left", left).css("top", top).css("height", height).css("width", width);

							// + transfert de tous les datas harmony
							var attributs = mc[0].attributes;
							$.each(attributs, function (key, attr) {
								if (attr.name.indexOf("data-divalto", 0) == 0) {
									$mc.attr(attr.name, attr.value);
								}
							});

							$mc = mc;



						}
						else
							$mc = $objetCourant;
					}

					// - il faut le faire que la premiere fois, sinon on passe plusieurs fois dans les gestionnaires d'ev
					if (nouvo == true) {
						$mc.on("open", function (e) {
							console.log("open multi choix");
							if (divaltoGlob.laisserPasserOpenMultiChoix == true) {
								divaltoGlob.laisserPasserOpenMultiChoix = false;
								divaltoGlob.idMultiChoixOuvert = $mc.attr("id");
								return true;
							}
							if (divaltoGlob.$objEnSaisie[0] == $(e.currentTarget)[0])	// je suis l'objet en cours de saisie
							{
								divaltoGlob.idMultiChoixOuvert = $mc.attr("id");
								return true;
							}
							e.preventDefault();
							e.stopPropagation();
							envoyerClic(1, e, null);
						});

						$mc.on("close", function (e) {
							divaltoGlob.idMultiChoixOuvert = "0";
						});
					}

					if (objetParam.position != "-1") {
						$mc.val(objetParam.position).trigger("change");
					}

					if (objetParam.autoOpen == true) {
						divaltoGlob.laisserPasserOpenMultiChoix = true;
					}

					break;

				// - création d'un tableau                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "creerObjetTableau":
					{
						var noms, modeles, options, nouvoparam, params, idpage;
						$objetCourant = $("#" + this.compl);
						if ($objetCourant.length == 0) {
							var listeParams = $.parseJSON(this.valeur);
							$.each(listeParams.commandes, function () {
								switch (this.commande) {
									case "tout":
										params = this.valeur;
										break;
									case "idObjet":
										ipage = this.compl;
										break;
								}
							});

							// tous les parametres sont dans cette chaine au format JSON
							var objetParam = $.parseJSON(params);

							//						objetParam.gridview = true;						// option qui parait t il augmente les perfs

							var c = $("<table></table>");
							c.attr("id", this.compl);
							//							c.attr("data-divalto-idpage", ipage);
							AjouterObjet(c, divaltoGlob.bodyCourant); 	// j'ajoute d'abord l'objet au dom (sinon ca marche pas)
							$objetCourant = c.jqGrid(objetParam); 		// je le transforme en tableau
						}
					}
					break;


				// - positionnement de la variable $objetCourant                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "setObjetCourant":
					$objetCourant = $("#" + this.valeur);

					if (this.valeur.slice(0, 5) == "s2id_") {
						var rrrr = 0;
					}

					break;

				// - Ajouter des lignes a un tableau                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "ajouterLignesTableau":
					{
						$objetCourant = $("#" + this.compl);
						var listeParams = $.parseJSON(this.valeur);
						$.each(listeParams.commandes, function () {
							switch (this.commande) {
								case "uneligne":
									{
										var tab = this.valeur.split(",");
										var idLigne = tab[0];
										var operation = tab[1];
										var indicePourInsertion = parseInt(tab[2]);

										var valeurs = $.parseJSON(this.compl);

										if (operation == "Nouvo") {

											// +1 car il y a l'entete qui occupe une case
											var tr = $objetCourant[0].rows[indicePourInsertion + 1];
											if (tr == undefined)
											// insertion en fin
												$objetCourant.jqGrid("addRowData", idLigne, valeurs);
											else
											// insertion avant
												$objetCourant.jqGrid("addRowData", idLigne, valeurs, "before", indicePourInsertion);
										}
										else {

											$objetCourant.jqGrid("setRowData", idLigne, valeurs);
										}
									}
									break;
							}
						});
					}
					break;

				// - changement du texte pour un entete de colonne                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "txtEnteteCol":
					$objetCourant.jqGrid("setLabel", this.valeur, this.compl);
					break;

				// - changement du fond d'une cellule  = ajout d'une classe                                                                                                                                                                                                                                               
				//   la nouvelle valeur est gardée dans data-divaltoFond ce qui permet de l'enlever                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "css-fond-cell":
					{
					    var tab = this.valeur.split(",");
						var lig = tab[0];
						var col = tab[1];
						var cell = getCellJquery($objetCourant, lig, col);
						var old = cell.attr("data-divaltoFond");
						if (old != null)
							cell.removeClass(old);
						if (this.valeur != "xxxx") {
							cell.addClass(this.compl);
							cell.attr("data-divaltoFond", this.compl);
						}
						else if (old != null)
							cell.removeAttr("data-divaltoFond");
					}
					break;

				// - changement de la police d'une cellule                                                                                                                                                                                                                                               
				//   la nouvelle valeur est gardée dans data-divaltoPolice ce qui permet de l'enlever                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "css-police-cell":
					{
						var tab = this.valeur.split(",");
						var lig = tab[0];
						var col = tab[1];
						var cell = getCellJquery($objetCourant, lig, col);
						var old = cell.attr("data-divaltoPolice");
						if (old != null)
							cell.removeClass(old);
						if (this.valeur != "xxxx") {
							cell.addClass(this.compl);
							cell.attr("data-divaltoPolice", this.compl);
						}
						else if (old != null)
							cell.removeAttr("data-divaltoPolice");
					}
					break;


				// - positionnement/taille de la page courante                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "positionPageCourante":
					{
						var t = this.compl.split(",");
						var x, y, lgx, lgy;
						x = t[0];
						y = t[1];
						lgx = t[2];
						lgy = t[3];
						var panel = parseInt(t[4]);

						if (panel == 0) {
						    divaltoGlob.bodyCourant.css("top", y.toString() + "px");
						    divaltoGlob.bodyCourant.css("left", x.toString() + "px");
						    divaltoGlob.bodyCourant.height(lgy);
						    divaltoGlob.bodyCourant.width(lgx);
						}

						var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);
						page.positionPageXOrg = parseInt(x);
						page.positionPageYOrg = parseInt(y);
						page.taillePageXOrg = parseInt(lgx);
						page.taillePageYOrg = parseInt(lgy);
						page.taillePageX = page.taillePageXOrg;
						page.taillePageY = page.taillePageYOrg;

						// - cela provoque le positionnement correct de la page si elle a des attachements
						//$(window).trigger("resize");
                        if (panel == 0)                         //!!!!!!!!!!!!!! a voir si il faut faire pour les panels
						    ResizeFenetreCourante();
					}
					break;

				case "fenetreTailleInitiale":
					{
						var libelle, num, lgxmin, lgymin;
						var t = this.valeur.split(",");
						libelle = t[0];
						num = t[1];
						lgxmin = parseInt(t[2]);
						lgymin = parseInt(t[3]);

						var fenetre = divaltoGlob.pileFenetres.Sommet();


						fenetre.setTailleOrg(lgxmin, lgymin);
						fenetre.libellePourTailleFenetre = libelle;
						fenetre.numeroPourTailleFenetre = num;

						// la feentre de l'explorateur peut etre plus grande si l'utilisateur l'a agrandie précédemment
						var lgxfinal, lgyfinal;
						var posx = 0, posy = 0;
						var positionUtilisateur = RechercherTaillePositionFenetre(libelle, fenetre.numeroPourTailleFenetre);

						if (positionUtilisateur != null) {
							lgxfinal = Math.max(lgxmin, positionUtilisateur.lgx);
							lgyfinal = Math.max(lgymin, positionUtilisateur.lgy);
							posx = positionUtilisateur.x;
							posy = positionUtilisateur.y;
						}
						else {
							lgxfinal = lgxmin;
							lgyfinal = lgymin;
						}
						if (divaltoGlob.pileFenetres.length == 1) {
							window.moveTo(posx, posy);
							window.resizeTo(lgxfinal, lgyfinal);

							//							alert("postionnement " + posx.toString() + "," + posy.toString() + "," + lgxfinal.toString() + "," + lgyfinal.toString());
							//							alert(window.screenLeft.toString() +"," + window.screenTop.toString() +"," + window.outerWidth.toString() +"," + window.outerHeight.toString());

						}
						else {

							//alert("fenetreTailleInitiale pour une dialog");
							var dial = divaltoGlob.pileFenetres.Sommet();
							var xy = [posx, posy];
							dial.codeJquery.dialog("option", "position", xy);
							dial.codeJquery.dialog("option", "width", lgxfinal);
							dial.codeJquery.dialog("option", "height", lgyfinal);
						}
					}
					break;

				case "attachementsPageCourante":
					{
						var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);
						var t = this.compl.split(",");
						page.attachementBas = (t[0] == "1" ? true : false);
						page.attachementDroite = (t[1] == "1" ? true : false);
						page.hauteurVariable = (t[2] == "1" ? true : false);
						page.largeurVariable = (t[3] == "1" ? true : false);
					}
					break;


				// - changement de page courante                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "pageCourante":
					{
						var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);
						if (page == null)
						    alert("page=null dans 'PageCourante'");
						if (this.compl) {
						    page.arretDemandeDeSaisie = this.compl;
						    page.numeroDePage = this.compl2;
						}
						divaltoGlob.bodyCourant = page.codeJquery;
						console.log("Page courante : " + this.valeur);
					}
					break;

				// - couleur de fond de la page                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "couleurPageCourante":
					divaltoGlob.bodyCourant.addClass(this.valeur);
					break;


				// - création d'une nouvelle page                                                                                                                                                                                                                                                
				//   on l'ajoute a la liste des pages de la fenetre en cours                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "nouvellePage":
					{
						var html = '<div id="' + this.valeur + '" style="position:absolute" ></div>';
						var j = $(html);
						AjouterObjet(j, divaltoGlob.pileFenetres.Sommet().codeJquery);
						divaltoGlob.bodyCourant = j;
						divaltoGlob.pileFenetres.Sommet().listePages.push(new unePage(j, this.valeur));
						console.log("nouvelle Page : " + this.valeur);
                    }
					break;

				// - retirer une page de l'écran                                                                                                                                                                                                                                                
				//   Je garde le code dans page.htmlDeLaPage comme ca je peux le restaurer                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "retirerPage":
					{
						var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);


                        // ca ne marche pas car les objets ne sont pas dupliqués en profondeur (les widget ne marchent plus)
						//page.htmlDeLaPage = page.codeJquery.html();
					    //				page.codeJquery.html("");
					    //  			page.codeJquery.html(page.htmlDeLaPage);

                        // ca c'est une solution qui marche
						//page.codeJquery.css("display", "none");
						//page.codeJquery.css("display", "block");

						page.htmlDeLaPage = page.codeJquery.detach();
						console.log("retirer Page : " + this.valeur);
                    }
					break;

				// - restauration d'une page                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "remettrePage":
					{
						var page = divaltoGlob.pileFenetres.Sommet().chercherPage(this.valeur);
						page.htmlDeLaPage.appendTo("#monbody");
						page.htmlDeLaPage = null;


						console.log("remettre Page : " + this.valeur);
                    }
					break;

				// - ouverture d'une nouvelle fenetre                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "ouvrirFenetre":
					{
						var params = $.parseJSON(this.valeur);
						var id; var tx; var ty; var libelle; var numpage; var tailleFixe = true;
						var couleurFond = ""; var titre = ""; var icone;
						$.each(params.p, function () {
							switch (this.n) {
								case "idFen":
									id = this.v;
									break;
								case "tx":
									tx = parseInt(this.v);
									break;
								case "ty":
									ty = parseInt(this.v);
									break;
								case "libelle":
									libelle = this.v;
									break;
								case "numpage":
									numpage = parseInt(this.v);
									break;
								case "tailleFixe":
									tailleFixe = (this.v == "true");
									break;
								case "titre":
									titre = this.v;
									break;
								case "couleurFond":
									couleurFond = this.v;
									break;
								case "icone":
									icone = this.v;
									break;
							}
						});

						var h = '<div id="' + id + '"></div>';
						var hh = $(h);


						// !!!!!!!!!!!!!!!! au pif pour l'instant a cause des ascenseurs
						tx += 40;
						ty += 55;

						// ! !!!!!!! a revoir !!!!!!!!!!!
						if (couleurFond != "") {
							hh.addClass(couleurFond);
						}

						hh.dialog({ modal: true, resizable: tailleFixe ? false : true, title: titre });



						//hh.dialog("option", "height", ty);
						//hh.dialog("option", "width", tx);


						var fen = new uneFenetre(id);

						fen.setTailleOrg(tx, ty);
						fen.libellePourTailleFenetre = libelle;
						fen.numeroPourTailleFenetre = numpage;
						fen.tailleFixeFenetre = tailleFixe;

						fen.codeJquery = hh;

						if (tailleFixe == false) {
							fen.codeJquery.on("dialogresize", function (event, ui) {
								TraiterResizeFenetre(divaltoGlob.pileFenetres.Sommet(), ui.size.width, ui.size.height);
							});
						}

						fen.codeJquery.on("dialogbeforeclose", function (event, ui) {
							if (divaltoGlob.attenteInput == true) {
								var event;
								event.shiftKey = false;
								event.altKey = false;
								event.ctrlKey = false;
								event.preventDefault();
								event.stopPropagation();
								EnvoyerTouche("F9", event, divaltoGlob.$objEnSaisie);
							}
							else {
								event.preventDefault();
								event.stopPropagation();
							}
						});

						divaltoGlob.pileFenetres.push(fen);
						divaltoGlob.bodyCourant = hh;

						// - position sauvée
						var lgxfinal, lgyfinal;
						var posx = 0, poxy = 0;
						var positionUtilisateur = null;

						positionUtilisateur = RechercherTaillePositionFenetre(libelle, fen.numeroPourTailleFenetre);

						if (positionUtilisateur != null) {
							if (tailleFixe) {
								lgxfinal = tx;
								lgyfinal = ty;
							}
							else {
								lgxfinal = Math.max(tx, positionUtilisateur.lgx);
								lgyfinal = Math.max(ty, positionUtilisateur.lgy);
							}
							posx = positionUtilisateur.x;
							posy = positionUtilisateur.y;
						}
						else {
							lgxfinal = tx;
							lgyfinal = ty;
						}

						var xy = [posx, posy];
						fen.codeJquery.dialog("option", "position", xy);
						fen.codeJquery.dialog("option", "width", lgxfinal);
						fen.codeJquery.dialog("option", "height", lgyfinal);

						// si on met l'icone il faut penser a la remettre si on change le titre. Ou alors on adapte le composant
						//												var aa = $('[aria-describedby="' + id + '"] div.ui-dialog-titlebar .ui-dialog-title');
						//												aa.prepend("<img src='images/majeur.bmp'>");

					}
					break;


				case "xmeTitle":
					{
						if (divaltoGlob.pileFenetres.length == 1) {
							window.document.title = this.valeur;
						}
						else {
							var dial = divaltoGlob.pileFenetres.Sommet();
							dial.codeJquery.dialog("option", "title", this.valeur);
						}
					}
					break;



				// - fermeture d'une fenetre                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "fermerFenetre":
					{
						var dial = divaltoGlob.pileFenetres.pop();

						// détruire les tableaux ?
						var xy = dial.codeJquery.dialog("option", "position");

						SauverTaillePositionFenetre(dial.libellePourTailleFenetre, dial.numeroPourTailleFenetre,
											xy[0], xy[1],
											dial.codeJquery.dialog("option", "width"),
											dial.codeJquery.dialog("option", "height")
											);
						dial.codeJquery.off("dialogbeforeclose");
						dial.codeJquery.dialog("close");
						dial.codeJquery.dialog("destroy");
						divaltoGlob.bodyCourant = divaltoGlob.pileFenetres.Sommet().codeJquery;
					}
					break;

				// - propriétés d'un objet                                                                                                                                                                                                                                                
				//   this.valeur = sous liste de propriétés à changer                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
				case "propsObjet":
					{
						var listeParams = $.parseJSON(this.valeur);
						$.each(listeParams.commandes, function () {
							switch (this.commande) {


								case "boutonsValides":
									$objetCourant.attr("data-divalto-boutonsValides", this.valeur);
									break;

								case "boutonLocal":
									$objetCourant.attr("data-divalto-boutonLocal", "true");
									break;

								case "boutonNomSelection":
									$objetCourant.attr("data-divalto-boutonNomSelection", this.valeur);
									break;


								case "caseChecked":
									$objetCourant.val(this.valeur == "true" ? 1 : 0);
									break;

								case "caseLibelle":
								    $objetCourant.divaltoCaseACocher("option", "libelle", this.valeur);
									break;

								case "tailleSaisie":
									$objetCourant.attr("maxlength", this.valeur);
									break;

								case "pointSequence":
									$objetCourant.attr("data-divalto-sequence", this.valeur);
									$objetCourant.attr("data-divalto-page", this.compl);
									break;

								case "texteChamp":
									//$objetCourant.attr("value", this.valeur);

									if ($objetCourant.attr("data-divaltoVisibilite") == "Illisible") {
										$objetCourant.val("xxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
										$objetCourant.data("valeurTexte", this.valeur);
									}
									else {
										$objetCourant.val(this.valeur);
									}
									break;


								case "lectureSeule":
									$objetCourant.attr("data-divalto-lectureSeule", this.valeur);
									break;


								// - texte d'un bouton                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                
								case "textBouton":
									$objetCourant.button("option", "label", this.valeur);
									break;


								// -  image d'un bouton                                                                                                                                                                                                                                                
								//    les images sont dans la feuille de styles                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "imagebouton":
									$objetCourant.button("option", "icons", { primary: "icodivalto_" + this.valeur });
									break;

								// - positionnement de l'action d'un objet bouton                                                                                                                                                                                                                                                
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "actionBouton":
									$objetCourant.attr("data-divalto-action", this.valeur);
									break;

								// - texte d'un label                                                                                                                                                                                                                                                
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "texteLabel":
									$objetCourant.html(this.valeur);
									break;

								// - texte d'un groupbox
                                //-------------------------------------------------------------------------------------------
							    case "texteGroupe":
							        $objetCourant.divaltoGroupbox("option", "libelle", this.valeur);
							        break;

							    case "couleurTitreGroupe":
							        {
							            $objetCourant.divaltoGroupbox("option", "couleurBandeau", this.valeur);
							        }
							        break;

							    case "policeTitreGroupe":
							        {
							            $objetCourant.divaltoGroupbox("option", "policeBandeau", this.valeur);
							        }
							        break;

							    case "paddingTitreGroupe":
							        {
							            $objetCourant.divaltoGroupbox("option", "paddingBandeau", this.valeur);
							        }
							        break;



								// - identifiant de l'objet                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "idObjet":
									$objetCourant.attr("id", this.valeur);
									$objetCourant.attr("data-divalto-idpage", this.compl);
									break;

								// - pour envoyer l'id de page a l'objet tableau                                                                                                                                    
								//----------------------------------------------------------------------------------------------                                                                                                                                   
								case "idPage":
									$objetCourant.attr("data-divalto-idpage", this.valeur);
									break;


								// - positionnement par css (relatif au parent)                                                                                                                                                                                                                                                 
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-position":
									$objetCourant.css("position", this.valeur);
									break;

								// -  hauteur                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-hauteur":
								    $objetCourant.height(this.valeur + "px");

								    if ($objetCourant.hasClass("ui-dialog") == false)           // groupbox
									    $objetCourant.css("line-height", this.valeur + "px"); // je ne sais plus pourquoi ?
									// en tous cas pour le tableau je ne passe pas par ici car cela me modifie la hauteur de chaque ligne

									$objetCourant.attr("data-divalto-hauteur-org", this.valeur);

									break;

								// -  largeur                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-largeur":
									$objetCourant.width(this.valeur);
									$objetCourant.attr("data-divalto-largeur-org", this.valeur);
									break;

								// -  position                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-xy":
									{
										$objetCourant.css("top", this.compl + "px"); // relatif au parent
										$objetCourant.css("left", this.valeur + "px");
										$objetCourant.attr("data-divalto-xy-org", this.valeur + "," + this.compl);
									}
									break;

								// -  angle                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-angle":
									{
										$objetCourant.css("transform", "rotate(" + this.valeur + ")");
									}
									break;


								// -  police                                                                                                                                                                                                                                                
								//    data-divaltoPolice me sert à garder la valeur afin de pouvoir l'enlever en cas de                                                                                                                                                                                                                                                 
								//    changement                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-police":
									{
										var old = $objetCourant.attr("data-divaltoPolice");
										if (old != null)
											$objetCourant.removeClass(old);
										if (this.valeur != "xxxx") {
											$objetCourant.addClass(this.valeur);
											$objetCourant.attr("data-divaltoPolice", this.valeur);
										}
										else if (old != null)
											$objetCourant.removeAttr("data-divaltoPolice");
									}
									break;

								// -  fond                                                                                                                                                                                                                                                 
								//    data-divaltoFond me sert à garder la valeur afin de pouvoir l'enlever en cas de                                                                                                                                                                                                                                                  
								//    changement                                                                                                                                                                                                                                                
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
								case "css-fond":
									{
										var old = $objetCourant.attr("data-divaltoFond");
										if (old != null)
											$objetCourant.removeClass(old);
										if (this.valeur != "xxxx") {
											$objetCourant.addClass(this.valeur);
											$objetCourant.attr("data-divaltoFond", this.valeur);
										}
										else if (old != null)
											$objetCourant.removeAttr("data-divaltoFond");
									}
									break;



								// -  bordure                                                                                                                                                                                                                                               
								//    data-divaltoBordure me sert à garder la valeur afin de pouvoir l'enlever en cas de                                                                                                                                                                                                                                                  
								//    changement                                                                                                                                                                                                                                                
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
								case "css-bordure":
									{
										var old = $objetCourant.attr("data-divaltoBordure");
										if (old != null)
											$objetCourant.removeClass(old);
										if (this.valeur != "xxxx") {
											$objetCourant.addClass(this.valeur);
											$objetCourant.attr("data-divaltoBordure", this.valeur);
										}
										else if (old != null)
											$objetCourant.removeAttr("data-divaltoBordure");
									}
									break;

							    // -  padding                                                                                                                                                                                                                                               
							    //    data-divaltopadding me sert à garder la valeur afin de pouvoir l'enlever en cas de                                                                                                                                                                                                                                                  
							    //    changement                                                                                                                                                                                                                                                
							    //----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
							    case "css-padding":
							        {
							            var old = $objetCourant.attr("data-divaltopadding");
							            if (old != null)
							                $objetCourant.removeClass(old);
							            if (this.valeur != "xxxx") {
							                $objetCourant.addClass(this.valeur);
							                $objetCourant.attr("data-divaltopadding", this.valeur);
							            }
							            else if (old != null)
							                $objetCourant.removeAttr("data-divaltopadding");
							        }
							        break;

								// -  positionnement des valeurs de base pour police fond padding                                                                                                                                                                                                                                               
								//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
								case "css-police-fond-padding":
									$objetCourant.addClass(this.valeur);
									break;

								case "code-page": 		//!!!!!!!!!!!!!!!!!!!! a tester !!!!!!!!!!!!!!
									$objetCourant.attr("data-divaltoCodePage", this.valeur);
									break;

								case "info-bulle":              // voir egalement pour onglet
									{
										$objetCourant.attr("title", this.valeur);
										if (typeof ($objetCourant.attr("data-divaltoInfoBulle")) != "undefined")
											$objetCourant.tooltip("destroy");
										$objetCourant.tooltip();
										//										if (typeof ($objetCourant.attr("data-divaltoInfoBulle")) == "undefined")
										//										{
										//											$objetCourant.tooltip({ content: this.valeur });
										//										}
										//										else
										//											$objetCourant.tooltip("option", "content", this.valeur);
										$objetCourant.attr("data-divaltoInfoBulle", this.valeur);

									}
									break;


								case "visibilite":
									{
										// Visible = 0,		Grise = 1,		Illisible = 2,		Cache = 3
										var old = $objetCourant.attr("data-divaltoVisibilite");
										$objetCourant.attr("data-divaltoVisibilite", this.valeur);

										if (old != this.valeur) {

											// je quitte le mode Illisible, je dois restaurer la valeur
											if (old == "Illisible") {
												$objetCourant.val($objetCourant.data("valeurTexte"));
												$objetCourant.removeData("valeurTexte");
											}
											if (this.valeur == "Illisible")
												$objetCourant.data("valeurTexte", $objetCourant.val());

											switch (this.valeur) {
												case "Visible":
													$objetCourant.css("visibility", "visible");
													$objetCourant.attr("data-divalto-enabled", "true");
													$objetCourant.css("background-color", "");
													break;
												case "Grise":
													$objetCourant.css("visibility", "visible");
													$objetCourant.attr("data-divalto-enabled", "false");
													$objetCourant.css("background-color", "rgb(224,224,244)");
													break;
												case "Illisible":
													$objetCourant.css("visibility", "visible");
													$objetCourant.attr("data-divalto-enabled", "false");
													$objetCourant.css("background-color", "rgb(224,224,244)");
													break;
												case "Cache":
													$objetCourant.css("visibility", "hidden");
													$objetCourant.attr("data-divalto-enabled", "false");
													$objetCourant.css("background-color", "");
													break;
											}
										}
									}
									break;

								case "attachements":
									{
										var t = this.valeur.split(",");
										if (t[0] == 1)
											$objetCourant.attr("data-divaltoAttachementBas", "1");
										if (t[1] == 1)
											$objetCourant.attr("data-divaltoAttachementDroite", "1");
										if (t[2] == 1)
											$objetCourant.attr("data-divaltoHauteurVariable", "1");
										if (t[3] == 1)
											$objetCourant.attr("data-divaltoLargeurVariable", "1");
									}
									break;

								default:
									alert("Parametre inconnu : " + this.commande);
							}
						});
						nblabels++;
						//	AjouterObjet($objetCourant, divaltoGlob.bodyCourant);
					}
					break;


				// - n'est plus utilisé je crois (peut etre par les maquettes)                                                                                                                                                                                                                                               
				//   SI ON VEUT L'UTILISER METTRE UN AUTRE NOM                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "html":
					$objetCourant = $(this.valeur);
					break;

				// - n'est plus utilisé je crois (peut etre par les maquettes)                                                                                                                                                                                                                                                 
				//   SI ON VEUT L'UTILISER METTRE UN AUTRE NOM                                                                                                                                                                                                                                               
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "script":
					s = this.valeur;
					eval(s);
					break;

				// -                                                                                                                                                                                                                     
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                 
				case "ajoutObjetCourant":
					AjouterObjet($objetCourant, divaltoGlob.bodyCourant);
					break;

				// - n'est plus utilisé je crois (peut etre par les maquettes)                                                                                                                                                                                                                                                   
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
				case "effacement":
					{
						var quoi = $("#" + this.valeur);
						quoi.html(" ");
					}
					break;


				// - n'est plus utilisé je crois (peut etre par les maquettes)                                                                                                                                                                                                                                                  
				//   SI ON VEUT L'UTILISER METTRE UN AUTRE NOM                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
				case "bouton":
					{
						var b = $(this.valeur);
						AjouterObjet(b, divaltoGlob.bodyCourant);
					}
					break;

				// - n'est plus utilisé je crois (peut etre par les maquettes)                                                                                                                                                                                                                                                  
				//   SI ON VEUT L'UTILISER METTRE UN AUTRE NOM                                                                                                                                                                                                                                                
				//----------------------------------------------------------------------------------------------                                                                                                                                                                                                                                                  
				case "onglet":
					s = this.valeur;
					eval(s);
					break;
				default:
					alert("Commande inconnue : " + this.commande);

			}
		});

		var hf = new Date();

		return retour;
	}

	//******************************************************************************************
	// ajout d'un ojet dans un objet jquery
	//******************************************************************************************
	function AjouterObjet(code, parent) {
	    parent.append(code);
	    //alert (code.css("z-index"));
	    //code.css("z-index", "auto"); // !!!!!!!!!!!!!!!!!!!!!!!!!!
	}


	function EnvoyerTouche(key, event, $target) {

//		var $target = $(event.currentTarget);

		var s = key + "," + event.shiftKey.toString() + "," + event.ctrlKey.toString() + "," + event.altKey.toString();

		var sparam = new listeCommandes;

		var scomm = new uneCommande("typeTouche", s);
		sparam.commandes.push(scomm);

		var node, type;

		node = $target[0].nodeName;
		type = $target[0].type;

		if ($target.attr("data-divaltoType") == "multichoix")
			type = "SELECT";

		var scomm = new uneCommande("typeObjet", node,type);
		sparam.commandes.push(scomm);

		var cp = $target.attr("data-divaltoCodePage");
		if (typeof (cp) == "undefined")
			cp = "0";

		scomm = new uneCommande("valeurObjet", $target.val(), cp);
		sparam.commandes.push(scomm);

		var chaineJson = JSON.stringify(sparam);

		var unecomm = new uneCommande("toucheFonction", $target.attr("id"), chaineJson);
		var _params = new listeCommandes;
		_params.commandes.push(unecomm);
		callServeur(_params);
	}


	function envoyerNotificationCaseACocher($target) {
	    var s = "";
	    var sparam = new listeCommandes;

	    var numpage = $target.attr("data-divalto-page");
	    if (typeof (numpage) == "undefined")
	        numpage = "0";

	    var page = divaltoGlob.pileFenetres.Sommet().chercherPageParNumero(numpage);
	    var arretDemandeSaisie = 0;
	    if (page != null)
	        arretDemandeSaisie = page.arretDemandeDeSaisie;

	    var seq = $target.attr("data-divalto-sequence");
	    if (typeof (seq) == "undefined")
	        seq = "0";

	    var scomm = new uneCommande("infosPage", seq.toString() + "," + numpage.toString() + "," + arretDemandeSaisie.toString());
	    sparam.commandes.push(scomm);

	    var scomm = new uneCommande("valeurCase", $target.val() == 1 ? "1" : "0");
	    sparam.commandes.push(scomm);

	    var chaineJson = JSON.stringify(sparam);

	    var unecomm = new uneCommande("notificationCaseACocher", $target.attr("id"), chaineJson);
	    var _params = new listeCommandes;
	    _params.commandes.push(unecomm);
	    callServeur(_params);
	}

	function envoyerClic(nombre, event, paramsOnglet) {
		var $target = $(event.currentTarget);
		var s = "";

		if (typeof(event.shiftKey) != "undefined")
			s = nombre.toString() + "," + event.which + "," + event.shiftKey.toString() + "," + event.ctrlKey.toString() + "," + event.altKey.toString();
		else
			s = nombre.toString() + "," + "1"         + "," + "false"                   + "," + "false"                  + "," + "false";

		var sparam = new listeCommandes;

		var scomm = new uneCommande("typeClic", s);
		sparam.commandes.push(scomm);


		var node, type;

		node = divaltoGlob.$objEnSaisie[0].nodeName;
		type = divaltoGlob.$objEnSaisie[0].type;

		if (divaltoGlob.$objEnSaisie.attr("data-divaltoType") == "multichoix")
			type = "SELECT";
		else if (divaltoGlob.$objEnSaisie.attr("data-divaltoType") == "caseACocher")
		    type = "CHECKBOX";

		var scomm = new uneCommande("typeObjet", node, type);

		sparam.commandes.push(scomm);

		var seq = $target.attr("data-divalto-sequence");
		if (typeof (seq) == "undefined")
			seq = "0";

		var numpage = $target.attr("data-divalto-page");
		if (typeof (numpage) == "undefined")
			numpage = "0";

		var page = divaltoGlob.pileFenetres.Sommet().chercherPageParNumero(numpage);
		var arretDemandeSaisie = 0;
		if (page != null)
			arretDemandeSaisie = page.arretDemandeDeSaisie;

		var scomm = new uneCommande("infosPage", seq.toString() + "," + numpage.toString() + "," + arretDemandeSaisie.toString());
		sparam.commandes.push(scomm);

		// attention il s'agit de la donnée que l'on quitte
		//!!!!!!!!!!!!!! revoir toutes les utilisations de $objEnSaisie et voir si non renseigné
		var cp = divaltoGlob.$objEnSaisie.attr("data-divaltoCodePage");
		if (typeof (cp) == "undefined")
			cp = "0";

		//if (divaltoGlob.$objEnSaisie[0].type == "checkbox") {
		//	var v = divaltoGlob.$objEnSaisie.prop("checked");
		//	scomm = new uneCommande("valeurObjet", v == "true" ? 1 : 0, cp);
		//}
	    //else
        // mon widget caseACocher a la fonction val
		scomm = new uneCommande("valeurObjet", divaltoGlob.$objEnSaisie.val(), cp);
		sparam.commandes.push(scomm);

		if (paramsOnglet != null) {
		    scomm = new uneCommande("clicOnglet", JSON.stringify(paramsOnglet));
		    sparam.commandes.push(scomm);
        }

		var chaineJson = JSON.stringify(sparam);

		var unecomm = new uneCommande("clicSouris", event.currentTarget.id, chaineJson);
		var _params = new listeCommandes;
		_params.commandes.push(unecomm);
		callServeur(_params);

	}

	//******************************************************************************************
	// Appel AJAX
	// Entree : params = liste de commandes à envoyer. On la transforme en JSON
	//                   coté IIS elle est recue comme pour un formulaire dans divaltoParametresAjax
	// Au premier chargement de la page, on appelle cette fonction pour démarrer le programme diva
	//******************************************************************************************
	function callServeur(params) {
		var calls = "";

		AjouterIdentsTableaux(params);
		chaineJson = JSON.stringify(params);


		var ajax = $.ajax("appelsajax.aspx", { datatype: "json",
			async: "false",
			data: "divaltoParametresAjax=" + chaineJson,
			type: "POST",
			error: function (xhr, texte) {
				alert("Erreur AJAX" + texte + xhr.responseText);
			},
			success: function (data, textStatus, jqXHR) {

				// en cas de succes (ce qui n'est pas un exploit) on traite les données recues
				//-----------------------------------------------------------------------------
				traiterDonneesRecuesDuServeur(data);


				// - apres avoir traité les données du serveur, soit on donne la main à l'utilisateur
				//   soit on rappelle le serveur
				// -----------------------------------------------------------------------------------
				if (divaltoGlob.rappelerLeServeur == true) {

					// pour ne pas faire monter la pile, je déclenche une fonction avec setTimeout
					//-----------------------------------------------------------------------------
					setTimeout(function () {

						var unecomm = new uneCommande(divaltoGlob.commandeRappelServeur);
						var _params = new listeCommandes;
						var chaineJson;

						if (divaltoGlob.paramCommandeRappelServeur != "")
							unecomm.valeur = divaltoGlob.paramCommandeRappelServeur;

						_params.commandes.push(unecomm);

						callServeur(_params);
					}, 0);
				}
			}
		});
	}



	//******************************************************************************************
	//******************************************************************************************
	// Fonction exécutée au début
	//******************************************************************************************
	//******************************************************************************************

	var touchesQuiValidentChamp = [ { code:27,nom:"Escape" },
										{ code: 9,nom: "Tab" } ,
										{ code:45,nom:"Insert"},
										{ code:13,nom: "Return" }			//!!!!!!!!!!!!!!!!!!!!!!!enter ???????????
										]

	var touchesQuiValidentMultiChoix = [{ code: 27, nom: "Escape" },
										{ code: 9, nom: "Tab" },
										{ code: 45, nom: "Insert" },
										{ code: 13, nom: "Return"}			//!!!!!!!!!!!!!!!!!!!!!!!enter ???????????
										]


	var touchesCtrl = [{ code: 65, nom: "A" },
							 { code: 66 , nom: "B" },
							 { code: 68, nom: "D" },
							 { code: 69, nom: "E" },
							 { code: 70, nom: "F" },
							 { code: 78, nom: "N" },
							 { code: 79, nom: "O" },
							 { code: 80, nom: "P" },
							 { code: 81, nom: "Q" },
							 { code: 82, nom: "R" },
							 { code: 83, nom: "S" },
							 { code: 84, nom: "T" },
							 { code: 85, nom: "U" },
							 { code: 87, nom: "W" },
							 { code: 89, nom: "Y" },
	]

	touchesQuiValidentChamp.chercher = function (quoi) {
		for (var i = 0; i < this.length; i++) {
			if (this[i].code == quoi)
				return this[i].nom;
		}
		return "";
	}

	touchesQuiValidentMultiChoix.chercher = function (quoi) {
		for (var i = 0; i < this.length; i++) {
			if (this[i].code == quoi)
				return this[i].nom;
		}
		return "";
	}

	touchesCtrl.chercher = function (quoi) {
		for (var i = 0; i < this.length; i++) {
			if (this[i].code == quoi)
				return this[i].nom;
		}
		return "";
	}


	//tester et faire pour les multi choix.
	//Multichoix bitmap

	function traiterTouche(event,$target, tableDesTouches) {
		if (event.keyCode == 16 || event.keyCode == 17 || event.keyCode == 123)    // 16=ctrl 17=alt F12=123 !!!!!!!!!!
			return;

		// si pas d'input en cours --> poubelle
		// voir également le clic sur le bouton de fermeture d'une fenetre
		if (divaltoGlob.attenteInput == false) {
			event.preventDefault();
			event.stopPropagation();
			return;
		}
		var key = "";

		if (event.keyCode >= 112 && event.keyCode <= 123) {
			key = "F" + (event.keyCode - 111).toString();
		}
		else {
			key = tableDesTouches.chercher(event.keyCode);
			if (key == "") {
				if (event.ctrlKey == true && event.altKey == false) {
					key = touchesCtrl.chercher(event.keyCode);
				}
			}
		}

		if (key != "") {
			event.preventDefault();
			event.stopPropagation();
			EnvoyerTouche(key, event, $target);
		}
	}

	function getIdMultiChoixFromContainer(idcont) {
		idcont = idcont.substring(5);
		return idcont;
	}

	function imageMultiChoix(item) {
		return "<img src='images/" + item.text + "' />";
	}
	function texteMultiChoix(item) {
		return item.text;
	}

	function _escapeMarkup(m) {
		return m;
	}

	$(document).ready(function () {

		// init de bodyCourant avec le tag présent dans la page
		divaltoGlob.bodyCourant = $("#monbody");

		// création de la premiere fenetre de la pile
		var fen = new uneFenetre("fenbase")

		fen.setTailleOrg($(window).width(), $(window).height());

		fen.codeJquery = divaltoGlob.bodyCourant;
		divaltoGlob.pileFenetres.push(fen);

		//		divaltoGlob.bodyCourant.on("click", "p", function (event) {
		//			alert("clic sur " + $(event.currentTarget).html() + $(event.currentTarget).attr("id"));
		//		});

		// positionnement des écouteurs
		// c'est valable pour tous les boutons qui seront créés ultérieurement
		//-------------------------------------------------------------------------------------
		$("body").on("click", "button", function (event) {
			var b = $(this);

			//- c'est peut etre pas malin de piéger tous les boutons car ils ne m'interessent pas forcément
			//			if (b.hasClass("ui-dialog-titlebar-close"))								// c'est un bouton de fermeture de fenetre !!!!!!!!!!
			//				return;


			var unecomm = new uneCommande("clicbouton", b.attr("id"), b.attr("data-divalto-action"));
			var _params = new listeCommandes;
			_params.commandes.push(unecomm);

			callServeur(_params);
		});

		document.oncontextmenu = function () { return false; }; 		// invalider le menu contextuel du navigateur par clic droit

		$("body").on("mousedown", "input", function (e) {

			console.log("mousedown");

			// clic gauche sur moi meme
			if (divaltoGlob.$objEnSaisie[0] == $(e.currentTarget)[0] && e.which == 1)
				return;

			e.preventDefault();
			e.stopPropagation();

			var $target = $(e.currentTarget);

			if ($target.attr("data-divalto-lectureSeule") == "true" || $target.attr("data-divalto-enabled") == "false")
				return;


			envoyerClic(1, e, null);
		});

        // evenement clic sur une case à cocher
		$("body").on("beforeModif", "[data-divaltoType=caseACocher]", function (e) {

		    console.log("clic sur case a cocher");

            // sur moi meme ?
		    if (divaltoGlob.$objEnSaisie[0] == $(e.currentTarget)[0]) {
		        console.log("   je suis deja en saisie");
		        if (divaltoGlob.$objEnSaisie.divaltoCaseACocher("option", "notification") == true) {
		            console.log("   notification");
		            e.preventDefault();
		            e.stopPropagation();
		            divaltoGlob.$objEnSaisie.divaltoCaseACocher("inverserLaValeur");
		            envoyerNotificationCaseACocher($(e.currentTarget));
		        }
		        return;
		    }

		    e.preventDefault();
		    e.stopPropagation();

		    var $target = $(e.currentTarget);

		    if ($target.attr("data-divalto-lectureSeule") == "true" || $target.attr("data-divalto-enabled") == "false")
		        return;

		    envoyerClic(1, e , null);
		});

		$("body").on("keydown", "[data-divaltoType=caseACocher]", function (event) {
		    if (event.keyCode == 32)
		        $(event.currentTarget).divaltoCaseACocher("simulerClick");
            else
		        traiterTouche(event, $(event.currentTarget), touchesQuiValidentChamp);
		});

		$(document).on("clickOnglet", function (event, params) {
		    //	if (!application.AttenteConsult && !application.AttenteInput) return; // garde-fou !!!! a faire !!!!!!!!!!!!!!
//		    var unecomm = new uneCommande("clicOnglet", event.currentTarget.id, JSON.stringify(params));
	//	    var _params = new listeCommandes;
		//    _params.commandes.push(unecomm);
		    //   callServeur(_params);
		    envoyerClic(1, event, params);
		});


		//		// clic souris sur les input
		//		$("body").on("click", "input", function (event) {


		//			event.preventDefault();
		//			event.stopPropagation();
		//		//	EnvoyerTouche(key, event);


		//		});

		$("body").on("dblclick", "input", function (e) {


			// clic gauche sur moi meme
			if (divaltoGlob.$objEnSaisie[0] == $(e.currentTarget)[0] && e.which == 1) return;

			e.preventDefault();
			e.stopPropagation();


			var $target = $(e.currentTarget);

			if ($target.attr("data-divalto-lectureSeule") == "true" || $target.attr("data-divalto-enabled") == "false")
				return;


			envoyerClic(2, e , null);
		});


		// - keydown sur un conteneur de multichoix
		$("body").on("keydown", ".select2-container", function (event) {
			// - retrouver l'element d'apres le container
			traiterTouche(event, $("#" + getIdMultiChoixFromContainer(event.target.id)), touchesQuiValidentMultiChoix);
		});


		$("body").on("keydown", function (event) {
			console.log("keydown global");
		});



		$("body").on("keydown", "input", function (event) {
			traiterTouche(event, $(event.currentTarget), touchesQuiValidentChamp);
		});


		$("body").on("tooltipopen", function (event, ui) {
			// je ne veux pas que ca s'ouvre quand on prnd le focus
			// d'autant plus que ca merde ! Je donne le focus pour entrer en saisie et ca ouvre l'infobulle qui ne s'efface plus
			if (event.originalEvent.type == "focusin")
				$("#" + event.target.id).tooltip("close");

		});

		//----------------------- gestion des attachements / largeur et hauteur variable
		//----------------------------------------------------------------------------------------------------------------------------------
		//		var largeurFenetreNavigateurCourante = largeurFenetreXwinOrg;
		//		var hauteurFenetreNavigateurCourante = hauteurFenetreXwinOrg;

		// - Modif de la taille de la fenetre
		//   Voir les 						$(window).trigger("resize");
		$(window).resize(function () {
			var nouvelleLargeur = $(window).width();
			var nouvelleHauteur = $(window).height();
			TraiterResizeFenetre(divaltoGlob.pileFenetres[0], nouvelleLargeur, nouvelleHauteur);
		});





		// envoi de la premiere commande qui va lancer le programme
		// Le nom du programme etc. se trouve dans la variable parametresAppli qui est dans la page standard.
		var unecomm = new uneCommande("premierappel", true, JSON.stringify(parametresAppli));
		var _params = new listeCommandes;
		_params.commandes.push(unecomm);
		callServeur(_params);
	});



</script>
    
</body>

</html>



