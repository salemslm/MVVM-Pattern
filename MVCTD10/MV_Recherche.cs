using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCTD10
{
    public class MV_Recherche : INotifyCollectionChanged, IEnumerable
    {
        M_GestionnaireDonnees gestionnaireDonnees = new M_GestionnaireDonnees();
        List<MV_Fonctionnaire> listeFonctionnaires = new List<MV_Fonctionnaire>();
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public MV_Recherche()
        {
            this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, listeFonctionnaires));
        }

        public List<MV_Fonctionnaire> ListeFonctionnaires
        {
            get { return listeFonctionnaires; }
        }
        public void EcrireXML(string nom, string poste, string fileName)
        {
            //Passage de la vue au modèle
            gestionnaireDonnees.EcritureXml(nom, poste, fileName);
        }
        public void Addfonctionnaire(string nom, string prenom, string poste, string deptmt, string salaire)
        {
            //On prend le M_fonctionnaire ajouté et on l'ajoute dans notre liste de MV_F.
            MV_Fonctionnaire m = new MV_Fonctionnaire(gestionnaireDonnees.AddFonctionnaire(nom, prenom, poste, deptmt, salaire));
            listeFonctionnaires.Add(m);
            this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, m));
        }

        public List<MV_Fonctionnaire> RechercherFonctionnaire(string nom, string poste)
        {
            //On met à jour notre nouvelle liste
            listeFonctionnaires.Clear();


            foreach (M_Fonctionnaire m_Fonctionnaire in gestionnaireDonnees.GetFonctionnaires(nom, poste))
            {
                listeFonctionnaires.Add(new MV_Fonctionnaire(m_Fonctionnaire));
                
            }

            this.OnNotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, listeFonctionnaires));
            return listeFonctionnaires;

        }
        #region implementation de l'interface INotifyCollectionChanged
        private void OnNotifyCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, args);
            }
        }

        #endregion
        #region implémentation de l'interface IEnumerable

        public IEnumerator GetEnumerator()
        {
            return listeFonctionnaires.GetEnumerator();
        }

        #endregion  
    }
}
