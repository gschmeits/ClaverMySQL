using System;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using ClaverMySQL.Utils;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    ///     Interaction logic for pg_Boekhouding.xaml
    /// </summary>
    public partial class pg_Boekhouding : Page
    {
        public decimal dTekstBeginSaldo = 0;
        public decimal dTekstEindSaldo = 0;
        private string bladnummer = "01";
        private CalenderBackground background;

        public pg_Boekhouding()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var sSQL = clsDataBoekhouding.cmbJaartal();
            var dtA = GenericDataRead.GetData(sSQL);
            cmbBoekjaar.ItemsSource = dtA.DefaultView;
            cmbBoekjaar.DisplayMemberPath = "jaartal";
            cmbBoekjaar.SelectedValuePath = "jaartal";

            sSQL = clsDataBoekhouding.cmbMaand();
            dtA = GenericDataRead.GetData(sSQL);
            cmbBoekmaand.ItemsSource = dtA.DefaultView;
            cmbBoekmaand.SelectedValuePath = "maand";

            sSQL = clsDataBoekhouding.cmbBanken();
            dtA = GenericDataRead.GetData(sSQL);
            cmbBank.ItemsSource = dtA.DefaultView;
            cmbBank.SelectedValuePath = "BankID";
            sSQL = clsDataBoekhouding.HaalCodeGegevens();
            dtA = GenericDataRead.GetData(sSQL);
            cmbCode.ItemsSource = dtA.DefaultView;
            cmbCode.DisplayMemberPath = dtA.Columns[1].ToString();
            cmbCode.SelectedValuePath = "IDCode";

            sSQL = clsDataBoekhouding.HaalAdresNummers();
            dtA = GenericDataRead.GetData(sSQL);
            cmbAdres.ItemsSource = dtA.DefaultView;
            cmbAdres.DisplayMemberPath = "adresnummer";
            cmbAdres.SelectedValuePath = "adresnummer";

            sSQL = clsDataBoekhouding.HaalAdresNamen();
            dtA = GenericDataRead.GetData(sSQL);
            cmbNaam.ItemsSource = dtA.DefaultView;
            cmbNaam.DisplayMemberPath = "Naam";
            cmbNaam.SelectedValuePath = "adresnummer";

            sSQL = clsDataBoekhouding.HaalAdresPostcode();
            dtA = GenericDataRead.GetData(sSQL);
            cmbPostcode.ItemsSource = dtA.DefaultView;
            cmbPostcode.DisplayMemberPath = "postcode1";
            cmbPostcode.SelectedValuePath = "adresnummer";

            sSQL = clsDataBoekhouding.HaalAdresBanken();
            dtA = GenericDataRead.GetData(sSQL);
            cmbBankrekening.ItemsSource = dtA.DefaultView;
            cmbBankrekening.DisplayMemberPath = "bank";
            cmbBankrekening.SelectedValuePath = "adresnummer";

            sSQL = clsDataBoekhouding.HaalAdresGiros();
            dtA = GenericDataRead.GetData(sSQL);
            cmbGiro.ItemsSource = dtA.DefaultView;
            cmbGiro.DisplayMemberPath = "giro";
            cmbGiro.SelectedValuePath = "adresnummer";

            sSQL = clsDataBoekhouding.HaalAdresIBAN();
            dtA = GenericDataRead.GetData(sSQL);
            cmbIBAN.ItemsSource = dtA.DefaultView;
            cmbIBAN.DisplayMemberPath = "iban";
            cmbIBAN.SelectedValuePath = "adresnummer";

            cmbBoekjaar.SelectedValue = DateTime.Now.Year.ToString();
            cmbBoekmaand.SelectedValue = 0;
        }

        private void cmbBoekjaar_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbBoekjaar.SelectedIndex != -1 &&
                cmbBoekmaand.SelectedIndex > 0) MaakBankZichtbaar();
        }

        private void cmbBoekjaar_GotFocus(object sender, RoutedEventArgs e)
        {
            uitgangssitutie(false);
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
        }

        private void cmbBoekmaand_GotFocus(object sender, RoutedEventArgs e)
        {
            uitgangssitutie(false);
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
        }

        private void cmbBoekmaand_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbBoekjaar.SelectedIndex != -1 &&
                cmbBoekmaand.SelectedIndex > 0) MaakBankZichtbaar();
        }

        private void cmbBank_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbBank.SelectedIndex != -1)
            {
                dpDatum.IsEnabled = true;
                lblDatum.Visibility = Visibility.Visible;
                dpDatum.Visibility = Visibility.Visible;
                totaalOpNull();

                BijwerkenBoekData();
            }
            else
            {
                dpDatum.IsEnabled = false;
            }
        }

        private void cmbBank_IsVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (cmbBank.Visibility == Visibility.Visible)
                Keyboard.Focus(cmbBank);
        }

        private void cmbBank_GotFocus(object sender, RoutedEventArgs e)
        {
            docDdetails.Visibility = Visibility.Hidden;
            dgBoekingen.Visibility = Visibility.Hidden;
            dpDatum.SelectedDate = null;
            Kalender.Visibility = Visibility.Hidden;
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
        }

        private void dpDatum_SelectedDateChanged(object sender,
            SelectionChangedEventArgs e)
        {
            dpDatum.IsDropDownOpen = false;
            if (dpDatum.SelectedDate != null)
            {
                var sJaar = dpDatum.SelectedDate.Value.Year.ToString();
                var sMaand = dpDatum.SelectedDate.Value.Month.ToString();
                var sDag = dpDatum.SelectedDate.Value.Day.ToString();
                var sBank = cmbBank.SelectedValue.ToString();
                var sSQL =
                    clsDataBoekhouding.cmbHaalGegevensDag(sJaar, sMaand, sDag,
                        sBank);
                var dtA = GenericDataRead.GetData(sSQL);
                if (dtA.Rows.Count > 0)
                {
                    txtVolgnummer.Text = dtA.Rows[0][1].ToString();
                    txtBeginSaldo.Text = string.Format("{0:C}",
                        Convert.ToDecimal(dtA.Rows[0][2].ToString()));
                    txtEindSaldo.Text = string.Format("{0:C}",
                        Convert.ToDecimal(dtA.Rows[0][3].ToString()));
                    dTekstBeginSaldo =
                        Convert.ToDecimal(dtA.Rows[0][2].ToString());
                    dTekstEindSaldo =
                        Convert.ToDecimal(dtA.Rows[0][3].ToString());
                    lblTeBoeken.Content = string.Format("{0:C}",
                        Convert.ToDecimal(dtA.Rows[0][4].ToString()));
                    BerekenGeboekt();
                    sSQL = clsDataBoekhouding.cmbHaalGegevensDagDetails(sJaar,
                        sMaand, sDag, sBank);
                    dtA = GenericDataRead.GetData(sSQL);
                    if (dtA.Rows.Count > 0)
                    {
                        dgBoekingen.ItemsSource = dtA.DefaultView;
                        dgBoekingen.SelectedValuePath = "ID";
                    }
                }
                else
                {
                    sSQL = clsDataBoekhouding.HaalVolgnummer(sJaar, sMaand,
                        sDag, sBank);
                    dtA = GenericDataRead.GetData(sSQL);
                    if (dtA.Rows.Count > 0)
                    {
                        txtVolgnummer.Text = Convert.ToString(
                            Convert.ToInt32(dtA.Rows[0][0].ToString()) + 1);
                        txtBeginSaldo.Text = string.Format("{0:C}",
                            Convert.ToDecimal(dtA.Rows[0][1].ToString()));
                        txtEindSaldo.Text = string.Format("{0:C}",
                            Convert.ToDecimal(dtA.Rows[0][1].ToString()));
                        dTekstBeginSaldo =
                            Convert.ToDecimal(dtA.Rows[0][1].ToString());
                        dTekstEindSaldo =
                            Convert.ToDecimal(dtA.Rows[0][1].ToString());
                        BerekenTeBoeken();
                        var bank = cmbBank.SelectedValue.ToString();
                        var datum = dpDatum.SelectedDate.ToString();
                        var datumc = datum.Split('-');
                        datum = datumc[2].Substring(0, 4) + "-" + datumc[1] +
                                "-" + datumc[0];
                        var volgnummer = txtVolgnummer.Text;
                        var saldo = dtA.Rows[0][1].ToString();
                        clsDataBoekhouding.INSERT_mutaties_bank(bank, datum,
                            volgnummer, saldo.Replace(",", "."));
                    }

                    lblTeBoeken.Content = "€ 0,00";
                    dgBoekingen.ItemsSource = null;
                }

                lblVolgnummer.Visibility = Visibility.Visible;
                txtVolgnummer.Visibility = Visibility.Visible;
                lblBeginSaldo.Visibility = Visibility.Visible;
                txtBeginSaldo.Visibility = Visibility.Visible;
                lblEindSaldo.Visibility = Visibility.Visible;
                txtEindSaldo.Visibility = Visibility.Visible;
                lblNogBoeken.Visibility = Visibility.Visible;
                lblNogTeBoeken.Visibility = Visibility.Visible;
                lblTeBoeken.Visibility = Visibility.Visible;
                spGeboekt.Visibility = Visibility.Visible;
                spNogBoeken.Visibility = Visibility.Visible;
                dgBoekingen.Visibility = Visibility.Visible;

                Dispatcher.BeginInvoke(
                    (Action)delegate { Keyboard.Focus(txtVolgnummer); },
                    DispatcherPriority.Render);
                docDdetails.Visibility = Visibility.Visible;
            }
            else
            {
                lblVolgnummer.Visibility = Visibility.Hidden;
                txtVolgnummer.Visibility = Visibility.Hidden;
                lblBeginSaldo.Visibility = Visibility.Hidden;
                txtBeginSaldo.Visibility = Visibility.Hidden;
                lblEindSaldo.Visibility = Visibility.Hidden;
                txtEindSaldo.Visibility = Visibility.Hidden;
                lblNogBoeken.Visibility = Visibility.Hidden;
                lblNogTeBoeken.Visibility = Visibility.Hidden;
                lblTeBoeken.Visibility = Visibility.Hidden;
                spGeboekt.Visibility = Visibility.Hidden;
                spNogBoeken.Visibility = Visibility.Hidden;
                dgBoekingen.Visibility = Visibility.Hidden;

                Dispatcher.BeginInvoke(
                    (Action)delegate { Keyboard.Focus(cmbBank); },
                    DispatcherPriority.Render);
                docDdetails.Visibility = Visibility.Hidden;
            }
        }

        private void dpDatum_KeyDown(object sender, KeyEventArgs e)
        {
            dpDatum.IsDropDownOpen = false;
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                if (dpDatum.Text != "")
                    Dispatcher.BeginInvoke(
                        (Action)delegate { Keyboard.Focus(txtVolgnummer); },
                        DispatcherPriority.Render);
                else
                    Dispatcher.BeginInvoke(
                        (Action)delegate { Keyboard.Focus(dpDatum); },
                        DispatcherPriority.Render);
            }

            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.Back:
                case Key.OemMinus:
                case Key.Subtract:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void dpDatum_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            dpDatum.IsDropDownOpen = false;
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    if (dpDatum.Text != "")
                    {
                        Keyboard.Focus(txtVolgnummer);
                        docDdetails.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        dpDatum.Focus();
                    }
                }
            }
        }

        private void dpDatum_DataContextChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            dpDatum.IsDropDownOpen = false;
            totaalOpNull();
            Dispatcher.BeginInvoke((Action)delegate
            {
                if (dpDatum.SelectedDate == null)
                {
                    Keyboard.Focus(cmbBank);
                }
                else
                {
                    if (txtVolgnummer.Text.Length == 0)
                        Keyboard.Focus(txtVolgnummer);
                    else
                        Keyboard.Focus(txtBlad);
                }
            }, DispatcherPriority.Render);
            docDdetails.Visibility = Visibility.Visible;
        }

        private void DpDatum_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Kalender.Visibility = Visibility;
            Kalender.Visibility = Visibility.Visible;
            lblVolgnummer.Visibility = Visibility.Hidden;
            txtVolgnummer.Visibility = Visibility.Hidden;


            lblBeginSaldo.Visibility = Visibility.Hidden;
            txtBeginSaldo.Visibility = Visibility.Hidden;

            lblEindSaldo.Visibility = Visibility.Hidden;
            txtEindSaldo.Visibility = Visibility.Hidden;

            lblNogBoeken.Visibility = Visibility.Hidden;
            lblNogTeBoeken.Visibility = Visibility.Hidden;
            lblTeBoeken.Visibility = Visibility.Hidden;
            spGeboekt.Visibility = Visibility.Hidden;
            spNogBoeken.Visibility = Visibility.Hidden;
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;

            dgBoekingen.Visibility = Visibility.Hidden;
            ChangeCalendarButtonVisibility(Visibility.Visible);
            Kalender.Visibility = Visibility.Visible;
            Kalender.Focus();
            dpDatum.IsDropDownOpen = false;
        }

        private void DpDatum_PreviewMouseDown(object sender,
            MouseButtonEventArgs e)
        {
            BijwerkenBoekData();
            WerkDataBij();
            Kalender.Visibility = Visibility.Visible;
            lblVolgnummer.Visibility = Visibility.Hidden;
            txtVolgnummer.Visibility = Visibility.Hidden;
            lblBeginSaldo.Visibility = Visibility.Hidden;
            txtBeginSaldo.Visibility = Visibility.Hidden;
            lblEindSaldo.Visibility = Visibility.Hidden;
            txtEindSaldo.Visibility = Visibility.Hidden;
            lblNogBoeken.Visibility = Visibility.Hidden;
            lblNogTeBoeken.Visibility = Visibility.Hidden;
            lblTeBoeken.Visibility = Visibility.Hidden;
            spGeboekt.Visibility = Visibility.Hidden;
            spNogBoeken.Visibility = Visibility.Hidden;
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
            dgBoekingen.Visibility = Visibility.Hidden;
            ChangeCalendarButtonVisibility(Visibility.Visible);
            Kalender.Visibility = Visibility.Visible;
            Kalender.Focus();
            dpDatum.IsDropDownOpen = false;
        }

        private void DpDatum_GotFocus(object sender, RoutedEventArgs e)
        {
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
        }

        private void Kalender_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            dpDatum.SelectedDate = Kalender.SelectedDate;
            Kalender.Visibility = Visibility.Hidden;
            txtEindSaldo.Focusable = true;
            Keyboard.Focus(txtEindSaldo);
        }

        private void Kalender_GotFocus(object sender, RoutedEventArgs e)
        {
            Nieuw();
            BijwerkenBoekData();
            if (cmbBoekjaar.SelectedIndex != -1 &&
                cmbBoekmaand.SelectedIndex != -1) MaakBankZichtbaar();
            dgBoekingen.ItemsSource = null;
        }

        private void Kalender_IsVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (Kalender.Visibility == Visibility.Visible)
            {
                dpDatum.IsDropDownOpen = false;
                BijwerkenBoekData();
                docDdetails.Visibility = Visibility.Hidden;
                dgBoekingen.Visibility = Visibility.Hidden;
                lblVolgnummer.Visibility = Visibility.Hidden;
                lblBeginSaldo.Visibility = Visibility.Hidden;
                lblEindSaldo.Visibility = Visibility.Hidden;
                lblNogTeBoeken.Visibility = Visibility.Hidden;
                spGeboekt.Visibility = Visibility.Hidden;
                txtBeginSaldo.Visibility = Visibility.Hidden;
                txtEindSaldo.Visibility = Visibility.Hidden;
                lblTeBoeken.Visibility = Visibility.Hidden;
                lblTeBoeken.Visibility = Visibility.Hidden;
                lblNogBoeken.Visibility = Visibility.Hidden;
                txtVolgnummer.Visibility = Visibility.Hidden;
                StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
            }
            else
            {
                docDdetails.Visibility = Visibility.Visible;
                dgBoekingen.Visibility = Visibility.Visible;
                lblVolgnummer.Visibility = Visibility.Visible;
                lblBeginSaldo.Visibility = Visibility.Visible;
                lblEindSaldo.Visibility = Visibility.Visible;
                lblNogTeBoeken.Visibility = Visibility.Visible;
                spGeboekt.Visibility = Visibility.Visible;
                txtBeginSaldo.Visibility = Visibility.Visible;
                txtEindSaldo.Visibility = Visibility.Visible;
                lblTeBoeken.Visibility = Visibility.Visible;
                lblTeBoeken.Visibility = Visibility.Visible;
                lblNogBoeken.Visibility = Visibility.Visible;
                txtVolgnummer.Visibility = Visibility.Visible;
                StackPanelAantalBedankjes.Visibility = Visibility.Visible;
            }
        }

        private void txtVolgnummer_GotFocus(object sender, RoutedEventArgs e)
        {
            txtVolgnummer.SelectAll();
        }

        private void txtVolgnummer_IsVisibleChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (txtVolgnummer.Visibility == Visibility.Visible &&
                dpDatum.Text != "")
            {
                Dispatcher.BeginInvoke(
                    (Action)delegate { Keyboard.Focus(txtVolgnummer); },
                    DispatcherPriority.Render);
                Keyboard.Focus(txtVolgnummer);
            }
            else
            {
                Dispatcher.BeginInvoke(
                    (Action)delegate { Keyboard.Focus(dpDatum); },
                    DispatcherPriority.Render);
                Keyboard.Focus(dpDatum);
            }
        }

        private void txtBeginSaldo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBeginSaldo.SelectAll();
        }

        private void txtBeginSaldo_GotKeyboardFocus(object sender,
            KeyboardFocusChangedEventArgs e)
        {
            txtBeginSaldo.SelectAll();
        }

        private void txtBeginSaldo_GotMouseCapture(object sender,
            MouseEventArgs e)
        {
            txtBeginSaldo.SelectAll();
        }

        private void txtBeginSaldo_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void txtBeginSaldo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBeginSaldo.Text.Length == 0) txtBeginSaldo.Text = "€ 0,00";

            var sTekst = txtBeginSaldo.Text.Replace("€", "");
            txtBeginSaldo.Text.Replace("€", "");
            txtBeginSaldo.Text.Replace(",", ".");
            sTekst.Replace("€", "");
            var sTekst2 = sTekst;
            var sTekst1 = sTekst2.Replace(".", ",");
            var dTekst2 = Convert.ToDecimal(sTekst2);
            dTekstBeginSaldo = dTekst2;
            txtBeginSaldo.Text =
                string.Format("{0:C}", Convert.ToDecimal(dTekst2));
            BerekenTeBoeken();
            Dispatcher.BeginInvoke(
                (Action)delegate { Keyboard.Focus(txtEindSaldo); },
                DispatcherPriority.Render);
        }

        private void txtBeginSaldo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(txtEindSaldo);
                }
            }
        }

        private void txtEindSaldo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtEindSaldo.SelectAll();
        }

        private void txtEindSaldo_GotKeyboardFocus(object sender,
            KeyboardFocusChangedEventArgs e)
        {
            txtEindSaldo.SelectAll();
        }

        private void txtEindSaldo_GotMouseCapture(object sender,
            MouseEventArgs e)
        {
            txtEindSaldo.SelectAll();
        }

        private void txtEindSaldo_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void txtEindSaldo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtEindSaldo.Text.Length == 0) txtEindSaldo.Text = "€ 0,00";
            var sTekst = txtEindSaldo.Text.Replace("€", "");
            txtEindSaldo.Text.Replace("€", "");
            txtEindSaldo.Text.Replace(",", ".");
            sTekst.Replace("€", "");
            var sTekst2 = sTekst;
            var sTekst1 = sTekst2.Replace(".", ",");
            var dTekst2 = Convert.ToDecimal(sTekst2);
            dTekstEindSaldo = dTekst2;
            txtEindSaldo.Text =
                string.Format("{0:C}", Convert.ToDecimal(dTekst2));
            BerekenTeBoeken();
            Dispatcher.BeginInvoke(
                (Action)delegate { Keyboard.Focus(txtVolgnummer); },
                DispatcherPriority.Render);
        }

        private void txtEindSaldo_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(txtVolgnummer);
                }
            }
        }

        private void cmbCode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
                Dispatcher.BeginInvoke(
                    (Action)delegate { Keyboard.Focus(txtBedrag); },
                    DispatcherPriority.Render);
        }

        private void cmbCode_LostFocus(object sender, RoutedEventArgs e)
        {
            subcodeUitvoer();
        }

        private void cmbCode_DataContextChanged(object sender,
            DependencyPropertyChangedEventArgs e)
        {
            if (cmbCode.SelectedIndex != -1 && txtBedrag.Text.Length > 0)
            {
                btnOpslaan.IsEnabled = true;
            }
            else
            {
                btnOpslaan.IsEnabled = false;
            }
        }

        private void txtBlad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbCode.Focusable = true;
                Keyboard.Focus(cmbCode);
            }
        }

        private void txtBlad_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBlad.SelectAll();
        }

        private void cmbSubdoe_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbSubdoe.SelectedIndex != -1)
            {
                txtOmschrijving.Text = cmbSubdoe.Text;
            }
        }

        private void cmbSubdoe_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbSubdoe.SelectedIndex != -1)
            {
                txtOmschrijving.Text = cmbSubdoe.Text;
            }
        }

        private void txtBedrag_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBedrag.SelectAll();
        }

        private void txtBedrag_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void txtBedrag_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtBedrag.Text.Length == 0)
            {
                txtBedrag.Text = "€ 0,00";
                txtBedrag1.Text = "0";

            }

            var sTekst = txtBedrag.Text.Replace("€", "");
            txtBedrag.Text.Replace("€", "");
            txtBedrag.Text.Replace(",", ".");
            sTekst.Replace("€", "");
            var sTekst2 = sTekst;
            var sTekst1 = sTekst2.Replace(".", ",");
            var dTekst2 = Convert.ToDecimal(sTekst2);
            dTekstEindSaldo = dTekst2;
            txtBedrag.Text = string.Format("{0:C}", Convert.ToDecimal(dTekst2));
            txtBedrag1.Text = Convert.ToString(dTekst2);
            CheckDank(dTekst2);
        }

        private void txtBedrag_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;

                if (grNaam.Visibility == Visibility.Visible)
                {
                    chkDank.IsEnabled = true;
                    Keyboard.Focus(cmbAdres);
                }
                else
                {
                    Keyboard.Focus(txtOmschrijving);
                }
            }

            if (cmbCode.SelectedIndex != -1 && txtBedrag.Text.Length > 0)
            {
                btnOpslaan.IsEnabled = true;
            }
            else
            {
                btnOpslaan.IsEnabled = false;
            }
        }

        private void txtOmschrijving_PreviewKeyDown(object sender,
            KeyEventArgs e)
        {
            if (cmbCode.SelectedIndex != -1 && txtBedrag.Text.Length > 0)
            {
                btnOpslaan.IsEnabled = true;
            }
            else
            {
                btnOpslaan.IsEnabled = false;
            }
        }

        private void txtOmschrijving_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                txtOpmerking.Focusable = true;
                Keyboard.Focus(txtOpmerking);
            }
        }

        private void txtOpmerking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                chkDank.Focusable = true;
                Keyboard.Focus(chkDank);
            }
        }

        private void chkDank_Click(object sender, RoutedEventArgs e)
        {
            cmbAdres.Focusable = true;
            Keyboard.Focus(cmbAdres);
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            if (txtBedrag.Text != "€ 0,00")
            {
                var sSQL = "";
                var naamSelectie = "0";
                var subdoe = "0";
                if (cmbSubdoe.SelectedValue != null)
                {
                    subdoe = cmbSubdoe.SelectedValue.ToString();
                }

                if (cmbNaam.SelectedValue != null)
                {
                    naamSelectie = cmbNaam.SelectedValue.ToString();
                }

                bool dank = chkDank.IsChecked == true;

                if (cmbCode.Text.Length > 0 && txtBedrag1.Text.Length > 0 && txtBlad.Text.Length > 0)
                {
                    if (txtID.Text == "0")
                    {
                        // INSERT
                        sSQL = clsDataBoekhouding.INSERTBoeking(naamSelectie, cmbBank.SelectedValue.ToString(),
                            dpDatum.Text, txtVolgnummer.Text, txtBlad.Text, txtBedrag1.Text, txtOpmerking.Text,
                            cmbCode.Text.Substring(0, 3), subdoe, txtNaambetaler.Text, cmbSubdoe.Text,
                            txtOmschrijving.Text, "0", dank);
                    }
                    else
                    {
                        // UPDATE
                        sSQL = clsDataBoekhouding.UPDATEBoeking(txtID.Text, naamSelectie,
                            cmbBank.SelectedValue.ToString(), txtVolgnummer.Text, txtBlad.Text, txtBedrag1.Text,
                            txtOpmerking.Text, cmbCode.Text.Substring(0, 3), subdoe, txtNaambetaler.Text,
                            cmbSubdoe.Text, txtOmschrijving.Text, "0", dank);
                    }

                    GenericDataRead.INUPDEL(sSQL);

                    BerekenGeboekt();
                    string sJaar = dpDatum.SelectedDate.Value.Year.ToString();
                    string sMaand = dpDatum.SelectedDate.Value.Month.ToString();
                    string sDag = dpDatum.SelectedDate.Value.Day.ToString();
                    var sBank = cmbBank.SelectedValue.ToString();
                    sSQL = clsDataBoekhouding.cmbHaalGegevensDag(sJaar, sMaand, sDag, sBank);
                    DataTable dtA = GenericDataRead.GetData(sSQL);
                    sSQL = clsDataBoekhouding.cmbHaalGegevensDagDetails(sJaar, sMaand, sDag, sBank);
                    dtA = GenericDataRead.GetData(sSQL);
                    if (dtA.Rows.Count > 0)
                    {
                        dgBoekingen.ItemsSource = dtA.DefaultView;
                        dgBoekingen.SelectedValuePath = "ID";
                    }

                    bladnummer = txtBlad.Text;
                    Nieuw();
                    // Controle bedankjes (geef melding als er meer dan het ingeselde aantal zijn)
                    sSQL = clsDataBoekhouding.controleBedankjes();
                    dtA = GenericDataRead.GetData(sSQL);
                    var aantal = Convert.ToInt32(Functions.sAantalBedankjes("ConfigData.xml"));
                    TextBlockAantalBedankjes.Text = aantal.ToString();

                    if (dtA.Rows.Count >= aantal)
                    {
                        var uitleg =
                            "Vanaf het moment dat de laatste keer de bedankjes gegenereerd zijn (middels Printen -> Dank -> Globaal) zijn er " +
                            aantal + " of meer boekingen gedaan, waar nog geen bedankje voor aangemaakt is.";
                        MessageBox.Show(
                            "Er zijn op dit moment " + dtA.Rows.Count.ToString() +
                            " mutaties met bedankje, waarvan nog geen bedankje is aangemaakt.", "Aantal bedankjes",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    naamLegen();
                }
                else
                {
                    MessageBox.Show(
                        "Bladnummer, code of bedrag dienen een waarde te hebben voordat ze opgeslagen kunnen worden.",
                        "Foutmelding", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Het te boeken bedrag mag niet 0 zijn.", "Waarschuwing", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                txtBedrag.Focusable = true;
                Keyboard.Focus(txtBedrag);
            }
        }

        private void btnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            var caption = "Verwijderen boeking";
            var message = "Weet u zeker dat u deze boeking wilt verwijderen?";
            var __result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (__result == MessageBoxResult.Yes)
            {
                message = "Boeking is verwijderd!!!";
                var strQue = clsDataBoekhouding.DELETEBoeking(txtID.Text);
                GenericDataRead.INUPDEL(strQue);
                BerekenGeboekt();
                string sJaar = dpDatum.SelectedDate.Value.Year.ToString();
                string sMaand = dpDatum.SelectedDate.Value.Month.ToString();
                string sDag = dpDatum.SelectedDate.Value.Day.ToString();
                var sBank = cmbBank.SelectedValue.ToString();
                var sSQL = clsDataBoekhouding.cmbHaalGegevensDagDetails(sJaar, sMaand, sDag, sBank);
                DataTable dtA = GenericDataRead.GetData(sSQL);
                if (dtA.Rows.Count > 0)
                {
                    dgBoekingen.ItemsSource = dtA.DefaultView;
                    dgBoekingen.SelectedValuePath = "id";
                }

                Nieuw();
                MessageBox.Show(message, caption, MessageBoxButton.OK);
            }
            else
            {
                message = "Boeking is niet verwijderd!!!";
                MessageBox.Show(message, caption, MessageBoxButton.OK);
            }

            btnVerwijderen.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnNiew_Click(object sender, RoutedEventArgs e)
        {
            Nieuw();
        }

        private void cmbAdres_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbAdres.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbAdres.SelectedValue.ToString());
            }
        }

        private void cmbAdres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbNaam.Focusable = true;
                Keyboard.Focus(cmbNaam);
            }
        }

        private void cmbAdres_LostFocus(object sender, RoutedEventArgs e)
        {
            if (cmbAdres.Text != String.Empty)
            {
                var sSQL = "SELECT Dank FROM tbl_Adressen WHERE adresnummer = " + cmbAdres.Text;
                var db = GenericDataRead.GetData(sSQL);
                if (db.Rows.Count > 0)
                {
                    if (db.Rows[0][0].ToString() == "False")
                    {
                        chkDank.IsChecked = false;
                    }
                }
            }
        }

        private void cmbAdres_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbAdres.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbAdres.SelectedValue.ToString());
                }
            }
        }

        private void cmbNaam_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbNaam.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbNaam.SelectedValue.ToString());
            }
        }

        private void cmbNaam_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbNaam.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbNaam.SelectedValue.ToString());
                }
            }
        }

        private void cmbNaam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbPostcode.Focusable = true;
                Keyboard.Focus(cmbPostcode);
            }
        }

        private void cmbBankrekening_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbBankrekening.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbBankrekening.SelectedValue.ToString());
            }
        }

        private void cmbBankrekening_PreviewKeyDown(object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbBankrekening.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbBankrekening.SelectedValue.ToString());
                }
            }
        }

        private void cmbBankrekening_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbGiro.Focusable = true;
                Keyboard.Focus(cmbGiro);
            }
        }

        private void cmbPostcode_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbPostcode.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbPostcode.SelectedValue.ToString());
            }
        }

        private void cmbPostcode_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbPostcode.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbPostcode.SelectedValue.ToString());
                }
            }
        }

        private void cmbPostcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbBankrekening.Focusable = true;
                Keyboard.Focus(cmbBankrekening);
            }
        }

        private void cmbGiro_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbGiro.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbGiro.SelectedValue.ToString());
            }
        }

        private void cmbGiro_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbGiro.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbGiro.SelectedValue.ToString());
                }
            }
        }

        private void cmbGiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                cmbIBAN.Focusable = true;
                Keyboard.Focus(cmbIBAN);
            }
        }

        private void CmbIBAN_KeyDown(object sender, KeyEventArgs e)
        {
            cmbAdres.Focusable = true;
            Keyboard.Focus(cmbAdres);
        }

        private void CmbIBAN_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab || e.Key == Key.Return)
            {
                if (cmbIBAN.SelectedIndex != -1)
                {
                    VulAdresGegevens(cmbIBAN.SelectedValue.ToString());
                }
            }
        }

        private void CmbIBAN_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbIBAN.SelectedIndex != -1)
            {
                VulAdresGegevens(cmbIBAN.SelectedValue.ToString());
            }
        }

        private void dgBoekingen_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            if (dgBoekingen.SelectedIndex != -1)
            {
                cmbSubdoe.SelectedIndex = -1;
                var sSQL = clsDataBoekhouding.HaalGridDetails(dgBoekingen.SelectedValue.ToString());
                DataTable dt = new DataTable();
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    ///////////////////////////////////////////////////////////////////
                    // Gegevens tonen om te kijken welke waarde een veld teruggeeft. //
                    ///////////////////////////////////////////////////////////////////
                    // MessageBox.Show(dt.Rows[0][9].ToString());
                    ///////////////////////////////////////////////////////////////////
                    chkDank.IsEnabled = true;
                    TextBlockUitleg.Visibility = Visibility.Hidden;
                    txtID.Text = dt.Rows[0][0].ToString();
                    cmbCode.Text = dt.Rows[0][1].ToString();
                    txtBedrag.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0][4].ToString()));
                    txtBedrag1.Text = dt.Rows[0][4].ToString();
                    cmbSubdoe.SelectedValue = dt.Rows[0][5].ToString();
                    txtOmschrijving.Text = dt.Rows[0][6].ToString();
                    txtOpmerking.Text = dt.Rows[0][7].ToString();

                    var waar = true;

                    if (dt.Rows[0][9].ToString() == "True" || dt.Rows[0][9].ToString() == "1")
                    {
                        waar = !(dt.Rows[0][8].ToString() == "0");
                        chkDank.IsChecked = waar;
                        chkDank.IsEnabled = true;
                        chkDank.Visibility = System.Windows.Visibility.Visible;
                        lblDank.Visibility = System.Windows.Visibility.Visible;
                        grNaam.Visibility = System.Windows.Visibility.Visible;
                        txtNaambetaler.Visibility = System.Windows.Visibility.Visible;
                        lblNaamBetaler.Visibility = System.Windows.Visibility.Visible;
                        cmbAdres.SelectedValue = dt.Rows[0][2].ToString();
                        VulAdresGegevens(cmbAdres.SelectedValue.ToString());
                    }
                    else
                    {
                        chkDank.Visibility = System.Windows.Visibility.Hidden;
                        lblDank.Visibility = System.Windows.Visibility.Hidden;
                        grNaam.Visibility = System.Windows.Visibility.Hidden;
                        txtNaambetaler.Visibility = System.Windows.Visibility.Hidden;
                        lblNaamBetaler.Visibility = System.Windows.Visibility.Hidden;
                    }

                    if (waar == true || dt.Rows[0][8].ToString() == "1")
                    {
                        var bedrag = Convert.ToDouble(Functions.sBedragBedankjes("ConfigData.xml"));
                        if (bedrag > Convert.ToDouble(dt.Rows[0][4].ToString()))
                        {
                            chkDank.IsChecked = false;
                            chkDank.IsEnabled = true;
                        }
                        else
                        {
                            chkDank.IsChecked = true;
                            chkDank.IsEnabled = true;
                        }
                        TextBlockUitleg.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        chkDank.IsChecked = false;
                        chkDank.IsEnabled = false;
                        TextBlockUitleg.Visibility = Visibility.Visible;
                    }

                    if (dt.Rows[0][5].ToString() != "0")
                    {
                        var sSQL1 = clsDataBoekhouding.HaalSubCode(dt.Rows[0][13].ToString());
                        DataTable dtS = GenericDataRead.GetData(sSQL1);
                        cmbSubdoe.ItemsSource = null;
                        cmbSubdoe.ItemsSource = dtS.DefaultView;
                        cmbSubdoe.SelectedValuePath = "ID_SubCode";
                        cmbSubdoe.DisplayMemberPath = "SumOms";
                        lblSubCode.Visibility = System.Windows.Visibility.Visible;
                        cmbSubdoe.SelectedValue = dt.Rows[0][5].ToString();
                        cmbSubdoe.Refresh();
                        cmbSubdoe.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        lblSubCode.Visibility = System.Windows.Visibility.Hidden;
                        cmbSubdoe.Visibility = System.Windows.Visibility.Hidden;
                    }

                    btnOpslaan.IsEnabled = true;
                    btnVerwijderen.Visibility = System.Windows.Visibility.Visible;
                    btnNiew.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            dpDatum.IsDropDownOpen = true;
        }

        private void uitgangssitutie(bool bVis)
        {
            if (bVis == false)
            {
                docBankSel.Visibility = Visibility.Hidden;
                docDdetails.Visibility = Visibility.Hidden;
                dgBoekingen.Visibility = Visibility.Hidden;
                Kalender.Visibility = Visibility.Hidden;
            }
            else
            {
                docBankSel.Visibility = Visibility.Visible;
                docDdetails.Visibility = Visibility.Visible;
                dgBoekingen.Visibility = Visibility.Visible;
            }

            cmbBank.SelectedIndex = -1;
            totaalOpNull();
        }

        private void totaalOpNull()
        {
            txtBeginSaldo.Text = "€ 0,00";
            txtEindSaldo.Text = "€ 0,00";
            txtVolgnummer.Text = "";
            lblGeboekt.Content = "€ 0,00";
            lblNogBoeken.Content = "€ 0,00";
        }

        private void MaakBankZichtbaar()
        {
            var sJaar = cmbBoekjaar.SelectedValue.ToString();
            var sMaand = cmbBoekmaand.SelectedValue.ToString();

            if (sMaand == "0")
            {
                sMaand = "1";
                sJaar = (Convert.ToInt16(sJaar) - 1).ToString();
            }
            docBankSel.Visibility = Visibility.Visible;
            dpDatum.DisplayDateStart = new DateTime(Convert.ToInt32(sJaar),
                Convert.ToInt32(sMaand), 1);
            dpDatum.DisplayDateEnd = new DateTime(Convert.ToInt32(sJaar),
                Convert.ToInt32(sMaand),
                DateTime.DaysInMonth(Convert.ToInt32(sJaar),
                    Convert.ToInt32(sMaand)));
            dpDatum.SelectedDate = null;
            Dispatcher.BeginInvoke(
                (Action)delegate { Keyboard.Focus(cmbBankrekening); },
                DispatcherPriority.Render);
            StackPanelAantalBedankjes.Visibility = Visibility.Hidden;
        }

        private void BijwerkenBoekData()
        {
            DatumZettenHalen.sDag = 1;
            DatumZettenHalen.sJaar =
                Convert.ToInt16(cmbBoekjaar.SelectedValue.ToString());
            DatumZettenHalen.sMaand =
                Convert.ToInt16(cmbBoekmaand.SelectedValue.ToString());
            DatumZettenHalen.sBank = cmbBank.SelectedValue.ToString();

            var sBank = DatumZettenHalen.sBank;
            var sJaar = DatumZettenHalen.sJaar;
            var sMaand = DatumZettenHalen.sMaand;

            if (sMaand == 0)
            {
                sMaand = 1;
                sJaar = (short)(sJaar - 1);
            }

            var daysAMonth = 1;
            if (DatumZettenHalen.sMaand != 0)
                daysAMonth = DateTime.DaysInMonth(sJaar, sMaand);
            
            Kalender.IsTodayHighlighted = false;
            Kalender.DisplayDateStart = new DateTime(sJaar, sMaand, 1);
            Kalender.DisplayDateEnd = new DateTime(sJaar, sMaand, daysAMonth);
            if (cmbBoekjaar.SelectedIndex != -1 &&
                cmbBoekmaand.SelectedIndex != -1)
                MaakBankZichtbaar();
            if (Kalender.IsVisible == false)
                Kalender.Visibility = Visibility.Visible;

            try
            {
                background = new CalenderBackground(Kalender);
                background.AddOverlay("circle", "Images\\circle.png");
                background.AddOverlay("tjek", "Images\\tjek.png");
                background.AddOverlay("cross", "Images\\cross.png");
                background.AddOverlay("box", "Images\\box.png");
                background.AddOverlay("gray", "Images\\gray.png");

                WerkDataBij();
                background.grayoutweekends = "gray";
                Kalender.Background = background.GetBackground();
                Kalender.DisplayDateChanged += KalenderOnDisplayDateChanged;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n" + e.StackTrace, "Error");
            }
        }

        private void WerkDataBij()
        {
            var sBank = DatumZettenHalen.sBank;
            var sJaar = DatumZettenHalen.sJaar;
            var sMaand = DatumZettenHalen.sMaand;

            var sSQL = clsDataBoekhouding.HaalGegevensBankPerMaand(sBank, sJaar, sMaand);
            var dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows.Count > 0)
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var datum = new DateTime(
                        Convert.ToDateTime(dt.Rows[i][0].ToString()).Year,
                        Convert.ToDateTime(dt.Rows[i][0].ToString()).Month,
                        Convert.ToDateTime(dt.Rows[i][0].ToString()).Day);

                    background.AddDate(datum, "tjek");
                }

            dpDatum.IsDropDownOpen = false;
        }

        private void KalenderOnDisplayDateChanged(
            object sender,
            CalendarDateChangedEventArgs calendarDateChangedEventArgs)
        {
            Kalender.Background = background.GetBackground();
        }

        private void BerekenTeBoeken()
        {
            var dBegin = dTekstBeginSaldo;
            var dEind = dTekstEindSaldo;
            var dGeboekt =
                Convert.ToDecimal(
                    lblGeboekt.Content.ToString().Replace("€", ""));
            lblTeBoeken.Content = string.Format("{0:C}", dEind - dBegin);
            lblNogBoeken.Content =
                string.Format("{0:c}", dEind - dBegin - dGeboekt);
            UpdateBankMutaties();
        }

        private void BerekenGeboekt()
        {
            var sBank = cmbBank.SelectedValue.ToString();
            var sJaar = dpDatum.SelectedDate.Value.Year.ToString();
            var sMaand = dpDatum.SelectedDate.Value.Month.ToString();
            var sDag = dpDatum.SelectedDate.Value.Day.ToString();
            var decTeBoeken =
                Convert.ToDecimal(txtEindSaldo.Text.Replace("€", "")) -
                Convert.ToDecimal(txtBeginSaldo.Text.Replace("€", ""));
            var sSQL = clsDataBoekhouding.sGeboekt(sJaar, sMaand, sDag, sBank);
            var dt = new DataTable();
            dt = GenericDataRead.GetData(sSQL);
            if (dt.Rows[0][0].ToString().Length > 0)
            {
                lblGeboekt.Content = string.Format("{0:C}",
                    Convert.ToDecimal(dt.Rows[0][0].ToString()));
                lblNogBoeken.Content =
                    string.Format("{0:C}",
                        decTeBoeken -
                        Convert.ToDecimal(dt.Rows[0][0].ToString()));

                UpdateBankMutaties();
            }
            else
            {
                lblGeboekt.Content = "€ 0,00";
                txtBlad.Text = "01";
            }
        }

        private void UpdateBankMutaties()
        {
            var sBank = cmbBank.SelectedValue.ToString();
            var decTeBoeken =
                Convert.ToDecimal(txtEindSaldo.Text.Replace("€", "")) -
                Convert.ToDecimal(txtBeginSaldo.Text.Replace("€", ""));
            var bank = cmbBank.SelectedValue.ToString();
            var datum = dpDatum.SelectedDate.ToString();
            var volgnummer = txtVolgnummer.Text;
            var beginsaldo = txtBeginSaldo.Text;
            var eindsaldo = txtEindSaldo.Text;
            var mutaties = lblNogTeBoeken.Content;
            clsDataBoekhouding.UPDATE_mutaties_bank(sBank, txtVolgnummer.Text,
                Convert.ToDecimal(txtBeginSaldo.Text.Replace("€", ""))
                    .ToString().Replace(",", "."),
                Convert.ToDecimal(txtEindSaldo.Text.Replace("€", "")).ToString()
                    .Replace(",", "."),
                decTeBoeken.ToString().Replace(",", "."));
        }

        private Button GetCalendarButton()
        {
            var template = dpDatum.Template;
            var button = (Button)template.FindName("PART_Button", dpDatum);
            return button;
        }

        private void ChangeCalendarButtonVisibility(Visibility visibility)
        {
            var button = GetCalendarButton();
            if (button != null) button.IsEnabled = false;
        }

        private void DecimaleInvoer(object sender, KeyEventArgs e)
        {
            var ch = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            switch (e.Key)
            {
                case Key.Decimal:
                case Key.OemPeriod:
                    var box = sender as TextBox;
                    var text = box.Text;
                    var caret = box.CaretIndex;
                    var urdu = ",";
                    box.Text = text.Insert(caret, urdu);
                    box.CaretIndex = caret + urdu.Length;
                    e.Handled = true;
                    break;
                case Key.OemComma:
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                case Key.NumLock:
                case Key.NumPad0:
                case Key.NumPad1:
                case Key.NumPad2:
                case Key.NumPad3:
                case Key.NumPad4:
                case Key.NumPad5:
                case Key.NumPad6:
                case Key.NumPad7:
                case Key.NumPad8:
                case Key.NumPad9:
                case Key.OemMinus:
                case Key.Subtract:
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void Nieuw()
        {
            grNaam.Visibility = Visibility.Hidden;
            txtBlad.Text = bladnummer;
            cmbCode.SelectedIndex = -1;
            TextBlockUitleg.Visibility = Visibility.Hidden;
            txtBedrag.Text = "€ 0,00";
            txtBedrag1.Text = "0";
            txtID.Text = "0";
            txtOmschrijving.Text = "";
            txtOpmerking.Text = "";
            txtNaambetaler.Text = "";
            chkDank.IsChecked = false;
            btnNiew.Visibility = Visibility.Hidden;
            btnOpslaan.IsEnabled = false;
            lblSubCode.Visibility = Visibility.Hidden;
            cmbSubdoe.Visibility = Visibility.Hidden;
            cmbSubdoe.ItemsSource = null;
            txtNaambetaler.Visibility = Visibility.Hidden;
            lblNaamBetaler.Visibility = Visibility.Hidden;
            btnNiew.Visibility = Visibility.Hidden;
            btnVerwijderen.Visibility = Visibility.Hidden;
            brdAdres.Visibility = Visibility.Hidden;
            cmbCode.Focusable = true;
            cmbAdres.SelectedIndex = -1;
            cmbNaam.SelectedIndex = -1;
            cmbPostcode.SelectedIndex = -1;
            cmbBankrekening.SelectedIndex = -1;
            cmbGiro.SelectedIndex = -1;
            cmbIBAN.SelectedIndex = 1;
            lblNaamDetail.Content = "";
            lblAdresDetail.Content = "";
            lblPCPlaatsDetail.Content = "";
            Keyboard.Focus(cmbCode);
        }
        
        private void subcodeUitvoer()
        {
            if (cmbCode.SelectedIndex != -1)
            {
                chkDank.IsEnabled = true;
                var sCode = cmbCode.Text.Split(' ');
                var sSQL = clsDataBoekhouding.HaalCodeIndividueel(sCode[0]);
                var dt = new DataTable();
                dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][6].ToString().ToUpper() == "TRUE" ||
                        dt.Rows[0][5].ToString() == "1"
                    ) // MetOmschrijving
                    {
                        var sSQL1 = clsDataBoekhouding.HaalSubCode(sCode[0]);
                        var dtA = new DataTable();
                        dtA = GenericDataRead.GetData(sSQL1);
                        if (dtA.Rows.Count > 0)
                        {
                            cmbSubdoe.ItemsSource = null;
                            cmbSubdoe.ItemsSource = dtA.DefaultView;
                            cmbSubdoe.SelectedValuePath = "ID_SubCode";
                            cmbSubdoe.DisplayMemberPath = "SumOms";
                            cmbSubdoe.SelectedIndex = 0;
                            lblSubCode.Visibility = Visibility.Visible;
                            cmbSubdoe.Visibility = Visibility.Visible;
                            if (cmbSubdoe.Text != string.Empty)
                                txtOmschrijving.Text = cmbSubdoe.Text;
                        }
                        else
                        {
                            lblSubCode.Visibility = Visibility.Hidden;
                            cmbSubdoe.Visibility = Visibility.Hidden;
                            if (cmbCode.Text.Contains("520"))
                            {
                                var jaar = DateTime.Now.Year;
                                if (Convert.ToInt32(cmbBoekmaand.SelectedValue
                                    .ToString()) > 6) jaar++;

                                txtOmschrijving.Text = "Kalender " + jaar;
                            }
                            else
                            {
                                txtOmschrijving.Text = string.Empty;
                            }
                        }

                        Keyboard.Focus(cmbSubdoe);
                    }
                    else
                    {
                        naamLegen();
                        cmbSubdoe.ItemsSource = null;
                        lblSubCode.Visibility = Visibility.Hidden;
                        cmbSubdoe.Visibility = Visibility.Hidden;
                        txtOmschrijving.Text = "";
                        lblNaamBetaler.Visibility = Visibility.Hidden;
                        txtNaambetaler.Visibility = Visibility.Hidden;
                        grNaam.Visibility = Visibility.Hidden;
                        lblDank.Visibility = Visibility.Hidden;
                        chkDank.Visibility = Visibility.Hidden;
                        brdAdres.Visibility = Visibility.Hidden;
                    }
                }

                if (dt.Rows[0][8].ToString().ToUpper() == "TRUE" ||
                    dt.Rows[0][8].ToString() == "1")
                {
                    chkDank.IsEnabled = true;
                    lblNaamBetaler.Visibility = Visibility.Visible;
                    txtNaambetaler.Visibility = Visibility.Visible;
                    grNaam.Visibility = Visibility.Visible;
                    lblDank.Visibility = Visibility.Visible;
                    chkDank.Visibility = Visibility.Visible;
                    brdAdres.Visibility = Visibility.Visible;
                }
                else
                {
                    chkDank.IsEnabled = false;
                    lblNaamBetaler.Visibility = Visibility.Hidden;
                    txtNaambetaler.Visibility = Visibility.Hidden;
                    grNaam.Visibility = Visibility.Hidden;
                    lblDank.Visibility = Visibility.Hidden;
                    chkDank.Visibility = Visibility.Hidden;
                    brdAdres.Visibility = Visibility.Hidden;
                }

                if (cmbCode.SelectedIndex != -1 && txtBedrag.Text.Length > 0)
                    btnOpslaan.IsEnabled = true;
                else
                    btnOpslaan.IsEnabled = false;
                txtBedrag.Focusable = true;
                Keyboard.Focus(txtBedrag);
            }
        }

        private void naamLegen()
        {
            cmbNaam.SelectedIndex = -1;
            cmbAdres.SelectedIndex = -1;
            cmbBankrekening.SelectedIndex = -1;
            cmbGiro.SelectedIndex = -1;
            cmbIBAN.SelectedIndex = -1;
            cmbPostcode.SelectedIndex = -1;
            TextBlockUitleg.Visibility = Visibility.Hidden;
            txtNaambetaler.Text = "";
            lblNaamDetail.Content = "";
            lblAdresDetail.Content = "";
            lblPCPlaatsDetail.Content = "";
            chkDank.IsChecked = false;
            chkDank.Visibility = Visibility.Hidden;
            lblDank.Visibility = Visibility.Hidden;
        }

        private void CheckDank(decimal dDecimal)
        {
            var dankbedrag = Convert.ToInt32(Functions.sBedragBedankjes("ConfigData.xml"));

            if (dDecimal >= dankbedrag)
            {
                chkDank.IsChecked = true;
            }
            else
            {
                chkDank.IsChecked = false;
            }

            chkDank.IsEnabled = true;
        }

        private void VulAdresGegevens(string sValue)
        {
            var iValue = Convert.ToInt64(sValue);
            cmbBankrekening.SelectedValue = iValue;
            cmbGiro.SelectedValue = iValue;
            cmbIBAN.SelectedValue = iValue;
            cmbPostcode.SelectedValue = iValue;
            cmbNaam.SelectedValue = iValue;
            cmbAdres.SelectedValue = iValue;

            var sSQL = clsDataBoekhouding.HaalAdresDetails(sValue);
            DataTable dtA = GenericDataRead.GetData(sSQL);
            if (dtA.Rows.Count > 0)
            {
                lblNaamDetail.Content = dtA.Rows[0][0].ToString();
                lblAdresDetail.Content = dtA.Rows[0][1].ToString();
                lblPCPlaatsDetail.Content = dtA.Rows[0][2].ToString();
                txtNaambetaler.Text = dtA.Rows[0][0].ToString();
                this.grNaam.Visibility = Visibility.Visible;
                if (dtA.Rows[0]["Dank"].ToString() == "1")
                {
                    TextBlockUitleg.Visibility = Visibility.Visible;
                    chkDank.IsEnabled = false;
                }
                else
                {
                    chkDank.IsEnabled = true;
                    TextBlockUitleg.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                this.grNaam.Visibility = Visibility.Hidden;
            }
        }
    }
}