using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage;

namespace DataStorage.Classes
{
    public class Bankafschriften
    {
        public List<GrootboekList> grootboekrekeningen { get; set; }
    }

    public class GrootboekList
    {
        public string grootboek_num_oms { get; set; }
    }


    public class BankafschriftenFactory
    {
        public List<GrootboekList> GetGrootboekList()
        {
            var grootboekrekeningen = new List<GrootboekList>();
            var sSQL =
                "SELECT CONCAT(IDCode, ' ', CodeOmschrijving) AS grootboek_num_om FROM tbl_CodeTabel ORDER BY IDCode";
            var db = GenericDataRead.GetData(sSQL);
            if (db.Rows.Count > 0)
            {
                for (var intX = 0; intX < db.Rows.Count; intX++)
                {
                    grootboekrekeningen.Add(new GrootboekList
                    {
                        grootboek_num_oms = db.Rows[intX][0].ToString()
                    });
                }
            }
            return grootboekrekeningen;
        }
    }
}
