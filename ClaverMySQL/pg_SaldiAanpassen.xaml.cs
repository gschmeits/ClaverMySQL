using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_SaldiAanpassen.xaml
    /// </summary>
    public partial class pg_SaldiAanpassen : Page
    {
        public pg_SaldiAanpassen()
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
            dtA = GenericDataRead.GetData(sSQL);
            cmbMaand.ItemsSource = dtA.DefaultView;
            cmbMaand.SelectedValuePath = "maand";

            cmbJaar.SelectedValue = DateTime.Now.Year;
            cmbMaand.SelectedValue = DateTime.Now.Month;
        }

        private void cmbJaar_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbJaar.SelectedIndex != -1 && cmbMaand.SelectedIndex != -1)
            {
                haalWaardenop();
            }
        }

        private void cmbMaand_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbJaar.SelectedIndex != -1 && cmbMaand.SelectedIndex != -1)
            {
                haalWaardenop();
            }
        }

        private void schermSlujiten_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void overzichtMaken_Click(object sender, RoutedEventArgs e)
        {
            haalWaardenop();
        }

        private void huisWaarde_GotFocus(object sender, RoutedEventArgs e)
        {
            huisWaarde.SelectAll();
        }

        private void huisWaarde_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            huisWaarde.SelectAll();
        }

        private void huisWaarde_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void huisWaarde_LostFocus(object sender, RoutedEventArgs e)
        {
            conversieBedrag("huis");
        }

        private void huisWaarde_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(missieWaarde);
                }
            }
        }

        private void missieWaarde_GotFocus(object sender, RoutedEventArgs e)
        {
            missieWaarde.SelectAll();
        }

        private void missieWaarde_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            missieWaarde.SelectAll();
        }

        private void missieWaarde_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void missieWaarde_LostFocus(object sender, RoutedEventArgs e)
        {
            conversieBedrag("missie");
        }

        private void missieWaarde_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(zakenWaarde);
                }
            }
        }

        private void zakenWaarde_GotFocus(object sender, RoutedEventArgs e)
        {
            zakenWaarde.SelectAll();
        }

        private void zakenWaarde_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            zakenWaarde.SelectAll();
        }

        private void zakenWaarde_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void zakenWaarde_LostFocus(object sender, RoutedEventArgs e)
        {
            conversieBedrag("zaken");
        }

        private void zakenWaarde_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(MTLWaarde);
                }
            }
        }

        private void MTLWaarde_GotFocus(object sender, RoutedEventArgs e)
        {
            MTLWaarde.SelectAll();
        }

        private void MTLWaarde_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            MTLWaarde.SelectAll();
        }
        private void MTLWaarde_KeyDown(object sender, KeyEventArgs e)
        {
            DecimaleInvoer(sender, e);
        }

        private void MTLWaarde_LostFocus(object sender, RoutedEventArgs e)
        {
            conversieBedrag("MTL");
        }

        private void MTLWaarde_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return || e.Key == Key.Tab)
            {
                e.Handled = true;
                {
                    Keyboard.Focus(huisWaarde);
                }
            }
        }

        private void opslaanSaldi_Click(object sender, RoutedEventArgs e)
        {
            DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(datum);
            var sSQL = clsDataExtra.verwijderSaldi(datum1);
            GenericDataRead.INUPDEL(sSQL);

            sSQL = clsDataExtra.opslaanSaldi(datum1, "1", huisWaarde1.Text.Replace(",", "."));
            GenericDataRead.INUPDEL(sSQL);

            sSQL = clsDataExtra.opslaanSaldi(datum1, "2", missieWaarde1.Text.Replace(",", "."));
            GenericDataRead.INUPDEL(sSQL);

            sSQL = clsDataExtra.opslaanSaldi(datum1, "3", zakenWaarde1.Text.Replace(",", "."));
            GenericDataRead.INUPDEL(sSQL);

            sSQL = clsDataExtra.opslaanSaldi(datum1, "4", MTLWaarde1.Text.Replace(",", "."));
            GenericDataRead.INUPDEL(sSQL);

            var caption = "Saldi aanpassen";
            var message = "De saldi zijn aangepast.";
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            // WPFMessageBox.Show(caption, message, WPFMessageBoxButtons.OK,WPFMessageBoxImage.Default);
            NavigationService.GoBack();
        }

        private void DecimaleInvoer(object sender, KeyEventArgs e)
        {
            var ch = (char)KeyInterop.VirtualKeyFromKey(e.Key);
            switch (e.Key)
            {
                case Key.Decimal:
                case Key.OemPeriod:
                    var box = (sender as TextBox);
                    var text = box.Text;
                    var caret = box.CaretIndex;
                    string urdu = ",";
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
                case Key.Back:
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void conversieBedrag(string p)
        {
            var myTextBox = (TextBox)this.FindName(p + "Waarde");
            var myTextBox1 = (TextBox)this.FindName(p + "Waarde1");
            if (myTextBox.Text.Length == 0)
            {
                myTextBox.Text = "€ 0,00";
            }
            var sTekst = myTextBox.Text.Replace("€", "");
            myTextBox.Text.Replace("€", "");
            myTextBox.Text.Replace(",", ".");
            sTekst.Replace("€", "");
            //var sTekst2 = sTekst.Replace(".", "");
            var sTekst2 = sTekst;
            var sTekst1 = sTekst2.Replace(".", ",");
            var dTekst2 = Convert.ToDecimal(sTekst2);
            myTextBox.Text = string.Format("{0:C}", Convert.ToDecimal(dTekst2));
            myTextBox1.Text = Convert.ToString(dTekst2);
        }
        
        private void haalWaardenop()
        {
            if (cmbMaand.Text.Length > 0 && cmbJaar.Text.Length > 0)
            {
                Int32 maand = Convert.ToInt32(cmbMaand.SelectedValue.ToString());
                Int32 jaar = Convert.ToInt32(cmbJaar.SelectedValue.ToString());
                var sSQL = clsDataExtra.haalSaldi(maand, jaar);
                DataTable dt = GenericDataRead.GetData(sSQL);
                if (dt.Rows.Count == 4)
                {
                    huisWaarde.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[0][1].ToString()));
                    missieWaarde.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[1][1].ToString()));
                    zakenWaarde.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[2][1].ToString()));
                    MTLWaarde.Text = string.Format("{0:C}", Convert.ToDecimal(dt.Rows[3][1].ToString()));
                    huisWaarde1.Text = dt.Rows[0][1].ToString();
                    missieWaarde1.Text = dt.Rows[1][1].ToString();
                    zakenWaarde1.Text = dt.Rows[2][1].ToString();
                    MTLWaarde1.Text = dt.Rows[3][1].ToString();
                }
                else
                {
                    huisWaarde.Text = "€ 0,00";
                    missieWaarde.Text = "€ 0,00";
                    zakenWaarde.Text = "€ 0,00";
                    MTLWaarde.Text = "€ 0,00";
                    huisWaarde1.Text = "0.00";
                    missieWaarde1.Text = "0.00";
                    zakenWaarde1.Text = "0.00";
                    MTLWaarde1.Text = "0.00";
                }
                fr.Visibility = System.Windows.Visibility.Visible;
                detail.Visibility = System.Windows.Visibility.Visible;
                opslaanSaldi.Visibility = System.Windows.Visibility.Visible;
                huisWaarde.Focus();
            }
        }

    }
}
