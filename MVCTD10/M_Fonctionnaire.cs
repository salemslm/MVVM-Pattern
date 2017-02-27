using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTD10
{
    public class M_Fonctionnaire
    {
        string nom;
        string prenom;
        M_Departement dep;
        M_Poste poste;
        int salaire;
        int id;
        public M_Fonctionnaire(int id, string nom, string prenom, M_Departement departement, M_Poste poste, int salaire)
        {
            this.nom = nom;
            this.poste = poste;
            this.prenom = prenom;
            dep = departement;
            this.salaire = salaire;
            this.poste = poste;
            this.id = id;
        }
        #region Propriétés
        public string Nom
        { get { return nom; } }
        public string Prenom
        {
            get { return prenom; }
        }
        public M_Poste Poste
        {
            get { return poste; }
        }
        public M_Departement Departement
        {
            get { return dep; }
        }
        public int Salaire
        { get { return salaire; } }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        #endregion

    }
}
