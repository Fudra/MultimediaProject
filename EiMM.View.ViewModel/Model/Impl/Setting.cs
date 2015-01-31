using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EiMM.ViewModel.Helper;
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class Setting : ObjectNotifyPropertyChanged, ISetting
    {
        private IRgbColor _rgbColorMin;
        private IRgbColor _rgbColorMax;
        private int _gaussianBlur;
        private int _cannyThreashold;
        private int _accumulatorThreashold;
        private int _minRadiusDetectedCircles;
        private int _maxRadiusDetectedCircles;
        private ImageSource _orginalImage;
        private ImageSource _processedImage;
        private string _ballPosition;
        private int _collectDataOfObjects;
        private bool _isCapture;
        private ObservableCollection<ITrackedObject> _trackedObjects;


        public Setting()
        {
            //OpenCV Settings
            _rgbColorMin = new RgbColor(175,0,0);
            _rgbColorMax = new RgbColor(256,100,100);
            _gaussianBlur = 9;
            _cannyThreashold = 100;
            _accumulatorThreashold = 50;
            _minRadiusDetectedCircles = 10;
            _maxRadiusDetectedCircles = 100;
            _collectDataOfObjects = 0;
            _isCapture = false;
            _trackedObjects = new ObservableCollection<ITrackedObject>();

            // OSC Settings

        }

       

        #region OpenCV Tracking Cotrols

        /// <summary>
        ///  RgbColorMin
        /// </summary>
        public IRgbColor RgbColorMin
        {
            get { return _rgbColorMin; }
            set
            {
                _rgbColorMin = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(()=>RgbColorMax);
            }
        }

        /// <summary>
        /// RgbColorMax
        /// </summary>
        public IRgbColor RgbColorMax
        {
            get { return _rgbColorMax; }
            set
            {
                _rgbColorMax = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(()=>RgbColorMax);
            }
        }

        /// <summary>
        /// GaussianBlur
        /// </summary>
        public int GaussianBlur
        {
            get { return _gaussianBlur; }
            set
            {
                _gaussianBlur = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => RgbColorMax);
            }
        }

        /// <summary>
        /// CannyThreashold
        /// </summary>
        public int CannyThreashold
        {
            get { return _cannyThreashold; }
            set
            {
                _cannyThreashold = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => CannyThreashold);
            }
        }

        /// <summary>
        /// AccumulatorThreashold
        /// </summary>
        public int AccumulatorThreashold
        {
            get { return _accumulatorThreashold; }
            set
            {
                _accumulatorThreashold = value; 
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => AccumulatorThreashold);
            }
        }

        /// <summary>
        /// MinRadiusDetectedCircles
        /// </summary>
        public int MinRadiusDetectedCircles
        {
            get { return _minRadiusDetectedCircles; }
            set
            {
                _minRadiusDetectedCircles = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => MinRadiusDetectedCircles);
            }
        }

        /// <summary>
        /// MaxRadiusDetectedCircles
        /// </summary>
        public int MaxRadiusDetectedCircles
        {
            get { return _maxRadiusDetectedCircles; }
            set
            {
                _maxRadiusDetectedCircles = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => MaxRadiusDetectedCircles);
           }
        }

        /// <summary>
        /// OrginalImage
        /// </summary>
        public ImageSource OrginalImage
        {
            get { return _orginalImage; }
            set
            {
                _orginalImage = value;
                OnPropertyChanged(() => OrginalImage);
            }
        }


        /// <summary>
        /// ProcessedImage
        /// </summary>
        public ImageSource ProcessedImage
        {
            get { return _processedImage; }
            set
            {
                _processedImage = value;
                OnPropertyChanged(() => ProcessedImage);
            }
        }

        /// <summary>
        /// CollectDataOfObjects
        /// </summary>
        public int CollectDataOfObjects
        {
            get { return _collectDataOfObjects; }
            set
            {
                _collectDataOfObjects = value;
                OnPropertyChanged(() => CollectDataOfObjects);
            }
        }

        /// <summary>
        /// BallPosition
        /// </summary>
        public String BallPosition
        {
            get { return _ballPosition; }
            set
            {
                _ballPosition = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(()=> BallPosition);
            }
        }

        /// <summary>
        /// IsCapture
        /// </summary>
        public bool IsCapture
        {
            get { return _isCapture; }
            set
            {
                _isCapture = value;
                OnSettingChanged(new EventArgs());
                OnPropertyChanged(() => IsCapture);
            }
        }

        /// <summary>
        /// TrackedObjects
        /// </summary>
        /// Observable ??
        public ObservableCollection<ITrackedObject> TrackedObjects
        {
            get { return _trackedObjects; }
            set
            {
                _trackedObjects = value;
                OnPropertyChanged(()=>TrackedObjects);
            }
        }

        #endregion

        #region OSC Controls
        
        #endregion


        #region Event

        
        public event EventHandler SettingChanged;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected void OnSettingChanged(EventArgs args)
        {
            var handler = SettingChanged;
            if (handler != null)
                handler(this, args);
        }

        #endregion

    }
}