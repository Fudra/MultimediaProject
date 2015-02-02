using System;
using System.Net;
using Bespoke.Common;
using Bespoke.Common.Osc;
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class TcpTransmitter : ITransmitter
    {
        private static IPEndPoint _destination;

        private OscPacket _packet;
        private OscClient _oscClient;

        public TcpTransmitter()
        {
  
        }

        public void Init(IPAddress ipAddress, int port)
        {
            Port = port;
            IpAddress = ipAddress;
            _destination = new IPEndPoint(ipAddress, port);
            _oscClient = new OscClient(_destination);
            _oscClient.Connect();
            IsInitialize = true;
        }

        public void Send(OscPacket packet)
        {
            if (!IsInitialize) throw new Exception("TCP Transmitter is not initialize!");
            Assert.ParamIsNotNull(packet);
            _packet = packet;

            try
            {
                _packet.Send(_destination);
                TransmissionCount++;
            }
            catch (Exception exception)
            {
                throw new Exception("TCP Transmission Error: " + exception.Message);
            }
        }

        public void Close()
        {
            _oscClient.Close();
            IsInitialize = false;
            Port = 0;
            IpAddress = null;
            TransmissionCount = 0;
        }

        public int TransmissionCount { get; internal set; }

        public int Port { get;  set; }

        public IPAddress IpAddress { get; set; }

        public bool IsInitialize { get; internal set; }

    }
}