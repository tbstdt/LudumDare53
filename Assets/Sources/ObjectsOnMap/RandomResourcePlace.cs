using System;
using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using UnityEngine;

public class RandomResourcePlace : ResourcePlace
{
    [SerializeField] private GameObject _resourceView;

    public Action<RandomResourcePlace> OnHide;

    public void SetResource(Resource resource)
    {
        _resource = resource;
        _resourceView.SetActive(false);
    }

    public override void Job(Man man)
    {
        GameCore.Instance.Get<MapManager>().LaunchMan(this, GameCore.Instance.Get<Hub>(), new List<Resource>{_resource});
        _resource.Amount = 0;

        _view.SetActive(false);
        OnHide?.Invoke(this);
    }
}