using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoveForvard : MonoBehaviour
{
    private Rigidbody _rigidbody;

    [SerializeField] private GlobalObjectMoving _globalObjectMoving;

    [SerializeField] private GameObject _handler;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _handler = GameObject.Find("Handler");
        _globalObjectMoving = _handler.GetComponent<GlobalObjectMoving>();

        Destroy(gameObject, 30f);
    }

    void FixedUpdate()
    {
        _globalObjectMoving.MoveForvard(_rigidbody);

        if(this.transform.position.z < -40 || this.transform.position.y < - 25)
        {
            Destroy(gameObject);
        }
    }
}