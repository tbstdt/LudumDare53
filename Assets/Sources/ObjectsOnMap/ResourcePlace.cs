using System;
using System.Collections;
using System.Collections.Generic;
using Sources.core;
using Sources.Editor.ObjectsOnMap;
using Sources.map;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourcePlace : ObjectOnMap
{
    [SerializeField] protected GameObject _view;
    [SerializeField] private TextMeshProUGUI _resourceCountText;
    [SerializeField] protected Resource _resource;
    
    [SerializeField] private List<Image> _graphic;
    [SerializeField] private float _scaleOnPMouse = 1.2f;

    private int _menInside = 0;

    private bool m_changeAmountByTutorial;

    public Resource Resource => _resource;

    public Action<ResourcePlace> OnManSend;

    private void Start() {
        _resourceCountText.text = _resource.Amount.ToString();
    }

    protected override void OnObjectClicked()
    {
        if (_resource.Amount <= 0) {
            return;
        }

        var hub = GameCore.Instance.Get<Hub>();
        if (hub.TrySendWorker(this))
            OnManSend?.Invoke(this);
    }

    public void ChangeAmount(int amount, bool byTutorial = false)
    {
        _resource.Amount = amount;
        m_changeAmountByTutorial = byTutorial;
        _resourceCountText.text = _resource.Amount.ToString();
    }

    public override void Job(Man man) {
        _menInside++;
        StartCoroutine(GatherResource());
    }

    public override void OnPointerEnter(PointerEventData eventData) {
        foreach (var image in _graphic) {
            image.transform.localScale = new Vector3(_scaleOnPMouse, _scaleOnPMouse, _scaleOnPMouse);
        }
    }

    public override void OnPointerExit(PointerEventData eventData) {
        foreach (var image in _graphic) {
            image.transform.localScale = Vector3.one;
        }
    }

    private IEnumerator GatherResource() {
        var hub = GameCore.Instance.Get<Hub>();
        float takenResourceAmount = 0;

        if (hub.TryGetResourcePerSecond(_resource.Type, out var resourcePerSecond)) {
            for (int i = 0; i < hub.TimeToWork; i++) {

                if (_resource.Amount <= 0) {
                    continue;
                }

                yield return new WaitForSeconds(1);
                if (_resource.Amount - takenResourceAmount >= 0) {
                    takenResourceAmount += resourcePerSecond;
                }
                else
                {
                    takenResourceAmount += _resource.Amount;
                    _resource.Amount = 0;
                }

                _resourceCountText.text = (_resource.Amount - Math.Round(takenResourceAmount)).ToString();
            }
        }

        var amount = Mathf.RoundToInt(takenResourceAmount);
        _resource.Amount -= amount;

        GameCore.Instance.Get<MapManager>().LaunchMan(this, hub, new List<Resource>{new (_resource.Type, amount)});
        _menInside--;

        if (_menInside == 0 && _resource.Amount < 0 && !m_changeAmountByTutorial)
            _view.SetActive(false);
    }
}