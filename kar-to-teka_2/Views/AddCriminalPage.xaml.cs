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
    /// Interaction logic for AddCriminalPage.xaml
    /// </summary>
    public partial class AddCriminalPage : Page
    {
        public AddCriminalPage()
        {
            InitializeComponent();
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void AddCriminal(object sender, RoutedEventArgs e)
        {
            AddCriminalViewModel addCriminalViewModel = new AddCriminalViewModel();

            addCriminalViewModel.Name = CriminalNameTextBox.Text;
            addCriminalViewModel.Surname = CriminalSurameTextBox.Text;
            addCriminalViewModel.Nickname = CriminalNicknameTextBox.Text;
            addCriminalViewModel.Residence = CriminalResidenceTextBox.Text;
            addCriminalViewModel.BirthPlace = CriminalBirthPlaceTextBox.Text;
            if (CriminalUnknownBirthDateCheckBox.IsChecked == false)
            {
                addCriminalViewModel.BirthDate = (DateTime)CriminalBirthDateCalendar.SelectedDate;
            }
            if (CriminalWantedCheckBox.IsChecked == true)
            {
                addCriminalViewModel.Wanted = true;
            }
            else
            {
                addCriminalViewModel.Wanted = false;
            }

            Database database = Database.Instance;
            database.AddCriminal(addCriminalViewModel);

            MessageBox.Show("Dodano przestępcę do bazy.", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
    }
}
