using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DataStorage;
using DataStorage.Classes;
using DataStorage.Data;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_DankIndividueel.xaml
    /// </summary>
    public partial class pg_DankIndividueel : Page
    {
        private string tekst { get; set; }
        private string bedrag { get; set; }

        public pg_DankIndividueel()
        {
            InitializeComponent();
            
            var sSQL = "SELECT * FROM tbl_MailTemplates";
            var dtT = GenericDataRead.GetData(sSQL);
            mailTemp.Items.Clear();
            mailTemp.ItemsSource = dtT.DefaultView;
            mailTemp.DisplayMemberPath = "name";
            mailTemp.SelectedValuePath = "mailID";
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Uitgangssituatie();
        }

        private void abonnees_DropDownClosed(object sender, EventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
            if (abonnees.SelectedIndex != -1)
                VulGegevens(abonnees.SelectedValue.ToString(),
                    abonnees.SelectedIndex);
        }

        private void abonnees_GotFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
        }

        private void abonnees_LostFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
        }

        private void abonNaam_DropDownClosed(object sender, EventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
            if (abonNaam.SelectedIndex != -1)
                VulGegevens(abonNaam.SelectedValue.ToString(),
                    abonNaam.SelectedIndex);
        }

        private void abonNaam_GotFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
        }

        private void abonNaam_LostFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
        }

        private void AbonMail_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void abonEmail_DropDownClosed(object sender, EventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;

            if (abonMail.SelectedIndex != -1)
            {
                VulGegevensMail(abonMail.Text);
                CheckBoxEmail.IsChecked = true;
                stelEmail.IsEnabled = true;
            }
        }

        private void abonEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
            StackPanelEmail.Visibility = Visibility.Hidden;
            webBrowser1.Visibility = Visibility.Hidden;
            webBrowser1.NavigateToString(" ");
            mailTemp.SelectedIndex = -1;
            stelEmail.IsEnabled = false;
        }

        private void abonEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Visible;
        }

        private void CheckBoxEmail_Checked(object sender, RoutedEventArgs e)
        {
            if (CheckBoxEmail.IsChecked == true)
            {
                mailTemp.Visibility = Visibility.Visible;
                LabelmailTemp.Visibility = Visibility.Visible;
            }
            else
            {
                LabelmailTemp.Visibility = Visibility.Hidden;
                mailTemp.Visibility = Visibility.Hidden;
            }
        }

        private void MailTemp_DropDownClosed(object sender, EventArgs e)
        {
            if (abonMail.SelectedIndex != -1)
            {
                VulGegevens(abonMail.SelectedValue.ToString(),
                    abonMail.SelectedIndex);

                bedrag = "0";
                var indexNr = Convert.ToInt32(mailTemp.SelectedIndex) + 1;

                var sql = "SELECT * FROM tbl_MailTemplates WHERE mailID = " +
                          indexNr;

                var dt = GenericDataRead.GetData(sql);
                if (dt.Rows.Count > 0)
                {
                    var string1 = dt.Rows[0]["template"].ToString();

                    char[] charsToTrim =
                        {'O', 'R', '`', ' ', '\"', '=', '1', '-', ';'};

                    var sql1 = "SELECT a.titel_en_voornaam, a.achternaam, a.email, ";
                    sql1 += "IFNULL((sum(b.bedrag) - a.donatieBedankt), 0) AS bedrag ";
                    sql1 += "FROM tbl_Mutaties AS b, tbl_Adressen AS a ";
                    sql1 += "WHERE NaamBetaler = " + abonnees.Text + " ";
                    sql1 += "AND b.NaamBetaler = a.adresnummer ";
                    sql1 +=
                        "group by  a.titel_en_voornaam, a.achternaam, a.email, a.donatiebedankt ";
                    var dtBedrag = GenericDataRead.GetData(sql1);
                    if (dtBedrag.Rows.Count > 0)
                    {
                        var string2 = string1.Trim(charsToTrim);

                        var bedrag1 =
                            Convert.ToDecimal(dtBedrag.Rows[0]["bedrag"]
                                .ToString());

                        bedrag = dtBedrag.Rows[0]["bedrag"]
                            .ToString();
                        string2 = string1.Replace("{{bedrag}}",
                            $"&euro; {bedrag1:N2}");

                        var abonnaam = dtBedrag.Rows[0][0] + " " +
                                       dtBedrag.Rows[0][1];
                        string1 = string2;
                        string2 = string1.Replace("{{aanspreektitel}}",
                            abonnaam);
                        string1 = string2.Replace("{{datum}}",
                            DateTime.Now.ToString("dd MMMM yyyy"));

                        string2 = string1;
                        string1 = string2.Replace('\"', '"');

                        webBrowser1.NavigateToString(string1);
                        tekst = string1;
                        TextBlockID.Text = dt.Rows[0]["mailID"].ToString();
                        TextBlockName.Text = dt.Rows[0]["name"].ToString();
                        webBrowser1.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void stelSamen_Click(object sender, RoutedEventArgs e)
        {
            stelSamen.Visibility = Visibility.Hidden;
            melding.Visibility = Visibility.Visible;

            var mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var sb = new StringBuilder();

            var libraryFactory = new BedankjesFactory();
            var bedankjes = new List<BedankjeIndividueel>();
            bedankjes =
                libraryFactory.GetBedankjeIndividueel(abonnees.SelectedValue
                    .ToString());
            sb.AppendLine(
                "titel_en_voornaam;achternaam;adres;postcode;woonplaats");
            foreach (var bedankje in bedankjes)
                sb.AppendLine(bedankje.titel + ";" + bedankje.naam + ";" +
                              bedankje.adres + ";" + bedankje.postcode + ";" +
                              bedankje.woonplaats);
            using (var outfile =
                new StreamWriter(mydocpath + @"\dankbriefIndivicudeel.csv"))
            {
                outfile.Write(sb.ToString());
            }

            melding.Visibility = Visibility.Hidden;
            var message =
                "Gegevens zijn het csv-bestenad gezet die op de desktop te vinden zijn!!!";
            var caption = "Melding";
            MessageBox.Show(message, caption, MessageBoxButton.OK,
                MessageBoxImage.Warning);
            stelSamen.Visibility = Visibility.Hidden;
            abonnees.SelectedIndex = -1;
        }

        private void StelEmail_Click(object sender, RoutedEventArgs e)
        {
            var outlookApp = new OutlookApp();
            MailItem mailItem =
                outlookApp.CreateItem(OlItemType.olMailItem);
            mailItem.Subject = "Bedankje";
            mailItem.HTMLBody = tekst;
            mailItem.To = abonMail.Text;

            // De mail opslaan in de brief tabel
            GenericDataRead.INUPDEL(clsDataExtra.opslaanMail(abonnees.Text, abonMail.Text, mailItem.Subject, tekst));

            // Sla het bedank bedrag op in de adres tabel
            GenericDataRead.INUPDEL(clsDataExtra.updateBedankBedrag(bedrag.ToString(), abonnees.Text));

            mailItem.Display(true);
            Uitgangssituatie();

            stelSamen.Visibility = Visibility.Visible;
            StackPanelEmail.Visibility = Visibility.Hidden;
            webBrowser1.Visibility = Visibility.Hidden;
            webBrowser1.NavigateToString(" ");
            mailTemp.SelectedIndex = -1;
            stelEmail.IsEnabled = false;
        }

        private void schermSlujiten_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Uitgangssituatie()
        {
            try
            {
                var sSQL = clsDataAdressen.cmbAdressenBedragSQL();
                var dtA = GenericDataRead.GetData(sSQL);
                abonnees.ItemsSource = dtA.DefaultView;
                abonnees.DisplayMemberPath = "adresnummer";
                abonnees.SelectedValuePath = "adresnummer";

                sSQL = clsDataAdressen.cmbNaamBedragSQL();
                var dtN = GenericDataRead.GetData(sSQL);
                abonNaam.ItemsSource = dtN.DefaultView;
                abonNaam.DisplayMemberPath = "naam";
                abonNaam.SelectedValuePath = "adresnummer";

                sSQL = clsDataAdressen.cmbEmailBedragSQL();
                var dtP = GenericDataRead.GetData(sSQL);
                abonMail.ItemsSource = dtP.DefaultView;
                abonMail.DisplayMemberPath = "email";
                abonMail.SelectedValuePath = "adresnummer";

                StackPanelEmail.Visibility = Visibility.Hidden;
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
            abonnees.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            abonNaam.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            abonMail.SelectedValue = dt.Rows[0]["adresnummer"].ToString();


            if (abonMail.SelectedValue == null)
                StackPanelEmail.Visibility = Visibility.Hidden;
            else
                StackPanelEmail.Visibility = Visibility.Visible;
        }

        private void VulGegevensMail(string strMail)
        {
            var sSQL = "";
            sSQL = clsDataAdressen.VulGegevensMailSQL(strMail);
            var dt = GenericDataRead.GetData(sSQL);
            abonnees.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            abonNaam.SelectedValue = dt.Rows[0]["adresnummer"].ToString();
            abonMail.SelectedValue = dt.Rows[0]["adresnummer"].ToString();

            if (abonMail.SelectedValue == null)
                StackPanelEmail.Visibility = Visibility.Hidden;
            else
                StackPanelEmail.Visibility = Visibility.Visible;
        }

    }
}
