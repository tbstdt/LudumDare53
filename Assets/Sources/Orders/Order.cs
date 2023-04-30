using System.Collections.Generic;

public class Order
{
    public List<Resource> Resources { get;}
    public Resource Reward { get; }
    public int ReputationReward { get; }
    public int ReputationPenalty { get; }

    public Order(OrderSO order) {
        Resources = new List<Resource>(order.Resources.Count);
        foreach (var resource in order.Resources) {
            Resources.Add(new Resource(resource.Type, resource.Amount));
        }
        Reward = new Resource(ResourceType.Money, order.MoneyReward);
        ReputationReward = order.ReputationReward;
        ReputationPenalty = order.ReputationPenalty;
    }
}