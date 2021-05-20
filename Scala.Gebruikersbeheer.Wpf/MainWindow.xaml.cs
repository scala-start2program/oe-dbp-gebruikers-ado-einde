using System;
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
using Scala.Gebruikersbeheer.Core.Entities;
using Scala.Gebruikersbeheer.Core.Services;
namespace Scala.Gebruikersbeheer.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        GebruikersService gebruikersService;
        bool isNew;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gebruikersService = new GebruikersService();
            ClearContents();
            BeeldNormaal();
            PopulateGebruikers();
            PopulateOnderdelen();
        }
        private void BeeldNormaal()
        {
            grpOverzichtGebruikers.IsEnabled = true;
            grpDetailGebruiker.IsEnabled = false;
            grpToegangGebruiker.IsEnabled = true;
            btnGebruikerBewaren.Visibility = Visibility.Hidden;
            btnGebruikerAnnuleren.Visibility = Visibility.Hidden;
        }
        private void BeeldBewerken()
        {
            grpOverzichtGebruikers.IsEnabled = false;
            grpDetailGebruiker.IsEnabled = true;
            grpToegangGebruiker.IsEnabled = false; 
            btnGebruikerBewaren.Visibility = Visibility.Visible;
            btnGebruikerAnnuleren.Visibility = Visibility.Visible;

        }
        private void ClearContents()
        {
            txtEmail.Text = "";
            txtFamilienaam.Text = "";
            txtGebruikersnaam.Text = "";
            txtPaswoord.Text = "";
            txtTelefoon.Text = "";
            txtVoornaam.Text = "";
            lblPaswoordInfo.Visibility = Visibility.Hidden;
            lstToegangTot.ItemsSource = null;
            lstGeenToegangTot.ItemsSource = null;
            lstGeenToegangTot.ItemsSource = gebruikersService.GetOnderdelen();

        }
        private void PopulateGebruikers()
        {
            lstGebruikers.ItemsSource = null;
            lstGebruikers.ItemsSource = gebruikersService.GetAlleGebruikers();
        }
        private void PopulateOnderdelen()
        {
            lstAlleOnderdelen.ItemsSource = null;
            lstAlleOnderdelen.ItemsSource = gebruikersService.GetOnderdelen();
        }
        private void lstGebruikers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearContents();
            if(lstGebruikers.SelectedItem != null)
            {
                Gebruiker gebruiker = (Gebruiker)lstGebruikers.SelectedItem;
                txtGebruikersnaam.Text = gebruiker.Gebruikersnaam;
                txtVoornaam.Text = gebruiker.Voornaam;
                txtFamilienaam.Text = gebruiker.Familienaam;
                txtEmail.Text = gebruiker.Email;
                txtTelefoon.Text = gebruiker.Telefoon;
                ToonRechten(gebruiker);
            }
        }
        private void ToonRechten(Gebruiker gebruiker)
        {
            lstToegangTot.ItemsSource = null;
            lstGeenToegangTot.ItemsSource = null;
            lstToegangTot.ItemsSource = gebruikersService.GetOnderdelenMetToegangVoorGebruiker(gebruiker);
            lstGeenToegangTot.ItemsSource = gebruikersService.GetOnderdelenZonderToegangVoorGebruiker(gebruiker);

        }

        private void btnGebruikerToevoegen_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ClearContents();
            BeeldBewerken();
            txtGebruikersnaam.Focus();
        }

        private void btnGebruikerWijzigen_Click(object sender, RoutedEventArgs e)
        {
            if(lstGebruikers.SelectedItem != null)
            {
                isNew = false;
                BeeldBewerken();
                txtGebruikersnaam.Focus();
                lblPaswoordInfo.Visibility = Visibility.Visible;
            }
        }
        private void btnGebruikerVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            if (lstGebruikers.SelectedItem != null)
            {
                if(MessageBox.Show("Ben je zeker?", "Gebruiker wissen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Gebruiker gebruiker = (Gebruiker)lstGebruikers.SelectedItem;
                    if(gebruikersService.GebruikerVerwijderen(gebruiker))
                    {
                        ClearContents();
                        PopulateGebruikers();
                    }
                    else
                    {
                        MessageBox.Show("DB Error", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }
        private void btnGebruikerAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            ClearContents();
            BeeldNormaal();
            lstGebruikers_SelectionChanged(null, null);
        }

        private void btnGebruikerBewaren_Click(object sender, RoutedEventArgs e)
        {
            string gebruikersnaam = txtGebruikersnaam.Text.Trim();
            string voornaam = txtVoornaam.Text.Trim();
            string familienaam = txtFamilienaam.Text.Trim();
            string email = txtEmail.Text.Trim();
            string telefoon = txtTelefoon.Text.Trim();
            string paswoord = txtPaswoord.Text.Trim();

            Gebruiker gebruiker = null;

            if(gebruikersnaam.Length < 5)
            {
                MessageBox.Show("Een gebruikersnaam dient minstens 5 karakters te zijn !", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtGebruikersnaam.Focus();
                return;
            }
            if(isNew && !gebruikersService.IsGebruikersnaamUniek(gebruikersnaam))
            {
                MessageBox.Show("Deze gebruikersnaam is reeds in gebruik !", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtGebruikersnaam.Focus();
                return;
            }
            if(!isNew)
            {
                gebruiker = (Gebruiker)lstGebruikers.SelectedItem;
                if(gebruikersnaam.ToUpper() != gebruiker.Gebruikersnaam.ToUpper())
                {
                    if(gebruikersService.IsGebruikersnaamUniek(gebruikersnaam))
                    {
                        MessageBox.Show("Deze gebruikersnaam is reeds in gebruik !", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtGebruikersnaam.Focus();
                        return;
                    }
                }
            }
            if(isNew && paswoord.Length < 6)
            {
                MessageBox.Show("Het paswoord dient minstens 6 karakters lang te zijn !", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtPaswoord.Focus();
                return;
            }
            if (!isNew && paswoord != "")
            {
                if (paswoord.Length < 6)
                {
                    MessageBox.Show("Het paswoord dient minstens 6 karakters lang te zijn !", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    txtPaswoord.Focus();
                    return;
                }
            }

            if(isNew)
            {
                gebruiker = new Gebruiker(gebruikersnaam, voornaam, familienaam, email, telefoon, paswoord);
                gebruikersService.GebruikerToevoegen(gebruiker);
            }
            else
            {
                gebruiker.Gebruikersnaam = gebruikersnaam;
                gebruiker.Voornaam = voornaam;
            }
        }

        private void btnToegangVerlenen_Click(object sender, RoutedEventArgs e)
        {
            if (lstGebruikers.SelectedItem == null)
                return;
            if (lstGeenToegangTot.SelectedItem == null)
                return;
            Gebruiker gebruiker = (Gebruiker)lstGebruikers.SelectedItem;
            Onderdeel onderdeel = (Onderdeel)lstGeenToegangTot.SelectedItem;
            gebruikersService.KenRechtToeAanGebruiker(gebruiker, onderdeel);
            ToonRechten(gebruiker);
        }

        private void btnToegangOntnemen_Click(object sender, RoutedEventArgs e)
        {
            if (lstGebruikers.SelectedItem == null)
                return;
            if (lstToegangTot.SelectedItem == null)
                return;
            Gebruiker gebruiker = (Gebruiker)lstGebruikers.SelectedItem;
            Onderdeel onderdeel = (Onderdeel)lstToegangTot.SelectedItem;
            gebruikersService.OntneemRechtVanGebruiker(gebruiker, onderdeel);
            ToonRechten(gebruiker);
        }

        private void lstAlleOnderdelen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lstGebruikersMetToegang.ItemsSource = null;
            lstGebruikersZonderToegang.ItemsSource = null;
            if(lstAlleOnderdelen.SelectedItem != null)
            {
                Onderdeel onderdeel = (Onderdeel)lstAlleOnderdelen.SelectedItem;
                ToonGebruikersVolgensOnderdeel(onderdeel);
            }
        }
        private void ToonGebruikersVolgensOnderdeel(Onderdeel onderdeel)
        {
            lstGebruikersMetToegang.ItemsSource = gebruikersService.GetGebruikersMetToegangTotOnderdeel(onderdeel);
            lstGebruikersZonderToegang.ItemsSource = gebruikersService.GetGebruikersZonderToegangTotOnderdeel(onderdeel);
        }

        private void btnGebruikerRechtToekennen_Click(object sender, RoutedEventArgs e)
        {
            if (lstAlleOnderdelen.SelectedItem == null)
                return;
            if (lstGebruikersZonderToegang.SelectedItem == null)
                return;
            Onderdeel onderdeel = (Onderdeel)lstAlleOnderdelen.SelectedItem;
            Gebruiker gebruiker = (Gebruiker)lstGebruikersZonderToegang.SelectedItem;
            gebruikersService.KenRechtToeAanGebruiker(gebruiker, onderdeel);
            ToonGebruikersVolgensOnderdeel(onderdeel);
        }

        private void btnGebruikerRechtOntnemen_Click(object sender, RoutedEventArgs e)
        {
            if (lstAlleOnderdelen.SelectedItem == null)
                return;
            if (lstGebruikersMetToegang.SelectedItem == null)
                return;
            Onderdeel onderdeel = (Onderdeel)lstAlleOnderdelen.SelectedItem;
            Gebruiker gebruiker = (Gebruiker)lstGebruikersMetToegang.SelectedItem;
            gebruikersService.OntneemRechtVanGebruiker(gebruiker, onderdeel);
            ToonGebruikersVolgensOnderdeel(onderdeel);
        }
    }
}
