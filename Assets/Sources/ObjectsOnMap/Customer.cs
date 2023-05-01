using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sources.core;
using Sources.Editor;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using TMPro;
using UnityEditor.TextCore.Text;
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
    [SerializeField] private TextMeshProUGUI m_costAmountText;
    [Space]
    [SerializeField] private int m_StartFirstQuest = 10;
    [SerializeField] private int m_reputation = 3;
    [SerializeField] private ReputationView _reputationView;
    [SerializeField] private ResourceBalloon _resourceBalloon;

    private OrderGenerator m_orderGenerator;

    private int m_timerInSeconds;
    private bool m_timeout;
    private bool m_manOnRoad;

    public Order Order { get; private set; }

    public Action OnOrderComplete;

    private void Start()
    {
        m_orderGenerator = GameCore.Instance.Get<OrderGenerator>();
        _reputationView.UpdateReputation(m_reputation);
    }

    private void StartOrder(OrderSO order)
    {
        Order = new Order(order);
        m_costAmountText.text = order.MoneyReward.ToString();

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
        StartOrder(m_orderGenerator.GetOrderData());
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
        StopAllCoroutines();
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

    private void UpdateOrders() {
        GameCore.Instance.Get<Hub>().UpdateOrders();
    }

    private void updateReputation(int value) {
        m_reputation += value;
        m_reputation = Mathf.Clamp(m_reputation, -1, 4);
        _reputationView.UpdateReputation(m_reputation);

        if (m_reputation < 0) {
            m_timerGO.SetActive(false);
            Debug.Log($"You lose because reputation is {m_reputation} in {gameObject.scene.path}");
            GameCore.Instance.Get<EndGamePanel>().Show(false);
        }
    }

    public override void Job(Man man) {
        m_manOnRoad = false;

        var mapManager = GameCore.Instance.Get<MapManager>();
        var hub = GameCore.Instance.Get<Hub>();

        if (Order != null)
        {
            UpdateOrders();
            OnOrderComplete?.Invoke();
        }

        var reward = Order != null ? new List<Resource> { Order.Reward } : null;
        _resourceBalloon.Show(man.Resources);

        mapManager.LaunchMan(this, hub, reward);

        if (!m_timeout)
        {
            m_timeout = true;
            m_timerGO.SetActive(false);

            playSound(SoundType.Positive);
            updateReputation(Order.ReputationReward);
        }
        else {
            playSound(SoundType.Neutral);
        }

        Order = null;

        StartCoroutine(ReorderTimer(m_orderGenerator.getReorderTime()));
    }

    public void playSound(SoundType type) {
        var soundManager = GameCore.Instance.Get<SoundManager>();
        
        if (Type.HasFlag(ObjectType.Aliens)) {
            soundManager.PlaySound(SoundType.Alien | type);
            return;
        }
        if (Type.HasFlag(ObjectType.Bikers)) {
            soundManager.PlaySound(SoundType.Biker | type);
            return;
        }
        if (Type.HasFlag(ObjectType.Cowboy)) {
            soundManager.PlaySound(SoundType.Cowboy | type);
            return;
        }
        if (Type.HasFlag(ObjectType.Robot)) {
            soundManager.PlaySound(SoundType.Robot | type);
            return;
        }
        if (Type.HasFlag(ObjectType.Vault)) {
            soundManager.PlaySound(SoundType.Mutant | type);
        }
        
    }

    public void ShowTutorialOrder(OrderSO order)
    {
        StartOrder(order);
    }

    public void StartFirstOrders()
    {
        StartCoroutine(ReorderTimer(m_StartFirstQuest));
    }
}