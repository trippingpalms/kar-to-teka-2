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

namespace kar_to_teka_2.Views
{
    /// <summary>
    /// Interaction logic for StatisticsPage.xaml
    /// </summary>
    public partial class StatisticsPage : Page
    {
        private List<string> listOfStatistics;

        public StatisticsPage()
        {
            InitializeComponent();

            Database database = Database.Instance;
            listOfStatistics = database.GetStatistics();

            NumberOfCrimesTextBlock.Text = listOfStatistics[0];
            NumberOfCriminalsTextBlock.Text = listOfStatistics[1];
        }

        private void ReturnToMainPage(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainPage());
        }
    }
}
