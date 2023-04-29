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

    public void TrySendCourier(Customer customer)
    {
        //if (m_resources.Amount >= customer.Order.Resource.Amount)
        {
            // send man
        }
    }

    public void TrySendWorker(ResourcePlace resPlace) {

        if (m_availableMen > 0)
        {
            // send man
            GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, resPlace);
            m_availableMen--;
        }
    }

    public override void Job(Man man) {
        m_availableMen++;
    }
}