using UnityEngine.InputSystem;

public abstract class GroundedState : MovementState
{
    private ObstacleCollisionChecker _groundChecker;

    public GroundedState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    {
        _groundChecker = character.GroundChecker;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartGrounded();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopGrounded();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches == false)
            StateSwitcher.SwitchState<FallingState>();
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        Input.Movement.Jump.started += OnJumpKeyPressed;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        Input.Movement.Jump.started -= OnJumpKeyPressed;
    }

    private void OnJumpKeyPressed(InputAction.CallbackContext obj) => StateSwitcher.SwitchState<JumpingState>();
}
