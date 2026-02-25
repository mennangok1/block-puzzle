using UnityEngine;
using MyGrid.Code;
using UnityEngine.EventSystems;
public class Movable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler

{

    private Vector3 _offset;

    [SerializeField] LayerMask mainGridMask;
    private Vector3 homePosition;

    private Transform _currentMovable;
    private TileControllerCustom customTile;
    private GridManager manager;
    private void Start()
    {
        _currentMovable = transform.parent;
        homePosition = transform.position;
        manager = transform.parent.GetComponent<GridManager>();
        customTile = GetComponent<TileControllerCustom>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {

        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        _offset = _currentMovable.position - target;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        target += _offset;
        target.z = 0;
        _currentMovable.position = target; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        var allowSetToGrid = AllowSetToGrid();
        if (allowSetToGrid) 
        {
            PlaceBlockToGrid();
            BaseGrid.Instance.CheckGrid();
        }
        else
        {
            ReturnAllToHome();
        }
    }

    private void PlaceBlockToGrid()
    {
        foreach ( var tile in manager.Tiles)
        {
            if (!tile.gameObject.activeSelf) continue;
            var currentTile = (TileControllerCustom) tile;
            currentTile.Movable.PlaceSingleSquareToGrid();
        }
    }
    private void PlaceSingleSquareToGrid()
    {
        var hit = Hit();
        var baseTile = hit.transform.GetComponent<TileControllerCustom>();
        baseTile.OnTile = customTile;

        var target = hit.transform.position;
        target.z = 0.5f;
        transform.position = target;
    }
    private bool AllowSetToGrid()
    {
        var allowSetToGrid = true;
        foreach (var tile in manager.Tiles)
        {
            if (!tile.gameObject.activeSelf) continue;
            var customTile = (TileControllerCustom) tile;
            var hitTile = customTile.Movable.Hit();
            if (!hitTile)
            {
                allowSetToGrid = false;
                break;
            }
            
            var baseTile = hitTile.transform.GetComponent<TileControllerCustom>();
            if (baseTile.OnTile)
            {
                // so there is another block on the grid position
                allowSetToGrid = false;
                break;   
            }
        }

        return allowSetToGrid;
    }

    private RaycastHit2D Hit()
    {
        return Physics2D.Raycast(transform.position, Vector3.forward, 10, mainGridMask);
        /* 
        if (hit)
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log($"hit {hit.transform.name}");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("didn't hit");
        } */
    }


    private void ReturnToHome()
    {
        transform.position = homePosition;
    }

    private void ReturnAllToHome()
    {
        foreach ( var tile in manager.Tiles)
        {
            if (!tile.gameObject.activeSelf) continue;
            var currentTile = (TileControllerCustom) tile;
            currentTile.Movable.ReturnToHome();
        }
    }
}
