using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCTD10
{
    public class M_Poste
    {
        string title;

        public M_Poste(string poste)
        {

            title = poste;
        }
        public string Poste
        {
            get { return title; }
        }
    }
}
