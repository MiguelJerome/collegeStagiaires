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
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography.X509Certificates;

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
        public bool sexe { get; set; } // only female, only male
        //          public int programme_id { get; set; }

        public Stagiaire(int id, string nom, string prenom, int dateNaissance)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
            this.dateNaissance = dateNaissance;
            this.dateNaissance = dateNaissance;
            //   this.sexe = sexe;
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
     
        private ObservableCollection<Programme> programmes = new ObservableCollection<Programme>();
        private ObservableCollection<Stagiaire> stagiaires = new ObservableCollection<Stagiaire>();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this.ajouterProgramme;
            DataContext = this.effacerProgramme;
            DataContext = this.ajouterStagiaire;
            DataContext = this.effacerStagiaire;

            programmes.Add(new Programme(2001, "Math", 60));
            programmes.Add(new Programme(2001, "English", 60));
            programmes.Add(new Programme(2001, "Spanish", 60));

            stagiaires.Add(new Stagiaire(5000, "Jerome", "Miguel", 28));
            stagiaires.Add(new Stagiaire(5001, "Igondjo", "Synn", 55));


            this.ListeViewProgrammes.ItemsSource = programmes;
            this.ListeViewStagiaires.ItemsSource = stagiaires;
            this.ListViewProgrammeConsulter.ItemsSource = stagiaires;
        }

        // les boutons pour les Programmes
        private void ajouter_Programme_Click(object sender, RoutedEventArgs e)
        {
            int idProgramme = int.Parse(idProgrammesTextbox.Text);
            string nomProgramme = nomProgrammeTextbox.Text;
            int dureeEnMois = int.Parse(dureeProgrammeTextbox.Text);
            programmes.Add(new Programme(idProgramme, nomProgramme , dureeEnMois));
            effacer_Programme_Formulaire();

        }
        private void effacer_Programme_Click(object sender, RoutedEventArgs e)
        {
            effacer_Programme_Formulaire();
        }

        private void effacer_Programme_Formulaire()
        {
            idProgrammesTextbox.Text = string.Empty; ;
            nomProgrammeTextbox.Text = string.Empty; ;
            dureeProgrammeTextbox.Text = string.Empty;
        }

        // les boutons pour les Stagiaires
        private void effacer_Stagiaire_Formulaire()
        {
            idStagiareTextbox.Text = string.Empty;
            nomStagiaireTextbox.Text = string.Empty;
            prenomStagiaireTextbox.Text = string.Empty;
            dateNaissanceTextbox.Text = string.Empty;
        }
        private void ajouter_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            int idStagiaire = int.Parse(idStagiareTextbox.Text);
            string nomStagiaire = nomStagiaireTextbox.Text;
            string prenomStagiaire = prenomStagiaireTextbox.Text;
            int dateNaissance = int.Parse(dateNaissanceTextbox.Text);
            stagiaires.Add(new Stagiaire(idStagiaire, nomStagiaire, prenomStagiaire, dateNaissance));
            effacer_Stagiaire_Formulaire();
        }

        private void effacer_Stagiaire_Click(object sender, RoutedEventArgs e)
        {
            effacer_Stagiaire_Formulaire();
        }
    }
}
