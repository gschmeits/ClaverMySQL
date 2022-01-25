using System;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using ClaverMySQL.Utils;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    ///     Interaction logic for pg_ControlePerKostenplaats.xaml
    /// </summary>
    public partial class pg_ControlePerKostenplaats : Page
    {
        private PrintDialog dialog = new PrintDialog();
        private PrintDocument document = new PrintDocument();

        public pg_ControlePerKostenplaats()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var sSQL = clsDataBoekhouding.cmbJaartal();
            var dtA = GenericDataRead.GetData(sSQL);
            cmbJaar.ItemsSource = dtA.DefaultView;
            cmbJaar.DisplayMemberPath = "jaartal";
            cmbJaar.SelectedValuePath = "jaartal";

            sSQL = clsDataBoekhouding.cmbMaand();
            dtA = GenericDataRead.GetData(sSQL);
            cmbMaand.ItemsSource = dtA.DefaultView;
            cmbMaand.SelectedValuePath = "maand";


            cmbJaar.SelectedValue = DateTime.Now.Year;
            cmbMaand.SelectedValue = DateTime.Now.Month;
        }

        private void overzichtMaken_Click(object sender, RoutedEventArgs e)
        {
            HaalWaardenOp();
        }

        private void printenOverzicht_Click(object sender, RoutedEventArgs e)
        {
            var datum = Convert.ToDateTime(cmbJaar.SelectedValue + "-" +
                                           cmbMaand.SelectedValue + "-" + "01");
            var datum1 = Utilities.dhLastDayInMonth(datum);
            var pr = new PrintControle
            {
                GetMaand = () => { return cmbMaand.SelectedValue.ToString(); },
                GetJaar = () => { return cmbJaar.SelectedValue.ToString(); },
                GetText = () => { return gegevens.Text; }
            };
            pr.PrintGegevens();
        }

        private void schermSlujiten_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void HaalWaardenOp()
        {
            gegevens.Visibility = Visibility.Hidden;
            melding.Visibility = Visibility.Visible;
            printenOverzicht.IsEnabled = false;
            gegevens.Refresh();
            if (cmbMaand.Text.Length > 0 && cmbJaar.Text.Length > 0)
            {
                var maandVorig = 0;
                var maandHuidig = 0;
                var jaarVorig = 0;
                var jaarHuidig = 0;
                decimal dblSaldo1 = 0;
                decimal dblSaldo2 = 0;
                decimal dblSaldo3 = 0;
                decimal dblSaldo4 = 0;
                decimal dblSaldoN1 = 0;
                decimal dblSaldoN2 = 0;
                decimal dblSaldoN3 = 0;
                decimal dblSaldoN4 = 0;
                var code1 = "";
                var code2 = "";
                var code3 = "";
                var code4 = "";
                if (cmbMaand.SelectedValue.ToString() == "1")
                {
                    maandVorig = 12;
                    jaarVorig = Convert.ToInt32(cmbJaar.Text) - 1;
                }
                else
                {
                    maandVorig =
                        Convert.ToInt32(cmbMaand.SelectedValue.ToString()) - 1;
                    jaarVorig =
                        Convert.ToInt32(cmbJaar.SelectedValue.ToString());
                }

                var sSQL =
                    clsDataControle.contVorigeMaand(maandVorig, jaarVorig);
                var dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    code1 = dt.Rows[0]["code"].ToString();
                    code2 = dt.Rows[1]["code"].ToString();
                    code3 = dt.Rows[2]["code"].ToString();
                    code4 = dt.Rows[3]["code"].ToString();
                    dblSaldo1 =
                        Convert.ToDecimal(dt.Rows[0]["saldo"].ToString());
                    dblSaldo2 =
                        Convert.ToDecimal(dt.Rows[1]["saldo"].ToString());
                    dblSaldo3 =
                        Convert.ToDecimal(dt.Rows[2]["saldo"].ToString());
                    dblSaldo4 =
                        Convert.ToDecimal(dt.Rows[3]["saldo"].ToString());
                }

                maandHuidig =
                    Convert.ToInt32(cmbMaand.SelectedValue.ToString());
                jaarHuidig = Convert.ToInt32(cmbJaar.SelectedValue.ToString());
                sSQL = "";
                sSQL = clsDataControle.contVorigeMaand(maandHuidig, jaarHuidig);
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    dblSaldoN1 =
                        Convert.ToDecimal(dt.Rows[0]["saldo"].ToString());
                    dblSaldoN2 =
                        Convert.ToDecimal(dt.Rows[1]["saldo"].ToString());
                    dblSaldoN3 =
                        Convert.ToDecimal(dt.Rows[2]["saldo"].ToString());
                    dblSaldoN4 =
                        Convert.ToDecimal(dt.Rows[3]["saldo"].ToString());
                }

                gegevens.Text = "";
                gegevens.Text = gegevens.Text +
                                "                                           Vorige maand:   Huidige maand:       Verschil:\r\n ";
                gegevens.Text = gegevens.Text +
                                "                                          -------------   --------------   -------------\r\n";
                gegevens.Text = gegevens.Text +
                                string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}",
                                    code1, dblSaldo1, dblSaldoN1,
                                    dblSaldoN1 - dblSaldo1) + "\r\n";
                gegevens.Text = gegevens.Text +
                                string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}",
                                    code2, dblSaldo2, dblSaldoN2,
                                    dblSaldoN2 - dblSaldo2) + "\r\n";
                gegevens.Text = gegevens.Text +
                                string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}",
                                    code3, dblSaldo3, dblSaldoN3,
                                    dblSaldoN3 - dblSaldo3) + "\r\n";
                gegevens.Text = gegevens.Text +
                                string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}",
                                    code4, dblSaldo4, dblSaldoN4,
                                    dblSaldoN4 - dblSaldo4) + "\r\n";
                gegevens.Text = gegevens.Text +
                                "                                           -------------   --------------   -------------\r\n";
                gegevens.Text = gegevens.Text + string.Format(
                    "{0,-40}{1,16:N}{2,17:N}{3,16:N}", "Totaal:",
                    dblSaldo1 + dblSaldo2 + dblSaldo3 + dblSaldo4,
                    dblSaldoN1 + dblSaldoN2 + dblSaldoN3 + dblSaldoN4,
                    dblSaldoN1 + dblSaldoN2 + dblSaldoN3 + dblSaldoN4 -
                    dblSaldo1 - dblSaldo2 - dblSaldo3 - dblSaldo4) + "\r\n";
                gegevens.Text = gegevens.Text +
                                "                                          ==============   ==============   =============\r\n";
                gegevens.Text += "\r\n\r\n";


                // Haal de geboekte subcodes op zonder nummer
                sSQL = clsDataControle.controleSubcodes(
                    Convert.ToString(maandHuidig),
                    Convert.ToString(jaarHuidig));
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    gegevens.Text += "Geen subcode geselecteerd\r\n";
                    gegevens.Text += "=========================\r\n";
                    gegevens.Text +=
                        "Boekdatum  Bank                     KPL Omschrijving            Opmerking          Bedrag\r\n";
                    gegevens.Text +=
                        "-----------------------------------------------------------------------------------------\r\n";
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var datumC = dt.Rows[i][1].ToString().Substring(0, 10);
                        var bankC = dt.Rows[i][3] + " " + dt.Rows[i][4];
                        var kpl = dt.Rows[i][5].ToString();
                        var bedragC =
                            Convert.ToDouble(dt.Rows[i][6].ToString());
                        var omschrijvingC = dt.Rows[i][7].ToString();
                        var opmerkingC = dt.Rows[i][10].ToString();
                        if (dt.Rows[i][7].ToString().Length > 24)
                            omschrijvingC = dt.Rows[i][7].ToString()
                                .Substring(0, 24);
                        if (dt.Rows[i][10].ToString().Length > 15)
                            opmerkingC = dt.Rows[i][10].ToString()
                                .Substring(0, 15);

                        gegevens.Text +=
                            string.Format(
                                "{0,-11}{1,-25}{2,-4}{3,-24}{4,-15}{5,10:N2}",
                                datumC, bankC, kpl, omschrijvingC, opmerkingC,
                                bedragC) + "\r\n";
                    }

                    gegevens.Text += "\r\n";
                    gegevens.Text +=
                        "CORRIGEER DEZE BOEKING(EN) VOORDAT U VERDER GAAT!!!";
                    gegevens.Text += "\r\n\r\n\r\n";
                }

                // Haal de geboekte bedragen met waarde 0
                sSQL = clsDataControle.controleBedragen(
                    Convert.ToString(maandHuidig),
                    Convert.ToString(jaarHuidig));
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    gegevens.Text += "Geen bedrag ingevuld; bedrag is 0\r\n";
                    gegevens.Text += "=================================\r\n";
                    gegevens.Text +=
                        "Boekdatum  Bank                     KPL Omschrijving            Opmerking          Bedrag\r\n";
                    gegevens.Text +=
                        "-----------------------------------------------------------------------------------------\r\n";
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        var datumC = dt.Rows[i][1].ToString().Substring(0, 10);
                        var bankC = dt.Rows[i][3] + " " + dt.Rows[i][4];
                        var kpl = dt.Rows[i][5].ToString();
                        var bedragC =
                            Convert.ToDouble(dt.Rows[i][6].ToString());
                        var omschrijvingC = dt.Rows[i][9].ToString();
                        var opmerkingC = dt.Rows[i][7].ToString();
                        if (dt.Rows[i][9].ToString().Length > 24)
                            omschrijvingC = dt.Rows[i][9].ToString()
                                .Substring(0, 24);
                        if (dt.Rows[i][7].ToString().Length > 15)
                            opmerkingC = dt.Rows[i][7].ToString()
                                .Substring(0, 15);

                        gegevens.Text +=
                            string.Format(
                                "{0,-11}{1,-25}{2,-4}{3,-24}{4,-15}{5,10:N2}",
                                datumC, bankC, kpl, omschrijvingC, opmerkingC,
                                bedragC) + "\r\n";
                    }

                    gegevens.Text += "\r\n";
                    gegevens.Text +=
                        "CORRIGEER DEZE BOEKING(EN) VOORDAT U VERDER GAAT!!!";
                    gegevens.Text += "\r\n\r\n\r\n";
                }

                gegevens.Text += HaalbankSaldiOp();

                var datum = Convert.ToDateTime(cmbJaar.SelectedValue + "-" +
                                               cmbMaand.SelectedValue + "-" +
                                               "01");
                var datum1 = Utilities.dhLastDayInMonth(datum);
                var datum2 = datum.AddMonths(-1);

                #region grootboekrekeningen

                var grootboek = "";
                var soort = "Huis";
                short intT = 0;
                decimal dblTot = 0;
                decimal dblSubTot = 0;
                decimal dblTotTot = 0;

                sSQL = clsDataControle.saldoPerKostenplaatsGrootboek(datum1);
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    for (var intX = 0; intX < dt.Rows.Count; intX++)
                    {
                        if (grootboek != dt.Rows[intX][1].ToString().TrimEnd()
                            .TrimStart())
                        {
                            if (intT > 1)
                            {
                                dblTot = 0;
                                if (soort != dt.Rows[intX][0].ToString())
                                {
                                    gegevens.Text +=
                                        "                                                                            -------------\r\n";
                                    gegevens.Text +=
                                        string.Format("{0,10}{1,-60}{2,18:N}",
                                            "Subtotaal: ", soort, dblSubTot) +
                                        "\r\n";
                                    gegevens.Text += "\r\n";
                                    soort = dt.Rows[intX][0].ToString();
                                    dblSubTot = 0;
                                }
                            }

                            gegevens.Text += string.Format(
                                "{0,-8}{1,-4}{2,-59}{4,18:N}", dt.Rows[intX][0],
                                dt.Rows[intX][1], dt.Rows[intX][2], "",
                                Convert.ToDecimal(dt.Rows[intX][3]
                                    .ToString())) + "\r\n";
                            var s = dt.Rows[intX][2].ToString().Split('-');
                            dblTot +=
                                Convert.ToDecimal(dt.Rows[intX][3].ToString());
                            grootboek = dt.Rows[0][1].ToString().TrimEnd()
                                .TrimStart();
                            intT = 2;
                        }

                        if (grootboek == dt.Rows[intX][1].ToString().TrimEnd()
                            .TrimStart()) intT = 2;
                        dblSubTot +=
                            Convert.ToDecimal(dt.Rows[intX][3].ToString());
                        dblTotTot +=
                            Convert.ToDecimal(dt.Rows[intX][3].ToString());
                        grootboek = dt.Rows[intX][1].ToString().TrimEnd()
                            .TrimStart();
                        soort = dt.Rows[intX][0].ToString();
                    }

                    dblTot = 0;
                    gegevens.Text +=
                        "                                                                            -------------\r\n";
                    gegevens.Text += string.Format("{0,10}{1,-60}{2,18:N}",
                        "Subtotaal: ", soort, dblSubTot) + "\r\n";
                    dblSubTot = 0;
                    gegevens.Text +=
                        "\r\n\r\n                                                                            -------------\r\n";
                    gegevens.Text +=
                        string.Format("{0,-40}{1,49:N}", "Totaal:", dblTotTot) +
                        "\r\n";
                    gegevens.Text +=
                        "                                                                            =============\r\n";

                    #endregion

                    melding.Visibility = Visibility.Hidden;
                    gegevens.Visibility = Visibility.Visible;
                    printenOverzicht.IsEnabled = true;
                }
            }
        }

        private string HaalbankSaldiOp()
        {
            #region banksaldi

            // Haal de het aantal bankrekeningen op
            var aantalBanken = 0;
            var sSQL = clsDataControle.aantalBanken();
            var Tekst = "";
            var dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
                aantalBanken = Convert.ToInt32(dt.Rows[0][0].ToString());
            var banken = new int[aantalBanken];

            var datum = Convert.ToDateTime(cmbJaar.SelectedValue + "-" +
                                           cmbMaand.SelectedValue + "-" + "01");
            var datum1 = Utilities.dhLastDayInMonth(datum);
            var datum0 = Utilities.dhLastDayInMonth(datum1.AddMonths(-1));

            for (var intX = 0; intX <= aantalBanken - 1; intX++)
                if (intX < 22)
                {
                    clsDataControle.VulBanksaldiBeginsaldo(intX + 1, datum0);
                    clsDataControle.VulBanksaldiEindsaldo(intX + 1, datum1);
                }

            Tekst += "Eindsaldi Banken\r\n";
            Tekst +=
                "-----------------------------------------------------------------------------------------\r\n";
            sSQL = clsDataControle.saldiBanken();
            dt = GenericDataRead.GetData(sSQL);
            decimal saldo = 0;
            if (dt.Rows.Count > 0)
                for (var intX = 0; intX < dt.Rows.Count; intX++)
                {
                    Tekst += string.Format("{0,-20}{1,20}{2,49:N}",
                                 dt.Rows[intX][1], dt.Rows[intX][2],
                                 dt.Rows[intX][3]) +
                             "\r\n";
                    saldo += Convert.ToDecimal(dt.Rows[intX][3].ToString());
                }

            Tekst +=
                "                                                                            -------------\r\n";
            Tekst += string.Format("{0,-40}{1,49:N}", "Totaal:", saldo) +
                     "\r\n";
            Tekst +=
                "                                                                            =============\r\n\r\n";

            sSQL = clsDataControle.saldoCodePerBank(datum1);
            dt = GenericDataRead.GetData(sSQL);
            decimal saldo1 = 0;
            if (dt.Rows.Count > 0)
            {
                for (var intX = 0; intX < dt.Rows.Count; intX++)
                {
                    Tekst += string.Format("{0,-18}{1,14}{2,7}{3,-34}{4,16:N}",
                                 dt.Rows[intX][3], dt.Rows[intX][4],
                                 dt.Rows[intX][0] + ": ", dt.Rows[intX][5],
                                 Convert.ToDecimal(dt.Rows[intX][1]
                                     .ToString())) +
                             "\r\n";
                    saldo1 += Convert.ToDecimal(dt.Rows[intX][1].ToString());
                }

                Tekst +=
                    "                                                                            -------------\r\n";
                Tekst += string.Format("{0,-40}{1,49:N}", "Totaal:", saldo1) +
                         "\r\n";
                Tekst +=
                    "                                                                            =============\r\n\r\n";
            }

            return Tekst;

            #endregion banksaldi
        }
    }
}