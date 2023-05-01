using System;
using System.Collections.Generic;
using Sources.core;
using Sources.Editor;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using UnityEngine;
using UnityEngine.UI;

public class Hub : ObjectOnMap, ICoreRegistrable {
    [SerializeField] private int _manCount = 3;
    [SerializeField] private float _manSpeed = 3f;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private UpgradesDataSO _upgradesData;
    [SerializeField] private ResourceBalloon _resourceBalloon;

    [SerializeField] private Image _canUpgrade;

    private float _speedUpgrade = 1;
    private float _rpsUpgrade = 1;

    private Dictionary<ResourceType, int> m_resources = new () {
        {ResourceType.One, 0},
        {ResourceType.Two, 0},
        {ResourceType.Three, 0},
        {ResourceType.Money, 0},
    };

    private int m_availableMen;
    private int _maxMenCount;
    public int TimeToWork = 10;

    private Dictionary<ResourceType, int> m_resourcePerSecond = new () {
        {ResourceType.One, 1},
        {ResourceType.Two, 1},
        {ResourceType.Three, 1},
    };

    public float ManSpeed => _manSpeed / _speedUpgrade;
    public int CurrentDeliveryCount = 0;

    public Dictionary<ResourceType, int> Resources => m_resources;

    public Action OnManArrived;

    private void Start()
    {
        GameCore.Instance.Register<Hub>(this);
        m_availableMen = _manCount;
        _maxMenCount = _manCount;
        UpdateResource();

        _upgradePanel.OnMenUpgradeClick.AddListener(MenUpgradeHandler);
        _upgradePanel.OnSpeedUpgradeClick.AddListener(SpeedUpgradeHandler);
        _upgradePanel.OnWeightUpgradeClick.AddListener(WeightUpgradeHandler);
    }

    #region Upgrades

    private int _curSpeedUpgradeIndex;
    private int _curWeightUpgradeIndex;
    private int _curMenUpgradeIndex;

    protected override void OnObjectClicked()
    {
        if(!_upgradePanel.SwitchActive())
            return;

        UpdateAllUpgradesView();
    }

    private void UpdateAllUpgradesView()
    {
        UpdateSpeedUpgrade();
        UpdateWeightUpgrade();
        UpdateMenUpgrade();
    }

    private bool canUpgrade() {
        var canUpgrade = false;

        canUpgrade |= GetViewData(out bool _, out int _, out bool _, _curSpeedUpgradeIndex, _upgradesData.SpeedUpgrades);
        canUpgrade |= GetViewData(out bool _, out int _, out bool _, _curWeightUpgradeIndex, _upgradesData.WeightUpgrades);
        canUpgrade |= GetViewData(out bool _, out int _, out bool _, _curMenUpgradeIndex, _upgradesData.MenUpgrades);

        return canUpgrade;
    }

    private void UpdateSpeedUpgrade()
    {
        GetViewData(out bool btnActive, out int cost, out bool interactable, _curSpeedUpgradeIndex, _upgradesData.SpeedUpgrades);
        _upgradePanel.SetSpeedUpgrade(btnActive, cost, interactable);
    }

    private void UpdateWeightUpgrade()
    {
        GetViewData(out bool btnActive, out int cost, out bool interactable, _curWeightUpgradeIndex, _upgradesData.WeightUpgrades);
        _upgradePanel.SetWeightUpgrade(btnActive, cost, interactable);
    }

    private void UpdateMenUpgrade()
    {
        GetViewData(out bool btnActive, out int cost, out bool interactable, _curMenUpgradeIndex, _upgradesData.MenUpgrades);
        _upgradePanel.SetMenUpgrade(btnActive, cost, interactable);
    }

    private bool GetViewData(out bool btnActive, out int cost, out bool interactable, int index, List<UpgradeData> data)
    {
        btnActive = index < data.Count;
        cost = btnActive ? data[index].Cost : -1;
        interactable = btnActive && m_resources[ResourceType.Money] >= data[index].Cost;

        return interactable;
    }

    private void SpeedUpgradeHandler()
    {
        if (m_resources[ResourceType.Money] >= _upgradesData.SpeedUpgrades[_curSpeedUpgradeIndex].Cost) {
            m_resources[ResourceType.Money] -= _upgradesData.SpeedUpgrades[_curSpeedUpgradeIndex].Cost;
            _speedUpgrade = _upgradesData.SpeedUpgrades[_curSpeedUpgradeIndex].ValueChange;
            _curSpeedUpgradeIndex++;
            GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Positive);
        }

        UpdateAllUpgradesView();
        UpdateResource();
    }

    private void WeightUpgradeHandler()
    {
        if (m_resources[ResourceType.Money] >= _upgradesData.WeightUpgrades[_curSpeedUpgradeIndex].Cost) {
            m_resources[ResourceType.Money] -= _upgradesData.WeightUpgrades[_curWeightUpgradeIndex].Cost;
            _rpsUpgrade = _upgradesData.WeightUpgrades[_curWeightUpgradeIndex].ValueChange;
            _curWeightUpgradeIndex++;
            GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Positive);
        }

        UpdateAllUpgradesView();
        UpdateResource();
    }

    private void MenUpgradeHandler()
    {
        if (m_resources[ResourceType.Money] >= _upgradesData.MenUpgrades[_curSpeedUpgradeIndex].Cost) {
            m_resources[ResourceType.Money] -= _upgradesData.MenUpgrades[_curMenUpgradeIndex].Cost;
            m_availableMen += (int)_upgradesData.MenUpgrades[_curMenUpgradeIndex].ValueChange;
            _maxMenCount += (int)_upgradesData.MenUpgrades[_curMenUpgradeIndex].ValueChange;
            _curMenUpgradeIndex++;
            GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Positive);
        }

        UpdateAllUpgradesView();
        UpdateResource();
    }

    #endregion

    public void SetManCount(int amount)
    {
        m_availableMen += amount;
        _maxMenCount += amount;
        UpdateResource();
    }

    public bool TryGetResourcePerSecond(ResourceType type, out float resourcePerSecond)
    {
        if (m_resourcePerSecond.TryGetValue(type, out int rps))
        {
            resourcePerSecond = rps * _rpsUpgrade;
            return true;
        }

        resourcePerSecond = 0;
        return false;
    }

    public bool TrySendCourier(Customer customer)
    {
        var uiManager = GameCore.Instance.Get<UIManager>();

        if (customer == null || customer.Order?.Resources == null) {
            return false;
        }
        
        if (m_availableMen > 0) {
            var succesVerifyCount = 0;

            foreach (var orderResource in customer.Order.Resources) {
                if (m_resources[orderResource.Type] >= orderResource.Amount) {
                    succesVerifyCount++;
                }
                else
                {
                    uiManager.ShowRedResource(orderResource.Type);
                }
            }

            if (succesVerifyCount == customer.Order.Resources.Count) {

                foreach (var orderResource in customer.Order.Resources) {
                    if (m_resources[orderResource.Type] >= orderResource.Amount) {
                        m_resources[orderResource.Type] -= orderResource.Amount;
                    }
                }

                GameCore.Instance.Get<MapManager>().LaunchMan(this, customer, customer.Order.Resources);
                m_availableMen--;
                UpdateResource();

                GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Enough);
                return true;
            }
        }
        else
        {
            uiManager.ShowRedMan();
        }

        return false;
    }

    public bool TrySendWorker(ResourcePlace resPlace) {
        if (m_availableMen > 0)
        {
            GameCore.Instance.Get<MapManager>().LaunchMan(this, resPlace, null);
            m_availableMen--;
            UpdateResource();
            GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Enough);
            return true;
        }

        GameCore.Instance.Get<UIManager>().ShowRedMan();
        return false;
    }

    public override void Job(Man man) {
        m_availableMen++;
        if (man.Resources != null) {
            foreach (var resource in man.Resources) {
                m_resources[resource.Type] += resource.Amount;
            }
            _resourceBalloon.Show(man.Resources);
        }

        GameCore.Instance.Get<SoundManager>().PlaySound(SoundType.FX_Positive);

        UpdateResource();
        UpdateAllUpgradesView();
        OnManArrived?.Invoke();
    }

    private void UpdateResource() {
        GameCore.Instance.Get<UIManager>().UpdateResource(m_resources, m_availableMen, _maxMenCount);
        _canUpgrade.gameObject.SetActive(canUpgrade());
    }

    public void UpdateOrders() {
        CurrentDeliveryCount++;
        GameCore.Instance.Get<UIManager>().UpdateOrders(CurrentDeliveryCount);
    }
}