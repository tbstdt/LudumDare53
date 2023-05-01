using Sources.Editor.ObjectsOnMap;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ObjectOnMap : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Transform PointForArrow;   
    public ObjectType Type { get; set; }
    public GameObject MapPoint { get; private set; }

    protected abstract void OnObjectClicked();

    public void OnPointerClick(PointerEventData eventData) => OnObjectClicked();

    public void AddMapPoint(GameObject point) {
        MapPoint = point;
    }

    public virtual void Job(Man man) { }
    public abstract void OnPointerEnter(PointerEventData eventData);
    public abstract void OnPointerExit(PointerEventData eventData);
}