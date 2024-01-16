using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace TrafficSignals.TrafficViewModel
{
    public class NumericTextBoxBehavior
    {
        public static readonly DependencyProperty AllowNumericOnlyProperty =
            DependencyProperty.RegisterAttached(
                "AllowNumericOnly",
                typeof(bool),
                typeof(NumericTextBoxBehavior),
                new PropertyMetadata(false, OnAllowNumericOnlyChanged));

        public static bool GetAllowNumericOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowNumericOnlyProperty);
        }

        public static void SetAllowNumericOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowNumericOnlyProperty, value);
        }

        private static void OnAllowNumericOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.PreviewTextInput += NumericOnlyTextBoxPreviewTextInput;
                    textBox.PreviewKeyDown += NumericOnlyTextBoxPreviewKeyDown;
                }
                else
                {
                    textBox.PreviewTextInput -= NumericOnlyTextBoxPreviewTextInput;
                    textBox.PreviewKeyDown -= NumericOnlyTextBoxPreviewKeyDown;
                }
            }
        }

        private static void NumericOnlyTextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true; // Ignore non-numeric input
            }
        }

        private static void NumericOnlyTextBoxPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true; // Ignore space key
            }
        }
    }
}
