using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ClaverMySQL.Utils;
using DataStorage;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_ControlePerGrootboek.xaml
    /// </summary>
    public partial class pg_ControlePerGrootboek : Page
    {
        public pg_ControlePerGrootboek()
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
            DataTable dtB = GenericDataRead.GetData(sSQL);
            cmbBeginMaand.ItemsSource = dtB.DefaultView;
            cmbBeginMaand.SelectedValuePath = "maand";
            cmbEindMaand.ItemsSource = dtB.DefaultView;
            cmbEindMaand.SelectedValuePath = "maand";

            cmbJaar.SelectedValue = DateTime.Now.Year;
            cmbBeginMaand.SelectedValue = DateTime.Now.Month;
            cmbEindMaand.SelectedValue = DateTime.Now.Month;

            sSQL = clsDataGrootboek.CodeOverzicht();
            dtB = GenericDataRead.GetData(sSQL);
            cmbBeginGrootboek.ItemsSource = dtB.DefaultView;
            cmbBeginGrootboek.SelectedValuePath = "IDCode";
            cmbEindGrootboek.ItemsSource = dtB.DefaultView;
            cmbEindGrootboek.SelectedValuePath = "IDCode";
        }

        private void overzichtMaken_Click(object sender, RoutedEventArgs e)
        {
            if (cmbJaar.SelectedIndex != -1 &&
                cmbBeginMaand.SelectedIndex != -1 &&
                cmbEindMaand.SelectedIndex != -1 &&
                cmbBeginGrootboek.SelectedIndex != -1 &&
                cmbEindGrootboek.SelectedIndex != -1)
            {
                if (Convert.ToInt32(cmbBeginGrootboek.SelectedValue.ToString()) >
                    Convert.ToInt32(cmbEindGrootboek.SelectedValue.ToString()))
                {
                    MessageBox.Show(
                        "Begin grootboek dient gelijk of groter dan eind grootboek te zijn!!!",
                        "Foutmelding", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }
                if (Convert.ToInt32(cmbBeginMaand.SelectedValue.ToString()) >
                    Convert.ToInt32(cmbEindMaand.SelectedValue.ToString()))
                {
                    MessageBox.Show(
                        "Begin maand dient gelijk of groter dan de eind maand te zijn!!!",
                        "Foutmelding", MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                HaalWaardenOp();
            }
            else
            {
                MessageBox.Show("Controleer de ingevoerde gegevens!!!",
                    "Melding", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void printenOverzicht_Click(object sender, RoutedEventArgs e)
        {
            DateTime datum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbBeginMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(datum);
            var pr = new PrintControle()
            {
                GetMaand = () => { return cmbBeginMaand.SelectedValue.ToString(); },
                GetMaand1 = () => { return cmbEindMaand.SelectedValue.ToString(); },
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
            DateTime beginDatum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbBeginMaand.SelectedValue.ToString() + "-" + "01");
            DateTime datum1 = Utilities.dhLastDayInMonth(beginDatum);
            DateTime eindDatum = Convert.ToDateTime(cmbJaar.SelectedValue.ToString() + "-" + cmbEindMaand.SelectedValue.ToString() + "-" + "01");

            var grootboek = "";
            var beginGrootboek = cmbBeginGrootboek.SelectedValue.ToString();
            var eindGrootboek = cmbEindGrootboek.SelectedValue.ToString();
            var soort = "Huis";
            gegevens.Text = String.Empty;
            Int16 intT = 0;
            Decimal dblTot = 0;
            Decimal dblSubTot = 0;
            Decimal dblTotTot = 0;
            var sSQL = clsDataControle.detailsPerGrootBoek( beginDatum, eindDatum, beginGrootboek, eindGrootboek);
            var dt = GenericDataRead.GetData(sSQL);
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
                            gegevens.Text += "                                                                            =============\r\n";
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
                gegevens.Text += "                                                                            =============\r\n";
                //dblSubTot += dblTot;
                //dblTot = 0;
                //gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                //gegevens.Text += string.Format("{0,10}{1,-60}{2,18:N}", "Subtotaal: ", soort, dblSubTot) + "\r\n";
                //gegevens.Text += "-----------------------------------------------------------------------------------------\r\n";
                //dblTotTot += dblSubTot;
               // dblSubTot = 0;
                //gegevens.Text += "\r\n\r\n                                                                            -------------\r\n";
                //gegevens.Text += string.Format("{0,-40}{1,49:N}", "Totaal:", dblTotTot) + "\r\n";
                //gegevens.Text += "                                                                            =============\r\n";
                melding.Visibility = System.Windows.Visibility.Hidden;
                gegevens.Visibility = System.Windows.Visibility.Visible;
                printenOverzicht.IsEnabled = true;
            }
        }


    }
}
