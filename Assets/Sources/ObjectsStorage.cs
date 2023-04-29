using System;
using System.Collections.Generic;
using System.Linq;
using Sources.core;
using UnityEngine;

public class ObjectsStorage : MonoBehaviour, ICoreRegistrable
{
    [SerializeField] private List<ObjectPrefabStruct> m_prefabs;

    public ObjectOnMap GetObjectByType(ObjectType type)
    {
        return Instantiate(m_prefabs.First(o => o.ObjectType == type).ObjectPrefab);
    }
}

[Serializable]
public struct ObjectPrefabStruct
{
    public ObjectType ObjectType;
    public ObjectOnMap ObjectPrefab;
}