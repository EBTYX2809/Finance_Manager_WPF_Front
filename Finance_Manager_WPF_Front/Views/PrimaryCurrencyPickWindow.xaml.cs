using Finance_Manager_WPF_Front.Services;
using Finance_Manager_WPF_Front.ViewModels;
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
using System.Windows.Shapes;

namespace Finance_Manager_WPF_Front.Views
{
    /// <summary>
    /// Interaction logic for PrimaryCurrencyPickWindow.xaml
    /// </summary>
    public partial class PrimaryCurrencyPickWindow : Window
    {
        private readonly UserService _userService;
        public PrimaryCurrencyPickWindow(PrimaryCurrencyPickViewModel viewModel, UserService userService)
        {
            InitializeComponent();
            DataContext = viewModel;
            _userService = userService;

            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _userService.UpdateUserBalanceAsync();
        }
    }
    
}
