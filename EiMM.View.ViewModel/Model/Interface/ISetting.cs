using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EiMM.ViewModel.Enums;

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

        ObservableCollection<ITrackedObject> TrackedObjects { get; set; } 

        /*
         *  OSC Properties
         */

        ITransmitter Transmitter { get; set; }

        TransmissionMode TransmissionMode { get; set; }

        int Port { get; set; }

        String IpAddress { get; set; }

        bool AvgOnSleepTime { get; set; }

        bool UseLoopback { get; set; }

        int SleepTime { get; set; }

        int TransmissonCounter { get; set; }

        bool IsOscSending { get; set; }

        String OscAddress { get; set; }

        BindingOscValue OscValue { get; set; }

        ObservableCollection<IBindValue> BindValues { get; set; } 

        /*
         *  Events
         */
        event EventHandler SettingChanged;
    }
}