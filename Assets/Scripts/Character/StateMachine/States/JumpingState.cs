using UnityEngine.InputSystem;

public class JumpingState : AirbornState
{
    private JumpingStateConfig _config;
    private ObstacleCollisionChecker _ceilChecker;

    private bool _isJumpButtonCanceled;

    private readonly float _startJumpVelocity;

    public JumpingState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    {
        _ceilChecker = character.CeilChecker;
        _config = character.Config.AirborneStateConfig.JumpingStateConfig;
        _startJumpVelocity = _config.StartYVelocity;
    }

    private bool CanMoveUp => (_isJumpButtonCanceled || _ceilChecker.IsTouches) == false;

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartJumping();

        Data.Speed = _config.Speed;
        Data.YVeloicty = _startJumpVelocity;

        _isJumpButtonCanceled = false;
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopJumping();
    }

    public override void Update()
    {
        base.Update();

        if (Data.YVeloicty < 0)
            StateSwitcher.SwitchState<FallingState>();
    }

    protected override void AddInputActionsCallbacks()
    {
        base.AddInputActionsCallbacks();

        Input.Movement.Jump.canceled += OnJumpKeyCanceled;
    }

    protected override void RemoveInputActionsCallbacks()
    {
        base.RemoveInputActionsCallbacks();

        Input.Movement.Jump.canceled -= OnJumpKeyCanceled;
    }

    protected override float GetGravityMultiplier()
    {
        if (CanMoveUp)
            return base.GetGravityMultiplier();

        return base.GetGravityMultiplier() * _config.BrakingMultiplier;
    }

    private void OnJumpKeyCanceled(InputAction.CallbackContext obj)
    {
        _isJumpButtonCanceled = true;

        Input.Movement.Jump.canceled -= OnJumpKeyCanceled;
    }
}
