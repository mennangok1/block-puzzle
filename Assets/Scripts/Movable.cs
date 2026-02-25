using UnityEngine;
using MyGrid.Code;
using UnityEngine.EventSystems;
public class Movable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler

{

    private Vector3 _offset;
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Down"); 

        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        _offset = transform.position - target;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        var target = Camera.main.ScreenToWorldPoint(eventData.position);
        target += _offset;
        target.z = 0;
        transform.position = target; 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Up");
    }
}
