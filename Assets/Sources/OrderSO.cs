using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "create " + nameof(OrderSO))]
public class OrderSO : ScriptableObject
{
    public ResourceType Type;
    public int Amount;
    public int TimeInSeconds;
    public int Reward;
}