using System;
using System.Collections.Generic;
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
    /// Interaction logic for pg_Niet_Betalenden.xaml
    /// </summary>
    public partial class pg_Niet_Betalenden : Page
    {
        public pg_Niet_Betalenden()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            cmbJaren.Focus();
            var intJaar = DateTime.Now.Year;
            for (int intX = intJaar - 15; intX <= intJaar - 2; intX++)
            {
                cmbJaren.Items.Add(intX);
            }
            cmbJaren.SelectedIndex = -1;
            if (cmbJaren.SelectedIndex == -1)
            {
                btnSamenstellen.IsEnabled = false;
            }
        }

        private void cmbJaren_DropDownClosed(object sender, EventArgs e)
        {
            jaartal.Visibility = Visibility.Hidden;
            melding.Visibility = Visibility.Visible;
            this.Refresh();
            if (cmbJaren.SelectedIndex != -1)
            {
                string mydocpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                StringBuilder sb = new StringBuilder();
                var jaar = cmbJaren.SelectedValue.ToString();
                var libraryFactory = new AbonnementenFactory();
                var nietbetalend = new List<NietBetalend>();
                nietbetalend = libraryFactory.GetNietBetalend(jaar);
                if (nietbetalend.Count > 0)
                {
                    sb.AppendLine("titel_en_voornaam;achternaam;adres;postcode;woonplaats;opmerking");
                    foreach (var niet in nietbetalend)
                    {
                        sb.AppendLine(niet.titel + ";" + niet.naam + ";" + niet.adres + ";" + niet.postcode + ";" + niet.woonplaats + ";" + niet.opmerking);
                    }
                    using (StreamWriter outfile = new StreamWriter(mydocpath + @"\herinnering.csv"))
                    {
                        outfile.Write(sb.ToString());
                    }
                    string message = "Gegevens zijn in bestanden gezet die op de desktop te vinden zijn!!!";
                    string caption = "Melding";
                    MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
                    NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Er zijn geen gegevens gevonden!!!", "Gegevens zoeken", MessageBoxButton.OK, MessageBoxImage.Information);
                    jaartal.Visibility = System.Windows.Visibility.Visible;
                    melding.Visibility = System.Windows.Visibility.Hidden;
                    this.Refresh();
                }
            }
        }

        private void cmbJaren_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbJaren.SelectedIndex == -1)
            {
                btnSamenstellen.IsEnabled = false;

            }
            else
            {
                btnSamenstellen.IsEnabled = true;
            }
        }
        
        private void schermSlujiten_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
