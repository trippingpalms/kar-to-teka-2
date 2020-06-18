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
using MongoDB.Bson;

namespace kar_to_teka_2.Views
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Database database;
        private List<Crime> listOfCrimes;
        private List<Criminal> listOfCriminals;
        private List<CommittedCrime> listOfCommittedCrimes;

        public MainPage()
        {
            InitializeComponent();

            database = Database.Instance;
            listOfCrimes = database.GetCrimes();
            listOfCriminals = database.GetCriminals();

            CrimesListBox.DataContext = listOfCrimes;
            CriminalsComboBox.DataContext = listOfCriminals;
        }

        private void AddCriminal(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCriminalPage());
        }
        private void EditCriminal(object sender, RoutedEventArgs e)
        {
            foreach (var item in listOfCriminals)
            {
                if (item._id == (string)CriminalsComboBox.SelectedValue)
                {
                     this.NavigationService.Navigate(new EditCriminalPage(item));
                }
            }        
        }
        private void AddPhoto(object sender, RoutedEventArgs e)
        {

        }

        private void AddCrime(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCrimePage());
        }
        private void AssignCrime(object sender, RoutedEventArgs e)
        {
            foreach (var item in listOfCriminals)
            {
                if (item._id == (string)CriminalsComboBox.SelectedValue)
                {
                    this.NavigationService.Navigate(new AssignCrimePage(item, listOfCrimes));
                }
            }
        }
        private void UnassignCrime(object sender, RoutedEventArgs e)
        {
            foreach (var item in listOfCriminals)
            {
                if (item._id == (string)CriminalsComboBox.SelectedValue)
                {
                    this.NavigationService.Navigate(new UnassignCrimePage(item));
                }
            }
            
        }

        private void ShowStatistics(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new StatisticsPage());
        }

        private void DisplayCriminalData(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in listOfCriminals)
            {
                if (item._id == (string)CriminalsComboBox.SelectedValue)
                {
                    CriminalNameTextBlock.Text = item.Name;
                    CriminalSurnameTextBlock.Text = item.Surname;
                    CriminalNicknameTextBlock.Text = item.Nickname;
                    CriminalResidenceTextBlock.Text = item.Residence;
                    CriminalBirthPlaceTextBlock.Text = item.BirthPlace;
                    if (item.BirthDate.ToString().Substring(0, 10) == "01.01.0001")
                    {
                        CriminalBirthDateTextBlock.Text = "Nieznana";
                    }
                    else
                    {
                        CriminalBirthDateTextBlock.Text = item.BirthDate.ToString().Substring(0, 10);
                    }
                    CriminalWantedTextBlock.Text = item.Wanted ? "Tak" : "Nie";
                    CriminalLeavingDateTextBlock.Text = "---";

                    listOfCommittedCrimes = item.CommittedCrimes;
                    CommittedCrimesListBox.DataContext = item.CommittedCrimes;
                }
            }
        }
        private void DisplayCommittedCrimeData(object sender, SelectionChangedEventArgs e)
        {
            foreach (var itemOne in listOfCriminals)
            {
                if (itemOne._id == (string)CriminalsComboBox.SelectedValue)
                {
                    listOfCommittedCrimes = itemOne.CommittedCrimes;
                    CommittedCrimesListBox.DataContext = listOfCommittedCrimes;

                    foreach (var itemTwo in listOfCommittedCrimes)
                    {
                        if (itemTwo._id == (ObjectId)CommittedCrimesListBox.SelectedValue)
                        {

                        }
                    }
                }
            }
        }
    }
}
