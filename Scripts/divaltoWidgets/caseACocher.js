/*

    Evenements associés : 
        "beforeModif"           : déclenché au clic sur la case. Si on fait PreventDefault, pas de de modif de la valeur sinon modif
        "afterModifUtilisateur" : apres la modif de la case par l'utilisateur, c'est à dire que la valeur a été changée par l'utilisateur

    Changement de la valeur en tapant espace au clavier ?????

*/
$.widget("divalto.divaltoCaseACocher", {

    // options par défaut du widget 
    // modifiables à la construction 
    // mais aussi après la construction du widget 
    options: {
        libelle: "libellé par defaut",       // texte
        texteAGauche: true,                  // position du texte
        notification: false,                 // point d'arret si clic
        valeur: 0,
    },

    _tagTexte: null,
    _tagImage: null,




    // La fonction _create est appelé à la construction du widget
    // la variable d'instance this.element contient un objet jQuery
    // contenant l'élément sur lequel porte le widget
    // la variable options contient la fusion des options passées en parametres et des options par défaut
    _create: function () {


        this._tagTexte = $("<span>" + this.options.libelle + "</span>");
        this._tagTexte.css("cursor", "default");
        //            var style = "background-image:url('images/caseNonCochee.png')";
        this._tagImage = $('<img src="images/caseNonCochee.png"/>');

        if (this.options.texteAGauche == true) {
            this._tagImage.css("float", "right");
            this.element.append(this._tagTexte);
            this.element.append(this._tagImage);
        }
        else {
            this._tagImage = $('<img src="images/caseNonCochee.png"/>');
            this._tagTexte.css("float", "right");
            this.element.append(this._tagImage);
            this.element.append(this._tagTexte);
        }

        this.element.addClass("divaltoCaseACocher");

        // tabindex=0 : permet que l'element recoive le focus
        //              cela fait marcher la frappe de espace pour changer la valeur (sous IE pas besoin), mais il le faut avec FireFox
        this.element.attr("data-divaltoType", "caseACocher").attr("tabindex","0");

        // evenement declenché quand la valeur a vraiment changé => adapter le dessin
        this.element.on("changeval", this, function (event) {
            var self = event.data;
            var valeur = self.element.val();
            if (typeof (valeur) != "undefined")
                valeur = parseInt(valeur);
            if (typeof (valeur) == "undefined" || valeur != self.options.valeur) {
                self.options.valeur = valeur;
                if (valeur == 0)
                    self._tagImage.attr("src", 'images/caseNonCochee.png');
                else
                    self._tagImage.attr("src", 'images/caseCochee.png');
            }

        });




        /*
        click : declencher un evenement beforeModif 
                si ce dernier renvoie ok on modifie la valeur
        */
        this._tagImage.on("click", this, function (event) {
        // this.element.on("click", this, function (event) {
                var self = event.data;

            // je déclenche un evenement pour savoir si j'ai le droit de changer la valeur
            var event = jQuery.Event("beforeModif");
            self.element.trigger(event);
            if (event.isDefaultPrevented()) {
                return;
            }
            self.inverserLaValeur();
        });
        //this._tagTexte.on("click", this, function (event) {
        //    // this.element.on("click", this, function (event) {
        //    var self = event.data;

        //    // je déclenche un evenement pour savoir si j'ai le droit de changer la valeur
        //    var event = jQuery.Event("beforeModif");
        //    self.element.trigger(event);
        //    if (event.isDefaultPrevented()) {
        //        return;
        //    }
        //    self.inverserLaValeur();
        //});

        


    },

    simulerClick : function ()
    {
        this._tagImage.trigger("click");
    },

    inverserLaValeur: function () {
        if (this.element.val() == 1)
            this.element.val(0);
        else
            this.element.val(1);

        var event1 = jQuery.Event("afterModifUtilisateur");
        this.element.trigger(event1);
    },

    _setOption: function (key, value) {
        // Surcharge de méthode, appelée a chaque changement d'option
        // ne pas oublier d'appelr la fonction standard sinon plus rien ne marche
        // Use the _setOption method to respond to changes to options
        switch (key) {
            case "libelle":
                this._tagTexte.html(value);
                break;
            case "notification":
                if (value == "true" || value == true || value == 1 || value == "1")
                    value = true;
                else
                    value = false;
                break;
        }
        // and call the parent function too! Ca positionne l'option

        this._super("_setOption", key, value);
        //return this._superApply(arguments);
    },

});

