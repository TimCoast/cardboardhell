using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class RewardAD : MonoBehaviour
{
    private CoinSystem coinSystem;

    private void OnEnable() => YandexGame.RewardVideoEvent += Reward;
    private void OnDisable() => YandexGame.RewardVideoEvent -= Reward;

    private void Start()
    {
        coinSystem = GetComponent<CoinSystem>();
    }

    private void Reward(int ID)
    {
        coinSystem.GoldForAd();
    }

    public void ShowRewardAD(int ID)
    {
        YandexGame.RewVideoShow(ID);
    }
}