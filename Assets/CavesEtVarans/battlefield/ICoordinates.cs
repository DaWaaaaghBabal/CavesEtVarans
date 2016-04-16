namespace CavesEtVarans.battlefield
{
    public interface ICoordinates : utils.IDisposable
    {
        int Column { get; }
        int Layer { get; }
        int Row { get; }
    }
}