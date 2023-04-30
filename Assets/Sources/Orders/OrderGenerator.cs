using System.Collections.Generic;
using Sources.core;
using UnityEngine;

public class OrderGenerator : MonoBehaviour, ICoreRegistrable
{
   [SerializeField] private int _reorderTimeSecondsMin = 15;
   [SerializeField] private int _reorderTimeSecondsMax = 15;
   

   [SerializeField] private List<OrderSO> m_orders;

   public OrderSO GetOrderData()
   {
      return m_orders[Random.Range(0, m_orders.Count)];
   }

   public int getReorderTime() {
      return Random.Range(_reorderTimeSecondsMin, _reorderTimeSecondsMax + 1);
   }
}