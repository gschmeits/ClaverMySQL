using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_SubCodes.xaml
    /// </summary>
    public partial class pg_SubCodes : Page
    {
        private string sGrootboek;
        private string sSubCode;
        private string sIDLCode;
        private string sRij;

        public pg_SubCodes()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            HaalDataUpdate();
        }

        private void cmbRekening_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbRekening.SelectedIndex != -1)
            {
                Legen();
                HaalData("rekeningnummer", cmbRekening.SelectedValue.ToString(), true);
            }
        }

        private void cmbSubcode_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbSubcode.SelectedIndex != -1)
            {
                Legen();
                HaalData("ID_Subcode", Convert.ToString(cmbSubcode.SelectedIndex + 1), true);
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            Legen();
        }

        private void btnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            var sSQL = "";
            if (txtIDLCode.Text == "")
            {
                sSQL = clsDataSubcodes.SubcodeInsert(txtGrootboek.Text, txtSubCode.Text, txtRij.Text);
            }
            else
            {
                sSQL = clsDataSubcodes.SubcodeUpdate(txtGrootboek.Text, txtSubCode.Text, txtRij.Text, txtIDLCode.Text);
            }
            GenericDataRead.INUPDEL(sSQL);
            Legen();
            HaalDataUpdate();
        }

        private void dgSubcode_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var intRow = dgSubcode.SelectedIndex;
            DataRowView dataRow = (DataRowView)dgSubcode.Items[intRow];

            if (dgSubcode.SelectedIndex != -1)
            {
                HaalData("idlcode", dataRow.Row.ItemArray[3].ToString(), false);
            }
        }

        private void HaalDataUpdate()
        {
            var sSQL = clsDataSubcodes.cmbRekeningHaalSQL();
            DataTable dt = GenericDataRead.GetData(sSQL);
            cmbRekening.ItemsSource = dt.DefaultView;
            cmbRekening.SelectedValuePath = "rekeningnummer";
            cmbRekening.DisplayMemberPath = "rekeningnummer";

            sSQL = clsDataSubcodes.cmbSubcodeHaalSQL();
            dt = GenericDataRead.GetData(sSQL);
            cmbSubcode.ItemsSource = dt.DefaultView;
            cmbSubcode.SelectedValuePath = "idlcode";
            cmbSubcode.DisplayMemberPath = "sumoms";
        }

        private void Legen()
        {
            txtGrootboek.Text = "";
            txtRij.Text = "";
            txtSubCode.Text = "";
            txtIDLCode.Text = "";
            dgSubcode.ItemsSource = null;
        }

        private void HaalData(string sKolom, string sWaarde, Boolean bAll)
        {
            var sSQL = "";
            if (sKolom == "idlcode")
            {
                sSQL = clsDataSubcodes.cmbSubcodeSQL(sWaarde);
                cmbRekening.SelectedIndex = -1;
            }
            else
            {
                sSQL = clsDataSubcodes.cmbSubcodeSQL(sKolom, sWaarde);
                cmbSubcode.SelectedIndex = -1;
            }
            DataTable dt = GenericDataRead.GetData(sSQL);
            if (bAll == true)
            {
                dgSubcode.ItemsSource = dt.DefaultView;
            }
            else
            {
                sGrootboek = dt.Rows[0][0].ToString();
                sSubCode = dt.Rows[0][1].ToString();
                sRij = dt.Rows[0][2].ToString();
                sIDLCode = dt.Rows[0][3].ToString();

                VulBox();
            }
        }

        private void VulBox()
        {
            txtGrootboek.Text = sGrootboek;
            txtSubCode.Text = sSubCode;
            txtRij.Text = sRij;
            txtIDLCode.Text = sIDLCode;
        }
    }
}
