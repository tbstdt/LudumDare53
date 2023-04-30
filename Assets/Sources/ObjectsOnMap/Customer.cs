using System;
using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_resourceAmountText;
    [Space]
    [SerializeField] private TextMeshProUGUI m_timerTime;
    [SerializeField] private Image m_timerView;
    [SerializeField] private GameObject m_timerGO;
    [Space]
    [SerializeField] private int m_StartFirstQuest = 10;
    [SerializeField] private int m_reputation = 3;
    [SerializeField] private ReputationView _reputationView;

    private OrderGenerator m_orderGenerator;

    private int m_timerInSeconds;
    private bool m_timeout;
    private bool m_manOnRoad;

    public Order Order { get; private set; }

    public override ObjectType Type => ObjectType.Customer;

    private void Start()
    {
        m_orderGenerator = GameCore.Instance.Get<OrderGenerator>();
        _reputationView.UpdateReputation(m_reputation);
        StartCoroutine(ReorderTimer(m_StartFirstQuest));
    }

    private void StartOrder()
    {
        var order = m_orderGenerator.GetOrderData();

        Order = new Order(order);
        m_resourceAmountText.text = order.Amount.ToString();
        m_timerInSeconds = order.TimeInSeconds;

        if (m_timerInSeconds <= 0)
        {
            m_timerGO.SetActive(false);
            return;
        }

        StartCoroutine(StartTimer());
    }

    private IEnumerator ReorderTimer(int time)
    {
        yield return new WaitForSeconds(time);
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

        if (!m_manOnRoad && Order != null)
        {
            updateReputation(-Order.ReputationPenalty);
            Order = null;
        }

        if (m_manOnRoad && Order != null) {
            updateReputation(-Order.ReputationPenalty);
        }

        StartCoroutine(ReorderTimer(m_orderGenerator.getReorderTime()));
    }

    protected override void OnObjectClicked()
    {
        if (m_timeout || m_manOnRoad)
            return;

        var hub = GameCore.Instance.Get<Hub>();
        m_manOnRoad = hub.TrySendCourier(this);
    }

    public override void Job(Man man) {
        m_manOnRoad = false;

        var mapManager = GameCore.Instance.Get<MapManager>();
        var hub = GameCore.Instance.Get<Hub>();

        if (Order != null)
            UpdateOrders();

        mapManager.LaunchMan(this, hub, Order?.Reward);

        if (!m_timeout)
        {
            m_timeout = true;
            m_timerGO.SetActive(false);

            updateReputation(Order.ReputationReward);
        }
      
        Order = null;

        StartCoroutine(ReorderTimer(m_orderGenerator.getReorderTime()));
    }

    private void UpdateOrders() {
        GameCore.Instance.Get<UIManager>().UpdateOrders();
    }

    private void updateReputation(int value) {
        m_reputation += value;
        m_reputation = Mathf.Clamp(m_reputation, 0, 5);
        _reputationView.UpdateReputation(m_reputation);
    }
}