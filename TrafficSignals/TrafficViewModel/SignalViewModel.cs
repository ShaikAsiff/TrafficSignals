using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TrafficSignals.Commands;

namespace TrafficSignals.TrafficViewModel
{
    public class TrafficLightViewModel : INotifyPropertyChanged
    {
        

        private static SolidColorBrush Red = Brushes.Red;
        private static SolidColorBrush Green = Brushes.Green;
        private static SolidColorBrush Gray = Brushes.Gray;


        private int _greenLightDuration = 5;
        //public int GreenLightDuration
        //{
        //    get { return _greenLightDuration; }
        //    set
        //    {
        //        if (value > 0 )
        //        {


        //            if (_greenLightDuration != value)
        //            {
        //                _greenLightDuration = value;
        //                OnPropertyChanged("GreenLightDuration");
        //            }
        //        }
        //        else
        //        {

        //            MessageBox.Show("value should not be zero", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //}
        public int GreenLightDuration
        {
            get { return _greenLightDuration; }
            set
            {


                {
                    if (_greenLightDuration != value)
                    {
                        _greenLightDuration = value;
                        OnPropertyChanged(nameof(GreenLightDuration));
                    }
                }
            }
        }

        //private static void GreenDuration_(object sender, TextCompositionEventArgs  e)
        //{
        //    e.Handled = new Regex("[^0-9]").IsMatch(e.Text);
        //}

        private SolidColorBrush _northlightColor = Red;
        public SolidColorBrush NorthLightColor
        {
            get { return _northlightColor; }
            set
            {
                if (_northlightColor != value)
                {
                    _northlightColor = value;
                    OnPropertyChanged("NorthLightColor");
                }
            }
        }

        private int _northcountdown;
        public int NorthCountdown
        {
            get { return _northcountdown; }
            set
            {
                if (_northcountdown != value)
                {
                    _northcountdown = value;
                    OnPropertyChanged("NorthCountdown");
                }
            }
        }

        private SolidColorBrush _eastLightColor = Red;
        public SolidColorBrush EastLightColor
        {
            get { return _eastLightColor; }
            set
            {
                if (_eastLightColor != value)
                {
                    _eastLightColor = value;
                    OnPropertyChanged("EastLightColor");
                }
            }
        }

        private int _eastCountdown;
        public int EastCountdown
        {
            get { return _eastCountdown; }
            set
            {
                if (_eastCountdown != value)
                {
                    _eastCountdown = value;
                    OnPropertyChanged("EastCountdown");
                }
            }
        }

        private SolidColorBrush _southLightColor = Red;
        public SolidColorBrush SouthLightColor
        {
            get { return _southLightColor; }
            set
            {
                if (_southLightColor != value)
                {
                    _southLightColor = value;
                    OnPropertyChanged("SouthLightColor");
                }
            }
        }

        private int _southCountdown;
        public int SouthCountdown
        {
            get { return _southCountdown; }
            set
            {
                if (_southCountdown != value)
                {
                    _southCountdown = value;
                    OnPropertyChanged("SouthCountdown");
                }
            }
        }

        private SolidColorBrush _westLightColor = Brushes.Red;
        public SolidColorBrush WestLightColor
        {
            get { return _westLightColor; }
            set
            {
                if (_westLightColor != value)
                {
                    _westLightColor = value;
                    OnPropertyChanged("WestLightColor");
                }
            }
        }

        private int _westCountdown;
        public int WestCountdown
        {
            get { return _westCountdown; }
            set
            {
                if (_westCountdown != value)
                {
                    _westCountdown = value;
                    OnPropertyChanged("WestCountdown");
                }
            }
        }
        private bool _isNorthEnabled = true;
        public bool IsNorthEnabled
        {
            get { return _isNorthEnabled; }
            set
            {
                if (_isNorthEnabled != value)
                {
                    _isNorthEnabled = value;
                    OnPropertyChanged(nameof(IsNorthEnabled));


                }

            }
        }

        private bool _isEastEnabled = true;
        public bool IsEastEnabled
        {
            get { return _isEastEnabled; }
            set
            {
                if (_isEastEnabled != value)
                {
                    _isEastEnabled = value;
                    OnPropertyChanged(nameof(IsEastEnabled));


                }

            }
        }
        private bool _isSouthEnabled = true;
        public bool IsSouthEnabled
        {
            get { return _isSouthEnabled; }
            set
            {
                if (_isSouthEnabled != value)
                {
                    _isSouthEnabled = value;
                    OnPropertyChanged(nameof(IsSouthEnabled));


                }

            }
        }


        private bool _isWestEnabled = true;
        public bool IsWestEnabled
        {
            get { return _isWestEnabled; }

            set
            {
                if (_isWestEnabled != value)
                {
                    _isWestEnabled = value;
                    OnPropertyChanged(nameof(IsWestEnabled));


                }

            }
        }

        private bool _isRunning;
        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged("IsRunning");

                }
            }
        }


        private bool _isNotRunning = true;
        public bool IsNotRunning
        {
            get { return _isNotRunning; }
            set
            {
                if (_isNotRunning != value)
                {
                    _isNotRunning = value;
                    OnPropertyChanged(nameof(IsNotRunning));
                }
            }
        }
        // public bool IsNotRunning => !IsRunning;

        public ICommand StartCommand { get; }
        public ICommand StopCommand { get; }

        private DispatcherTimer _timer;

        public TrafficLightViewModel()
        {
            MainWindow mainWindow = new MainWindow();

            // Set the DataContext of MainWindow to this instance of TrafficLightViewModel
            mainWindow.DataContext = this;

            // Show the MainWindow
            mainWindow.Show();



            StartCommand = new RelayCommand(StartTrafficLight, () => IsNotRunning);
            StopCommand = new RelayCommand(StopTrafficLight, () => IsRunning);

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(1000);
            _timer.Tick += Timer_Tick;

           

            //Application.Current.MainWindow.DataContext = this;



        }



        private void Timer_Tick(Object? sender, EventArgs e)
        {
            if (IsRunning)
            {

                if (IsNorthEnabled)
                {
                    NorthCountdown--;

                    if (NorthCountdown == 0)
                    {
                        NorthLightColor = NorthLightColor.Color == Colors.Red
                            ? Green
                            : Red;

                        NorthCountdown = NorthLightColor.Color == Colors.Green ? GreenLightDuration : (GreenLightDuration * 3);


                    }
                }
                else
                {
                    NorthLightColor = Gray;
                    NorthCountdown = 0;


                }
                if (IsEastEnabled)
                {
                    EastCountdown--;
                    if (EastCountdown == 0)
                    {
                        EastLightColor = EastLightColor.Color == Colors.Red
                            ? Green
                            : Red;

                        EastCountdown = EastLightColor.Color == Colors.Green ? GreenLightDuration : (GreenLightDuration * 3);

                    }

                }
                else
                {
                    EastLightColor = Gray;
                    EastCountdown = 0;
                }
                if (IsSouthEnabled)
                {
                    SouthCountdown--;
                    if (SouthCountdown == 0)
                    {
                        SouthLightColor = SouthLightColor.Color == Colors.Red
                            ? Green
                            : Red;

                        SouthCountdown = SouthLightColor.Color == Colors.Green ? GreenLightDuration : (GreenLightDuration * 3);

                    }
                }
                else
                {
                    SouthLightColor = Gray;
                    SouthCountdown = 0;
                }
                if (IsWestEnabled)
                {

                    WestCountdown--;
                    if (WestCountdown == 0)
                    {
                        WestLightColor = WestLightColor.Color == Colors.Red
                            ? Green
                            : Red;

                        WestCountdown = WestLightColor.Color == Colors.Green ? GreenLightDuration : (GreenLightDuration * 3);


                    }
                }
                else
                {
                    WestLightColor = Gray;
                    WestCountdown = 0;
                }
            }
        }



        private void StartTrafficLight()
        {
            IsRunning = true;
            IsNotRunning = false;
            _timer.Start();

            int enabledLightsCount = GreenLightDuration;
            switch (true)
            {
                case true when IsNorthEnabled && IsEastEnabled && IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Red;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount * 2;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount * 3;
                    break;
                case true when !IsNorthEnabled && IsEastEnabled && IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Green;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount * 2;
                    break;
                case true when IsNorthEnabled && !IsEastEnabled && IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount * 2;
                    break;
                case true when IsNorthEnabled && IsEastEnabled && !IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Red;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount * 2;
                    break;
                case true when IsNorthEnabled && IsEastEnabled && IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Red;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount * 2;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;

                //2
                case true when !IsNorthEnabled && !IsEastEnabled && IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Green;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount;
                    break;
                case true when IsNorthEnabled && IsEastEnabled && !IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Red;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;
                case true when IsNorthEnabled && !IsEastEnabled && !IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount;
                    break;
                case true when !IsNorthEnabled && IsEastEnabled && IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Green;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;
                case true when !IsNorthEnabled && IsEastEnabled && !IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Green;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Red;
                    WestCountdown = enabledLightsCount;
                    break;
                case true when IsNorthEnabled && !IsEastEnabled && IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Red;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;

                //3
                case true when !IsNorthEnabled && !IsEastEnabled && !IsSouthEnabled && IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Green;
                    WestCountdown = enabledLightsCount;
                    break;
                case true when IsNorthEnabled && !IsEastEnabled && !IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Green;
                    NorthCountdown = enabledLightsCount;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;
                case true when !IsNorthEnabled && IsEastEnabled && !IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Green;
                    EastCountdown = enabledLightsCount;

                    SouthLightColor = Gray;
                    SouthCountdown = 0;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;

                case true when !IsNorthEnabled && !IsEastEnabled && IsSouthEnabled && !IsWestEnabled:

                    NorthLightColor = Gray;
                    NorthCountdown = 0;

                    EastLightColor = Gray;
                    EastCountdown = 0;

                    SouthLightColor = Green;
                    SouthCountdown = enabledLightsCount;

                    WestLightColor = Gray;
                    WestCountdown = 0;
                    break;

            }

        }

        private void StopTrafficLight()
        {
            IsRunning = false;
            IsNotRunning = true;
            _timer.Stop();

            Reset();
        }

        private void Reset()
        {
            NorthCountdown = 0;
            NorthLightColor = Gray;
            EastCountdown = 0;
            EastLightColor = Gray;
            SouthCountdown = 0;
            SouthLightColor = Gray;
            WestCountdown = 0;
            WestLightColor = Gray;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {


            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    //public static class NumericTextBoxBehavior
    //{
    //    public static readonly DependencyProperty AllowNumericOnlyProperty =
    //        DependencyProperty.RegisterAttached(
    //            "AllowNumericOnly",
    //            typeof(bool),
    //            typeof(NumericTextBoxBehavior),
    //            new PropertyMetadata(false, OnAllowNumericOnlyChanged));

    //    public static bool GetAllowNumericOnly(DependencyObject obj)
    //    {
    //        return (bool)obj.GetValue(AllowNumericOnlyProperty);
    //    }

    //    public static void SetAllowNumericOnly(DependencyObject obj, bool value)
    //    {
    //        obj.SetValue(AllowNumericOnlyProperty, value);
    //    }

    //    private static void OnAllowNumericOnlyChanged(
    //        DependencyObject d,
    //        DependencyPropertyChangedEventArgs e)
    //    {
    //        if (d is TextBox textBox)
    //        {
    //            if ((bool)e.NewValue)
    //            {
    //                textBox.PreviewTextInput += TextBox_PreviewTextInput;
    //                DataObject.AddPastingHandler(textBox, TextBox_Pasting);
    //            }
    //            else
    //            {
    //                textBox.PreviewTextInput -= TextBox_PreviewTextInput;
    //                DataObject.RemovePastingHandler(textBox, TextBox_Pasting);
    //            }
    //        }
    //    }

    //    private static void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
    //    {
    //        if (!IsNumeric(e.Text))
    //        {
    //            e.Handled = true;
    //        }
    //    }

    //    private static void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
    //    {
    //        if (e.DataObject.GetDataPresent(typeof(string)))
    //        {
    //            string pastedText = (string)e.DataObject.GetData(typeof(string));
    //            if (!IsNumeric(pastedText))
    //            {
    //                e.CancelCommand();
    //            }
    //        }
    //        else
    //        {
    //            e.CancelCommand();
    //        }
    //    }

    //    private static bool IsNumeric(string text)
    //    {
    //        return int.TryParse(text, out _);
    //    }
    //}
}
