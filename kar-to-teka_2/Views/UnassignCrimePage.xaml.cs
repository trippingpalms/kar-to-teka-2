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
using kar_to_teka_2.Models;
using kar_to_teka_2.ViewModels;
using MongoDB.Bson;

namespace kar_to_teka_2.Views
{
    /// <summary>
    /// Interaction logic for UnassignCrimePage.xaml
    /// </summary>
    public partial class UnassignCrimePage : Page
    {
        private Criminal criminal;
        private List<CommittedCrime> listOfCommittedCrimes;

        public UnassignCrimePage(Criminal criminal)
        {
            InitializeComponent();

            this.criminal = criminal;
            listOfCommittedCrimes = criminal.CommittedCrimes;

            CommittedCrimesComboBox.DataContext = listOfCommittedCrimes;
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }

        private void UnassignCrime(object sender, RoutedEventArgs e)
        {
            UnassignCrimeViewModel unassignCrimeViewModel = new UnassignCrimeViewModel();

            unassignCrimeViewModel.Criminal = criminal;
            unassignCrimeViewModel.CommittedCrimes = criminal.CommittedCrimes;

            foreach (var item in listOfCommittedCrimes)
            {
                if (item._id == (ObjectId)CommittedCrimesComboBox.SelectedValue)
                {
                    criminal.CommittedCrimes.Remove(item);
                }
            }

            MessageBox.Show("Przestępstwo zostało usunięte z profilu przestępcy w bazie.", "Sukces!");

            this.NavigationService.Navigate(new MainPage());
        }
    }
}