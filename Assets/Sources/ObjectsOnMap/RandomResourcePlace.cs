using System;
using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using UnityEngine;

public class RandomResourcePlace : ResourcePlace
{
    [SerializeField] private GameObject _resourceView;

    private bool m_manOnRoad;

    public Action<RandomResourcePlace> OnHide;

    public void SetResource(Resource resource)
    {
        _resource = resource;
        _resourceView.SetActive(false);
    }

    protected override void OnObjectClicked()
    {
        if (_resource.Amount <= 0 || m_manOnRoad)
            return;

        var hub = GameCore.Instance.Get<Hub>();
        m_manOnRoad = hub.TrySendWorker(this);
    }

    public override void Job(Man man)
    {
        GameCore.Instance.Get<MapManager>().LaunchMan(this, GameCore.Instance.Get<Hub>(),
            new List<Resource> {new (_resource.Type, _resource.Amount)});
        _resource.Amount = 0;

        _view.SetActive(false);
        OnHide?.Invoke(this);
    }
}