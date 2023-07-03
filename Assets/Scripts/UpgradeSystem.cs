using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeSystem : MonoBehaviour
{
    [SerializeField] private GameObject _healthAmountText;
    [SerializeField] private GameObject _healthCostText;
    [SerializeField] private GameObject _speedAmountText;
    [SerializeField] private GameObject _speedCostText;
    [SerializeField] private GameObject _earnAmountText;
    [SerializeField] private GameObject _earnCostText;
    [SerializeField] private GameObject _armorCostText;
    [SerializeField] private GameObject _jumpCostText;
    [SerializeField] private GameObject _blastCostText;
    [SerializeField] private GameObject _bossCostText;
    [SerializeField] private GameObject _bossLevelButtonText;
    [SerializeField] private GameObject _goldForAdText;
    [SerializeField] private GameObject _car;

    private CarHealthSystem _carHealthSystem;
    private PlayerMove _playerMove;
    private CoinSystem _coinSystem;
    private TextMeshProUGUI _healthCostTMP;
    private TextMeshProUGUI _healthAmountTMP;
    private TextMeshProUGUI _speedAmountTMP;
    private TextMeshProUGUI _speedCostTMP;
    private TextMeshProUGUI _earnAmountTMP;
    private TextMeshProUGUI _earnCostTMP;
    private TextMeshProUGUI _armorCostTMP;
    private TextMeshProUGUI _jumpCostTMP;
    private TextMeshProUGUI _blastCostTMP;
    private TextMeshProUGUI _bossCostTMP;
    private TextMeshProUGUI _bossLevelButtonTMP;
    private TextMeshProUGUI _goldForAdTMP;

    private int _currentMaxHealth;
    private int _healthUpgradeCost;
    private int _currentMaxSpeed;
    private int _speedUpgradeCost;
    private float _currentEarn;
    private int _earnUpgradeCost;
    private int _armorUpgradeCost;
    private int _jumpUpgradeCost;
    private int _blastUpgradeCost;
    private int _bossUpgradeCost;
    private float _goldForAd;

    private bool _isArmorUpgrade;
    private bool _isJumpUpgrade;
    private bool _isBlastUpgrade;
    private bool _isBossUpgrade;


    private void Start()
    {
        _carHealthSystem = _car.GetComponent<CarHealthSystem>();
        _playerMove = _car.GetComponent<PlayerMove>();
        _coinSystem = GetComponent<CoinSystem>();
        _healthAmountTMP = _healthAmountText.GetComponent<TextMeshProUGUI>();
        _healthCostTMP = _healthCostText.GetComponent<TextMeshProUGUI>();
        _speedAmountTMP = _speedAmountText.GetComponent<TextMeshProUGUI>();
        _speedCostTMP = _speedCostText.GetComponent<TextMeshProUGUI>();
        _earnAmountTMP = _earnAmountText.GetComponent<TextMeshProUGUI>();
        _earnCostTMP = _earnCostText.GetComponent<TextMeshProUGUI>();
        _armorCostTMP = _armorCostText.GetComponent<TextMeshProUGUI>();
        _jumpCostTMP = _jumpCostText.GetComponent<TextMeshProUGUI>();
        _blastCostTMP = _blastCostText.GetComponent<TextMeshProUGUI>();
        _bossCostTMP = _bossCostText.GetComponent<TextMeshProUGUI>();
        _bossLevelButtonTMP = _bossLevelButtonText.GetComponent<TextMeshProUGUI>();
        _goldForAdTMP = _goldForAdText.GetComponent<TextMeshProUGUI>();
        

        _armorUpgradeCost = 500;
        _jumpUpgradeCost = 850;
        _blastUpgradeCost = 1200;
        _bossUpgradeCost = 4000;
        _goldForAd = 1000f;

        CheckUpgrades();
        DisplayAllButtons();
    }

    public void UpgradeHealth()
    {
        if (_coinSystem.CoinsAmount() >= _healthUpgradeCost)
        {
            _coinSystem.TakeCoins(_healthUpgradeCost);
            _healthUpgradeCost += 5;
            PlayerPrefs.SetInt("healthCost", _healthUpgradeCost);

            _currentMaxHealth += 10;
            PlayerPrefs.SetInt("health", _currentMaxHealth);

            _carHealthSystem.ResetHealth();
            DisplayAllButtons();
        }
    }

    public void UpgradeSpeed()
    {
        if (_coinSystem.CoinsAmount() >= _speedUpgradeCost)
        {
            _coinSystem.TakeCoins(_speedUpgradeCost);
            _speedUpgradeCost += 120;
            PlayerPrefs.SetInt("speedCost", _speedUpgradeCost);

            _currentMaxSpeed += 1;
            PlayerPrefs.SetInt("MoveSpeed", _currentMaxSpeed);

            DisplayAllButtons();
        }
    }

    public void UpgradeEarn()
    {
        if (_coinSystem.CoinsAmount() >= _earnUpgradeCost)
        {
            _coinSystem.TakeCoins(_earnUpgradeCost);
            _earnUpgradeCost += 80;
            PlayerPrefs.SetInt("earnCost", _earnUpgradeCost);

            _currentEarn += 0.200000f;
            PlayerPrefs.SetFloat("earn", _currentEarn);

            DisplayAllButtons();
        }
    }

    public void UpgradeArmor()
    {
        if (_coinSystem.CoinsAmount() >= _armorUpgradeCost)
        {
            if (!_isArmorUpgrade)
            {
                _coinSystem.TakeCoins(_armorUpgradeCost);
                _carHealthSystem.GiveArmor();
                PlayerPrefs.SetInt("armorUpgrade", 1);
                CheckUpgrades();

                DisplayAllButtons();
            }
        }
    }

    public void UpgradeJump()
    {
        if (_coinSystem.CoinsAmount() >= _jumpUpgradeCost)
        {
            if (!_isJumpUpgrade)
            {
                _coinSystem.TakeCoins(_jumpUpgradeCost);
                PlayerPrefs.SetInt("jumpUpgrade", 1);
                CheckUpgrades();

                DisplayAllButtons();
            }
        }
    }

    public void UpgradeBlast()
    {
        if (_coinSystem.CoinsAmount() >= _blastUpgradeCost)
        {
            if (!_isBlastUpgrade)
            {
                _coinSystem.TakeCoins(_blastUpgradeCost);
                PlayerPrefs.SetInt("blastUpgrade", 1);
                CheckUpgrades();

                DisplayAllButtons();
            }
        }
    }

    public void UpgradeBoss()
    {
        if (_coinSystem.CoinsAmount() >= _bossUpgradeCost)
        {
            if (!_isBossUpgrade)
            {
                _coinSystem.TakeCoins(_bossUpgradeCost);
                PlayerPrefs.SetInt("bossUpgrade", 1);
                CheckUpgrades();

                DisplayAllButtons();
            }
        }
    }

    private void CheckUpgrades()
    {
        int ArmorUpgrade = PlayerPrefs.GetInt("armorUpgrade", 0);

        if (ArmorUpgrade == 1)
            _isArmorUpgrade = true;
        else
            _isArmorUpgrade = false;

        int JumpUpgrade = PlayerPrefs.GetInt("jumpUpgrade", 0);

        if (JumpUpgrade == 1)
            _isJumpUpgrade = true;
        else
            _isJumpUpgrade = false;

        int BlastUpgrade = PlayerPrefs.GetInt("blastUpgrade", 0);

        if (BlastUpgrade == 1)
            _isBlastUpgrade = true;
        else
            _isBlastUpgrade = false;

        int BossUpgrade = PlayerPrefs.GetInt("bossUpgrade", 0);

        if (BossUpgrade == 1)
            _isBossUpgrade = true;
        else
            _isBossUpgrade = false;
    }

    void DisplayAllButtons()
    {
        _currentMaxHealth = PlayerPrefs.GetInt("health", 10);
        _healthUpgradeCost = PlayerPrefs.GetInt("healthCost", 50);
        _healthAmountTMP.text = _currentMaxHealth.ToString() + "HP";
        _healthCostTMP.text = _healthUpgradeCost.ToString();

        _currentMaxSpeed = PlayerPrefs.GetInt("MoveSpeed", 2);
        _speedUpgradeCost = PlayerPrefs.GetInt("speedCost", 10);
        _speedAmountTMP.text = _currentMaxSpeed.ToString();
        _speedCostTMP.text = _speedUpgradeCost.ToString();

        _currentEarn = PlayerPrefs.GetFloat("earn", 1f);
        _earnUpgradeCost = PlayerPrefs.GetInt("earnCost", 80);
        _earnAmountTMP.text = _currentEarn.ToString() + "X";
        _earnCostTMP.text = _earnUpgradeCost.ToString();
        _goldForAdTMP.text = "+" + (_goldForAd * PlayerPrefs.GetFloat("earn", 1.0000f)).ToString();

        if (_isArmorUpgrade)
            _armorCostTMP.text = "Куплено!";
        else
            _armorCostTMP.text = _armorUpgradeCost.ToString();

        if (_isJumpUpgrade)
            _jumpCostTMP.text = "Куплено!";
        else
            _jumpCostTMP.text = _jumpUpgradeCost.ToString();

        if (_isBlastUpgrade)
            _blastCostTMP.text = "Куплено!";
        else
            _blastCostTMP.text = _blastUpgradeCost.ToString();

        if (_isBossUpgrade)
        {
            _bossCostTMP.text = "Открыто.";
            _bossLevelButtonTMP.text = "Нажми чтобы зайти.";
        }
        else
        {
            _bossCostTMP.text = _bossUpgradeCost.ToString();
            _bossLevelButtonTMP.text = "Разблокируй в разделе спец. улучшений";
        }
        if(PlayerPrefs.GetInt("isBossBeaten", 0) == 1)
        {
            _bossLevelButtonTMP.text = "Пройдено!";
        }

        PlayerPrefs.Save();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();

        _carHealthSystem.ResetHealth();
        _coinSystem.SyncCoins();
        CheckUpgrades();
        DisplayAllButtons();
    }
}