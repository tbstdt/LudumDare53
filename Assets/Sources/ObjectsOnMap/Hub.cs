using Sources.core;

public class Hub : ObjectOnMap, ICoreRegistrable
{
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
UnityEngine.Debug.Log("click");
    }
}