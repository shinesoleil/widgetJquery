// une grille est composée de cellules
// une cellule a un fils qui est son contenu
//             a un fils qui est sa rebarre
// J'ai mis 2 fils car sinon la rebarre fait apparaitre les ascenseurs a cause de "overflow"
// La gestion du contenu est simple vu qu'il hérite de la cellule.
// la fonction getCellule retourne l'objet contenu
// On génére l'evenement "resizeDivaltoGrille" pour les objets contenus dans les cellules, c'est à dire les panels page
// A finaliser : la gestion des triggers. Voir exactement ce que l'on fait
$.widget("divalto.divaltoGrille", {

    // options par défaut du widget 
    // modifiables à la construction 
    // mais aussi après la construction du widget 
    options: {
        vertical: true,
        nbcellules: 0,
        modePourcent: false,
        tailleX: 100,
        tailleY: 100,
        cellules: null  // tableau d'objets { 'avecResize': 1, 'posX': 0, 'posY': 50, 'tailleX': 360, 'tailleY': 100 }
    },

    _$cellules: null, //  new Array();       // tableau des cellules jquery { conteneur , contenu }
    // attention ca ne marche pas de faire new Array ici, toutes les instances vont partager le meme tableau

    // La fonction _create est appelé à la construction du widget
    // la variable d'instance this.element contient un objet jQuery
    // contenant l'élément sur lequel porte le widget
    // la variable options contient la fusion des options passées en parametres et des options par défaut
    _create: function () {
        var $self = this;
        this._$cellules = new Array();


        $self.element.css("position", "absolute");
        $self.element.css("top", "0px");
        $self.element.css("left", "0px");
        $self.element.css("width", $self.options.tailleX);
        $self.element.css("height", $self.options.tailleY);
        $self.element.attr("data-divalto-type", "grille");

        // - creation des cellules
        //   une cellule est composée de deux tag div imbriqués
        //          - le conteneur
        //          - le contenu
        //   Dans mon tableau je garde un pointeur sur les 2
        var cumulX = 0, cumulY = 0;

        $.each($self.options.cellules, function (i) {
            var cell = this;
            var $cell = $("<div></div>");

            $cell.css("position", "absolute");
            $cell.attr("data-divalto-type", "cellule-conteneur");
            $cell.css("border", "1px dotted");

            if ($self.options.modePourcent == false) {
                $cell.css("top", cell.posY + "px").css("left", cell.posX + "px");// relatif au parent
                $cell.width(cell.tailleX).height(cell.tailleY);
            }
            else {
                if ($self.options.vertical == false) {
                    var l = cell.tailleX * 100 / $self.options.tailleX;
                    $cell.width(l.toString() + "%").height("inherit");
                    $cell.css("left", cumulX.toString() + "%");
                    cell.taillePourcent = l;
                    cell.positionPourcent = cumulX;
                    cumulX += l;
                }
                else {
                    var l = cell.tailleY * 100 / $self.options.tailleY;
                    $cell.height(l.toString() + "%").width("inherit");
                    $cell.css("top", cumulY.toString() + "%");
                    cell.taillePourcent = l;
                    cell.positionPourcent = cumulY;
                    cumulY += l;
                }

            }

            var $contenu = $("<div data-divalto-type='cellule-contenu'></div>");
            if ($self.options.modePourcent == false)
                $contenu.css("overflow", "auto").css("width", "inherit").css("height", "inherit");
            else
                $contenu.css("overflow", "auto").css("width", "100%").css("height", "100%");

            $cell.append($contenu);

            if (cell.avecResize && i + 1 < $self.options.cellules.length) {
                if ($self.options.vertical == true)
                    $cell.resizable({ handles: 's', helper: "divalto-resizable-helper-s", ghost: "true" });
                else
                    $cell.resizable({ handles: 'e', helper: "divalto-resizable-helper-e", ghost: "true" });
            }

            //if (i % 2)
            //    $cell.css("background-color", "blue")
            //else
            //    $cell.css("background-color", "red")

            $self.element.append($cell);

            $self._$cellules.push({ "conteneur": $cell, "contenu": $contenu });



            // Debut de resize, je limite le déplacement à -20 du suivant
            //
            // j'ai essayé dans cette fonction d'utiliser $cell i etc... et ca marche !
            // ca ne me semble pas prudent, je préfère la méthode sûre ci-dessous
            //
            // event.data recoit l'objet passé en deuxieme argument
            $cell.on("resizestart", { "indice": i, "grille": $self }, function (event) {
                var $grille = event.data.grille;
                var indice = event.data.indice;
                var cellcour = $grille._$cellules[indice].conteneur;
                var cellsuiv = $grille._$cellules[indice + 1].conteneur;

                if ($grille.options.vertical == false) {
                    var old = cellcour.width();
                    var suiv = cellsuiv.width();
                    var nouv = old + suiv - 20;
                    cellcour.resizable("option", "maxWidth", nouv);
                }
                else {
                    var old = cellcour.height();
                    var suiv = cellsuiv.height();
                    var nouv = old + suiv - 20;
                    cellcour.resizable("option", "maxHeight", nouv);
                }
                event.stopPropagation();
            });


            // Fin de resize
            // Je met a jour la cellule a droite
            $cell.on("resizestop", { "indice": i, "grille": $self }, function (event, ui) {
                var indice = event.data.indice;
                var $grille = event.data.grille;
                var cellcour = $grille._$cellules[indice].conteneur;
                var cellsuiv = $grille._$cellules[indice + 1].conteneur;
                var diff = 0;
                var tailleOrgSuivante = {
                    "width": $grille.options.cellules[indice + 1].tailleX,
                    "height": $grille.options.cellules[indice + 1].tailleY
                };

                if ($grille.options.vertical == false) {
                    //  if (ui.size.width > ui.originalSize.width) {        // agrandissement
                    // pas besoin, ca marche avec des négatifs
                    diff = ui.size.width - ui.originalSize.width;

                    if ($grille.options.modePourcent == false) {

                        // maj de la taille de la suivante
                        $grille.options.cellules[indice + 1].tailleX -= diff;
                        cellsuiv.width($grille.options.cellules[indice + 1].tailleX);

                        // maj de la position de la suivante
                        $grille.options.cellules[indice + 1].posX += diff;
                        cellsuiv.css("left", $grille.options.cellules[indice + 1].posX.toString() + "px");

                        //maj de ma taille
                        $grille.options.cellules[indice].tailleX = ui.size.width;
                        cellcour.height($grille.options.cellules[indice].tailleY);// sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044
                    }
                    else {
                        var diffPourcent = diff * 100 / $grille.element.width();

                        // maj de la taille de la suivante
                        $grille.options.cellules[indice + 1].taillePourcent -= diffPourcent;
                        cellsuiv.width($grille.options.cellules[indice + 1].taillePourcent.toString() + "%");

                        // maj de la position de la suivante
                        $grille.options.cellules[indice + 1].positionPourcent += diffPourcent;
                        cellsuiv.css("left", $grille.options.cellules[indice + 1].positionPourcent.toString() + "%");

                        //maj de ma taille
                        $grille.options.cellules[indice].taillePourcent += diffPourcent;
                        cellcour.width($grille.options.cellules[indice].taillePourcent.toString() + "%");

                        // cellcour.height($grille.options.cellules[indice].tailleY);// sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044
                        cellcour.height("100%");  // sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044

                        //var total = 0;
                        //var cumulPos = 0;
                        //for (var i = 0 ; i < $grille.options.cellules.length ; i++) {
                        //    var c = $grille.options.cellules[i];
                        //    if (c.positionPourcent != cumulPos) {
                        //    }
                        //    cumulPos += c.positionPourcent;

                        //    total += $grille.options.cellules[i].taillePourcent;
                        //}
                        //if (total != 100) {
                        ////    alert("total " + total.toString());
                        //}

                    }


                }
                else {
                    diff = ui.size.height - ui.originalSize.height;
                    if ($grille.options.modePourcent == false) {

                        // maj de la taille de la suivante
                        $grille.options.cellules[indice + 1].tailleY -= diff;
                        cellsuiv.height($grille.options.cellules[indice + 1].tailleY);

                        // maj de la position de la suivante
                        $grille.options.cellules[indice + 1].posY += diff;
                        cellsuiv.css("top", $grille.options.cellules[indice + 1].posY.toString() + "px");

                        //maj de ma taille
                        $grille.options.cellules[indice].tailleY = ui.size.height;

                        cellcour.width($grille.options.cellules[indice].tailleX);// sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044
                    }
                    else {
                        var diffPourcent = diff * 100 / $grille.element.height();

                        // maj de la taille de la suivante
                        $grille.options.cellules[indice + 1].taillePourcent -= diffPourcent;
                        cellsuiv.height($grille.options.cellules[indice + 1].taillePourcent.toString() + "%");

                        // maj de la position de la suivante
                        $grille.options.cellules[indice + 1].positionPourcent += diffPourcent;
                        cellsuiv.css("top", $grille.options.cellules[indice + 1].positionPourcent.toString() + "%");

                        //maj de ma taille
                        $grille.options.cellules[indice].taillePourcent += diffPourcent;
                        cellcour.height($grille.options.cellules[indice].taillePourcent.toString() + "%");

                        // cellcour.height($grille.options.cellules[indice].tailleY);// sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044
                        cellcour.width("100%");  // sinon ca diminue de 1. C'est un bug http://bugs.jqueryui.com/ticket/8044

                    }
                }

                $grille.options.cellules[indice]  .tailleX = $grille._$cellules[indice].conteneur.width();
                $grille.options.cellules[indice+1].tailleX = $grille._$cellules[indice+1].conteneur.width();

                $grille.options.cellules[indice]    .tailleY = $grille._$cellules[indice].conteneur.height();
                $grille.options.cellules[indice + 1].tailleY = $grille._$cellules[indice + 1].conteneur.height();


                // envoyer a tous les enfants pour dire que leur parent a changé
                // pour la cellule courante :
                //      - ancienne taille, nouvelle taille
                // pour la cellule de droite
                //      - ancienne taille, nouvelle taille
                $grille._$cellules[indice].contenu.children("*").trigger("resizeDivaltoGrille", {
                    "originalSize": ui.originalSize,
                    "size": ui.size
                });

                var tailleSuivante = {
                    "width": $grille.options.cellules[indice + 1].tailleX,
                    "height": $grille.options.cellules[indice + 1].tailleY
                };

                $grille._$cellules[indice + 1].contenu.children("*").trigger("resizeDivaltoGrille", {
                    "originalSize": tailleOrgSuivante,
                    "size": tailleSuivante
                });
                event.stopPropagation();
            });
        });
    },


    // Il faut que j'installe une 7.3 pour voir ce que ca donne en xwpf
    // Voir si la page contenue dans le panel doit preciser ses attachements
    //      - si oui (il faut le rajouter), attention a l'algorithme standard
    // Est ce que l'algorithme standard ne devrait pas se contenter de traiter les pages de niveau 1
    //       - si oui, comment extraire uniquement les pages de ce niveau (voir 	    fenetre.balayerPages(function (indice, page) {
    //              . balayerPages ne peut pas convenir puisque toutes les pages sont au meme niveau
    //                      --> voir en Jquery comment faire 
    //              Attention : dans TraiterResizeUnePage, je cherche et traite les grilles uniquement de la page courante

    // TESTER !!!!!!!!!
    //    - page de fond avec/sans grille
    //    - fenetre (input en mode gosub) avec/sans  grille


    modifierTailleGrille: function (largeurXwinOrg, hauteurXwinOrg, nouvelleLargeur, nouvelleHauteur)
    {
        var offsetLargeur = nouvelleLargeur - largeurXwinOrg;
        var offsetHauteur = nouvelleHauteur - hauteurXwinOrg;

        for (var i = 0 ; i < this.options.nbCellules ; i++) {
            var cell = this._$cellules[i].conteneur;
            var pcell = this.options.cellules[i];

            var largeurLue = cell.width();
            var hauteurLue = cell.height();

            if (largeurLue != pcell.tailleX || hauteurLue != pcell.tailleY) {
                alert("ca a changé");
            }

        }

        
    },


    // fonction qui retourne le contenu de la cellule
    getCellule: function (i) {
        return this._$cellules[i].contenu; //    .children(":first");
    },

    _setOption: function (key, value) {
        // Surcharge de méthode, appelée a chaque changement d'option
        // ne pas oublier d'appelr la fonction standard sinon plus rien ne marche
        // Use the _setOption method to respond to changes to options
        switch (key) {
        }
        // and call the parent function too! Ca positionne l'option
        return this._superApply(arguments);
    },


});

