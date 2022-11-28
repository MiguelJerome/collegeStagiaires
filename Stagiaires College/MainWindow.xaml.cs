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

    /// <summary>
    /// voici la classe Programme qui contient les attribut id, nom, dureeEnMois 
    /// </summary>
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

    /// <summary>
    /// voici la classe Stagiaire qui contient les attribut id, nom, prenom, date, Naissance, sexe 
    /// </summary>
    public class Stagiaire
    {
        public int id { get; set; } // 7 chiffres only

        public string nom { get; set; } // string letters only 

        public string prenom { get; set; } // string letters only

        public string dateNaissance { get; set; } // string letters only
        public string sexe { get; set; } // only female or only male
        
        public Programme programme { get; set; }

        public Stagiaire(int id, string nom, string prenom, string dateNaissance, string sexe, Programme programme)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.sexe = sexe;
            this.programme = programme;

        }

        public override string ToString()
        {
            return $"{this.id} {this.nom} {this.prenom} {this.dateNaissance} {this.sexe}";
        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// erreur message attraper par les try ...  catch utiliser globalement a travers du programme
        /// </summary>
        private string ERREUR_RECOMMANCER_FORMULAIRE = "Veuillez recommencer";

        /// <summary>
        /// creer les objets programmes et objects stagiaires pour qu'il reagissent dynamiquement
        /// </summary>
        private ObservableCollection<Programme> programmes = new ObservableCollection<Programme>();
        private ObservableCollection<Stagiaire> stagiaires = new ObservableCollection<Stagiaire>();

        public MainWindow()
        {
            /// <summary>
            /// est une méthode qui est automatiquement créée et gérée par le concepteur Windows Forms et qui définit tout ce que vous voyez sur le formulaire
            /// </summary>
            InitializeComponent();

            /// <summary>
            /// la fenêtre ne peut pas modifier sa grosseur
            /// la fenêtre est centrer a l'ecran
            /// </summary>
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.ResizeMode = ResizeMode.NoResize;

            /// <summary>
            /// on bind avec DataContext avec le programmes et le stagiaires
            /// </summary>
            DataContext = this;
            DataContext = this.programmes;
            DataContext = this.stagiaires;

            /// <summary>
            /// voici des data dummy ou seed data pour d objet programmes pour tester
            /// </summary>
            programmes.Add(new Programme(2001, "Math", 60));
            programmes.Add(new Programme(2001, "English", 60));
            programmes.Add(new Programme(2001, "Spanish", 60));

            /// <summary>
            /// voici des data dummy ou seed data pour d objet stagiaire pour tester
            /// </summary>
            stagiaires.Add(new Stagiaire(5000, "Jerome", "Miguel", "28/11/2022", "Homme", new Programme(2001, "Math", 60)));
            stagiaires.Add(new Stagiaire(5001, "Igondjo", "Synn", "28/11/2022", "Homme", new Programme(2001, "Math", 60)));

            /// <summary>
            /// faire le data binding pour different listeview avec les programmes et stagiaires
            /// </summary>
            this.listeViewProgrammes.ItemsSource = programmes;
            this.listeViewStagiaires.ItemsSource = stagiaires;
            this.listViewProgrammeConsulter.ItemsSource = programmes;

            /*
                        // <summary>
                        /// Verifier la validation de la selecton du programme pour le stagiaire en utilisant le formulaire Ajouter Stagiaire
                        /// </summary>

                        Programme programmeConsulterChoix = (Programme)this.listViewProgrammeConsulter.SelectedItem;

                        const string ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE = "Erreur Obligation selection de donnee Stagiaire";
                        const string ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE =
                            "Invalide selection de donnee pour le Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un programme";

                        if (programmeConsulterChoix == null)
                        {
                            MessageBox.Show(
                                ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE,
                                ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE,
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        }
            */
        }

        /// <summary>
        /// ajouter toute les data entry pour le formulaire Ajouter Programme en appuyant sur le bouton effacer Programme
        /// </summary>
        private void ajouter_Programme_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                    /// <summary>
                    /// Verifier la validation du No du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    const string OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE = "Erreur Obligation Data entry ID Programme";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE =
                    "Invalide data entry pour ID Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    if ((idProgrammesTextbox.Text) == string.Empty)
                    {
                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE,
                        OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    }
                    
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
                    /// Verifier la validation du nom du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    const string OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE = "Erreur Obligation Data entry nom Programme";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE =
                        "Invalide data entry pour nom Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    if ((nomProgrammeTextbox.Text) == string.Empty)
                    {
                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE,
                            OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

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
                    /// Verifier la validation de la duree du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    const string OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE = "Erreur Obligation Data entry duree du Programme";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE =
                        "Invalide data entry pour la duree du Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    if ((dureeProgrammeTextbox.Text) == string.Empty)
                    {
                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE,
                            OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

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
                    /// Verifier la validation de tous les inputs pour le formulaire Ajouter Programme
                    /// </summary>
                    if (regNoProgramme.IsMatch(idProgrammesTextbox.Text) && regNomProgramme.IsMatch(nomProgramme) && regDureeProgramme.IsMatch(dureeProgrammeTextbox.Text))
                    {
                        programmes.Add(new Programme(idProgramme, nomProgramme, dureeEnMois));
                        effacer_Programme_Formulaire();
                    }
                }
            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formulaire Ajouter Programme
            /// </summary>
            catch (Exception)
            {
                const string ERREUR_AJOUTER_FORMULAIRE_PROGRAMME = "Erreur!!! Ajouter Programme Contraintes\n";
                const string UNIQUE_AJOUTER_PROGRAMME_TITLE = "Ajouter Programme erreur";
                MessageBox.Show($"{ERREUR_AJOUTER_FORMULAIRE_PROGRAMME} {ERREUR_RECOMMANCER_FORMULAIRE}", UNIQUE_AJOUTER_PROGRAMME_TITLE,MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// effacer toute les data entry pour le formulaire Ajouter Programme en appuyant sur le bouton effacer Programme
        /// </summary>
        private void effacer_Programme_Click(object sender, RoutedEventArgs e)
        {
            effacer_Programme_Formulaire();
        }

        /// <summary>
        /// effacer toute les data entry pour le formulaire Ajouter Programme
        /// </summary>
        private void effacer_Programme_Formulaire()
        {
            idProgrammesTextbox.Text = string.Empty; 
            nomProgrammeTextbox.Text = string.Empty; 
            dureeProgrammeTextbox.Text = string.Empty;
        }

        /// <summary>
        /// ajouter toute les data entry pour le formulaire Ajouter Stagiaire en appuyant sur le bouton effacer Stagiaire
        /// </summary>
        private void ajouter_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// <summary>
                /// Verifier la validation du No du Stagiaire pour le formulaire Ajouter Stagiaire
                /// </summary>

                const string OBLIGATION_DATA_ENTRY_ID_STAGIAIRE_TITLE = "Erreur Obligation Data entry ID Stagiaire";
                const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_STAGIAIRE =
                    "Invalide data entry pour ID Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                if ((idStagiareTextbox.Text) == string.Empty)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_ID_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

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
                /// Verifier la validation du nom pour le formulaire Ajouter Stagiaire
                /// </summary>

                const string OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE_TITLE = "Erreur Obligation Data entry nom Stagiaire";
                const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE =
                    "Invalide data entry pour nom Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                if ((nomStagiaireTextbox.Text) == string.Empty)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

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
                /// Verifier la validation du prenom pour le formulaire Ajouter Stagiaire
                /// </summary>

                const string OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE_TITLE = "Erreur Obligation Data entry prenom Stagiaire";
                const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE =
                    "Invalide data entry pour prenom Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                if ((prenomStagiaireTextbox.Text) == string.Empty)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

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
                /// Verifier la validation de la date de naissance pour le formulaire Ajouter Stagiaire
                /// </summary>

                const string OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE_TITLE = "Erreur Obligation Data entry date de naissance Stagiaire";
                const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE =
                    "Invalide data entry pour date de naissance Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                if ((dateNaissanceTextbox.Text) == string.Empty)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                string dateNaissance = dateNaissanceTextbox.Text;
                const string ERREUR_DATE_DE_NAISSANCE_STAGIAIRE_TITLE = "Date de naissance de stagiaire erreur";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_DATE_DE_NAISSANCE_STAGIAIRE =
                    "Invalide Date de naissance de stagiaire doit etre de format dd/mm/yyyy";

                Regex regDateNaissanceStagiaire = new Regex("^(0?[1-9]|[12][0-9]|3[01])[\\/\\-](0?[1-9]|1[012])[\\/\\-]\\d{4}$");
                if (!regDateNaissanceStagiaire.IsMatch(dateNaissance))
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_DATE_DE_NAISSANCE_STAGIAIRE,
                        ERREUR_DATE_DE_NAISSANCE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du sexe pour le formulaire Ajouter Stagiaire
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

                const string ERREUR_SEXE_STAGIAIRE_TITLE = "Erreur Obligation Data entry sexe Stagiaire";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_SEXE_STAGIAIRE =
                    "Invalide data entry pour sexe Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un sexe";

                if (sexe == String.Empty)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_SEXE_STAGIAIRE,
                        ERREUR_SEXE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                // <summary>
                /// Verifier la validation de la selecton du programme pour le stagiaire en utilisant le formulaire Ajouter Stagiaire
                /// </summary>

                Programme programmeChoix = (Programme)this.listeViewProgrammes.SelectedItem;

                const string ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE = "Erreur Obligation selection de donnee Stagiaire";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE =
                    "Invalide selection de donnee pour le Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un programme";

                if (programmeChoix == null)
                {
                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE,
                        ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation de tous les inputs pour le formulaire Ajouter Stagiaire
                /// </summary>
                if (regNoStagiaire.IsMatch(idStagiareTextbox.Text) && regNomStagiaire.IsMatch(nomStagiaire) && regPrenomStagiaire.IsMatch(prenomStagiaire) && regDateNaissanceStagiaire.IsMatch(dateNaissance) && sexe!= String.Empty && programmeChoix != null)
                {
                    stagiaires.Add(new Stagiaire(idStagiaire, nomStagiaire, prenomStagiaire, dateNaissance, sexe, programmeChoix));
                    effacer_Stagiaire_Formulaire();
                }
            }

            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formulaire Ajouter Stagiaire
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

        /// <summary>
        /// effacer toute les data entry pour le formulaire Ajouter Stagiaire en appuyant sur le bouton effacer Stagiaire
        /// </summary>
        private void effacer_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            effacer_Stagiaire_Formulaire();
        }

        /// <summary>
        /// effacer toute les data entry pour le formulaire Ajouter Stagiaire
        /// </summary>
        private void effacer_Stagiaire_Formulaire()
        {
            idStagiareTextbox.Text = string.Empty;
            nomStagiaireTextbox.Text = string.Empty;
            prenomStagiaireTextbox.Text = string.Empty;
            dateNaissanceTextbox.Text = string.Empty;
            sexeHommeRadioBox.IsChecked = false;
            sexeFemmeRadioBox.IsChecked = false;
        }

}
}