

// A FINALISER
// multi ligne : pour l'instant il se met toujours en multi ligne
// ascenseur si debordement : non règlé pour l'instant
// bitmap

$.widget("divalto.divaltoOnglet", {

    // options par défaut du widget 
    // modifiables à la construction 
    // mais aussi après la construction du widget 
    options: {
        items:"",
        police:"",
        padding:"",
        hauteurImage:16,
        largeurImage:16,
        multiLigne:"",
        pointArret: 0,
        pageCourante:1
    },

    _tagTexte: null,
    _tagBarre: null,


    // La fonction _create est appelé à la construction du widget
    // la variable d'instance this.element contient un objet jQuery
    // contenant l'élément sur lequel porte le widget
    // la variable options contient la fusion des options passées en parametres et des options par défaut
    _create: function () {

        var $self = this;                                       // $ self est l'objet jqueryUI

        $self.element.addClass("ui-tabs ui-widget ui-corner-all divaltoTabs cssdivalto");
            
        var $ul = $("<ul></ul>");
        $ul.addClass("ui-tabs-nav ui-helper-reset ui-helper-clearfix ui-widget-header ui-corner-all cssdivalto");
        $ul.attr("role", "tablist");

        if ($self.options.police != "") {
            $self.element.addClass($self.options.police);
        }

        // - boucle sur les items
        $.each($self.options.items, function () {
            var $item = $("<li class='ui-state-default ui-corner-top' tabindex='0'></li>");
            var $lien = $('<a class="ui-tabs-anchor" tabindex="-1"></a>');
            $lien.append(this.texte);

            // image
            if (this.bitmap != null) {
                $img = $('<img src="images/majeur.bmp"/>');     //!!!!!!!!!!!!!!!!!! a finaliser
                $img.css("padding", "2px");
                $img.width($self.options.largeurImage);
                $img.height($self.options.hauteurImage);
                $lien.prepend($img);
            }

            // bulle
            if (this.bulle != null && this.bulle != "") {
                $item.attr("title", this.bulle);
                if (typeof ($item.attr("data-divaltoInfoBulle")) != "undefined")
                    $item.tooltip("destroy");
                $item.tooltip();
                $item.attr("data-divaltoInfoBulle", this.bulle);
            }

            // traitement pour le courant
            if ($self.options.pageCourante == this.numeroPage) {
                $item.addClass("ui-tabs-active ui-state-active");
                if (this.fond != null && this.fond != "")
                    $self.element.addClass(this.fond);

            }

            $item.attr("data-pageonglet", this.numeroPage.toString());

            // l'élément de la liste contient l'item qui contient le lien
            $item.append($lien);
            $ul.append($item);
            $lien.css("cursor", "default");


            if (this.visibilite == 1) {                         // grisé
                $item.css("opacity", "0.5");
            }
            else if (this.visibilite != 0) {
                $item.css("display","none");
            }


            // evenement clic
            $item.on("click", { "item": this , "onglet":$self }  , function (event) {
                var self = event.data;      // self est de la classe item+onglet

                var onglet = self.onglet;
                var item = self.item;

                event.preventDefault();
                event.stopPropagation();

                // numero arret + nouvelle page + ancienne page
                // alert("clic itempage=" + item.numeroPage.toString() + " idgroupe=" + onglet.element.attr("id"));
                if (onglet.options.pageCourante == item.numeroPage) {
                    return;
                }
                if (item.visibilite != 0)
                    return;

                onglet.element.trigger("clickOnglet", {
                    "idOnglet": onglet.element.attr("id"),
                    "pageOngletClick": item.numeroPage,
                    "anciennePage": onglet.options.pageCourante,
                    "arretOnglet": onglet.options.pointArret
                });
            });
        });
        $self.element.append($ul);
    },

    _setOption: function (key, value) {
        // Surcharge de méthode, appelée a chaque changement d'option
        // ne pas oublier d'appelr la fonction standard sinon plus rien ne marche
        // Use the _setOption method to respond to changes to options
        switch (key) {
        }
        // and call the parent function too! Ca positionne l'option
        return this._superApply(arguments);
    }
});



