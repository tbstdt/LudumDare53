public class Order
{
    public Resource Resource { get;}

    public Resource Reward { get; }

    public Order(Resource resource, int rewardAmount)
    {
        Resource = resource;
        Reward = new Resource(ResourceType.Money, rewardAmount);
    }
}