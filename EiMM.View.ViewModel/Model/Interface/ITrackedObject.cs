namespace EiMM.ViewModel.Model.Interface
{
    public interface ITrackedObject
    {
        int Id { get; set; }
        float X { get; set; }
        float Y { get; set; }
        float Radius { get; set; }
    }
}