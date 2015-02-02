using System.ComponentModel;

namespace EiMM.ViewModel.Enums
{
    public enum TransmissionMode
    {
        //[Browsable(false)]
        [Description("TCP")]
        Tcp,
        [Description("UDP")]
        Udp,
        Multicast
    }
}