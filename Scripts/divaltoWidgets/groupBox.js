//////////////////////////////////////////////////////////////////////////
//http: //www.novius-labs.com/developper-plugin-jquery-introduction,18.html
// Pour créer mon plugin il suffit d'appeller la méthode $.widget
// Le 1er paramètre est le nom de mon plugin préfixé par divalto. (le namespace du widget, ui étant le namespace de jQuery UI)
// Le 2eme paramètres est un objet json de paramétrage du plugin

// voir Propriete css overflow qui permet de ne pas déborder du parent

$.widget("divalto.divaltoGroupbox", {

    // options par défaut du widget 
    // modifiables à la construction 
    // mais aussi après la construction du widget 
    options: {
        libelle: "",                            // titre
        couleurBandeau: "",                     // classe css
        policeBandeau:"",                       // class css police
        paddingBandeau:"",
    },

    _tagTexte: null,
    _tagBarre: null,


    // La fonction _create est appelé à la construction du widget
    // la variable d'instance this.element contient un objet jQuery
    // contenant l'élément sur lequel porte le widget
    // la variable options contient la fusion des options passées en parametres et des options par défaut
    _create: function () {

        this._tagTexte = $("<span id='ui-id-1' >" + this.options.libelle + "</span>");
        this._tagBarre = $("<div class='ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix divaltoGroupBox-titre'></div>");

        if (this.options.couleurBandeau != "") {
            this._tagBarre.addClass(this.options.couleurBandeau);
        }

        this._tagBarre.append(this._tagTexte);

        this.element.addClass('ui-dialog ui-widget .divalto-widget-content ui-corner-all divaltoGroupBox-cadre');
        this.element.css({
            'position': 'absolute',
            'height': 'inherit',
            'width': 'inherit',
            'display': 'block;',
        });

        this.element.attr({
            tabindex: '-1',
            role: 'dialog',
            'data-DivaltoGroupBox' : '',
            // "aria-describedby":'dialog' ,     // a voir , je ne sais pas trop a quoi ca sert
            // "aria-labelledby":'ui-id-1'
        });

        this.element.append(this._tagBarre);

    },

    _setOption: function (key, value) {
        // Surcharge de méthode, appelée a chaque changement d'option
        // ne pas oublier d'appelr la fonction standard sinon plus rien ne marche
        // Use the _setOption method to respond to changes to options
        switch (key) {
            case "libelle":
                this._tagTexte.html(value);
                break;

            case "couleurBandeau":
                if (this.options.couleurBandeau != "xxxx")
                    this._tagBarre.removeClass(this.options.couleurBandeau);
                if (value != "xxxx")
                    this._tagBarre.addClass(value);
                break;
            case "policeBandeau":
                if (this.options.policeBandeau != "xxxx")
                    this._tagTexte.removeClass(this.options.policeBandeau);
                if (value != "xxxx")
                    this._tagTexte.addClass(value);
                break;
            case "paddingBandeau":
                if (this.options.paddingBandeau != "xxxx") {
                    this._tagBarre.removeClass(this.options.paddingBandeau);
                }
                if (value != "xxxx") {
                    this._tagBarre.addClass(value);
                    // je rajoute des constantes pour avoir le meme look que wpf
                    var top = parseInt( this._tagBarre.css("padding-top"));
                    var bottom = parseInt(this._tagBarre.css("padding-bottom"));
                    this._tagBarre.css("padding-top", (top + 3).toString() + "px");
                    this._tagBarre.css("padding-bottom", (bottom + 5).toString()+ "px");
                }
                break;
        }
        // and call the parent function too! Ca positionne l'option
        return this._superApply(arguments);
    },

    groupBoxActif: function () {
        this._tagBarre.addClass("divaltoGroupBox-actif");
    },

    groupBoxInactif: function () {
        this._tagBarre.removeClass("divaltoGroupBox-actif");
    },

});
