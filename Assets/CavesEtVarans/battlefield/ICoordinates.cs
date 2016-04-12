namespace CavesEtVarans.battlefield
{
    public interface ICoordinates
    {
        int Column { get; }
        int Layer { get; }
        int Row { get; }
    }
}