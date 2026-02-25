using UnityEngine;
using MyGrid.Code;

public class TileControllerCustom : TileController
{
    public Movable Movable {get; private set;}
    public TileControllerCustom OnTile;
    private void Start()
    {
        Movable = GetComponent<Movable>();
    }
}
