
FireFox Firebug ouverture toujours
	In Firefox you can set Firebug to be always on by opening about:config and setting extensions.firebug.allPagesActivation to on.

Vider les caches
	Dans IE c'est la derniere option des parametres avances		

DOC
	- il faut que le serveur laisse passer les <> !!!!! Sinon rien ne marche et on n'a pas d'erreur
	// http: //msdn.microsoft.com/en-us/library/hh882339.aspx

	 <system.web>
			<httpRuntime requestValidationMode="2.0" />
		  <pages validateRequest="false" />

IMAGES BITMAPS	
	- revoir l'affichage des images et bitmaps
		- onglet, boutons, images etc....
			Attention aux modifs du positionnement dans les onglets	


TABLEAUX

	+ VISIBILITE
		Dans GenericCell on a ca :
		case ProprietesWpf.CHAMP_VALEUR:													// Contenu du champ (string)
			buffer.GetStringCP(out text, CodePage);
			//		if ((!Presentation.VisibilityFlag || Presentation.Visibilite != Visibilite.Illisible) && column.Presentation.Visibilite != Visibilite.Illisible)
				Text = text;
				rowText = text;
		Le traitement de l'invisibilité doit etre fait coté client

	+ zonage d'apres valeurs
		http://stackoverflow.com/questions/6575192/jqgrid-change-background-color-of-row-based-on-row-cell-value-by-column-name

DESTRUCTION
	+ quand appeler la destruction des objets jquery ?
		- au dépilement de la fenetre il faudrait regarder quels sont les objets de ce genre et appeler leurs destructeurs ?
		- d'apres JPS rien à faire sauf si c'est dit explicitement dans la doc.
		- c'est pas compliqué de faire une requete jquery pour sortir tous les tableaux ayant la fenetre comme parent


- changement de taille de la fenetre contenant un objet tableau et qu'on est en saisie de ligne

- femeture de la fenetre par la croix,: on ne sait pas empecher la fermeture. Idée : tant pis si elle se ferme, on la rouvre à nouveau ...
- xmeSetIcon : j'ai rien fait ni pour la premiere fenetre ni pour les dialog
- a revoir, la couleur de fond pour les fenetres Dialog. Ca laisse un liseret blanc autour
- taille des fenetre Dialog : la taille que l'on passe es celle de toute la fenetre, bandeau et ascenseurs compris. Je rajoute au pif pour que ca marche 
- IE : je mémorise la taille des fenetres. Le probleme c'est que ca se décale a chaque fois. Si je positionne et dimensionne une fenetre et que je relis immédiatement
       ces valeurs, je trouve autres chose.

- champs illisibles : Leur valeur transite entre le client et le serveur. En WPF on peut donc la voir en regardant la trame (difficile). Par contre en Html il suffit
  de regarder avec le debug (je l'ai mise dans $objetCourant.data pour la cacher un peu, mais qq qui connait le jquery n'aura pas de mal a trouver

- connaitre le nombre de lignes d'un tableau ??? je ne vois pas comment

- Revoir l'affichage de l'icone des boutons avec JPS. Ca ne s'affiche pas. +++ comment avoir la bonne taille ? 

- preparer pour hao pan
	+ MessageBox
	+ toolbar et menu
	+ groupe radio
	+ aaccordeon
	+ case à cocher
		- texte + case
		- taille = taille totale du conteneur
		- texte à gauche ou a droite
		- evenements : onchange = avant le changement, possibilité d'annuler le changement
		- positionnement de la valeur
		- readOnly
		- caché / grisé
		- la fonction val doit retourner true/false


Grilles / PANELS
	La fenetre contient la liste des pages (toutes les pages, peu importe leur niveau)
	Une page contient une liste de grilles

   Les panels sont des objets page

	Dans la lecteur d'une page on peut donc rencontrer ProprietesWpf.PAGE_DEBUT:
			- ca appelle page.ReadProperties (216), donc les objets de cette page vont se
			  mettre sous cette page
			- on l'ajoute a la liste de la fenetre et egalement dans la cellule

	Voir les 2 appartions de ProprietesWpf.PAGE_DEBUT

La premiere page est stockée dans winwo.mainGrid  (224)

	1 - lecture des proprietes de la grille + cellules
	2 - dans la page a jouter la liste des grilles
			private readonly Collection<Grid> listOfGrids = new Collection<Grid>();									// pour les grilles

	2 - SetGridCell doit etre entierement déportée coté client

	3 - dans la présentation des objets, envoyer gridId et cellIndex
	9 - lecture de la taille des cellules dans le registre


Est ce que je dois :
	- conserver la liste des grilles entre 2 appels ? 			//grid = GetGrid(gridId); BHGRILLE

	- mettre cette liste coté explorateur

Modifier l'adressage pour qu'il soit relatif a la cellule
