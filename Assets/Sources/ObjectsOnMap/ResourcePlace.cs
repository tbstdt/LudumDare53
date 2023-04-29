using System;
using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using UnityEngine;
using UnityEngine.UI;

public class ResourcePlace : ObjectOnMap
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Text _resourceCountText;
    [SerializeField] private Resource _resource;

    private int _menInside = 0;

    public override ObjectType Type => ObjectType.Resource;

    private void Start() {
        _resourceCountText.text = _resource.Amount.ToString();
    }

    protected override void OnObjectClicked()
    {
        if (_resource.Amount <= 0) {
            return;
        }
        
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendWorker(this);
    }
    
    public override void Job(Man man) {
        _menInside++;
        StartCoroutine(GatherResource());
    }

    private IEnumerator GatherResource() {
        var hub = GameCore.Instance.Get<Hub>();
        var takenResource = new Resource(_resource.Type);
        if (hub.ResourcePerSecond.TryGetValue(_resource.Type, out var resourcePerSecond)) {
            for (int i = 0; i < hub.TimeToWork; i++) {
                if (_resource.Amount <= 0) {
                    continue;
                }

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

                _resourceCountText.text = _resource.Amount.ToString();
            }
        }

        GameCore.Instance.Get<MapManager>().LaunchMan(this, hub, takenResource);
        _menInside--;

        if (_menInside == 0 && _resource.Amount == 0) {
            _view.SetActive(false);
        }
    }
}