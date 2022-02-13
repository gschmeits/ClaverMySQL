using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Drawing.Printing;
using ClaverMySQL.Utils;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_ControleDetails.xaml
    /// </summary>
    public partial class pg_ControleDetails : Page
    {

        PrintDocument document = new PrintDocument();
        System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog();

        public pg_ControleDetails()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var sSQL = clsDataBoekhouding.cmbJaartal();
            DataTable dtA = GenericDataRead.GetData(sSQL);
            cmbJaar.ItemsSource = dtA.DefaultView;
            cmbJaar.DisplayMemberPath = "jaartal";
            cmbJaar.SelectedValuePath = "jaartal";

            sSQL = clsDataBoekhouding.cmbMaand();
            DataTable dtB= GenericDataRead.GetData(sSQL);
            cmbMaand.ItemsSource = dtB.DefaultView;
            cmbMaand.SelectedValuePath = "maand";

            cmbJaar.SelectedValue = DateTime.Now.Year;
            cmbMaand.SelectedValue = DateTime.Now.Month;
        }

        private void printenOverzicht_Click(object sender, RoutedEventArgs e)
        {
            DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(datum);
            var pr = new PrintControle()
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

        private void overzichtMaken_Click(object sender, RoutedEventArgs e)
        {
            HaalWaardenOp();
        }

        private void HaalWaardenOp()
        {
            gegevens.Visibility = Visibility.Hidden;
            melding.Visibility = Visibility.Visible;
            printenOverzicht.IsEnabled = false;
            gegevens.Refresh();
            if (cmbMaand.Text.Length > 0 && cmbJaar.Text.Length > 0)
            {
                Int32 maandVorig = 0;
                Int32 maandHuidig = 0;
                Int32 jaarVorig = 0;
                Int32 jaarHuidig = 0;
                decimal dblSaldo1 = 0;
                decimal dblSaldo2 = 0;
                decimal dblSaldo3 = 0;
                decimal dblSaldo4 = 0;
                decimal dblSaldo5 = 0;
                decimal dblSaldoN1 = 0;
                decimal dblSaldoN2 = 0;
                decimal dblSaldoN3 = 0;
                decimal dblSaldoN4 = 0;
                decimal dblSaldoN5 = 0;
                string code1 = "";
                string code2 = "";
                string code3 = "";
                string code4 = "";
                if (cmbMaand.SelectedIndex == 1)
                {
                    maandVorig = 12;
                    jaarVorig = Convert.ToInt32(cmbJaar.Text) - 1;
                }
                else
                {
                    maandVorig = Convert.ToInt32(cmbMaand.SelectedValue.ToString()) - 1;
                    jaarVorig = Convert.ToInt32(cmbJaar.SelectedValue.ToString());
                }

                var sSQL = clsDataControle.contVorigeMaand(maandVorig, jaarVorig);
                DataTable dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    code1 = dt.Rows[0]["code"].ToString();
                    code2 = dt.Rows[1]["code"].ToString();
                    code3 = dt.Rows[2]["code"].ToString();
                    code4 = dt.Rows[3]["code"].ToString();
                    dblSaldo1 = Convert.ToDecimal(dt.Rows[0]["saldo"].ToString());
                    dblSaldo2 = Convert.ToDecimal(dt.Rows[1]["saldo"].ToString());
                    dblSaldo3 = Convert.ToDecimal(dt.Rows[2]["saldo"].ToString());
                    dblSaldo4 = Convert.ToDecimal(dt.Rows[3]["saldo"].ToString());
                }
                maandHuidig = Convert.ToInt32(cmbMaand.SelectedValue.ToString());
                jaarHuidig = Convert.ToInt32(cmbJaar.SelectedValue.ToString());
                sSQL = "";
                sSQL = clsDataControle.contVorigeMaand(maandHuidig, jaarHuidig);
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    dblSaldoN1 = Convert.ToDecimal(dt.Rows[0]["saldo"].ToString());
                    dblSaldoN2 = Convert.ToDecimal(dt.Rows[1]["saldo"].ToString());
                    dblSaldoN3 = Convert.ToDecimal(dt.Rows[2]["saldo"].ToString());
                    dblSaldoN4 = Convert.ToDecimal(dt.Rows[3]["saldo"].ToString());
                }

                BijwerkenBankenSaldi();

                dt = GenericDataRead.GetData(
                    "SELECT SUM(Beginsaldo), Sum(Eindsaldo) FROM tbl_Banken WHERE BankID = 15 or BankID = 19");
                if (dt.Rows.Count > 0)
                {
                    dblSaldo5 = Convert.ToDecimal(dt.Rows[0][0].ToString());
                    dblSaldoN5 = Convert.ToDecimal(dt.Rows[0][1].ToString());
                }

                gegevens.Text = "";
                gegevens.Text = gegevens.Text + "                                           Vorige maand:   Huidige maand:       Verschil:\r\n ";
                gegevens.Text = gegevens.Text + "                                          -------------   --------------   -------------\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", code1, dblSaldo1, dblSaldoN1, dblSaldoN1 - dblSaldo1) + "\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", code2, dblSaldo2, dblSaldoN2, dblSaldoN2 - dblSaldo2) + "\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", code3, dblSaldo3, dblSaldoN3, dblSaldoN3 - dblSaldo3) + "\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", code4, dblSaldo4, dblSaldoN4, dblSaldoN4 - dblSaldo4) + "\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", "Doti", dblSaldo5, dblSaldoN5, dblSaldoN5 - dblSaldo5) + "\r\n";
                gegevens.Text = gegevens.Text + "                                           -------------   --------------   -------------\r\n";
                gegevens.Text = gegevens.Text + string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", "Totaal:", dblSaldo1 + dblSaldo2 + dblSaldo3 + dblSaldo4 + dblSaldo5, dblSaldoN1 + dblSaldoN2 + dblSaldoN3 + dblSaldoN4 + dblSaldoN5, dblSaldoN1 + dblSaldoN2 + dblSaldoN3 + dblSaldoN4 + dblSaldoN5 - dblSaldo1 - dblSaldo2 - dblSaldo3 - dblSaldo4 - dblSaldo5) + "\r\n";
                gegevens.Text = gegevens.Text + "                                          ==============   ==============   =============\r\n";
                gegevens.Text += "\r\n\r\n";


                // Haal de geboekte subcodes op zonder nummer
                sSQL = clsDataControle.controleSubcodes(Convert.ToString(maandHuidig), Convert.ToString(jaarHuidig));
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    gegevens.Text += "Geen subcode geselecteerd\r\n";
                    gegevens.Text += "=========================\r\n";
                    gegevens.Text += "Boekdatum  Bank                     KPL Omschrijving            Opmerking          Bedrag\r\n";
                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var datumC = dt.Rows[i][1].ToString().Substring(0, 10);
                        var bankC = dt.Rows[i][3].ToString() + " " + dt.Rows[i][4].ToString();
                        var kpl = dt.Rows[i][5].ToString();
                        var bedragC = Convert.ToDouble(dt.Rows[i][6].ToString());
                        var omschrijvingC = dt.Rows[i][7].ToString();
                        var opmerkingC = dt.Rows[i][10].ToString();
                        if (dt.Rows[i][7].ToString().Length > 24)
                        {
                            omschrijvingC = dt.Rows[i][7].ToString().Substring(0, 24);
                        }
                        if (dt.Rows[i][10].ToString().Length > 15)
                        {
                            opmerkingC = dt.Rows[i][10].ToString().Substring(0, 15);
                        }

                        gegevens.Text += string.Format("{0,-11}{1,-25}{2,-4}{3,-24}{4,-15}{5,10:N2}", datumC, bankC, kpl, omschrijvingC, opmerkingC, bedragC) + "\r\n";
                    }
                    gegevens.Text += "\r\n";
                    gegevens.Text += "CORRIGEER DEZE BOEKING(EN) VOORDAT U VERDER GAAT!!!";
                    gegevens.Text += "\r\n\r\n\r\n";
                }

                // Haal de geboekte bedragen met waarde 0
                sSQL = clsDataControle.controleBedragen(Convert.ToString(maandHuidig), Convert.ToString(jaarHuidig));
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    gegevens.Text += "Geen bedrag ingevuld; bedrag is 0\r\n";
                    gegevens.Text += "=================================\r\n";
                    gegevens.Text += "Boekdatum  Bank                     KPL Omschrijving            Opmerking          Bedrag\r\n";
                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var datumC = dt.Rows[i][1].ToString().Substring(0, 10);
                        var bankC = dt.Rows[i][3].ToString() + " " + dt.Rows[i][4].ToString();
                        var kpl = dt.Rows[i][5].ToString();
                        var bedragC = Convert.ToDouble(dt.Rows[i][6].ToString());
                        var omschrijvingC = dt.Rows[i][9].ToString();
                        var opmerkingC = dt.Rows[i][7].ToString();
                        if (dt.Rows[i][9].ToString().Length > 24)
                        {
                            omschrijvingC = dt.Rows[i][9].ToString().Substring(0, 24);
                        }
                        if (dt.Rows[i][7].ToString().Length > 15)
                        {
                            opmerkingC = dt.Rows[i][7].ToString().Substring(0, 15);
                        }

                        gegevens.Text += string.Format("{0,-11}{1,-25}{2,-4}{3,-24}{4,-15}{5,10:N2}", datumC, bankC, kpl, omschrijvingC, opmerkingC, bedragC) + "\r\n";
                    }
                    gegevens.Text += "\r\n";
                    gegevens.Text += "CORRIGEER DEZE BOEKING(EN) VOORDAT U VERDER GAAT!!!";
                    gegevens.Text += "\r\n\r\n\r\n";
                }
                gegevens.Text += HaalbankSaldiOp();

                DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbMaand.SelectedValue.ToString() + "-" + "01");
                DateTime datum1 = Utilities.dhLastDayInMonth(datum);

                var grootboek = "";
                var soort = "Huis";
                Int16 intT = 0;
                Decimal dblTot = 0;
                Decimal dblSubTot = 0;
                Decimal dblTotTot = 0;
                sSQL = clsDataControle.saldoPerGrootboek(datum1);
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    for (int intX = 0; intX < dt.Rows.Count; intX++)
                    {
                        if (grootboek != dt.Rows[intX][3].ToString().TrimEnd().TrimStart())
                        {
                            if (intT > 1)
                            {
                                gegevens.Text += "                                                                            -------------\r\n";
                                gegevens.Text += string.Format("{0,-40}{1,49:N}", "", dblTot) + "\r\n";
                                gegevens.Text += "                                                                            -------------\r\n";
                                dblSubTot += dblTot;
                                dblTot = 0;
                                if (soort != dt.Rows[intX][0].ToString())
                                {
                                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                                    gegevens.Text += string.Format("{0,10}{1,-60}{2,18:N}", "Subtotaal: ", soort, dblSubTot) + "\r\n";
                                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                                    dblTotTot += dblSubTot;
                                    dblSubTot = 0;
                                    soort = dt.Rows[intX][0].ToString();
                                }
                            }
                            gegevens.Text += "\r\n-----------------------------------------------------------------------------------------\r\n";
                            //dhPadRight(rs.Fields(0), 8) & dhPadRight(rs.Fields(1), 4) & dhPadRight(rs.Fields(3), 42) & vbCrLf
                            gegevens.Text += string.Format("{0,-8}{1,-4}{2,-42}", dt.Rows[intX][0].ToString(), dt.Rows[intX][1].ToString(), (dt.Rows[intX][3].ToString())) + "\r\n";
                            gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                            string[] s = dt.Rows[intX][2].ToString().Split('-');

                            var string4 = dt.Rows[intX][4].ToString();
                            if (string4.Length > 25)
                            {
                                string4 = dt.Rows[intX][4].ToString().Substring(0, 25);
                            }

                            gegevens.Text += string.Format("{0,10}{1,1}{2,-35}{3,-25}{4,18:N}", s[0] + "-" + s[1] + "-" + s[2].Substring(0, 4), " ", string4, dt.Rows[intX][5].ToString(), Convert.ToDecimal(dt.Rows[intX][6].ToString())) + "\r\n";
                            dblTot += Convert.ToDecimal(dt.Rows[intX][6].ToString());
                            intT = 2;
                        }
                        if (grootboek == dt.Rows[intX][3].ToString().TrimEnd().TrimStart())
                        {
                            string[] s = dt.Rows[intX][2].ToString().Split('-');
                            gegevens.Text += string.Format("{0,10}{1,1}{2,-35}{3,-25}{4,18:N}", s[0] + "-" + s[1] + "-" + s[2].Substring(0, 4), " ", dt.Rows[intX][4].ToString(), dt.Rows[intX][5].ToString(), Convert.ToDecimal(dt.Rows[intX][6].ToString())) + "\r\n";
                            dblTot += Convert.ToDecimal(dt.Rows[intX][6].ToString());
                            intT = 2;
                        }
                        grootboek = dt.Rows[intX][3].ToString().TrimEnd().TrimStart();
                        soort = dt.Rows[intX][0].ToString();
                    }
                    gegevens.Text += "                                                                            -------------\r\n";
                    gegevens.Text += string.Format("{0,-40}{1,49:N}", "", dblTot) + "\r\n";
                    gegevens.Text += "                                                                            -------------\r\n";
                    dblSubTot += dblTot;
                    dblTot = 0;
                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                    gegevens.Text += string.Format("{0,10}{1,-60}{2,18:N}", "Subtotaal: ", soort, dblSubTot) + "\r\n";
                    gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                    dblTotTot += dblSubTot;
                    dblSubTot = 0;
                    gegevens.Text += "\r\n\r\n                                                                            -------------\r\n";
                    gegevens.Text += string.Format("{0,-40}{1,49:N}", "Totaal:", dblTotTot) + "\r\n";
                    gegevens.Text += "                                                                            =============\r\n";
                    melding.Visibility = System.Windows.Visibility.Hidden;
                    gegevens.Visibility = System.Windows.Visibility.Visible;
                    printenOverzicht.IsEnabled = true;
                }
            }
        }

        private void BijwerkenBankenSaldi()
        {
            DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(datum);
            DateTime datum0 = Utilities.dhLastDayInMonth(datum1.AddMonths(-1));

            // Haal de het aantal bankrekeningen op
            var aantalBanken = 0;
            var sSQL = clsDataControle.aantalBanken();

            DataTable dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
            {
                aantalBanken = Convert.ToInt32(dt.Rows[0][0].ToString());
            }
            int[] banken = new int[aantalBanken];

            for (int intX = 0; intX <= aantalBanken - 1; intX++)
            {
                if (intX < 22)
                {
                    clsDataControle.VulBanksaldiBeginsaldo(intX + 1, datum0);
                    clsDataControle.VulBanksaldiEindsaldo(intX + 1, datum1);
                }
            }
        }

        private string HaalbankSaldiOp()
        {
            #region banksaldi

            string Tekst = "";
            Tekst += "SaldiBanken\r\n";
            Tekst += "-----------------------------------------------------------------------------------------\r\n";
            //Tekst += "                                           Vorige maand:   Huidige maand:       Verschil:\r\n ";
            //gegevens.Text = gegevens.Text + "                                          -------------   --------------   -------------\r\n";

            var sSQL = clsDataControle.saldiBanken();
            var dt = GenericDataRead.GetData(sSQL);
            Decimal saldoBegin = 0;
            Decimal saldoEind = 0;
            if (dt.Rows.Count > 0)
            {
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    //Tekst += string.Format("{0,-20}{1,20}{2,49:N}", dt.Rows[intX][1], dt.Rows[intX][2], dt.Rows[intX][3]) + "\r\n";

                    gegevens.Text = gegevens.Text + string.Format("{0,-20}{1,20}{1,16:N}{2,17:N}{3,16:N}", dt.Rows[intX][1], dt.Rows[intX][2], dt.Rows[intX][3], dt.Rows[intX][4]) + "\r\n";


                    Tekst += string.Format("{0,-20}{1,20}{2,16:N}{3,17:N}{4,16:N}",
                        dt.Rows[intX][1], dt.Rows[intX][2], dt.Rows[intX][3], dt.Rows[intX][4], Convert.ToDecimal(dt.Rows[intX][3].ToString()) - Convert.ToDecimal(dt.Rows[intX][4].ToString())) + "\r\n";

                    if (dt.Rows[intX][3].ToString().Length != 0)
                    {
                        saldoBegin += Convert.ToDecimal(dt.Rows[intX][3].ToString());
                        saldoEind += Convert.ToDecimal(dt.Rows[intX][4].ToString());
                    }

                }
            }

            Tekst += "                                           -------------   --------------   -------------\r\n";
            Tekst += string.Format("{0,-40}{1,16:N}{2,17:N}{3,16:N}", "Totaal:", saldoBegin, saldoEind, saldoEind - saldoBegin) + "\r\n";
            Tekst += "                                           =============   ==============   =============\r\n\r\n";

            DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(datum);
            sSQL = clsDataControle.saldoCodePerBank(datum1);
            dt = GenericDataRead.GetData(sSQL);
            Decimal saldo1 = 0;
            Decimal saldoSub = 0;
            var BankNummer = "";
            if (dt.Rows.Count > 0)
            {
                BankNummer = dt.Rows[0][2].ToString();
                for (int intX = 0; intX < dt.Rows.Count; intX++)
                {
                    if (BankNummer != dt.Rows[intX][2].ToString() && BankNummer.Length != 0)
                    {
                        Tekst +=
                            "                                                                            -------------\r\n";
                        Tekst += string.Format("{0,-40}{1,49:N}", "SubTotaal:", saldoSub) + "\r\n\r\n";
                        saldoSub = 0;
                        BankNummer = dt.Rows[intX][2].ToString();
                    }
                    Tekst += string.Format("{0,-18}{1,14}{2,7}{3,-34}{4,16:N}", dt.Rows[intX][3].ToString(), dt.Rows[intX][4].ToString(), dt.Rows[intX][0].ToString() + ": ", dt.Rows[intX][5], Convert.ToDecimal(dt.Rows[intX][1].ToString())) + "\r\n";
                    saldo1 += Convert.ToDecimal(dt.Rows[intX][1].ToString());
                    saldoSub += Convert.ToDecimal(dt.Rows[intX][1].ToString());
                }

                Tekst +=
                    "                                                                            -------------\r\n";
                Tekst += string.Format("{0,-40}{1,49:N}", "SubTotaal:", saldoSub) + "\r\n\r\n";

                Tekst += "                                                                            -------------\r\n";
                Tekst += string.Format("{0,-40}{1,49:N}", "Totaal:", saldo1) + "\r\n";
                Tekst += "                                                                            =============\r\n\r\n";
            }
            return Tekst;

            #endregion banksaldi
        }

    }
}
