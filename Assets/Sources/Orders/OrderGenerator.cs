using System.Collections.Generic;
using System.Linq;
using Sources.core;
using UnityEngine;

public class OrderGenerator : MonoBehaviour, ICoreRegistrable
{
   [SerializeField] private int _reorderTimeSecondsMin = 15;
   [SerializeField] private int _reorderTimeSecondsMax = 15;
   

   [SerializeField] private List<OrderSO> m_orders;

   public OrderSO GetOrderData()
   {
      var currentDeliveryCount = GameCore.Instance.Get<Hub>().CurrentDeliveryCount;

      var orders = m_orders
         .Where(x => currentDeliveryCount >= x.StartOrderRequirementDeliveryCount
                     && currentDeliveryCount <= x.EndOrderRequirementDeliveryCount)
         .ToList();

      return orders[Random.Range(0, orders.Count)];
   }

   public int getReorderTime() {
   
      return Random.Range(_reorderTimeSecondsMin, _reorderTimeSecondsMax + 1);
   }
}