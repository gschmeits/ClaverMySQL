using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using DataStorage.Classes;

namespace ClaverMySQL
{
    /// <summary>
    /// Interaction logic for pg_PerPersoon.xaml
    /// </summary>
    public partial class pg_PerPersoon : Page
    {
        public pg_PerPersoon()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var libraryFactory = new PerPersoonFactory();
            var perpersonen = new List<PerPersoonAll>();
            perpersonen = libraryFactory.GetPerPersoonAll();

            cmbAdresnaam.Items.Clear();
            foreach (var persoon in perpersonen)
            {
                cmbAdresnaam.Items.Add(persoon.naam);
            }

            cmbAdresnummer.Items.Clear();
            perpersonen = libraryFactory.GetPerPersoonAllNummer();
            foreach (var persoon in perpersonen)
            {
                cmbAdresnummer.Items.Add(persoon.adresnummer);
            }
        }

        private void cmbAdresnummer_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbAdresnummer.SelectedIndex != -1)
            {
                cmbAdresnaam.SelectedIndex = cmbAdresnummer.SelectedIndex;
                VulGegevens();
            }
        }

        private void cmbAdresnaam_DropDownClosed(object sender, EventArgs e)
        {
            if (cmbAdresnaam.SelectedIndex != -1)
            {
                cmbAdresnummer.SelectedIndex = cmbAdresnaam.SelectedIndex;
                VulGegevens();
            }
        }

        private void VulGegevens()
        {
            var libraryFactory = new PerPersoonFactory();
            var perpersoon = new List<PerPersoon>();
            perpersoon = libraryFactory.GetPerPersoon(cmbAdresnummer.SelectedValue.ToString());
            naam.Content = perpersoon[0].naam;
            adres.Content = perpersoon[0].adres;
            postcodePlaats.Content = perpersoon[0].plaats;

            var donaties = new List<Donatie>();
            donaties = libraryFactory.GetDonaties(cmbAdresnummer.SelectedValue.ToString());
            decimal bedrag = 0;
            gegevensDonatie.Text = "";
            foreach (var donatie in donaties)
            {
                string[] s = donatie.boekdatum.Split(' ');
                bedrag += Convert.ToDecimal(donatie.bedrag);
                gegevensDonatie.Text += string.Format("{0,-30}{1,15}{2,20:N2}    {3,-55}", donatie.codeomschrijving, s[0], donatie.bedrag, donatie.omschrijving) + "\r\n";
            }
            gegevensDonatie.Text += string.Format("{0,45}{1,20}", " ", "---------------") + "\r\n";
            gegevensDonatie.Text += string.Format("{0,45}{1,20:N2}", " ", bedrag) + "\r\n";
            gegevensDonatie.Text += string.Format("{0,45}{1,20}", " ", "===============") + "\r\n";
        }
    }
}
