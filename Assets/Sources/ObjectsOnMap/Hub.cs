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
        GameCore.Instance.Get<UIManager>().UpdateResource(m_resources);
    }

    protected override void OnObjectClicked()
    {

    }

    public void TrySendCourier(Customer customer)
    {
        var orderResource = customer.Order.Resource;

        if (m_resources[orderResource.Type] >= orderResource.Amount)
        {
            // send man
            GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, customer, null);
            m_availableMen--;
        }
    }

    public void TrySendWorker(ResourcePlace resPlace) {

        if (m_availableMen > 0)
        {
            // send man
            GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, resPlace, null);
            m_availableMen--;
        }
    }

    public override void Job(Man man) {
        m_availableMen++;
        if (man.Resource != null) {
            m_resources[man.Resource.Type] += man.Resource.Amount;
        }
        GameCore.Instance.Get<UIManager>().UpdateResource(m_resources);
    }
}