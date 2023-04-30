using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : ObjectOnMap
{
    [SerializeField] private GameObject _resourceOne;
    [SerializeField] private GameObject _resourceTwo;
    [SerializeField] private GameObject _resourceThree;
    [Space]
    [SerializeField] private TextMeshProUGUI m_resourceOneAmountText;
    [SerializeField] private TextMeshProUGUI m_resourceTwoAmountText;
    [SerializeField] private TextMeshProUGUI m_resourceThreeAmountText;
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
        m_timerGO.SetActive(false);
        m_orderGenerator = GameCore.Instance.Get<OrderGenerator>();
        _reputationView.UpdateReputation(m_reputation);
        StartCoroutine(ReorderTimer(m_StartFirstQuest));
    }

    private void StartOrder()
    {
        var order = m_orderGenerator.GetOrderData();

        Order = new Order(order);

        _resourceOne.SetActive(false);
        _resourceTwo.SetActive(false);
        _resourceThree.SetActive(false);

        foreach (var resource in Order.Resources) {
            switch (resource.Type) {
                case ResourceType.One:
                    _resourceOne.SetActive(true);
                    m_resourceOneAmountText.text = resource.Amount.ToString();
                    break;
                case ResourceType.Two:
                    _resourceTwo.SetActive(true);
                    m_resourceTwoAmountText.text = resource.Amount.ToString();
                    break;
                case ResourceType.Three:
                    _resourceThree.SetActive(true);
                    m_resourceThreeAmountText.text = resource.Amount.ToString();
                    break;
            }
        }

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

        if (Order != null)
        {
            updateReputation(-Order.ReputationPenalty);
            if (!m_manOnRoad)
                Order = null;
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

        var reward = Order != null ? new List<Resource> { Order.Reward } : null;
        
        mapManager.LaunchMan(this, hub, reward);

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
        GameCore.Instance.Get<Hub>().UpdateOrders();
    }

    private void updateReputation(int value) {
        if (m_reputation == 0) {
            m_timerGO.SetActive(false);
            GameCore.Instance.Get<EndGamePanel>().Show(false);
            return;
        }

        m_reputation += value;
        m_reputation = Mathf.Clamp(m_reputation, 0, 5);
        _reputationView.UpdateReputation(m_reputation);
    }
}