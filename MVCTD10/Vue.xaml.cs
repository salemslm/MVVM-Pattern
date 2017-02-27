using Microsoft.Win32;
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
using System.Xml;
using System.IO;


namespace MVCTD10
{
    /// <summary>
    /// Logique d'interaction pour Vue.xaml
    /// </summary>
    public partial class Vue : Window
    {
        MV_Recherche mv_rech;

        public Vue()
        {
            InitializeComponent();
            mv_rech = new MV_Recherche();
        }
        private void FindPerson(object sender, RoutedEventArgs e)
        {
            mv_rech = new MV_Recherche();
            lvFonctionnaires.ItemsSource =  mv_rech.RechercherFonctionnaire(Nom.Text, Poste.Text);
        }
        public void EcritureXML(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";
            if (saveFileDialog.ShowDialog() == true)//Si le fichier a bien été créé
            {
                mv_rech.EcrireXML(Nom.Text, Poste.Text, saveFileDialog.FileName);
                MessageBox.Show("Enregistrement effectué avec succès !");
            }


        }
        private void RedirectToAddFonctionnaire(object sender, RoutedEventArgs e)
        {
            //On change de fenêtre pour aller à cette de l'inscription d'un fonctionnaire.
            VueAjoutFonctionnaire NewVueAjoutFonctionnaire = new VueAjoutFonctionnaire(mv_rech);
            NewVueAjoutFonctionnaire.Show();
            this.Close();
        }

    }
}
