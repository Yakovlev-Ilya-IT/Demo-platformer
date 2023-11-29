using System;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Health))]
public class Character : MonoBehaviour
{
    public event Action ReceivedDamage;
    public event Action Died;

    [SerializeField] private CharacterConfig _config;
    [SerializeField] private CharacterView _view;
    [SerializeField] private ObstacleCollisionChecker _ceilChecker;
    [SerializeField] private ObstacleCollisionChecker _groundChecker;
    [SerializeField] private ObstacleCollisionChecker _leftWallChecker;
    [SerializeField] private ObstacleCollisionChecker _rightWallChecker;

    private Health _health;
    private PlayerInput _input;
    private CharacterStateMachine _stateMachine;
    private CharacterController _characterController;

    public PlayerInput Input => _input;
    public ObstacleCollisionChecker CeilChecker => _ceilChecker;
    public ObstacleCollisionChecker GroundChecker => _groundChecker;
    public ObstacleCollisionChecker LeftWallChecker => _leftWallChecker;
    public ObstacleCollisionChecker RightWallChecker => _rightWallChecker;
    public CharacterController Controller => _characterController;
    public CharacterConfig Config => _config;
    public CharacterView View => _view;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _characterController = GetComponent<CharacterController>();
        _input = new PlayerInput();
        _stateMachine = new CharacterStateMachine(this);

        _health.Initialize();
    }

    private void Update()
    {
        _stateMachine.HandleInput();

        _stateMachine.Update();
    }

    private void OnEnable()
    {
        _input.Enable();

        _health.ReceivedDamage += OnReceivedDamage;
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _input.Disable();

        _health.ReceivedDamage -= OnReceivedDamage;
        _health.Died -= OnDied;
    }

    private void OnDied() => Died?.Invoke();

    private void OnReceivedDamage() => ReceivedDamage?.Invoke();
}
