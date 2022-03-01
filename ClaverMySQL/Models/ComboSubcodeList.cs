using System.Collections.Generic;
using DataStorage;

namespace ClaverMySQL.Models
{
    public class ComboSubcodeList : List<string>
    {
        public ComboSubcodeList()
        {
            var sSQL =
                "SELECT CONCAT(SumOms,'_', Rekeningnummer, '_(', ID_SubCode, ')' ) " +
                "FROM tbl_SubCode WHERE !ISNULL(Rij) ORDER BY Rekeningnummer, ID_SubCode";
            var db = GenericDataRead.GetData(sSQL);

            this.Add("");
            for (var intX = 0; intX < db.Rows.Count; intX++)
            {
                Add(db.Rows[intX][0].ToString());
            }
        }
    }
}
