using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Reflection;


namespace App1
{
    public partial class MainPage : ContentPage
    {
        private SolutionsMedicamenteuses programme = new SolutionsMedicamenteuses();
        private ObservableCollection<ElementsView> classesAges = new ObservableCollection<ElementsView>();
        private ObservableCollection<ElementsView> classesPoids = new ObservableCollection<ElementsView>();
        private ObservableCollection<ElementsView> elements = new ObservableCollection<ElementsView>();//instance de la liste de l'auto completion

        public MainPage()
        {
            InitializeComponent();
            //définition de la bannière de l'application
            ToolbarItem TB = new ToolbarItem
            {
                Text = "Solutions Médicamenteuses"
            };
            selectAge.ItemsSource = classesAges;
            selectPoids.ItemsSource = classesPoids;
            listView.ItemsSource = elements;
            FillPickerAge();
            selectPoids.IsEnabled = false;
        }

        /// <summary>
        /// Va remplir le Picker de catégories d'âge en fonction des différentes classes d'âge contenues dans la base de données.
        /// </summary>
        private void FillPickerAge()
        {
            foreach (string s in programme.ListeClassesAge)
            {
                ElementsView catAge = new ElementsView
                {
                    Name = s
                };
                classesAges.Add(catAge);
            }
        }

        /// <summary>
        /// Correspond aux actions que le programme va exécuter lorsque l'utilisateur va changer de catégorie d'âge, en outre, remplir le Picker de poids en conséquence et demander au programme de définir l'âge du patient sur la valeur choisie.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectAge.SelectedIndex != -1)
            {
                string selection = selectAge.Items[selectAge.SelectedIndex];
                selectPoids.IsEnabled = true;
                classesPoids.Clear();
                string categorieAge = selection;
                FillPickerPoids(categorieAge);
                programme.InitAgePatient(selection);
            }
            else
            {
                selectPoids.IsEnabled = false;
                classesPoids.Clear();
                programme.InitAgePatient("");
            }
            verification();
        }

        /// <summary>
        /// Va remplir le Picker des différentes catégories de poids en fonction de la catégorie d'âge sélectionnée.
        /// </summary>
        /// <param name="s">Chaîne de caractères correspondant à la catégorie d'âge sélectionnée</param>
        private void FillPickerPoids(String s)
        {
            foreach (string s1 in programme.ListeClassesPoids(s))
            {
                ElementsView catPoids = new ElementsView
                {
                    Name = s1
                };
                classesPoids.Add(catPoids);
            }
        }

        /// <summary>
        /// Correspond aux actions que le programme va exécuter lorsque l'utilisateur va changer de catégorie de poids et demander au programme de changer le poids du patient avec la valeur sélectionnée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectPoids_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (selectPoids.SelectedIndex != -1)
            {
                programme.InitPoidsPatient(selectPoids.Items[selectPoids.SelectedIndex]);
            }
            else
            {
                programme.InitPoidsPatient("");
            }
            verification();
        }

        /// <summary>
        /// Va remplir la ListView en fonction de ce qui est saisi par l'utilisateur, à chaque modification de ce champ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Entry_TextChanged(object sender, TextChangedEventArgs e) //evenement lorsque l'on rentre ou efface un caractère dans le champ de la substance
        {
            elements.Clear();
            HideListViewAndBorder();
            string nom;
            string molecule;
            string saisie = entrySubstance.Text;
            foreach (KeyValuePair<string, string> solution in programme.ListeNomsMol)
            {
                molecule = solution.Key;
                nom = solution.Value;

                if (entrySubstance.Text.Length != 0 && programme.SaisieCorrespondBD(nom, molecule, saisie))
                {
                    elements.Add(new ElementsView { Name = nom + "/" + molecule });//on ajoute à l'auto complete (ou la liste) la string reconnue comme ayant un début similaire à la saisie qui s'affichera grâce au Binding Name dans l'xaml
                    DefSizeListViewAndBorder(elements.Count);
                    ShowListViewAndBorder();
                }
            }
        }

        /// <summary>
        /// Va cacher la liste de médicaments et ses contours.
        /// </summary>
        private void HideListViewAndBorder()
        {
            listView.IsVisible = false;
            contours.IsVisible = false;
        }

        /// <summary>
        /// Va afficher la liste de médicaments et ses contours.
        /// </summary>
        private void ShowListViewAndBorder()
        {
            listView.IsVisible = true;
            contours.IsVisible = true;
        }

        /// <summary>
        /// Va définir la taille de la listView et de sa bordure en fonction du nombre d'éléments correspondant à la saisie de l'utilisateur.
        /// </summary>
        /// <param name="nbElement">Nombre d'éléments correspondant à la saisie de l'utilisateur.</param>
        private void DefSizeListViewAndBorder(int nbElement)
        {
            if (nbElement < 4)
            {
                listView.HeightRequest = 40 * nbElement;
                contours.HeightRequest = 40 * nbElement; //si la liste filtrée compte moins de 4 éléments, la taille totale de la fenetre de l'auto complete est sizée en fonction de son contenu (40 étant la taille verticale d'une ligne de la liste)
            }
            else
            {
                listView.HeightRequest = 120; // =3*40
                contours.HeightRequest = 120; //si la liste filtrée compte plus de 3 éléments, la taille de la liste est sizé de manière a faire apparaitre 3 éléments maximum, avec un scroll pour accéder aux éléments suivants
            }
            contours.WidthRequest = listView.Width + 1;
        }

        /// <summary>
        /// Va cacher la listView et ses bordures lorsque le champ d'entrée de substance perdra le focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entrySubstance_Unfocused(object sender, FocusEventArgs e)
        {
            HideListViewAndBorder();
        }

        /// <summary>
        /// Va permettre la saisie d'une nouveau médicament directement en tappant sur le champ. En d'autres termes, va réinitialiser le champ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entrySubstance_Focused(object sender, FocusEventArgs e)
        {
            entrySubstance.Text = "";
            programme.ViderSingletonMedicamentChoisi();
            ClearLabels();
            verification();
        }

        /// <summary>
        /// Va remplir le champ de la substance avec la substance sélectionnée et va demander au programme de remplir le singleton MedicamentChoisi avec les valeurs correspondantes lorsqu'une ligne est cliquée.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            entrySubstance.Text = listView.SelectedItem.ToString();
            entrySubstance.WidthRequest = listView.Width;
            HideListViewAndBorder();
            programme.RemplirSingletonMedicamentChoisi(entrySubstance.Text);
            verification();
        }

        /// <summary>
        /// Va redonner les valeurs par défaut aux différents labels et leur réattribuer leur couleur par défaut et leur format.
        /// </summary>
        private void ClearLabels()
        {
            LabelInfos.Text = "?";
            labelTypeSolvant.Text = "?";
            labelUniteProduit.Text = "?";
            labelUniteSolution.Text = "?";
            labelUniteSolvant.Text = "?";
            labelQteSolvant.Text = "?";
            labelQteProduit.Text = "?";
            labelQteMelange.Text = "?";
            LabelPosologie.Text = "?";
            LabelConcentrationInit.Text = "?";
            LabelConcentrationMelange.Text = "?";
            entrySubstance.TextColor = Color.Black;
            labelTypeSolvant.TextColor = Color.Black;
            labelUniteProduit.TextColor = Color.Black;
            labelUniteSolution.TextColor = Color.Black;
            labelUniteSolvant.FontAttributes = FontAttributes.None;
            labelUniteSolvant.TextColor = Color.Black;
            labelQteSolvant.TextColor = Color.Black;
            labelQteProduit.TextColor = Color.Black;
            labelQteMelange.TextColor = Color.Black;
        }


        /// <summary>
        /// Va changer les couleurs des labels et du nom du médicament en fonction de la couleur saisie dans la base de données XML.
        /// </summary>
        /// <param name="couleur">Correspond à la couleur saisie dans la base de données XML pour la substance correspondante.</param>
        private void SetColorLabel (string couleur)
        {
            if(string.IsNullOrEmpty(couleur))
            {
                couleur = "Black";
            }

            ColorTypeConverter c = new ColorTypeConverter();
            c.ConvertFromInvariantString(couleur);

            entrySubstance.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelTypeSolvant.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelUniteProduit.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelUniteSolution.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelUniteSolvant.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelQteSolvant.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelQteProduit.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            labelQteMelange.TextColor = (Color)c.ConvertFromInvariantString(couleur);
            if(programme.GetUniteSolvant().Equals("Non Injectable"))
            {
                labelUniteSolvant.TextColor = Color.Red;
                labelUniteSolvant.FontAttributes = FontAttributes.Bold;
            }
           
        }

        /// <summary>
        /// Va rendre de nouveau visible les labels de titre.
        /// </summary>
        private void SetAllLabelTitreVisible()
        {
            LabelTitreMelange.IsVisible = true;
            LabelTitreProduit.IsVisible = true;
            LabelTitreSolvant.IsVisible = true;
            LabelTitrePosologie.IsVisible = true;
            LabelTitreConcentrationMelange.IsVisible = true;
        }

        /// <summary>
        /// Va vérifier qu'une classe d'âge et qu'une classe de poids ont été choisies ainsi qu'une substance. Dans le cas contraire, affichera progressivement des alertes pour informer l'utilisateur sur les données manquantes.
        /// Puis, va afficher si tout a été choisi, les informations de préparation de solution de la substance choisie en fonction de la classe d'âge et de la classe de poids du patient choisies.
        /// </summary>
        private void verification()
        {
            ClearLabels();
            SetAllLabelTitreVisible();
            if (string.IsNullOrWhiteSpace(entrySubstance.Text))
                LabelError.Text = "Choisissez une substance.";
            else
            {
                LabelInfos.Text = programme.GetInfoMedicament();
                if (string.IsNullOrWhiteSpace(programme.GetAgePatient()))
                {
                    LabelError.Text = "Choisissez une classe d'age."; 
                }
                else if (string.IsNullOrWhiteSpace(programme.GetPoidsPatient()))
                {
                    LabelError.Text = "Choisissez une classe de poids.";
                }
                else
                {
                    LabelError.Text = "";
                    labelTypeSolvant.Text = programme.GetSolvant();
                    labelUniteProduit.Text = programme.GetUniteProduit();
                    labelUniteSolution.Text = programme.GetUniteSolution();
                    labelUniteSolvant.Text = programme.GetUniteSolvant();
                    labelQteSolvant.Text = programme.GetQteSolvant();
                    labelQteProduit.Text = programme.GetQteProduit();
                    labelQteMelange.Text = programme.GetQteSolution();
                    LabelPosologie.Text = programme.GetPosologie();
                    LabelConcentrationInit.Text = programme.GetConcentrationInit();
                    LabelConcentrationMelange.Text = programme.GetConcentrationMelange();
                    SetColorLabel(programme.GetCouleurMedicament());

                    if (labelUniteProduit.Text.Equals("Pur"))
                    {
                        LabelTitreSolvant.IsVisible = false;
                        LabelTitreConcentrationMelange.IsVisible = false;
                    }
                    if(labelUniteSolvant.Text.Equals("Non Injectable"))
                    {
                        LabelTitreProduit.IsVisible = false;
                        LabelTitreSolvant.IsVisible = false;
                        LabelTitrePosologie.IsVisible = false;
                        LabelTitreMelange.IsVisible = false;
                        LabelTitreConcentrationMelange.IsVisible = false;
                    }
                }                
            }                
        }
    }
}