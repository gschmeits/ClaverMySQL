using System;

namespace Domain
{
    public static class clsSQLData
    {
        public static string banken()
        {
            var sSQL = "SELECT BankID, BankOms, Nummer, Soort, balans, rij, kolom FROM tbl_Banken";
            return sSQL;
        }

        public static string sBankgegevens(DateTime datum, string bank)
        {
            var sSQL = "SELECT mb.Bank, mb.Datum, b.BankOms, b.nummer, b.rij, b.kolom, IFNULL(mb.Eindsaldo,0)  ";
            sSQL += "FROM tbl_MutatiesBanken as mb, tbl_Banken as b ";
            sSQL += "WHERE bank = '" + bank + "' AND Datum <= '";
            sSQL += datum.Year + "-" + datum.Month + "-" + datum.Day + "' ";
            sSQL += "AND b.BankID = mb.Bank ";
            sSQL += "AND b.actief = 1 ";
            sSQL += "ORDER BY datum DESC LIMIT 1";
            return sSQL;
        }

        public static string sBeginbalans(int iMaand, int iJaar)
        {
            var sSQL = "SELECT idcode, datum, saldo FROM tbl_Saldo ";
            sSQL += "WHERE (((Month(datum)) = " + iMaand + ") AND (Year(datum)) = " + iJaar + ") ";
            sSQL += "ORDER BY tbl_Saldo.idcode; ";
            return sSQL;
        }

        public static string BedragPerGrootboek(int iMaand, int iJaar)
        {
            var sSQL = "SELECT nummerkostenplaats, ABS(Sum(tbl_Mutaties.Bedrag)) AS dblbedrag, ";
            sSQL += "tbl_CodeTabel.Code, tbl_CodeTabel.rij, tbl_CodeTabel.kolom ";
            sSQL += "FROM tbl_Mutaties RIGHT JOIN tbl_CodeTabel ON tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.IDCode ";
            sSQL += "WHERE ((Month(boekdatum)= " + iMaand + " And Year(boekdatum) = " + iJaar + "))  ";
            sSQL += "AND tbl_CodeTabel.rij <> 0 AND tbl_CodeTabel.kolom <> 0 ";
            sSQL += "GROUP BY nummerkostenplaats, tbl_CodeTabel.IDCode, Code, tbl_CodeTabel.rij, tbl_CodeTabel.kolom ";
            return sSQL;
        }

        public static string sSpec318(int iMaand, int iJaar)
        {
            var sSQL = "SELECT CONCAT(a.titel_en_voornaam , ' ',  a.achternaam) as adresnaam, " ;
            sSQL += "t.Aantal, ct.CodeOmschrVreemd, t.Bedrag ";
            sSQL += "FROM ";
            sSQL += "tbl_Mutaties t ";
            sSQL += "LEFT JOIN tbl_Adressen AS a ON t.NaamBetaler = a.adresnummer ";
            sSQL += "LEFT JOIN  tbl_CodeTabel AS ct ON t.NummerKostenplaats = ct.IDCode ";
            sSQL += "WHERE month(t.boekdatum) = " + iMaand;
            sSQL += " AND year(t.Boekdatum) = " + iJaar;
            sSQL += " AND t.Bedrag <> 0 AND t.NummerKostenplaats = '318' ";
            
            return sSQL;
        }

        public static string sSpec330(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Sum(tbl_Mutaties.Bedrag) AS Totaal, tbl_SubCode.Rij ";
            sSQL += "FROM tbl_Mutaties INNER JOIN tbl_SubCode ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCode ";
            sSQL += "WHERE Month(boekdatum) = " + iMaand + " And Year(boekdatum) = " + iJaar + " AND Bedrag <> 0 ";
            sSQL += " GROUP BY tbl_SubCode.Rij,  tbl_Mutaties.NummerKostenplaats ";
            sSQL += "HAVING (((tbl_Mutaties.NummerKostenplaats)='330'))";
            return sSQL;
        }

        public static string sSpec331(int iMaand, int iJaar)
        {
            var sSQL =
                "SELECT CONCAT(titel_en_voornaam, ' ', achternaam , ' ' , plaats) AS Donateur, omschrijving, tbl_Mutaties.Bedrag ";
            sSQL += "FROM tbl_Mutaties LEFT JOIN  tbl_Adressen ON tbl_Mutaties.NaamBetaler =  tbl_Adressen.adresnummer ";
            sSQL += "WHERE Month(boekdatum) = " + iMaand + " And Year(Boekdatum) = " + iJaar + " AND Bedrag <> 0 AND ";
            sSQL += "(((tbl_Mutaties.NummerKostenplaats) = '331'))";
            return sSQL;
        }

        public static string sSpec333(int iMaand, int iJaar)
        {
            var sSQL = "SELECT s.sumoms, sum(t.bedrag) FROM tbl_SubCode s, tbl_Mutaties t ";
            sSQL += "where s.Rekeningnummer = '333' ";
            sSQL += "AND t.Subcode = s.ID_SubCode ";
            sSQL += "AND month(t.boekdatum) = " + iMaand;
            sSQL += " AND year(t.Boekdatum) = " + iJaar;
            sSQL += " AND t.Bedrag <> 0 "; 
            sSQL += " GROUP BY s.ID_SubCode, s.SumOms ";
            return sSQL;
        }

        public static string sSpec336(int iMaand, int iJaar)
        {
            var sSQL =
                "SELECT tbl_Mutaties.NaamBetaler, CONCAT(titel_en_voornaam, ' ',achternaam) as naam, tbl_Adressen.plaats, ";
            sSQL += "tbl_Mutaties.Boekdatum, tbl_Mutaties.Bedrag, tbl_Mutaties.NummerKostenplaats ";
            sSQL += "FROM tbl_Mutaties LEFT JOIN tbl_Adressen ON tbl_Mutaties.NaamBetaler = tbl_Adressen.adresnummer ";
            sSQL += "WHERE Month(boekdatum) = " + iMaand + " And Year(boekdatum)= " + iJaar + " AND Bedrag <> 0 "
                    + "  AND  tbl_Mutaties.NummerKostenplaats='336' ";
            return sSQL;
        }

        public static string sSpec426(int iMaand, int iJaar)
        {
            var sSQL =
                "SELECT tbl_Mutaties.NummerKostenplaats, tbl_Mutaties.Bedrag * -1, tbl_Mutaties.Omschrijving, tbl_Mutaties.Boekdatum, tbl_Mutaties.opmerking ";
            sSQL += "FROM tbl_Mutaties ";
            sSQL += "WHERE Month(Boekdatum) = " + iMaand + " And Year(Boekdatum) = " + iJaar + " AND Bedrag <> 0 ";
            sSQL += " AND (((tbl_Mutaties.NummerKostenplaats) = '426'))";
            return sSQL;
        }

        public static string sSpec451(int iMaand, int iJaar)
        {
            var sSQL =
                "SELECT tbl_Mutaties.NaamBetaler, CONCAT(titel_en_voornaam,' ',achternaam,  ', ',tbl_Adressen.plaats) ";
            sSQL += "AS betNaam, tbl_Adressen.plaats, ";
            sSQL +=
                "tbl_Mutaties.Boekdatum, tbl_Mutaties.Bedrag * -1, tbl_Mutaties.NummerKostenplaats, tbl_Mutaties.NaamBetaler ";
            sSQL += "FROM tbl_Mutaties LEFT JOIN tbl_Adressen ON tbl_Mutaties.NaamBetaler = tbl_Adressen.adresnummer ";
            sSQL += "WHERE ((Month(boekdatum)= " + iMaand + " And Year(boekdatum)=" + iJaar + " )  AND Bedrag <> 0 ";
            sSQL += "AND ((tbl_Mutaties.NummerKostenplaats)='451'))";
            return sSQL;
        }

        public static string sSpec450(int iMaand, int iJaar)
        {
            var query = "Select subcode, sumoms, IFNULL(rij, 202) AS rij,  abs(sum(bedrag)) As bedrag ";
            query += "from tbl_Mutaties LEFT JOIN tbl_SubCode  ";
            query += "ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCode ";
            query += "where month(boekdatum) = " + iMaand + " and year(boekdatum) = " + iJaar + " ";
            query += "AND NummerKostenplaats = 450  ";
            query += "group by tbl_Mutaties.Subcode, SumOms, rij, NummerKostenplaats order by tbl_Mutaties.Subcode  ";

            return query;
        }

        public static string sSpec452(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Omschrijving, bedrag ";
            sSQL += "FROM tbl_Mutaties ";
            sSQL += "WHERE ((Month(boekdatum)= " + iMaand + " And Year(boekdatum)=" + iJaar 
                    + " ) AND ((tbl_Mutaties.NummerKostenplaats)='452'))  AND Bedrag <> 0 ";
            return sSQL;
        }

        public static string sBilancio0(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Sum((tbl_Mutaties.Bedrag * -1)) AS Totaal, tbl_Mutaties.Subcode, tbl_SubCode.Rij ";
            sSQL += "FROM tbl_Mutaties INNER JOIN tbl_SubCode ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCode ";
            sSQL += "WHERE Month(DatumOverzicht) = " + iMaand + " And Year(DatumOverzicht) = " + iJaar;            
            sSQL += " AND Bedrag <> 0 ";
            sSQL += " GROUP BY tbl_Mutaties.Subcode, tbl_Mutaties.NummerKostenplaats, tbl_SubCode.Rij ";
            sSQL += "HAVING LEFT(tbl_Mutaties.NummerKostenplaats,1)='4'";
            return sSQL;
        }

        public static string sBilancio448(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Sum((tbl_Mutaties.Bedrag * -1)) AS Totaal, tbl_Mutaties.Subcode, tbl_SubCode.Rij ";
            sSQL += "FROM tbl_Mutaties INNER JOIN tbl_SubCode ON tbl_Mutaties.Subcode = tbl_SubCode.ID_SubCode ";
            sSQL += "WHERE Month(DatumOverzicht) = " + iMaand + " And Year(DatumOverzicht) = " + iJaar;
            sSQL += " AND Bedrag <> 0 ";
            sSQL += " GROUP BY tbl_Mutaties.Subcode, tbl_Mutaties.NummerKostenplaats, tbl_SubCode.Rij ";
            sSQL += "HAVING tbl_Mutaties.NummerKostenplaats='448'";
            return sSQL;
        }

        public static string sBilancio346(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Sum((tbl_Mutaties.Bedrag)) AS Totaal, ";
            sSQL += "tbl_CodeTabel.Rij, tbl_CodeTabel.Kolom FROM tbl_Mutaties ";
            sSQL += "INNER JOIN tbl_CodeTabel ON tbl_Mutaties.NummerKostenplaats = tbl_CodeTabel.IDCODE";
            sSQL += " WHERE Month(DatumOverzicht) = " + iMaand + " And Year(DatumOverzicht) = " + iJaar;
            sSQL += " AND Bedrag <> 0";
            sSQL += " GROUP BY tbl_Mutaties.NummerKostenplaats, tbl_CodeTabel.Rij, tbl_CodeTabel.Kolom";
            sSQL += " HAVING tbl_Mutaties.NummerKostenplaats = '346'";
            return sSQL;
        }

        public static string sBilancioRest(int iMaand, int iJaar)
        {
            var sSQL = "SELECT Sum((tbl_Mutaties.Bedrag * -1)) AS Totaal, tbl_Mutaties.Subcode, tbl_SubCode.Rij ";
            sSQL += "FROM tbl_Mutaties INNER JOIN tbl_SubCode ON tbl_Mutaties.NummerKostenplaats = tbl_SubCode.Rekeningnummer ";
            sSQL += "WHERE Month(DatumOverzicht) = " + iMaand + " And Year(DatumOverzicht) = " + iJaar;
            sSQL += " AND Bedrag <> 0 ";
            sSQL += "GROUP BY tbl_Mutaties.Subcode, tbl_Mutaties.NummerKostenplaats, tbl_SubCode.Rij  ";
            sSQL += "HAVING tbl_Mutaties.Subcode = 0;";
            return sSQL;
        }

        public static string eindbalansDelete(int maand, int jaar)
        {
            var sSQL = "DELETE FROM tbl_Saldo WHERE year(datum) = " + jaar + " AND month(datum) = " + maand;
            return sSQL;
        }

        public static string eindbalansVoegtoe(int idcode, DateTime datum, string bedrag)
        {
            var sSQL = "INSERT INTO tbl_Saldo (idcode, datum, saldo) VALUES (";
            sSQL += idcode + ", '" + datum.Year + "-" + datum.Month + "-" + datum.Day;
            sSQL += "',  '" + bedrag + "')";
            return sSQL;
        }
    }
}