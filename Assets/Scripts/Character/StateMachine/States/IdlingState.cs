public class IdlingState : GroundedState
{
    public IdlingState(IStationStateSwitcher stateSwitcher, StateMachineData movementData, Character character) : base(stateSwitcher, movementData, character)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartIdling();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopIdling();
    }

    public override void Update()
    {
        base.Update();

        if (IsHorizontalInputZero())
            return;

        StateSwitcher.SwitchState<RunningState>();
    }
}
