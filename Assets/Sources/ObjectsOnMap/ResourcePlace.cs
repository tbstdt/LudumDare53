using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using UnityEngine;

public class ResourcePlace : ObjectOnMap
{
    [SerializeField] private Resource _resource;

    public override ObjectType Type => ObjectType.Resource;

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendWorker(this);
    }
    
    public override void Job(Man man) {
        StartCoroutine(GatherResource());
    }

    private IEnumerator GatherResource() {
        var hub = GameCore.Instance.Get<Hub>();
        yield return new WaitForSeconds(hub.TimeToWork);
        GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, hub, null);
    }
}