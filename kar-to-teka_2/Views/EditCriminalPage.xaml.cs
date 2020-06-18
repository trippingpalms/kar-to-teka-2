using kar_to_teka_2.Models;
using kar_to_teka_2.ViewModels;
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

namespace kar_to_teka_2.Views
{
    /// <summary>
    /// Interaction logic for EditCriminalPage.xaml
    /// </summary>
    public partial class EditCriminalPage : Page
    {
        private Criminal criminal;
        private Database database;

        public EditCriminalPage(Criminal criminal)
        {
            InitializeComponent();

            database = Database.Instance; 

            this.criminal = criminal;

            CriminalNameTextBox.Text = criminal.Name;
            CriminalSurameTextBox.Text = criminal.Surname;
            CriminalNicknameTextBox.Text = criminal.Nickname;
            CriminalResidenceTextBox.Text = criminal.Residence;
            CriminalBirthPlaceTextBox.Text = criminal.BirthPlace;
            if (criminal.BirthDate.ToString().Substring(0, 10) == "01.01.0001")
            {
                CriminalUnknownBirthDateCheckBox.IsChecked = true;
            }
            else
            {
                CriminalBirthDateCalendar.SelectedDate = criminal.BirthDate;
            }
            if (criminal.Wanted == true)
            {
                CriminalWantedCheckBox.IsChecked = true;
            }
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void DeleteCriminal(object sender, RoutedEventArgs e)
        {
            EditCriminalViewModel editCriminalViewModel = new EditCriminalViewModel();

            editCriminalViewModel.Criminal = criminal;
            database.DeleteCriminal(editCriminalViewModel);

            MessageBox.Show("Usunięto przestępcę z bazy.", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
        private void EditCriminal(object sender, RoutedEventArgs e)
        {
            EditCriminalViewModel editCriminalViewModel = new EditCriminalViewModel();

            editCriminalViewModel.Criminal = criminal;
            editCriminalViewModel.Name = CriminalNameTextBox.Text;
            editCriminalViewModel.Surname = CriminalSurameTextBox.Text;
            editCriminalViewModel.Nickname = CriminalNicknameTextBox.Text;
            editCriminalViewModel.Residence = CriminalResidenceTextBox.Text;
            editCriminalViewModel.BirthPlace = CriminalBirthPlaceTextBox.Text;
            if (CriminalUnknownBirthDateCheckBox.IsChecked == false)
            {
                editCriminalViewModel.BirthDate = (DateTime)CriminalBirthDateCalendar.SelectedDate;
            }
            if (CriminalWantedCheckBox.IsChecked == true)
            {
                editCriminalViewModel.Wanted = true;
            }
            else
            {
                editCriminalViewModel.Wanted = false;
            }
            database.EditCriminal(editCriminalViewModel);

            MessageBox.Show("Edytowano dane przestępcy z bazy.", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
    }
}
