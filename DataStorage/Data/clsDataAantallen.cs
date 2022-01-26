namespace DataStorage.Data
{
    public static class clsDataAantallen
    {
        public static string abonnementen()
        {
            var sSQL = "SELECT tbl_Adressen.LETTERA,  Count(*) AS TotaalAantal, FORMAT(Count(*), 0, 'de_DE') AS TotaalAantals ";
            sSQL += "FROM tbl_Adressen Where (((tbl_Adressen.Verwijderd) = 0)) and lettera is not null ";
            sSQL += "GROUP BY tbl_Adressen.LETTERA ";
            sSQL += "ORDER BY LetterA";
            return sSQL;
        }

        public static string echo()
        {
            var sSQL = "";
            sSQL += "SELECT LetterA, Count(*) AS TotaalAantal, FORMAT(Count(*), 0, 'de_DE') AS TotaalAantals ";
            sSQL += "FROM tbl_Adressen ";
            sSQL += " WHERE LEFT(LetterA, 1) = 'A' ";
            sSQL += "AND Verwijderd = 0 ";
            sSQL += "GROUP BY LetterA ";
            sSQL += "ORDER BY LetterA ";
            return sSQL;
        }

        public static string kalender()
        {
            var sSQL = "SELECT tbl_Adressen.LETTERA, Count(*) AS TotaalAantal,  FORMAT(Count(*), 0, 'de_DE') AS TotaalAantals ";
            sSQL += "FROM tbl_Adressen ";
            sSQL += "WHERE (((tbl_Adressen.Verwijderd) = 0)) ";
            sSQL += "GROUP BY tbl_Adressen.LETTERA, tbl_Adressen.kalender ";
            sSQL += "HAVING (((tbl_Adressen.kalender)=1))";
            sSQL += "ORDER BY LETTERA";
            return sSQL;
        }
    }
}
