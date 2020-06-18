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
    /// Interaction logic for AssignCrimePage.xaml
    /// </summary>
    public partial class AssignCrimePage : Page
    {
        private Criminal criminal;
        private List<Crime> listOfCrimes;

        public AssignCrimePage(Criminal criminal, List<Crime> listOfCrimes)
        {
            InitializeComponent();

            this.criminal = criminal;
            this.listOfCrimes = listOfCrimes;
            CrimesComboBox.DataContext = this.listOfCrimes;
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void AssignCrime(object sender, RoutedEventArgs e)
        {
            AssignCrimeViewModel assignCrimeViewModel = new AssignCrimeViewModel();
            DateTime selectedDate;

            assignCrimeViewModel.Criminal = criminal;
            foreach (var item in listOfCrimes)
            {
                if (item._id == (string)CrimesComboBox.SelectedValue)
                {
                    assignCrimeViewModel.Crime = item;
                }
            }
            assignCrimeViewModel.Description = CrimeDescriptionTextBox.Text;
            selectedDate = (DateTime)ImprisonmentDateCalendar.SelectedDate;
            assignCrimeViewModel.ImprisonmentDate = selectedDate.AddDays(1);
            assignCrimeViewModel.ImprisonmentLength = Convert.ToInt32(ImprisonmentLengthTextBox.Text);

            Database database = Database.Instance;
            database.AssignCrime(assignCrimeViewModel);

            MessageBox.Show("Przypisano przestępstwo do przestępcy z bazy.", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
    }
}
