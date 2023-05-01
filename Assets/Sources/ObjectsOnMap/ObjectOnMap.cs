using Sources.Editor.ObjectsOnMap;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ObjectOnMap : MonoBehaviour, IPointerClickHandler
{
    public Transform PointForArrow;
    public abstract ObjectType Type { get; }
    public GameObject MapPoint { get; private set; }

    protected abstract void OnObjectClicked();

    public void OnPointerClick(PointerEventData eventData) => OnObjectClicked();

    public void AddMapPoint(GameObject point) {
        MapPoint = point;
    }

    public virtual void Job(Man man) { }
}