 var date = new Date();
 var heure =date.getHours();
 var minute=date.getMinutes();
 var seconde=date.getSeconds();
var f = function() {
if(seconde<59)
    seconde++;
else{   
    minute++;
    seconde=00;
}
if(minute>59){
    heure++;
    minute=0;
}
document.getElementById("horloge").textContent=heure+" h "+minute+" min et "+seconde+" sec";
 setTimeout(f, 1000);
}
 setTimeout(f, 1000);

var maintenant=new Date();
var jour=maintenant.getDate();
var mois=maintenant.getMonth()+1;
var an=maintenant.getFullYear();
document.getElementById("D").textContent =jour+"/"+mois+"/"+an;