using UnityEngine;

/// <summary>
/// Class is responsible for activate/deactivate grid on the field
/// </summary>
public class GridController : Singleton<GridController>
{
    private GridGenerator grid;
    private bool isActive = true;

    private void Start()
    {
        grid = GridGenerator.Instance;
    }

    public void GridDisplay()
    {
        isActive = !isActive;
        grid.SetGridActive(isActive);
    }
}
