using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;

    namespace ClaverMySQL
    {
        /// <summary>
        ///     Class Inlezen.
        /// </summary>
        public static class Inlezen
        {
            /// <summary>
            ///     The afschriftregel
            /// </summary>
            public static List<FormaatMT940.Afschriftregel> _afschriftregel = new List<FormaatMT940.Afschriftregel>();

            /// <summary>
            ///     The beginsaldo
            /// </summary>
            public static List<FormaatMT940.Beginsaldo> _beginsaldo = new List<FormaatMT940.Beginsaldo>();

            /// <summary>
            ///     The eindsaldo
            /// </summary>
            public static List<FormaatMT940.Eindsaldo> _eindsaldo = new List<FormaatMT940.Eindsaldo>();

            /// <summary>
            ///     The rekeninghouder
            /// </summary>
            public static List<FormaatMT940.Rekeninghouder> _rekeninghouder = new List<FormaatMT940.Rekeninghouder>();

            /// <summary>
            ///     The rekeningidentificatie
            /// </summary>
            public static List<FormaatMT940.Rekeningidentificatie> _rekeningidentificatie =
                new List<FormaatMT940.Rekeningidentificatie>();

            /// <summary>
            ///     The transactierefernetienummer
            /// </summary>
            public static List<FormaatMT940.Transactiereferentienummer> _transactierefernetienummer =
                new List<FormaatMT940.Transactiereferentienummer>();

            /// <summary>
            ///     The subcode lists1
            /// </summary>
            public static List<FormaatMT940.SubcodeList> _subcodeLists1 = new List<FormaatMT940.SubcodeList>();

            /// <summary>
            ///     The subcode lists2
            /// </summary>
            public static List<FormaatMT940.SubcodeList> _subcodeLists2 = new List<FormaatMT940.SubcodeList>();

            /// <summary>
            ///     The subcode lists3
            /// </summary>
            public static List<FormaatMT940.SubcodeList> _subcodeLists3 = new List<FormaatMT940.SubcodeList>();

            /// <summary>
            ///     The subcode lists4
            /// </summary>
            public static List<FormaatMT940.SubcodeList> _subcodeLists4 = new List<FormaatMT940.SubcodeList>();

            /// <summary>
            ///     The subcode lists5
            /// </summary>
            public static List<FormaatMT940.SubcodeList> _subcodeLists5 = new List<FormaatMT940.SubcodeList>();

            /// <summary>
            ///     The teller
            /// </summary>
            private static short teller;

            /// <summary>
            ///     Gets or sets the bedrag.
            /// </summary>
            /// <value>The bedrag.</value>
            public static string bedrag { get; set; }

            /// <summary>
            ///     Gets or sets the aanvulling.
            /// </summary>
            /// <value>The aanvulling.</value>
            private static string aanvulling { get; set; }

            /// <summary>
            ///     Gets or sets a value indicating whether this <see cref="Inlezen" /> is afbreken.
            /// </summary>
            /// <value><c>true</c> if afbreken; otherwise, <c>false</c>.</value>
            private static bool afbreken { get; set; }

            /// <summary>
            ///     Gets or sets the bank.
            /// </summary>
            /// <value>The bank.</value>
            private static string bank { get; set; }

            /// <summary>
            ///     Gets or sets the betalingskenmerk.
            /// </summary>
            /// <value>The betalingskenmerk.</value>
            private static string betalingskenmerk { get; set; }

            /// <summary>
            ///     Gets or sets the boekdatum.
            /// </summary>
            /// <value>The boekdatum.</value>
            private static string boekdatum { get; set; }

            /// <summary>
            ///     Gets or sets the credit debet.
            /// </summary>
            /// <value>The credit debet.</value>
            private static string credit_debet { get; set; }

            /// <summary>
            ///     Gets or sets the datum.
            /// </summary>
            /// <value>The datum.</value>
            private static string datum { get; set; }

            /// <summary>
            ///     Gets or sets the iban.
            /// </summary>
            /// <value>The iban.</value>
            private static string iban { get; set; }

            /// <summary>
            ///     Gets or sets the inhoud regel.
            /// </summary>
            /// <value>The inhoud regel.</value>
            private static string[] inhoudRegel { get; set; }

            /// <summary>
            ///     Gets or sets the lines.
            /// </summary>
            /// <value>The lines.</value>
            private static string[] lines { get; set; }

            /// <summary>
            ///     Gets or sets the naam.
            /// </summary>
            /// <value>The naam.</value>
            private static string naam { get; set; }

            /// <summary>
            ///     Gets or sets the omschrijving.
            /// </summary>
            /// <value>The omschrijving.</value>
            private static string omschrijving { get; set; }

            /// <summary>
            ///     Gets or sets the rekeninghouder information.
            /// </summary>
            /// <value>The rekeninghouder information.</value>
            private static string rekeninghouderInfo { get; set; }

            /// <summary>
            ///     Gets or sets the rekeningnummer.
            /// </summary>
            /// <value>The rekeningnummer.</value>
            private static string rekeningnummer { get; set; }

            /// <summary>
            ///     Gets or sets the bank i d1.
            /// </summary>
            /// <value>The bank i d1.</value>
            private static string bankID1 { get; set; }

            /// <summary>
            ///     Gets or sets the tag.
            /// </summary>
            /// <value>The tag.</value>
            private static string tag { get; set; }

            /// <summary>
            ///     Gets or sets the tag1.
            /// </summary>
            /// <value>The tag1.</value>
            private static string[] tag1 { get; set; }

            /// <summary>
            ///     Gets or sets the transactiereferentie.
            /// </summary>
            /// <value>The transactiereferentie.</value>
            private static string transactiereferentie { get; set; }

            /// <summary>
            ///     Gets or sets the transactietype.
            /// </summary>
            /// <value>The transactietype.</value>
            private static string transactietype { get; set; }

            /// <summary>
            ///     Gets or sets the valuatedatum.
            /// </summary>
            /// <value>The valuatedatum.</value>
            private static string valuatedatum { get; set; }

            /// <summary>
            ///     Gets or sets the valuta.
            /// </summary>
            /// <value>The valuta.</value>
            private static string valuta { get; set; }

            /// <summary>
            ///     Gets or sets the valuta eigenaar.
            /// </summary>
            /// <value>The valuta eigenaar.</value>
            private static string valuta_eigenaar { get; set; }

            /// <summary>
            ///     Gets or sets the iban eigen.
            /// </summary>
            /// <value>The iban eigen.</value>
            private static string IBANEigen { get; set; }

            /// <summary>
            ///     Gets or sets the IDL code.
            /// </summary>
            /// <value>The IDL code.</value>
            private static int idlCode { get; set; }

            /// <summary>
            ///     Gets or sets the subcodeall.
            /// </summary>
            /// <value>The subcodeall.</value>
            private static string[] subcodeall { get; set; }


            /// <summary>
            ///     Opens this instance.
            /// </summary>
            /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
            public static bool Open()
            {
                _afschriftregel.Clear();
                _transactierefernetienummer.Clear();
                _beginsaldo.Clear();
                _eindsaldo.Clear();
                _rekeninghouder.Clear();
                _rekeningidentificatie.Clear();

                using (var openFileDialog = new OpenFileDialog())
                {
                    var ok = false;
                    openFileDialog.Filter = "MT940 bestanden (*.940)|*.940|MT940 bestanden (MT940*.*)|MT940*.*";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ReadLine(openFileDialog.FileName);

                        ok = true;
                    }

                    return ok;
                }
            }


            /// <summary>
            ///     Ings the detail.
            /// </summary>
            /// <param name="regel">The regel.</param>
            private static void INGDetail(string regel)
            {
                inhoudRegel = regel.Split('/');

                iban = string.Empty;
                naam = string.Empty;
                omschrijving = string.Empty;
                tag = string.Empty;
                valuatedatum = string.Empty;
                boekdatum = string.Empty;
                credit_debet = string.Empty;
                bedrag = string.Empty;
                transactietype = string.Empty;
                transactiereferentie = string.Empty;
                betalingskenmerk = string.Empty;
                idlCode = 0;

                for (var intX = 0; intX < inhoudRegel.Length; intX++)
                    switch (inhoudRegel[intX])
                    {
                        case "CNTP":
                            iban = inhoudRegel[intX + 1];
                            naam = inhoudRegel[intX + 3];
                            break;
                        case "USTD":
                            omschrijving = inhoudRegel[intX + 2];
                            break;
                    }

                tag = inhoudRegel[0].Substring(0, 4); // tag
                valuatedatum = "20" + inhoudRegel[0].Substring(4, 6); // Valutadatum;
                boekdatum = inhoudRegel[0].Substring(4, 6); // Boekdatum
                credit_debet = inhoudRegel[0].Substring(14, 1); // Credit_Debet ;
                var pos = inhoudRegel[0].IndexOf("N", StringComparison.Ordinal) - 1;

                bedrag = inhoudRegel[0].Substring(15, pos - 14); // Bedrag;
                if (credit_debet == "C")
                    bedrag = "-" + bedrag;
                transactietype = inhoudRegel[0].Substring(pos + 2, 3).Trim();
                aanvulling = regel;

                transactiereferentie = inhoudRegel[2];
                betalingskenmerk = inhoudRegel[0].Substring(pos + 5, inhoudRegel[0].Length - (pos + 5)).Trim();
                var naamID = string.Empty;
                var codeID = string.Empty;

                // Subcode defini
                string[] subCode1 = { };
                List<string> list = new List<string>();
                if (betalingskenmerk.Length == 16)
                    if (Convert.ToInt64(betalingskenmerk) > 0)
                    {
                        codeID = betalingskenmerk.Substring(1, 3);
                        naamID = Convert.ToString(Convert.ToInt32(betalingskenmerk.Substring(10, 6)));
                        var sql = "SELECT idlcode FROM SubCode WHERE Rekeningnummer = " + codeID;
                        var db = GenericDataRead.GetData(sql);

                        if (db.Rows.Count > 0)
                        {
                            for (int i = 0; i < db.Rows.Count; i++)
                            {
                                list.Add(db.Rows[i][0].ToString());
                            }
                            subCode1 = list.ToArray();
                        }
                    }

                _afschriftregel.Add(
                    new FormaatMT940.Afschriftregel
                    {
                        Tag = tag,
                        ValutaDatum = valuatedatum,
                        BoekDatum = boekdatum,
                        Credit_Debet = credit_debet,
                        Bedrag = bedrag,
                        Transactietype = transactietype,
                        Betalingskenmerk = betalingskenmerk,
                        Transactiereferentie = transactiereferentie,
                        Aanvulling = aanvulling,
                        IBAN = iban,
                        Naam = naam,
                        Omschrijving = omschrijving,
                        IBANEigen = IBANEigen,
                        bankNaam = bank,
                        bankID = bankID1,
                        CodeID = codeID,
                        NaamID = naamID,
                        IDLCode = idlCode,
                        Rekeningummer = "",
                        SubOms = "",
                        idlcode_rek_oms = "",
                        subCode = subCode1
                    });
            }

            /// <summary>
            ///     Raboes the detail.
            /// </summary>
            /// <param name="regel">The regel.</param>
            private static void RABODetail(string regel)
            {
                iban = string.Empty;
                naam = string.Empty;
                omschrijving = string.Empty;
                tag = string.Empty;
                valuatedatum = string.Empty;
                boekdatum = string.Empty;
                credit_debet = string.Empty;
                bedrag = string.Empty;
                transactietype = string.Empty;
                transactiereferentie = string.Empty;
                betalingskenmerk = string.Empty;

                var te = regel.Split(new[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                iban = te[1];

                regel = regel.Replace("\r\n", string.Empty).Trim();

                inhoudRegel = regel.Split('/');

                for (var intX = 0; intX < inhoudRegel.Length; intX++)
                    switch (inhoudRegel[intX])
                    {
                        case "NAME":
                            naam = inhoudRegel[intX + 1];
                            break;
                        case "REMI":
                            omschrijving = inhoudRegel[intX + 1];
                            break;
                    }

                if (inhoudRegel.Length > 1)
                    if (inhoudRegel[1] == "PREF")
                        omschrijving = inhoudRegel[2];

                tag = inhoudRegel[0].Substring(0, 4); // tag
                valuatedatum = "20" + inhoudRegel[0].Substring(4, 6); // Valutadatum;
                boekdatum = inhoudRegel[0].Substring(4, 6); // Boekdatum
                credit_debet = inhoudRegel[0].Substring(10, 1); // Credit_Debet ;
                var pos = inhoudRegel[0].IndexOf("N", StringComparison.Ordinal) - 1;

                bedrag = inhoudRegel[0].Substring(15, pos - 14); // Bedrag;
                if (credit_debet == "C")
                    bedrag = "-" + bedrag;
                transactietype = inhoudRegel[0].Substring(pos + 2, 3).Trim();
                aanvulling = regel;

                if (inhoudRegel.Length > 2) transactiereferentie = inhoudRegel[2];

                betalingskenmerk = inhoudRegel[0].Substring(pos + 5, inhoudRegel[0].Length - (pos + 5)).Trim();

                _afschriftregel.Add(
                    new FormaatMT940.Afschriftregel
                    {
                        Tag = tag,
                        ValutaDatum = valuatedatum,
                        BoekDatum = boekdatum,
                        Credit_Debet = credit_debet,
                        Bedrag = Convert.ToDecimal(bedrag).ToString(),
                        Transactietype = transactietype,
                        Betalingskenmerk = betalingskenmerk,
                        Transactiereferentie = transactiereferentie,
                        Aanvulling = aanvulling,
                        IBAN = iban,
                        Naam = naam,
                        Omschrijving = omschrijving,
                        IBANEigen = IBANEigen,
                        bankNaam = bank,
                        bankID = bankID1
                    });

                var tekst = "\r\nTag                  : " + tag + "\r\n";
                tekst += "Valutadatum          : " + valuatedatum + "\r\n";
                tekst += "Boekdatum            : " + boekdatum + "\r\n";
                tekst += "Credit_Debet         : " + credit_debet + "\r\n";
                tekst += "Bedrag               : " + Convert.ToDecimal(bedrag) + "\r\n";
                tekst += "Transactietype       : " + transactietype + "\r\n";
                tekst += "Betalingskenmerk     : " + betalingskenmerk + "\r\n";
                tekst += "Transactiereferentie : " + transactiereferentie + "\r\n";
                tekst += "Aanvulling           : " + aanvulling + "\r\n";
                tekst += "IBAN                 : " + iban + "\r\n";
                tekst += "Naam                 : " + naam + "\r\n";
                tekst += "Omschrijving         : " + omschrijving + "\r\n";

                Functions.LogMessageToFile(tekst, 1);
            }

            /// <summary>
            ///     SNSs the detail.
            /// </summary>
            /// <param name="regel">The regel.</param>
            private static void SNSDetail(string regel)
            {
                if (regel.Length > 30)
                {
                    iban = string.Empty;
                    naam = string.Empty;
                    omschrijving = string.Empty;
                    tag = string.Empty;
                    valuatedatum = string.Empty;
                    boekdatum = string.Empty;
                    credit_debet = string.Empty;
                    bedrag = string.Empty;
                    transactietype = string.Empty;
                    transactiereferentie = string.Empty;
                    betalingskenmerk = string.Empty;

                    var regelSlipt = regel.Split(':');

                    tag = regel.Substring(0, 4);
                    var regel61 = regelSlipt[2];
                    if (regel61.Length > 30)
                    {
                        valuatedatum = "20" + regel61.Substring(0, 6);
                        boekdatum = regel61.Substring(0, 2) + regel61.Substring(6, 4);
                        credit_debet = regel61.Substring(10, 1);
                        var totPos = regel61.Substring(11, 15).IndexOf("N");
                        bedrag = regel61.Substring(11, totPos);
                        transactietype = regel61.Substring(totPos + 11, 4);
                        iban = regel61.Substring(totPos + 11 + 4, 18);
                        var tekst1 = regel61.Substring(totPos + 11 + 4 + 18, regel61.Length - (totPos + 11 + 4 + 18));
                        var tel86 = regelSlipt.Length;
                        var regel86 = "";
                        for (var intX = 4; intX < tel86; intX++) regel86 += regelSlipt[intX] + ":";

                        regel86 = regel86.Substring(0, regel86.Length - 1);

                        switch (transactietype)
                        {
                            case "NKNT":
                                omschrijving = regel86.Trim();
                                break;
                            case "NKST":
                                omschrijving = regel86.Trim();
                                break;
                            case "NOVB":
                            case "NIOB":
                                naam = tekst1.ToUpper();

                                omschrijving = regel86.Substring(
                                    18 + tekst1.Length + 1,
                                    regel86.Length - 18 - tekst1.Length - 1).Trim();
                                if (omschrijving.IndexOf(iban) != -1)
                                    omschrijving = omschrijving.Substring(19, omschrijving.Length - 19);

                                Functions.LogMessageToFile("Naam: " + naam, 1);
                                break;
                            case "NINC":
                                var beginPos = regel86.IndexOf(':') + 1;
                                var regel86SplitI = regel86.Substring(
                                    beginPos,
                                    regel86.Length - (regel86.IndexOf(':') + 1));
                                var regel86SplitII = regel86SplitI.Split('-');
                                naam = regel86SplitII[0].Trim();
                                var teller = regel86SplitII.Length;
                                for (var intX = 1; intX < teller; intX++) omschrijving += regel86SplitII[intX].Trim() + " ";

                                omschrijving = omschrijving.Trim();
                                break;
                        }

                        aanvulling = regel;
                        // Boekjaar laten berekenen
                        //var boekDatumActief = AdministratiesQueries.BoekjaarActief();
                        var boekDatumActief = "2019";

                        if (boekDatumActief.Substring(2, 2) == boekdatum.Substring(0, 2))
                        {
                            _afschriftregel.Add(
                                new FormaatMT940.Afschriftregel
                                {
                                    Tag = tag,
                                    ValutaDatum = valuatedatum,
                                    BoekDatum = boekdatum,
                                    Credit_Debet = credit_debet,
                                    Bedrag = bedrag,
                                    Transactietype = transactietype,
                                    Betalingskenmerk = betalingskenmerk,
                                    Transactiereferentie = transactiereferentie,
                                    Aanvulling = aanvulling,
                                    IBAN = iban,
                                    Naam = naam,
                                    Omschrijving = omschrijving,
                                    IBANEigen = IBANEigen,
                                    bankNaam = bank,
                                    bankID = bankID1
                                });

                            var tekst = "\r\nTag                  : " + tag + "\r\n";
                            tekst += "Valutadatum          : " + valuatedatum + "\r\n";
                            tekst += "Boekdatum            : " + boekdatum + "\r\n";
                            tekst += "Credit_Debet         : " + credit_debet + "\r\n";
                            tekst += "Bedrag               : " + Convert.ToDecimal(bedrag) + "\r\n";
                            tekst += "Transactietype       : " + transactietype + "\r\n";
                            tekst += "Betalingskenmerk     : " + betalingskenmerk + "\r\n";
                            tekst += "Transactiereferentie : " + transactiereferentie + "\r\n";
                            tekst += "Aanvulling           : " + aanvulling + "\r\n";
                            tekst += "IBAN                 : " + iban + "\r\n";
                            tekst += "Naam                 : " + naam + "\r\n";
                            tekst += "Omschrijving         : " + omschrijving + "\r\n";

                            Functions.LogMessageToFile(tekst, 1);
                        }
                        else
                        {
                            MessageBox.Show(
                                "De geselecteerde mutaties bevatten niet de juiste datum!!!\r\n\r\nSelecteer een bestand waarin het jaar van de boekingen overeenkomt met het geselecteerde boekjaar van de betreffende administratie.",
                                "Foutmelding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            afbreken = true;
                        }
                    }
                }
            }

            /// <summary>
            ///     Reads the line.
            /// </summary>
            /// <param name="bestand">The bestand.</param>
            private static void ReadLine(string bestand)
            {
                lines = File.ReadAllLines(bestand);
                var indexG = 0;
                var indexEind = lines.Length;
                var indexTeller = 0;
                var regel = string.Empty;
                afbreken = false;

                foreach (var b in lines)
                    if (b.Length > 4)
                        if (b.Substring(0, 4) == ":25:")
                        {
                            bank = b.Substring(8, 4);
                            var banknummer = b.Substring(4, 18);
                            bankID1 = banknummer;
                            //zoekBank();
                            break;
                        }

                foreach (var line in lines)
                {
                    if (line.Length > 4)
                    {
                        afbreken = false;
                        SetRekeningIdenticatie(line);
                        regel = string.Empty;
                        if (line.Substring(0, 4) == ":61:" || line.Substring(0, 5) == ":62F:")
                            for (var intX = indexTeller; intX < indexEind - 1; intX++)
                            {
                                if (intX > indexTeller && lines[intX].Length > 4
                                                       && (lines[intX].Substring(0, 4) == ":61:"
                                                           || lines[intX].Substring(0, 5) == ":62F:"))
                                {
                                    if (bank == "INGB") INGDetail(regel.Replace("\r\n", string.Empty).Trim());

                                    if (bank == "RABO")
                                    {
                                        if (regel.Contains(":62F:"))
                                        {
                                            var regel1 = regel.Substring(0, regel.IndexOf(":62F:"));
                                            regel = regel1;
                                        }

                                        if (regel.Length > 2) RABODetail(regel);
                                    }

                                    if (bank == "SNSB") SNSDetail(regel.Replace("\r\n", string.Empty).Trim());

                                    break;
                                }

                                regel = regel + "\r\n" + lines[intX];
                            }
                    }

                    indexTeller++;
                    if (afbreken) break;
                }

                if (clsBankafschriften.CheckBankrekeningnummer(rekeningnummer))
                {
                    foreach (var line in lines)
                    {
                        if (line.Length >= 4)
                        {
                            SetTransactiereferentieNummer(line);
                            SetBeginsaldo(line);

                            // SetAfschriftregel(line, indexG);
                            SetInfoRekeninghouder(line, indexG);
                            SetEindsaldo(line);
                        }

                        indexG++;
                    }
                }
                else
                {
                    MessageBox.Show(
                        "De geselecteerde mutaties bevatten niet de juiste datum!!!\r\n\r\nSelecteer een bestand waarin het jaar van de boekingen overeenkomt met het geselecteerde boekjaar van de betreffende administratie.",
                        "Foutmelding", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    afbreken = true;
                }
            }

            /// <summary>
            ///     Return86s the specified line.
            /// </summary>
            /// <param name="line">The line.</param>
            /// <param name="indexF">The index f.</param>
            /// <returns>System.String.</returns>
            private static string return86(string line, int indexF)
            {
                var teller = indexF;
                var regel = string.Empty;
                do
                {
                    regel += lines[teller];
                    teller++;
                    if (lines[teller].Length < 2) lines[teller] += " ";
                } while (lines[teller].Substring(0, 2) != ":6" || lines[teller].Length < 2);

                return regel;
            }

            /// <summary>
            ///     Sets the afschriftregel.
            /// </summary>
            /// <param name="line">The line.</param>
            /// <param name="indexP">The index p.</param>
            private static void SetAfschriftregel(string line, int indexP)
            {
                if (line.Substring(0, 4) == ":61:")
                {
                    var s = line;
                    var pos = s.Substring(15, s.Length - 15).IndexOf("N", StringComparison.Ordinal);
                    var pos1 = s.IndexOf("//", StringComparison.Ordinal) + 2;

                    tag = s.Substring(0, 4); // Tag;
                    valuatedatum = s.Substring(4, 6); // Valutadatum;
                    boekdatum = s.Substring(10, 4); // Boekdatum
                    if (bank == "RABO")
                        credit_debet = s.Substring(10, 1); // Credit_Debet ;
                    else
                        credit_debet = s.Substring(14, 1); // Credit_Debet ;

                    bedrag = s.Substring(15, pos); // Bedrag;
                    if (credit_debet == "C")
                        bedrag = "-" + bedrag;
                    transactietype = s.Substring(16 + pos, 3).Trim();

                    if (bank == "RABO")
                        _afschriftregel.Add(
                            new FormaatMT940.Afschriftregel
                            {
                                Tag = tag,
                                ValutaDatum = valuatedatum,
                                BoekDatum = boekdatum,
                                Credit_Debet = credit_debet,
                                Bedrag = bedrag,
                                Transactietype = transactietype,
                                Betalingskenmerk = betalingskenmerk,
                                Transactiereferentie = transactiereferentie,
                                Aanvulling = string.Empty
                            });
                    if (bank == "INGB")
                    {
                        // betalingskenmerk = s.Substring(15 + pos + 4, pos1 - 2 - 15 - 4 - pos).Trim();
                        // transactiereferentie = s.Substring(pos1, 14).Trim();
                    }

                    if (bank == "SNSB")
                    {
                        boekdatum = s.Substring(4, 6);
                        var regels = s + lines[indexP + 1];

                        betalingskenmerk = regels.Substring(15 + pos + 4, regels.Length - (15 + pos + 4)).Trim();
                        transactiereferentie = regels.Substring(15 + pos + 4, regels.Length - (15 + pos + 4)).Trim();
                        aanvulling = regels.Substring(15 + pos + 4, regels.Length - (15 + pos + 4)).Trim();
                        _afschriftregel.Add(
                            new FormaatMT940.Afschriftregel
                            {
                                Tag = tag,
                                ValutaDatum = valuatedatum,
                                BoekDatum = boekdatum,
                                Credit_Debet = credit_debet,
                                Bedrag = bedrag,
                                Transactietype = transactietype,
                                Betalingskenmerk = betalingskenmerk,
                                Transactiereferentie = transactiereferentie,
                                Aanvulling = aanvulling
                            });
                    }
                }
            }

            /// <summary>
            ///     Sets the beginsaldo.
            /// </summary>
            /// <param name="line">The line.</param>
            private static void SetBeginsaldo(string line)
            {
                if (line.Length > 4)
                    if (line.Substring(0, 5) == ":60F:")
                    {
                        var tag = line.Substring(0, 5);
                        var credit_debet = line.Substring(5, 1);
                        var datum = "20" + line.Substring(6, 2) + "-" + line.Substring(8, 2) + "-" + line.Substring(10, 2);
                        var valuta = line.Substring(12, 3).Trim();
                        var bedrag = line.Substring(15, line.Length - 15);
                        _beginsaldo.Add(
                            new FormaatMT940.Beginsaldo
                            {
                                Tag = tag,
                                Credit_Debet = credit_debet,
                                Bedrag = Convert.ToDecimal(bedrag),
                                Valuta = valuta,
                                Datum = Convert.ToDateTime(datum)
                            });
                    }
            }

            /// <summary>
            ///     Sets the eindsaldo.
            /// </summary>
            /// <param name="line">The line.</param>
            private static void SetEindsaldo(string line)
            {
                if (line.Length > 4)
                    if (line.Substring(0, 5) == ":62F:")
                    {
                        tag = line.Substring(0, 5);
                        credit_debet = line.Substring(5, 1);
                        datum = "20" + line.Substring(6, 2) + "-" + line.Substring(8, 2) + "-" + line.Substring(10, 2);
                        valuta = line.Substring(12, 3);
                        bedrag = line.Substring(15, line.Length - 15);
                        _eindsaldo.Add(
                            new FormaatMT940.Eindsaldo
                            {
                                Tag = tag,
                                Bedrag = Convert.ToDecimal(bedrag),
                                Datum = Convert.ToDateTime(datum),
                                Credit_Debet = credit_debet,
                                Valuta = valuta
                            });
                    }
            }

            /// <summary>
            ///     Sets the information rekeninghouder.
            /// </summary>
            /// <param name="line">The line.</param>
            /// <param name="indexF">The index f.</param>
            private static void SetInfoRekeninghouder(string line, int indexF)
            {
                // MessageBox.Show(line);
                if (line.Substring(0, 4) == ":86:")
                {
                    rekeninghouderInfo = line;
                    teller = 1;
                    tag = ":86:";

                    if (bank == "RABO")
                    {
                        iban = lines[indexF - 1].Replace("\r\n", string.Empty).Trim();
                        if (iban.Substring(0, 2) == "00")
                            iban = string.Empty;
                        var regel86 = return86(line, indexF);
                        var regelN = regel86.Split('/');

                        var posOmschrijving = Array.IndexOf(regelN, "REMI") + 1;

                        var posNaam = Array.IndexOf(regelN, "NAME") + 1;
                        if (Array.IndexOf(regelN, "PREF") == 1)
                        {
                            posOmschrijving = Array.IndexOf(regelN, "PREF") + 1;
                            posNaam = Array.IndexOf(regelN, "PREF") + 1;
                        }

                        naam = regelN[posNaam];
                        omschrijving = regelN[posOmschrijving];

                        if (regelN.Length > Array.IndexOf(regelN, "REMI") + 2 && Array.IndexOf(regelN, "REMI") != -1)
                        {
                            posOmschrijving += 1;
                            omschrijving += " " + regelN[posOmschrijving];
                        }

                        if (regelN.Length > Array.IndexOf(regelN, "REMI") + 3 && Array.IndexOf(regelN, "REMI") != -1)
                        {
                            posOmschrijving += 2;
                            omschrijving += " " + regelN[posOmschrijving];
                        }

                        if (regelN.Length > Array.IndexOf(regelN, "REMI") + 4 && Array.IndexOf(regelN, "REMI") != -1)
                        {
                            posOmschrijving += 3;
                            omschrijving += " " + regelN[posOmschrijving];
                        }

                        _rekeninghouder.Add(
                            new FormaatMT940.Rekeninghouder
                            {
                                Tag = tag,
                                IBAN = iban,
                                Naam = naam,
                                Omschrijving = omschrijving
                            });
                    }

                    if (bank == "SNSB")
                    {
                        var omschrijving86 = lines[indexF] + lines[indexF + 2] + lines[indexF + 3] + lines[indexF + 4]
                                             + lines[indexF + 5];
                        var tempS = omschrijving86.Replace("\r\n", string.Empty).Trim();
                        if (lines[indexF + 2].Length > 54 && lines[indexF + 2].Substring(0, 54)
                            == "ABONNEMENTSKOSTEN               SNS INTERNET BANKIEREN")
                        {
                            iban = " ";
                            naam = "Bank";
                            omschrijving = tempS.Substring(4, 18) + tempS.Substring(36, tempS.Length - 36).Trim();
                        }
                        else
                        {
                            var infoS = tempS.Split('-');
                            iban = infoS[0].Substring(4, 18).Trim();
                            if (infoS.Length > 1) naam = infoS[1].Trim();
                            if (infoS.Length > 2) omschrijving = infoS[2].Trim();
                        }

                        _rekeninghouder.Add(
                            new FormaatMT940.Rekeninghouder
                            {
                                Tag = tag,
                                IBAN = iban,
                                Naam = naam,
                                Omschrijving = omschrijving
                            });
                    }
                }

                if (bank == "INGB")
                {
                    if (line.Length > 5)
                        if (teller != 1 && line.Length > 5 || line.Substring(0, 6) == "/TRCD/" ||
                            line.Substring(0, 4) == ":61:"
                            || line.Substring(0, 4) == ":20:" || line.Substring(0, 4) == ":86:")
                            return;

                    rekeninghouderInfo += line;
                    var temp = rekeninghouderInfo.Replace("\r\n", string.Empty).Trim();

                    var info = temp.Split('/');

                    if (info.Length < 10)
                    {
                        iban = string.Empty;
                        naam = "Bank";
                        omschrijving = info[4];
                    }
                    else
                    {
                        var teller1 = 2;

                        for (var intX = 0; intX < info.Length; intX++)
                            if (info[intX] == "CNTP")
                            {
                                teller1 = intX + 1;
                                break;
                            }

                        iban = info[teller1];
                        if (teller1 < 11) naam = info[teller1 + 2];
                        if (teller1 < 6) omschrijving = info[2];
                    }

                    _rekeninghouder.Add(
                        new FormaatMT940.Rekeninghouder
                        {
                            Tag = tag,
                            IBAN = iban,
                            Naam = naam,
                            Omschrijving = omschrijving
                        });
                }

                teller = 0;
            }

            /// <summary>
            ///     Sets the rekening identicatie.
            /// </summary>
            /// <param name="line">The line.</param>
            private static void SetRekeningIdenticatie(string line)
            {
                if (line.Substring(0, 4) != ":25:") return;
                tag = line.Substring(0, 4).Trim();
                rekeningnummer = line.Substring(4, 18).Trim();
                bank = line.Substring(8, 4).Trim();
                IBANEigen = line.Substring(4, 18).Trim();
                _rekeningidentificatie.Add(
                    new FormaatMT940.Rekeningidentificatie { Tag = tag, Rekeningnummer = rekeningnummer });
            }

            /// <summary>
            ///     Sets the transactiereferentie nummer.
            /// </summary>
            /// <param name="line">The line.</param>
            private static void SetTransactiereferentieNummer(string line)
            {
                if (line.Substring(0, 4) != ":20:") return;
                var tag = line.Substring(0, 4);
                var transactiereferentienummer = line.Substring(5, line.Length - 5).Trim();
                _transactierefernetienummer.Add(
                    new FormaatMT940.Transactiereferentienummer
                    {
                        Tag = tag,
                        Transactiereferentie_nummer = transactiereferentienummer
                    });
            }

            public static string zoekBank()
            {
                var sSQL = "SELECT BankID FROM tbl_Banken WHERE IBAN = " + bankID1;
                var dt = GenericDataRead.GetData(sSQL);
                var data = "";
                if (dt.Rows.Count > 0)
                {
                    data = dt.Rows[0][0].ToString();
                }
                return data;
            }
        }
    }
}
