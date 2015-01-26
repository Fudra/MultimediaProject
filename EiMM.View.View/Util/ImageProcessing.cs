using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using EiMM.View.View;

namespace EiMM.View.Util
{
    public class ImageProcessing
    {

        #region Image dependency property

        /// <summary>
        /// An attached dependency property which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static readonly DependencyProperty ImageProperty;

        /// <summary>
        /// Gets the <see cref="ImageProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static BitmapSource GetImage(DependencyObject obj)
        {
            return (BitmapSource)obj.GetValue(ImageProperty);
        }

        /// <summary>
        /// Sets the attached <see cref="ImageProperty"/> for a given
        /// <see cref="DependencyObject"/>, which provides an
        /// <see cref="ImageSource" /> for arbitrary WPF elements.
        /// </summary>
        public static void SetImage(DependencyObject obj, BitmapSource value)
        {
          obj.SetValue(ImageProperty, value);
        }

        #endregion

        static ImageProcessing()
        {
          //register attached dependency property
            var metadata = new FrameworkPropertyMetadata((BitmapSource)null);
          ImageProperty = DependencyProperty.RegisterAttached("Image",
                                                              typeof(BitmapSource),
                                                              typeof(ImageProcessing), metadata);
        }

    }
}