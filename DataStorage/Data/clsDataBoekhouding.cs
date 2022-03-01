using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStorage.Data
{
    public static class clsDataBoekhouding
    {
        public static string cmbJaartal()
        {
            var sSQL = "SELECT jaartal FROM tbl_Jaartal ORDER BY jaartal";
            return sSQL;
        }

        public static string cmbMaand()
        {
            var sSQL = "SELECT maand FROM tbl_Maanden ORDER BY Maand";
            return sSQL;
        }

        public static string cmbBanken()
        {
            var sSQL = "SELECT BankID, BankOms, Nummer FROM tbl_Banken";
            return sSQL;
        }

        public static string HaalGegevensBankPerMaand(string sBank, short sJaar, short sMaand)
        {
            var sSQL =
                "SELECT Datum FROM tbl_MutatiesBanken WHERE bank = '";
            sSQL += sBank + "' AND YEAR(Datum) = " + sJaar +
                     " AND MONTH(Datum) = " + sMaand;

            return sSQL;
        }

        public static string cmbDatum(string sJaar, string sMaand, string sBank)
        {
            var sSQL = "SELECT CONCAT(Day(Datum),'-', Month(Datum), '-', Year(Datum) ";
            sSQL += "AS Datum1, tbl_MutatiesBanken.Volgnummer, ";
            sSQL += "tbl_mutatiesBanken.Beginsaldo, tbl_mutatiesBanken.Eindsaldo, tbl_mutatiesBanken.TeBoeken ";
            sSQL += "From tbl_mutatiesBanken ";
            sSQL += "Where (((Year(Datum)) = " + sJaar + ") And ((Month(Datum)) = " + sMaand;
            sSQL += ") And ((tbl_mutatiesBanken.Bank) = '" + sBank + "')) ";
            sSQL += "ORDER BY tbl_mutatiesBanken.Datum;";
            return sSQL;
        }

        public static string cmbHaalGegevensDag(string sJaar, string sMaand, string sDag, string sBank)
        {
            var sSQL = "SELECT CONCAT(Day(Datum) ,'-', Month(datum) ,'-' + Year(datum)) AS Datum1, " +
                       "Volgnummer, Beginsaldo, Eindsaldo, TeBoeken ";
            sSQL += "FROM tbl_MutatiesBanken ";
            sSQL += "WHERE Year(Datum) = " + sJaar;
            sSQL += " AND Month(Datum) = " + sMaand;
            sSQL += " AND Day(Datum) = " + sDag;
            sSQL += " AND Bank = '" + sBank + "' ";
            sSQL += " ORDER BY tbl_MutatiesBanken.Datum; ";
            return sSQL;
        }

        public static string cmbHaalGegevensDagDetails(string sJaar, string sMaand, string sDag, string sBank)
        {
            var sSQL = "";
            sSQL += "SELECT ID, IF(a.NaamBetaler = 0 , '', a.NaamBetaler) AS NaamBetaler, ";
            sSQL += "IFNULL(CONCAT(b.titel_en_voornaam, ' ', b.Achternaam), ''), ";
            sSQL += "a.Boekdatum, a.Blad, a.Bedrag, ";
            sSQL += "a.Opmerking, a.NummerKostenplaats as Code, a.NaamVermelding, a.OmsSub, ";
            sSQL += "a.Omschrijving , a.Aantal, a.Dank, a.subcode ";
            sSQL += "FROM tbl_Mutaties AS a LEFT JOIN tbl_Adressen AS b ON a.NaamBetaler = b.adresnummer ";
            sSQL += "WHERE Year(Boekdatum) = " + sJaar;
            sSQL += " AND Month(Boekdatum) = " + sMaand;
            sSQL += " AND Day(Boekdatum) = " + sDag;
            sSQL += " AND Bank = " + sBank;
            sSQL += " ORDER BY a.ID DESC;";
            return sSQL;
        }

        public static string sGeboekt(string sJaar, string sMaand, string sDag, string sBank)
        {
            var sSQL = "SELECT sum(tbl_Mutaties.Bedrag) ";
            sSQL += "FROM tbl_Mutaties ";
            sSQL += "WHERE Year(DatumOverzicht) = " + sJaar;
            sSQL += " AND Month(DatumOverzicht) = " + sMaand;
            sSQL += " AND Day(DatumOverzicht) = " + sDag;
            sSQL += " AND Bank = " + sBank;
            return sSQL;
        }

        public static string HaalVolgnummer(string sJaar, string sMaand, string sDag, string sBank)
        {
            var sSQL = "SELECT Volgnummer, Eindsaldo ";
            sSQL += "FROM tbl_MutatiesBanken ";
            sSQL += "WHERE Datum < '" + sJaar + "-";
            sSQL += sMaand + "-" + sDag + " 00:00:00.000'";
            sSQL += " AND Bank = " + sBank;
            sSQL += " ORDER BY tbl_MutatiesBanken.Datum DESC; ";
            return sSQL;
        }

        public static string HaalCodeGegevens()
        {
            var sSQL = "SELECT IDCode, " +
                       "CONCAT(IDCode,' ',CodeOmschrijving) as sCode " +
                       "FROM tbl_CodeTabel WHERE ";
            sSQL += "CodeOmschrijving IS NOT NULL ORDER BY IDCODE";
            return sSQL;
        }

        public static string HaalCodeIndividueel(string sCode)
        {
            var sSQL = "SELECT * FROM tbl_CodeTabel WHERE IDCode = '" + sCode + "'";
            return sSQL;
        }

        public static string HaalSubCode(string sCode)
        {
            var sSQL = "SELECT ID_SubCode, SumOms FROM tbl_SubCode WHERE Rekeningnummer = '" + sCode + "';";
            return sSQL;
        }

        public static string HaalAdresNummers()
        {
            var sSQL = "SELECT adresnummer FROM tbl_Adressen";
            return sSQL;
        }

        public static string HaalAdresNamen()
        {
            var sSQL = "SELECT CONCAT(achternaam,', ',titel_en_voornaam) AS Naam, adresnummer " +
                       "FROM tbl_Adressen ORDER BY achternaam;";
            return sSQL;
        }

        public static string HaalAdresBanken()
        {
            var sSQL = "SELECT Bankrekening as bank, adresnummer FROM tbl_Adressen " +
                       "WHERE Bankrekening <> '' AND Bankrekening <> '0' AND " +
                       "(LEFT(Bankrekening, 1)) REGEXP '^[0-9]+$' ORDER BY Bankrekening;";
            return sSQL;
        }

        public static string HaalAdresGiros()
        {
            var sSQL = "SELECT Girorekening as giro, adresnummer FROM tbl_Adressen " +
                       "WHERE (((Girorekening) <> '' ";
            sSQL += "AND (Girorekening) <> 'x')) AND Girorekening REGEXP '^[0-9]+$' " +
                    "ORDER BY Girorekening;";
            return sSQL;
        }

        public static string HaalAdresIBAN()
        {
            var sSQL = "SELECT IBAN as iban, adresnummer FROM tbl_Adressen " +
                       "WHERE (((tbl_Adressen.IBAN) <> '' ";
            sSQL += "And (IBAN) <> 'x' )) AND LENGTH(IBAN) <> 0 ORDER BY tbl_Adressen.IBAN;";
            return sSQL;
        }

        public static string HaalAdresPostcode()
        {
            var sSQL = "SELECT CONCAT(postcode,', ',huisnummer) as postcode1, adresnummer " +
                       "FROM tbl_Adressen " +
                       "WHERE postcode <> '' and postcode <> '0000' ";
            sSQL += "AND postcode IS NOT NULL AND huisnummer IS NOT NULL ORDER BY postcode, huisnummer";
            return sSQL;
        }

        public static string HaalAdresDetails(string sAdresnummer)
        {
            var sSQL =
                "SELECT CONCAT(titel_en_voornaam, ' ',achternaam) AS Naam, ";
            sSQL += "CONCAT(straat, ' ', huisnummer) AS Adres, ";
            sSQL += "CONCAT(postcode, ' ', plaats) AS Plaats, ";
            sSQL += "Dank ";
            sSQL += "FROM tbl_Adressen WHERE adresnummer = " + sAdresnummer;

            return sSQL;
        }

        public static string HaalGridDetails(string sID)
        {
            var sSQL = "SELECT m.ID, CONCAT(m.NummerKostenplaats,' ',c.codeomschrijving) as sCode, m.naambetaler, " +
                       "m.blad, m.bedrag, ";
            sSQL += "m.subcode, m.omschrijving, m.opmerking, m.dank, c.metnaam, c.metomschr, c.subcode, " +
                    "c.aantal, m.NummerKostenplaats ";
            sSQL += "FROM tbl_Mutaties as m, tbl_CodeTabel as c WHERE m.ID = " + sID + " " +
                    "AND c.IDCode = m.NummerKostenplaats ";
            return sSQL;
        }

        public static string INSERTBoeking(string naamBetaler, string bank, string datumOverzicht, string volgnummer, string blad, string bedrag, string opmerking, string nummerkostenplaats, string subcode, string naamvermelding, string omssub, string omschrijving, string aantal, Boolean dank)
        {
            var bedrag1 = bedrag.Replace(',', '.');
            var s = datumOverzicht.Split('-');
            var sSQL = "";
            sSQL = "INSERT INTO tbl_Mutaties (NaamBetaler, Boekdatum ";
            sSQL += ",DatumOverzicht ";
            sSQL += ",VolgnummerBlad ";
            sSQL += ",Volgnummer ";
            sSQL += ",Blad ";
            sSQL += ",Bank ";
            sSQL += ",Bedrag  ";
            sSQL += ",Opmerking ";
            sSQL += ",BankVolgnummer ";
            sSQL += ",NummerKostenplaats ";
            sSQL += ",BankVolgnummerBlad ";
            sSQL += ",Periode ";
            sSQL += ",Subcode ";
            sSQL += ",NaamVermelding ";
            sSQL += ",OmsSub ";
            sSQL += ",Omschrijving ";
            sSQL += ",Aantal ";
            sSQL += ",Dank ";
            sSQL += ",Bedankt ) ";
            sSQL += "VALUES (";
            sSQL += naamBetaler.Replace("'", "''") + ", ";
            sSQL += "'" + s[2] + '-' + s[1] + '-' + s[0] + "', ";
            sSQL += "'" + s[2] + '-' + s[1] + '-' + s[0] + "', ";
            sSQL += "'" + volgnummer + blad + "', ";
            sSQL += "'" + volgnummer + "', ";
            sSQL += "'" + blad.Replace("'", "''") + "', ";
            sSQL += bank + ", ";
            sSQL += bedrag1 + ", ";
            sSQL += "'" + opmerking.Replace("'", "''") + "', ";
            sSQL += "'" + bank + volgnummer + "', ";
            sSQL += "'" + nummerkostenplaats + "', ";
            sSQL += "'" + bank + volgnummer + blad + "', ";
            sSQL += "'" + s[2]+ '-' + s[1] + '-' + s[0] + "', ";
            sSQL += "'" + subcode + "', ";
            sSQL += "'" + naamvermelding.Replace("'", "''") + "', ";
            sSQL += "'" + omssub.Replace("'", "''") + "', ";
            sSQL += "'" + omschrijving.Replace("'", "''") + "', ";
            sSQL += aantal + ",";
            if (dank == true)
            {
                sSQL += 1;
            }
            else
            {
                sSQL += 0;
            }
            sSQL += ", ";
            sSQL += 0;
            sSQL += ");";
            return sSQL;
        }

        public static string UPDATEBoeking(string id, string naamBetaler, string bank, string volgnummer, string blad, string bedrag, string opmerking, string nummerkostenplaats, string subcode, string naamvermelding, string omssub, string omschrijving, string aantal, Boolean dank)
        {
            var bedrag1 = bedrag.Replace(',', '.');
            var sSQL = "";
            sSQL += "UPDATE tbl_Mutaties ";
            sSQL += "SET NaamBetaler = '" + naamBetaler.Replace("'", "''") + "' ";
            sSQL += ",Blad = '" + blad.Replace("'", "''") + "' ";
            sSQL += ",Bedrag = " + bedrag1.Replace("'", "''");
            sSQL += ",Opmerking = '" + opmerking.Replace("'", "''") + "' ";
            sSQL += ",NummerKostenplaats = '" + nummerkostenplaats.Replace("'", "''") + "' ";
            sSQL += ",Bank = " + bank.Replace("'", "''");
            sSQL += ",Volgnummer = '" + volgnummer.Replace("'", "''") + "' ";
            sSQL += ",VolgnummerBlad = '" + volgnummer.Replace("'", "''") + blad.Replace("'", "''") + "' ";
            sSQL += ",BankVolgnummerBlad = '" + bank.Replace("'", "''") + volgnummer.Replace("'", "''") + blad.Replace("'", "''") + "' ";
            sSQL += ",Subcode = '" + subcode.Replace("'", "''") + "' ";
            sSQL += ",NaamVermelding = '" + naamvermelding.Replace("'", "''") + "' ";
            sSQL += ",OmsSub = '" + omssub.Replace("'", "''") + "' ";
            sSQL += ",Omschrijving = '" + omschrijving.Replace("'", "''") + "' ";
            if (dank == true)
            {
                sSQL += ",Dank = " + 1;
            }
            else
            {
                sSQL += ",Dank = " + 0;
            }

            sSQL += " WHERE ID = " + id;
            return sSQL;
        }

        public static string DELETEBoeking(string id)
        {
            var sSQL = "";
            sSQL = "DELETE FROM tbl_Mutaties WHERE ID = " + id;
            return sSQL;
        }

        public static void INSERT_mutaties_bank(string bank, string datum, string volgnummer, string saldo)
        {
            var sSQL = "";
            sSQL = "INSERT INTO tbl_MutatiesBanken (Bank, Datum, Volgnummer, Beginsaldo, Eindsaldo, " +
                   "TeBoeken, BankVolgNummer) VALUES (";
            sSQL += "'" + bank + "', '" + datum + "', '" + volgnummer + "', '" + saldo + "', '" + saldo + "', '0', '" + bank + volgnummer + "')";
            GenericDataRead.INUPDEL(sSQL);
        }

        public static void UPDATE_mutaties_bank(string bank, string volgnummer, string beginsaldo, string eindsaldo, string mutaties)
        {
            var sSQL = "";
            sSQL = "UPDATE tbl_MutatiesBanken SET Beginsaldo = '" + beginsaldo + "', ";
            sSQL += "Eindsaldo = '" + eindsaldo + "', ";
            sSQL += "TeBoeken = '" + mutaties + "' WHERE Bank = '" + bank + "' AND  ";
            sSQL += "Volgnummer = '" + volgnummer + "' ";
            GenericDataRead.INUPDEL(sSQL);
        }

        public static string controleBedankjes()
        {
            var bedrag = Convert.ToDouble(Functions.sBedragBedankjes("ConfigData.xml"));
            var sSQL = "SELECT * FROM tbl_Mutaties WHERE Dank = 1 and Bedankt = 0 and Bedrag >= " + bedrag;
            return sSQL;
        }

        public static string controleBedankjesAantal()
        {
            var bedrag = Convert.ToDouble(Functions.sBedragBedankjes("ConfigData.xml"));
            var sSQL = "SELECT COUNT(*) FROM tbl_Mutaties WHERE Dank = 1 and Bedankt = 0 and Bedrag >= " + bedrag;
            return sSQL;
        }

        public static string HaalBetalingenAdres(string naamBetaler, string jaar)
        {
            var sSQL = "SELECT CONCAT(a.nummerkostenplaats,' ', b.CodeOmschrijving) AS Code, SUM(a.bedrag) AS Bedrag ";
            sSQL += "FROM tbl_Mutaties AS a, ";
            sSQL += "tbl_CodeTabel AS b ";
            sSQL += "WHERE naambetaler > 0  ";
            sSQL += "AND NaamBetaler = " + naamBetaler + " ";
            sSQL += "AND YEAR(boekdatum) = " + jaar + " ";
            sSQL += "AND a.NummerKostenplaats = b.IDCode ";
            sSQL += "GROUP BY NummerKostenplaats";
            return sSQL;
        }

    }
}
