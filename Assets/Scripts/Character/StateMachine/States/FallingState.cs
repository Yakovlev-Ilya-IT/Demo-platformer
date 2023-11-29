public class FallingState : AirbornState
{
    private FallingStateConfig _config;
    private ObstacleCollisionChecker _groundChecker;

    public FallingState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    {
        _config = character.Config.AirborneStateConfig.FallingStateConfig;
        _groundChecker = character.GroundChecker;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartFalling();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopFalling();
    }

    public override void Update()
    {
        base.Update();

        if (_groundChecker.IsTouches)
        {
            Data.YVeloicty = 0;

            if (IsHorizontalInputZero())
                StateSwitcher.SwitchState<IdlingState>();
            else
                StateSwitcher.SwitchState<RunningState>();
        }
    }

    protected override float GetGravityMultiplier() => base.GetGravityMultiplier() * _config.GravityMultiplier;
}
