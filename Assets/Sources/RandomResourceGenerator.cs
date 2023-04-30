using System.Collections;
using System.Collections.Generic;
using Sources.core;
using Sources.map;
using UnityEngine;

public class RandomResourceGenerator : MonoBehaviour, ICoreRegistrable
{
    [SerializeField] private List<Resource> m_randomResoures;
    [SerializeField] private List<GameObject> m_mapPoints;
    [SerializeField] private float m_spawnTime = 20;

    private void Start()
    {
        GameCore.Instance.Register<RandomResourceGenerator>(this);
        StartCoroutine(SpawnPoint());
    }

    private IEnumerator SpawnPoint()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(m_spawnTime);
            GameCore.Instance.Get<MapGenerator>().
                SpawnObject(m_randomResoures[Random.Range(0, m_randomResoures.Count)], m_mapPoints[Random.Range(0, m_mapPoints.Count)]);
        }
    }
}