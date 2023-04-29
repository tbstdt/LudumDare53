using System;
using System.Collections.Generic;
using System.Collections;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Customer : ObjectOnMap
{
    [SerializeField] private TextMeshProUGUI m_text;
    [Space]
    [SerializeField] private int m_timerInSeconds = 30;
    [SerializeField] private TextMeshProUGUI m_timerTime;
    [SerializeField] private Image m_timerView;

    public Order Order { get; private set; }

    public override ObjectType Type => ObjectType.Customer;

    private void Start()
    {
        var amount = Random.Range(0, 10);
        Order = new Order(amount);
        m_text.text = amount.ToString();

        if (m_timerInSeconds <= 0)
            return;

        if (m_timerTime == null || m_timerView == null)
            return;

        StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        float time = m_timerInSeconds;

        while (time > 0)
        {
            time -= Time.deltaTime;

            m_timerTime.text = Math.Ceiling(time).ToString();
            m_timerView.fillAmount = time / m_timerInSeconds;

            yield return null;
        }

        OnTimerEnd();
    }

    private void OnTimerEnd()
    {
        Destroy(gameObject);
    }

    protected override void OnObjectClicked()
    {
        var hub = GameCore.Instance.Get<Hub>();
        hub.TrySendCourier(this);
    }

    public override void Job(Man man) {
       
    }
}