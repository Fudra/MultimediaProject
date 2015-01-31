using System.Windows;
using System.Windows.Controls;

namespace EiMM.View.View
{
    /// <summary>
    /// Interaktionslogik für EiMMControl.xaml
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public partial class EiMMControl : UserControl
    {
         public EiMMControl()
        {
            InitializeComponent();
        }

        private void OpenCvButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Window
            {
                Title = "OpenCV Settings",
                Content = new OpenCVControl(),
                SizeToContent =  SizeToContent.Width,
                Width = 400,
                Height = 500
            };

            window.ShowDialog();
        }

        private void OscButton_OnClick(object sender, RoutedEventArgs e)
        {
            var window = new Window
            {
                Title = "OSC Settings",
                Content = new OSCControl(),
                SizeToContent = SizeToContent.Width,
                Width = 400,
                Height = 500
            };

            window.ShowDialog();
        }
    }
}
