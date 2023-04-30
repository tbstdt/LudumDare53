using Sources.core;
using UnityEngine;

public class RandomResourcePlace : ResourcePlace
{
    [SerializeField] private GameObject _resourceView;

    public void SetResource(Resource resource)
    {
        _resource = resource;
        _resourceView.SetActive(false);
    }
}