using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Agent.Common.Presentation.RegistryUser;

namespace Agent.Localization
{
    /// <summary>
    /// Interaction logic for LanguageSelectionControl.xaml
    /// </summary>
    public partial class LanguageSelectionControl : UserControl
    {
        private ObservableCollection<LanguageViewModel> languages = 
            new ObservableCollection<LanguageViewModel>();

        public LanguageSelectionControl()
        {
            InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            comboBoxLanguages.ItemsSource = languages;
            InitializeLanguages();
            comboBoxLanguages.SelectionChanged += OnLanguageSelectionChanged;
            comboBoxLanguages.SelectedItem = languages.First(l => 
                l.ModelEnum == StoredSettings.StoredLanguage);
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
