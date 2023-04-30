public class Order
{
    public Resource Resource { get;}
    public Resource Reward { get; }
    public int ReputationReward { get; }
    public int ReputationPenalty { get; }

    public Order(OrderSO order)
    {
        Resource = new Resource(order.Type, order.Amount);
        Reward = new Resource(ResourceType.Money, order.MoneyReward);
        ReputationReward = order.ReputationReward;
        ReputationPenalty = order.ReputationPenalty;
    }
}