using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Resource Resource { get; private set; }

    public Order(int resoureAmount)
    {
        Resource = new Resource(resoureAmount);
    }
}