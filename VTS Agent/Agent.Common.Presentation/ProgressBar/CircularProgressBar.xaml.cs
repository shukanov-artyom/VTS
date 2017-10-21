using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Agent.Common.Presentation.ProgressBar
{
    /// <summary>
    /// Interaction logic for RounderProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBar : UserControl
    {
        private const string PERCENTS_TEXT = "{0}%";

        private delegate void VoidDelegete();
        private DispatcherTimer timer;
        private bool loaded;
        private int progress;

        public CircularProgressBar()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += OnTimerElapsed;
            timer.Start();
            loaded = true;
        }

        void OnTimerElapsed(object sender, EventArgs e)
        {
            rotationCanvas.Dispatcher.BeginInvoke
                (
                new VoidDelegete(
                    delegate
                    {
                        SpinnerRotate.Angle += 30;
                        if (SpinnerRotate.Angle == 360)
                        {
                            SpinnerRotate.Angle = 0;
                        }
                    }
                    ),
                null
                );

        }

        private void TextBlockPercentsTextChanged(object sender, TextChangedEventArgs e)
        {
            if (loaded)
            {
                Canvas.SetLeft(tbPercents, (rotationCanvas.ActualHeight - tbPercents.ActualWidth) / 2);
                Canvas.SetTop(tbPercents, (rotationCanvas.ActualHeight - tbPercents.ActualHeight) / 2);
            }
        }

        private void UpdateProgress()
        {
            tbPercents.Text = string.Format(PERCENTS_TEXT, progress);
        }

        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
                UpdateProgress();
            }
        }

    }
}
