using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyTween : HCMonobehavior
{
    [SerializeField] private TMP_Text moneyText;
    
    private int _currentMoney;

    // Start is called before the first frame update
    void Start()
    {
        _currentMoney = PlayerPrefs.GetInt("diamond");
        moneyText.text = _currentMoney.ToFormatString();
    }

    private void OnEnable()
    {
        EventGlobalManager.Instance.OnMoneyChange.AddListener(UpdateMoney);
    }

    private void OnDisable()
    {
        if (EventGlobalManager.Instance)
            EventGlobalManager.Instance.OnMoneyChange.RemoveListener(UpdateMoney);
    }

    void UpdateMoney(bool success)
    {
        DOTween.Kill(this);
        if (success)
        {
            int tmp = _currentMoney;
            DOTween.To(() => tmp, UpdateMoneyText, PlayerPrefs.GetInt("diamond"), .2f)
                .SetEase(Ease.Linear).SetTarget(this);
        }
        else
        {
            // Error anim
        }
    }

    void UpdateMoneyText(int money)
    {
        _currentMoney = money;
        moneyText.text = money.ToFormatString();
    }
}
