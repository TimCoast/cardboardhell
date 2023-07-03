using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GlobalObjectMoving _globalObjectMoving;
    [SerializeField] private PrefabInstantiate _prefabInstantiate;
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _specialsPanel;
    [SerializeField] private GameObject _specialsHeaderPanel;

    private void Start()
    {
        _globalObjectMoving = GetComponent<GlobalObjectMoving>();
        _prefabInstantiate = GetComponent<PrefabInstantiate>();
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadEasyLevel()
    {
        SceneManager.LoadScene("EasyLevel");
    }

    public void LoadMiddleLevel()
    {
        SceneManager.LoadScene("MiddleLevel");
    }

    public void LoadHardLevel()
    {
        SceneManager.LoadScene("HardLevel");
    }

    public void LoadVeryHardLevel()
    {
        SceneManager.LoadScene("VeryHardLevel");
    }

    public void LoadBossLevel()
    {
        if (PlayerPrefs.GetInt("bossUpgrade", 0) == 1)
        {
            SceneManager.LoadScene("BossLevel");
        }
    }

    public void LoadInfoPanel()
    {
        if (PlayerPrefs.GetInt("bossUpgrade", 0) == 1)
        {
            _specialsHeaderPanel.SetActive(false);
            _specialsPanel.SetActive(false);
            _infoPanel.SetActive(true);
        }
    }
}