namespace Planets
{
    public interface ISector
    {
        int GetPlanet(int index);
        int GetX();
        int GetY();
        int GetPlanetRating(int index);
    }
}
