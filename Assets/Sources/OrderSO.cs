using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "create " + nameof(OrderSO))]
public class OrderSO : ScriptableObject
{
    public string OrderName;
    public ResourceType Type;
    public int Amount;
    public int TimeInSeconds;
    public int MoneyReward;
    public int ReputationReward;
    public int ReputationPenalty;
}