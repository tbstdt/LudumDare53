using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;

public class Hub : ObjectOnMap, ICoreRegistrable
{
    private Resource m_resources = new Resource();
    private List<Man> m_availableMen = new();

    public override ObjectType Type => ObjectType.Hub;

    private void Start()
    {
        GameCore.Instance.Register<Hub>(this);
    }

    protected override void OnObjectClicked()
    {

    }

    public void TrySendCourier(Customer customer)
    {
        if (m_resources.Amount >= customer.Order.Resource.Amount)
        {
            // send man
        }
    }

    public void TrySendWorker(ResourcePlace resPlace)
    {
        if (m_availableMen.Count > 0)
        {
            // send man
        }
    }
}