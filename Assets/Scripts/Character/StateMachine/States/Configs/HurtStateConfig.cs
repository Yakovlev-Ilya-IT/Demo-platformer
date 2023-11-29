using System;
using UnityEngine;

[Serializable]
public class HurtStateConfig
{
    [SerializeField, Range(0, 40)] private float _startYVelocity;
    [SerializeField, Range(0, 40)] private float _startXVelocity;
    [SerializeField, Range(3, 10)] private float _brakingMultiplier;
    [SerializeField, Range(5, 150)] private float _baseGravity;

    public float StartYVelocity => _startYVelocity;
    public float StartXVelocity => _startXVelocity;
    public float BrakingMultiplier => _brakingMultiplier;
    public float BaseGravity => _baseGravity;
}
