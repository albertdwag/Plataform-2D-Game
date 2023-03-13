using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Core.Singleton;
using TMPro;


public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _coinsText;

    public void UpdateText()
    {
        _coinsText.text = "x" + ItemManager.Instance.coins.ToString();
    }
}
