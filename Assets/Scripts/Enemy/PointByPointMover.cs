using System;
using System.Collections.Generic;
using UnityEngine;

public class PointByPointMover : MonoBehaviour
{
    private const float MinDistanceToTarget = 0.05f;

    [SerializeField] private Transform[] _targets;

    [SerializeField] private float _speed;

    private Queue<Transform> _targetsQueue;
    private Transform _currentTarget;

    private void Awake()
    {
        if (_targets.Length == 0)
            throw new InvalidOperationException(nameof(_targets));

        _targetsQueue = new Queue<Transform>();

        foreach (var target in _targets)
            _targetsQueue.Enqueue(target);

        SwitchTarget();
    }

    private void Update()
    {
        Vector3 direction = _currentTarget.position - transform.position;
        transform.Translate(direction.normalized * _speed * Time.deltaTime);

        if (direction.magnitude <= MinDistanceToTarget)
            SwitchTarget();
    }

    private void SwitchTarget()
    {
        if (_currentTarget != null)
            _targetsQueue.Enqueue(_currentTarget);

        _currentTarget = _targetsQueue.Dequeue();
    }
}
