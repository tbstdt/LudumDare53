using System.Collections;
using System.Collections.Generic;
using Sources.core;
using UnityEngine;

public class ObjectsStorage : ICoreRegistrable
{
    private Dictionary<ObjectType, ObjectOnMap> m_objectsbyType = new Dictionary<ObjectType, ObjectOnMap>();
}