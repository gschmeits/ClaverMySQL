using System;
using System.Collections.Generic;
using System.Data;
using DataStorage.Data;

namespace DataStorage.Classes
{
    public class Personen
    {
        public List<PerPersoonAll> personen { get; set; }
        public List<PerPersoon> persoon { get; set; }
        public List<Donatie> donatie { get; set; }
    }

    public class PerPersoonAll
    {
        public string naam { get; set; }
        public string adresnummer { get; set; }
    }

    public class PerPersoon
    {
        public string adresnummer { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string plaats { get; set; }
    }

    public class Donatie
    {
        public string codeomschrijving { get; set; }
        public string boekdatum { get; set; }
        public decimal bedrag { get; set; }
        public string omschrijving { get; set; }
    }

    public class PerPersoonFactory
    {
        public List<PerPersoonAll> GetPerPersoonAll()
        {
            var personen = new List<PerPersoonAll>();
            var sSQL = clsDataPerPersoon.GetPersonenAll();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    personen.Add(new PerPersoonAll
                    {
                        adresnummer = dt.Rows[intX][0].ToString(),
                        naam = dt.Rows[intX][1].ToString()
                    });
                }
            }
            return personen;
        }

        public List<PerPersoonAll> GetPerPersoonAllNummer()
        {
            var personen = new List<PerPersoonAll>();
            var sSQL = clsDataPerPersoon.GetPersonenAllNummer();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    personen.Add(new PerPersoonAll
                    {
                        adresnummer = dt.Rows[intX][0].ToString(),
                        naam = dt.Rows[intX][1].ToString()
                    });
                }
            }
            return personen;
        }

        public List<PerPersoon> GetPerPersoon(string adresnummer)
        {
            var personen = new List<PerPersoon>();
            var sSQL = clsDataPerPersoon.GetPersonen(adresnummer);
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    personen.Add(new PerPersoon
                    {
                        adresnummer = dt.Rows[intX][0].ToString(),
                        naam = dt.Rows[intX][1].ToString(),
                        adres = dt.Rows[intX][2].ToString(),
                        plaats = dt.Rows[intX][3].ToString()
                    });
                }
            }
            return personen;
        }

        public List<Donatie> GetDonaties(string adresnummer)
        {
            var donatie = new List<Donatie>();
            var sSQL = clsDataPerPersoon.GetBijdragen(adresnummer);
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    donatie.Add(new Donatie
                    {
                        codeomschrijving = dt.Rows[intX][0].ToString(),
                        boekdatum = dt.Rows[intX][1].ToString(),
                        bedrag = Convert.ToDecimal(dt.Rows[intX][2].ToString()),
                        omschrijving = dt.Rows[intX][3].ToString()
                    });
                }
            }
            return donatie;
        }
    }
}
