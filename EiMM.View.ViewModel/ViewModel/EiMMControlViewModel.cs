using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Mime;
using System.Windows.Input;
using System.Windows.Threading;
using EiMM.ViewModel.Model.Impl;
using EiMM.ViewModel.Model.Interface;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.WPF;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Point = System.Drawing.Point;

namespace EiMM.ViewModel.ViewModel
{
    public class EimmControlViewModel : ViewModelBase
    {
        private  readonly ISetting _setting;
        private bool _isBusy;
        private readonly  DispatcherTimer _timer = new DispatcherTimer();
        private readonly DispatcherTimer _captureTimer = new DispatcherTimer();
        private Capture _capture;

        public EimmControlViewModel(ISetting setting)
        {
            _setting = setting;
            Init();
        }

        private void Init()
        {
            // Init Timer
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += TimerOnTick;
            _timer.Start();

           // _captureTimer.Interval = new TimeSpan(0,0,0,0,200);
            _captureTimer.Tick += ProcessFrameOnTimerTick;
            // settings
            _setting.SettingChanged += OnSettingChanged;
            // init Commands
            InitCommands();
            // refresh
            RefreshCommand.Execute(null);

            try
            {
                _capture = new Capture();
                _capture.ImageGrabbed += ProcessFrame;
                //_capture.Start();
            }
            catch (NullReferenceException excpt)
            {
                throw new Exception(excpt.Message); ;
            }
        }

        private void ProcessFrameOnTimerTick(object sender, EventArgs e)
        {
            _captureTimer.Stop();

            // Get Min and MaxColorRange 
            var minColor = _setting.RgbColorMin;
            var maxColor = _setting.RgbColorMax;

            // processed image
            var imgOriginal = _capture.RetrieveBgrFrame();
            imgOriginal = imgOriginal.Flip(FLIP.HORIZONTAL);
           
            var imgProcessed = imgOriginal.InRange(new Bgr(minColor.Blue, minColor.Green, minColor.Red), new Bgr(maxColor.Blue, maxColor.Green, maxColor.Red));
            imgProcessed = imgProcessed.SmoothGaussian(_setting.GaussianBlur);
            var circles = imgProcessed.HoughCircles(new Gray(_setting.CannyThreashold), new Gray(_setting.AccumulatorThreashold), 2, imgProcessed.Height / 4f, _setting.MinRadiusDetectedCircles, _setting.MaxRadiusDetectedCircles)[0];

            _setting.CollectDataOfObjects = 0;
            _setting.TrackedObjects.Clear();
            foreach (var circle in circles)
            {
                var tb = new TrackedObject
                {
                    Id = _setting.CollectDataOfObjects,
                    X = circle.Center.X,
                    Y = circle.Center.Y,
                    Radius = circle.Radius
                };
                _setting.BallPosition = "ball position : x = " + circle.Center.X.ToString().PadLeft(4) +
                                ", y =" + circle.Center.Y.ToString().PadLeft(4) +
                                ", radius = " + circle.Radius.ToString("###.000").PadLeft(7);

                _setting.CollectDataOfObjects ++;

                CvInvoke.cvCircle(imgOriginal, new Point((int)circle.Center.X, (int)circle.Center.Y), 3,
                    new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);
                
                imgOriginal.Draw(circle, new Bgr(Color.Red), 3);
                _setting.TrackedObjects.Add(tb);
            }

            foreach (var to in _setting.TrackedObjects)
            {
                Debug.WriteLine(to.ToString());
            }

            // Set Image to Settings:
            _setting.OrginalImage = BitmapSourceConvert.ToBitmapSource(imgOriginal);
            _setting.ProcessedImage = BitmapSourceConvert.ToBitmapSource(imgProcessed);

        }


        private void ProcessFrame(object sender, EventArgs e)
        {
            _captureTimer.Start();
        }

        private void OnSettingChanged(object sender, EventArgs e)
        {
            if (_setting.IsCapture)
            {
                //_captureTimer.Start();
                _capture.Start();
            }
            else
            {
               // _captureTimer.Stop();
                _capture.Stop();
            }
            RefreshCommand.Execute(null);
        }

        private void TimerOnTick(object sender, EventArgs e)
        {
            _timer.Stop();
            RefreshAync();
        }

        private async void RefreshAync()
        {
            IsBusy = true;
       
            IsBusy = false;
        }

  

        #region Commands

        public ICommand RefreshCommand { get; internal set; }

        private void InitCommands()
        {
            RefreshCommand = new RelayCommand<object>(RefreshCommandExcecute, RefreshCommandCanExcecute);
        }

        private void RefreshCommandExcecute(object o)
        {
            _timer.Stop();
            _timer.Start();
        }

        private bool RefreshCommandCanExcecute(object o)
        {
            return !IsBusy;
        }

        #endregion

        #region Properties

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ISetting Setting
        {
            get { return _setting; }
        }

        #endregion


    }
}