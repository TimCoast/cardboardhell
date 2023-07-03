using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinSystem : MonoBehaviour
{
    [SerializeField] private GameObject _coinText;


    private TextMeshProUGUI _coinTMP;
    private int _coinNumber;

    void Start()
    {
        SyncCoins();
        
        _coinTMP = _coinText.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        DisplayCoins();
    }

    public void AddCoins(int Number)
    {
        if (Number > 0)
        {
            _coinNumber += Number;

            PlayerPrefs.SetInt("coins", _coinNumber);
            PlayerPrefs.Save();
        }
    }

    public void TakeCoins(int Number)
    {
        if (Number > 0)
        {
            _coinNumber -= Number;

            PlayerPrefs.SetInt("coins", _coinNumber);
            PlayerPrefs.Save();
        }
    }

    public int CoinsAmount()
    {
        return _coinNumber;
    }

    public void SyncCoins()
    {
        _coinNumber = PlayerPrefs.GetInt("coins", 0);
    }

    private void DisplayCoins()
    {
        _coinTMP.text = _coinNumber.ToString();
    }

    public void GoldForAd()
    {
        float reward = 1000f * PlayerPrefs.GetFloat("earn", 1.00000f);

        AddCoins((int)reward);
    }
}
