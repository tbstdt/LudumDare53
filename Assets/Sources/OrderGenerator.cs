using System.Collections.Generic;
using Sources.core;
using UnityEngine;

public class OrderGenerator : MonoBehaviour, ICoreRegistrable
{
   [SerializeField] private List<OrderSO> m_orders;

   public OrderSO GetOrderData()
   {
      return m_orders[Random.Range(0, m_orders.Count)];
   }
}