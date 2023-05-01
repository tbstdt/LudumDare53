using System.Collections;
using System.Collections.Generic;
using Sources.core;
using Sources.map;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sources
{
    public class RandomResourceGenerator : MonoBehaviour, ICoreRegistrable
    {
        [SerializeField] private List<Resource> m_randomResoures;
        [SerializeField] private List<GameObject> m_mapPoints;
        [SerializeField] private float m_spawnTime = 20;

        private List<GameObject> m_availablePoints;
        private Dictionary<ObjectOnMap, GameObject> m_usedPoint;

        private void Start()
        {
            m_availablePoints = new List<GameObject>(m_mapPoints);
            m_usedPoint = new Dictionary<ObjectOnMap, GameObject>();

            GameCore.Instance.Register<RandomResourceGenerator>(this);
        }

        public void StartSpawns()
        {
            StartCoroutine(SpawnPoint());
        }

        private IEnumerator SpawnPoint()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(m_spawnTime);

                if (m_availablePoints.Count > 0)
                {
                    var point = m_availablePoints[Random.Range(0, m_availablePoints.Count)];
                    m_availablePoints.Remove(point);

                    var objectOnMap = GameCore.Instance.Get<MapGenerator>().SpawnRandomObject(point);
                    var place = (RandomResourcePlace)objectOnMap;
                    var resource = m_randomResoures[Random.Range(0, m_randomResoures.Count)];
                    place.SetResource(new Resource(resource.Type, resource.Amount));
                    place.OnHide = OnPlaceDestroy;
                    m_usedPoint.Add(objectOnMap, point);
                }
            }
        }

        private void OnPlaceDestroy(RandomResourcePlace objectOnMap)
        {
            var point = m_usedPoint[objectOnMap];
            m_availablePoints.Add(point);
            m_usedPoint.Remove(objectOnMap);
            objectOnMap.OnHide = null;
        }
    }
}