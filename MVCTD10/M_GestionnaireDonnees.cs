using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO;

namespace MVCTD10
{

    public class M_GestionnaireDonnees
    {
        List<M_Fonctionnaire> ListeFonctionnaires = new List<M_Fonctionnaire>();
        public List<M_Fonctionnaire> GetFonctionnaires(string nom, string poste)
        {
            string requete;

            #region Requete sql

            //Nom choisi seul
            if (nom != "" && poste == "")
            {
                nom = "%" + nom + "%";
                requete = "Select employee.id,lastName, firstName, name,title,annualSalary FROM employee INNER JOIN position ON positionID = position.id INNER JOIN department on departementID= department.id WHERE lastName LIKE '" + nom + "' order by lastName;";
            }
            //Poste choisi seul
            else if (poste != "" && nom == "")
            {
                poste = "%" + poste + "%";
                requete = "Select employee.id,lastName, firstName, name,title,annualSalary FROM employee INNER JOIN position ON positionID = position.id INNER JOIN department on departementID= department.id WHERE title LIKE '" + poste + "' order by lastName;";
            }
            //2 champs remplis
            else if (poste != "" && nom != "")
            {
                poste = "%" + poste + "%";
                nom = "%" + nom + "%";
                requete = "Select employee.id,lastName, firstName, name,title,annualSalary FROM employee INNER JOIN position ON positionID = position.id INNER JOIN department on departementID= department.id WHERE lastName LIKE '" + nom + "' AND title LIKE '" + poste + "' order by lastName;";
            }
            else
            {
                requete = "Select employee.id,lastName, firstName, name,title,annualSalary FROM employee INNER JOIN position ON positionID = position.id INNER JOIN department on departementID= department.id order by lastName LIMIT 200";
            }
            #endregion
            #region Connexion à la BDD
            string connectionString = "SERVER=localhost ;PORT=3306;DATABASE=S6_MVC_Chicago;UID=esilvs6 ;PASSWORD= esilvs6 ; ";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            MySqlDataReader reader;
            command.CommandText = requete;
            reader = command.ExecuteReader();
            #endregion
            while (reader.Read())
            {
                //A chaque tuple, on crée un Fonctionnaire.
                M_Fonctionnaire m_Fonctionnaire = new M_Fonctionnaire(int.Parse(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), reader.GetValue(2).ToString(), new M_Departement(reader.GetValue(3).ToString()), new M_Poste(reader.GetValue(4).ToString()), int.Parse(reader.GetValue(5).ToString()));
                ListeFonctionnaires.Add(m_Fonctionnaire);
            }
            connection.Close();

            return ListeFonctionnaires;
        }
        public M_Fonctionnaire AddFonctionnaire(string nom, string prenom, string poste, string deptmt, string salaire)
        {
            //Création d'id éventuel
            int idPoste;
            int idDepartment;
            #region REQUETES
            #region Requetes obtenir ID maximum
            string requeteGetMaxIdDepartment = "SELECT max(id) FROM department;";
            string requeteGetMaxIdPoste = "SELECT max(id) FROM position;";
            #endregion
            #region Requetes de vérification
            string verifPosition = "SELECT id,title from position WHERE title like '" + poste + "'";
            string verifDepartment = "SELECT id,name from department WHERE name like '" + deptmt + "'";
            #endregion
            #region Requetes d'insertion
            string insererPoste = "INSERT INTO S6_MVC_Chicago.Position (title) VALUES ('" + poste + "');";
            string insererDepartement = "INSERT INTO S6_MVC_Chicago.Department(name) VALUES('" + deptmt + "');";
            string insererFonctionnaire;
            #endregion
            #endregion
            #region Connexion BDD
            string connectionString = "SERVER=localhost ;PORT=3306;DATABASE=S6_MVC_Chicago;UID=esilvs6 ;PASSWORD= esilvs6 ; ";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = connection.CreateCommand();
            #endregion
            #region Verification ID poste
            command = connection.CreateCommand();
            command.CommandText = verifPosition;

            MySqlDataReader readerverifPosition;
            readerverifPosition = command.ExecuteReader();
            if (readerverifPosition.HasRows)
            {
                readerverifPosition.Read();
                //On récupère cet id
                idPoste = int.Parse(readerverifPosition.GetValue(0).ToString());

            }
            else
            {
                //On ajoute cet id et on récupere ce new id
                connection.Close();
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = insererPoste;
                MySqlDataReader readerinsertion = command.ExecuteReader();
                readerinsertion.Read();

                connection.Close();
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = requeteGetMaxIdPoste;
                MySqlDataReader readerId = command.ExecuteReader();
                readerId.Read();
                idPoste = int.Parse(readerId.GetValue(0).ToString());
            }

            #endregion
            #region Verification ID Departement
            connection.Close();
            connection.Open();
            command = connection.CreateCommand();
            command.CommandText = insererDepartement;
            MySqlDataReader readerinsertiondpt = command.ExecuteReader();
            readerinsertiondpt.Read();

            connection.Close();
            connection.Open();
            MySqlDataReader readerDepartment;
            command = connection.CreateCommand();
            command.CommandText = verifDepartment;
            readerDepartment = command.ExecuteReader();
            if (readerDepartment.HasRows)
            {
                readerDepartment.Read();
                //Recupere nouvel ID
                idDepartment = int.Parse(readerDepartment.GetValue(0).ToString());
            }
            else
            {
                // Ajoute cet ID et on récupere ce new ID

                connection.Close();
                connection.Open();
                command = connection.CreateCommand();
                command.CommandText = requeteGetMaxIdDepartment;
                MySqlDataReader readerId = command.ExecuteReader();
                readerId.Read();
                idDepartment = int.Parse(readerId.GetValue(0).ToString());
                command.CommandText = insererDepartement;
                Console.WriteLine(idDepartment);

            }
            #endregion            
            #region Insere finalement le fonctionnaire
            try
            {
                connection.Close();
                connection.Open();
                command = connection.CreateCommand();
                insererFonctionnaire = "INSERT INTO S6_MVC_Chicago.employee (lastName, firstName,positionID, departementID, annualSalary) VALUES ('" + nom + "','" + prenom + "','" + idPoste + "','" + idDepartment + "','" + salaire + "')";
                command.CommandText = insererFonctionnaire;

                MySqlDataReader fonctio = command.ExecuteReader();
                fonctio.Read();

                connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur : Fonctionnaire non ajouté\n" + e.Message);
            }

            connection.Open();
            command = connection.CreateCommand();
            string obtenirIdNewFonction = "Select max(id) from employee";
            command.CommandText = obtenirIdNewFonction;
            MySqlDataReader readerobtenirId = command.ExecuteReader();
            readerobtenirId.Read();
            int id = int.Parse(readerobtenirId.GetValue(0).ToString());
            connection.Close();

            #endregion

            M_Fonctionnaire newM_Fonct = new M_Fonctionnaire(id, nom, prenom, new M_Departement(deptmt), new M_Poste(poste), int.Parse(salaire));
            return newM_Fonct;

        }
        public void EcritureXml(string nom, string poste, string fileName)
        {
            XmlDocument docXml = new XmlDocument();


            XmlDocumentType doctype;
            XmlDeclaration docdecla = docXml.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            docXml.AppendChild(docdecla);
            doctype = docXml.CreateDocumentType("resultats", "SYSTEM", "resultats.dtd", null);

            docXml.AppendChild(doctype);
            XmlElement racine = docXml.CreateElement("resultats");
            docXml.AppendChild(racine);

            //Ajout de la requete
            XmlElement requete = docXml.CreateElement("requete");
            racine.AppendChild(requete);

            XmlElement nomxml = docXml.CreateElement("nom");
            XmlElement postexml = docXml.CreateElement("poste");
            nomxml.InnerText = nom;
            postexml.InnerText = poste;

            if (nom != "")
                requete.AppendChild(nomxml);
            if (poste != "")
                requete.AppendChild(postexml);

            //Ajout des fonctionnaires sous format XML
            foreach (M_Fonctionnaire fonctionnaire in ListeFonctionnaires)
            {
                XmlElement elefonctionnaire = docXml.CreateElement("fonctionnaire");
                racine.AppendChild(elefonctionnaire);

                XmlElement souselement = docXml.CreateElement("nom");
                souselement.InnerText = fonctionnaire.Nom;
                elefonctionnaire.AppendChild(souselement);

                souselement = docXml.CreateElement("prenom");
                souselement.InnerText = fonctionnaire.Prenom;
                elefonctionnaire.AppendChild(souselement);

                souselement = docXml.CreateElement("poste");
                souselement.InnerText = fonctionnaire.Poste.Poste;
                elefonctionnaire.AppendChild(souselement);

                souselement = docXml.CreateElement("departement");
                souselement.InnerText = fonctionnaire.Departement.NomDepartement;
                elefonctionnaire.AppendChild(souselement);

                souselement = docXml.CreateElement("salaire");
                souselement.InnerText = fonctionnaire.Salaire.ToString();
                elefonctionnaire.AppendChild(souselement);

            }
            docXml.Save(fileName);
        }
    }

}
