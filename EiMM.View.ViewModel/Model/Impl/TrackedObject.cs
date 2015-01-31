using System.Text;
using EiMM.ViewModel.Model.Interface;

namespace EiMM.ViewModel.Model.Impl
{
    public class TrackedObject : ITrackedObject
    {

        public TrackedObject(int id, float x, float y, float r)
        {
            Id = id;
            X = x;
            Y = y;
            Radius = r;
        }

        public TrackedObject()
        {
            
        }

        public int Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Radius { get; set; }


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Id: " + Id);
            sb.Append(" ,X: " + X);
            sb.Append(" ,Y: " + Y);
            sb.Append(" ,Radius: " + Radius);
            return sb.ToString();
        }
    }
}