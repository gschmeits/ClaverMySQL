using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    public partial class pg_Grootboek : Page
    {
        public pg_Grootboek()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Starten();
        }

        private void cmbCodenummer_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbCodenummer.SelectedIndex != -1)
            {
                HaalData(cmbCodenummer.SelectedValue.ToString());
            }
        }

        private void cmbCodering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OpSlaanZichtbaar();
        }

        private void cmbOmschrijving_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbOmschrijving.SelectedIndex != -1)
            {
                HaalData(cmbOmschrijving.SelectedValue.ToString());
            }
        }

        private void txtCodenummer_TextChanged(object sender, TextChangedEventArgs e)
        {
            OpSlaanZichtbaar();
        }

        private void txtOmschrijving_TextChanged(object sender, TextChangedEventArgs e)
        {
            OpSlaanZichtbaar();
        }

        private void rdGEG_Click(object sender, RoutedEventArgs e)
        {
            if (rdGEG.IsChecked == true)
            {
                rdMN.IsChecked = false;
                rdMO.IsChecked = false;
                rdMS.IsChecked = false;
                rdNEB.IsChecked = false;
            }

            OpSlaanZichtbaar();
        }

        private void rdMO_Click(object sender, RoutedEventArgs e)
        {
            OpSlaanZichtbaar();
            if (rdMO.IsChecked == true)
            {
                rdGEG.IsChecked = false;
            }
        }

        private void rdMN_Click(object sender, RoutedEventArgs e)
        {
            OpSlaanZichtbaar();
            if (rdMN.IsChecked == true)
            {
                rdGEG.IsChecked = false;
            }
        }

        private void rdMS_Click(object sender, RoutedEventArgs e)
        {
            OpSlaanZichtbaar();
            if (rdMS.IsChecked == true)
            {
                rdGEG.IsChecked = false;
            }
        }

        private void rdNEB_Click(object sender, RoutedEventArgs e)
        {
            OpSlaanZichtbaar();
            if (rdNEB.IsChecked == true)
            {
                rdGEG.IsChecked = false;
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            Legen();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            var sSQL = string.Empty;
            var sCode = Convert.ToString(cmbCodering.SelectedIndex + 1);
            var sCodenummer = txtCodenummer.Text;
            var sOmschrijving = txtOmschrijving.Text;
            var sOmschrijvingV = txtOmschrijvingV.Text;
            var rij = this.TextBoxRij.Text;
            var kolom = this.TextBoxKolom.Text;
            var zoekwaarden = this.TextBoxZoektermen.Text;
            bool bGEG = rdGEG.IsChecked.Value;
            bool bMN = rdMN.IsChecked.Value;
            bool bMO = rdMO.IsChecked.Value;
            bool bMS = rdMS.IsChecked.Value;
            bool bGetallen = chkGetallen.IsChecked.Value;
            var sID = txtID.Text;

            if (txtID.Text.Length > 0)
            {
                sSQL = clsDataGrootboek.sUpdateCodetabel(
                    sCode,
                    sCodenummer,
                    sOmschrijving,
                    sOmschrijvingV,
                    bGEG,
                    bMN,
                    bMO,
                    bMS,
                    bGetallen,
                    sID,
                    rij,
                    kolom,
                    zoekwaarden);
            }
            else
            {
                sSQL = clsDataGrootboek.sInsertCodetabel(
                    sCode,
                    sCodenummer,
                    sOmschrijving,
                    sOmschrijvingV,
                    bGEG,
                    bMN,
                    bMO,
                    bMS,
                    bGetallen,
                    rij,
                    kolom,
                    zoekwaarden);
            }

            GenericDataRead.INUPDEL(sSQL);
            Legen();
            Starten();
        }

        private void dgGrootboek_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var intRow = dgGrootboek.SelectedIndex;
            int intKol = dgGrootboek.CurrentColumn.DisplayIndex;
            DataRowView dataRow = (DataRowView)dgGrootboek.Items[intRow];

            if (dgGrootboek.SelectedIndex != -1)
            {
                HaalData(dataRow.Row.ItemArray[2].ToString());
            }
        }

        private void Starten()
        {
            var sSQL = clsDataGrootboek.sCodetabel();
            DataTable dtA = GenericDataRead.GetData(sSQL);
            cmbCodenummer.ItemsSource = dtA.DefaultView;
            cmbCodenummer.DisplayMemberPath = "idcode";
            cmbCodenummer.SelectedValuePath = "idcode";

            cmbOmschrijving.ItemsSource = dtA.DefaultView;
            cmbOmschrijving.DisplayMemberPath = "codeomschrijving";
            cmbOmschrijving.SelectedValuePath = "idcode";

            sSQL = clsDataGrootboek.sCodesoort();
            dtA = GenericDataRead.GetData(sSQL);
            cmbCodeSoort.ItemsSource = dtA.DefaultView;
            cmbCodeSoort.DisplayMemberPath = "code";
            cmbCodeSoort.SelectedValuePath = "idcode";

            cmbCodering.ItemsSource = dtA.DefaultView;
            cmbCodering.DisplayMemberPath = "code";
            cmbCodering.SelectedValuePath = "idcode";

            sSQL = clsDataGrootboek.sCodesoortCodeTabel();
            DataTable dt = GenericDataRead.GetData(sSQL);
            dgGrootboek.ItemsSource = dt.DefaultView;
            dgGrootboek.SelectedValuePath = "idcode";
        }

        private void HaalData(string sWaarde)
        {
            var sSQL = clsDataGrootboek.sSelecteerGegevens(sWaarde);
            DataTable dt = GenericDataRead.GetData(sSQL);
            txtID.Text = dt.Rows[0][0].ToString();
            cmbCodenummer.SelectedValue = dt.Rows[0][2].ToString();
            cmbCodering.SelectedIndex = Convert.ToInt16(dt.Rows[0][1].ToString()) - 1;
            cmbOmschrijving.SelectedValue = dt.Rows[0][2].ToString();
            cmbCodeSoort.SelectedIndex = Convert.ToInt16(dt.Rows[0][1].ToString()) - 1;
            txtCodenummer.Text = dt.Rows[0][2].ToString();
            txtOmschrijving.Text = dt.Rows[0][3].ToString();
            txtOmschrijvingV.Text = dt.Rows[0][4].ToString();
            TextBoxZoektermen.Text = dt.Rows[0][13].ToString();
            this.TextBoxRij.Text = dt.Rows[0][11].ToString();
            this.TextBoxKolom.Text = dt.Rows[0][12].ToString();
            if (dt.Rows[0][10].ToString() == "True" || dt.Rows[0][10].ToString() == "1")
            {
                chkGetallen.IsChecked = true;
            }
            else
            {
                chkGetallen.IsChecked = false;
            }

            if (dt.Rows[0][5].ToString() == "True" || dt.Rows[0][5].ToString() == "1")
            {
                rdGEG.IsChecked = true;
            }
            else
            {
                rdGEG.IsChecked = false;
            }

            if (dt.Rows[0][6].ToString() == "True" || dt.Rows[0][6].ToString() == "1")
            {
                rdMN.IsChecked = true;
            }
            else
            {
                rdMN.IsChecked = false;
            }

            if (dt.Rows[0][7].ToString() == "True" || dt.Rows[0][7].ToString() == "1")
            {
                rdMO.IsChecked = true;
            }
            else
            {
                rdMO.IsChecked = false;
            }

            if (dt.Rows[0][8].ToString() == "True" || dt.Rows[0][8].ToString() == "1")
            {
                rdMS.IsChecked = true;
            }
            else
            {
                rdMS.IsChecked = false;
            }
        }

        private void Legen()
        {
            cmbOmschrijving.SelectedIndex = -1;
            cmbCodering.SelectedIndex = -1;
            cmbCodenummer.SelectedIndex = -1;
            cmbCodeSoort.SelectedIndex = -1;
            txtID.Text = string.Empty;
            txtCodenummer.Text = string.Empty;
            txtOmschrijving.Text = string.Empty;
            txtOmschrijvingV.Text = string.Empty;
            TextBoxZoektermen.Text = string.Empty;
            chkGetallen.IsChecked = false;
            rdGEG.IsChecked = false;
            rdMN.IsChecked = false;
            rdMS.IsChecked = false;
            rdMO.IsChecked = false;
            rdNEB.IsChecked = false;
            this.TextBoxRij.Text = string.Empty;
            this.TextBoxKolom.Text = string.Empty;
        }

        private void OpSlaanZichtbaar()
        {
            if (cmbCodering.SelectedIndex != -1 && txtCodenummer.Text.Length > 0 && txtOmschrijving.Text.Length > 0)
            {
                if (rdGEG.IsChecked == true || rdMN.IsChecked == true || rdMO.IsChecked == true
                    || rdMS.IsChecked == true
                    || rdNEB.IsChecked == true) btnOpslaan.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
