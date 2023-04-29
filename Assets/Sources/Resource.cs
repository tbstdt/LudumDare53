using System;

[Serializable]
public class Resource {
    public int Amount;
    public ResourceType Type;

    public Resource(ResourceType type, int resoureAmount = 0)
    {
        Amount = resoureAmount;
        Type = type;
    }
}