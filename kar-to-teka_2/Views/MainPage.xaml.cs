using System;
using System.IO;
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
using MongoDB.Bson;
using Microsoft.Win32;


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
            AddCriminalPhotoViewModel addCriminalPhotoViewModel = new AddCriminalPhotoViewModel();

            foreach (var item in listOfCriminals)
            {
                if (item._id == (string)CriminalsComboBox.SelectedValue)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "png|*.png|jpeg|*.jpg";
                    openFileDialog.Title = "Wybierz zdjęcie przestępcy";

                    if ((bool)openFileDialog.ShowDialog())
                    {
                        string fileName = openFileDialog.FileName;
                        byte[] fileBytes = File.ReadAllBytes(fileName);

                        addCriminalPhotoViewModel.Criminal = (Criminal)CriminalsComboBox.SelectedItem;
                        addCriminalPhotoViewModel.Image = fileBytes;

                        Database database = Database.Instance;
                        database.AddCriminalPhoto(addCriminalPhotoViewModel);

                        MessageBox.Show("Dodano zdjęcie do profilu przestępcy w bazie.", "Sukces!");
                    }
                }
            }
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

                    try
                    {
                        BitmapImage criminalImage = new BitmapImage();
                        using (MemoryStream memoryStream = new MemoryStream(item.Image))
                        {
                            memoryStream.Position = 0;
                            criminalImage.BeginInit();
                            criminalImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            criminalImage.CacheOption = BitmapCacheOption.OnLoad;
                            criminalImage.UriSource = null;
                            criminalImage.StreamSource = memoryStream;
                            criminalImage.EndInit();

                            CriminalImage.Source = criminalImage;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Profil przestępcy nie zawiera zdjęcie. Dodaj zdjęcie, aby uzupełnić profil.", "Problem!");
                    }
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
                        CommittedCrime selectedItem = (CommittedCrime)CommittedCrimesListBox.SelectedItem;
                        if (itemTwo._id == selectedItem._id)
                        {
                            CrimeNameTextBlock.Text = selectedItem.Crime.Name;
                            CrimeParagraphNumberTextBlock.Text = Convert.ToString(selectedItem.Crime.Paragraph);
                            CrimeParagraphDescriptionTextBlock.Text = selectedItem.Crime.Description;
                            CrimeDescriptionTextBlock.Text = selectedItem.Description;
                            CrimeImprisonmentDateTextBlock.Text = selectedItem.ImprisonmentDate.ToString().Substring(0, 10);
                            CrimeImprisonmentLengthTextBlock.Text = Convert.ToString(selectedItem.ImprisonmentLength);
                        }
                    }
                }
            }
        }
    }
}
