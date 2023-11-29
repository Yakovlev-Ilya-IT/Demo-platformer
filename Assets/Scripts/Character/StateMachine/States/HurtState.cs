using UnityEngine;

public class HurtState : IState
{
    protected readonly IStationStateSwitcher StateSwitcher;
    protected readonly StateMachineData Data;

    private readonly Character _character;

    public HurtState(IStationStateSwitcher stateSwitcher, StateMachineData data, Character character)
    {
        StateSwitcher = stateSwitcher;
        Data = data;
        _character = character;
    }

    private ObstacleCollisionChecker GroundChecker => _character.GroundChecker;
    private ObstacleCollisionChecker CeilChecker => _character.CeilChecker;
    private ObstacleCollisionChecker LeftWallChecker => _character.LeftWallChecker;
    private ObstacleCollisionChecker RightWallChecker => _character.RightWallChecker;
    private HurtStateConfig Config => _character.Config.HurtStateConfig;
    private CharacterView View => _character.View;

    public void Enter()
    {
        View.StartHurtFalling();

        Data.YVeloicty = Config.StartYVelocity;

        if (Data.XVelocity == 0)
            return;

        if(Data.XVelocity > 0)
            Data.XVelocity = -Config.StartXVelocity;
        else
            Data.XVelocity = Config.StartXVelocity;
    }

    public void Exit()
    {
        View.StopHurtFalling();
    }

    public void HandleInput() { }

    public void Update()
    {
        Data.YVeloicty -= GetGravity() * Time.deltaTime;

        _character.Controller.Move(GetConvertedVelocity() * Time.deltaTime);

        if (Data.YVeloicty <= 0 && GroundChecker.IsTouches)
        {
            Data.YVeloicty = 0;

            StateSwitcher.SwitchState<IdlingState>();
        }

        if(LeftWallChecker.IsTouches || RightWallChecker.IsTouches)
        {
            Data.YVeloicty = 0;
            Data.XVelocity = 0;

            StateSwitcher.SwitchState<FallingState>();
        }
    }

    private float GetGravity()
    {
        if(CeilChecker.IsTouches)
            return Config.BaseGravity * Config.BrakingMultiplier;

        return Config.BaseGravity;
    }

    private Vector3 GetConvertedVelocity() => new Vector3(Data.XVelocity, Data.YVeloicty, 0);
}
