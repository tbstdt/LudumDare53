using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradesData", menuName = "create " + nameof(UpgradesDataSO))]
public class UpgradesDataSO : ScriptableObject
{
    public List<UpgradeData> SpeedUpgrades;
    public List<UpgradeData> WeightUpgrades;
    public List<UpgradeData> MenUpgrades;
}

[Serializable]
public struct UpgradeData
{
    public int Cost;
    public float ValueChange;
}