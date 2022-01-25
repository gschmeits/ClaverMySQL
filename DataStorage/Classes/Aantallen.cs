using System;
using System.Collections.Generic;
using DataStorage.Data;

namespace DataStorage.Classes
{
    public class Aantallen
    {
        public List<Abonnement> abonnementen { get; set; }
        public List<Echo> echos { get; set; }
        public List<Kalender> kalender { get; set; }
    }

    public class Abonnement
    {
        public string lettera { get; set; }
        public int aantal { get; set; }
        public string aantals { get; set; }
    }

    public class Echo
    {
        public string lettera { get; set; }
        public int aantal { get; set; }
        public string aantals { get; set; }
    }

    public class Kalender
    {
        public string lettera { get; set; }
        public int aantal { get; set; }
        public string aantals { get; set; }
    }

    public class AantalFactory
    {
        public List<Abonnement> GetAantalAbonnementen()
        {
            var abonnementen = new List<Abonnement>();
            var sSQL = clsDataAantallen.abonnementen();
            var dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
                for (var intX = 0; intX < dt.Rows.Count; intX++)
                    abonnementen.Add(new Abonnement
                    {
                        lettera = dt.Rows[intX][0].ToString(),
                        aantal = Convert.ToInt32(dt.Rows[intX][1].ToString()),
                        aantals = dt.Rows[intX][2].ToString()
                    });
            return abonnementen;
        }

        public List<Echo> GetAantalEchos()
        {
            var echos = new List<Echo>();
            var sSQL = clsDataAantallen.echo();
            var dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
                for (var intX = 0; intX < dt.Rows.Count; intX++)
                    echos.Add(new Echo
                    {
                        lettera = dt.Rows[intX][0].ToString(),
                        aantal = Convert.ToInt32(dt.Rows[intX][1].ToString()),
                        aantals = dt.Rows[intX][2].ToString()
                    });
            return echos;
        }

        public List<Kalender> GetAantalKalenders()
        {
            var kalenders = new List<Kalender>();
            var sSQL = clsDataAantallen.kalender();
            var dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
                for (var intX = 0; intX < dt.Rows.Count; intX++)
                    kalenders.Add(new Kalender
                    {
                        lettera = dt.Rows[intX][0].ToString(),
                        aantal = Convert.ToInt32(dt.Rows[intX][1].ToString()),
                        aantals = dt.Rows[intX][2].ToString()
                    });
            return kalenders;
        }
    }
}