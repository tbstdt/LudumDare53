using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_text;

    private Order m_order;

    public override ObjectType Type => ObjectType.Customer;

    public override int TimerInSeconds => 3;

    private void Start()
    {
        var amount = Random.Range(0, 10);
        m_order = new Order(amount);
        m_text.text = amount.ToString();
    }
}