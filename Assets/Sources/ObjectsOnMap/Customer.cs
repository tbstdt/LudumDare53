using System;
using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_text;
    [Space]
    [SerializeField] private TextMeshProUGUI m_timerTime;
    [SerializeField] private Image m_timerView;
    [SerializeField] private GameObject m_timerGO;

    private int m_timerInSeconds;

    public Order Order { get; private set; }

    public override ObjectType Type => ObjectType.Customer;

    private void Start()
    {
        var order = GameCore.Instance.Get<OrderGenerator>().GetOrderData();

        Order = new Order(new Resource(order.Type, order.Amount));
        m_text.text = order.Amount.ToString();
        m_timerInSeconds = order.TimeInSeconds;

        if (m_timerInSeconds <= 0)
        {
            m_timerGO.SetActive(false);
            return;
        }

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        m_timerGO.SetActive(true);
        float time = m_timerInSeconds;

        while (time > 0)
        {
            time -= Time.deltaTime;

            m_timerTime.text = Math.Ceiling(time).ToString();
            m_timerView.fillAmount = time / m_timerInSeconds;

            yield return null;
        }

        m_timerGO.SetActive(false);
    }

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendCourier(this);
    }

    public override void Job(Man man) {

    }
}