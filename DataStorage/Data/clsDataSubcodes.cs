using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataSubcodes
    {
        public static string cmbRekeningHaalSQL()
        {
            var sSQL = "SELECT DISTINCT rekeningnummer FROM tbl_SubCode;";
            return sSQL;
        }

        public static string cmbSubcodeHaalSQL()
        {
            var sSQL = "SELECT rekeningnummer, sumoms, rij, ID_Subcode FROM tbl_SubCode;";
            return sSQL;
        }

        public static string cmbSubcodeSQL(string sKolom, string sWaarde)
        {
            var sSQL = "SELECT rekeningnummer, sumoms, rij, ID_Subcode FROM tbl_SubCode " +
                       "WHERE " + sKolom + " = '" + sWaarde + "';";
            return sSQL;
        }

        public static string cmbSubcodeSQL(string sWaarde)
        {
            var sSQL = "SELECT rekeningnummer, sumoms, rij, ID_Subcode FROM tbl_SubCode " +
                       "WHERE ID_SubCode = " + sWaarde + ";";
            return sSQL;
        }

        public static string SubcodeInsert(string sGrootboek, string sSubcode, string sRij)
        {
            var sSQL = "INSERT INTO tbl_SubCode (rekeningnummer, sumoms, rij) " +
                       "VALUES( " + sGrootboek.Replace("'", "''") +
                       ", '" + sSubcode.Replace("'", "''") + "', " + sRij + ");";
            return sSQL;
        }

        public static string SubcodeUpdate(string sGrootboek, string sSubcode, string sRij, string sID)
        {
            var sSQL = "UPDATE tbl_SubCode SET rekeningnummer = " + 
                       sGrootboek.Replace("'", "''") + ", " +
                       "sumoms = '" + sSubcode.Replace("'", "''") + "', " +
                       "rij = " + sRij + " WHERE ID_Subcode = " + sID;

            return sSQL;
        }
    }
}
