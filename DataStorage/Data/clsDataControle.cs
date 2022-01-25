using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataControle
    {
        public static string contVorigeMaand(int maand, int jaar)
        {
            var sSQL = string.Empty;
            sSQL += "SELECT t.idcode, c.Code, t.datum, t.saldo ";
            sSQL += "FROM tbl_Saldo as t INNER JOIN tbl_CodeSoort as c ON t.IDCode = c.IDCode ";
            sSQL += "WHERE (((Month(datum)) = " + maand + ") AND (Year(datum)) = " + jaar + ") ";
            return sSQL;
        }

        public static string aantalBanken()
        {
            var sSQL = "SELECT COUNT(*) FROM tbl_Banken";
            return sSQL;
        }
        public static void VulBanksaldiBeginsaldo(int bank, DateTime datum)
        {
            var sSQL = "SELECT tbl_MutatiesBanken.Eindsaldo ";
            sSQL += "FROM tbl_MutatiesBanken INNER JOIN tbl_Banken ";
            sSQL += "ON tbl_MutatiesBanken.Bank = tbl_Banken.BankID ";
            sSQL += "WHERE(((tbl_MutatiesBanken.Bank) = '" + bank + "') ";
            sSQL += "AND((tbl_MutatiesBanken.Datum) IN ";
            sSQL += "(SELECT Max(Datum) ";
            sSQL += "FROM tbl_MutatiesBanken ";
            sSQL += "WHERE Datum <= '" + datum.Year + "-" + datum.Month + "-" + datum.Day + "' ";
            sSQL += "AND(bank) = '" + bank + "'))); ";

            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                sSQL = "UPDATE tbl_Banken SET tbl_Banken.Beginsaldo = "; 
                sSQL += dt.Rows[0][0].ToString().Replace(",", ".");
                sSQL += " WHERE BankID = " + bank + ";";
                GenericDataRead.INUPDEL(sSQL);
            }
        }
        public static void VulBanksaldiEindsaldo(int bank, DateTime datum)
        {
            var sSQL = "SELECT tbl_MutatiesBanken.Eindsaldo FROM ";
            sSQL += "tbl_MutatiesBanken INNER JOIN tbl_Banken ";
            sSQL += "ON tbl_MutatiesBanken.Bank = tbl_Banken.BankID ";
            sSQL += "WHERE (((tbl_MutatiesBanken.Bank)='" + bank + "') ";
            sSQL += "AND ((tbl_MutatiesBanken.Datum) IN ";
            sSQL += "(SELECT Max(Datum) ";
            sSQL += "FROM tbl_MutatiesBanken ";
            sSQL += "WHERE Datum <= '" + datum.Year + "-" + datum.Month + "-" + datum.Day + "' ";
            sSQL += "AND (bank) = '" + bank + "')));";

            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                var sSQL1 = "UPDATE tbl_Banken SET tbl_Banken.Eindsaldo = ";
                sSQL1 += dt.Rows[0][0].ToString().Replace(",", ".");
                sSQL1 += " WHERE BankID = " + bank + ";";
                GenericDataRead.INUPDEL(sSQL1);
            }
        }

        public static string saldiBanken()
        {
            var sSQL = "SELECT BankID, BankOms, Nummer, IFNULL(BeginSaldo, 0), ";
            sSQL += "IFNULL(EindSaldo, 0) FROM tbl_Banken WHERE Actief = 1 ";
            sSQL += "ORDER BY BankID";
            return sSQL;
        }

        public static string saldiSoort(DateTime datum, int soort)
        {
            var sSQL = "SELECT Saldo FROM tbl_Saldo ";
            sSQL += "WHERE Month(Datum) = " + datum.Month;
            sSQL += " AND Year(Datum) = " + datum.Year;
            sSQL += " AND IDCode = ";
            sSQL += soort + ";";
            return sSQL;
        }

        public static string saldosBanken(int bank, DateTime datum)
        {
            var sSQL = "SELECT tbl_MutatiesBanken.Bank, tbl_Banken.BankOms, ";
            sSQL += "tbl_Banken.Nummer, tbl_MutatiesBanken.Datum, tbl_MutatiesBanken.Eindsaldo ";
            sSQL += "FROM tbl_MutatiesBanken INNER JOIN tbl_Banken ";
            sSQL += "ON tbl_MutatiesBanken.Bank = tbl_Banken.BankID ";
            sSQL += "WHERE (((tbl_MutatiesBanken.Bank)='" + bank;
            sSQL += "') AND ((tbl_MutatiesBanken].Datum) IN (SELECT Max(Datum) ";
            sSQL += "FROM tbl_Mutaties Banken ";
            sSQL += "WHERE Datum <= '" + datum.Year + "-" + datum.Month + "-" + datum.Day + "' ";
            sSQL += "AND (bank) = '" + bank + "'))) ";
            return sSQL;
        }

        public static string saldoCodePerBank(DateTime datum)
        {
            var sSQL = "";
            sSQL += "SELECT tbl_CodeTabel.IDCode, Sum(tbl_Mutaties.Bedrag) AS Totaal, ";
            sSQL += "tbl_Mutaties.Bank, tbl_Banken.BankOms, tbl_Banken.Nummer, ";
            sSQL += "LEFT(tbl_CodeTabel.CodeOmschrijving, 34) ";
            sSQL += "FROM(tbl_Mutaties INNER JOIN tbl_Banken ON ";
            sSQL += "tbl_Mutaties.Bank = tbl_Banken.BankID) LEFT JOIN ";
            sSQL += "tbl_CodeTabel ON tbl_Mutaties.NummerKostenplaats ";
            sSQL += "= tbl_CodeTabel.IDCode GROUP BY tbl_CodeTabel.IDCode, ";
            sSQL += "tbl_Mutaties.Bank, tbl_Banken.BankOms, ";
            sSQL += "tbl_Banken.Nummer, LEFT(tbl_CodeTabel.CodeOmschrijving, 34), ";
            sSQL += "tbl_Mutaties.NummerKostenplaats, tbl_Mutaties.Bank, MONTH(Boekdatum), ";
            sSQL += "YEAR(tbl_Mutaties.Boekdatum) AND(((MONTH(tbl_Mutaties.Boekdatum)) = " + datum.Month + " ) ";
            sSQL += "AND((Year(Boekdatum)) = " + datum.Year + " )) ";
            sSQL += "ORDER BY tbl_Mutaties.Bank, tbl_Mutaties.NummerKostenplaats;";
            return sSQL;
        }

        public static string saldoPerGrootboek(DateTime datum)
        {
            var sSQL = "SELECT tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats, ";
            sSQL += "tbl_Mutaties.Boekdatum, tbl_CodeTabel.CodeOmschrijving, ";
            sSQL += "IF(LENGTH(tbl_Mutaties.NaamVermelding) < 3, ";
            sSQL += "LEFT(tbl_Mutaties.Omschrijving, 32), ";
            sSQL += "LEFT(tbl_Mutaties.NaamVermelding, 32)) AS omschrijving, ";
            sSQL += "tbl_SubCode.SumOms, tbl_Mutaties.Bedrag AS Totaal, tbl_Mutaties.Bank, ";
            sSQL += "tbl_Banken.BankOms, tbl_Banken.Nummer ";
            sSQL += "FROM(((tbl_Mutaties INNER JOIN tbl_CodeTabel ON ";
            sSQL += "tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.IDCode) ";
            sSQL += "LEFT JOIN tbl_CodeSoort ON tbl_CodeTabel.Code = tbl_CodeSoort.IDCode) ";
            sSQL += "INNER JOIN tbl_Banken ON tbl_Mutaties.Bank = tbl_Banken.BankID) ";
            sSQL += "LEFT JOIN tbl_SubCode ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCODE ";
            sSQL += "WHERE(((MONTH(Boekdatum)) = " + datum.Month + ") ";
            sSQL += "AND((YEAR(Boekdatum)) = " + datum.Year + " )) ";
            sSQL += "ORDER BY tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats, ";
            sSQL += "tbl_Mutaties.Boekdatum, tbl_Mutaties.Bank;";
            return sSQL;
        }

        public static string detailsPerGrootBoek(DateTime beginDatum,
            DateTime eindDatum, string beginGrootboek, string eindGrootboek)
        {
            var sSQL = "";
            sSQL += "SELECT tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats, "; 
            sSQL += "tbl_Mutaties.Boekdatum, tbl_CodeTabel.CodeOmschrijving, ";
            sSQL += "IF(LENGTH(tbl_Mutaties.NaamVermelding) < 3, LEFT(tbl_Mutaties.Omschrijving, 32), ";
            sSQL += "LEFT(tbl_Mutaties.NaamVermelding, 32)) AS omschrijving, ";
            sSQL += "tbl_SubCode.SumOms, tbl_Mutaties.Bedrag AS Totaal, ";
            sSQL += "tbl_Mutaties.Bank, tbl_Banken.BankOms, tbl_Banken.Nummer ";
            sSQL += "FROM(((tbl_Mutaties INNER JOIN tbl_CodeTabel ";
            sSQL += "ON tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.IDCode) ";
            sSQL += "LEFT JOIN tbl_CodeSoort ON tbl_CodeTabel.Code = tbl_CodeSoort.IDCode) "; 
            sSQL += "INNER JOIN tbl_Banken ON tbl_Mutaties.Bank = tbl_Banken.BankID) ";
            sSQL += "LEFT JOIN tbl_SubCode ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCODE ";
            sSQL += "WHERE MONTH(Boekdatum) >= " + beginDatum.Month + " ";
            sSQL += "AND MONTH(Boekdatum) <= " + eindDatum.Month + " ";
            sSQL += "AND YEAR(Boekdatum) = " + beginDatum.Year + " ";
            sSQL += "AND tbl_Mutaties.NummerKostenplaats >= " + beginGrootboek + " ";
            sSQL += "AND tbl_Mutaties.NummerKostenplaats <= " + eindGrootboek + " ";
            sSQL += "ORDER BY tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats,  ";
            sSQL += "tbl_Mutaties.Boekdatum, tbl_Mutaties.Bank; ";
            return sSQL;
        }

        public static string saldoPerKostenplaatsGrootboek(DateTime datum)
        {
            var sSQL = "SELECT tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats, ";
            sSQL += "tbl_CodeTabel.CodeOmschrijving, SUM(tbl_Mutaties.Bedrag) ";
            sSQL += "AS Totaal FROM tbl_Mutaties INNER JOIN tbl_CodeTabel ";
            sSQL += "ON tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.idCode ";
            sSQL += "LEFT JOIN tbl_CodeSoort ON ";
            sSQL += "tbl_CodeTabel.Code = tbl_CodeSoort.idCode ";
            sSQL += "LEFT JOIN tbl_SubCode ON ";
            sSQL += "tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCode ";
            sSQL += "WHERE (((MONTH(Boekdatum)) = " + datum.Month + ") ";
            sSQL += "AND ((YEAR(Boekdatum)) = " + datum.Year + ")) ";
            sSQL += "GROUP BY tbl_CodeSoort.Code, ";
            sSQL += "tbl_Mutaties.NummerKostenplaats, tbl_CodeTabel.CodeOmschrijving ";
            sSQL += "ORDER BY tbl_CodeSoort.Code, tbl_Mutaties.NummerKostenplaats; ";
            return sSQL;
        }

        public static string controleSubcodes(string maand, string jaar)
        {
            var sSQL = "SELECT ID, Boekdatum, Bank, tbl_Banken.BankOms, tbl_Banken.Nummer, ";
            sSQL += "NummerKostenplaats, Bedrag, Opmerking, Subcode, Omschrijving, naambetaler ";
            sSQL += "FROM tbl_Mutaties, tbl_Banken ";
            sSQL += "WHERE MONTH(Boekdatum) = 12 ";
            sSQL += "AND YEAR(Boekdatum) = 2021 AND ";
            sSQL += "(NummerKostenplaats = 330 ";
            sSQL += "OR NummerKostenplaats = 333 ";
            sSQL += "OR NummerKostenplaats = 448 ";
            sSQL += "OR NummerKostenplaats = 449 ";
            sSQL += "OR NummerKostenplaats = 450 ";
            sSQL += "OR NummerKostenplaats = 452) ";
            sSQL += "AND tbl_Mutaties.Bank = tbl_Banken.BankID AND SubCode = '0' ORDER BY SubCode";
            return sSQL;
        }

        public static string controleBedragen(string maand, string jaar)
        {
            var sSQL = string.Empty;
            sSQL += "SELECT ID, Boekdatum, Bank, tbl_Banken.BankOms, ";
            sSQL += "tbl_Banken.Nummer, NummerKostenplaats, Bedrag , ";
            sSQL += "Opmerking, tbl_CodeTabel.CodeOmschrijving, ";
            sSQL += "Omschrijving FROM tbl_Mutaties, ";
            sSQL += "tbl_Banken, tbl_CodeTabel WHERE MONTH(Boekdatum) = " + maand;
            sSQL += " AND YEAR(Boekdatum) = " + jaar;
            sSQL += " AND tbl_Mutaties.Bank = tbl_Banken.BankID AND ";
            sSQL += "tbl_CodeTabel.IDCode = tbl_Mutaties.NummerKostenplaats ";
            sSQL += "AND bedrag = 0; ";
            return sSQL;
        }
    }
}
