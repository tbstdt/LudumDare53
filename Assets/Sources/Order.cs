public class Order
{
    public string OrderName { get; }
    public Resource Resource { get;}
    public Resource Reward { get; }
    public int ReputationReward { get; }
    public int ReputationPenalty { get; }

    public Order(OrderSO order)
    {
        OrderName = order.OrderName;
        Resource = new Resource(order.Type, order.Amount);
        Reward = new Resource(ResourceType.Money, order.MoneyReward);
        ReputationReward = order.ReputationReward;
        ReputationPenalty = order.ReputationPenalty;
    }
}