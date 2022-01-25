﻿using System;
using System.Collections.Generic;
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
using ClaverMySQL.Utils;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataStorage.Functions.GetCurrentDir(1);
            DataStorage.Functions.InitializeDatabaseConnection(false);
            try
            {
                frame1.Source = new Uri("pg_Logo.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItemBankafscriten_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnu_Stamgegevens_Adressen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_Adressen.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void mnu_Stamgegevens_Grootboekrekeningen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_Grootboek.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); 
            }
        }

        private void mnu_Stamgegevens_Sub_codes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_SubCodes.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_Stamgegevens_Einde_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var strVraag = "Weet u zeker dat u de applicatie wilt afsluiten?";

                MessageBoxResult result = MessageBox.Show(strVraag, "Applicatie afsluiten", MessageBoxButton.OKCancel);

                if (result == MessageBoxResult.OK)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_Boekhouding_Boeken_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_Boekhouding.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void mnu_Boekhouding_Controle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_ControleDetails.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void Mnu_BoekhoudingControlePerRekening_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_ControlePerKostenplaats.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void Mnu_BoekhoudingControleGrootboek_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_ControlePerGrootboek.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void mnu_Overzichten_Maandoverzichten_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnu_Overzichten_PerPersoon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_PerPersoon.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void mnu_Overzichten_Aantallen_Abonnementen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatumZettenHalen.sOverzicht = "Abonnementen";
                frame1.Content = null;
                frame1.Source = new Uri("pg_AantalAbonnementen.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void mnu_Overzichten_Aantallen_EchoUitAfrika_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatumZettenHalen.sOverzicht = "Echo";
                frame1.Content = null;
                frame1.Source = new Uri("pg_AantalEchoUitAfrika.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void _mnu_Overzichen_Aantallen_Kalender_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DatumZettenHalen.sOverzicht = "Kalender";
                frame1.Content = null;
                frame1.Source = new Uri("pg_AantalKalender.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void mnu_Printen_Acceptgiro_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                frame1.Source = new Uri("pg_Acceptgiro.xaml", UriKind.RelativeOrAbsolute);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Log exception, then show it to the user with apologies...
            }
        }

        private void _mnu_Printen_Dank_Globaal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _mnu_Printen_Dank_Individueel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void extraInstellingen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnu_Extra_SaldiAanpassen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _mnu_Extra_NietBetalenden_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Mnu_Extra_Email_Click(object sender, RoutedEventArgs e)
        {

        }

        private void beginscherm_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
