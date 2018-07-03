namespace Planets
{
    public interface IConstants
    {
        int GetSectorsInSegment();
        int GetPlanetsInSector();
        int GetCellsInSector();
        int GetSectorSideSize();
        float GetPlanetsPercent();
        int GetMaxCameraSize();
        int GetMinCameraSize();
        int GetMaxPlanetScore();
        int GetMinPlanetScore();
        int GetPlanetsToVisualize();
    }
}
