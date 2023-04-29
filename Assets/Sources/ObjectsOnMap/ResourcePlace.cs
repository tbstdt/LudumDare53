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
        var takenResource = new Resource(_resource.Type);
        if (hub.ResourcePerSecond.TryGetValue(_resource.Type, out var resourcePerSecond) == false) {
            yield break;
        }

        for (int i = 0; i < hub.TimeToWork; i++) {
            if (_resource.Amount - resourcePerSecond >= 0) {
                yield return new WaitForSeconds(1);
                takenResource.Amount += resourcePerSecond;
                _resource.Amount -= resourcePerSecond;
            }

            if (_resource.Amount < resourcePerSecond) {
                yield return new WaitForSeconds(1);
                takenResource.Amount += _resource.Amount;
                _resource.Amount = 0;
            }
            
        }
        
        
        GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, hub, takenResource);
    }
}