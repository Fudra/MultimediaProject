using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using EiMM.ViewModel.Enums;
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
        private ITransmitter _transmitter;
        private TransmissionMode _transmissionMode;
        private int _port;
        private String _ipAddress;
        private bool _avgOnSleepTime;
        private int _sleepTime;
        private bool _useLoopback;
        private int _transmissonCounter;
        private bool _isOscSending;
        private string _oscAddress;
        private BindingOscValue _oscValue;
        private ObservableCollection<IBindValue> _bindValues;


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
            _port = 5103;
            _useLoopback = true;
            _sleepTime = 250;
            _avgOnSleepTime = true;
            _ipAddress = "127.0.0.1";
            _transmissionMode = TransmissionMode.Udp;
            _isOscSending = false;
            _bindValues = new ObservableCollection<IBindValue>();
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

        /// <summary>
        /// 
        /// </summary>
        public ITransmitter Transmitter
        {
            get { return _transmitter; }
            set
            {
                _transmitter = value;
                OnPropertyChanged(() => Transmitter);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public TransmissionMode TransmissionMode
        {
            get { return _transmissionMode; }
            set
            {
                _transmissionMode = value;
                OnPropertyChanged(() => TransmissionMode);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get { return _port; }
            set
            {
                _port = value;
                OnPropertyChanged(() => Port);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public String IpAddress
        {
            get { return _ipAddress; }
            set
            {
                _ipAddress = value;
                OnPropertyChanged(() => IpAddress);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AvgOnSleepTime
        {
            get { return _avgOnSleepTime; }
            set
            {
                _avgOnSleepTime = value;
                OnPropertyChanged(() => AvgOnSleepTime);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool UseLoopback
        {
            get { return _useLoopback; }
            set
            {
                _useLoopback = value;
                OnPropertyChanged(()=> UseLoopback);
                OnSettingChanged(new EventArgs());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int SleepTime
        {
            get { return _sleepTime; }
            set
            {
                _sleepTime = value;
                OnPropertyChanged(()=> SleepTime);
                OnSettingChanged(new EventArgs());
            }
        }

        public int TransmissonCounter
        {
            get { return _transmissonCounter; }
            set
            {
                _transmissonCounter = value;
                OnPropertyChanged(()=> TransmissonCounter);
            }
        }

        public bool IsOscSending
        {
            get { return _isOscSending; }
            set
            {
                _isOscSending = value;
                OnPropertyChanged(()=> IsOscSending);
            }
        }

        public string OscAddress
        {
            get { return _oscAddress; }
            set
            {
                _oscAddress = value;
                OnPropertyChanged(()=> OscAddress);
            }
        }

        public BindingOscValue OscValue
        {
            get { return _oscValue; }
            set
            {
                _oscValue = value;
                OnPropertyChanged(()=>OscValue);
            }
        }

        public ObservableCollection<IBindValue> BindValues
        {
            get { return _bindValues; }
            set
            {
                _bindValues = value;
                OnPropertyChanged(()=>BindValues);
            }
        }

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