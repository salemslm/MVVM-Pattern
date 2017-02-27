using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTD10
{
    public class M_Departement
    {
        string name;
        public M_Departement(string nomDepartement)
        {
            name = nomDepartement;
        }
        public string NomDepartement
        {
            get { return name; }
        }
    }
}
