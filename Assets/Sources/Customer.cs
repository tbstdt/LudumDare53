using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : ObjectOnMap
{
    public override ObjectType Type => ObjectType.Customer;

    public override int TimerInSeconds => 3;
}