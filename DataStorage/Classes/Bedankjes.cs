using System;
using System.Collections.Generic;
using System.Data;
using DataStorage.Data;

namespace DataStorage.Classes
{
    public class Bedankjes
    {
        public List<AantalBedankje> aantalBedankjes { get; set; }
        public List<BedankjeHoger> bedankjeHoger { get; set; }
        public List<BedankjeLager> bedankjeLager { get; set; }
        public List<BedankjeIndividueel> bedankjeIndividueel { get; set; }
    }

    public class AantalBedankje
    {
        public Int32 aantal { get; set; }
    }

    public class BedankjeHoger
    {
        public string titel { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string email { get; set; }
        public decimal bedrag { get; set; }
    }

    public class BedankjeLager
    {
        public string titel { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string email { get; set; }
        public decimal bedrag { get; set; }
    }

    public class BedankjeIndividueel
    {
        public string titel { get; set; }
        public string naam { get; set; }
        public string adres { get; set; }
        public string postcode { get; set; }
        public string woonplaats { get; set; }
        public string email { get; set; }
    }


    public class BedankjesFactory
    {
        public List<AantalBedankje> GetAantalBedankjes()
        {

            var aantalBedankjes = new List<AantalBedankje>();
            var sSQL = clsDataDank.aantalBedankjes();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    aantalBedankjes.Add(new AantalBedankje
                    {
                        aantal = Convert.ToInt32(dt.Rows[intX][0].ToString())
                    });
                }
            }
            return aantalBedankjes;
        }

        public List<BedankjeHoger> GetBedankjeHoger()
        {
            var bedankjeHoger = new List<BedankjeHoger>();
            var sSQL = clsDataDank.maakDankbriefMeer();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    bedankjeHoger.Add(new BedankjeHoger
                    {
                        titel = dt.Rows[intX][1].ToString(),
                        naam = dt.Rows[intX][2].ToString(),
                        adres = dt.Rows[intX][3].ToString() + " " + dt.Rows[intX][4].ToString(),
                        postcode = dt.Rows[intX][5].ToString(),
                        woonplaats = dt.Rows[intX][6].ToString(),
                        bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                        email = dt.Rows[intX][8].ToString()
                    });
                }
            }
            return bedankjeHoger;
        }

        public List<BedankjeHoger> GetBedankjeHogerEmail()
        {
            var bedankjeHoger = new List<BedankjeHoger>();
            var sSQL = clsDataDank.maakDankbriefMeerEmail();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    bedankjeHoger.Add(new BedankjeHoger
                    {
                        titel = dt.Rows[intX][1].ToString(),
                        naam = dt.Rows[intX][2].ToString(),
                        adres = dt.Rows[intX][3].ToString() + " " + dt.Rows[intX][4].ToString(),
                        postcode = dt.Rows[intX][5].ToString(),
                        woonplaats = dt.Rows[intX][6].ToString(),
                        bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                        email = dt.Rows[intX][8].ToString()
                    });
                }
            }
            return bedankjeHoger;
        }

        public List<BedankjeLager> GetBedankjeLager()
        {
            var bedankjeLager = new List<BedankjeLager>();
            var sSQL = clsDataDank.maakDankbriefMinder();
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    bedankjeLager.Add(new BedankjeLager
                    {
                        titel = dt.Rows[intX][1].ToString(),
                        naam = dt.Rows[intX][2].ToString(),
                        adres = dt.Rows[intX][3].ToString() + " " + dt.Rows[intX][4].ToString(),
                        postcode = dt.Rows[intX][5].ToString(),
                        woonplaats = dt.Rows[intX][6].ToString(),
                        bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                        email = dt.Rows[intX][9].ToString()
                    });
                }
            }
            return bedankjeLager;
        }

        public List<BedankjeIndividueel> GetBedankjeIndividueel(string betalerCode)
        {
            var bedankjeIndividueel = new List<BedankjeIndividueel>();
            var sSQL = clsDataDank.maakDankbriefIndividueel(betalerCode);
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    bedankjeIndividueel.Add(new BedankjeIndividueel
                    {
                        titel = dt.Rows[intX][0].ToString(),
                        naam = dt.Rows[intX][1].ToString(),
                        adres = dt.Rows[intX][2].ToString(),
                        postcode = dt.Rows[intX][3].ToString(),
                        woonplaats = dt.Rows[intX][4].ToString(),
                        email = dt.Rows[intX][5].ToString()
                    });
                }
            }
            return bedankjeIndividueel;
        }
    }
}
