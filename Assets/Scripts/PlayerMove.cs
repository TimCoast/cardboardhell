using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject _scrollBarObject;
    [SerializeField] private bool _isBossLevel;

    private Rigidbody _rigidbody;
    private Scrollbar _scrollBar;

    private float _X_carAngle = 0f;
    private float _Y_carAngle = 7f;
    private float _Z_carAngle = 0f;
    private float _roadSideLeft;
    private float _roadSideRight;

    private int _startMoveSpeed = 2;
    private int _moveSpeed;

    private bool _isTouchSupport;

    private Vector3 _movement = Vector3.zero;
    private Quaternion carRotationOnMove;

    private ObjectMoveForvard _objectMoveForvard;
    private CarHealthSystem _carHealthSystem;

    private void OnEnable() => YandexGame.GetDataEvent += GetDeviceData;
    private void OnDisable() => YandexGame.GetDataEvent -= GetDeviceData;

    private void GetDeviceData()
    {
        _isTouchSupport = YandexGame.EnvironmentData.isMobile;
    }

    void Start()
    {
        if (_isTouchSupport)
            _scrollBarObject.SetActive(true);
        else
            _scrollBarObject.SetActive(false);

        if(_isBossLevel)
        {
            _roadSideLeft = -20f;
            _roadSideRight = 20f;
        }
        else
        {
            _roadSideLeft = -4.2f;
            _roadSideRight = 4.2f;
        }

        _scrollBar = _scrollBarObject.GetComponent<Scrollbar>();
        _carHealthSystem = GetComponent<CarHealthSystem>();
        _moveSpeed = PlayerPrefs.GetInt("MoveSpeed", _startMoveSpeed);
        _rigidbody = GetComponent<Rigidbody>();
        _objectMoveForvard = GetComponent<ObjectMoveForvard>();
        carRotationOnMove = Quaternion.identity;

        if (PlayerPrefs.GetInt("jumpUpgrade", 0) == 1)
        {
            if(!_isBossLevel)
            InvokeRepeating("Jump", 5f, 10f);
        }
    }

    void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.z = Input.GetAxis("Vertical");

        if(_isTouchSupport)
        _movement.x = _scrollBar.value * 2 - 1;

        carRotationOnMove = Quaternion.Euler(_X_carAngle * _movement.z, _Y_carAngle * _movement.x, _Z_carAngle * _movement.x);
    }

    private void FixedUpdate()
    {        
        _rigidbody.MovePosition(_rigidbody.position + _movement * _moveSpeed * Time.fixedDeltaTime);
        
        if (_objectMoveForvard.enabled != true)
        _rigidbody.position = new Vector3 (Mathf.Clamp(_rigidbody.position.x, _roadSideLeft, _roadSideRight), Mathf.Clamp(_rigidbody.position.y, -0.1f, 15f), Mathf.Clamp(_rigidbody.position.z, 8.3f, 82f));

        _rigidbody.MoveRotation(carRotationOnMove);

        if (_rigidbody.position.x > 5.4f || _rigidbody.position.x < -5.4f)
        {
            _carHealthSystem.TakePeriodicDamage(1);
        }
    }

    public void Jump()
    {
        Vector3 currentPosition = _rigidbody.position;
        Vector3 jumpForce = transform.up * 15f;

        if (currentPosition.y < 2f)
        {
            _rigidbody.AddForce(jumpForce, ForceMode.VelocityChange);
        }
    }

    public void PressLeftArrow()
    {
        //код который нажимает левую стрелку
    }
}