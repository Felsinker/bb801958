using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace App1
{
    class SolutionsMedicamenteuses
    {
        private XDocument bdd = new XDocument();//instance de la base de données
        private XElement root;//déclaration de l'emplacement racine de la base de données
        private Dictionary<string, string> listeNomsMol = new Dictionary<string, string>();
        private List<string> listeClassesAge = new List<string>();
        private List<string> listeClassesPoids = new List<string>();

        public Dictionary<string, string> ListeNomsMol { get => listeNomsMol; set => listeNomsMol = value; }
        public List<string> ListeClassesAge { get => listeClassesAge; set => listeClassesAge = value; }

        public SolutionsMedicamenteuses()
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App1.Resource1).GetTypeInfo()).Assembly;
            Stream stream = assembly.GetManifestResourceStream("App1.testDB.xml");
            bdd = XDocument.Load(stream);
            root = bdd.Root;//récupère la racine de la base de données xml afin de situé l'emplacement initial de recherche
            InitListeNomsMol();
            InitListeClassesAge();
        }

        /// <summary>
        /// Va remplir une liste de catégories d'âge en fonction des différentes classes d'âge contenues dans la base de données.
        /// </summary>
        public void InitListeClassesAge()
        {
            var results = from age in bdd.Descendants("solution") select age.Descendants("classeAge").Attributes("categorie").Distinct();
            foreach(var age in results.FirstOrDefault())
            {
                listeClassesAge.Add(age.Value);
            }
            
        }

        /// <summary>
        /// Va remplir une liste de catégories de poids en fonction des différentes classes de poids contenues dans la base de données.
        /// </summary>
        public List<string> ListeClassesPoids(string s)
        {
            listeClassesPoids.Clear();
            var results = from age in bdd.Descendants("solution").Descendants("classeAge") where age.Attribute("categorie").Value.Equals(s) select age.Descendants("dosePoids").Distinct();
            foreach(var p in results.FirstOrDefault())
            { 
                listeClassesPoids.Add(p.Attribute("categoriePoids").Value);
            }   
            return listeClassesPoids;
        }

        /// <summary>
        /// Va initialiser la map stockant les molécules et noms des substances de la base de données.
        /// </summary>
        public void InitListeNomsMol()
        {
            foreach (var solution in bdd.Descendants("solution"))
            {
                listeNomsMol.Add(solution.Element("molecule").Value, solution.Element("nom").Value);                
            }
        }

        /// <summary>
        /// Va retirer les accents de la valeur saisie par l'utilisateur et de la valeur du nom et de la molécule de la substance correspondante avant de les passer en majuscule, puis, va les comparer.
        /// </summary>
        /// <param name="nom">Nom de la substance</param>
        /// <param name="molecule">Molécule de la substance</param>
        /// <param name="saisie">Valeur saisie par l'utilisateur</param>
        /// <returns>Vrai si les valeurs correspondent, faux dans le cas contraire.</returns>
        public bool SaisieCorrespondBD(string nom, string molecule, string saisie)
        {
            bool pareil = false;
            saisie = RemoveDiacritics(saisie).ToUpper();
            if (SaisieCorrespondMol(molecule, saisie) || SaisieCorrespondNom(nom, saisie))
            {
                pareil = true;
            }
            return pareil;
        }

        /// <summary>
        /// Va retirer les accents de la valeur saisie par l'utilisateur et de la valeur du nom de la substance correspondante avant de les passer en majuscule, puis, va les comparer.
        /// </summary>
        /// <param name="nom">Nom de la substance</param>
        /// <param name="saisie">Valeur saisie par l'utilisateur</param>
        /// <returns>Vrai si les valeurs correspondent, faux dans le cas contraire.</returns>
        private bool SaisieCorrespondNom(string nom, string saisie)
        {
            bool pareil = false;
            nom = RemoveDiacritics(nom).ToUpper();
            if (nom.StartsWith(saisie))
            {
                pareil = true;
            }
            return pareil;
        }

        /// <summary>
        /// Va retirer les accents de la valeur saisie par l'utilisateur et de la valeur de la molécule de la substance correspondante avant de les passer en majuscule, puis, va les comparer.
        /// </summary>
        /// <param name="molecule">Molécule de la substance</param>
        /// <param name="saisie">Valeur saisie par l'utilisateur</param>
        /// <returns>Vrai si les valeurs correspondent, faux dans le cas contraire.</returns>
        private bool SaisieCorrespondMol(string molecule, string saisie)
        {
            bool pareil = false;
            saisie = RemoveDiacritics(saisie).ToUpper();
            molecule = RemoveDiacritics(molecule).ToUpper();
            if (molecule.StartsWith(saisie))
            {
                pareil = true;
            }
            return pareil;
        }

        /// <summary>
        /// Enlève les accents d'une chaîne de caractères.
        /// </summary>
        /// <param name="text">Chaîne de caractères à laquelle on doit retirer les accents.</param>
        /// <returns>La chaîne de caractères sans accents.</returns>
        public static string RemoveDiacritics(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Va concaténer le nom et la molécule d'une substance sous la forme "nom/molécule".
        /// </summary>
        /// <param name="nom">Correspond au nom de la substance.</param>
        /// <param name="mol">Correspond à la molécule de la substance.</param>
        /// <returns></returns>
        public string ConcatNomMolSubstance(string nom, string mol)
        {
            string concatenation;
            concatenation = nom + "/" + mol;
            return concatenation;
        }
		
        /// <summary>
        /// Va vider tous les attributs du médicament choisi.
        /// </summary>
		public void ViderSingletonMedicamentChoisi()
		{
			MedicamentChoisi.Instance.Id = null;
            MedicamentChoisi.Instance.Info = null;
            MedicamentChoisi.Instance.Nom = null;
            MedicamentChoisi.Instance.Molecule = null;
            MedicamentChoisi.Instance.Couleur = null;
			MedicamentChoisi.Instance.ClassesAge.Clear();
		}

        /// <summary>
        /// Va remplir le singleton MedicamentChoisi en fonction des valeurs dans la base de données correspondant à la substance sélectionnée par l'utilisateur.
        /// </summary>
        /// <param name="saisie">Correspond à la substance sélectionnée par l'utilisateur</param>
        public void RemplirSingletonMedicamentChoisi(string saisie)
        {
            var results = from solution in bdd.Descendants("solution") where ConcatNomMolSubstance(solution.Element("nom").Value, solution.Element("molecule").Value).Equals(saisie) select solution;
            MedicamentChoisi.Instance.Id = results.FirstOrDefault().Attribute("id").Value;
            MedicamentChoisi.Instance.Info = results.FirstOrDefault().Element("infoSup").Value;
            MedicamentChoisi.Instance.Nom = results.FirstOrDefault().Element("nom").Value;
            MedicamentChoisi.Instance.Molecule = results.FirstOrDefault().Element("molecule").Value;
            MedicamentChoisi.Instance.Couleur = results.FirstOrDefault().Element("couleur").Value;
            MedicamentChoisi.Instance.ConcentrationInitiale = results.FirstOrDefault().Element("concentrationInitiale").Value;
            foreach (var c in results.FirstOrDefault().Descendants("classeAge"))
            {
                ClasseAge cAge = new ClasseAge(c.Attribute("categorie").Value);
                string uniteSolvant = c.Element("uniteSolvant").Value;
                string uniteProduit = c.Element("uniteProduit").Value;
                if (!string.IsNullOrEmpty(uniteSolvant))
                {
                    cAge.UniteSolution = c.Element("uniteSolution").Value;
                    cAge.UniteSolvant = uniteSolvant;
                    if (!cAge.UniteSolvant.Equals("Non Injectable"))
                    {
                        cAge.UniteProduit = c.Element("uniteProduit").Value;
                        cAge.Posologie = c.Element("posologie").Value;
                        if (!cAge.UniteProduit.Equals("qsp"))
                        {

                            cAge.ConcentrationMelange = c.Element("concentrationMelange").Value;
                        }
                        cAge.QteProduit = c.Element("qteProduit").Value;
                        cAge.QteSolvant = XmlConvert.ToDouble(c.Element("qteSolvant").Value);
                        cAge.UniteProduit = c.Element("uniteProduit").Value;
                        cAge.Solvant = c.Element("solvant").Value;

                        foreach (var d in c.Descendants("dosePoids"))
                        {
                            cAge.DosePoids.Add(d.Attribute("categoriePoids").Value, XmlConvert.ToDouble(d.Value));
                        }
                    }
                }
                else if (uniteProduit.Equals("Pur"))
                {
                    cAge.UniteProduit = uniteProduit;
                    cAge.UniteSolution = c.Element("uniteSolution").Value;
                    cAge.Posologie = c.Element("posologie").Value;
                    foreach (var d in c.Descendants("dosePoids"))
                    {
                        cAge.DosePoids.Add(d.Attribute("categoriePoids").Value, XmlConvert.ToDouble(d.Value));
                    }
                }
                MedicamentChoisi.Instance.ClassesAge.Add(cAge);
            }
        }

        /// <summary>
        /// Va changer le poids du patient en la valeur sélectionnée par l'utilisateur.
        /// </summary>
        /// <param name="selection">Correspond au poids du patient choisi par l'utilisateur.</param>
        public void InitPoidsPatient(string selection)
        {
            Patient.Instance.CategoriePoids = selection;
        }

        /// <summary>
        /// Va changer l'âge du patient en la valeur sélectionnée par l'utilisateur.
        /// </summary>
        /// <param name="selection">Correspond à l'âge du patient choisi par l'utilisateur.</param>
        public void InitAgePatient(string selection)
        {
            Patient.Instance.CategorieAge = selection;
        }

        /// <summary>
        /// Va récupérer la classe d'âge du patient.
        /// </summary>
        /// <returns>La classe d'âge du patient.</returns>
        public string GetAgePatient()
        {
            return Patient.Instance.CategorieAge;
        }

        /// <summary>
        /// Va récupérer la classe de poids du patient.
        /// </summary>
        /// <returns>La classe de poids du patient.</returns>
        public string GetPoidsPatient()
        {
            return Patient.Instance.CategoriePoids;
        }

        /// <summary>
        /// Récupère les informations complémentaires du médicament choisi.
        /// </summary>
        /// <returns>Les informations complémentaires du médicament choisi.</returns>
        public string GetInfoMedicament()
        {
            return MedicamentChoisi.Instance.Info;
        }

        /// <summary>
        /// Récupère la couleur d'affichage propre au médicament choisi.
        /// </summary>
        /// <returns>La couleur d'affichage du médicament choisi.</returns>
        public string GetCouleurMedicament()
        {
            return MedicamentChoisi.Instance.Couleur;
        }

        /// <summary>
        /// Va récupérer la posologie du médicament choisi.
        /// </summary>
        /// <returns>La posologie du médicament choisi.</returns>
        public string GetPosologie()
        {
            string posologie = "";
            ClasseAge c = GetClasseAge();
            if (!string.IsNullOrEmpty(c.Posologie))
            {
                posologie = c.Posologie;
            }
            return posologie;
        }

        /// <summary>
        /// Va récupérer la concentration initiale du médicament choisi.
        /// </summary>
        /// <returns>La concentration initiale du médicament choisi.</returns>
        public string GetConcentrationInit()
        {
            return MedicamentChoisi.Instance.ConcentrationInitiale;
        }

        /// <summary>
        /// Va récupérer la classe d'âge du médicament choisi correspondant à la classe d'âge du patient.
        /// </summary>
        /// <returns>La classe d'âge correspondant à la classe d'âge du patient.</returns>
        public ClasseAge GetClasseAge()
        {
            ClasseAge cAge = null;
            foreach (ClasseAge c in MedicamentChoisi.Instance.ClassesAge)
            {
                if (c.Categorie.Equals(Patient.Instance.CategorieAge))
                {
                    cAge = c;
                }
            }
            return cAge;
        }

        /// <summary>
        /// Va récupérer le solvant de la classe d'âge correspondant à celle du patient permettant de diluer la solution.
        /// </summary>
        /// <returns>Le nom du solvant permettant de diluer la solution.</returns>
        public string GetSolvant()
        {
            string solvant = "";
            ClasseAge c = GetClasseAge();
            if (!string.IsNullOrEmpty(c.Solvant))
            {
                solvant = c.Solvant;
            }
            return solvant;
        }

        /// <summary>
        /// Va récupérer l'unité du produit de la classe d'âge correspondant à celle du patient à l'origine de la solution.
        /// </summary>
        /// <returns>L'unité du produit à l'origine de la solution</returns>
        public string GetUniteProduit()
        {
            string uniteProduit = "";
            ClasseAge c = GetClasseAge();
            if (!string.IsNullOrEmpty(c.UniteProduit))
            {
                uniteProduit = c.UniteProduit;
            }
            return uniteProduit;
        }

        /// <summary>
        /// Va récupérer l'unité de la solution finale à injecter de la classe d'âge correspondant à celle du patient.
        /// </summary>
        /// <returns>L'unité de la solution finale à injecter.</returns>
        public string GetUniteSolution()
        {
                string uniteSolution = "";
                ClasseAge c = GetClasseAge();
                if (!string.IsNullOrEmpty(c.UniteSolution))
                {
                    uniteSolution = c.UniteSolution;
                }
                return uniteSolution;
        }

        /// <summary>
        /// Va récupérer l'unité du solvant nécessaire à l'obtention de la solution pour un patient d'une classe d'âge donnée.
        /// </summary>
        /// <returns>L'unité du solvant nécessaire.</returns>
        public string GetUniteSolvant()
        {
                string uniteSolvant = "";
                ClasseAge c = GetClasseAge();
                if (!string.IsNullOrEmpty(c.UniteSolvant))
                {
                    uniteSolvant = c.UniteSolvant;
                }
                return uniteSolvant;
        }
        /// <summary>
        /// Va récupérer la quantité de solvant nécessaire à mélanger pour obtenir la solution adéquate pour un patient d'une classe d'âge donnée.
        /// </summary>
        /// <returns>La quantité de solvant nécessaire.</returns>
        public string GetQteSolvant()
        {
                string qteSolvant = "";
                ClasseAge c = GetClasseAge();
                if (!string.IsNullOrEmpty(c.QteSolvant.ToString()))
                {
                    qteSolvant = c.QteSolvant.ToString();
                }
                return qteSolvant;
        }

        /// <summary>
        /// Va récupérer la quantité de produit à mélanger pour obtenir la solution adéquate pour une classe d'âge donnée.
        /// </summary>
        /// <returns>La quantité de produit à mélanger.</returns>
        public string GetQteProduit()
        {
                string qteProduit = "";
                ClasseAge c = GetClasseAge();
                if (!string.IsNullOrEmpty(c.QteProduit))
                { 
                    qteProduit = c.QteProduit.ToString();
                }
                return qteProduit;
        }

        /// <summary>
        /// Va récupérer la quantité de solution finale à injecter en fonction du poids et de l'âge du patient.
        /// </summary>
        /// <returns>La quantité de solution finale à injecter.</returns>
        public string GetQteSolution()
        {
            string qteSolution = "";
            ClasseAge c = GetClasseAge();
            if (c.DosePoids.Any())
            {
                foreach(KeyValuePair<string,double> dose in c.DosePoids)
                {
                    if (dose.Key.Equals(Patient.Instance.CategoriePoids))
                    {
                        qteSolution = dose.Value.ToString();
                    }
                }
            }
            return qteSolution;
        }

        /// <summary>
        /// Va récupérer la concentration en substance de la solution finale.
        /// </summary>
        /// <returns>La concentration en substance de la solution finale</returns>
        public string GetConcentrationMelange()
        {
            string concentrationMelange = "";
            ClasseAge c = GetClasseAge();
            if (!string.IsNullOrEmpty(c.ConcentrationMelange))
            {
                concentrationMelange = c.ConcentrationMelange.ToString();
            }
            return concentrationMelange;
        }
    }
}
