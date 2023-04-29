public class Order
{
    public Resource Resource { get; private set; }

    public Order(Resource resource)
    {
        Resource = resource;
    }
}