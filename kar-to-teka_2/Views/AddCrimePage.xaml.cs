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
using kar_to_teka_2.Models;
using kar_to_teka_2.ViewModels;

namespace kar_to_teka_2.Views
{
    /// <summary>
    /// Interaction logic for AddCrimePage.xaml
    /// </summary>
    public partial class AddCrimePage : Page
    {
        public AddCrimePage()
        {
            InitializeComponent();
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void AddCrime(object sender, RoutedEventArgs e)
        {
            AddCrimeViewModel addCrimeViewModel = new AddCrimeViewModel();

            try
            {
                addCrimeViewModel.Paragraph = Convert.ToInt32(ParagraphNumberTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wpisana wartość nie jest liczbą. Wpisz liczbę, by uzyskać poprawny rezultat.", "Błąd!");
            }
            if (String.IsNullOrEmpty(CrimeNameTextBox.Text) || String.IsNullOrWhiteSpace(CrimeNameTextBox.Text))
            {
                MessageBox.Show("Nie wpisano tekstu. Wpisz tekst, by uzyskać poprawny rezultat.", "Błąd!");
            }
            else
            {
                addCrimeViewModel.Name = CrimeNameTextBox.Text;
            }
            if (String.IsNullOrEmpty(CrimeDescriptionTextBox.Text) || String.IsNullOrWhiteSpace(CrimeDescriptionTextBox.Text))
            {
                MessageBox.Show("Nie wpisano tekstu. Wpisz tekst, by uzyskać poprawny rezultat.", "Błąd!");
            }
            else
            {
                addCrimeViewModel.Description = CrimeDescriptionTextBox.Text;
            }

            Database database = Database.Instance;
            database.AddCrime(addCrimeViewModel);

            MessageBox.Show("Dodano nowy akt prawny do bazy", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
    }
}
