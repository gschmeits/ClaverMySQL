using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using ClaverMySQL.Utils;
using DataStorage;
using DataStorage.Classes;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_Acceptgiro.xaml
    /// </summary>
    public partial class pg_Acceptgiro : Page
    {
        public pg_Acceptgiro()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var libraryFactory = new AcceptgiroFactory();
            var acceptgiros = new List<Acceptgiro>();
            acceptgiros = libraryFactory.GetAcceptgiro();
            cmbCode.ItemsSource = acceptgiros;
            cmbCode.DataContext = acceptgiros;
            cmbCode.SelectedValuePath = "IDCode";
            cmbCode.DisplayMemberPath = "Code";
        }

        private void btnCSV_Click(object sender, RoutedEventArgs e)
        {
            bestandMaken("CSV");
            btnCSV.Visibility = System.Windows.Visibility.Visible;
        }

        private void bestandMaken(string strVersie)
        {
            // Wanneer een code geselecteerd is, dan uitvoeren
            if (cmbCode.SelectedIndex != -1)
            {
                if (optA.IsChecked == false && optA2.IsChecked == false && optA3.IsChecked == false && optA4.IsChecked == false && optA5.IsChecked == false && optA6.IsChecked == false && optA7.IsChecked == false && optW.IsChecked == false && optD.IsChecked == false && optW1.IsChecked == false && optD1.IsChecked == false)
                {
                    string message = "Geen optie geselecteerd.\r\nSelecteer eerst een optie!!!";
                    string caption = "Foutmelding";
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    lblOgenblik.Content = "Een ogenblik geduld aub...";
                    lblOgenblik.Visibility = System.Windows.Visibility.Visible;
                    btnCSV.Visibility = System.Windows.Visibility.Hidden;

                    int intX = 0;
                    string strVerwijderd = "tbl_Adressen.verwijderd = 0 ";
                    if (cmbCode.SelectedValue.ToString() == "520" || cmbCode.SelectedValue.ToString() == "521")
                    {
                        strVerwijderd = strVerwijderd + " AND tbl_Adressen.kalender = T1";
                    }

                    // Bouw de string op voor het selecteren van de gegevens
                    string strQue = "SELECT adresnummer, CONCAT(titel_en_voornaam, ' ', achternaam), ";
                    strQue = strQue + "straat, huisnummer, postcode, ";
                    strQue = strQue + "plaats , land, aantal, ";
                    strQue = strQue + "verwijderd, IBAN ";
                    strQue = strQue + "FROM tbl_Adressen ";
                    strQue = strQue + "WHERE ";

                    // Vul string aan indien optA is aangevinkt(optA.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA2 is aangevinkt
                    if (optA2.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A2' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA3 is aangevinkt
                    if (optA3.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A3' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA4 is aangevinkt
                    if (optA4.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A4' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA5 is aangevinkt
                    if (optA5.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A5' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA6 is aangevinkt
                    if (optA6.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A6' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optA7 is aangevinkt
                    if (optA7.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'A7' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optW1 is aangevinkt
                    if (optW1.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'W1' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optW is aangevinkt
                    if (optW.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'W'  AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optD is aangevinkt
                    if (optD.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'D' AND " + strVerwijderd + ")";
                    }

                    // Vul string aan indien optD1 is aangevinkt
                    if (optD1.IsChecked == true)
                    {
                        intX = intX + 1;
                        if (intX > 1)
                        {
                            strQue = strQue + " OR ";
                        }
                        strQue = strQue + " (LetterA = 'D1' AND " + strVerwijderd + ")";
                    }

                    if (optPostcode.IsChecked == true)
                    {
                        strQue = strQue + " ORDER BY land, postcode;";
                    }
                    if (optNaam.IsChecked == true)
                    {
                        strQue = strQue + " ORDER BY achternaam;";
                    }

                    if (intX == 0)
                    {
                        strQue = strQue + " verwijderd = 0 ";
                    }

                    // Acceptgiro is geselecteerd (altijd geselecteerd; gelocked)

                    if (optAccept.IsChecked == true)
                    {
                        // Haal de gegevens op uit de tabel
                        DataTable dtH = GenericDataRead.GetData(strQue);
                        pbStatus.Minimum = 0;
                        pbStatus.Maximum = 100;
                        pbStatus.Value = 0;

                        btnCSV.Visibility = System.Windows.Visibility.Hidden;
                        //gridP.Visibility = System.Windows.Visibility.Visible;
                        lblOgenblik.Visibility = System.Windows.Visibility.Visible;
                        this.Refresh();

                        if (strVersie == "CSV")
                        {
                            string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                            StringBuilder sb = new StringBuilder();
                            var aantal = dtH.Rows.Count;
#pragma warning disable CS0219 // The variable 'step' is assigned but its value is never used
                            Int32 step = 0;
#pragma warning restore CS0219 // The variable 'step' is assigned but its value is never used
                            for (int x = 0; x < dtH.Rows.Count; x++)
                            {
                                string strVerloop = "00000000000";
                                int intLengte = (dtH.Rows[x][0].ToString().Length);
                                string strCode = cmbCode.SelectedValue.ToString() + strVerloop.Substring(0, 12 - intLengte) + dtH.Rows[x][0].ToString();
                                // Codenummer
                                sb.AppendLine(strCode + ";" + dtH.Rows[x][1].ToString() + ";" + dtH.Rows[x][2].ToString() + ";" + dtH.Rows[x][3].ToString() + ";" + dtH.Rows[x][4].ToString() + ";" + dtH.Rows[x][5].ToString() + ";Missiezusters van St. Petrus Claver;Bouillonstraat 4;6211 LH;Maastricht;NL47INGB0001041536;" + txtBoven.Text + ";" + txtBoven1.Text + ";" + txtLinks.Text);
                                // BicCODE INGBNL2A
                            }
                            DateTime date1 = DateTime.Now;
                            string strDatum = date1.Year.ToString() + date1.Month.ToString() + date1.Day.ToString() + date1.Hour.ToString() + date1.Minute.ToString() + date1.Second.ToString();

                            using (StreamWriter outfile = new StreamWriter(mydocpath + @"\accept" + cmbCode.SelectedValue + "_" + strDatum + ".csv"))
                            {
                                outfile.Write(sb.ToString());
                            }
                        }
                        gridP.Visibility = System.Windows.Visibility.Hidden;
                        lblOgenblik.Visibility = System.Windows.Visibility.Hidden;
                        string message = "Gegevens zijn klaar!!!";
                        string caption = "Melding";
                        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        string message = "Geen code geselecteerd.\r\nSelecteer eerst een code!!!";
                        string caption = "Foutmelding";
                        MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
