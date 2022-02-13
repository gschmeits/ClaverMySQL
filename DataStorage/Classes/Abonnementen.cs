using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage.Data;

namespace DataStorage.Classes
{
    public class Abonnementen
    {
        public List<HaalAA4A7WW1> haalaa4a7ww1 { get; set; }
        public List<NietBetalend> nietbetalenden { get; set; }
    }

    public class HaalAA4A7WW1
    {
        public string adresnummer { get; set; }
        public string titel { get; set; }
        public string achternaam { get; set; }
        public string straat { get; set; }
        public string huisnummer { get; set; }
        public string postcode { get; set; }
        public string plaats { get; set; }
        public string opmerking { get; set; }
        public string lettera { get; set; }
    }

    public class NietBetalend
    {
        public string titel { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string opmerking { get; set; }
    }

    public class AbonnementenFactory
    {
        public List<HaalAA4A7WW1> GetHaalAA4A7WW1()
        {
            var haalaa4a7ww1 = new List<HaalAA4A7WW1>();
            var sSQL = clsDataAbonnementen.HaalAA4A7WW1();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    haalaa4a7ww1.Add(new HaalAA4A7WW1
                    {
                        adresnummer = dt.Rows[intX][0].ToString(),
                        titel = dt.Rows[intX][1].ToString(),
                        achternaam = dt.Rows[intX][2].ToString(),
                        straat = dt.Rows[intX][3].ToString(),
                        huisnummer = dt.Rows[intX][4].ToString(),
                        postcode = dt.Rows[intX][5].ToString(),
                        plaats = dt.Rows[intX][6].ToString(),
                        opmerking = dt.Rows[intX][7].ToString(),
                        lettera = dt.Rows[intX][8].ToString()
                    });
                }
            }
            return haalaa4a7ww1;
        }

        public List<NietBetalend> GetNietBetalend(string jaar)
        {
            var sSQL = "";
            DataTable dt = new DataTable();
            var libraryFactory = new AbonnementenFactory();
            var haal = new List<HaalAA4A7WW1>();
            haal = libraryFactory.GetHaalAA4A7WW1();
            var nietbetalenden = new List<NietBetalend>();
            foreach (var niet in haal)
            {
                sSQL = clsDataAbonnementen.HaalGegevensAbonnee(jaar, niet.adresnummer);
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count == 0)
                {
                    nietbetalenden.Add(new NietBetalend
                    {
                        titel = niet.titel,
                        naam = niet.achternaam,
                        adres = niet.straat + " " + niet.huisnummer,
                        postcode = niet.postcode,
                        woonplaats = niet.plaats,
                        opmerking = niet.opmerking
                    });
                }
            }
            return nietbetalenden;
        }
    }
}
