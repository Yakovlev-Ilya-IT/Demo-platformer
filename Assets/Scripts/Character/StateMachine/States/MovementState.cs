using System;
using UnityEngine;

public abstract class MovementState : IState
{
    protected readonly IStationStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;

    private readonly Character _character;

    public MovementState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        _character = character;
    }

    protected PlayerInput Input => _character.Input;
    protected CharacterView CharacterView => _character.View;
    protected CharacterController CharacterController => _character.Controller;
    private Quaternion TurnRight => new Quaternion(0, 0, 0, 0);
    private Quaternion TurnLeft => Quaternion.Euler(0, 180, 0);

    public virtual void Enter()
    {
        CharacterView.StartMovement();
        _character.ReceivedDamage += OnReceivedDamage;
        _character.Died += OnDied;
        AddInputActionsCallbacks();
    }

    public virtual void Exit()
    {
        CharacterView.StopMovement();
        _character.ReceivedDamage -= OnReceivedDamage;
        _character.Died -= OnDied;
        RemoveInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        Data.XInput = ReadHorizontalInput();
        Data.XVelocity = Data.XInput * Data.Speed;
    }

    public virtual void Update()
    {
        Vector3 velocity = GetConvertedVelocity();

        CharacterController.Move(velocity * Time.deltaTime);
        _character.transform.rotation = GetRotationFrom(velocity);
    }

    protected virtual void AddInputActionsCallbacks() { }

    protected virtual void RemoveInputActionsCallbacks() { }

    protected bool IsHorizontalInputZero() => Data.XInput == 0;

    private float ReadHorizontalInput() => Input.Movement.Move.ReadValue<float>();
    private Vector3 GetConvertedVelocity() => new Vector3(Data.XVelocity, Data.YVeloicty, 0);

    private Quaternion GetRotationFrom(Vector3 velocity)
    {
        if(velocity.x > 0)
            return TurnRight;

        if(velocity.x < 0)
            return TurnLeft;

        return _character.transform.rotation;
    }

    private void OnReceivedDamage() => StateSwitcher.SwitchState<HurtState>();

    private void OnDied() => StateSwitcher.SwitchState<DeathState>();
}
