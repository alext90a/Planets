namespace Planets
{
    public interface ISector
    {
        int GetPlanet(int index);
        int GetX { get; }
        int GetY { get; }
        int GetPlanetRating(int index);
    }
}
