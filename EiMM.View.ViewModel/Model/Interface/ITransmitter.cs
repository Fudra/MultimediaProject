using System.Net;
using Bespoke.Common.Osc;

namespace EiMM.ViewModel.Model.Interface
{
    /// <summary>
    /// Osc packet Transmitter interface.
    /// </summary>
    public interface ITransmitter
    {
        /// <summary>
        /// Send OSCPacket.
        /// </summary>
        /// <param name="packet">The packet to transmit.</param>
        void Send(OscPacket packet);

        /// <summary>
        /// Init Connection
        /// </summary>
        void Init(IPAddress ipAddress, int port);

        /// <summary>
        /// Close Connection.
        /// </summary>
        void Close();

        /// <summary>
        /// Get the Transmission Count
        /// </summary>
        int TransmissionCount { get; }

        /// <summary>
        ///  Get/set the Port
        /// </summary>
        int Port { get; set; }

        /// <summary>
        /// The Ip Address
        /// </summary>
        IPAddress IpAddress { get; set; }

        /// <summary>
        /// IsInitialize
        /// </summary>
        bool IsInitialize { get; }
    }
}