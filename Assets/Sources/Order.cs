public class Order
{
    public Resource Resource { get;}
    public Resource Reward { get; }
    public int ReputationReward { get; }
    public int ReputationPenalty { get; }

    public Order(Resource resource, int rewardAmount, int repReward, int repPenalty)
    {
        Resource = resource;
        Reward = new Resource(ResourceType.Money, rewardAmount);
        ReputationReward = repReward;
        ReputationPenalty = repPenalty;
    }
}