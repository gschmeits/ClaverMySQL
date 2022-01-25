namespace DataStorage.Data
{
    public static class clsDataGrootboek
    {
        public static string sCodetabel()
        {
            var sSQL = string.Empty;
            sSQL = "SELECT idcode, codeomschrijving FROM tbl_CodeTabel ";
            return sSQL;
        }

        public static string sCodesoort()
        {
            var sSQL = string.Empty;
            sSQL = "SELECT idCode, code FROM tbl_CodeSoort";
            return sSQL;
        }

        public static string CodeOverzicht()
        {
            var sSQL = "SELECT IDCode , CodeOmschrijving  FROM tbl_CodeTabel ORDER BY IDCode;";
            return sSQL;
        }

        public static string sCodesoortCodeTabel()
        {
            var sSQL = string.Empty;
            sSQL =
                "SELECT ct.idcodesleutel, cs.Code as codeg, ct.IDCode as idcodeg, " +
                "ct.codeOmschrijving as eigen, " +
                "ct.CodeOmschrVreemd as vreemd, ct.Aantal " +
                "FROM tbl_CodeTabel as ct, tbl_CodeSoort as cs where ct.code = cs.idcode";

            return sSQL;
        }

        public static string sSelecteerGegevens(string sWaarde)
        {
            var sSQL = string.Empty;
            sSQL =
                "SELECT IDCodeSleutel, Code, IDCode, CodeOmschrijving, CodeOmschrVreemd, GeenExtraGegevens, "
                + "MetNaam, MetOmschr, MetSubcode, Subcode, Aantal, rij, kolom, zoekbegrippen " +
                "FROM tbl_CodeTabel WHERE IDCode = '"
                + sWaarde + "';";


            sSQL =
                "SELECT a.IDCodeSleutel, a.Code, a.IDCode, a.CodeOmschrijving, " +
                "a.CodeOmschrVreemd, a.GeenExtraGegevens, a.MetNaam, a.MetOmschr, " +
                "a.MetSubcode, a.Subcode, a.Aantal, a.rij, a.kolom, a.zoekbegrippen, b.Code " +
                "FROM tbl_CodeTabel as a, tbl_CodeSoort as b " +
                "WHERE a.IDCode = '" + sWaarde + "' AND b.idCode = a.Code; ";


            return sSQL;
        }

        public static string sUpdateCodetabel(
            string sCode,
            string sCodenummer,
            string sOmschrijving,
            string sOmschrijvingV,
            bool bGEG,
            bool bMN,
            bool bMO,
            bool bMS,
            bool bGetallen,
            string sID,
            string rij,
            string kolom,
            string zoekwaarden)
        {
            if (rij.Length == 0) rij = "";
            if (kolom.Length == 0) kolom = "";
            var sSQL = string.Empty;
            sSQL = "UPDATE tbl_CodeTabel SET Code = " + sCode + ", ";
            sSQL += "IDCode = '" + sCodenummer + "', ";
            sSQL += "CodeOmschrijving = '" + sOmschrijving.Replace("'", "''") +
                    "', ";
            sSQL += "CodeOmschrVreemd = '" + sOmschrijvingV.Replace("'", "''") +
                    "', ";
            sSQL += "GeenExtraGegevens = ";
            if (bGEG)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += "MetNaam = ";

            if (bMN)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += "MetOmschr = ";
            if (bMO)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += "MetSubcode = ";
            if (bMS)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += "Aantal = ";
            if (bGetallen)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += "Subcode = 0, ";
            sSQL += "rij = '" + rij + "', ";
            sSQL += "kolom = '" + kolom + "', ";
            sSQL += "zoekbegrippen = '" + zoekwaarden.Replace("'", "''") + "' ";
            sSQL += "WHERE idcodesleutel = '" + sID + "';";
            return sSQL;
        }

        public static string sInsertCodetabel(
            string sCode,
            string sCodenummer,
            string sOmschrijving,
            string sOmschrijvingV,
            bool bGEG,
            bool bMN,
            bool bMO,
            bool bMS,
            bool bGetallen,
            string rij,
            string kolom,
            string zoekwaarden)
        {
            if (rij.Length == 0) rij = "";
            if (kolom.Length == 0) kolom = "";
            var sSQL = string.Empty;
            sSQL =
                "INSERT INTO tbl_CodeTabel (Code, IDCode, CodeOmschrijving, CodeOmschrVreemd, GeenExtraGegevens, MetNaam, MetOmschr, ";
            sSQL +=
                "MetSubcode, Subcode, Aantal, rij, kolom, zoekbegrippen) VALUES (";
            sSQL += sCode + ",";
            sSQL += "'" + sCodenummer + "', ";
            sSQL += "'" + sOmschrijving.Replace("'", "''") + "', ";
            sSQL += "'" + sOmschrijvingV.Replace("'", "''") + "',";
            if (bGEG)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            if (bMN)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            if (bMO)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            if (bMS)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            if (bGetallen)
                sSQL += 1 + ", ";
            else
                sSQL += 0 + ", ";

            sSQL += 0 + ",' ";
            sSQL += rij + "',' ";
            sSQL += kolom + "', '";
            sSQL += zoekwaarden.Replace("'", "''") + "'";
            sSQL += ");";

            return sSQL;
        }
    }
}