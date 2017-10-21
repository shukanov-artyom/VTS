using System;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation.Data;

namespace Agent.Common.Presentation.Controls.Cascade
{
    /// <summary>
    /// Interaction logic for CascadeControl.xaml
    /// </summary>
    public partial class CascadeControl : UserControl
    {
        private event EventHandler MoveRight;
        private event EventHandler MoveLeft;
        private event Action<double> SliderMoved;

        public CascadeControl()
        {
            InitializeComponent();
        }

        public void Display(CascadeViewModel vm)
        {
            foreach (PsaParameterDataViewModel parameter in vm.Data)
            {
                stackPanelCascade.Children.Add(new Expander
                {
                    Content = GetCascadeChart(parameter), 
                    IsExpanded = true, 
                    Header = String.Format("{0}, {1}", parameter.Type, parameter.MeasureUnits)
                });
            }
        }

        private CascadeChartControl GetCascadeChart(PsaParameterDataViewModel parameter)
        {
            var cn = new CascadeChartControl()
            {
                DataContext = parameter,
                Height = 180,
                HorizontalAlignment = HorizontalAlignment.Stretch
            };
            MoveRight += cn.MoveCursorRight;
            MoveLeft += cn.MoveCursorLeft;
            SliderMoved += cn.SliderMoved;
            return cn;
        }

        private void MoveRightClick(object sender, RoutedEventArgs e)
        {
            EventHandler moveRight = MoveRight;
            if (moveRight != null)
            {
                moveRight.Invoke(this, EventArgs.Empty);
            }
        }

        private void MoveLeftClick(object sender, RoutedEventArgs e)
        {
            EventHandler moveLeft = MoveLeft;
            if (moveLeft != null)
            {
                moveLeft.Invoke(this, EventArgs.Empty);
            }
        }

        private void SliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double val = sliderPosition.Value;
            if (SliderMoved != null)
            {
                SliderMoved.Invoke(val);
            }
        }
    }
}
