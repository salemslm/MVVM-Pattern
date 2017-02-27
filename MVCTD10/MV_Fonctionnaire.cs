using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVCTD10
{
    public class MV_Fonctionnaire
    {
        private string nom;
        private string prenom;
        private int salaire;
        private string poste;
        private string departement;

        public MV_Fonctionnaire(M_Fonctionnaire m)
        {
            nom = m.Nom;
            prenom = m.Prenom;
            departement = m.Departement.NomDepartement;
            poste = m.Poste.Poste;
            salaire = m.Salaire;
        }
        #region Propriétés Accesseurs/Mutateurs
        public int Salaire
             {
                 get { return salaire; }
             }


             public string Prenom
             {
                 get { return prenom; }
             }

             public string Name
             {
                 get { return nom; }
             }
        public string Poste
        {
            get { return poste; }
        }
        public string Departement
        {
            get { return departement; }
        }
             #endregion
             






    }
}
