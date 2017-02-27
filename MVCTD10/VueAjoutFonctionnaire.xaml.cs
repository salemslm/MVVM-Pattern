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
using System.Windows.Shapes;

namespace MVCTD10
{
    /// <summary>
    /// Logique d'interaction pour VueAjoutFonctionnaire.xaml
    /// </summary>
    public partial class VueAjoutFonctionnaire : Window
    {
        MV_Recherche mv_Recherche;
        public VueAjoutFonctionnaire(MV_Recherche mv_rech)
        {
            InitializeComponent();
            this.mv_Recherche =mv_rech;
            
        }
        public MV_Recherche MV_Recherche
        {
            get {return mv_Recherche; }
            set { mv_Recherche = value; }
        }
        private void AddFonctionnaire(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(Nom.Text + Prenom.Text + Poste.Text + Departement.Text + Salaire.Text);
            mv_Recherche.Addfonctionnaire(Nom.Text, Prenom.Text, Poste.Text, Departement.Text, Salaire.Text);
            MessageBox.Show("Fonctionnaire ajouté avec succès !");
            Vue v = new Vue();
            v.Show();
            this.Close();
        }

        private void Return(object sender, RoutedEventArgs e)
        {
            Vue v = new Vue();
            v.Show();
            this.Close();
        }
    }
}
