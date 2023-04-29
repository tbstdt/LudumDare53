using System;
using System.Collections.Generic;
using System.Linq;
using Sources.core;
using UnityEngine;

public class ObjectsStorage : MonoBehaviour, ICoreRegistrable
{
    [SerializeField] private List<ObjectPrefabStruct> m_prefabs;
    [SerializeField] private Transform _poolContainer;
    private Dictionary<ObjectType, List<ObjectOnMap>> _pool = new();

    public ObjectOnMap GetObjectByType(ObjectType type)
    {
        if (_pool.ContainsKey(type) == false) {
            _pool.Add(type, new List<ObjectOnMap>());
        }

        if (_pool.TryGetValue(type, out var list)) {
            if (list.Any()) {
                var value = list.First();
                list.Remove(value);
                return value;
            }
        }

        return Instantiate(m_prefabs.First(o => o.ObjectType == type).ObjectPrefab);
    }

    public void AddObject(ObjectOnMap mapObject) {
        if (_pool.ContainsKey(mapObject.Type) == false) {
            _pool.Add(mapObject.Type, new List<ObjectOnMap>());
        }
        
        _pool[mapObject.Type].Add(mapObject);
        mapObject.transform.SetParent(_poolContainer);
    }
}

[Serializable]
public struct ObjectPrefabStruct
{
    public ObjectType ObjectType;
    public ObjectOnMap ObjectPrefab;
}