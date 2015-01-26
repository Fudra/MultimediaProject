using System.Windows;
using EiMM.View.View;

namespace EiMM
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Add(new EiMMControl());
        }
    }
}
