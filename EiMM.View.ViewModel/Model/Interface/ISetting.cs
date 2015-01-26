using System;
using System.Windows.Media;

namespace EiMM.ViewModel.Model.Interface
{
    public interface ISetting
    {

        /*
         *  OpenCV Tracking Properties
         */

        IRgbColor RgbColorMin { get; set; }

        IRgbColor RgbColorMax { get; set; }

        int GaussianBlur { get; set; }

        int CannyThreashold { get; set; }

        int AccumulatorThreashold { get; set; }

        int MinRadiusDetectedCircles { get; set; }

        int MaxRadiusDetectedCircles { get; set; }

        ImageSource OrginalImage { get; set; }

        ImageSource ProcessedImage { get; set; }

        int CollectDataOfObjects { get; set; }

        String BallPosition { get; set; }

        bool IsCapture { get; set; }

        /*
         *  OSC Properties
         */


        /*
         *  Events
         */
        event EventHandler SettingChanged;
    }
}