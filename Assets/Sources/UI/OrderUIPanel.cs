using Sources.core;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class OrderUIPanel : MonoBehaviour, ICoreRegistrable
{
    [SerializeField] private SpriteAtlas m_resourceIcons;
    [Space]
    [SerializeField] private TextMeshProUGUI m_orderNameText;
    [SerializeField] private TextMeshProUGUI m_resourceAmountText;
    [SerializeField] private Image m_resourceImage;
    [SerializeField] private TextMeshProUGUI m_rewardAmountText;
    [SerializeField] private Image m_rewardImage;
    [SerializeField] private TextMeshProUGUI m_timerText;
    [SerializeField] private Button m_btn;

    public void SetView(Order order, int time)
    {
        m_orderNameText.text = order.OrderName;
        m_resourceAmountText.text = order.Resource.Amount.ToString();
        m_resourceImage.sprite = m_resourceIcons.GetSprite(nameof(order.Resource.Type));
        m_rewardAmountText.text = order.Reward.Amount.ToString();
        m_rewardImage.sprite = m_resourceIcons.GetSprite(nameof(ResourceType.Money));
        m_timerText.text = time.ToString();
    }
}