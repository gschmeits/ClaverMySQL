using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;

namespace ClaverMySQL.Models
{
    public class ComboSubcodeList333 : List<string>
    {
        public ComboSubcodeList333()
        {
            var sSQL =
                "SELECT CONCAT(SumOms,'_', Rekeningnummer, '_(', ID_SubCode, ')' ) " +
                "FROM tbl_SubCode WHERE !ISNULL(Rij) AND " +
                "Rekeningnummer = 333 " +
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
