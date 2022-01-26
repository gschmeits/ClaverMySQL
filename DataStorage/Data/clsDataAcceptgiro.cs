namespace DataStorage.Data
{
    public class clsDataAcceptgiro
    {
        public static string GetCode()
        {
            var sSQL = "SELECT IDCode, CONCAT(IDCode, ' ', CodeOmschrijving) "; 
            sSQL += "FROM tbl_CodeTabel ";
            sSQL += "WHERE MetNaam = 1 ORDER BY IDCode";
            return sSQL;
        }

        public static string GetCount()
        {
            var sSQL = "SELECT COUNT(*) FROM tb_CodeTabel";
            return sSQL;
        }
    }
}