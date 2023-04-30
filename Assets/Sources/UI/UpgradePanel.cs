using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_speedCost;
    [SerializeField] private TextMeshProUGUI m_weightCost;
    [SerializeField] private TextMeshProUGUI m_menCost;
    [Space]
    [SerializeField] private Button m_speedBtn;
    [SerializeField] private Button m_weightBtn;
    [SerializeField] private Button m_menBtn;

    public UnityEvent OnSpeedUpgradeClick;
    public UnityEvent OnWeightUpgradeClick;
    public UnityEvent OnMenUpgradeClick;

    private void Awake()
    {
        Hide();
    }

    public bool SwitchActive()
    {
        if (gameObject.activeSelf)
        {
            Hide();
            return false;
        }
        else
        {
            Show();
            return true;
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);

        m_speedBtn.onClick.AddListener(()=>OnSpeedUpgradeClick?.Invoke());
        m_weightBtn.onClick.AddListener(()=>OnWeightUpgradeClick?.Invoke());
        m_menBtn.onClick.AddListener(()=>OnMenUpgradeClick?.Invoke());
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        m_speedBtn.onClick.RemoveAllListeners();
        m_weightBtn.onClick.RemoveAllListeners();
        m_menBtn.onClick.RemoveAllListeners();
    }

    public void SetSpeedUpgrade(bool active, int cost = 0, bool interactable = true)
    {
        m_speedBtn.gameObject.SetActive(active);
        m_speedBtn.interactable = interactable;
        m_speedCost.text = cost.ToString();
    }

    public void SetWeightUpgrade(bool active, int cost = 0, bool interactable = true)
    {
        m_weightBtn.gameObject.SetActive(active);
        m_weightBtn.interactable = interactable;
        m_weightCost.text = cost.ToString();
    }

    public void SetMenUpgrade(bool active, int cost = 0, bool interactable = true)
    {
        m_menBtn.gameObject.SetActive(active);
        m_menBtn.interactable = interactable;
        m_menCost.text = cost.ToString();
    }
}