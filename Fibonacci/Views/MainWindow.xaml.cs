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

namespace Fibonacci.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtnummin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text,0)) e.Handled = true;
        }

        private void txtnummax_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text,0)) e.Handled = true;
        }

        private void txtnumfib_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text,0)) e.Handled = true;
        }

        private bool isFocused = false;
        private void txtnummin_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused) {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }

        private void txtnummin_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void txtnummax_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void txtnummax_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused) {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }

        private void txtnumfib_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (isFocused) {
                isFocused = false;
                (sender as TextBox).SelectAll();
            }
        }

        private void txtnumfib_GotFocus(object sender, RoutedEventArgs e)
        {
            isFocused = true;
        }

        private void primenumbers_Click(object sender, RoutedEventArgs e)
        {
            txtnummin.Text = "2";
            txtnummax.Text = "200000000";

        }

        private void btnstopfib_Click(object sender, RoutedEventArgs e)
        {
            txtnumfib.Text = "0";
        }
    }
}
