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

        public bool sexe { get; set; } // only female, only male
        //          public int programme_id { get; set; }

        public Stagiaire(int id, string nom, string prenom)
        {
            this.id = id;
            this.nom = nom;
            this.prenom = prenom;
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

            programmes.Add(new Programme(2001, "Math", 60));
            programmes.Add(new Programme(2001, "English", 60));
            programmes.Add(new Programme(2001, "Spanish", 60));
            stagiaires.Add(new Stagiaire(2001111, "Papa", "Mama"));
            stagiaires.Add(new Stagiaire(2001111, "sister", "brother"));

            this.ListeViewProgrammes.ItemsSource = programmes;
            this.ListeViewStagiaires.ItemsSource = stagiaires;

            this.ListViewProgrammeConsulter.ItemsSource = stagiaires;


        }
    }






}
