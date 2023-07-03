using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using YG;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameObject _timerText;
    [SerializeField] private GameObject _totalTimeText;
    [SerializeField] private GameObject _car;
    [SerializeField] private float _coinLevelMultiplier;
    [SerializeField] private bool _isBossLevel;

    private GameObject _handler;
    private CoinSystem _coinSystem;
    private ObjectMoveForvard _objectMoveForvard;
    private TextMeshProUGUI _timerTMPUGUI;
    private TextMeshProUGUI _totalTimeTMPUGUI;

    private float _earnMultiplier;
    private float _timeSpent;
    private string _formattedTimeSpent;
    private float _totalTime;
    private bool _running;

    void Start()
    {
        _earnMultiplier = PlayerPrefs.GetFloat("earn", 1.00000f);
        _running = true;
        _totalTime = 0f;
        _handler = GameObject.Find("Handler");
        _coinSystem = _handler.GetComponent<CoinSystem>();
        _timerTMPUGUI = _timerText.GetComponent<TextMeshProUGUI>();
        _totalTimeTMPUGUI = _totalTimeText.GetComponent<TextMeshProUGUI>();
        _objectMoveForvard = _car.GetComponent<ObjectMoveForvard>();
    }

    void Update()
    {
        if (_objectMoveForvard.enabled != true)
        {
            _timeSpent = Time.timeSinceLevelLoad;
            _formattedTimeSpent = string.Format("{0:0.00}", _timeSpent);
            _timerTMPUGUI.text = "" + _formattedTimeSpent;
        }
        else
        {
            if(_running == true)
            {
                _totalTime = _timeSpent;
                _totalTimeTMPUGUI.text = "Результат заезда: " + _formattedTimeSpent + " сек. " + "Монеты: +" + TimeToCoins();
                _timerText.SetActive(false);
                _coinSystem.AddCoins(TimeToCoins());
                if (_isBossLevel && _totalTime >= 80f)
                {
                    PlayerPrefs.SetInt("isBossBeaten", 1);
                    _coinSystem.AddCoins(2000);
                    _totalTimeTMPUGUI.text = "Уровень босса пройден! Награда за прохождение: 2000. Теперь ты есть в таблице рекордов.\n" + "Результат заезда: " + _formattedTimeSpent + " сек. " + "Монеты: +" + TimeToCoins();
                    YandexGame.NewLeaderboardScores("BossTime", (int)_totalTime);
                }
                _running = false;
            }
        }
    }

    private int TimeToCoins()
    {
        float coins = (_totalTime - 10) * _coinLevelMultiplier * _earnMultiplier;

        if (coins < 0)
            coins = 0;

        return (int)coins;
    }
}