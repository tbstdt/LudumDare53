using Sources.core;

public class ResourcePlace : ObjectOnMap
{
    public override ObjectType Type => ObjectType.Resource;

    public override int TimerInSeconds => 3;

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendWorker(this);
    }
}