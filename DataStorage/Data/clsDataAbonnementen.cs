using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataAbonnementen
    {
        public static string HaalAA4A7WW1()
        {
            var sSQL = "SELECT adresnummer, titel_en_voornaam, achternaam, straat, ";
            sSQL += "huisnummer , postcode, plaats, Opmerking, LETTERA ";
            sSQL += "FROM tbl_Adressen ";
            sSQL += "WHERE (((LETTERA) = 'A') AND ((Verwijderd) = 0))  ";
            sSQL += "OR (((LETTERA) = 'A4') AND ((Verwijderd) = 0))  ";
            sSQL += "OR (((LETTERA) = 'A7') AND ((Verwijderd) = 0)) ";
            sSQL += "OR (((LETTERA) = 'W') AND ((Verwijderd) = 0)) ";
            sSQL += "OR (((LETTERA) = 'W1') AND ((Verwijderd) = 0)) ";
            sSQL += "ORDER BY LETTERA;";
            return sSQL;
        }

        public static string HaalGegevensAbonnee(string jaar, string abonnee)
        {
            var sSQL = "SELECT IFNULL(Sum(tbl_Mutaties.Bedrag), 0) AS SomVanBedrag ";
            sSQL += "FROM tbl_Mutaties ";
            sSQL += "WHERE (((Year(boekdatum)) >= " + jaar + ")) and NaamBetaler = " + abonnee;
            return sSQL;
        }
    }
}
