using System;

[Serializable]
public class Resource
{
    public int Amount { get; set; }
    public ResourceType Type { get; private set; }

    public Resource(ResourceType type, int resoureAmount = 0)
    {
        Amount = resoureAmount;
        Type = type;
    }
}