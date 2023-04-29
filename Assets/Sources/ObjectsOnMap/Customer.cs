using System;
using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_text;
    [Space]
    [SerializeField] private TextMeshProUGUI m_timerTime;
    [SerializeField] private Image m_timerView;
    [SerializeField] private GameObject m_timerGO;

    private OrderGenerator m_orderGenerator;

    private int m_timerInSeconds;
    private bool m_timeout;
    private bool m_manOnRoad;

    private int m_reputation;

    public Order Order { get; private set; }

    public override ObjectType Type => ObjectType.Customer;

    private void Start()
    {
        m_orderGenerator = GameCore.Instance.Get<OrderGenerator>();

        StartOrder();
    }

    private IEnumerator StartTimer()
    {
        m_timerGO.SetActive(true);
        m_timeout = false;
        float time = m_timerInSeconds;

        while (time > 0 && !m_timeout)
        {
            time -= Time.deltaTime;

            m_timerTime.text = Math.Ceiling(time).ToString();
            m_timerView.fillAmount = time / m_timerInSeconds;

            yield return null;
        }

        m_timerGO.SetActive(false);
        OnTimeout();
    }

    private void OnTimeout()
    {
        m_timeout = true;
        if (!m_manOnRoad)
        {
            m_reputation -= Order.ReputationPenalty;
            Order = null;
        }
    }

    protected override void OnObjectClicked()
    {
        if (m_timeout)
            return;

        var hub = GameCore.Instance.Get<Hub>();
        m_manOnRoad = hub.TrySendCourier(this);
    }

    public override void Job(Man man) {
        if (!m_timeout)
        {
            StopCoroutine(StartTimer());
            m_timeout = true;
            m_timerGO.SetActive(false);
        }

        m_manOnRoad = false;

        var hub = GameCore.Instance.Get<Hub>();
        Resource resource = Order.Reward;
        GameCore.Instance.Get<MapManager>().LaunchMan(MapPoint, hub, resource);
    }

    public void StartOrder()
    {
        var order = m_orderGenerator.GetOrderData();

        Order = new Order(new Resource(order.Type, order.Amount), order.Reward, order.ReputationReward, order.ReputationPenalty);
        m_text.text = order.Amount.ToString();
        m_timerInSeconds = order.TimeInSeconds;

        if (m_timerInSeconds <= 0)
        {
            m_timerGO.SetActive(false);
            return;
        }

        StartCoroutine(StartTimer());
    }
}