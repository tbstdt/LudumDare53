using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ObjectOnMap : MonoBehaviour, IPointerClickHandler
{
    public abstract ObjectType Type { get; }
    public GameObject MapPoint { get; private set; }

    public virtual int TimerInSeconds { get; } = -1;

    protected abstract void OnObjectClicked();

    public void OnPointerClick(PointerEventData eventData) => OnObjectClicked();

    public void AddMapPoint(GameObject point) {
        MapPoint = point;
    }
}