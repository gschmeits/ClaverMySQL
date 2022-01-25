using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataPerPersoon
    {
        public static string GetPersonenAll()
        {
            var sSQL = "SELECT adresnummer, CONCAT(achternaam, ', ', titel_en_voornaam) AS naam1 FROM tbl_Adressen ORDER BY achternaam";
            return sSQL;
        }

        public static string GetPersonenAllNummer()
        {
            var sSQL = "SELECT adresnummer, CONCAT(achternaam, ', ', titel_en_voornaam) AS naam1 FROM tbl_Adressen ORDER BY adresnummer";
            return sSQL;
        }

        public static string GetPersonen(string adresnummer)
        {
            var sSQL = "SELECT adresnummer, CONCAT(achternaam, ', ', titel_en_voornaam) AS naam1, straat + ' ' + huisnummer as adres, ";
            sSQL += "postcode + ' ' + plaats as pplaats FROM tbl_Adressen WHERE adresnummer = " + adresnummer;
            sSQL += " ORDER BY achternaam ";
            return sSQL;
        }

        public static string GetBijdragen(string adresnummer)
        {
            var sSQL = "SELECT tbl_CodeTabel.CodeOmschrijving, tbl_Mutaties.Boekdatum, tbl_Mutaties.Bedrag,  tbl_Mutaties.Omschrijving ";
            sSQL += "FROM (tbl_Adressen RIGHT JOIN tbl_Mutaties ON tbl_Adressen.adresnummer = tbl_Mutaties.NaamBetaler) ";
            sSQL += "LEFT JOIN tbl_CodeTabel ON tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.IDCode ";
            sSQL += "Where (((tbl_Mutaties.NaamBetaler) = " + adresnummer + ")) ";
            sSQL += "ORDER BY  tbl_Mutaties.Boekdatum,tbl_Mutaties.NummerKostenplaats; ";
            return sSQL;
        }
    }
}
