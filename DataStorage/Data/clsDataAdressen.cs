using System;

namespace DataStorage.Data
{
    public static class clsDataAdressen
    {
        public static string cmbAdresnummerSQL()
        {
            var sSQL = "SELECT adresnummer FROM tbl_Adressen ORDER BY adresnummer, " +
                       "achternaam, titel_en_voornaam ";
            return sSQL;
        }

        public static string cmNaamSQL()
        {
            var sSQL = "SELECT CONCAT(achternaam, ', ', titel_en_voornaam ) as naam, " +
                       "adresnummer " +
                       "FROM tbl_Adressen ORDER BY achternaam, titel_en_voornaam";
            return sSQL;
        }

        public static string cmbNaamBedragSQL()
        {
            var sSQL =
                "SELECT a.achternaam + ', ' + a.titel_en_voornaam] as naam, " +
                "a.adresnummer, sum(b.bedrag) as bedrag FROM tbl_Mutaties as b, " +
                "tbl_Adressen as a WHERE b.NaamBetaler = a.adresnummer " +
                "AND a.dank = 1" +
                "GROUP BY a.achternaam, a.titel_en_voornaam, a.adresnummer " +
                "ORDER BY a.achternaam, a.titel_en_voornaam";
            return sSQL;
        }

        public static string cmbPostcodeSQL()
        {
            var sSQL = "SELECT DISTINCT CONCAT(postcode, ' ',  IF(huisnummer IS NULL,' ', huisnummer))" +
                       " AS postcode1, postcode, huisnummer, adresnummer " +
                       "FROM tbl_Adressen where LENGTH(postcode) > 3 " +
                       "AND postcode <> '0000' ORDER BY postcode";
            return sSQL;
        }

        public static string cmbAdressenSQL()
        {
            var sSQL = "SELECT adresnummer, titel_en_voornaam, " +
                       "achternaam, straat, huisnummer, " +
                       "postcode, plaats, land, aantal, " +
                       "LETTERA, Verwijderd, Reden, kalender, " +
                       "IBAN, Bankrekening, Girorekening, Dank, Opmerking, " +
                       "email, telefoon, Brief FROM tbl_Adressen";
            return sSQL;
        }

        public static string cmbAdressenBedragSQL()
        {
            var sSQL =
                "select a.adresnummer, a.titel_en_voornaam], a.achternaam, a.email, " +
                "sum(b.bedrag) as bedrag from tbl_mutaties as b, tbl_Adressen as a " +
                "where b.NaamBetaler = a.adresnummer AND a.dank = 1" +
                " group by a.adresnummer, " +
                "a.titel_en_voornaam, a.achternaam, a.email " +
                "ORDER BY a.adresnummer";
            return sSQL;
        }

        public static string cmbEmailSQL()
        {
            var sSQL =
                "SELECT adresnummer, [titel en voornaam], achternaam, straat, huisnummer, " +
                "postcode, " +
                "plaats, land, aantal, LETTERA, Verwijderd, Reden, " +
                "kalender, IBAN, Bankrekening, Girorekening, Dank, " +
                "Opmerking, email, telefoon, Brief FROM adressen  " +
                "WHERE NULLIF(email, '') IS NOT NULL ";
            return sSQL;
        }

        public static string cmbEmailBedragSQL()
        {
            var sSQL =
                "SELECT a.achternaam + ', ' + a.titel_en_voornaam as naam, " +
                "a.adresnummer, a.email, sum(b.bedrag) as bedrag " +
                "FROM tbl_mutaties as b, tbl_Adressen as a " +
                "WHERE b.NaamBetaler = a.adresnummer " +
                "AND NULLIF(email, '') IS NOT NULL AND " +
                "a.dank = 1" +
                "GROUP BY a.achternaam, a.[titel en voornaam], " +
                "a.adresnummer, a.email " +
                "ORDER BY a.email, a.achternaam, " +
                "a.titel_en_voornaam";
            return sSQL;
        }

        public static string VulGegevensSQL(string strAdresnummer)
        {
            var sSQL = "SELECT adresnummer, titel_en_voornaam, " +
                       "achternaam, straat, huisnummer, postcode, plaats, land, " +
                       "aantal, LETTERA, Verwijderd, reden, kalender,bankrekening, " +
                       "Girorekening, IBAN, Dank, Opmerking, email, telefoon, Brief, " +
                       "donatieTotaal, donatieBedankt " +
                       "FROM tbl_Adressen WHERE adresnummer =" + strAdresnummer;
            return sSQL;
        }

        public static string VulGegevensMailSQL(string strEmail)
        {
            var sSQL = "SELECT adresnummer, titel_en_voornaam, " +
                       "achternaam, straat, huisnummer, postcode, plaats, land, " +
                       "aantal, LETTERA, Verwijderd, reden, kalender,bankrekening, " +
                       "Girorekening, IBAN, Dank, Opmerking, email, telefoon, Brief " +
                       "FROM tbl_Adressen WHERE email ='" + strEmail + "';";
            return sSQL;
        }

        public static void insertAdres(string naam, string achternaam, string straat, string huisnummer, string postcode, string plaats, string land, string aantal, string letterA, Boolean verwijderd, string reden, Boolean kalender, string bank, string giro, string iban, Boolean dank, string opmerking, string email, string telefoon, string adresnummer)
        {
            var sSQL = "";

            sSQL = "INSERT INTO tbl_Adressen (titel_en_voornaam, achternaam, straat, huisnummer, ";
            sSQL += "postcode, plaats, land, aantal, LETTERA, Verwijderd, Reden, kalender, ";
            sSQL += "Bankrekening, Girorekening, IBAN, Dank, Opmerking, email, telefoon) VALUES ('";

            sSQL += naam.Replace("'", "''") + "', '";
            sSQL += achternaam.Replace("'", "''") + "', '";
            sSQL += straat.Replace("'", "''") + "', '";
            sSQL += huisnummer.Replace("'", "''") + "', '";
            sSQL += postcode.Replace("'", "''") + "', '";
            sSQL += plaats.Replace("'", "''") + "', '";
            sSQL += land.Replace("'", "''") + "', '";
            sSQL += aantal.Replace("'", "''") + "', '";
            sSQL += letterA.Replace("'", "''") + "', '";
            if (verwijderd == true)
            {
                sSQL += "1', '";
            }
            if (verwijderd == false)
            {
                sSQL += "0', '";
            }
            sSQL += reden.Replace("'", "''") + "', '";
            if (kalender == true)
            {
                sSQL += "1', '";
            }
            if (kalender == false)
            {
                sSQL += "0', '";
            }
            sSQL += bank.Replace("'", "''") + "', '";
            sSQL += giro.Replace("'", "''") + "', '";
            sSQL += iban.Replace("'", "''") + "', '";
            if (dank == true)
            {
                sSQL += "1', '";
            }
            if (dank == false)
            {
                sSQL += "0', '";
            }
            sSQL += opmerking.Replace("'", "''") + "', '";
            sSQL += email.Replace("'", "''") + "', '";
            sSQL += telefoon.Replace("'", "''") + "');";

            GenericDataRead.INUPDEL(sSQL);
        }

        public static void updateAdres(string naam, string achternaam, string straat, string huisnummer, string postcode, string plaats, string land, string aantal, string letterA, Boolean verwijderd, string reden, Boolean kalender, string bank, string giro, string iban, Boolean dank, string opmerking, string email, string telefoon, string adresnummer)
        {
            var sSQL = "";
            sSQL += "UPDATE tbl_Adressen SET ";
            sSQL += "titel_en_voornaam = '" + naam.Replace("'", "''") + "', ";
            sSQL += "achternaam = '" + achternaam.Replace("'", "''") + "', ";
            sSQL += "straat = '" + straat.Replace("'", "''") + "', ";
            sSQL += "huisnummer = '" + huisnummer.Replace("'", "''") + "', ";
            sSQL += "postcode = '" + postcode.Replace("'", "''") + "', ";
            sSQL += "plaats = '" + naam.Replace("'", "''") + "', ";
            sSQL += "land = '" + land.Replace("'", "''") + "', ";
            sSQL += "aantal = " + aantal.Replace("'", "''") + ", ";
            sSQL += "lettera = '" + letterA.Replace("'", "''") + "', ";
            if (verwijderd == true)
            {
                sSQL += "verwijderd = 1, ";
            }
            if (verwijderd == false)
            {
                sSQL += "verwijderd = 0, ";
            }
            sSQL += "reden = '" + reden.Replace("'", "''") + "', ";
            if (kalender == true)
            {
                sSQL += "kalender = 1, ";
            }
            if (kalender == false)
            {
                sSQL += "kalender = 0, ";
            }

            sSQL += "bankrekening = '" + bank.Replace("'", "''") + "', ";
            sSQL += "girorekening = '" + giro.Replace("'", "''") + "', ";
            sSQL += "IBAN = '" + iban.Replace("'", "''") + "', ";
            if (dank == true)
            {
                sSQL += "dank = 1, ";
            }
            if (dank == false)
            {
                sSQL += "dank = 0, ";
            }

            sSQL += "opmerking = '" + opmerking.Replace("'", "''") + "', ";
            sSQL += "email = '" + email.Replace("'", "''") + "', ";
            sSQL += "telefoon = '" + telefoon.Replace("'", "''") + "' ";
            sSQL += "WHERE adresnummer = " + adresnummer;

            GenericDataRead.INUPDEL(sSQL);
        }
    }
}
