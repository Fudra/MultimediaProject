using System;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.Mime;
using System.Security.Policy;
using System.Windows.Input;
using System.Windows.Threading;
using Bespoke.Common.Osc;
using EiMM.ViewModel.Enums;
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
        private readonly DispatcherTimer _oscTimer = new DispatcherTimer();
        private Capture _capture;
        private bool _isOscInit;
        private int _bindValues;

        public EimmControlViewModel(ISetting setting)
        {
            _setting = setting;
            Init();
        }

        private void Init()
        {
            _bindValues = 0;
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
                _setting.CollectDataOfObjects++; // start by 1 object
                var tb = new TrackedObject
                {
                    Id = _setting.CollectDataOfObjects,
                    X = circle.Center.X,
                    Y = circle.Center.Y,
                    Radius = circle.Radius
                };

                /*
                _setting.BallPosition = "ball position : x = " + circle.Center.X.ToString().PadLeft(4) +
                                ", y =" + circle.Center.Y.ToString().PadLeft(4) +
                                ", radius = " + circle.Radius.ToString("###.000").PadLeft(7);
                 * */

                

                CvInvoke.cvCircle(imgOriginal, new Point((int)circle.Center.X, (int)circle.Center.Y), 3,
                    new MCvScalar(0, 255, 0), -1, LINE_TYPE.CV_AA, 0);

                var color = _setting.CollectDataOfObjects > 1 ? Color.Blue : Color.Red; 
                imgOriginal.Draw(circle, new Bgr(color), 3);
                _setting.TrackedObjects.Add(tb);
                if(_setting.CollectDataOfObjects>1) continue;

                foreach (var bindValue in _setting.BindValues)
                {
                    switch (bindValue.OscToValue)
                    {
                        case BindingOscValue.Radius:
                            bindValue.Value = circles[0].Radius;
                            break;
                        case BindingOscValue.X:
                            bindValue.Value = circles[0].Center.X;
                            break;
                        case BindingOscValue.Y:
                            bindValue.Value = circles[0].Center.Y;
                            break;
                    }
                }
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

        private void OscTimerOnTick(object sender, EventArgs e)
        {
            // Send OscData on Tick
            _setting.Transmitter.Send(CreateBundle());
            _setting.TransmissonCounter = _setting.Transmitter.TransmissionCount;
        }

        private OscBundle CreateBundle()
        {
            var ipAddress = _setting.UseLoopback ? IPAddress.Loopback : IPAddress.Parse(_setting.IpAddress);
            var sourceEndPoint = new IPEndPoint(ipAddress, _setting.Port);
            var bundle = new OscBundle(sourceEndPoint);
           
            var nestedBundle = new OscBundle(sourceEndPoint);

            foreach (var bindValue in _setting.BindValues)
            {
                var message = new OscMessage(sourceEndPoint, bindValue.Address);
                message.Append(bindValue.Value);
                nestedBundle.Append(message);
            }
            bundle.Append(nestedBundle);
            return bundle;
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
        public ICommand OscInitCommand { get; internal set; }
        public ICommand OscStartStopCommand { get; internal set; }
        public ICommand BindCommand { get; internal set; }
        public ICommand ClearBindingCommand { get; internal set; }

        private void InitCommands()
        {
            RefreshCommand = new RelayCommand<object>(RefreshCommandExcecute, RefreshCommandCanExcecute);
            OscInitCommand = new RelayCommand<object>(OscInitCommandExcecute);
            OscStartStopCommand = new RelayCommand<object>(OscStartStopCommandExcecute);
            BindCommand = new RelayCommand<object>(BindCommandExceute);
            ClearBindingCommand = new RelayCommand<object>(ClearBindingCommandExcecute);
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

        private void OscInitCommandExcecute(object o)
        {
            //TODO: Crazy OSC Init Stuff.. 
            // Init Mode
            if (_setting.Transmitter != null)
            {
                _setting.Transmitter.Close();
                _setting.Transmitter = null;
            }
            switch (_setting.TransmissionMode)
            {
                case TransmissionMode.Udp:
                    _setting.Transmitter = new UdpTransmitter();
                    break;

                case TransmissionMode.Tcp:
                    _setting.Transmitter = new TcpTransmitter();
                    break;

                case TransmissionMode.Multicast:
                    _setting.Transmitter = new MulticastTransmitter();
                    break;

                default:
                    throw new Exception("Unsupported transmitter type.");
            }

            if (_setting.UseLoopback)
                _setting.IpAddress = IPAddress.Loopback.ToString();

            var ipAddress = _setting.UseLoopback ? IPAddress.Loopback : IPAddress.Parse(_setting.IpAddress);
            _setting.Transmitter.Init(ipAddress, _setting.Port);
            _oscTimer.Interval = new TimeSpan(0,0,0,0,_setting.SleepTime);
            _oscTimer.Tick += OscTimerOnTick;
            _isOscInit = true;
            
        }


        private bool OscInitCommandCanExcecute(object o)
        {
            return !_setting.IsOscSending;
        }
        private void OscStartStopCommandExcecute(object o)
        {
            if (_setting.IsOscSending)
                _oscTimer.Start();
            else
                _oscTimer.Stop();
        }

        private bool OscStartStopCommandCanExcecute(object o)
        {
            return _isOscInit;
        }

        private void BindCommandExceute(object o)
        {
            if (_setting.OscAddress == "") return;
            _bindValues++;

            var bindValue = new BindValue
            {
                Id = _bindValues,
                Address = _setting.OscAddress,
                OscToValue = _setting.OscValue,
                Avg = _setting.AvgOnSleepTime
            };
            _setting.BindValues.Add(bindValue);

            // reset
            _setting.OscAddress = "";
        }

        private void ClearBindingCommandExcecute(object o)
        {
            _setting.BindValues.Clear();
            _bindValues = 0;
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