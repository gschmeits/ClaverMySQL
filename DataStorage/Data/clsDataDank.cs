using System;

namespace DataStorage.Data
{
    public static class clsDataDank
    {
        public static string aantalBedankjes()
        {
            var sSQL = "SELECT count(ID) As Teller FROM tbl_Mutaties WHERE dank = 1 AND bedankt = 0 ";
            return sSQL;
        }

        public static string maakDankbriefMeer()
        {
            var bedrag = Convert.ToDouble(Functions.sBedragBedankjes("ConfigData.xml"));
            var sSQL = string.Empty;
            sSQL = "SELECT sum(a.bedrag) ";
            sSQL += "as totaalbedrag,  ";
            sSQL += "a.titel,  ";
            sSQL += "a.achternaam,  ";
            sSQL += "a.straat, ";
            sSQL += "a.huisnummer, ";
            sSQL += "a.postcode, ";
            sSQL += "a.plaats, ";
            sSQL += "a.land, ";
            sSQL += "a.email ";
            sSQL += "FROM (";
            sSQL += "SELECT DISTINCT tbl_Mutaties.Bedrag AS bedrag, ";
            sSQL += "tbl_Adressen.titel_en_voornaam as titel, ";
            sSQL += "tbl_Adressen.achternaam as achternaam, ";
            sSQL += "tbl_Adressen.straat as straat, ";
            sSQL += "tbl_Adressen.huisnummer as huisnummer, ";
            sSQL += "tbl_Adressen.postcode as postcode, ";
            sSQL += "tbl_Adressen.plaats as plaats, ";
            sSQL += "tbl_Adressen.land as land, ";
            sSQL += "tbl_Mutaties.NaamBetaler as betaler, ";
            sSQL += "tbl_Adressen.email as email ";
            sSQL += "FROM tbl_Mutaties INNER JOIN tbl_Adressen ON tbl_Mutaties.NaamBetaler" ;
            sSQL += "tbl_Adressen.adresnummer ";
            sSQL += "WHERE tbl_Mutaties.Dank = 1 And tbl_Mutaties.Bedankt = 0 and tbl_Mutaties.bedrag >= 10 AND email IS NULL) ";
            sSQL += "as a ";
            sSQL += "GROUP BY a.betaler, a.titel, a.achternaam, a.straat, a.huisnummer, a.postcode, a.plaats, a.land, a.email ";
            sSQL += "ORDER BY a.postcode, a.huisnummer";
            return sSQL;
        }

        public static string maakDankbriefMeerEmail()
        {
            var bedrag = Convert.ToDouble(Functions.sBedragBedankjes("ConfigData.xml"));
            var ssQL = "SELECT DISTINCT tbl_Mutaties.Bedrag, tbl_Adressen.titel_en_voornaam, ";
            ssQL += "tbl_Adressen.achternaam, tbl_Adressen.straat, tbl_Adressen.huisnummer, tbl_Adressen.postcode, tbl_Adressen.plaats, tbl_Adressen.land, ";
            ssQL += "tbl_Mutaties.NaamBetaler, tbl_Adressen.email ";
            ssQL += "FROM tbl_Mutaties INNER JOIN adressen ON tbl_Mutaties.NaamBetaler = tbl_Adressen.adresnummer  ";
            ssQL += "WHERE (((tbl_Mutaties.Dank) = 1) And ((tbl_Mutaties.Bedankt) = 0) and ((tbl_Mutaties.bedrag) >= 10 ))  ";
            ssQL += "tbl_Adressen.email.length > 0 ";
            ssQL += "ORDER BY tbl_Mutaties.NaamBetaler ";

            ssQL = "SELECT sum(a.bedrag) ";
            ssQL += "as totaalbedrag,  ";
            ssQL += "a.titel,  ";
            ssQL += "a.achternaam,  ";
            ssQL += "a.straat, ";
            ssQL += "a.huisnummer, ";
            ssQL += "a.postcode, ";
            ssQL += "a.plaats, ";
            ssQL += "a.land, ";
            ssQL += "a.email ";
            ssQL += "FROM (";
            ssQL += "SELECT DISTINCT tbl_Mutaties.Bedrag AS bedrag, ";
            ssQL += "tbl_Adressen.titel_en_voornaam as titel, ";
            ssQL += "tbl_Adressen.achternaam as achternaam, ";
            ssQL += "tbl_Adressen.straat as straat, ";
            ssQL += "tbl_Adressen.huisnummer as huisnummer, ";
            ssQL += "tbl_Adressen.postcode as postcode, ";
            ssQL += "tbl_Adressen.plaats as plaats, ";
            ssQL += "tbl_Adressen.land as land, ";
            ssQL += "tbl_Mutaties.NaamBetaler as betaler, ";
            ssQL += "tbl_Adressen.email as email ";
            ssQL += "FROM tbl_Mutaties INNER JOIN adressen ON tbl_Mutaties.NaamBetaler = tbl_Adressen.adresnummer ";
            ssQL += "WHERE tbl_Mutaties.Dank = 1 And tbl_Mutaties.Bedankt = 0 and tbl_Mutaties.bedrag >= " + bedrag + " AND LEN(email) > 0) ";
            ssQL += "as a ";
            ssQL += "GROUP BY a.betaler, a.titel, a.achternaam, a.straat, a.huisnummer, a.postcode, a.plaats, a.land, a.email ";
            ssQL += "ORDER BY a.postcode, a.huisnummer";
            return ssQL;
        }

        public static string maakDankbriefMinder()
        {
            var ssQL = "SELECT DISTINCT tbl_Mutaties.Bedrag, tbl_Adressen.titel_en_voornaam], ";
            ssQL += "tbl_Adressen.achternaam, tbl_Adressen.straat, tbl_Adressen.huisnummer, tbl_Adressen.postcode, tbl_Adressen.plaats, " +
                    "tbl_Adressen.land, ";
            ssQL += "tbl_Mutaties.NaamBetaler, tbl_Adressen.email ";
            ssQL += "FROM tbl_Mutaties INNER JOIN adressen ON tbl_Mutaties.NaamBetaler = tbl_Adressen.adresnummer  ";
            ssQL += "WHERE (((tbl_Mutaties.Dank) = 1) And ((tbl_Mutaties.Bedankt) = 0) and ((tbl_Mutaties.bedrag) < 10 ))  ";
            ssQL += "ORDER BY tbl_Mutaties.NaamBetaler ";
            return ssQL;
        }

        public static string maakDankbriefAllen()
        {
            var ssQL = "SELECT titel_en_voornaam + ' ' + achternaam AS Naam_adres, straat + ' ' + huisnummer AS Adres_adres, ";
            ssQL += "postcode + ' ' + plaats AS Plaats_Adres FROM tbl_Adressen ";
            ssQL += "ORDER BY achternaam ";
            return ssQL;

        }

        public static string maakDankbriefIndividueel(string betalerCode)
        {
            var ssQL = "SELECT titel_en_voornaam, achternaam , straat + ' ' + huisnummer AS adres, ";
            ssQL += "postcode , plaats, email FROM tbl_Adressen ";
            ssQL += "WHERE adresnummer = " + betalerCode;
            return ssQL;
        }

        public static string updateDankMutaties()
        {
            var sSQL = "UPDATE tbl_Mutaties SET tbl_Mutaties.Bedankt = 1 ";
            sSQL += "WHERE (((tbl_Mutaties.Dank) = 1) AND ((tbl_Mutaties.Bedankt) = 0)) ";
            sSQL += "AND email IS NULL";
            return sSQL;
        }

        public static string updateDankMutatiesEmail()
        {
            var sSQL = "UPDATE tbl_Mutaties SET tbl_Mutaties.Bedankt = 1 ";
            sSQL += "WHERE (((tbl_Mutaties.Dank) = 1) AND ((tbl_Mutaties.Bedankt) = 0)) ";
            sSQL += "AND LEN(email) > 0";
            return sSQL;
        }
    }
}
