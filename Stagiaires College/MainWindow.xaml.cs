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
using MySql.Data.MySqlClient;

namespace Stagiaires_College
{
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Diagnostics.Eventing.Reader;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// erreur message attraper par les try ...  catch utiliser globalement a travers du programme
        /// </summary>
        
        private string ERREUR_RECOMMANCER_FORMULAIRE = "Veuillez recommencer";

        private MySqlConnection conBD;

        /// <summary>
        /// creer les objets programmes et objects stagiaires pour qu'il reagissent dynamiquement
        /// </summary>

        public string programme_id = "";

        public MainWindow()
        {
            /// <summary>
            /// est une méthode qui est automatiquement créée et gérée par le concepteur Windows Forms et qui définit tout ce que vous voyez sur le formulaire
            /// </summary>
            
            InitializeComponent();

            conBD = new MySqlConnection("Server=localhost;Uid=root;Pwd=;database=college_stagiaire");
           // Charger_programme();
           // Charger_stagiaires();

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

            /// <summary>
            /// voici des data dummy ou seed data pour d objet programmes pour tester
            /// </summary>
            

            Charger_programme();
            Charger_stagiaire();
        }

        private void Charger_programme()
        {
            try
            {
                conBD.Open();
                string sqlId = "SELECT * FROM programme";
                MySqlCommand cmdId = new MySqlCommand(sqlId, conBD);
                MySqlDataReader drId = cmdId.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(drId);
                this.listeViewProgrammes.ItemsSource = dataTable.DefaultView;
                this.listViewProgrammeConsulter.ItemsSource = dataTable.DefaultView;
                drId.Close();
                conBD.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void Charger_stagiaire()
        {
            try
            {
                conBD.Open();
                string sqlId = "SELECT * FROM stagiaire";
                MySqlCommand cmdId = new MySqlCommand(sqlId, conBD);
                MySqlDataReader drId = cmdId.ExecuteReader();
                DataTable dataTable = new DataTable();
                dataTable.Load(drId);
                drId.Close();
                conBD.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// ajouter toute les data entry pour le formulaire Ajouter Programme en appuyant sur le bouton ajouter Programme
        /// </summary>

        private void ajouter_Programme_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    /// <summary>
                    /// Voici les donnees de validation a verifier pour le formulaire Ajouter Programme
                    /// </summary>
                    conBD.Close();
                    conBD.Open();
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
                        
                        string sql = "INSERT INTO programme(id, nom, dureeEnMois) values (@id,@nom, @dureeEnMois)";

                        MySqlCommand cmd = new MySqlCommand(sql, conBD);
                        
                        cmd.CommandType = CommandType.Text;
                        
                        //Inserer nos valeurs
                        cmd.Parameters.AddWithValue("@id", idProgrammesTextbox.Text);
                        cmd.Parameters.AddWithValue("@nom", nomProgrammeTextbox.Text);
                        cmd.Parameters.AddWithValue("@dureeEnMois", dureeProgrammeTextbox.Text); 
                        cmd.ExecuteNonQuery();
                        conBD.Close(); 
                        effacer_Programme_Formulaire();
                        Charger_programme();
                    }
            }

            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formulaire Ajouter Programme
            /// </summary>
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                const string UNIQUE_NO_DE_PROGRAMME_TITLE = "No de programme erreur existe deja";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME =
                    "Invalide No. de programme, ce numero de programme existe deja, veuillez recommencer le numero de programme";

                MessageBox.Show(
                    ERREUR_MESSAGE_INPUT_INVALIDE_NO_PROGRAMME,
                    UNIQUE_NO_DE_PROGRAMME_TITLE,
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
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
        /// ajouter toute les data entry pour le formulaire Ajouter Stagiaire en appuyant sur le bouton ajouter Stagiaire
        /// </summary>
        
        private void ajouter_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                /// <summary>
                /// Voici les donnees de validation a verifier pour le formulaire Ajouter Stagiaire
                /// </summary>
                conBD.Close();
                conBD.Open();

                Regex regNoStagiaire = new Regex("^([0-9]){7}$");
                Regex regNomStagiaire = new Regex("^([a-zA-Z][a-zA-Z _]*){1,25}$");
                Regex regPrenomStagiaire = new Regex("^([a-zA-Z][a-zA-Z _]*){1,25}$");
                string sexe = String.Empty;
                
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
                    sexe = "Homme";
                }

                else if (sexeFemmeRadioBox.IsChecked == true)
                {
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

                    /// new line sql
                    
                    //// new sql
                  
                        string sql2 =
                            "INSERT INTO stagiaire(id_stagiaire, nom, prenom, date_naissance, sexe, programme_id) values (@id_stagiaire, @nom, @prenom, @date_naissance, @sexe, @programme_id)";

                        MySqlCommand cmd2 = new MySqlCommand(sql2, conBD);

                        cmd2.CommandType = CommandType.Text;

                        //Inserer nos valeurs
                        cmd2.Parameters.AddWithValue("@id_stagiaire", idStagiareTextbox.Text);
                        cmd2.Parameters.AddWithValue("@nom", nomStagiaireTextbox.Text);
                        cmd2.Parameters.AddWithValue("@prenom", prenomStagiaireTextbox.Text);
                        cmd2.Parameters.AddWithValue("@date_naissance", dateNaissanceTextbox.Text);
                        cmd2.Parameters.AddWithValue("@sexe", sexe);
                        cmd2.Parameters.AddWithValue("@programme_id", programmeIdStagiaireTextbox.Text);
                        cmd2.ExecuteNonQuery();
                        conBD.Close();
                    
                    effacer_Stagiaire_Formulaire();
                    Charger_stagiaire();
                }
            }

            /// <summary>
            /// Verifier si il y a des excepction de contraites qui n'ont pas ete encore traiter pour le formulaire Ajouter Stagiaire
            /// </summary>
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                const string UNIQUE_NO_DE_STAGIAIRE_TITLE = "No de stagiaire erreur existe deja";
                const string ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE =
                    "Invalide No. de programme, ce numero de stagiaire existe deja, veuillez recommencer le numero stagiaire ";

                MessageBox.Show(
                    ERREUR_MESSAGE_INPUT_INVALIDE_NO_STAGIAIRE,
                    UNIQUE_NO_DE_STAGIAIRE_TITLE,
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
            programmeIdStagiaireTextbox.Clear();
            listeViewProgrammes.SelectedItem = null;
        }

        private void ListeViewProgrammes_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            DataRowView ligne_selectionnee = ((DataRowView)((ListView)sender).SelectedItem) as DataRowView;

            if (ligne_selectionnee != null)
            {
                //cbxGroupe.Text = ligne_selectionnee["num_groupe"].ToString();
                programmeIdStagiaireTextbox.Text = ligne_selectionnee["id"].ToString();
            }
        }

        private void ListViewProgrammeConsulter_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView ligne_selectionnee = ((DataRowView)((ListView)sender).SelectedItem) as DataRowView;

            if (ligne_selectionnee != null)
            {
                programmeIdStagiaireConsulterTextbox.Text = ligne_selectionnee["id"].ToString();

                try
                {
                    conBD.Open();

                    string sql = "SELECT * FROM stagiaire WHERE programme_id = " + ligne_selectionnee["id"].ToString();

                    MySqlCommand cmd = new MySqlCommand(sql, conBD);
                    MySqlDataReader dr = cmd.ExecuteReader();

                    DataTable dataTable = new DataTable();
                    dataTable.Load(dr);

                    dr.Close();
                    conBD.Close();

                    this.listeViewStagiaires.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

        }

    }
}
