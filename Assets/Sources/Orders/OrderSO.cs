using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Order", menuName = "create " + nameof(OrderSO))]
public class OrderSO : ScriptableObject {
    
    public List<Resource> Resources;
    public int TimeInSeconds;
    public int MoneyReward;
    public int ReputationReward;
    public int ReputationPenalty;

    public int StartOrderRequirementDeliveryCount;
    public int EndOrderRequirementDeliveryCount;
}