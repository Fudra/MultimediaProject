using System;
using System.Net;
using Bespoke.Common;
using Bespoke.Common.Osc;
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class UdpTransmitter : ITransmitter
    {
        private static IPEndPoint _destination;

        private OscPacket _packet;

        public UdpTransmitter()
        {
            
        }

        public void Init(IPAddress ipAddress, int port)
        {
            Port = port;
            IpAddress = ipAddress;
            _destination = new IPEndPoint(ipAddress, port);
            IsInitialize = true;
        }

        public void Send(OscPacket packet)
        {
            if (!IsInitialize) throw new Exception("UDP Transmitter is not initialize!");
            Assert.ParamIsNotNull(packet);
            _packet = packet;

            try
            {
                _packet.Send(_destination);
                TransmissionCount++;

            }
            catch (Exception exception)
            {
                throw new Exception("UDP Transmission Error: " + exception.Message);
            }
        }

        public void Close()
        {
            IsInitialize = false;
            Port = 0;
            IpAddress = null;
            TransmissionCount = 0;
        }

        public int TransmissionCount { get; internal set; }

        public int Port {  get; set; }

        public IPAddress IpAddress { get; set; }

        public bool IsInitialize { get; internal set; }
    }
}