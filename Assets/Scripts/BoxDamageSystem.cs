using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDamageSystem : MonoBehaviour
{
    private int _basicBoxDamage;
    private float _boxMass;

    public int _boxDamage;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _basicBoxDamage = 1;
        _rigidbody = GetComponent<Rigidbody>();
        _boxMass = _rigidbody.mass;

        _boxDamage = _basicBoxDamage * (int)_boxMass;
    }
}
