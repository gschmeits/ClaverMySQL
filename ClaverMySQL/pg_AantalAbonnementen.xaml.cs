using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ClaverMySQL.Utils;
using DataStorage.Classes;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_AantalAbonnementen.xaml
    /// </summary>
    public partial class pg_AantalAbonnementen : Page
    {
        private static string soortGegeven;

        public pg_AantalAbonnementen()
        {
            InitializeComponent();
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("nl-NL");
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            soortGegeven = DatumZettenHalen.sOverzicht;
            switch (soortGegeven)
            {
                case "Echo":
                    LabelHeader.Content = "Overzicht aantal Echo uit Afrika per letterA";
                    AantalEcho();
                    break;
                case "Kalender":
                    LabelHeader.Content = "Overzicht aantal Kalender per letterA";
                    AantalKalender();
                    break;
                case "Abonnementen":
                    LabelHeader.Content = "Overzicht aantal abonnementen per letterA";
                    AantalAbonnementen();
                    break;
            }
        }
        
        private void AantalAbonnementen()
        {
            var libraryFactory = new AantalFactory();
            var abonnementen = new List<Abonnement>();
            abonnementen = libraryFactory.GetAantalAbonnementen();
            dtgEcho.ItemsSource = abonnementen;
            dtgEcho.DataContext = abonnementen;
            var totaalaantal = 0;
            foreach (var abonnement in abonnementen) totaalaantal += abonnement.aantal;
            totaalAantal.Text = string.Format("{0:N0}", totaalaantal);
        }

        private void AantalEcho()
        {
            var libraryFactory = new AantalFactory();
            var echos = new List<Echo>();
            echos = libraryFactory.GetAantalEchos();
            dtgEcho.ItemsSource = echos;
            dtgEcho.DataContext = echos;
            var totaalaantal = 0;
            foreach (var echo in echos) totaalaantal += echo.aantal;
            totaalAantal.Text = string.Format("{0:N0}", totaalaantal);
        }

        private void AantalKalender()
        {
            var libraryFactory = new AantalFactory();
            var kalenders = new List<Kalender>();
            kalenders = libraryFactory.GetAantalKalenders();
            dtgEcho.ItemsSource = kalenders;
            dtgEcho.DataContext = kalenders;
            var totaalaantal = 0;
            foreach (var kalender in kalenders) totaalaantal += kalender.aantal;
            totaalAantal.Text = string.Format("{0:N0}", totaalaantal);
        }
    }
}
