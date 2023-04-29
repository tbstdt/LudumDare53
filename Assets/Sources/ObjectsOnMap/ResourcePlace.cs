using Sources.core;

public class ResourcePlace : ObjectOnMap
{
    public override ObjectType Type => ObjectType.Resource;

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendWorker(this);
    }
}