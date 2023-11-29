public class RunningState : GroundedState
{
    private RunningStateConfig _config;

    public RunningState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    {
        _config = character.Config.RunningStateConfig;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartRunning();

        Data.Speed = _config.Speed;
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopRunning();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            StateSwitcher.SwitchState<IdlingState>();
    }
}
