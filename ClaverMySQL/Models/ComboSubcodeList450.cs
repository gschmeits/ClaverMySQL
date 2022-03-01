using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;

namespace ClaverMySQL.Models
{
    public class ComboSubcodeList450 : List<string>
    {
        public ComboSubcodeList450()
        {
            var sSQL =
                "SELECT CONCAT(SumOms,'_', Rekeningnummer, '_(', ID_SubCode, ')' ) " +
                "FROM tbl_SubCode WHERE !ISNULL(Rij) AND " +
                "Rekeningnummer = 450 " +
                " ORDER BY Rekeningnummer, ID_SubCode";
            var db = GenericDataRead.GetData(sSQL);

            this.Add("");
            for (var intX = 0; intX < db.Rows.Count; intX++)
            {
                Add(db.Rows[intX][0].ToString());
            }
        }
    }
}
