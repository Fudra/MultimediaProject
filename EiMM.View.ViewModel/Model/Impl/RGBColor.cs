
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class RgbColor : IRgbColor
    {
        public RgbColor(int r, int g, int b)
        {
            Red = r;
            Green = g;
            Blue = b;
        }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}