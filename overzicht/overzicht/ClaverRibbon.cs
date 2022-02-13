using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using Domain;
using Microsoft.Office.Tools.Excel;
using Microsoft.Office.Tools.Ribbon;

namespace overzicht
{
    public partial class ClaverRibbon
    {
        public int iJaar;
        public int iMaand;

        private void ClaverRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("it-IT");
            for (var intX = 1; intX < 13; intX++)
            {
                var mnd = Factory.CreateRibbonDropDownItem();
                var jar = Factory.CreateRibbonDropDownItem();
                mnd.Label = intX.ToString();
                cmbMaand.Items.Add(mnd);

                var Jaar = DateTime.Now.Year - 6 + intX;
                jar.Label = Jaar.ToString();
                cmbJaar.Items.Add(jar);
            }

            iMaand = DateTime.Now.Month - 1;
            iJaar = DateTime.Now.Year;
            if (iMaand == 0)
            {
                iMaand = 12;
                iJaar -= 1;
            }

            cmbMaand.Text = iMaand.ToString();
            cmbJaar.Text = iJaar.ToString();

            btnHalen.Enabled = true;
        }

        private void btnHalen_Click(object sender, RibbonControlEventArgs e)
        {
            var folderPath = Application.ExecutablePath;
            var myPath = Globals.ThisWorkbook.Path;

            // Huis
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Huisll"]);

            // Missie
            Worksheet worksheet2 =
                Globals.Factory.GetVstoObject(
                    Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[
                        "Missiell"]);

            // MTL
            Worksheet worksheet3 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["MTLl"]);

            // Zaken
            Worksheet worksheet4 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Zakenll"]);

            // Balans
            Worksheet worksheet11 =
                Globals.Factory.GetVstoObject(
                    Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[
                        "Bilancio"]);


            // Spec336
            Worksheet worksheet8 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec336"]);

            // Spec333
            Worksheet worksheet7 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec333"]);

            // Spec318
            Worksheet worksheet5 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec318"]);

            // Spec330
            Worksheet worksheet6 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec330"]);

            // Spec426
            Worksheet worksheet9 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec426"]);

            // Spec450
            Worksheet worksheet10 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec450"]);

            var sMaand = iMaand;
            var sJaar = iJaar;
            var d = new DateTimeOffset(sJaar, sMaand, 1, 0, 0, 0,
                TimeSpan.Zero);
            var sDatum = d.ToString("MMMM yyyy");

            clsFuncties.WriteCellByCell(1, 11, worksheet1, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(2, 4, worksheet2, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(1, 11, worksheet3, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(1, 11, worksheet4, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(1, 4, worksheet8, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(1, 4, worksheet7, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(2, 5, worksheet5, "Mese: " + sDatum);
            clsFuncties.WriteCellByCell(1, 2, worksheet6, "Month: " + sDatum);
            clsFuncties.WriteCellByCell(1, 3, worksheet9, "Month: " + sDatum);
            clsFuncties.WriteCellByCell(1, 2, worksheet10, "Month: " + sDatum);
            sMaand = iMaand - 1;
            sJaar = iJaar;
            if (sMaand == 0)
            {
                sMaand = 12;
                sJaar -= 1;
            }

            d = new DateTimeOffset(sJaar, sMaand, 1, 0, 0, 0, TimeSpan.Zero);
            sDatum = d.ToString("MMMM yyyy");
            clsFuncties.WriteCellByCell(5, 1, worksheet1,
                "Saldo per " + sDatum);

            // clsFuncties.WriteCellByCell(5, 3, worksheet2,  "Saldo per "+sDatum);
            clsFuncties.WriteCellByCell(5, 3, worksheet3,
                "Saldo per " + sDatum);
            clsFuncties.WriteCellByCell(5, 1, worksheet4,
                "Balance for " + sDatum);
            clsFuncties.WriteCellByCell(19, 1, worksheet2,
                "Balance for " + sDatum);
            sMaand = iMaand + 1;
            sJaar = iJaar;
            if (sMaand == 13)
            {
                sMaand = 1;
                sJaar += 1;
            }

            d = new DateTimeOffset(sJaar, sMaand, 1, 0, 0, 0, TimeSpan.Zero);
            sDatum = d.ToString("MMMM yyyy");
            clsFuncties.WriteCellByCell(22, 1, worksheet1,
                "Saldo per " + sDatum);
            clsFuncties.WriteCellByCell(22, 4, worksheet2,
                "Saldo per " + sDatum);
            clsFuncties.WriteCellByCell(22, 4, worksheet3,
                "Saldo per " + sDatum);
            clsFuncties.WriteCellByCell(22, 1, worksheet4,
                "Balance for " + sDatum);
            clsFuncties.WriteCellByCell(19, 7, worksheet2,
                "Balance for " + sDatum);
            BeginBalans();
            bankgegevens();
            Grootboek();
            Spec336();
            Spec333();
            Spec318();
            Spec330();
            Spec331();
            Spec426();
            Spec451();

            SpecBilanci0();
            EindBalans();
            bijwerkenSaldo.Enabled = true;
            MessageBox.Show("Overzicht is samengesteld.");
        }

        private void BeginBalans()
        {
            // Huis
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Huisll"]);

            // Missie
            Worksheet worksheet2 =
                Globals.Factory.GetVstoObject(
                    Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[
                        "Missiell"]);

            // MTL
            Worksheet worksheet3 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["MTLl"]);

            // Zaken
            Worksheet worksheet4 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Zakenll"]);

            // Balans
            Worksheet worksheet11 =
                Globals.Factory.GetVstoObject(
                    Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[
                        "Bilancio"]);

            var sMaand = iMaand - 1;
            var sJaar = iJaar;
            if (sMaand == 0)
            {
                sMaand = 12;
                sJaar -= 1;
            }

            var libraryFactory = new LibraryFactory();
            var beginsaldi = new List<BeginBalans>();
            beginsaldi = libraryFactory.GetBeginBalans(sMaand, sJaar);
            foreach (var beginsaldo in beginsaldi)
                switch (Convert.ToInt32(beginsaldo.idcode))
                {
                    case 1:
                        clsFuncties.WriteCellByCell(5, 9, worksheet1,
                            beginsaldo.saldo.ToString().Replace(",", "."));
                        break;

                    case 2:
                        clsFuncties.WriteCellByCell(19, 4, worksheet2,
                            beginsaldo.saldo.ToString().Replace(",", "."));
                        clsFuncties.WriteCellByCell(33, 3, worksheet11,
                            beginsaldo.saldo.ToString().Replace(",", "."));
                        break;

                    case 3:
                        clsFuncties.WriteCellByCell(5, 9, worksheet3,
                            beginsaldo.saldo.ToString().Replace(",", "."));
                        break;

                    case 4:
                        clsFuncties.WriteCellByCell(5, 9, worksheet4,
                            beginsaldo.saldo.ToString().Replace(",", "."));
                        break;
                }
        }

        private void bankgegevens()
        {
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Huisll"]);

            var iDay = DateTime.DaysInMonth(iJaar, iMaand);

            var datum =
                Utilities.dhLastDayInMonth(
                    Convert.ToDateTime(iJaar + "-" + iMaand + "-01"));
            var libraryFactory = new LibraryFactory();
            var banken = new List<Bank>();
            var bankSaldi = new List<BankSaldo>();
            banken = libraryFactory.GetBanken();
            foreach (var bank in banken)
            {
                bankSaldi =
                    libraryFactory.GetBankSaldi(bank.bankId.ToString(), datum);
                foreach (var bankSaldo in bankSaldi)
                {
                    // Plaats het bedrag
                    clsFuncties.WriteCellByCell(
                        Convert.ToInt16(bankSaldo.rij),
                        Convert.ToInt16(bankSaldo.kolom),
                        worksheet1,
                        bankSaldo.saldo.ToString(CultureInfo.InvariantCulture)
                            .Replace(".", "."));

                    // Plaats de banknaam
                    clsFuncties.WriteCellByCell(
                        Convert.ToInt16(bankSaldo.rij),
                        Convert.ToInt16(bankSaldo.kolom) + 1,
                        worksheet1,
                        bankSaldo.omschrijving);

                    // Plaats het rekeningnummer
                    clsFuncties.WriteCellByCell(
                        Convert.ToInt16(bankSaldo.rij),
                        Convert.ToInt16(bankSaldo.kolom) + 2,
                        worksheet1,
                        bankSaldo.nummer);

                    // Plaats het bankID
                    clsFuncties.WriteCellByCell(
                        Convert.ToInt16(bankSaldo.rij),
                        Convert.ToInt16(bankSaldo.kolom) + 3,
                        worksheet1,
                        bankSaldo.bank);

                    // Plaats de laatste datum
                    clsFuncties.WriteCellByCell(
                        Convert.ToInt16(bankSaldo.rij),
                        Convert.ToInt16(bankSaldo.kolom) + 4,
                        worksheet1,
                        bankSaldo.datum);
                }
            }
        }

        private void Grootboek()
        {
            clsCellenLeeg.MaakGrootboekLeegHuis();
            clsCellenLeeg.MaakGrootboekLeegMissie();
            clsCellenLeeg.MaakGrootboekLeegMTL();
            clsCellenLeeg.MaakGrootboekLeegZaken();
            clsCellenLeeg.MaakGrootboekLeegBalans();

            var sSheet = string.Empty;
            var libraryFactory = new LibraryFactory();
            var grootboek = new List<GrootBoek>();
            grootboek =
                libraryFactory.GetGrootboek(iMaand,
                    iJaar); // , Globals.ThisWorkbook.Path);
            foreach (var boeking in grootboek)
            {
                switch (boeking.code)
                {
                    case 1:
                        sSheet = "Huisll";
                        break;

                    case 2:
                        sSheet = "Missiell";
                        break;

                    case 4:
                        sSheet = "MTLl";
                        break;

                    case 3:
                        sSheet = "Zakenll";
                        break;

                    default:
                        sSheet = "Huisll";
                        break;
                }

                clsFuncties.WriteCellByCell(
                    boeking.rij,
                    boeking.kolom,
                    sSheet,
                    boeking.saldo.ToString().Replace(",", "."));
            }
        }

        private void Spec318()
        {
            clsCellenLeeg.Spec318Leeg();
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec318"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec318 = new List<Specificatie318>();
            spec318 = libraryFactory.GetSpecificatie318(iMaand, iJaar);
            foreach (var spec in spec318)
            {
                clsFuncties.WriteCellByCell(intRij + 5, 2, worksheet1,
                    spec.naam);
                clsFuncties.WriteCellByCell(intRij + 5, 3, worksheet1,
                    spec.aantal.ToString());
                clsFuncties.WriteCellByCell(intRij + 5, 4, worksheet1,
                    spec.omschrijving);
                clsFuncties.WriteCellByCell(intRij + 5, 5, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void Spec330()
        {
            clsCellenLeeg.Spec330Leeg();
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec330"]);

            var libraryFactory = new LibraryFactory();
            var spec330 = new List<Specificatie330>();
            spec330 = libraryFactory.GetSpecificatie330(iMaand, iJaar);
            foreach (var spec in spec330)
                clsFuncties.WriteCellByCell(
                    Convert.ToInt16(spec.rij),
                    2,
                    worksheet1,
                    spec.saldo.ToString().Replace(",", "."));
        }

        private void Spec331()
        {
            clsCellenLeeg.LeegMetOpties(21, 47, 1, 2, "Spec330", string.Empty);
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec330"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec331 = new List<Specificatie331>();
            spec331 = libraryFactory.GetSpecificatie331(iMaand, iJaar);
            foreach (var spec in spec331)
            {
                clsFuncties.WriteCellByCell(intRij + 21, 1, worksheet1,
                    spec.donateur);
                clsFuncties.WriteCellByCell(intRij + 21, 2, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void Spec333()
        {
            clsCellenLeeg.Spec333Leeg();
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec333"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec333 = new List<Specificatie333>();
            spec333 = libraryFactory.GetSpecificatie333(iMaand, iJaar);
            foreach (var spec in spec333)
            {
                clsFuncties.WriteCellByCell(
                    intRij + 4,
                    1,
                    worksheet1,
                    Convert.ToString(intRij + 1) + ". " + spec.omschrijving);
                clsFuncties.WriteCellByCell(intRij + 4, 5, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void Spec336()
        {
            clsCellenLeeg.Spec336Leeg();
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec336"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec336 = new List<Specificatie336>();
            spec336 = libraryFactory.GetSpecificatie336(iMaand, iJaar);
            foreach (var spec in spec336)
            {
                clsFuncties.WriteCellByCell(intRij + 4, 1, worksheet1,
                    spec.naamVolledig);
                clsFuncties.WriteCellByCell(intRij + 4, 5, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void Spec426()
        {
            clsCellenLeeg.LeegMetOpties(5, 44, 1, 3, "Spec426", string.Empty);
            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec426"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec426 = new List<Specificatie426>();
            spec426 = libraryFactory.GetSpecificatie426(iMaand, iJaar);
            foreach (var spec in spec426)
            {
                clsFuncties.WriteCellByCell(intRij + 5, 1, worksheet1,
                    spec.opmerking);
                clsFuncties.WriteCellByCell(intRij + 5, 2, worksheet1,
                    spec.omschrijving);
                clsFuncties.WriteCellByCell(intRij + 5, 3, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void Spec451()
        {
            clsCellenLeeg.LeegMetOpties(19, 30, 1, 2, "Spec450", string.Empty);
            clsCellenLeeg.LeegMetOpties(34, 48, 1, 2, "Spec450", string.Empty);

            Worksheet worksheet1 =
                Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec450"]);

            var intRij = 0;
            var libraryFactory = new LibraryFactory();
            var spec451 = new List<Specificatie451>();
            spec451 = libraryFactory.GetSpecificatie451(iMaand, iJaar);
            foreach (var spec in spec451)
            {
                clsFuncties.WriteCellByCell(intRij + 19, 1, worksheet1,
                    spec.naamPlaats);
                clsFuncties.WriteCellByCell(intRij + 19, 2, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }

            intRij = 0;
            var spec452 = new List<Specificatie452>();
            spec452 = libraryFactory.GetSpecificatie452(iMaand, iJaar);
            foreach (var spec in spec452)
            {
                clsFuncties.WriteCellByCell(intRij + 34, 1, worksheet1,
                    spec.omschrijving);
                clsFuncties.WriteCellByCell(intRij + 34, 2, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));
                intRij++;
            }
        }

        private void SpecBilanci0()
        {
            Worksheet worksheet1 = Globals.Factory.GetVstoObject(
                Globals.ThisWorkbook.Application.ActiveWorkbook.Worksheets[
                    "Bilancio"]);

            var libraryFactory = new LibraryFactory();
            var eindbalans = new List<Balansio>();
            eindbalans = libraryFactory.GetBalansio(iMaand, iJaar);
            foreach (var spec in eindbalans)
                clsFuncties.WriteCellByCell(spec.rij, 6, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));

            var eindbalans346 = new List<Balansio346>();
            eindbalans346 = libraryFactory.GetBalansio346(iMaand, iJaar);
            foreach (var spec in eindbalans346)
                clsFuncties.WriteCellByCell(spec.rij, spec.kolom, worksheet1,
                    spec.bedrag.ToString().Replace(",", "."));

            var eindbalans450 = new List<Specificatie450>();
            eindbalans450 = libraryFactory.GetSpecificatie450(iMaand, iJaar);
            foreach (var spec in eindbalans450)
                clsFuncties.WriteCellByCell(
                    Convert.ToInt32(spec.Rij),
                    6,
                    worksheet1,
                    spec.Bedrag.ToString().Replace(",", "."));

            // 457, 458, 459
/*
            var eindbalansRest = new List<BalansioRest>();
            eindbalansRest = libraryFactory.GetBalansRest(iMaand, iJaar);
            foreach (var spec in eindbalansRest)
            {
                clsFuncties.WriteCellByCell(spec.rij, 6, worksheet1, spec.bedrag.ToString().Replace(",", "."));
            }
*/
        }

        private void EindBalans()
        {
            var sSQL = clsSQLData.eindbalansDelete(iMaand, iJaar);
            GenericDataRead.INUPDEL(sSQL);

            var datum =
                Utilities.dhLastDayInMonth(
                    Convert.ToDateTime(iJaar + "-" + iMaand + "-01"));
            var worksheet = "Huisll";
            short idcode = 1;
            var rij = 54;
            var kolom = 9;
            var bedrag = clsFuncties.GetCellValue(rij, kolom, worksheet)
                .Replace(",", ".");
            sSQL = clsSQLData.eindbalansVoegtoe(idcode, datum, bedrag);
            GenericDataRead.INUPDEL(sSQL);

            idcode = 2;
            rij = 52;
            bedrag = clsFuncties.GetCellValue(rij, kolom, worksheet)
                .Replace(",", ".");
            sSQL = clsSQLData.eindbalansVoegtoe(idcode, datum, bedrag);
            GenericDataRead.INUPDEL(sSQL);

            idcode = 3;
            rij = 55;
            bedrag = clsFuncties.GetCellValue(rij, kolom, worksheet)
                .Replace(",", ".");
            sSQL = clsSQLData.eindbalansVoegtoe(idcode, datum, bedrag);
            GenericDataRead.INUPDEL(sSQL);

            idcode = 4;
            rij = 53;
            bedrag = clsFuncties.GetCellValue(rij, kolom, worksheet)
                .Replace(",", ".");
            sSQL = clsSQLData.eindbalansVoegtoe(idcode, datum, bedrag);
            GenericDataRead.INUPDEL(sSQL);
        }

        private string dhLastDayInMonth(DateTime dDatum)
        {
            var d = new DateTimeOffset(
                dDatum.Year,
                dDatum.Month,
                DateTime.DaysInMonth(dDatum.Year, dDatum.Month),
                0,
                0,
                0,
                TimeSpan.Zero);
            return d.Day.ToString();
        }

        private void cmbMaand_TextChanged(object sender,
            RibbonControlEventArgs e)
        {
            iMaand = Convert.ToInt16(cmbMaand.Text);
        }

        private void cmbJaar_TextChanged(object sender,
            RibbonControlEventArgs e)
        {
            iJaar = Convert.ToInt16(cmbJaar.Text);
        }

        private void bijwerkenSaldo_Click(object sender,
            RibbonControlEventArgs e)
        {
            EindBalans();
            MessageBox.Show("Saldi zijn bijgewerkt.");
        }

        private void btnLeegmaken_Click(object sender, RibbonControlEventArgs e)
        {
            clsCellenLeeg.MaakGrootboekLeegHuis();
            clsCellenLeeg.MaakGrootboekLeegMissie();
            clsCellenLeeg.MaakGrootboekLeegMTL();
            clsCellenLeeg.MaakGrootboekLeegZaken();
            clsCellenLeeg.MaakGrootboekLeegBalans();

            // Bankstanden leegmaken
            for (int rij = 52; rij <= 74; rij++)
            {
                for (int kolom = 15; kolom <= 19; kolom++)
                {
                    clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                        .ActiveWorkbook.Worksheets["Huisll"]), "");
                }
            }

            // Spec330 leegmaken
            for (int rij = 5; rij <= 17; rij++)
            {
                var kolom = 2;
                    clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                        .ActiveWorkbook.Worksheets["Spec330"]), "");
            }

            for (int rij = 20; rij <= 47; rij++)
            {
                var kolom = 2;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Spec330"]), "");
            }

            // Balans leegmaken
            for (int rij = 77; rij <= 90; rij++)
            {
                var kolom = 6;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Bilancio"]), "0");
            }
            for (int rij = 94; rij <= 96; rij++)
            {
                var kolom = 6;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Bilancio"]), "0");
            }
            for (int rij = 100; rij <= 102; rij++)
            {
                var kolom = 6;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Bilancio"]), "0");
            }
            for (int rij = 106; rij <= 110; rij++)
            {
                var kolom = 6;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Bilancio"]), "0");
            }
            for (int rij = 114; rij <= 117; rij++)
            {
                var kolom = 6;
                clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                    .ActiveWorkbook.Worksheets["Bilancio"]), "0");
            }

            for (int rij = 5; rij <= 44; rij++)
            {
                for (int kolom = 1; kolom <= 4; kolom++)
                {
                    clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                        .ActiveWorkbook.Worksheets["Spec426"]), "");
                }
            }

            // Spec318
            for (int rij = 5; rij <= 54; rij++)
            {
                for (int kolom = 1; kolom <= 5; kolom++)
                {
                    clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                        .ActiveWorkbook.Worksheets["Spec318"]), "");
                }
            }

            // Spec333
            for (int rij = 4; rij <= 44; rij++)
            {
                for (int kolom = 1; kolom <= 5; kolom++)
                {
                    clsFuncties.WriteCellByCell(rij, kolom, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                        .ActiveWorkbook.Worksheets["Spec333"]), "");
                }
            }

            // Beginbalans
            clsFuncties.WriteCellByCell(5, 9, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["Huisll"]), "0");
            clsFuncties.WriteCellByCell(19, 4, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["Missiell"]), "0");
            clsFuncties.WriteCellByCell(18, 10, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["Missiell"]), "0");
            clsFuncties.WriteCellByCell(5, 9, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["MTLl"]), "0");
            clsFuncties.WriteCellByCell(5, 9, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["Zakenll"]), "0");
            clsFuncties.WriteCellByCell(33, 3, Globals.Factory.GetVstoObject(Globals.ThisWorkbook.Application
                .ActiveWorkbook.Worksheets["Bilancio"]), "0");
        }
    }
}