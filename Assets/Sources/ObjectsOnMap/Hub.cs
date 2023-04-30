using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.Editor.UI;
using Sources.map;
using UnityEngine;

public class Hub : ObjectOnMap, ICoreRegistrable {
    [SerializeField] private int _manCount = 3;
    [SerializeField] private float _manSpeed = 3f;
    [SerializeField] private UpgradePanel _upgradePanel;
    [SerializeField] private UpgradesDataSO _upgradesData;

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
        {ResourceType.Two, 2},
        {ResourceType.Three, 3},
    };

    public float ManSpeed => _manSpeed / _speedUpgrade;

    public Dictionary<ResourceType, int> Resources => m_resources;

    public override ObjectType Type => ObjectType.Hub;

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

    private void GetViewData(out bool btnActive, out int cost, out bool interactable, int index, List<UpgradeData> data)
    {
        btnActive = index < data.Count;
        cost = btnActive ? data[index].Cost : -1;
        interactable = btnActive && m_resources[ResourceType.Money] >= data[index].Cost;
    }

    private void SpeedUpgradeHandler()
    {
        m_resources[ResourceType.Money] -= _upgradesData.SpeedUpgrades[_curSpeedUpgradeIndex].Cost;
        _speedUpgrade = _upgradesData.SpeedUpgrades[_curSpeedUpgradeIndex].ValueChange;
        _curSpeedUpgradeIndex++;
        UpdateSpeedUpgrade();
        UpdateResource();
    }

    private void WeightUpgradeHandler()
    {
        m_resources[ResourceType.Money] -= _upgradesData.WeightUpgrades[_curWeightUpgradeIndex].Cost;
        _rpsUpgrade = _upgradesData.WeightUpgrades[_curWeightUpgradeIndex].ValueChange;
        _curWeightUpgradeIndex++;
        UpdateWeightUpgrade();
        UpdateResource();
    }

    private void MenUpgradeHandler()
    {
        m_resources[ResourceType.Money] -= _upgradesData.MenUpgrades[_curMenUpgradeIndex].Cost;
        m_availableMen += (int)_upgradesData.MenUpgrades[_curMenUpgradeIndex].ValueChange;
        _maxMenCount += (int)_upgradesData.MenUpgrades[_curMenUpgradeIndex].ValueChange;
        _curMenUpgradeIndex++;
        UpdateMenUpgrade();
        UpdateResource();
    }

    #endregion

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
        var orderResource = customer.Order.Resource;

        if (m_resources[orderResource.Type] >= orderResource.Amount)
        {
            m_resources[orderResource.Type] -= orderResource.Amount;
            GameCore.Instance.Get<MapManager>().LaunchMan(this, customer, customer.Order.Resource);
            m_availableMen--;
            UpdateResource();
            return true;
        }

        return false;
    }

    public void TrySendWorker(ResourcePlace resPlace) {
        if (m_availableMen > 0)
        {
            GameCore.Instance.Get<MapManager>().LaunchMan(this, resPlace, null);
            m_availableMen--;
            UpdateResource();
        }
    }

    public override void Job(Man man) {
        m_availableMen++;
        if (man.Resource != null) {
            m_resources[man.Resource.Type] += man.Resource.Amount;
        }
        UpdateResource();
        UpdateAllUpgradesView();
    }

    private void UpdateResource() {
        GameCore.Instance.Get<UIManager>().UpdateResource(m_resources, m_availableMen, _maxMenCount);
    }
}