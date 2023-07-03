using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CarHealthSystem : MonoBehaviour
{
    private int _carHealth;
    private int _carMinHealth = 0;
    private int _carMaxHealth;

    [SerializeField] private GameObject _carHealthTextObject;
    [SerializeField] private GameObject _resultPanel;

    private TextMeshPro _CarHealthText;
    private BoxDamageSystem _boxDamageSystem;
    private ObjectMoveForvard _objectMoveForvard;
    private int _armor;

    private void OnCollisionEnter(Collision collision)
    {
        _boxDamageSystem = collision.gameObject.GetComponent<BoxDamageSystem>();

        if (_boxDamageSystem != null)
        {
            _carHealth -= (_boxDamageSystem._boxDamage - _armor);

            if (PlayerPrefs.GetInt("blastUpgrade", 0) == 1)
            Blast();
        }
    }

    void Start()
    {
        _armor = PlayerPrefs.GetInt("armor", 0);
        _carMaxHealth = PlayerPrefs.GetInt("health", 10);
        _carHealth = _carMaxHealth;

        _objectMoveForvard = GetComponent<ObjectMoveForvard>();
        _CarHealthText = _carHealthTextObject.GetComponent<TextMeshPro>();

        _objectMoveForvard.enabled = false;
    }

    void Update()
    {
        _carHealth = Mathf.Clamp(_carHealth, 0, 4000);

        if (_carHealth == _carMinHealth)
        {
            CrashCar();
        }

        _CarHealthText.text = _carHealth.ToString();
    }

    public void GiveArmor()
    {
        PlayerPrefs.SetInt("armor", 2);
    }

    public void ResetHealth()
    {
        _carHealth = PlayerPrefs.GetInt("health", 10);
    }

    public void CrashCar()
    {
        _objectMoveForvard.enabled = true;
        _resultPanel.SetActive(true);
    }

    public void TakePeriodicDamage(int damage)
    {
        _carHealth -= damage;
    }

    public void Blast()
    {
        Debug.Log("Blast!");
        float blastRadius = 7f;
        float blastForce = 100f;
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(blastForce, transform.position, blastRadius, 0f, ForceMode.Impulse);
            }
        }
    }
}