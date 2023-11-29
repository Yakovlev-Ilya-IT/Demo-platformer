using System;
using UnityEngine;

[Serializable]
public class JumpingStateConfig
{
    [SerializeField, Range(0, 10)] private float _speed;
    [SerializeField, Range(0, 10)] private float _maxHeight;
    [SerializeField, Range(0, 10)] private float _timeToReachMaxHeight;
    [SerializeField, Range(3, 100)] private float _brakingMultiplier;

    public float StartYVelocity => 2 * _maxHeight / _timeToReachMaxHeight;
    public float Speed => _speed;
    public float MaxHeight => _maxHeight;
    public float TimeToReachMaxHeight => _timeToReachMaxHeight;
    public float BrakingMultiplier => _brakingMultiplier;
}
