using Sources.core;
using TMPro;
using UnityEngine;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_text;

    public Order Order { get; private set; }

    public override ObjectType Type => ObjectType.Customer;

    public override int TimerInSeconds => 3;

    private void Start()
    {
        var amount = Random.Range(0, 10);
        Order = new Order(amount);
        m_text.text = amount.ToString();
    }

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendCourier(this);
    }
}