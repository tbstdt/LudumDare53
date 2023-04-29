using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using UnityEngine;

public class Hub : ObjectOnMap, ICoreRegistrable {
    [SerializeField] private int _manCount = 3;

    private Dictionary<ResourceType, int> m_resources = new () {
        {ResourceType.One, 0},
        {ResourceType.Two, 0},
        {ResourceType.Three, 0},
        {ResourceType.Money, 0},
    };

    private int m_availableMen;
    private int _maxMenCount;
    public int TimeToWork = 10;
    
    public Dictionary<ResourceType, int> ResourcePerSecond = new () {
        {ResourceType.One, 1},
        {ResourceType.Two, 2},
        {ResourceType.Three, 3},
    };

    public Dictionary<ResourceType, int> Resources => m_resources;

    public override ObjectType Type => ObjectType.Hub;

    private void Start()
    {
        GameCore.Instance.Register<Hub>(this);
        m_availableMen = _manCount;
        _maxMenCount = _manCount;
        updateResource();
    }

    protected override void OnObjectClicked()
    {

    }

    public bool TrySendCourier(Customer customer)
    {
        var orderResource = customer.Order.Resource;

        if (m_resources[orderResource.Type] >= orderResource.Amount)
        {
            m_resources[orderResource.Type] -= orderResource.Amount;
            GameCore.Instance.Get<MapManager>().LaunchMan(this, customer, customer.Order.Resource);
            m_availableMen--;
            updateResource();
            return true;
        }

        return false;
    }

    public void TrySendWorker(ResourcePlace resPlace) {
        if (m_availableMen > 0)
        {
            GameCore.Instance.Get<MapManager>().LaunchMan(this, resPlace, null);
            m_availableMen--;
            updateResource();
        }
    }

    public override void Job(Man man) {
        m_availableMen++;
        if (man.Resource != null) {
            m_resources[man.Resource.Type] += man.Resource.Amount;
        }
        updateResource();
    }

    private void updateResource() {
        GameCore.Instance.Get<UIManager>().UpdateResource(m_resources, m_availableMen, _maxMenCount);
    }
}