using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Controls;
using VTSWeb.Localization;

namespace VTSWeb.Presentation.Common
{
    public partial class LanguageSelectionControl : UserControl
    {
        private ObservableCollection<LanguageViewModel> languages =
            new ObservableCollection<LanguageViewModel>();

        public LanguageSelectionControl()
        {
            InitializeComponent();
            comboBoxLanguages.ItemsSource = languages;
            comboBoxLanguages.SelectionChanged += OnLanguageSelectionChanged;
            InitializeLanguages();
            comboBoxLanguages.SelectedItem = languages[0];
        }

        private void OnLanguageSelectionChanged(object obj,
            SelectionChangedEventArgs e)
        {
            TranslationManager.Instance.CurrentLanguage =
                ((LanguageViewModel)e.AddedItems[0]).Model;
        }

        private void InitializeLanguages()
        {
            foreach (CultureInfo c in TranslationManager.Instance.Languages)
            {
                languages.Add(new LanguageViewModel(c));
            }
        }
    }
}
