using System;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation;

namespace Agent.Workspace.Views.Evaluation
{
    /// <summary>
    /// Interaction logic for EvaluationTabItemControl.xaml
    /// </summary>
    public partial class EvaluationTabItemControl : UserControl
    {
        public EvaluationTabItemControl()
        {
            InitializeComponent();
        }

        private void EvaluationDataTreeSelectedItemChanged(object sender,
            RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModelBase viewModel = e.NewValue as ViewModelBase;
            if (viewModel == null)
            {
                contentControlDataItemDetailsEvaluation.Content = null;
            }
            IEvaluationDataItemView view = EvaluationDataItemViewFactory.Create(viewModel);
            contentControlDataItemDetailsEvaluation.Content = view;
        }
    }
}
