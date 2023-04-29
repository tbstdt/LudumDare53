using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    Resource,
    Customer,
    Hub
}

public abstract class ObjectOnMap : MonoBehaviour
{
    public abstract ObjectType Type { get; }
}