using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePlace : ObjectOnMap
{
    public override ObjectType Type => ObjectType.Resource;

    public override int TimerInSeconds => 3;

    protected override void OnObjectClicked()
    {

    }
}