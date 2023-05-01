using System.Collections.Generic;
using System.Linq;
using Sources.core;
using Sources.map;
using UnityEngine;

namespace Sources
{
    public class TutorialController : MonoBehaviour, ICoreRegistrable, ICoreInit
    {
        [SerializeField] private OrderSO m_startOrder;
        [SerializeField] private int m_manCountAfterTutorial = 2;

        private Dictionary<ResourcePlace, int> m_defaultAmount = new();
        private List<Customer> m_customers = new();

        private Customer m_customerForOrder;

        private void OnObjectSpawnedHandler(ObjectOnMap objectOnMap)
        {
            var place = objectOnMap as ResourcePlace;
            if (place != null)
            {
                m_defaultAmount.Add(place, place.Resource.Amount);

                var newAmount = 0;
                var resData = m_startOrder.Resources.Where(r => r.Type == place.Resource.Type).ToList();
                if (resData.Count > 0)
                    newAmount = resData.First().Amount;

                place.ChangeAmount(newAmount, true);
            }
            else
            {
                var customer = objectOnMap as Customer;
                if (customer != null)
                {
                    m_customers.Add(customer);
                    if (m_customerForOrder == null)
                    {
                        m_customerForOrder = customer;
                        StartTutorial();
                    }
                }
            }
        }

        private void StartTutorial()
        {
            m_customerForOrder.ShowTutorialOrder(m_startOrder);
            m_customerForOrder.OnOrderComplete = OnTutorialFinish;
        }

        private void OnTutorialFinish()
        {
            foreach (var pair in m_defaultAmount)
                pair.Key.ChangeAmount(pair.Value);
            m_customers.ForEach(c => c.StartFirstOrders());
            GameCore.Instance.Get<Hub>().SetManCount(m_manCountAfterTutorial);

            m_customerForOrder.OnOrderComplete = null;
            m_customerForOrder = null;
            m_defaultAmount.Clear();
            m_customers.Clear();
        }

        public void Init()
        {
            GameCore.Instance.Get<MapGenerator>().OnObjectSpawned = OnObjectSpawnedHandler;
        }
    }
}