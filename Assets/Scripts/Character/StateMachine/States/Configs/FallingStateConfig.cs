using System;
using UnityEngine;

[Serializable]
public class FallingStateConfig
{
    [SerializeField, Range(1, 5)] private float _gravityMultiplier;

    public float GravityMultiplier => _gravityMultiplier;
}
