using System;
using UnityEngine;

public class ObstacleCollisionChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _ground;

    [SerializeField, Range(0.01f, 1)] private float _distanceToCheck;

    public bool IsTouches { get; private set; }

    private void Update() => IsTouches = Physics.Raycast(transform.position, transform.up, _distanceToCheck, _ground);
}
