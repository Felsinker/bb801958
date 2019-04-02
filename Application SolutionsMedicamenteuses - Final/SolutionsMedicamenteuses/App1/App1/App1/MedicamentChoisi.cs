using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class MedicamentChoisi
    {
        public static MedicamentChoisi instance;
        private string nom;
        private string molecule;
        private string info;
        private string id;
        private string couleur;
		private string concentrationInitiale;        
        private List<ClasseAge> classesAge = new List<ClasseAge>();

        private MedicamentChoisi()
        {

        }
        public string Nom { get => nom; set => nom = value; }
        public string Molecule { get => molecule; set => molecule = value; }
        public string Info { get => info; set => info = value; }
        public string Id { get => id; set => id = value; }
        public List<ClasseAge> ClassesAge { get => classesAge; set => classesAge = value; }
        public string Couleur { get => couleur; set => couleur = value; }
		public string ConcentrationInitiale { get => concentrationInitiale; set => concentrationInitiale = value; }

        public static MedicamentChoisi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicamentChoisi();
                }
                return instance;
            }
        }

        
    }
}
