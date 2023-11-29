using System;
using UnityEngine;

[Serializable]
public class AirborneStateConfig
{
    [SerializeField] private JumpingStateConfig _jumpingStateConfig;
    [SerializeField] private FallingStateConfig _fallingStateConfig;

    public JumpingStateConfig JumpingStateConfig => _jumpingStateConfig;
    public FallingStateConfig FallingStateConfig => _fallingStateConfig;

    public float BaseGravity => 2f * _jumpingStateConfig.MaxHeight / (_jumpingStateConfig.TimeToReachMaxHeight * _jumpingStateConfig.TimeToReachMaxHeight);
}
