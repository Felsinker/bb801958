function validerNom() {
    var n = document.forms["form"]["nom"].value;
    if (n == "") {
        return false;
    }
    else{
        return true;
    }
}

function validerPrenom() {
    var p = document.forms["form"]["prenom"].value;
    if (p == "") {
        return false;
    }
    else{
        return true;
    }
}

function validerEmail() {
    var e = document.forms["form"]["email"].value;
    if (e == "") {
        return false;
    }
    else{
        return true;
    }
}

function validerMsg() {
    var m = document.forms["form"]["msg"].value;
    if (m == "") {
        return false;
    }
    else{
        return true;
    }
}

function validerForm(f) {
    var Nom = validerNom(f.nom);
    var Prenom = validerPrenom(f.prenom);
    var Email = validerEmail(f.email);
    var Msg = validerMsg(f.msg);
    if (Nom && Prenom && Email && Msg) {
        return true;
    }
    else{
        alert("Veuillez remplir toutes les informations");
        return false;
    }
}