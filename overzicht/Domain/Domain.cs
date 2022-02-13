using System;
using System.Collections.Generic;
using System.Windows;

namespace Domain
{
    public class MaandLibrary
    {
        public List<Bank> Banken { get; set; }
        public List<BankSaldo> BankSaldi { get; set; }

        public List<BeginBalans> Beginbalans { get; set; }

        public List<Balansio> Eindbalans { get; set; }

        public List<Balansio448> Eindbalans448 { get; set; }

        public List<BalansioRest> EindbalansRest { get; set; }

        public List<GrootBoek> Grootboek { get; set; }

        public List<Specificatie318> Spec318 { get; set; }

        public List<Specificatie330> Spec330 { get; set; }

        public List<Specificatie331> Spec331 { get; set; }

        public List<Specificatie333> Spec333 { get; set; }

        public List<Specificatie336> Spec336 { get; set; }

        public List<Specificatie426> Spec426 { get; set; }

        public List<Specificatie451> Spec451 { get; set; }

        public List<Specificatie452> Spec452 { get; set; }
    }

    public class Bank
    {
        public short balans { get; set; }
        public int bankId { get; set; }

        public string bankOms { get; set; }

        public int kolom { get; set; }

        public string nummer { get; set; }

        public int rij { get; set; }

        public string soort { get; set; }
    }

    public class BankSaldo
    {
        public string bank { get; set; }

        public string datum { get; set; }

        public string kolom { get; set; }

        public string nummer { get; set; }

        public string omschrijving { get; set; }
        public string rij { get; set; }

        public decimal saldo { get; set; }
    }

    public class BeginBalans
    {
        public string datum { get; set; }
        public string idcode { get; set; }

        public decimal saldo { get; set; }
    }

    public class GrootBoek
    {
        public int code { get; set; }

        public int kolom { get; set; }
        public string kostenplaats { get; set; }

        public int rij { get; set; }

        public decimal saldo { get; set; }
    }

    public class Specificatie318
    {
        public int aantal { get; set; }

        public decimal bedrag { get; set; }
        public string naam { get; set; }

        public string omschrijving { get; set; }
    }

    public class Specificatie330
    {
        public int rij { get; set; }
        public decimal saldo { get; set; }
    }

    public class Specificatie331
    {
        public decimal bedrag { get; set; }
        public string donateur { get; set; }

        public string omschrijving { get; set; }
    }

    public class Specificatie333
    {
        public decimal bedrag { get; set; }
        public string omschrijving { get; set; }
    }

    public class Specificatie336
    {
        public decimal bedrag { get; set; }

        public string datum { get; set; }

        public string kostenplaats { get; set; }
        public string naamBetaler { get; set; }

        public string naamVolledig { get; set; }

        public string plaats { get; set; }
    }

    public class Specificatie426
    {
        public decimal bedrag { get; set; }

        public string datum { get; set; }
        public string kostenplaats { get; set; }

        public string omschrijving { get; set; }

        public string opmerking { get; set; }
    }

    public class Specificatie450
    {
        public decimal Bedrag { get; set; }

        public string Rij { get; set; }
        public string Subcode { get; set; }

        public string Sumoms { get; set; }
    }

    public class Specificatie451
    {
        public decimal bedrag { get; set; }

        public string datum { get; set; }

        public string kostenplaats { get; set; }
        public string naamBetaler { get; set; }

        public string naamPlaats { get; set; }

        public string plaats { get; set; }
    }

    public class Specificatie452
    {
        public decimal bedrag { get; set; }
        public string omschrijving { get; set; }
    }

    public class Balansio
    {
        public decimal bedrag { get; set; }

        public int rij { get; set; }

        public string subcode { get; set; }
    }

    public class Balansio448
    {
        public decimal bedrag { get; set; }

        public int rij { get; set; }

        public string subcode { get; set; }
    }

    public class BalansioRest
    {
        public decimal bedrag { get; set; }

        public int rij { get; set; }
    }

    // Storno
    public class Balansio346
    {
        public decimal bedrag { get; set; }
        public int rij { get; set; }
        public int kolom { get; set; }
    }

    public class LibraryFactory
    {
        public List<Balansio> GetBalansio(int maand, int jaar)
        {
            var Eindbalans = new List<Balansio>();

            try
            {
                var sSQL = clsSQLData.sBilancio0(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Eindbalans.Add(
                            new Balansio
                            {
                                bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                                subcode = dt.Rows[intX][1].ToString(),
                                rij = Convert.ToInt16(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Eindbalans;
        }


        public List<Balansio346> GetBalansio346(int maand, int jaar)
        {
            var Eindbalans346 = new List<Balansio346>();
            try
            {
                var sSQL = clsSQLData.sBilancio346(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Eindbalans346.Add(
                            new Balansio346
                            {
                                bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                                rij = Convert.ToInt16(dt.Rows[intX][1].ToString()),
                                kolom = Convert.ToInt16(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Eindbalans346;
        }

        public List<Balansio448> GetBalansio448(int maand, int jaar)
        {
            var Eindbalans448 = new List<Balansio448>();
            try
            {
                var sSQL = clsSQLData.sBilancio448(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Eindbalans448.Add(
                            new Balansio448
                            {
                                bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                                subcode = dt.Rows[intX][1].ToString(),
                                rij = Convert.ToInt16(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Eindbalans448;
        }

        public List<BalansioRest> GetBalansRest(int maand, int jaar)
        {
            var EindbalansRest = new List<BalansioRest>();

            try
            {
                var sSQL = clsSQLData.sBilancioRest(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        EindbalansRest.Add(
                            new BalansioRest
                            {
                                bedrag = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                                rij = Convert.ToInt16(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }

            return EindbalansRest;
        }

        public List<Bank> GetBanken()
        {
            var Banken = new List<Bank>();

            try
            {
                var sSQL = clsSQLData.banken();
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Banken.Add(
                            new Bank
                            {
                                bankId = Convert.ToInt32(dt.Rows[intX][0].ToString()),
                                bankOms = dt.Rows[intX][1].ToString(),
                                nummer = dt.Rows[intX][2].ToString(),
                                soort = dt.Rows[intX][3].ToString(),
                                balans = Convert.ToInt16(dt.Rows[intX][4].ToString()),
                                rij = Convert.ToInt32(dt.Rows[intX][5].ToString()),
                                kolom = Convert.ToInt32(dt.Rows[intX][6].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Banken;
        }

        public List<BankSaldo> GetBankSaldi(string bankID, DateTime datum)
        {
            var BankSaldi = new List<BankSaldo>();
            var te = "";
            try
            {
                var sSQL = clsSQLData.sBankgegevens(datum, bankID);
                te = sSQL;
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        BankSaldi.Add(
                            new BankSaldo
                            {
                                bank = dt.Rows[intX][0].ToString(),
                                datum = dt.Rows[intX][1].ToString(),
                                omschrijving = dt.Rows[intX][2].ToString(),
                                nummer = dt.Rows[intX][3].ToString(),
                                rij = dt.Rows[intX][4].ToString(),
                                kolom = dt.Rows[intX][5].ToString(),
                                saldo = Convert.ToDecimal(dt.Rows[intX][6].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace + "\r\n" + te);
                throw;
            }


            return BankSaldi;
        }

        public List<BeginBalans> GetBeginBalans(int maand, int jaar)
        {
            var Beginbalans = new List<BeginBalans>();
            try
            {
                var sSQL = clsSQLData.sBeginbalans(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Beginbalans.Add(
                            new BeginBalans
                            {
                                idcode = dt.Rows[intX][0].ToString(),
                                datum = dt.Rows[intX][1].ToString(),
                                saldo = Convert.ToDecimal(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n\r\n" + e.Source + "\r\n\r\n" + e.StackTrace);
            }

            return Beginbalans;
        }

        public List<GrootBoek> GetGrootboek(int maand, int jaar)
        {
            var Grootboek = new List<GrootBoek>();
            try
            {
                var sSQL = clsSQLData.BedragPerGrootboek(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Grootboek.Add(
                            new GrootBoek
                            {
                                kostenplaats = dt.Rows[intX][0].ToString(),
                                saldo = Convert.ToDecimal(dt.Rows[intX][1].ToString()),
                                code = Convert.ToInt16(dt.Rows[intX][2].ToString()),
                                rij = Convert.ToInt16(dt.Rows[intX][3].ToString()),
                                kolom = Convert.ToInt16(dt.Rows[intX][4].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
            }


            return Grootboek;
        }

        public List<Specificatie318> GetSpecificatie318(int maand, int jaar)
        {
            var Spec318 = new List<Specificatie318>();

            try
            {
                var sSQL = clsSQLData.sSpec318(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec318.Add(
                            new Specificatie318
                            {
                                naam = dt.Rows[intX][0].ToString(),
                                aantal = Convert.ToInt16(dt.Rows[intX][1].ToString()),
                                omschrijving = dt.Rows[intX][2].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][3].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
            }


            return Spec318;
        }

        public List<Specificatie330> GetSpecificatie330(int maand, int jaar)
        {
            var Spec330 = new List<Specificatie330>();

            try
            {
                var sSQL = clsSQLData.sSpec330(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec330.Add(
                            new Specificatie330
                            {
                                saldo = Convert.ToDecimal(dt.Rows[intX][0].ToString()),
                                rij = Convert.ToInt16(dt.Rows[intX][1].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
            }


            return Spec330;
        }

        public List<Specificatie331> GetSpecificatie331(int maand, int jaar)
        {
            var Spec331 = new List<Specificatie331>();
            try
            {
                var sSQL = clsSQLData.sSpec331(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec331.Add(
                            new Specificatie331
                            {
                                donateur = dt.Rows[intX][0].ToString(),
                                omschrijving = dt.Rows[intX][1].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][2].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
            }


            return Spec331;
        }

        public List<Specificatie333> GetSpecificatie333(int maand, int jaar)
        {
            var Spec333 = new List<Specificatie333>();
            try
            {
                var sSQL = clsSQLData.sSpec333(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec333.Add(
                            new Specificatie333
                            {
                                omschrijving = dt.Rows[intX][0].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][1].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec333;
        }

        public List<Specificatie336> GetSpecificatie336(int maand, int jaar)
        {
            var Spec336 = new List<Specificatie336>();

            try
            {
                var sSQL = clsSQLData.sSpec336(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec336.Add(
                            new Specificatie336
                            {
                                naamBetaler = dt.Rows[intX][0].ToString(),
                                naamVolledig = dt.Rows[intX][1].ToString(),
                                plaats = dt.Rows[intX][2].ToString(),
                                datum = dt.Rows[intX][3].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][4].ToString()),
                                kostenplaats = dt.Rows[intX][5].ToString()
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec336;
        }

        public List<Specificatie426> GetSpecificatie426(int maand, int jaar)
        {
            var Spec426 = new List<Specificatie426>();
            try
            {
                var sSQL = clsSQLData.sSpec426(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec426.Add(
                            new Specificatie426
                            {
                                kostenplaats = dt.Rows[intX][0].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][1].ToString()),
                                omschrijving = dt.Rows[intX][2].ToString(),
                                datum = dt.Rows[intX][3].ToString(),
                                opmerking = dt.Rows[intX][4].ToString()
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec426;
        }

        public List<Specificatie450> GetSpecificatie450(int maand, int jaar)
        {
            var Spec450 = new List<Specificatie450>();
            try
            {
                var sSQL = clsSQLData.sSpec450(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec450.Add(
                            new Specificatie450
                            {
                                Subcode = dt.Rows[intX][0].ToString(),
                                Sumoms = dt.Rows[intX][1].ToString(),
                                Rij = dt.Rows[intX][2].ToString(),
                                Bedrag = Convert.ToDecimal(dt.Rows[intX][3].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec450;
        }

        public List<Specificatie451> GetSpecificatie451(int maand, int jaar)
        {
            var Spec451 = new List<Specificatie451>();
            try
            {
                var sSQL = clsSQLData.sSpec451(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec451.Add(
                            new Specificatie451
                            {
                                naamBetaler = dt.Rows[intX][0].ToString(),
                                naamPlaats = dt.Rows[intX][1].ToString(),
                                plaats = dt.Rows[intX][2].ToString(),
                                datum = dt.Rows[intX][3].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][4].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec451;
        }

        public List<Specificatie452> GetSpecificatie452(int maand, int jaar)
        {
            var Spec452 = new List<Specificatie452>();
            try
            {
                var sSQL = clsSQLData.sSpec452(maand, jaar);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                        Spec452.Add(
                            new Specificatie452
                            {
                                omschrijving = dt.Rows[intX][0].ToString(),
                                bedrag = Convert.ToDecimal(dt.Rows[intX][1].ToString())
                            });
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.Source + "\r\n" + e.StackTrace);
                throw;
            }


            return Spec452;
        }
    }
}