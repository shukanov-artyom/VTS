using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Agent.Common.Presentation.ProgressBar
{
    /// <summary>
    /// Interaction logic for RounderProgressBar.xaml
    /// </summary>
    public partial class CircularProgressBarSmall : UserControl
    {
        private delegate void VoidDelegete();
        private DispatcherTimer timer;
        private int progress;

        public CircularProgressBarSmall()
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

        public int Progress
        {
            get
            {
                return progress;
            }
            set
            {
                progress = value;
            }
        }

    }
}
