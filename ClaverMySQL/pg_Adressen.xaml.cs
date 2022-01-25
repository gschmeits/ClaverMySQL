using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    public partial class pg_Adressen : Page
    {
        public pg_Adressen()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Uitgangssituatie();
        }

        private void cmbAdresnummer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
                if (cmbAdresnummer.SelectedIndex != -1)
                {
                }
        }

        private void cmbAdresnummer_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbAdresnummer.SelectedIndex != -1)
            {
                VulGegevens(cmbAdresnummer.SelectedValue.ToString(),
                    cmbAdresnummer.SelectedIndex);
                btnOpslaan.IsEnabled = true;
            }
        }

        private void cmbAdresnummer_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (cmbAdresnummer.SelectedIndex != -1 &&
                cmbAdresnummer.SelectedValue != null)
            {
                txtAdresnummer.Text = cmbAdresnummer.SelectedValue.ToString();
                dgAdressen.SelectedIndex = cmbAdresnummer.SelectedIndex;
            }
        }

        private void cmbNaam_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbNaam.SelectedIndex != -1)
                VulGegevens(cmbNaam.SelectedValue.ToString(),
                    cmbNaam.SelectedIndex);
        }

        private void cmbPostcode_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbPostcode.SelectedIndex != -1)
            {
                VulGegevens(cmbPostcode.SelectedValue.ToString(), cmbPostcode.SelectedIndex);
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdresnummer.Text.Length > 0)
            {
                string message = "U bent gegevens aan het wijzigen\r\n\r\nToch nieuwe gegevens toevoegen?";
                string caption = "St. Petrus Claver";
                MessageBoxResult result;
                result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

                if (result == MessageBoxResult.Yes)
                {
                    Legen();
                }
            }
            else
            {
                Legen();
            }
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            var adresnummer = "";
            if (cmbAdresnummer.SelectedIndex != -1)
            {
                adresnummer = cmbAdresnummer.SelectedValue.ToString();
            }
            Opslaan(adresnummer);
            Legen();
        }

        private void dgAdressen_MouseDoubleClick(object sender,
            MouseButtonEventArgs e)
        {
            if (dgAdressen.SelectedIndex != -1)
            {
                txtAdresnummer.Text = dgAdressen.SelectedValue.ToString();
                var sString = dgAdressen.SelectedValue.ToString();
                VulGegevens(sString, cmbAdresnummer.SelectedIndex);

                cmbNaam.Text = txtAchternaam.Text + ", " + txtNaam.Text;
                cmbAdresnummer.Text = sString;
                btnOpslaan.IsEnabled = true;
            }
        }

        private void Uitgangssituatie()
        {
            try
            {
                var sSQL = clsDataAdressen.cmbAdresnummerSQL();
                var dtA = GenericDataRead.GetData(sSQL);
                cmbAdresnummer.ItemsSource = dtA.DefaultView;
                cmbAdresnummer.DisplayMemberPath = "adresnummer";
                cmbAdresnummer.SelectedValuePath = "adresnummer";

                sSQL = clsDataAdressen.cmNaamSQL();
                var dtN = GenericDataRead.GetData(sSQL);
                cmbNaam.ItemsSource = dtN.DefaultView;
                cmbNaam.DisplayMemberPath = "naam";
                cmbNaam.SelectedValuePath = "adresnummer";

                sSQL = clsDataAdressen.cmbPostcodeSQL();
                var dtP = GenericDataRead.GetData(sSQL);
                cmbPostcode.ItemsSource = dtP.DefaultView;
                cmbPostcode.DisplayMemberPath = "postcode1";
                cmbPostcode.SelectedValuePath = "adresnummer";

                sSQL = clsDataAdressen.cmbAdressenSQL();
                var dt = GenericDataRead.GetData(sSQL);
                dgAdressen.ItemsSource = dt.DefaultView;
                dgAdressen.SelectedValuePath = "adresnummer";
                dgAdressen.DisplayMemberPath = "adresnummer";

                dgAdressen.Columns[0].Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VulGegevens(string strAdresnummer, int intIndex)
        {
            var sSQL = "";
            sSQL = clsDataAdressen.VulGegevensSQL(strAdresnummer);
            var dt = GenericDataRead.GetData(sSQL);
            cmbAdresnummer.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            cmbNaam.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            cmbPostcode.SelectedValue = dt.Rows[0]["adresnummer"].ToString();

            if (dt.Rows.Count > 0)
            {
                txtNaam.Text = dt.Rows[0]["titel_en_voornaam"].ToString();
                txtAchternaam.Text = dt.Rows[0]["achternaam"].ToString();
                txtStraat.Text = dt.Rows[0]["straat"].ToString();
                txtHuisnummer.Text = dt.Rows[0]["huisnummer"].ToString();
                txtPostcode.Text = dt.Rows[0]["postcode"].ToString();
                txtPlaats.Text = dt.Rows[0]["plaats"].ToString();
                txtLand.Text = dt.Rows[0]["land"].ToString();
                txtEmail.Text = dt.Rows[0]["email"].ToString();
                txtTelefoon.Text = dt.Rows[0]["telefoon"].ToString();
                txtAantal.Text = dt.Rows[0]["aantal"].ToString();
                txtBank.Text = dt.Rows[0]["bankrekening"].ToString();
                txtGiro.Text = dt.Rows[0]["girorekening"].ToString();
                txtReden.Text = dt.Rows[0]["reden"].ToString();
                txtOpmerking.Text = dt.Rows[0]["opmerking"].ToString();
                txtLetterA.Text = dt.Rows[0]["lettera"].ToString();
                txtIBAN.Text = dt.Rows[0]["IBAN"].ToString();
                if (dt.Rows[0]["dank"].ToString() == "True" ||
                    dt.Rows[0]["dank"].ToString() == "1")
                    chkDank.IsChecked = true;
                if (dt.Rows[0]["dank"].ToString() == "False" ||
                    dt.Rows[0]["dank"].ToString() == "0")
                    chkDank.IsChecked = false;
                if (dt.Rows[0]["kalender"].ToString() == "True" ||
                    dt.Rows[0]["kalender"].ToString() == "1")
                    chkKalender.IsChecked = true;
                if (dt.Rows[0]["kalender"].ToString() == "False" ||
                    dt.Rows[0]["kalender"].ToString() == "0")
                    chkKalender.IsChecked = false;
                if (dt.Rows[0]["verwijderd"].ToString() == "True" ||
                    dt.Rows[0]["verwijderd"].ToString() == "1")
                    chkVerwijderd.IsChecked = true;
                if (dt.Rows[0]["verwijderd"].ToString() == "False" ||
                    dt.Rows[0]["verwijderd"].ToString() == "0")
                    chkVerwijderd.IsChecked = false;

                var bedragTotaal = 0;
                var bedragBedankt = 0;

                if (dt.Rows[0]["donatieTotaal"].ToString() != string.Empty)
                    Convert.ToDouble(dt.Rows[0]["donatieTotaal"].ToString());

                if (dt.Rows[0]["donatieBedankt"].ToString() != string.Empty)
                    Convert.ToDouble(dt.Rows[0]["donatieBedankt"].ToString());

                LabelTotaalGedoneerd.Content = $"€ {bedragTotaal:N2}";
                LabelReedsBedankt.Content = $"€ {bedragBedankt:N2}";
            }
        }

        private void Legen()
        {
            txtAdresnummer.Text = "";
            txtNaam.Text = "";
            txtAchternaam.Text = "";
            txtStraat.Text = "";
            txtHuisnummer.Text = "";
            txtPostcode.Text = "";
            txtPlaats.Text = "";
            txtLand.Text = "";
            txtEmail.Text = "";
            txtTelefoon.Text = "";
            txtAantal.Text = "";
            txtBank.Text = "";
            txtGiro.Text = "";
            txtIBAN.Text = "";
            txtReden.Text = "";
            txtOpmerking.Text = "";
            txtLetterA.Text = "";
            chkDank.IsChecked = false;
            chkKalender.IsChecked = false;
            chkVerwijderd.IsChecked = false;

            cmbAdresnummer.SelectedIndex = -1;
            cmbNaam.SelectedIndex = -1;
            cmbPostcode.SelectedIndex = -1;

            txtNaam.Focusable = true;
            txtNaam.Focus();
        }

        private void Opslaan(string strAdresnummer)
        {
            if (strAdresnummer.Length == 0 &&
               txtAchternaam.Text.Length > 0)
            {
                clsDataAdressen.insertAdres(txtNaam.Text, txtAchternaam.Text, txtStraat.Text, txtHuisnummer.Text, txtPostcode.Text, txtPlaats.Text, txtLand.Text, txtAantal.Text, txtLetterA.Text, chkVerwijderd.IsChecked.Value, txtReden.Text, chkKalender.IsChecked.Value, txtBank.Text, txtGiro.Text, txtIBAN.Text, chkDank.IsChecked.Value, txtOpmerking.Text, txtEmail.Text, txtTelefoon.Text, strAdresnummer);
                Uitgangssituatie();
            }
            if (txtAchternaam.Text.Length == 0)
            {
                var titel = "Gegevens toevoegen";
                var melding = "Gegevens kunnen niet toegevoegd worden, omdat de achternaam ontbreekt";
                MessageBox.Show(melding, titel, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (strAdresnummer.Length > 0)
            {
                clsDataAdressen.updateAdres(txtNaam.Text, txtAchternaam.Text, txtStraat.Text, txtHuisnummer.Text, txtPostcode.Text, txtPlaats.Text, txtLand.Text, txtAantal.Text, txtLetterA.Text, chkVerwijderd.IsChecked.Value, txtReden.Text, chkKalender.IsChecked.Value, txtBank.Text, txtGiro.Text, txtIBAN.Text, chkDank.IsChecked.Value, txtOpmerking.Text, txtEmail.Text, txtTelefoon.Text, strAdresnummer);
                var message = "Page: pg_Adressen;Function: Opslaan;Action: INSERT;Data: 'clsDataAdressen.updateAdres(txtNaam.Text, txtAchternaam.Text, txtStraat.Text, txtHuisnummer.Text, txtPostcode.Text, txtPlaats.Text, txtLand.Text, txtAantal.Text, txtLetterA.Text, chkVerwijderd.IsChecked.Value, txtReden.Text, chkKalender.IsChecked.Value, txtBank.Text, txtGiro.Text, txtIBAN.Text, chkDank.IsChecked.Value, txtOpmerking.Text, txtEmail.Text, txtTelefoon.Text, strAdresnummer);'";
            }
        }
    }
}