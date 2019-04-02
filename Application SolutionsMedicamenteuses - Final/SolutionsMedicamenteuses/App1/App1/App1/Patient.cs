using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{

    public class Patient
    {
        private static Patient instance = null;
        private string categoriePoids;
        private string categorieAge;

        private Patient()
        {

        }

        public static Patient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Patient();
                }
                return instance;
            }
        }

        public string CategoriePoids { get => categoriePoids; set => categoriePoids = value; }
        public string CategorieAge { get => categorieAge; set => categorieAge = value; }
    }
}
