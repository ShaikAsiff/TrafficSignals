using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using TrafficSignals.TrafficViewModel;

namespace TrafficSignals
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
          //  DataContext = new TrafficLightViewModel();
        }

        //private void GreenDuration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //   e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        //}

        //private void Green_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.V && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        //    {
        //        string clipboardText = Clipboard.GetText();
        //        if (!int.TryParse(clipboardText, out _))
        //        {
        //            e.Handled = true; // Prevent pasting non-numeric values
        //        }
        //    }
        //}
    }
}
