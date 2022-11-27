using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Stagiaires_College
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.Eventing.Reader;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;

    public class Programme
    {
        public int id { get; set; }

        public string nom { get; set; }

        public int dureeEnMois { get; set; }

        public Programme(int id, string nom, int dureeEnMois)
        {
            this.id = id; // 7 chiffres only
            this.nom = nom; // testing programme name
            this.dureeEnMois = dureeEnMois; // minimun 1 , maxinmum 60 mois
        }

        public override string ToString()
        {
            return $"{this.id} {this.nom} {this.dureeEnMois}";
        }
    }

    public class Stagiaire
    {
        public int id { get; set; } // 7 chiffres only

        public string nom { get; set; } // string letters only 

        public string prenom { get; set; } // string letters only

        public int dateNaissance { get; set; } // string letters only
        public string sexe { get; set; } // only female or only male
        //          public int programme_id { get; set; }

        public Stagiaire(int id, string nom, string prenom, int dateNaissance, string sexe)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.sexe = sexe;
            //     this.programme_id = programme_id;

        }

        public override string ToString()
        {
            return $"{this.id} {this.nom} {this.prenom} {this.sexe}";
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private string ERREUR_RECOMMANCER_FORMULAIRE = "Veuillez recommencer";
        private ObservableCollection<Programme> programmes = new ObservableCollection<Programme>();
        private ObservableCollection<Stagiaire> stagiaires = new ObservableCollection<Stagiaire>();

        public MainWindow()
        {
            InitializeComponent();

            /// <summary>
            /// la fenêtre ne peut pas modifier sa grosseur
            /// la fenêtre est centrer a l'ecran
            /// </summary>
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            DataContext = this.ajouterProgramme;
            DataContext = this.effacerProgramme;
            DataContext = this.ajouterStagiaire;
            DataContext = this.effacerStagiaire;

            programmes.Add(new Programme(2001, "Math", 60));
            programmes.Add(new Programme(2001, "English", 60));
            programmes.Add(new Programme(2001, "Spanish", 60));

            stagiaires.Add(new Stagiaire(5000, "Jerome", "Miguel", 28, "Homme"));
            stagiaires.Add(new Stagiaire(5001, "Igondjo", "Synn", 55, "Homme"));

            this.ListeViewProgrammes.ItemsSource = programmes;
            this.ListeViewStagiaires.ItemsSource = stagiaires;
            this.ListViewProgrammeConsulter.ItemsSource = programmes;
        }

        // les boutons pour les Programmes
        private void ajouter_Programme_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                    /// <summary>
                     /// Verifier la validation du No du Programme pour le formaulaire Ajouter Programme
                    /// </summary>
                    const string UNIQUE_NO_DE_PROGRAMME_TITLE = "No de programme erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME =
                    "Invalide No. de programme doit etre juste des chiffres uniquemenent entre 0000000 et 9999999 donc seulement un nombre de longueur de 7 chiffres uniquemenent";
                    int idProgramme = int.Parse(idProgrammesTextbox.Text);
                
                    Regex regNoProgramme = new Regex("^([0-9]){7}$");

                    if (!regNoProgramme.IsMatch(idProgrammesTextbox.Text))
                    {
                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME,
                            UNIQUE_NO_DE_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                        
                    /// <summary>
                    /// Verifier la validation du nom du Programme pour le formaulaire Ajouter Programme
                    /// </summary>
                    string nomProgramme = nomProgrammeTextbox.Text;
                    const string ERREUR_NOM_PROGRAMME_TITLE = "Nom de programme erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_NOM_PROGRAMME =
                        "Invalide Nom de programme doit etre juste des lettres entre 1 et 25 characteres de longueur";
                    Regex regNomProgramme = new Regex("^([a-zA-Z]){1,25}$");
                    
                    if (!regNomProgramme.IsMatch(nomProgramme))
                    {
                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_NOM_PROGRAMME,
                            ERREUR_NOM_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

                     /// <summary>
                     /// Verifier la validation de la duree du Programme pour le formaulaire Ajouter Programme
                    /// </summary>
                    const string ERREUR_DUREE_MOIS_TITLE = "Duree du programme erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_DUREE_MOIS = "Invalide duree du programme doit etre juste des chiffres uniquemenent entre 1 et 60";
                    int dureeEnMois = int.Parse(dureeProgrammeTextbox.Text);

                    Regex regDureeProgramme = new Regex("^([1-9]|[1-5][0-9]|60|all)$");

                    if (!regDureeProgramme.IsMatch(dureeProgrammeTextbox.Text))
                    {
                    MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_DUREE_MOIS ,
                            ERREUR_DUREE_MOIS_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    /// <summary>
                    /// Verifier la validation de tous les inputs pour le formaulaire Ajouter Programme
                    /// </summary>
                    if (regNoProgramme.IsMatch(idProgrammesTextbox.Text) && regNomProgramme.IsMatch(nomProgramme) && regDureeProgramme.IsMatch(dureeProgrammeTextbox.Text))
                    {
                        programmes.Add(new Programme(idProgramme, nomProgramme, dureeEnMois));
                        effacer_Programme_Formulaire();
                    }
                }
            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formaulaire Ajouter Programme
            /// </summary>
            catch (Exception)
            {
                const string ERREUR_AJOUTER_FORMULAIRE_PROGRAMME = "Erreur!!! Ajouter Programme Contraintes\n";
                const string UNIQUE_AJOUTER_PROGRAMME_TITLE = "Ajouter Programme erreur";
                MessageBox.Show($"{ERREUR_AJOUTER_FORMULAIRE_PROGRAMME} {ERREUR_RECOMMANCER_FORMULAIRE}", UNIQUE_AJOUTER_PROGRAMME_TITLE,MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void effacer_Programme_Click(object sender, RoutedEventArgs e)
        {
            effacer_Programme_Formulaire();
        }

        private void effacer_Programme_Formulaire()
        {
            idProgrammesTextbox.Text = string.Empty; 
            nomProgrammeTextbox.Text = string.Empty; 
            dureeProgrammeTextbox.Text = string.Empty;
        }

        // les boutons pour les Stagiaires
        private void effacer_Stagiaire_Formulaire()
        {
            idStagiareTextbox.Text = string.Empty;
            nomStagiaireTextbox.Text = string.Empty;
            prenomStagiaireTextbox.Text = string.Empty;
            dateNaissanceTextbox.Text = string.Empty;
            sexeHommeRadioBox.IsChecked = false;
            sexeFemmeRadioBox.IsChecked = false;
        }
        private void ajouter_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// <summary>
                /// Verifier la validation du No du Stagiaire pour le formaulaire Ajouter Stagiaire
                /// </summary>
                const string UNIQUE_NO_DE_STAGIAIRE_TITLE = "No d etudiant erreur";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE =
                    "Invalide No. de stagiaire doit etre juste des chiffres uniquemenent entre 0000000 et 9999999 donc seulement un nombre de longueur de 7 chiffres uniquemenent";
                int idStagiaire = int.Parse(idStagiareTextbox.Text);

                Regex regNoStagiaire = new Regex("^([0-9]){7}$");

                if (!regNoStagiaire.IsMatch(idStagiareTextbox.Text))
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE,
                        UNIQUE_NO_DE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du nom pour le formaulaire Ajouter Stagiaire
                /// </summary>
                string nomStagiaire = nomStagiaireTextbox.Text;
                const string ERREUR_NOM_STAGIAIRE_TITLE = "Nom de stagiaire erreur";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_NOM_STAGIAIRE =
                    "Invalide Nom de stagiaire doit etre juste des lettres entre 1 et 25 characteres";

                Regex regNomStagiaire = new Regex("^([a-zA-Z]){1,25}$");
                if (!regNomStagiaire.IsMatch(nomStagiaire))
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_NOM_STAGIAIRE,
                        ERREUR_NOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du prenom pour le formaulaire Ajouter Stagiaire
                /// </summary>
                string prenomStagiaire = prenomStagiaireTextbox.Text;
                const string ERREUR_PRENOM_STAGIAIRE_TITLE = "Prenom de stagiaire erreur";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_PRENOM_STAGIAIRE =
                    "Invalide Prenom de stagiaire doit etre juste des lettres entre 1 et 25 characteres";

                Regex regPrenomStagiaire = new Regex("^([a-zA-Z]){1,25}$");
                if (!regPrenomStagiaire.IsMatch(prenomStagiaire))
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_PRENOM_STAGIAIRE,
                        ERREUR_PRENOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation de la date de naissance pour le formaulaire Ajouter Stagiaire
                /// </summary>
                int dateNaissance = int.Parse(dateNaissanceTextbox.Text);

                /// <summary>
                /// Verifier la validation du sexe pour le formaulaire Ajouter Stagiaire
                /// </summary>
                string sexe = String.Empty;
                if (sexeHommeRadioBox.IsChecked == true)
                {
                    sexe = (sexeHommeRadioBox.IsChecked == true) ? "Homme" : String.Empty;
                }

                if (sexeFemmeRadioBox.IsChecked == true)
                {
                    sexe = (sexeFemmeRadioBox.IsChecked == true) ? "Femme" : String.Empty;
                }

                /// <summary>
                /// Verifier la validation de tous les inputs pour le formaulaire Ajouter Stagiaire
                /// </summary>
                if (regNoStagiaire.IsMatch(idStagiareTextbox.Text) && regNomStagiaire.IsMatch(nomStagiaire) && regPrenomStagiaire.IsMatch(prenomStagiaire) && sexe!= String.Empty)
                {
                    stagiaires.Add(new Stagiaire(idStagiaire, nomStagiaire, prenomStagiaire, dateNaissance, sexe));
                    effacer_Stagiaire_Formulaire();
                }
            }

            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formaulaire Ajouter Stagiaire
            /// </summary>
            catch (Exception ex)
            {
                 const string ERREUR_AJOUTER_FORMULAIRE_STAGIAIRE = "Erreur!!! Ajouter Stagiaire Contraintes";
                    MessageBox.Show(
                    $"{ERREUR_AJOUTER_FORMULAIRE_STAGIAIRE}\n                              {ERREUR_RECOMMANCER_FORMULAIRE}",
                    "Erreur Contraintes Ajouter Stagiaire",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
        }

        private void effacer_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            effacer_Stagiaire_Formulaire();
        }
    }
}