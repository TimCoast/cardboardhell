using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjectMoving : MonoBehaviour
{
    [SerializeField] private float _worldSpeed;
    
    private Vector3 _moveDirection = Vector3.back;

    public void SetWorldSpeed(float WorldSpeed)
    {
        _worldSpeed = WorldSpeed;
    }

    public float GetWorldSpeed()
    {
        return _worldSpeed;
    }

    public void MoveForvard(Rigidbody rb)
    {
        rb.MovePosition(rb.position + _worldSpeed * Time.fixedDeltaTime * _moveDirection);
    }
}