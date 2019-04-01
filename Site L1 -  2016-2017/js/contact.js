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

function validerTel() {
    var t = document.forms["form"]["tel"].value;
    if (t == "") {
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
    var Tel = validerTel(f.tel);
    var Msg = validerMsg(f.msg);
    if (Nom && Prenom && Email && Tel && Msg) {
        return true;
    }
    else{
        alert("Veuillez remplir toutes les informations");
        return false;
    }
}