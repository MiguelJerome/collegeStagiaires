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

        public string GetterId()
        {
            return $"{this.id}";
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
        
        public int programme_id { get; set; }

        public Stagiaire(int id, string nom, string prenom, string dateNaissance, string sexe, int programme_id)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.sexe = sexe;
            this.programme_id = programme_id;
        }

        public string GetterId()
        {
            return $"{this.id}";
        }
        public string GetterPrenom()
        {
            return $"{this.prenom}";
        }
        public string GetterProgrammePrenom()
        {
            return $"{this.prenom}";
        }
        public string GetterProgrammeDateNaissance()
        {
            return $"{this.dateNaissance}";
        }
        public string GetterProgrammeSexe()
        {
            return $"{this.sexe}";
        }
        public int GetterProgrammeId()
        {
            return this.programme_id;
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
        private ObservableCollection<Stagiaire> stagiairesSelectionner = new ObservableCollection<Stagiaire>();

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
            programmes.Add(new Programme(9992001, "Math", 60));
            programmes.Add(new Programme(9992011, "English", 48));
            programmes.Add(new Programme(9992021, "Spanish", 24));

            /// <summary>
            /// voici des data dummy ou seed data pour d objet stagiaire pour tester
            /// </summary>
           stagiaires.Add(new Stagiaire(9995000, "Jerome", "Miguel", "28/11/2022", "Homme", 9992011));
            stagiaires.Add(new Stagiaire(9995011, "Jerome", "Danielle", "28/11/2022", "Femme", 9992001));
            stagiaires.Add(new Stagiaire(9995012, "Jerome", "Daniel", "28/11/2022", "Homme", 9992001));
            stagiaires.Add(new Stagiaire(9995013, "Jerome", "Maria", "28/11/2022", "Femme", 9992021)); 
            stagiaires.Add(new Stagiaire(9995014, "Jerome", "Alponse", "28/11/2022", "Homme", 9992001));
            stagiaires.Add(new Stagiaire(9995015, "Jerome", "Stephanie", "28/11/2022", "Femme", 9992011));
            stagiaires.Add(new Stagiaire(9995016, "Jerome", "James", "28/11/2022", "Femme", 9992021));
            stagiaires.Add(new Stagiaire(9995017, "Jerome", "Gabrielle", "28/11/2022", "Femme", 9992011));
            stagiaires.Add(new Stagiaire(9995018, "Jerome", "George", "28/11/2022", "Femme", 9992001));
            stagiaires.Add(new Stagiaire(9995019, "Jerome", "Jenifer", "28/11/2022", "Femme", 9992021));
            stagiaires.Add(new Stagiaire(9995020, "Jerome", "Marc", "28/11/2022", "Homme", 9992001));
            stagiaires.Add(new Stagiaire(9995021, "Jerome", "Carly", "28/11/2022", "Femme", 9992011));
            stagiaires.Add(new Stagiaire(9995022, "Jerome", "Synn", "28/11/2022", "Homme", 9992021));

            /// <summary>
            /// faire le data binding pour different listeview avec les programmes et stagiaires
            /// </summary>
            this.listeViewProgrammes.ItemsSource = programmes;
            this.listeViewStagiaires.ItemsSource = stagiairesSelectionner;
            this.listViewProgrammeConsulter.ItemsSource = programmes;
        }

        /// <summary>
        /// ajouter toute les data entry pour le formulaire Ajouter Programme en appuyant sur le bouton effacer Programme
        /// </summary>
        private void ajouter_Programme_Click(object sender, RoutedEventArgs e)
        {
            try
                {
                    /// <summary>
                    /// Voici les donnees de validation a verifier pour le formulaire Ajouter Programme
                    /// </summary>

                    Regex regNoProgramme = new Regex("^([0-9]){7}$");
                    Regex regNomProgramme = new Regex("^([a-zA-Z][a-zA-Z _]*){1,25}$");
                    Regex regDureeProgramme = new Regex("^([1-9]|[1-5][0-9]|60|all)$");

                    /// <summary>
                    /// Verifier la validation si id Programmes TextBox est vide
                    /// </summary>

                    if ((idProgrammesTextbox.Text) == string.Empty)
                        {
                            const string OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE = "Erreur Obligation Data entry ID Programme";
                            const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE =
                                "Invalide data entry pour ID Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                            MessageBox.Show(
                                ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE,
                            OBLIGATION_DATA_ENTRY_ID_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        }

                    /// <summary>
                    /// Verifier la validation du No du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    else if (!regNoProgramme.IsMatch(idProgrammesTextbox.Text))
                    {
                        //idProgramme = int.Parse(idProgrammesTextbox.Text);
                        const string UNIQUE_NO_DE_PROGRAMME_TITLE = "No de programme erreur";
                        const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME =
                            "Invalide No. de programme doit etre juste des chiffres uniquemenent entre 0000000 et 9999999 donc seulement un nombre de longueur de 7 chiffres uniquement";

                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME,
                            UNIQUE_NO_DE_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

                    /// <summary>
                    /// Verifier la validation si nom Programmes TextBox est vide
                    /// </summary>
                     
                    else if ((nomProgrammeTextbox.Text) == string.Empty)
                        {
                            const string OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE = "Erreur Obligation Data entry nom Programme";
                            const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE =
                                "Invalide data entry pour nom Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                            MessageBox.Show(
                                ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE,
                                OBLIGATION_DATA_ENTRY_NOM_PROGRAMME_TITLE,
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        }
                    /// <summary>
                    /// Verifier la validation du nom du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    else if (!regNomProgramme.IsMatch(nomProgrammeTextbox.Text))
                    {
                        const string ERREUR_NOM_PROGRAMME_TITLE = "Nom de programme erreur";
                        const string ERREUR_MESSAGE_INPUT_INVALIDE_NOM_PROGRAMME =
                            "Invalide Nom de programme doit etre juste des lettres entre 1 et 25 characteres de longueur";

                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_NOM_PROGRAMME,
                            ERREUR_NOM_PROGRAMME_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

                    /// <summary>
                    /// Verifier la validation si la duree Programmes TextBox est vide
                    /// </summary>
                     
                    else if ((dureeProgrammeTextbox.Text) == string.Empty)
                        {
                            const string OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE = "Erreur Obligation Data entry duree du Programme";
                            const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE =
                                "Invalide data entry pour la duree du Programme, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                            MessageBox.Show(
                                ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE,
                                OBLIGATION_DATA_ENTRY_DUREE_PROGRAMME_TITLE,
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                        }

                    /// <summary>
                    /// Verifier la validation de la duree du Programme pour le formulaire Ajouter Programme
                    /// </summary>

                    else if (!regDureeProgramme.IsMatch(dureeProgrammeTextbox.Text))
                    {
                        const string ERREUR_DUREE_MOIS_TITLE = "Duree du programme erreur";
                        const string ERREUR_MESSAGE_INPUT_INVALIDE_DUREE_MOIS = "Invalide duree du programme doit etre juste des chiffres uniquemenent entre 1 et 60";

                        MessageBox.Show(
                            ERREUR_MESSAGE_INPUT_INVALIDE_DUREE_MOIS ,
                            ERREUR_DUREE_MOIS_TITLE,
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }

                    /// <summary>
                    /// Verifier la validation de tous les inputs pour le formulaire Ajouter Programme
                    /// </summary>
                    if (regNoProgramme.IsMatch(idProgrammesTextbox.Text) && regNomProgramme.IsMatch(nomProgrammeTextbox.Text) && regDureeProgramme.IsMatch(dureeProgrammeTextbox.Text))
                    {
                        programmes.Add(new Programme(int.Parse(idProgrammesTextbox.Text), nomProgrammeTextbox.Text, int.Parse(dureeProgrammeTextbox.Text)));
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
                /// Voici les donnees de validation a verifier pour le formulaire Ajouter Stagiaire
                /// </summary>

                Regex regNoStagiaire = new Regex("^([0-9]){7}$");
                Regex regNomStagiaire = new Regex("^([a-zA-Z][a-zA-Z _]*){1,25}$");
                Regex regPrenomStagiaire = new Regex("^([a-zA-Z][a-zA-Z _]*){1,25}$");
                string sexe = String.Empty;
                Programme programmeChoix = (Programme)this.listeViewProgrammes.SelectedItem;

                // <summary>
                /// Verifier la validation de la selecton du programme pour le stagiaire en utilisant le formulaire Ajouter Stagiaire
                /// </summary>

                if (listeViewProgrammes.SelectedItem == null)
                {
                    const string ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE = $"Erreur Obligation selection d'un programme pour Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE =
                        "Invalide selection d'un programme pour le Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un programme";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE,
                        ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                /// <summary>
                /// Verifier la validation si id Stagiaire TextBox est vide
                /// </summary>

                else if ((idStagiareTextbox.Text) == string.Empty)
                {
                    const string OBLIGATION_DATA_ENTRY_ID_STAGIAIRE_TITLE = "Erreur Obligation Data entry ID Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_STAGIAIRE =
                        "Invalide data entry pour ID Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_ID_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_ID_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du No du Stagiaire pour le formulaire Ajouter Stagiaire
                /// </summary>
                else if (!regNoStagiaire.IsMatch(idStagiareTextbox.Text))
                {
                    const string UNIQUE_NO_DE_STAGIAIRE_TITLE = "No d etudiant erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE =
                        "Invalide No. de stagiaire doit etre juste des chiffres uniquemenent entre 0000000 et 9999999 donc seulement un nombre de longueur de 7 chiffres uniquement";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE,
                        UNIQUE_NO_DE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation si nom Stagiaire TextBox est vide
                /// </summary>
                else if ((nomStagiaireTextbox.Text) == string.Empty)
                {
                    const string OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE_TITLE = "Erreur Obligation Data entry nom Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE =
                        "Invalide data entry pour nom Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_NOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du nom pour le formulaire Ajouter Stagiaire
                /// </summary>

                else if (!regNomStagiaire.IsMatch(nomStagiaireTextbox.Text))
                {
                    const string ERREUR_NOM_STAGIAIRE_TITLE = "Nom de stagiaire erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_NOM_STAGIAIRE =
                        "Invalide Nom de stagiaire doit etre juste des lettres entre 1 et 25 characteres";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_NOM_STAGIAIRE,
                        ERREUR_NOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation si prenom Stagiaire TextBox est vide
                /// </summary>
                /// 
                else if ((prenomStagiaireTextbox.Text) == string.Empty)
                {
                    const string OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE_TITLE = "Erreur Obligation Data entry prenom Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE =
                        "Invalide data entry pour prenom Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_PRENOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation du prenom pour le formulaire Ajouter Stagiaire
                /// </summary>

                else if (!regPrenomStagiaire.IsMatch(prenomStagiaireTextbox.Text))
                {
                    const string ERREUR_PRENOM_STAGIAIRE_TITLE = "Prenom de stagiaire erreur";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_PRENOM_STAGIAIRE =
                        "Invalide Prenom de stagiaire doit etre juste des lettres entre 1 et 25 characteres";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_PRENOM_STAGIAIRE,
                        ERREUR_PRENOM_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation si date de naissance Stagiaire TextBox est vide
                /// </summary>
                /// 
                else if ((dateNaissanceTextbox.Text) == string.Empty)
                {
                    const string OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE_TITLE = "Erreur Obligation Data entry date de naissance Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE =
                        "Invalide data entry pour date de naissance Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE,
                        OBLIGATION_DATA_ENTRY_DATE_NAISSANCE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
                
                /// <summary>
                /// Verifier la validation du sexe pour le formulaire Ajouter Stagiaire
                /// </summary>
                
                 else if (sexeHommeRadioBox.IsChecked == true)
                {
                    //sexe = (sexeHommeRadioBox.IsChecked == true) ? "Homme" : String.Empty;
                   
                        sexe = "Homme";
                 
                }

                else if (sexeFemmeRadioBox.IsChecked == true)
                {
                   // sexe = (sexeFemmeRadioBox.IsChecked == true) ? "Femme" : String.Empty;
                  
                        sexe = "Femme";
                   
                }
                /// <summary>
                /// Verifier la validation si le sexe du Stagiaire RadioBox n'est pas selectionner
                /// </summary>
                
                else if (sexeHommeRadioBox.IsChecked == false && sexeFemmeRadioBox.IsChecked == false)
                {
                    const string ERREUR_SEXE_STAGIAIRE_TITLE = "Erreur Obligation Data entry sexe Stagiaire";
                    const string ERREUR_MESSAGE_INPUT_INVALIDE_SEXE_STAGIAIRE =
                        "Invalide data entry pour sexe Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un sexe";

                    MessageBox.Show(
                        ERREUR_MESSAGE_INPUT_INVALIDE_SEXE_STAGIAIRE,
                        ERREUR_SEXE_STAGIAIRE_TITLE,
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                /// <summary>
                /// Verifier la validation de tous les inputs pour le formulaire Ajouter Stagiaire
                /// </summary>
                if (regNoStagiaire.IsMatch(idStagiareTextbox.Text) && regNomStagiaire.IsMatch(nomStagiaireTextbox.Text) && regPrenomStagiaire.IsMatch(prenomStagiaireTextbox.Text) && dateNaissanceTextbox.Text != string.Empty && sexe!= String.Empty && this.listeViewProgrammes.SelectedItem != null)
                {
                    stagiaires.Add(new Stagiaire(int.Parse(idStagiareTextbox.Text), nomStagiaireTextbox.Text, prenomStagiaireTextbox.Text, dateNaissanceTextbox.Text, sexe, int.Parse(programmeChoix.GetterId())));
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
            listeViewProgrammes.SelectedItem = null;
        }

        private void ChercherProgrammeStagiaires_OnClick(object sender, RoutedEventArgs e)
        {
            // <summary>
            /// Verifier la validation de la selecton du programme pour afficher tous les stagiaires incrit a ce programme en utilisant la list view Programme consulter
            /// </summary>

            Programme programmeChoix = (Programme)this.listViewProgrammeConsulter.SelectedItem;
       
            this.stagiairesSelectionner.Clear();

            if (listViewProgrammeConsulter.SelectedItem == null)
            {
                const string ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE = $"Erreur Obligation selection d'un programme pour Stagiaire";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE =
                    "Invalide selection d'un programme pour le Stagiaire, obligation de suivre les restrictions de ce champ parce qu'en ce moment il est vide, vous devez en selectionner un programme";

                MessageBox.Show(
                    ERREUR_MESSAGE_INPUT_INVALIDE_PROGRAMME_CHOIX_STAGIAIRE,
                    ERREUR_PROGRAMME_CHOIX_STAGIAIRE_TITLE,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else if (listViewProgrammeConsulter.SelectedItem != null)
            {
                foreach (var VARIABLE in this.stagiaires)
                {

                    if (int.Parse(programmeChoix.GetterId()) == VARIABLE.GetterProgrammeId())
                        {
                            stagiairesSelectionner.Add(new Stagiaire(VARIABLE.id, VARIABLE.nom, VARIABLE.prenom, VARIABLE.dateNaissance, VARIABLE.sexe, VARIABLE.GetterProgrammeId()));
                        }
                }
            }
        }
    }
}
