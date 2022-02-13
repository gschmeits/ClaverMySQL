using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Office.Interop.Outlook;
using OutlookApp = Microsoft.Office.Interop.Outlook.Application;
using System;
using System.Windows.Forms;
using DataStorage;
using DataStorage.Classes;
using DataStorage.Data;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_DankGlobaal.xaml
    /// </summary>
    public partial class pg_DankGlobaal : Page
    {
        public pg_DankGlobaal()
        {
            InitializeComponent();
            melding.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var libraryFactory = new BedankjesFactory();
            var bedankjes = new List<AantalBedankje>();
            bedankjes = libraryFactory.GetAantalBedankjes();

            if (bedankjes[0].aantal == 0)
            {
                var caption = "Aanmaken bedankjes";
                var message = "Er hoeven geen dankbrieven gemaakt te worden.";
                // WPFMessageBox.Show(caption, message, WPFMessageBoxButtons.OK, WPFMessageBoxImage.Information);
                System.Windows.MessageBox.Show(message, caption, MessageBoxButton.OK,
                    MessageBoxImage.Information);

                NavigationService.GoBack();
            }
        }

        private void stelSamen_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxBrief.IsChecked == false &&
                 CheckBoxEmail.IsChecked == false)
            {
                System.Windows.MessageBox.Show("Selecteer mininmaal de brieven en/of email",
                    "Melding", MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
            }
            else
            {
                stelSamen.Visibility = Visibility.Hidden;
                melding.Visibility = Visibility.Visible;

                var date1 = DateTime.Now;
                var strDatum = date1.Year +
                               date1.Month.ToString() +
                               date1.Day + date1.Hour +
                               date1.Minute +
                               date1.Second;

                var mydocpath =
                    Environment.GetFolderPath(Environment.SpecialFolder
                        .Desktop);
                var sb = new StringBuilder();

                var libraryFactory = new BedankjesFactory();

                if (CheckBoxBrief.IsChecked == true)
                {
                    var bedankjes = new List<BedankjeHoger>();

                    bedankjes = libraryFactory.GetBedankjeHoger();
                    sb.AppendLine(
                        "titel_en_voornaam;achternaam;adres;postcode;woonplaats;email;bedrag");
                    foreach (var bedankje in bedankjes)
                        sb.AppendLine(bedankje.titel + ";" + bedankje.naam +
                                      ";" +
                                      bedankje.adres + ";" + bedankje.postcode +
                                      ";" + bedankje.woonplaats + ";" +
                                      bedankje.email + ";" + bedankje.bedrag);

                    using (var outfile =
                        new StreamWriter(mydocpath + @"\dankbriefMeer.csv"))
                    {
                        outfile.Write(sb.ToString());
                    }

                    label1.Content =
                        "De gegevens voor de brieven waar de bedragen";
                    label2.Content =
                        "kleiner dan € 10,00 zijn, worden aangemaakt.";
                    var sbL = new StringBuilder();

                    var bedankjesL = new List<BedankjeLager>();
                    bedankjesL = libraryFactory.GetBedankjeLager();

                    sbL.AppendLine(
                        "titel_en_voornaam;achternaam;adres;postcode;woonplaats;email;bedrag");
                    foreach (var bedankje in bedankjes)
                        sbL.AppendLine(bedankje.titel + ";" + bedankje.naam +
                                       ";" +
                                       bedankje.adres + ";" +
                                       bedankje.postcode +
                                       ";" + bedankje.woonplaats + ";" +
                                       bedankje.email + bedankje.bedrag);

                    using (var outfile =
                        new StreamWriter(mydocpath + @"\dankbriefMinder.csv"))
                    {
                        outfile.Write(sbL.ToString());
                    }

                    // Update Bedankje op 0 zetten
                    var sSQL = clsDataDank.updateDankMutaties();
                    GenericDataRead.INUPDEL(sSQL);

                    var message =
                        "Gegevens zijn in bestanden gezet die op de desktop te vinden zijn!!!";
                    var caption = "Melding";
                    System.Windows.MessageBox.Show(message, caption, MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }

                if (CheckBoxEmail.IsChecked == true)
                {
                    var bedankjesEmail = new List<BedankjeHoger>();
                    bedankjesEmail = libraryFactory.GetBedankjeHogerEmail();

                    var string1 = readHTMLfile();


                    foreach (var bedankje_per_mail in bedankjesEmail)
                    {
                        var string2 = string1.Replace("{{aanspreektitel}}",
                            bedankje_per_mail.titel + " " +
                            bedankje_per_mail.naam);

                        string1 = string2.Replace("{{datum}}", DateTime.Now.ToString("dd MMMM yyyy"));
                        string2 = string1.Replace("{{bedrag}}",
                            $"{bedankje_per_mail.bedrag:C2}");

                        var outlookApp = new OutlookApp();
                        MailItem mailItem =
                            outlookApp.CreateItem(OlItemType.olMailItem);
                        mailItem.Subject = "Bedankje";
                        mailItem.HTMLBody = string2;
                        mailItem.To = bedankje_per_mail.email;
                        mailItem.Display(true);
                    }

                    // Update Bedankje Emails op 0 zetten
                    var sSQL = clsDataDank.updateDankMutatiesEmail();
                    GenericDataRead.INUPDEL(sSQL);
                }

                melding.Visibility = Visibility.Hidden;
                stelSamen.Visibility = Visibility.Visible;
            }
        }

        private string readHTMLfile()
        {
            string string1 = String.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                string1 = File.ReadAllText(openFileDialog.FileName);
            }
            return string1;
        }

        private void schermSlujiten_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
