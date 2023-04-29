using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using UnityEngine;

public class Hub : ObjectOnMap, ICoreRegistrable {
    [SerializeField] private int _manCount = 3;

    private Dictionary<ResourceType, int> m_resources;

    private int m_availableMen;

    public int TimeToWork = 10;

    public override ObjectType Type => ObjectType.Hub;

    private void Start()
    {
        GameCore.Instance.Register<Hub>(this);
        m_availableMen = _manCount;
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
            GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, customer, customer.Order.Resource);
            m_availableMen--;
            return true;
        }

        return false;
    }

    public void TrySendWorker(ResourcePlace resPlace) {

        if (m_availableMen > 0)
        {
            GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, resPlace, null);
            m_availableMen--;
        }
    }

    public override void Job(Man man) {
        m_availableMen++;
    }
}