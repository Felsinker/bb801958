using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class ClasseAge
    {

        private string categorie;
        private Dictionary<string,double> dosePoids = new Dictionary<string, double>();
        private string solvant;
        private string uniteProduit;
        private string uniteSolution;
        private string uniteSolvant;
        private string concentrationMelange;
        private double? qteSolvant;
        private string qteProduit;
        private string posologie;

        public ClasseAge(string categorie)
        {
            this.categorie = categorie;
        }

        public string Solvant { get => solvant; set => solvant = value; }
        public string UniteProduit { get => uniteProduit; set => uniteProduit = value; }
        public string UniteSolution { get => uniteSolution; set => uniteSolution = value; }
        public double? QteSolvant { get => qteSolvant; set => qteSolvant = value; }
        public string QteProduit { get => qteProduit; set => qteProduit = value; }
        public string Categorie { get => categorie; set => categorie = value; }
        public Dictionary<string, double> DosePoids { get => dosePoids; set => dosePoids = value; }
        public string UniteSolvant { get => uniteSolvant; set => uniteSolvant = value; }
        public string ConcentrationMelange { get => concentrationMelange; set => concentrationMelange = value; }
        public string Posologie { get => posologie; set => posologie = value; }
    }
}
