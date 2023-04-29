using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class OrderUIPanel : MonoBehaviour
{
    [SerializeField] private SpriteAtlas m_resourceIcons;
    [Space]
    [SerializeField] private TextMeshProUGUI m_orderNameText;
    [SerializeField] private TextMeshProUGUI m_resourceAmountText;
    [SerializeField] private Image m_resourceImage;
    [SerializeField] private TextMeshProUGUI m_rewardAmountText;
    [SerializeField] private TextMeshProUGUI m_timerText;


}