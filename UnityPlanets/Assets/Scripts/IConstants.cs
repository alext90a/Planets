namespace Planets
{
    public interface IConstants
    {
        int SectorsInSegment { get; }
        int PlanetsInSector { get; }
        int CellsInSector { get; }
        int SectorSideSize { get; }
        float PlanetsPercent { get; }
        int MaxCameraSize { get; }
        int MinCameraSize { get; }
        int MaxPlanetScore { get; }
        int MinPlanetScore { get; }
    }
}
