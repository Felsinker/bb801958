function raz() {
    document.getElementById("resultat").innerHTML = ""; //Efface le contenu du paragraphe 'resultat' 
}


function test(){
    
    var a = 0;
    var b = 0;
    var c = 0;
    
//La "première" partie de la fonction consiste à récupérer les valeurs des réponses données à chaque bouton radio
    
    var i;
    var tab1 = document.getElementsByName('r1');
    for (i=0;i<tab1.length;i++) //Parcourt les différents boutons radio de 'r1'
    {
        if(tab1[i].checked)
        {
            var s1 = tab1[i].value; //Quand un de bouton radio est coché, affecte sa valeur à la variable s1
            break;
        }
    }

    var i;
    var tab2 = document.getElementsByName('r2');
    for (i=0;i<tab2.length;i++)
    {
        if(tab2[i].checked)
        {
            var s2 = tab2[i].value;
            break;
        }
    }
    
    var i;
    var tab3 = document.getElementsByName('r3');
    for (i=0;i<tab3.length;i++)
    {
        if(tab3[i].checked)
        {
            var s3 = tab3[i].value;
            break;
        }
    }
    
    var i;
    var tab4 = document.getElementsByName('r4');
    for (i=0;i<tab4.length;i++)
    {
        if(tab4[i].checked)
        {
            var s4 = tab4[i].value;
            break;
        }
    }
    
    var i;
    var tab5 = document.getElementsByName('r5');
    for (i=0;i<tab5.length;i++)
    {
        if(tab5[i].checked)
        {
            var s5 = tab5[i].value;
            break;
        }
    }
    
    var i;
    var tab6 = document.getElementsByName('r6');
    for (i=0;i<tab6.length;i++)
    {
        if(tab6[i].checked)
        {
            var s6 = tab6[i].value;
            break;
        }
    }
    
    var i;
    var tab7 = document.getElementsByName('r7');
    for (i=0;i<tab7.length;i++)
    {
        if(tab7[i].checked)
        {
            var s7 = tab7[i].value;
            break;
        }
    }
    
    var i;
    var tab8 = document.getElementsByName('r8');
    for (i=0;i<tab8.length;i++)
    {
        if(tab8[i].checked)
        {
            var s8 = tab8[i].value;
            break;
        }
    }
    
    var i;
    var tab9 = document.getElementsByName('r9');
    for (i=0;i<tab9.length;i++)
    {
        if(tab9[i].checked)
        {
            var s9 = tab9[i].value;
            break;
        }
    }
    
    var i;
    var tab10 = document.getElementsByName('r10');
    for (i=0;i<tab10.length;i++)
    {
        if(tab10[i].checked)
        {
            var s10 = tab10[i].value;
            break;
        }
    }
    
// La "deuxième" partie de la fonction consiste à "regarder" chaque valeur des boutons radio afin d'incrémenter, selon la valeur obtenue, les variables a, b ou c. Ces 3 variables déterminent le résultat de la 3e partie
    
    if (s1 == 1){       //si la variable s1 vaut 1, ajoute +1 à la variable a
        a = a + 1;
    }
    else{ 
        if (s1 == 2){   //si la variable s1 vaut 2, ajoute +1 à la variable b
            b = b + 1;
        }
         else if (s1 != undefined){ //si la variable s1 vaut 3 ou + et est définie, ajoute +1 à la variable c
            c = c + 1;
        }
    }
    
    if (s2 == 1){
        a = a + 1;
    }
    else{ 
        if (s2 == 2){
            b = b + 1;
        }
        else if (s2 != undefined){
            c = c + 1;
        }
    }
    
    if (s3 == 1){
        a = a + 1;
    }
    else{ 
        if (s3 == 2){
            b = b + 1;
        }
        else if (s3 != undefined){
            c = c + 1;
        }
    }
    
    if (s4 == 1){
        a = a + 1;
    }
    else{ 
        if (s4 == 2){
            b = b + 1;
        }
        else if (s4 != undefined){
            c = c + 1;
        }
    }
    
    if (s5 == 1){
        a = a + 1;
    }
    else{ 
        if (s5 == 2){
            b = b + 1;
        }
        else if (s5 != undefined){
            c = c + 1;
        }
    }
    
    if (s6 == 1){
        a = a + 1;
    }
    else{ 
        if (s6 == 2){
            b = b + 1;
        }
        else if (s6 != undefined){
            c = c + 1;
        }
    }
    
    if (s7 == 1){
        a = a + 1;
    }
    else{ 
        if (s7 == 2){
            b = b + 1;
        }
        else if (s7 != undefined){
            c = c + 1;
        }
    }
    
    if (s8 == 1){
        a = a + 1;
    }
    else{ 
        if (s8 == 2){
            b = b + 1;
        }
        else if (s8 != undefined){
            c = c + 1;
        }
    }
    
    if (s9 == 1){
        a = a + 1;
    }
    else{ 
        if (s9 == 2){
            b = b + 1;
        }
        else if (s9 != undefined){
            c = c + 1;
        }
    }
    
    if (s10 == 1){
        a = a + 1;
    }
    else{ 
        if (s10 == 2){
            b = b + 1;
        }
        else if (s10 != undefined){
            c = c + 1;
        }
    }
    
//La "troisième" partie de la fonction consiste à afficher le résultat du questionnaire, grâce aux valeurs de a, b et c et donc grâce aux  réponses données dans le questionnaire
    
    if (a>=b && a>=c){
        document.getElementById("resultat").innerHTML = ("Résultat du quizz : Tu as le profil pour venir faire le DUT Informatique ! Tu t'es bien renseigné sur le DUT, tu sais que, malgré son libellé 'informatique', il y a d'autres matières qui n'appartiennent pas à ce domaine, telles que l'économie, l'expression/communication ou encore le fonctionnement des organisations et tu sais également comment se passe le DUT dans son ensemble. ");         //Si la variable a est plus grande que les variables b et c, affiche un contenu
    }
    else{ 
        if (b>=c){
           document.getElementById("resultat").innerHTML = ("Résultat du quizz : Tu peux venir faire un DUT mais renseigne-toi avant pour ne pas avoir de surprise lors de ta décision : regarde comment se passe le DUT, son contenu... En effet le DUT ne se résume pas à l'informatique, mais il y a aussi d'autres matières telles que l'économie, l'expression/communication ou encore le fonctionnement des organisations. ");      //Si la variable b est plus grande que les variables a et c, affiche un autre contenu
        }
        else{
            document.getElementById("resultat").innerHTML = ("Résultat du quizz : Aïe Aïe... Es-tu vraiment sûr d'être fait pour ce DUT ? Tu devrais peut-être te renseigner avant de choisir cette formation, pour que tu ne sois pas déçu : en effet le DUT informatique, ce n'est pas de l'informatique à 100%, il y a aussi d'autres matières plus générales qui permettent de découvrir le monde du travail, de l'entreprise telle que l'économie, l'expression/communication ou encore le fonctionnement des organisations mais également de l'anglais, des mathématiques... c'est aussi une formation où l'on travaille une bonne partie en groupe ! Mais si tu es prêt à faire tout cela, tu es la bienvenue !");     //Si la variable c est plus grande que les variables a et b, affiche encore un autre contenu
        }
    }
    
}
