using UnityEngine;

public abstract class AirbornState : MovementState
{
    private readonly float _baseGravity;

    public AirbornState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character) : base(stateSwitcher, data, character)
    {
        _baseGravity = character.Config.AirborneStateConfig.BaseGravity;
    }

    public override void Enter()
    {
        base.Enter();

        CharacterView.StartAirborne();
    }

    public override void Exit()
    {
        base.Exit();

        CharacterView.StopAirborne();
    }

    public override void Update()
    {
        base.Update();

        Data.YVeloicty -= GetGravityMultiplier() * Time.deltaTime;
    }

    protected virtual float GetGravityMultiplier() => _baseGravity;
}
