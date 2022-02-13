using System;
using System.Collections.Generic;
using System.IO;
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

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_Instellingen.xaml
    /// </summary>
    public partial class pg_Instellingen : Page
    {
        public pg_Instellingen()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            txtExcelDir.Text = Functions.sDirectory("ConfigData.xml");
            txtExcelBestand.Text = Functions.sFile("ConfigData.xml");
            txtHOST.Text = Functions.sHost("ConfigData.xml");
            txtDATABASE.Text = Functions.sDatabase("ConfigData.xml");
            txtUSERNAME.Text = Functions.sUsername("ConfigData.xml");
            txtPASSWORD.Password = Functions.sPassword("ConfigData.xml");
            txtPORT.Text = Functions.sPort("ConfigData.xml");
            txtAantalBedankjes.Text = Functions.sAantalBedankjes("ConfigData.xml");
            txtMininaalBedankjes.Text = Functions.sBedragBedankjes("ConfigData.xml");
            txtDir.Text = Functions.sDirectoryTemps("ConfigData.xml");
            
        }

        private void btnInstellingenOpslaan_Click(object sender, RoutedEventArgs e)
        {
            var waardes = bouwString();
            opslaanSettings(Directory.GetCurrentDirectory(), waardes);

            MessageBox.Show("Gegevens zijn opgeslagen", "Opslaan instellingen", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void btnInstellingenAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void opslaanSettings(string sDir, string waardes)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(sDir + "\\ConfigData.xml");
            file.WriteLine(waardes);
            file.Close();
        }

        private string bouwString()
        {
            char c = (char)34;
            var sString = "<?xml version=" + c + "1.0" + c + " encoding=" + c + "utf-8" + c + "?>\r\n";
            sString += "<configdata>\r\n";
            sString += "\t<dir>" + txtExcelDir.Text + "</dir>\r\n";
            sString += "\t<file>" + txtExcelBestand.Text + "</file>\r\n";
            sString += "\t<connection>\r\n";
            sString += "\t\t<host>" + txtHOST.Text + "</host>\r\n";
            sString += "\t\t<database>" + txtDATABASE.Text + "</database>\r\n";
            sString += "\t\t<username>" + txtUSERNAME.Text + "</username>\r\n";
            sString += "\t\t<password>" + txtPASSWORD.Password + "</password>\r\n";
            sString += "\t\t<port>" + txtPORT.Text + "</port>\r\n";
            sString += "\t</connection>\r\n";
            sString += "\t<aantal>" + txtAantalBedankjes.Text + "</aantal>\r\n";
            sString += "\t<bedrag>" + txtMininaalBedankjes.Text + "</bedrag>\r\n";
            sString += "\t<tempdir>" + txtDir.Text + "</tempdir>\r\n";
            sString += "</configdata>";
            return sString;
        }
    }
}
