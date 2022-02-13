using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataExtra
    {
        public static string haalSaldi(Int32 maand, Int32 jaar)
        {
            var sSQL = "SELECT IDCode, Saldo ";
            sSQL += "FROM tbl_Saldo ";
            sSQL += "WHERE ((Month(datum) = " + maand + ") AND (Year(datum) = " + jaar + ")) ";
            sSQL += "ORDER BY tbl_Saldo.IDCode; ";
            return sSQL;
        }

        public static string verwijderSaldi(DateTime datum)
        {
            var sSQL = "DELETE FROM tbl_Saldo WHERE YEAR(Datum) = " + datum.Year;
            sSQL += " AND MONTH(datum) = " + datum.Month + " AND DAY(datum) = " + datum.Day;
            return sSQL;
        }

        public static string opslaanSaldi(DateTime datum, string code, string waarde)
        {
            var sSQL = "INSERT INTO tbl_Saldo (IDCode, datum, saldo) VALUES ( " + code + ", '";
            sSQL += datum.Year + "-" + datum.Month + "-" + datum.Day + "', " + waarde + ");";
            return sSQL;
        }

        public static string opslaanMail(string naam, string mail, string subject, string tekst)
        {
            var sSQL =
                "INSERT INTO brieven (naam_id, EmailTo, subject, tekst, datum) ";
            sSQL += "VALUES ";
            sSQL += "( '" + naam + "', '" + mail + "', '" + subject + "', '" + tekst + "', '" + DateTime.Now + "');";

            return sSQL;
        }

        public static string updateBedankBedrag(string bedrag, string naam)
        {
            var sSQL = "UPDATE tbl_Adressen SET donatieBedankt = ";
            sSQL += "donatieBedankt + " + bedrag.Replace(",", ".");
            sSQL += " WHERE adresnummer = " + naam;
            return sSQL;
        }
    }
}
