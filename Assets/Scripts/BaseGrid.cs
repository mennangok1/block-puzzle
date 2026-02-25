using UnityEngine;
using MyGrid.Code;
using System.Collections.Generic;
public class BaseGrid : Singleton<BaseGrid>
{
    [SerializeField] private GridManager manager;

    public void CheckGrid()
    {
        for ( int i = 0; i < 10; i ++)
        {
            if (IsRowFull(i))
            {
                Debug.Log($"Row {i} is full");
            }
            if (IsColumnFull(i))
            {
                Debug.Log($"Column {i} is full");
            }
        }
    }


    private bool IsRowFull(int rowIndx)
    {
        for (int i = 0; i < 10; i++ )
        {
            var tile = (TileControllerCustom)manager.GetTile(new Vector2Int(i, rowIndx));
            if (!tile.OnTile) return false;
        }

        return true;
    }
    private bool IsColumnFull(int colIndx)
    {
        for (int i = 0; i < 10; i++ )
        {
            var tile = (TileControllerCustom)manager.GetTile(new Vector2Int(colIndx, i));
            if (!tile.OnTile) return false;
        }

        return true;
    }
}
