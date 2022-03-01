using DataStorage;

namespace ClaverMySQL.Models
{
    using System.Collections.Generic;
    public class ComboGrootboekListB : List<string>
    {
        public ComboGrootboekListB()
        {
            var sSQL = "SELECT CONCAT(IDCode, ' ', CodeOmschrijving) " +
                       "FROM tbl_CodeTabel ORDER BY IDCode";
            var db = GenericDataRead.GetData(sSQL);

            this.Add("");
            for (var intX = 0; intX < db.Rows.Count; intX++)
            {
                Add(db.Rows[intX][0].ToString());
            }
        }
    }
}
